using System;
using System.Collections.Generic;
using UnityEngine;
using ClientCommon;
using LywGames.Messages;

/// <summary>
/// 请求管理器
/// </summary>
public class RequestManager : AbsManager<RequestManager>
{
    // 请求和响应集合
    private List<BaseRequest> requestList = new List<BaseRequest>();
    private List<BaseResponse> responseList = new List<BaseResponse>();

    // 执行请求和响应集合
    private List<BaseRequest> excRequestList = new List<BaseRequest>();
    private List<BaseResponse> excResponseList = new List<BaseResponse>();

    // 服务器业务处理器
    private ServerBusiness business;
    public ServerBusiness Business { get { return business; } }

    // 中断回调
    private Action<string> brokenDelegate;
    // 繁忙回调
    private Action<bool> busyDelegate;

    // Busy flag
    private int busyNumber;
    private int lastBusyNumber;

    public bool IsBusy
    {
        get { return busyNumber != 0; }
    }

    private const float cMaxRequestDelayTime = 3.0f;
    private float lastAddRequestTime = 0;

    private float responseTimeoutTime = 10f;
    private float lastRequestTime = float.MaxValue;

    private UiPnlTipIndicator indicator;

    public override void Initialize(params object[] parameters)
    {
        if (parameters == null || parameters.Length < 3)
        {
            return;
        }

        responseTimeoutTime = (float)parameters[0];
        brokenDelegate = (Action<string>)parameters[1];
        busyDelegate = (Action<bool>)parameters[2];

        busyNumber = 0;
        lastBusyNumber = 0;

        business = new ServerBusiness();
        business.Initialize();
    }

    public override void Dispose()
    {
        business.Dispose();
    }

    public override void OnUpdate()
    {
        // check time out
        if (responseTimeoutTime > 0.1f && Time.realtimeSinceStartup - lastRequestTime > responseTimeoutTime)
        {
            if (GameStateMachineManager.Instance.GetCurrentState().IsGamingState)
            {
                Broken(null);
            }
            return;
        }

        OnUpdate(Time.realtimeSinceStartup - lastAddRequestTime > cMaxRequestDelayTime);
    }

    private void OnUpdate(bool excAllRequest)
    {
        // reset busy flag
        lastBusyNumber = busyNumber;

        // update business
        if (business != null)
        {
            business.Update();
        }

        // update response
        UpdateResponse();

        // update request
        UpdateRequest(excAllRequest);

        // if busy change, notice outside
        if (lastBusyNumber != busyNumber)
        {
            CallBusyDelegate();
        }
    }

    private void UpdateRequest(bool excAllRequest)
    {
        excRequestList.Clear();

        for (int requestIdx = 0; requestIdx < requestList.Count; requestIdx++)
        {
            BaseRequest request = requestList[requestIdx];
            if (request.IsDiscarded || request.IsCombined || (request.IsExecuted && (request.HasResponse == false || request.IsResponded)))
            {
                continue;
            }

            excRequestList.Add(request);
        }

        for (int i = 0; i < excRequestList.Count; i++)
        {
            BaseRequest request = excRequestList[i];
            if (request.IsDiscarded || request.IsCombined || request.IsExecuted)
            {
                continue;
            }

            if (request.Combinable && i + 1 < excRequestList.Count)
            {
                BaseRequest nextRequest = excRequestList[i + 1];
                if (nextRequest.CombineWithPrevRequest(request))
                {
                    request.IsCombined = true;
                    continue;
                }
            }

            if (excAllRequest == false && request.Delayable)
            {
                continue;
            }

            for (int j = 0; j < i; j++)
            {
                BaseRequest prevRequest = excRequestList[i];
                if (prevRequest.IsDiscarded || prevRequest.IsCombined || prevRequest.IsExecuted)
                {
                    continue;
                }
                ExcRequest(prevRequest);
            }

            ExcRequest(request);
        }

        for (int i = 0; i < requestList.Count; i++)
        {
            BaseRequest request = requestList[i];
            if (request.IsDiscarded || request.IsCombined || (request.IsExecuted && (request.HasResponse == false || request.IsResponded)))
            {
                requestList.RemoveAt(i);
                --i;

                if (request.IsExecuteSuccess && request.HasResponse && request.WaitingResponse)
                {
                    busyNumber = Math.Max(busyNumber - 1, 0);
                }
            }
        }
    }

    private void UpdateResponse()
    {
        excResponseList.Clear();

        for (int responseIdx = 0; responseIdx < responseList.Count; responseIdx++)
        {
            BaseResponse response = responseList[responseIdx];
            if (response.IsExecuted)
            {
                continue;
            }
            excResponseList.Add(response);
        }

        for (int i = 0; i < excResponseList.Count; i++)
        {
            ExcResponse(excResponseList[i]);
        }

        responseList.RemoveAll((BaseResponse response) =>
        {
            if (response.IsExecuted)
            {
                return true;
            }
            return false;
        });
    }

    private void CallBusyDelegate()
    {
        if (busyDelegate != null)
        {
            busyDelegate(busyNumber > 0);
        }
    }

    public void RetainBusy()
    {
        busyNumber++;
        CallBusyDelegate();
    }

    public void ReleaseBusy()
    {
        busyNumber = Math.Max(busyNumber - 1, 0);
        CallBusyDelegate();
    }

    public bool IsWaitingResponse(Type requestType)
    {
        for (int i = 0; i < requestList.Count; i++)
        {
            BaseRequest request = requestList[i];
            if ((!request.IsDiscarded) && request.HasResponse && request.WaitingResponse && request.GetType() == requestType)
            {
                return true;
            }
        }
        return false;
    }

    private bool isWaitForRequest;
    public bool IsWaitForRequest
    {
        get { return isWaitForRequest; }
    }

    /// <summary>
	/// 是否上一个消息还未响应
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
    public bool HasPreviousUnResponsedReq(BaseRequest request)
    {
        return (request.HasResponse && request.WaitingResponse && request.MutuallyExclusive && IsWaitingResponse(request.GetType()));
    }

    public bool SendRequest(BaseRequest request, bool checkLoseConnection = true)
    {
        Message message = request.GetMessage();
        if (!request.NeedResponse)
        {
            if (message != null)
            {
                Business.SendMessage(message, 0, checkLoseConnection);
            }
            else
            {
                request.Execute(Business);
            }
            return true;
        }
        else
        {
            if (indicator == null)
            {
                indicator = (UiPnlTipIndicator)UiManager.Instance.ShowByName(UiPrefabNames.UiPnlTipIndicator);
            }
            if (!indicator.IsShowing)
            {
                indicator.Show();
            }

            isWaitForRequest = true;
            SetUiCameraEvents(false);
        }

        if (HasPreviousUnResponsedReq(request))
        {
            return false;
        }

        requestList.Add(request);

        lastAddRequestTime = Time.realtimeSinceStartup;
        if (request.HasResponse && request.CheckTimeout)
        {
            lastRequestTime = Time.realtimeSinceStartup;
        }

        return true;
    }

    public bool ReceiveResponse(BaseResponse response)
    {
        responseList.Add(response);

        BaseRequest request = FindRequest(response.CallBackId);
        if (request != null && request.HasResponse && request.CheckTimeout)
        {
            lastRequestTime = float.MaxValue;
        }

        if (request != null)
        {
            if (indicator == null)
            {
                indicator = (UiPnlTipIndicator)UiManager.Instance.ShowByName(UiPrefabNames.UiPnlTipIndicator);
            }
            if (indicator.IsShowing)
            {
                indicator.Hide();
            }

            isWaitForRequest = false;
            SetUiCameraEvents(true);
        }

        return true;
    }

    public void FlushAllRequest()
    {
        OnUpdate(true);
    }

    public void DiscardRequest(Type requestType)
    {
        foreach (var request in requestList)
        {
            if (request.GetType() == requestType)
            {
                request.IsDiscarded = true;
            }
        }
    }

    public void DiscardAllRequests()
    {
        foreach (var request in requestList)
        {
            request.IsDiscarded = true;
        }
        requestList.Clear();
    }

    public void Broken(string brokenMessage)
    {
        DiscardAllRequests();

        busyNumber = 0;
        lastBusyNumber = 0;
        lastRequestTime = float.MaxValue;

        if (brokenDelegate != null)
        {
            brokenDelegate(brokenMessage);
        }
    }

    private void SetUiCameraEvents(bool value)
    {
        enableInput = value;
    }

    private void ExcRequest(BaseRequest request)
    {
        try
        {
            request.IsExecuteSuccess = request.RunExecute(business);
        }
        catch (Exception e)
        {
            request.IsExecuteSuccess = false;
            request.IsDiscarded = true;
            LoggerManager.Instance.Error("ExcRequest Exception Message:" + e.Message + " \r\nException StackTrace:" + e.StackTrace);
        }
        finally
        {
            if ((!request.IsDiscarded) && (!request.IsExecuteSuccess) && (brokenDelegate != null))
            {
                brokenDelegate(null);
            }
            if (request.IsExecuteSuccess && request.HasResponse && request.WaitingResponse)
            {
                busyNumber++;
            }
        }
    }

    private void ExcResponse(BaseResponse response)
    {
        bool errOcc = false;
        BaseRequest request = null;
        if (response.CallBackId > 0) // 判断是否推送, 目前和gs约定 0 是推送。 -1是在登陆过程中发送的消息
        {
            request = FindRequest(response.CallBackId);
        }

        try
        {
            errOcc = !response.RunExecute(request);
        }
        catch (Exception e)
        {
            errOcc = true;
            LoggerManager.Instance.Error("ExcResponse Exception Message:" + e.Message + " \r\nException StackTrace:" + e.StackTrace);
        }
        finally
        {
            if (request != null && request.HasResponse)
            {
                request.IsResponded = true;
            }
        }
    }

    private BaseRequest FindRequest(int requestId)
    {
        if (requestId >= 0 && requestId != ConstValue.NullDataValue) // 负数和0作为网络层断线重连过程中登陆协议的id
        {
            for (int i = 0; i < requestList.Count; i++)
            {
                var request = requestList[i];
                if (request.CallBackId == requestId)
                {
                    return request;
                }
            }
        }

        return null;
    }

    private bool enableInput = true;
    /// <summary>
    /// 当前能否输入（点击等事件）。设置为true的时候可以输入，false的时候不能输入
    /// </summary>
    public bool EnableInput
    {
        get { return enableInput; }
        set
        {
            if (enableInput == value)
                return;

            enableInput = value;
        }
    }

}
