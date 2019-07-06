using UnityEngine;
using System.Collections;

namespace LywGames
{
    public class WWWRequestChecker : IEnumerator
    {
        private WWW www;
        public WWW GetWWW()
        {
            return www;
        }

        private bool isError = false;
        public bool IsError { get { return isError; } }

        private bool enableTimer = false;
        private float timer = 0;

        private bool isTimeOut = false;
        public bool IsTimeOut { get { return isTimeOut; } }

        public WWWRequestChecker(WWW www, bool enableTimer = false)
        {
            this.www = www;
            this.enableTimer = enableTimer;
            this.timer = Time.realtimeSinceStartup;
        }

        public object Current
        {
            get
            {
                return null;
            }
        }

        public void Reset()
        {

        }

        public bool MoveNext()
        {
            return !IsDone();
        }

        public void Dispose()
        {
            www.Dispose();
            www = null;
        }

        public bool IsDone()
        {
            if (null == www)
            {
                isError = true;
                return true;
            }

            if (www.isDone || (!string.IsNullOrEmpty(www.error)))
            {
                if (!string.IsNullOrEmpty(www.error))
                {
                    Debug.LogWarning("WWWRequestChecker error: " + www.error + " url = " + www.url);
                    isError = true;
                }
                return true;
            }

            if (enableTimer && Time.realtimeSinceStartup - timer > 10)
            {
                isError = true;
                isTimeOut = true;
                return true;
            }

            return false;
        }

    }
}