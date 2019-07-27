using System;
using System.IO;

namespace LywGames.Common
{
    public class NetworkBuffer
    {
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

        public void ResetReadOffset()
        {
            readOffset = 0;
        }

        public void ResetWriteOffset()
        {
            readOffset = 0;
            writeOffset = 0;
        }

        public NetworkBuffer(int capacity, bool isBigEndian)
        {
            this.ms = new MemoryStream(capacity);
            this.br = new BinaryReader(ms);
            this.bw = new BinaryWriter(ms);
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

        public void DiscardReadBytes()
        {
            int num = writeOffset - readOffset;
            if (this.readOffset > 0)
            {
                if (num > 0)
                {
                    byte[] buffer = Readbytes(this.readOffset, num);
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
            if (this.readOffset + length > this.writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            this.readOffset += length;
        }

        public bool EnsureWritableBytes(int writableBytes)
        {
            return writableBytes <= this.ms.Capacity - this.writeOffset;
        }

        public byte ReadByte(int offset)
        {
            if (offset + 1 > this.writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            this.readOffset = offset + 1;
            return this.br.ReadByte();
        }

        public byte ReadByte()
        {
            return this.ReadByte(this.readOffset);
        }

        public short ReadInt16(int offset)
        {
            if (offset + 2 > this.writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            this.readOffset = offset + 2;
            short result;
            if (this.isBigEndian && BitConverter.IsLittleEndian)
            {
                result = BigEndianTransfer.ToInt16(this.br.ReadBytes(2));
            }
            else
            {
                result = this.br.ReadInt16();
            }
            return result;
        }

        public short ReadInt16()
        {
            return this.ReadInt16(this.readOffset);
        }

        public ushort ReadUInt16(int offset)
        {
            if (offset + 2 > this.writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            this.readOffset = offset + 2;
            ushort result;
            if (BitConverter.IsLittleEndian && this.isBigEndian)
            {
                result = BigEndianTransfer.ToUInt16(this.br.ReadBytes(2));
            }
            else
            {
                result = this.br.ReadUInt16();
            }
            return result;
        }

        public ushort ReadUInt16()
        {
            return this.ReadUInt16(this.readOffset);
        }

        public int ReadInt32(int offset)
        {
            if (offset + 4 > this.writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            this.readOffset = offset + 4;
            int result;
            if (BitConverter.IsLittleEndian && this.isBigEndian)
            {
                result = BigEndianTransfer.ToInt32(this.br.ReadBytes(4));
            }
            else
            {
                result = this.br.ReadInt32();
            }
            return result;
        }

        public int ReadInt32()
        {
            return this.ReadInt32(this.readOffset);
        }

        public uint ReadUInt32(int offset)
        {
            if (offset + 4 > this.writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            this.readOffset = offset + 4;
            uint result;
            if (BitConverter.IsLittleEndian && this.isBigEndian)
            {
                result = BigEndianTransfer.ToUInt32(this.br.ReadBytes(4));
            }
            else
            {
                result = this.br.ReadUInt32();
            }
            return result;
        }

        public uint ReadUInt32()
        {
            return this.ReadUInt32(this.readOffset);
        }

        public byte[] Readbytes(int offset, int count)
        {
            if (offset + count > this.writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            this.readOffset = offset + count;
            return this.br.ReadBytes(count);
        }

        public byte[] Readbytes(int count)
        {
            return this.Readbytes(this.readOffset, count);
        }
        public byte GetByte(int offset)
        {
            if (offset + 1 > this.writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            return this.br.ReadByte();
        }

        public byte GetByte()
        {
            return this.GetByte(this.readOffset);
        }

        public short GetInt16(int offset)
        {
            if (offset + 2 > this.writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            short result;
            if (BitConverter.IsLittleEndian && this.isBigEndian)
            {
                result = BigEndianTransfer.ToInt16(this.br.ReadBytes(2));
            }
            else
            {
                result = this.br.ReadInt16();
            }
            return result;
        }

        public short GetInt16()
        {
            return this.GetInt16(this.readOffset);
        }

        public ushort GetUInt16(int offset)
        {
            if (offset + 2 > this.writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            ushort result;
            if (BitConverter.IsLittleEndian && this.isBigEndian)
            {
                result = BigEndianTransfer.ToUInt16(this.br.ReadBytes(2));
            }
            else
            {
                result = this.br.ReadUInt16();
            }
            return result;
        }

        public ushort GetUInt16()
        {
            return this.GetUInt16(this.readOffset);
        }

        public int GetInt32(int offset)
        {
            if (offset + 4 > this.writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            int result;
            if (BitConverter.IsLittleEndian && this.isBigEndian)
            {
                result = BigEndianTransfer.ToInt32(this.br.ReadBytes(4));
            }
            else
            {
                result = this.br.ReadInt32();
            }
            return result;
        }

        public int GetInt32()
        {
            return this.GetInt32(this.readOffset);
        }

        public uint GetUInt32(int offset)
        {
            if (offset + 4 > this.writeOffset)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            uint result;
            if (BitConverter.IsLittleEndian && this.isBigEndian)
            {
                result = BigEndianTransfer.ToUInt32(this.br.ReadBytes(4));
            }
            else
            {
                result = this.br.ReadUInt32();
            }
            return result;
        }

        public uint GetUInt32()
        {
            return this.GetUInt32(this.readOffset);
        }

        public void Write(int offset, byte value)
        {
            if (offset + 1 > this.ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            this.writeOffset = offset + 1;
            this.bw.Write(value);
        }

        public void Write(byte value)
        {
            this.Write(this.writeOffset, value);
        }

        public void Write(int offset, short value)
        {
            if (offset + 2 > this.ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            this.writeOffset = offset + 2;
            if (BitConverter.IsLittleEndian && this.isBigEndian)
            {
                this.bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                this.bw.Write(value);
            }
        }

        public void Write(short value)
        {
            this.Write(this.writeOffset, value);
        }

        public void Write(int offset, ushort value)
        {
            if (offset + 2 > this.ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            this.writeOffset = offset + 2;
            if (BitConverter.IsLittleEndian && this.isBigEndian)
            {
                this.bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                this.bw.Write(value);
            }
        }

        public void Write(ushort value)
        {
            this.Write(this.writeOffset, value);
        }

        public void Write(int offset, int value)
        {
            if (offset + 4 > this.ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            this.writeOffset = offset + 4;
            if (BitConverter.IsLittleEndian && this.isBigEndian)
            {
                this.bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                this.bw.Write(value);
            }
        }

        public void Write(int value)
        {
            this.Write(this.writeOffset, value);
        }

        public void Write(int offset, uint value)
        {
            if (offset + 4 > this.ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            this.writeOffset = offset + 4;
            if (BitConverter.IsLittleEndian && this.isBigEndian)
            {
                this.bw.Write(BigEndianTransfer.ToBytes(value));
            }
            else
            {
                this.bw.Write(value);
            }
        }

        public void Write(uint value)
        {
            this.Write(this.writeOffset, value);
        }

        public void Write(int offset, byte[] value, int index, int count)
        {
            if (offset + count > this.ms.Capacity)
            {
                throw new IndexOutOfRangeException();
            }
            this.ms.Position = (long)offset;
            this.writeOffset = offset + count;
            this.bw.Write(value, index, count);
        }

        public void Write(byte[] value, int index, int count)
        {
            this.Write(this.writeOffset, value, index, count);
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