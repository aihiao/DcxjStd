using System;
using System.Net.Sockets;
using System.Collections.Generic;

namespace LywGames.Network
{
    internal class BufferManager
    {
        private int totalBytes;
        private byte[] buffer;
        private int bufferSize;
        private int currentIndex;
        private Stack<int> freeIndexPool; // 先进后出

        public BufferManager(int totalBytes, int bufferSize)
        {
            this.totalBytes = totalBytes;
            this.bufferSize = bufferSize;
            currentIndex = 0;
            freeIndexPool = new Stack<int>();
        }

        public void InitBuffer()
        {
            buffer = new byte[totalBytes];
        }

        public bool SetBuffer(SocketAsyncEventArgs args)
        {
            if (freeIndexPool.Count > 0)
            {
                args.SetBuffer(buffer, freeIndexPool.Pop(), bufferSize);
            }
            else
            {
                if (totalBytes - bufferSize < currentIndex)
                {
                    LoggerManager.Instance.Warn("SetBuffer found buffer is not enough, total {0} curIndex {1} perSize {2}", totalBytes, currentIndex, bufferSize);
            
                    return false;
                }

                args.SetBuffer(buffer, currentIndex, bufferSize);
                currentIndex += bufferSize;
            }
            
            return true;
        }

        public bool SetBuffer(SocketAsyncEventArgs args, byte[] buffer, int offset, int count)
        {
            if (freeIndexPool.Count > 0)
            {
                int num = freeIndexPool.Pop();
                Buffer.BlockCopy(buffer, offset, this.buffer, num, count);
                args.SetBuffer(buffer, num, count);
            }
            else
            {
                if (totalBytes - bufferSize < currentIndex)
                {
                    LoggerManager.Instance.Warn("SetBuffer found buffer is not enough, total {0} curIndex {1} perSize {2}", totalBytes, currentIndex, bufferSize);
                    
                    return false;
                }

                Buffer.BlockCopy(buffer, offset, this.buffer, currentIndex, count);
                args.SetBuffer(this.buffer, currentIndex, count);
                currentIndex += bufferSize;
            }
            
            return true;
        }

        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }

    }
}
