using LywGames;
using LywGames.Messages;

/// <summary>
/// 发送的协议基类
/// </summary>
public abstract class BaseRequest
{
    // Id分配器
    private static IdAllocator idAllc = new IdAllocator();
    public static int GetAnNewId()
    {
        return idAllc.NewId();
    }

    public BaseRequest()
    {
        callBackId = idAllc.NewId();
    }

    ~BaseRequest()
    {
        // 如果该请求有响应, 当丢弃请求的时候还没有响应应答。保留该Id给接受请求。
        if (HasResponse && !IsResponded)
        {
            // Do nothing
        }
        else
        {
            idAllc.ReleaseId(callBackId);
        }
    }

    // 发生请求的时候携带了一个回调Id, 服务器在响应的时候会返回它。
    private int callBackId;
    public virtual int CallBackId { get { return callBackId; } }

    // 是否需要回应，默认true，若需要则在收到回应前禁止客户端操作
    protected bool needResponse = true;
    public virtual bool NeedResponse
    {
        get { return needResponse; }
        set { needResponse = value; }
    }

    // 是否有响应
    public virtual bool HasResponse
    {
        get { return true; }
    }

    // 正在等待响应
    public virtual bool WaitingResponse
    {
        get { return true; }
    }

    // 该请求是否响应过了
    private bool isResponded;
    public bool IsResponded
    {
        get { return isResponded; }
        set { isResponded = value; }
    }

    // 该请求被丢弃
    private bool isDiscarded = false;
    public bool IsDiscarded
    {
        get { return isDiscarded; }
        set { isDiscarded = value; }
    }

    // 请求是否已经执行过了
    private bool isExecuted = false;
    public bool IsExecuted { get { return isExecuted; } }

    // 是否成功执行请求。在执行请求的过程中发生异常, 就是false; 没有发生异常, 就为true。
    private bool isExecuteSuccess = false;
    public bool IsExecuteSuccess
    {
        get { return isExecuteSuccess; }
        set { isExecuteSuccess = value; }
    }

    private bool idCombined = false;
    public bool IsCombined
    {
        get { return idCombined; }
        set { idCombined = value; }
    }

    public virtual bool Combinable
    {
        get { return false; }
    }

    public virtual bool CombineWithPrevRequest(BaseRequest request)
    {
        return GetType() == request.GetType();
    }

    public virtual Message GetMessage()
    {
        return null;
    }

    public virtual bool CheckTimeout
    {
        get { return true; }
    }

    public virtual bool MutuallyExclusive
    {
        get { return true; }
    }

    // 可以被延迟执行的请求
    public virtual bool Delayable
    {
        get { return false; }
    }

    public bool RunExecute(ServerBusiness bsn)
    {
        if (IsExecuted)
        {
            LoggerManager.Instance.Error("Execute executed request " + ToString());
        }
        if (IsDiscarded)
        {
            LoggerManager.Instance.Error("Execute discarded request " + ToString());
        }
        if (IsCombined)
        {
            LoggerManager.Instance.Error("Execute combined request " + ToString());
        }

        isExecuteSuccess = Execute(bsn);
        isExecuted = true;

        return isExecuteSuccess;
    }

    public virtual bool Execute(ServerBusiness bsn)
    {
        return false;
    }

    public override string ToString()
    {
        return string.Format("Type:{0} CallBackId:{1:d} IsExecuted:{2} IsExecuteSuccess:{3} IsResponded:{4} IsCombined:{5} HasResponse:{6}", GetType(), CallBackId, IsExecuted, IsExecuteSuccess, IsResponded, IsCombined, HasResponse);
    }
}

public abstract class  AbsRequest<T> : BaseRequest where T : Message, new()
{
    public T sendMessage = new T();

    public AbsRequest()
    {
        sendMessage.CallBackId = this.CallBackId;
    }

    public override Message GetMessage()
    {
        return sendMessage;
    }

    public override bool Execute(ServerBusiness bsn)
    {
        if (bsn != null)
        {
            return bsn.SendMessage(sendMessage);
        }

        return false;
    }

}
