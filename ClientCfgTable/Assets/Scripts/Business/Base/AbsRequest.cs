using LywGames;
using LywGames.Messages;

/// <summary>
/// 发送的协议基类
/// </summary>
public abstract class BaseRequest
{
    // Invalid request id.
    public const int InvalidID = IdAllocator.InvalidId;

    // ID.
    public virtual int ID { get { return id; } }

    //private KodGames.Messages.Message sendMessage;

    // 是否需要回应，默认true，若需要则在收到回应前禁止客户端操作
    protected bool needResponse = true;
    public virtual bool NeedResponse
    {
        get { return needResponse; }
        set { needResponse = value; }
    }

    public virtual Message GetMessage()
    {
        return null;
    }

    // Information.
    public override string ToString()
    {
        return string.Format("{0} ID:{1:d} Executed:{2} ExecResult:{3} IsResponded:{4} Combined:{5} HasResponse:{6}",
            this.GetType(), ID, IsExecuted, ExecResult, IsResponded, IsCombined, HasResponse);
    }

    public virtual bool CheckTimeout
    {
        get { return true; }
    }

    public virtual bool MutuallyExclusive
    {
        get { return true; }
    }

    // If has response.
    public virtual bool HasResponse
    {
        get { return true; }
    }

    // Waiting response flag.
    public virtual bool WaitingResponse
    {
        get { return true; }
    }

    public virtual bool Combinable
    {
        get { return false; }
    }

    private bool combined = false;
    public bool IsCombined
    {
        get { return combined; }
        set { combined = value; }
    }

    public virtual bool CombineWithPrevRequest(BaseRequest request)
    {
        return this.GetType() == request.GetType();
    }

    public virtual bool Delayable
    {
        get { return false; }
    }

    // Is discarded flag.
    private bool isDiscarded = false;
    public bool IsDiscarded
    {
        get { return isDiscarded; }
        set { isDiscarded = value; }
    }

    // Is executed.
    private bool executed = false;
    public bool IsExecuted { get { return executed; } }

    private bool execResult = false;
    public bool ExecResult
    {
        get { return execResult; }
        set { execResult = value; }
    }

    // Get responded state, only valid for request that waiting for response. 
    // The flag to mark response state to this request.
    private bool responded;
    public bool IsResponded
    {
        get { return responded; }
        set { responded = value; }
    }

    public BaseRequest()
    {
        id = idAllc.NewId();
    }

    ~BaseRequest()
    {
        // If this request has response and has not been responded when it is discarded,
        // We keep the id for the response.
        if (HasResponse && !IsResponded)
        {
            // Do nothing.
        }
        else
            idAllc.ReleaseID(id);
    }

    public bool RunExecute(ServerBusiness bsn)
    {
        if (IsExecuted)
            LoggerManager.Instance.Error("Execute executed request " + this.ToString());
        if (IsDiscarded)
            LoggerManager.Instance.Error("Execute discarded request " + this.ToString());
        if (IsCombined)
            LoggerManager.Instance.Error("Execute combined request " + this.ToString());

        bool executeResult = Execute(bsn);
        executed = true;
        return executeResult;
    }

    // Execute.
    public virtual bool Execute(ServerBusiness bsn)
    {
        /*if (bsn != null)
		{
			return bsn.SendMessage(sendMessage);
		}*/
        return false;
    }

    public static int GetAnNewId()
    {
        return idAllc.NewId();
    }

    // ID
    private static IdAllocator idAllc = new IdAllocator();
    private int id;
}

public abstract class  AbsRequest<T> : BaseRequest where T : Message, new()
{
    public T sendMessage = new T();

    public AbsRequest()
    {
        this.sendMessage.Callback = this.ID;
    }

    public override Message GetMessage()
    {
        return this.sendMessage;
    }

    public override bool Execute(ServerBusiness bsn)
    {
        if (bsn != null)
        {
            return bsn.SendMessage(this.sendMessage);
        }
        return false;
    }

}
