using System;
using System.Collections.Generic;

namespace Combat
{
    public class SignalGenerator
    {
        private object parent = null;
        private Dictionary<int, List<ISignalListener>> signalListenerDic;
        private bool listModified = false;
        private int recurseLevel = 0;

        public SignalGenerator() { }
        public SignalGenerator(object parent)
        {
            this.parent = parent;
        }

    }
}
