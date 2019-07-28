using System;
using LywGames.Messages;
using LywGames.Corgi.Protocol;

/// <summary>
/// 接收的协议基类
/// </summary>
public abstract class BaseResponse
{
    public override string ToString()
    {
        return string.Format("{0} (RequestID:{1}) {2}", this.GetType(), requestID, errorContent);
    }

    public virtual bool isSucceed { get; set; }
    public virtual int result { get; set; }
    public string errorContent { get; set; } // Error message.

    private int _requestId;
    // The request id to this response.
    public virtual int requestID { set { _requestId = value; } get { return _requestId; } }

    // IsExecuted state.
    private bool executed;
    public bool IsExecuted
    {
        get { return executed; }
    }

    public bool RunExecute(BaseRequest request)
    {
        if (IsExecuted)
        {
            LoggerManager.Instance.Error("Execute executed response " + this.ToString());
        }

        if (isSucceed)
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
            LoggerManager.Instance.Warn("Execute response with server error: " + result);
#endif
            ErrorHandler(request, result, errorContent);
        }

        executed = true;

        return isSucceed;
    }

    public abstract void Execute(BaseRequest request);

    public virtual void ExectueWithGsError(BaseRequest request)
    {

    }

    protected virtual void ErrorHandler(BaseRequest request, int result, string errMsg)
    {
        AlertMessageManager.Instance.Show(result);
    }

}

public abstract class AbsResponse<T> : BaseResponse where T : Message
{
    protected T receiveMessage;

    public AbsResponse<T> InitMessage(T t)
    {
        this.receiveMessage = t;
        return this;
    }

    public override int requestID
    {
        get
        {
            if (receiveMessage != null)
            {
                return receiveMessage.Callback;
            }
            return 0;
        }
    }

    public override int result
    {
        get
        {
            if (receiveMessage != null)
            {
                return receiveMessage.Result;
            }

            return 0;
        }
    }

    public override bool isSucceed
    {
        get
        {
            return Protocols.isSuccess(result);
        }
    }

}
