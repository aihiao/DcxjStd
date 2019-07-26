using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机控制器
/// 用于游戏大状态的切换, 比如登录, 选区, 主城等.
/// </summary>
public class GameStateMachineManager : AbsManager<GameStateMachineManager>, SceneManager.ISceneManagerListener
{
    private GameStateBase currentState;
    // 有时, 某些状态只是被临时stop, 有个更高的状态被处理, 此时不销毁, 而是把它压栈, 再高级状态结束后再出栈
    private Stack<GameStateBase> stateStack = new Stack<GameStateBase>();

    private GameStateChangingContexes stateChangingContex = new GameStateChangingContexes();
    /// <summary>
	/// 跳转状态的上下文信息
	/// </summary>
	public GameStateChangingContexes StateChangingContex
    {
        get { return stateChangingContex; }
        set { stateChangingContex = value; }
    }

    public override void OnUpdate()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

#if UNITY_EDITOR
    public override void OnGUIUpdate()
    {
        if (currentState != null)
        {
            currentState.OnGUIUpdate();
        }
    }
#endif

    /// <summary>
    /// 切换GameState
    /// </summary>
    /// <param name="force">false时, 如果当前状态为目标状态, 不执行切换, 直接返回当前状态</param>
    public T EnterState<T>(object userData = null, bool force = false) where T : GameStateBase
    {
        if (force == false)
        {
            // 如果当前状态为目标状态, 直接返回
            if (currentState != null && typeof(T) == currentState.GetType())
            {
                return currentState as T;
            }
        }

        GameStateBase oldState = currentState;
        currentState = rootGo.AddComponent<T>();
        currentState.MachineManager = this;
        currentState.Create(userData);

        if (oldState != null)
        {
            oldState.PrepareExit();
        }
        currentState.PrepareEnter();

        if (oldState != null)
        {
            var oldStateType = oldState.StateType;
            oldState.Exit();
            // 记录上次的状态
            stateChangingContex.RecordLastState(oldStateType);
        }
        currentState.Enter();

        if (oldState != null)
        {
            oldState.DoneExit();
            oldState.Dispose();
            GameObject.Destroy(oldState);
        }

        currentState.DoneEnter();

        return currentState as T;
    }

    /// <summary>
	/// 将oldState压栈但不销毁，新state作为当前的currentState]
	/// oldState 执行 preparePushStack等系列函数， currentState执行 prepareEnter系列函数
	/// </summary>
    public T PushStackState<T>(object userData, bool force) where T : GameStateBase
    {
        if (force == false)
        {
            if (currentState != null && typeof(T) == currentState.GetType())
            {
                return currentState as T;
            }
        }

        GameStateBase oldState = currentState;
        currentState = rootGo.AddComponent<T>();
        currentState.MachineManager = this;
        currentState.Create(userData);

        if (oldState != null)
        {
            oldState.PreparePushStack();
        }
        currentState.PrepareEnter();

        if (oldState != null)
        {
            oldState.PushStack();
            stateStack.Push(oldState);
        }
        currentState.Enter();

        if (oldState != null)
        {
            oldState.DonePushStack();
        }
        currentState.DoneEnter();

        return currentState as T;
    }

    /// <summary>
	/// 将临时入栈的state出栈, 并设置成当前状态
	/// </summary>
	public void PopStackState()
    {
        GameStateBase oldState = currentState;

        if (oldState != null)
        {
            oldState.PrepareExit();
        }
        currentState = stateStack.Pop();
        currentState.PreparePopStack();

        if (oldState != null)
        {
            var oldStateType = oldState.StateType;
            oldState.Exit();
            // 记录上次的状态
            stateChangingContex.RecordLastState(oldStateType);
        }
        currentState.PopStack();

        if (oldState != null)
        {
            oldState.DoneExit();
            oldState.Dispose();
            GameObject.Destroy(oldState);
        }
        currentState.DonePopStack();
    }

    /// <summary>
	/// 清理掉所有如栈的状态
	/// </summary>
	public void ClearPushedStates()
    {
        while (stateStack.Count > 0)
        {
            GameStateBase state = stateStack.Pop();
            state.PrepareExit();

            var oldStateType = state.StateType;
            state.Exit();
            // 记录上次的状态
            stateChangingContex.RecordLastState(oldStateType);

            state.DonePushStack();
            state.Dispose();
            GameObject.Destroy(state);
        }
    }

    /// <summary>
	/// 从stack里获取
	/// </summary>
	public T GetPushedState<T>() where T : GameStateBase
    {
        foreach (var state in stateStack)
        {
            T tmp = state as T;
            if (tmp != null)
            {
                return tmp;
            }
        }
        return null;
    }

    public T GetCurrentState<T>() where T : GameStateBase
    {
        return currentState as T;
    }

    public GameStateBase GetCurrentState()
    {
        return currentState;
    }

    /// <summary>
	/// 检查currentstate和StateStack, 从中找到(如果有)指定的状态
	/// </summary>
	public T GetAvailableState<T>() where T : GameStateBase
    {
        T tmp = currentState as T;
        if (tmp != null)
        {
            return tmp;
        }
        return GetPushedState<T>();
    }

    public GameStateBase.GameStateType GetCurrentStateType()
    {
        return currentState != null ? currentState.StateType : GameStateBase.GameStateType.InValid;
    }

    #region ISceneManagerListener
    public void OnSceneWillChange(SceneManager manager, string currentScene, string newScene)
    {
        // 切换场景之前, 进行一次无效资源的释放
        GameShellManager.Instance.FreeMemory();
    }

    public void OnSceneChanged(SceneManager manager, string oldScene, string currentScene)
    {
        System.GC.Collect();
    }
    #endregion

}
