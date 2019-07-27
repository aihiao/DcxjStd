using System;

namespace LywGames.Common
{
    public class BigEndianTransfer
    {
        private static byte[] ToFixedLength(byte[] a, int length)
        {
            byte[] result;
            if (a.Length > length)
            {
                result = null;
            }
            else
            {
                if (a.Length == length)
                {
                    result = a;
                }
                else
                {
                    byte[] array = new byte[length];
                    a.CopyTo(array, length - a.Length);
                    result = array;
                }
            }
            return result;
        }

        public static ulong ToUInt64(byte[] data)
        {
            byte[] array = ToFixedLength(data, 8);
            ulong result;
            if (array == null)
            {
                result = 0uL;
            }
            else
            {
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(array);
                }
                result = BitConverter.ToUInt64(array, 0);
            }
            return result;
        }

        public static byte[] ToBytes(ulong num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static long ToInt64(byte[] data)
        {
            byte[] array = ToFixedLength(data, 8);
            long result;
            if (array == null)
            {
                result = 0L;
            }
            else
            {
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(array);
                }
                result = BitConverter.ToInt64(array, 0);
            }
            return result;
        }

        public static byte[] ToBytes(long num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static uint ToUInt32(byte[] data)
        {
            byte[] array = ToFixedLength(data, 4);
            uint result;
            if (array == null)
            {
                result = 0u;
            }
            else
            {
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(array);
                }
                result = BitConverter.ToUInt32(array, 0);
            }
            return result;
        }

        public static byte[] ToBytes(uint num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static int ToInt32(byte[] data)
        {
            byte[] array = ToFixedLength(data, 4);
            int result;
            if (array == null)
            {
                result = 0;
            }
            else
            {
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(array);
                }
                result = BitConverter.ToInt32(array, 0);
            }
            return result;
        }

        public static int ToInt32(byte[] data, int offset)
        {
            int result;
            if (data.Length <= offset)
            {
                result = 0;
            }
            else
            {
                if (BitConverter.IsLittleEndian)
                {
                    byte[] array = new byte[4];
                    int count = (data.Length - offset > 4) ? 4 : (data.Length - offset);
                    Buffer.BlockCopy(data, offset, array, 0, count);
                    Array.Reverse(array);
                    result = BitConverter.ToInt32(array, 0);
                }
                else
                {
                    result = BitConverter.ToInt32(data, offset);
                }
            }
            return result;
        }

        public static byte[] ToBytes(int num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static ushort ToUInt16(byte[] data)
        {
            byte[] array = ToFixedLength(data, 2);
            ushort result;
            if (array == null)
            {
                result = 0;
            }
            else
            {
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(array);
                }
                result = BitConverter.ToUInt16(array, 0);
            }
            return result;
        }

        public static ushort ToUInt16(byte[] data, int offset)
        {
            ushort result;
            if (data.Length <= offset)
            {
                result = 0;
            }
            else
            {
                if (BitConverter.IsLittleEndian)
                {
                    byte[] array = new byte[]
                    {
                        0,
                        data[offset]
                    };
                    if (data.Length - offset > 1)
                    {
                        array[0] = data[offset + 1];
                    }
                    result = BitConverter.ToUInt16(array, 0);
                }
                else
                {
                    result = BitConverter.ToUInt16(data, offset);
                }
            }
            return result;
        }

        public static byte[] ToBytes(ushort num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static short ToInt16(byte[] data)
        {
            byte[] array = ToFixedLength(data, 2);
            short result;
            if (array == null)
            {
                result = 0;
            }
            else
            {
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(array);
                }
                result = BitConverter.ToInt16(array, 0);
            }
            return result;
        }

        public static byte[] ToBytes(short num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

    }
}
