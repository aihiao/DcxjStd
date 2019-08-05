using System.Collections.Generic;

namespace LywGames
{
    /// <summary>
    /// Id分配器
    /// </summary>
    public class IdAllocator
    {
        public const int InvalidId = 0; // 无效的Id
        private const int idStart = InvalidId + 1; // 起始Id

        private int idCounter = idStart; // Id计数器
        private int idLoops = 0; // Id循环条件
        private List<int> usedIdList = new List<int>(); // 使用过的Id容器

        /// <summary>
        /// 生成一个新的Id
        /// </summary>
        /// <returns></returns>
        public int NewId()
        {
            do
            {
                idCounter++;

                if (idCounter >= int.MaxValue)
                {
                    idCounter = idStart;
                    idLoops++;
                }

            } while (idLoops > 0 && usedIdList.Contains(idCounter));

            usedIdList.Add(idCounter);

            return idCounter;
        }

        /// <summary>
        /// 释放使用过的Id
        /// </summary>
        /// <param name="id"></param>
        public void ReleaseId(int id)
        {
            if (id == InvalidId)
            {
                return;
            }

            usedIdList.Remove(id);
        }

    }
}
