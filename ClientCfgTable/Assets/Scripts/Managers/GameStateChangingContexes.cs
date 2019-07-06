using System.Collections.Generic;

/// <summary>
/// 用于保存状态机切换时的上下文
/// 例如进了哪种副本, 因为什么出了主城.等等
/// </summary>
public class GameStateChangingContexes
{
    // 进入副本的上下文
    private EnterDungeonContexVo enterDungeonContex;
    public EnterDungeonContexVo EnterDungeonContex
    {
        get { return enterDungeonContex; }
        set { enterDungeonContex = value; }
    }

    // 出主城的上下文
    OutCentryCityContexVo outCentryCityContex = new OutCentryCityContexVo();
    public OutCentryCityContexVo OutCentryCityContex
    {
        get { return outCentryCityContex; }
        set { outCentryCityContex = value; }
    }

    // ... 有需要添加其他上下文可以在此添加

    private const int MaxPreStateStoreCount = 16;
    private List<GameStateBase.GameStateType> preStatesType = new List<GameStateBase.GameStateType>();

    public void RecordLastState(GameStateBase.GameStateType stateType)
    {
        preStatesType.Add(stateType);
        if (preStatesType.Count >= MaxPreStateStoreCount)
        {
            preStatesType.RemoveAt(0);
        }
    }

    /// <summary>
	/// 获取之前的状态，offset表示上几次的状态
	/// </summary>
	/// <param name="offset">上几次的状态, 1表示上次的状态, 2表示上上次的, 负数无意义, 0值不推荐使用但是会返回当前的状态</param>
	/// <returns></returns>
    public GameStateBase.GameStateType GetLastState(int offset = 1)
    {
        if (offset < 0)
        {
            return GameStateBase.GameStateType.InValid;
        }

        if (offset == 0)
        {
            return GameStateMachineManager.Instance.GetCurrentStateType();
        }

        if (offset >= preStatesType.Count)
        {
            return GameStateBase.GameStateType.InValid;
        }

        return preStatesType[preStatesType.Count - offset];
    }

}

public class OutCentryCityContexVo
{
    // 以何种形式出了主城
    private float outCityPosX;

    public float OutCityPosX
    {
        get { return outCityPosX; }
        set { outCityPosX = value; }
    }

    private float outCityPosZ;

    public float OutCityPosZ
    {
        get { return outCityPosZ; }
        set { outCityPosZ = value; }
    }

    public bool isOutCityPosAvalible = false;
}

public class EnterDungeonContexVo
{
    /// <summary>
    /// 表示是否进入战斗或在战斗中
    /// </summary>
    public bool isEnter = true;

}