using System;
using System.Collections.Generic;

namespace Combat
{
    public class ObjectManager
    {
        public virtual ObjectBase CreateObject(ObjectCreationContext context)
        {
            return null;
        }
    }
}
