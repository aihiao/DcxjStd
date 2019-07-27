using System;

namespace Combat
{
    public class ObjectCreationContext
    {
        public static class CreationMethodType
        {
            public const int OBJECT_CREATION_METHOD = 0;
            public const int GAME_OBJECT_CREATION_METHOD = 1;
            public const int ABILITY_CREATION_METHOD = 2;
            public const int PLAYER_CREATION_METHOD = 3;
        }

        public bool clientNotServer = false;
        public ObjectBase Object = null;
        public int templateId = 0;

        public int CreationMethod = CreationMethodType.OBJECT_CREATION_METHOD;

        public int OwnerID = NetworkObjectTraits.InvalidId;
        public int NetworkID = NetworkObjectTraits.InvalidId;
    }
}
