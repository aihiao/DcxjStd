public class GameStateSelectArea : GameStateBase
{
    public override bool IsGamingState
    {
        get
        {
            return false;
        }
    }

    public override GameStateType StateType
    {
        get
        {
            return GameStateType.SelectArea;
        }
    }

    public GameStateSelectArea StartUi()
    {
        return this;
    }

}

