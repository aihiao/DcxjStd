using System;

namespace Combat
{
    /// <summary>
	/// Representation of 2D vectors and points.
	/// </summary>
	public struct Vector2
    {
        public const float kEpsilon = 1e-005f;

        public static Vector2 zero { get { return new Vector2(0, 0); } }
        public static Vector2 one { get { return new Vector2(1, 1); } }
        public static Vector2 up { get { return new Vector2(0, 1); } }
        public static Vector2 down { get { return new Vector2(0, -1); } }
        public static Vector2 left { get { return new Vector2(-1, 0); } }
        public static Vector2 right { get { return new Vector2(1, 0); } }

        /// <summary>
        /// X component of the vector.
        /// </summary>
        public float x;

        /// <summary>
        /// Y component of the vector.
        /// </summary>
        public float y;

        public Vector2(float x, float y) { this.x = x; this.y = y; }
        public Vector2(Vector2 rhs) { this.x = rhs.x; this.y = rhs.y; }
        public Vector2(Vector3 rhs) { this.x = rhs.x; this.y = rhs.y; }

#if COMBAT_CLIENT
        public Vector2(UnityEngine.Vector2 rhs)
        {
            this.x = rhs.x;
            this.y = rhs.y;
        }

        public static implicit operator UnityEngine.Vector2(Vector2 v) { return new UnityEngine.Vector2(v.x, v.y); }
        public static implicit operator Vector2(UnityEngine.Vector2 v) { return new Vector2(v); }
        public UnityEngine.Vector3 ToUnityVector3(float height) { return new UnityEngine.Vector3(x, height, y); }
#endif
#if UNITY_EDITOR
        [Obsolete("为了转移到java, 屏蔽这个函数", true)]
        public static bool operator ==(Vector2 lhs, Vector2 rhs) { return false; }
        [Obsolete("为了转移到java, 屏蔽这个函数", true)]
        public static bool operator !=(Vector2 lhs, Vector2 rhs) { return false; }
        [Obsolete("为了转移到java, 屏蔽这个函数", true)]
        public override bool Equals(object obj) { return false; }
#endif

        public bool IsEqual(Vector2 lhs) { return IsEqual(lhs, LywGames.Math.EPSILON); }
        public bool IsEqual(Vector2 lhs, float tolerance)
        {
            return LywGames.Math.IsEqualFloat(x, lhs.x, tolerance)
                && LywGames.Math.IsEqualFloat(y, lhs.y, tolerance);
        }

        public override string ToString()
        {
            return ToString("({0},{1})");
        }

        /// <summary>
        /// Returns a nicely formatted string for this vector.
        /// </summary>
        public string ToString(string format)
        {
            return string.Format(format, x, y);
        }

        /// <summary>
        /// Returns the length of this vector (Read Only).
        /// </summary>
        public float Magnitude() { return (float)Math.Sqrt(Dot(this)); }

        /// <summary>
        /// Returns the squared length of this vector (Read Only).
        /// </summary>
        /// <returns></returns>
        public float SqrMagnitude() { return Dot(this); }

        /// <summary>
        /// Returns this vector with a magnitude of 1 (Read Only).
        /// </summary>
        public Vector2 Normalize()
        {
            float s = this.Magnitude();
            return s == 0 ? Vector2.zero : new Vector2(x / s, y / s);
        }

        public float NormalizeSelf()
        {
            float s = this.Magnitude();
            if (s != 0)
            {
                x /= s; y /= s;
            }
            else
            {
                x = y = 0;
            }
            return s;
        }

        public Vector2 Negate() { return new Vector2(-x, -y); }
        public Vector2 Add(Vector2 rhs) { return new Vector2(x + rhs.x, y + rhs.y); }
        public Vector2 Sub(Vector2 rhs) { return new Vector2(x - rhs.x, y - rhs.y); }
        public Vector2 Scale(float a) { return new Vector2(x * a, y * a); }
        public Vector2 Scale(Vector2 rhs) { return new Vector2(x * rhs.x, y * rhs.y); }
        public float Dot(Vector2 rhs) { return this.x * rhs.x + this.y * rhs.y; }
        public Vector2 Rotate(float rads) { return Rotate((float)Math.Sin(rads), (float)Math.Cos(rads)); }
        public Vector2 Rotate(float s, float c) { return new Vector2(c * x - s * y, s * x + c * y); }

        public float GetComponent(int index)
        {
            switch (index)
            {
                case 0: return x;
                case 1: return y;
                default: AssertHelper.AssetFalse(false); return 0;
            }
        }

        public float GetMinComponent() { return Math.Min(x, y); }
        public float GetMaxComponent() { return Math.Max(x, y); }

        public void UpdateMin(Vector2 v) { x = Math.Min(x, v.x); y = Math.Min(y, v.y); }
        public void UpdateMax(Vector2 v) { x = Math.Max(x, v.x); y = Math.Max(y, v.y); }

        /// <summary>
        /// Returns the angle in degrees between from and to.
        /// </summary>
        public static float Angle(Vector2 from, Vector2 to)
        {
            return (float)Math.Acos(LywGames.Math.Clamp(from.Normalize().Dot(to.Normalize()), -1f, 1f)) * 57.29578f;
        }

        /// <summary>
        /// Returns the distance between a and b.
        /// </summary>
        public static float Distance(Vector2 a, Vector2 b)
        {
            return (float)Math.Sqrt(SqrDistance(a, b));
        }

        public static float SqrDistance(Vector2 a, Vector2 b)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            return dx * dx + dy * dy;
        }

        public static Vector2 Lerp(Vector2 from, Vector2 to, float t)
        {
            t = LywGames.Math.Clamp(t, 0, 1);
            return new Vector2(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t);
        }

        /// <summary>
		/// 通过向量点积判断是否到达（跨越过）了目标位置, 若跨越过了，那么夹角大于90度，即点击小于0 
		/// </summary>
		public static bool IsReached(Vector2 dest, Vector2 currentPos, Vector2 lastFramePos)
        {
            if (LywGames.Math.IsEqualFloat(Vector2.SqrDistance(currentPos, lastFramePos), 0f))
                return true;

            Vector2 lastToCurrent = currentPos.Sub(lastFramePos);
            Vector2 currentToDest = dest.Sub(currentPos);
            return lastToCurrent.Dot(currentToDest) <= 0;
        }

    }
}
