using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LywGames
{
    public class IdAllocator
    {
        public int NewId()
        {
            do
            {
                idCounter++;

                if (idCounter >= Int32.MaxValue)
                {
                    idCounter = idStart;
                    idLoops++;
                }

            } while (idLoops > 0 && usedIds.Contains(idCounter));

            usedIds.Add(idCounter);

            return idCounter;
        }

        public void ReleaseID(int id)
        {
            if (id == InvalidId)
                return;

            usedIds.Remove(id);
        }

        public const int InvalidId = 0;
        protected const int idStart = InvalidId + 1;
        protected int idCounter = idStart;
        protected int idLoops = 0;
        protected List<int> usedIds = new List<int>();
    }
}
