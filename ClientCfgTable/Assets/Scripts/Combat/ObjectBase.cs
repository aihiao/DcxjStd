using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combat
{
    public class ObjectBase : ISignalGenerator
    {
        public SignalGenerator GetSignalGenerator()
        {
            throw new NotImplementedException();
        }
    }
}
