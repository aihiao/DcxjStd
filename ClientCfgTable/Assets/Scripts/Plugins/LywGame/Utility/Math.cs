using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LywGames
{
    public static class Math
    {
        public const float PI = 3.14159f;
        public const float EPSILON = 0.0001f;

        public static int Abs(int value)
        {
            return value > 0 ? value : -value;
        }

        public static float Abs(float value)
        {
            return value > 0 ? value : -value;
        }

        public static double Abs(double value)
        {
            return value > 0 ? value : -value;
        }
        public static float Min(float a, float b)
        {
            return a < b ? a : b;
        }
        public static int RoundToInt(double value)
        {
            if (value > 0)
                return (int)(value + 0.5);
            else
                return -((int)(Abs(value) + 0.5));
        }

        public static float RadianToDegree(float radian)
        {
            return radian * 360.0f / ((float)PI * 2);
        }

        public static float DegreeToRadian(float degree)
        {
            return degree * ((float)PI * 2) / 360.0f;
        }

        public static double Ceil(double value)
        {
            return (double)CeilToInt(value);
        }

        public static double Floor(double value)
        {
            return (double)FloorToInt(value);
        }

        public static long CeilToInt(double value)
        {
            if (value >= 0)
                return (value - (long)value) != 0 ? (long)(value + 1) : (long)value;
            else
                return (long)value;
        }

        public static long FloorToInt(double value)
        {
            if (value >= 0)
                return (long)value;
            else
                return (value - (long)value) != 0 ? (long)(value - 1) : (long)value;
        }

        /// <summary>
        /// Clamp a angle in 0 to 360
        /// </summary>
        public static float ClampAngle(float angle)
        {
            angle = angle - (((int)angle) / 360) * 360;
            if (angle < 0)
                angle += 360;

            return angle >= 0 ? angle : angle + 360;
        }

        /// <summary>
        /// Clamp a angle in 0 to 360
        /// </summary>
        public static float ClampAngle180(float angle)
        {
            return ClampAngle(angle + 180) - 180;
        }

        public static float Clamp(float value, float minValue, float maxValue)
        {
            AssertHelper.AssetFalse(minValue <= maxValue);
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        public static int Clamp(int value, int minValue, int maxValue)
        {
            AssertHelper.AssetFalse(minValue <= maxValue);
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        public static float LerpWithoutClamp(float from, float to, float t)
        {
            return from + (to - from) * t;
        }

        public static float LerpAngleWithoutClamp(float from, float to, float t)
        {
            return ClampAngle(LerpWithoutClamp(from, to, t));
        }

        /// <summary>
        /// Integer Wrap
        /// </summary>
        /// <param name="val">Value to be tested for between <b>min</b>, <b>max</b></param>
        /// <param name="minv">Minimum bound to apply to <b>val</b>.</param>
        /// <param name="maxv">Maximum bound to apply to <b>val</b>.</param>
        /// <returns>Returns the wrapped value between <b>min</b> and <b>max</b>.  Such that <b>min</b> = 1, <b>max</b> = 5 then passing in 7 will return a value of 2.</returns>
        public static int Wrap(int val, int minv, int maxv)
        {
            // T must be an integral
            int range = (maxv - minv);
            if (range == 0) return 0;

            if (val > maxv)
            {
                int tval = ((val - 1) - maxv) % range;
                return minv + tval;
            }
            else if (val < minv)
            {
                int tval = (minv - (val + 1)) % range;
                return maxv - tval;
            }
            return val;
        }

        public static int CombineValue(int high, int low)
        {
            return ((int)((high & 0x0000FFFF) << 16)) | (low & 0x0000FFFF);
        }

        public static void SplitValue(int value, out int high, out int low)
        {
            high = (int)((value & 0xFFFF0000) >> 16);
            low = value & 0x0000FFFF;
        }

        public static bool IsEqualFloat(float a, float b)
        {
            return IsEqualFloat(a, b, EPSILON);
        }

        public static bool IsEqualFloat(float a, float b, float diff)
        {
            return Abs(a - b) <= diff;
        }

    }
}
