using System;
using LywGames.Messages;
using LywGames.Corgi.Protocol;

/// <summary>
/// 接收的协议基类
/// </summary>
public abstract class BaseResponse
{
    public virtual bool IsSucceed { get; set; }
    public virtual int ResultCode { get; set; } // 响应结果码
    public string ErrorMsg { get; set; } // 错误信息

    // 回调Id, 发送请求的时候携带了这个Id, 接收请求的时候服务器原封不动的把它返回了。
    private int callBackId;
    public virtual int CallBackId
    {
        set { callBackId = value; }
        get { return callBackId; }
    }

    // 是否已经执行过了
    private bool isExecuted;
    public bool IsExecuted
    {
        get { return isExecuted; }
    }

    public bool RunExecute(BaseRequest request)
    {
        if (IsExecuted)
        {
            LoggerManager.Instance.Error("Execute executed response " + ToString());
        }

        if (IsSucceed)
        {
            try
            {
                Execute(request);
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error(ex.Message);
            }
        }
        else
        {
            ExectueWithGsError(request);
#if UNITY_EDITOR
            LoggerManager.Instance.Warn("Execute response with server error: " + ResultCode);
#endif
            ErrorHandler(request, ResultCode, ErrorMsg);
        }

        isExecuted = true;

        return IsSucceed;
    }

    public abstract void Execute(BaseRequest request);

    public virtual void ExectueWithGsError(BaseRequest request)
    {

    }

    protected virtual void ErrorHandler(BaseRequest request, int resultCode, string errMsg)
    {
        AlertMessageManager.Instance.Show(resultCode);
    }

    public override string ToString()
    {
        return string.Format("Type:{0} CallBackId:{1} ErrorMsg:{2}", GetType(), CallBackId, ErrorMsg);
    }

}

public abstract class AbsResponse<T> : BaseResponse where T : Message
{
    protected T receiveMessage;

    public AbsResponse<T> InitMessage(T t)
    {
        receiveMessage = t;
        return this;
    }

    public override int CallBackId
    {
        get
        {
            if (receiveMessage != null)
            {
                return receiveMessage.CallBackId;
            }
            return 0;
        }
    }

    public override int ResultCode
    {
        get
        {
            if (receiveMessage != null)
            {
                return receiveMessage.ResultCode;
            }

            return 0;
        }
    }

    public override bool IsSucceed
    {
        get
        {
            return Protocols.isSuccess(ResultCode);
        }
    }

}
