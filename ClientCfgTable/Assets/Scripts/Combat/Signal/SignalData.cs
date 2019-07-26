namespace Combat
{
    public class SignalData
    {
    }

    public class GameStateChangedSignalData : SignalData
    {
        private int gameState;
        public int GetGameState() { return gameState; }

        public GameStateChangedSignalData(int gameState) { this.gameState = gameState; }
    }

    public class KillingSignalData : SignalData
    {
        private int attackerId;
        private int deathObjFaction;

        public int DeathObjFaction
        {
            get { return deathObjFaction; }
            set { deathObjFaction = value; }
        }
        public int AttackerId() { return attackerId; }

        public KillingSignalData(int attackerId, int deathObjFaction)
        {
            this.attackerId = attackerId;
            this.deathObjFaction = deathObjFaction;
        }
    }

    /// <summary>
    /// 矿触发
    /// </summary>
    public class MineTriggeredSignalData : SignalData
    {
        private int mineTriggerObjId;
        /// <summary>
        /// 被触发的矿
        /// </summary>
        public int MineTriggerObjId { get { return mineTriggerObjId; } }

        private int mineTriggingObjId;
        /// <summary>
        /// 触发者
        /// </summary>
        public int MineTriggingObjId { get { return mineTriggingObjId; } }

        /// <param name="mineTriggingObjId">触发者</param>
        /// <param name="mineTriggerObjId">被触发的矿</param>
        public MineTriggeredSignalData(int mineTriggingObjId, int mineTriggerObjId)
        {
            this.mineTriggingObjId = mineTriggingObjId;
            this.mineTriggerObjId = mineTriggerObjId;
        }
    }

    public class CastingSignalData : SignalData
    {
        private int abilityId;
        public int AbilityId() { return abilityId; }

        public CastingSignalData(int abltyId)
        {
            this.abilityId = abltyId;
        }
    }

    public class EffectSignalData : SignalData
    {
        private int effectId;
        public int EffectId() { return effectId; }

        public EffectSignalData(int _effectId)
        {
            this.effectId = _effectId;
        }
    }

    public class ObjectStateChangedSignalData : SignalData
    {
        //State
        public int stateType;

        //ObjectStateChangeType
        public int changeType;

        public ObjectStateChangedSignalData(int stateType, int changType)
        {
            this.stateType = stateType;
            this.changeType = changType;
        }
    }

    public class ControllingAvatarChangedSignalData : SignalData
    {
        public GameObject previousAvatar;
        public GameObject newAvatar;

        public ControllingAvatarChangedSignalData(GameObject previousAvatar, GameObject newAvatar)
        {
            this.previousAvatar = previousAvatar;
            this.newAvatar = newAvatar;
        }
    }

}
