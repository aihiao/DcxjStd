using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combat
{
    abstract class InstanceBase : ISignalListener
    {
        protected ObjectManager objectManager = null;

        public void ReceiveSignal(SignalGenerator generator, int signalType, SignalData signalData)
        {
            throw new NotImplementedException();
        }

        public static InstanceBase GetCurrentInstance()
        {
            return null;
        }

        public ObjectManager GetObjectManager() { return objectManager; }

    }
}
