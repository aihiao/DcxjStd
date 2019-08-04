using System;
using System.Net.Sockets;
using System.Collections.Generic;

namespace LywGames.Network
{
    internal class BufferManager
    {
        private int m_numBytes;
        private byte[] m_buffer;
        private Stack<int> m_freeIndexPool;
        private int m_currentIndex;
        private int m_bufferSize;
        public BufferManager(int totalBytes, int bufferSize)
        {
            this.m_numBytes = totalBytes;
            this.m_currentIndex = 0;
            this.m_bufferSize = bufferSize;
            this.m_freeIndexPool = new Stack<int>();
        }
        public void InitBuffer()
        {
            this.m_buffer = new byte[this.m_numBytes];
        }
        public bool SetBuffer(SocketAsyncEventArgs args)
        {
            bool result;
            if (this.m_freeIndexPool.Count > 0)
            {
                args.SetBuffer(this.m_buffer, this.m_freeIndexPool.Pop(), this.m_bufferSize);
            }
            else
            {
                if (this.m_numBytes - this.m_bufferSize < this.m_currentIndex)
                {
                    LoggerManager.Instance.Warn("SetBuffer found buffer is not enough, total {0} curIndex {1} perSize {2}", new object[]
                    {
                        this.m_numBytes,
                        this.m_currentIndex,
                        this.m_bufferSize
                    });
                    result = false;
                    return result;
                }
                args.SetBuffer(this.m_buffer, this.m_currentIndex, this.m_bufferSize);
                this.m_currentIndex += this.m_bufferSize;
            }
            result = true;
            return result;
        }
        public bool SetBuffer(SocketAsyncEventArgs args, byte[] buffer, int offset, int count)
        {
            bool result;
            if (this.m_freeIndexPool.Count > 0)
            {
                int num = this.m_freeIndexPool.Pop();
                Buffer.BlockCopy(buffer, offset, this.m_buffer, num, count);
                args.SetBuffer(this.m_buffer, num, count);
            }
            else
            {
                if (this.m_numBytes - this.m_bufferSize < this.m_currentIndex)
                {
                    LoggerManager.Instance.Warn("SetBuffer found buffer is not enough, total {0} curIndex {1} perSize {2}", new object[]
                    {
                        this.m_numBytes,
                        this.m_currentIndex,
                        this.m_bufferSize
                    });
                    result = false;
                    return result;
                }
                Buffer.BlockCopy(buffer, offset, this.m_buffer, this.m_currentIndex, count);
                args.SetBuffer(this.m_buffer, this.m_currentIndex, count);
                this.m_currentIndex += this.m_bufferSize;
            }
            result = true;
            return result;
        }
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            this.m_freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }
    }
}
