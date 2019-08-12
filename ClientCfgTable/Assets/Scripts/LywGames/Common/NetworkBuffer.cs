using System;
using System.IO;

namespace LywGames.Common
{
    public class NetworkBuffer
    {
        /** 不同编码方式1个英文字母占的字节是不同的
         * ASCII码: 一个英文字母(不分大小写)占一个字节的空间, 一个英文标点占一个字节。ASCII码不认识汉字。而且ASCII码就一个字节。
         * UTF-8编码: 一个英文字符(不分大小写)占一个字节, 一个英文标点占一个字节。一个中文(含繁体)占三个字节, 一个中文标点占三个字节。
         * Unicode编码: 一个英文字符(不分大小写)占两个字节, 一个英文标点占两个字节。一个中文(含繁体)占两个字节, 一个中文标点占两个字节。
         **/

        /** 创建一个容量为100的MemoryStream。当容量超过100的时候, MemoryStream会自动扩容到256; 当容量超过256的时候, MemoryStream会自动扩容到512。
         *  若创建一个容量为0的MemoryStream, 也就是new MemoryStream()。当写入数据的时候, MemoryStream会自动扩容到256; 当容量超过256的时候, MemoryStream会自动扩容到512。
         *  MemoryStream的扩容规律是: 256, 256 * 2, 256 * 2 * 2 ...
         *  
         *  MemoryStream Capacity: 获取或设置分配给该流的字节数。
         *  MemoryStream Length: 获取流的长度(以字节为单位)。
         *  MemoryStream Position: 获取或设置流中的当前位置。
         *  MemoryStream Seek(Int64, SeekOrigin): 将当前流中的位置设置为指定值。SeekOrigin.Begin就是0的位置; SeekOrigin.Current就是Position的位置; SeekOrigin.End就是Length的位置。
        **/
        private MemoryStream ms;
        private BinaryReader br;
        private BinaryWriter bw;
        private bool isBigEndian;
        private int readOffset;
        private int writeOffset;

        public int ReadOffset
        {
            get
            {
                return readOffset;
            }
            set
            {
                if ((long)value > ms.Length || value > writeOffset)
                {
                    throw new IndexOutOfRangeException();
                }
                readOffset = value;
            }
        }

        public int WriteOffset
        {
            get
            {
                return writeOffset;
            }
            set
            {
                if (value > ms.Capacity || value < readOffset)
                {
                    throw new IndexOutOfRangeException();
                }
                writeOffset = value;
            }
        }

        public int Capacity
        {
            get
            {
                return ms.Capacity;
            }
        }

        public int ReadableBytes
        {
            get
            {
                return writeOffset - readOffset;
            }
        }

        public bool Readable
        {
            get
            {
                return readOffset < writeOffset;
            }
        }

        public int WritableBytes
        {
            get
            {
                return ms.Capacity - writeOffset;
            }
        }

        public NetworkBuffer(int capacity, bool isBigEndian)
        {
            this.ms = new MemoryStream(capacity);
            this.br = new BinaryReader(ms);
            this.bw = new BinaryWriter(ms);
            this.readOffset = 0;
            this.writeOffset = 0;
            this.isBigEndian = isBigEndian;
        }

        public NetworkBuffer(byte[] buffer, int offset, int count, bool isBigEndian)
        {
            this.ms = new MemoryStream(buffer, offset, count);
            this.br = new BinaryReader(ms);
            this.bw = new BinaryWriter(ms);
            this.readOffset = 0;
            this.writeOffset = count;
            this.isBigEndian = isBigEndian;
        }

        public void ResetReadOffset()
        {
            readOffset = 0;
        }

        public void ResetWriteOffset()
        {
            readOffset = 0;
            writeOffset = 0;
        }

        public void DiscardReadBytes()
        {
            int num = writeOffset - readOffset;
            if (readOffset > 0) // 这里等于0是不行的吗???
            {
                if (num > 0)
                {
                    byte[] buffer = Readbytes(readOffset, num);
                    ms.Position = 0L;
                    bw.Write(buffer, 0, num);
                    writeOffset = num;
                    readOffset = 0;
                }
                else
                {
                    ResetWriteOffset();
                }
            }
        }

        public void SkipBytes(int length)
        {
            if (readOffset + length > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            readOffset += length;
        }

        /// <summary>
        /// 确认是否还可以写入writableBytes这么多字节吗
        /// </summary>
        /// <param name="writableBytes"></param>
        /// <returns></returns>
        public bool EnsureWritableBytes(int writableBytes)
        {
            return writableBytes <= ms.Capacity - writeOffset;
        }

        /// <summary>
        /// 这里有Read和Get, 2种方法。如: ReadByte和GetByte。
        /// Read方法readOffset滑动了相应的字节数, Get方法readOffset不变。
        /// </summary>
        public byte ReadByte(int offset)
        {
            if (offset + 1 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            readOffset = offset + 1;

            return br.ReadByte();
        }

        public byte ReadByte()
        {
            return ReadByte(readOffset);
        }

        public short ReadInt16(int offset)
        {
            if (offset + 2 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            readOffset = offset + 2;

            short result;
            if (isBigEndian && BitConverter.IsLittleEndian)
            {
                result = BigEndianTransfer.ToInt16(br.ReadBytes(2));
            }
            else
            {
                result = br.ReadInt16();
            }

            return result;
        }

        public short ReadInt16()
        {
            return ReadInt16(readOffset);
        }

        public ushort ReadUInt16(int offset)
        {
            if (offset + 2 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            readOffset = offset + 2;

            ushort result;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                result = BigEndianTransfer.ToUInt16(br.ReadBytes(2));
            }
            else
            {
                result = br.ReadUInt16();
            }

            return result;
        }

        public ushort ReadUInt16()
        {
            return ReadUInt16(readOffset);
        }

        public int ReadInt32(int offset)
        {
            if (offset + 4 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            readOffset = offset + 4;

            int result;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                result = BigEndianTransfer.ToInt32(br.ReadBytes(4));
            }
            else
            {
                result = br.ReadInt32();
            }

            return result;
        }

        public int ReadInt32()
        {
            return ReadInt32(readOffset);
        }

        public uint ReadUInt32(int offset)
        {
            if (offset + 4 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            readOffset = offset + 4;

            uint result;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                result = BigEndianTransfer.ToUInt32(br.ReadBytes(4));
            }
            else
            {
                result = br.ReadUInt32();
            }

            return result;
        }

        public uint ReadUInt32()
        {
            return ReadUInt32(readOffset);
        }

        public byte[] Readbytes(int offset, int count)
        {
            if (offset + count > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            readOffset = offset + count;

            return br.ReadBytes(count);
        }

        public byte[] Readbytes(int count)
        {
            return Readbytes(readOffset, count);
        }

        public byte GetByte(int offset)
        {
            if (offset + 1 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;

            return br.ReadByte();
        }

        public byte GetByte()
        {
            return GetByte(readOffset);
        }

        public short GetInt16(int offset)
        {
            if (offset + 2 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;

            short result;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                result = BigEndianTransfer.ToInt16(br.ReadBytes(2));
            }
            else
            {
                result = br.ReadInt16();
            }

            return result;
        }

        public short GetInt16()
        {
            return GetInt16(readOffset);
        }

        public ushort GetUInt16(int offset)
        {
            if (offset + 2 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;

            ushort result;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                result = BigEndianTransfer.ToUInt16(br.ReadBytes(2));
            }
            else
            {
                result = br.ReadUInt16();
            }

            return result;
        }

        public ushort GetUInt16()
        {
            return GetUInt16(readOffset);
        }

        public int GetInt32(int offset)
        {
            if (offset + 4 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;

            int result;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                result = BigEndianTransfer.ToInt32(br.ReadBytes(4));
            }
            else
            {
                result = br.ReadInt32();
            }

            return result;
        }

        public int GetInt32()
        {
            return GetInt32(readOffset);
        }

        public uint GetUInt32(int offset)
        {
            if (offset + 4 > writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;

            uint result;
            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                result = BigEndianTransfer.ToUInt32(br.ReadBytes(4));
            }
            else
            {
                result = br.ReadUInt32();
            }

            return result;
        }

        public uint GetUInt32()
        {
            return GetUInt32(readOffset);
        }

        public void Write(int offset, byte value)
        {
            if (offset + 1 > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + 1;
            bw.Write(value);
        }

        public void Write(byte value)
        {
            Write(writeOffset, value);
        }

        public void Write(int offset, short value)
        {
            if (offset + 2 > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + 2;

            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                bw.Write(value);
            }
        }

        public void Write(short value)
        {
            Write(writeOffset, value);
        }

        public void Write(int offset, ushort value)
        {
            if (offset + 2 > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + 2;

            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                bw.Write(value);
            }
        }

        public void Write(ushort value)
        {
            Write(writeOffset, value);
        }

        public void Write(int offset, int value)
        {
            if (offset + 4 > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + 4;

            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                bw.Write(value);
            }
        }

        public void Write(int value)
        {
            Write(writeOffset, value);
        }

        public void Write(int offset, uint value)
        {
            if (offset + 4 > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + 4;

            if (BitConverter.IsLittleEndian && isBigEndian)
            {
                bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                bw.Write(value);
            }
        }

        public void Write(uint value)
        {
            Write(writeOffset, value);
        }

        public void Write(int offset, byte[] value, int index, int count)
        {
            if (offset + count > ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            ms.Position = offset;
            writeOffset = offset + count;
            bw.Write(value, index, count);
        }

        public void Write(byte[] value, int index, int count)
        {
            Write(writeOffset, value, index, count);
        }

        public byte[] GetContent()
        {
            return ms.ToArray();
        }

        public MemoryStream GetStream()
        {
            return ms;
        }

        public byte[] GetBuffer()
        {
            return ms.GetBuffer();
        }

    }
}