using System;
using UnityEngine;

namespace ClientCommon
{
    public class Configuration
    {
        protected const long MaxStayTime = 600 * 10000000L; // 单位100豪微妙(10^-7), 目前是600s, 10分钟。
        protected const long CheckReleaseTime = 60 * 10000000L; // 单位100豪微妙(10^-7), 目前是60s, 1分钟。

        private static int lastGetTimeTickFrame = -1;
        private static long cachedTimeTick = 0;

        /// <summary>
        /// 由于db里频繁调用DateTime.Now.Ticks，在这里封装一下。同一帧之内最多调用一次。
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentTimeTick()
        {
            if (lastGetTimeTickFrame != Time.frameCount)
            {
                lastGetTimeTickFrame = Time.frameCount;
                cachedTimeTick = DateTime.Now.Ticks;
            }

            return cachedTimeTick;
        }

        public virtual void ReleaseData(bool isForce) { }
        public virtual void LoadAllData()
        {
            Debug.LogError("should not reach here!");
        }

        private bool hasLoadedAll = false;
        public bool HasLoadedAll
        {
            get { return hasLoadedAll; }
            set { hasLoadedAll = value; }
        }

        public void LoadAllDataIfNotYet()
        {
            if (hasLoadedAll)
            {
                return;
            }

            LoadAllData();
            hasLoadedAll = true;
        }

    }
}