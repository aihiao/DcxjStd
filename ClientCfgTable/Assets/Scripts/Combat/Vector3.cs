using System;

namespace Combat
{
    /// <summary>
	/// Representation of 3D vectors and points.
	/// 为了转换为Java重写的类
	/// 注意
	/// 1. Java没有Struct, 函数中不要直接修改成员变量, 用返回新构造的类来表示.
	/// 2. Java不能重载操作符, 所有的操作符重载, 使用函数实现
	/// 3. 注意所有赋值操作, 除了初始化(new), 不要使用=赋值, 保证在Java不会引用其他的实例
	/// </summary>
    public struct Vector3
    {
        public static Vector3 zero { get { return new Vector3(0, 0, 0); } }
        public static Vector3 one { get { return new Vector3(1, 1, 1); } }
        public static Vector3 up { get { return new Vector3(0, 1, 0); } }
        public static Vector3 down { get { return new Vector3(0, -1, 0); } }
        public static Vector3 forward { get { return new Vector3(0, 0, 1); } }
        public static Vector3 back { get { return new Vector3(0, 0, -1); } }
        public static Vector3 left { get { return new Vector3(-1, 0, 0); } }
        public static Vector3 right { get { return new Vector3(1, 0, 0); } }


        /// <summary>
        /// X component of the vector.
        /// </summary>
        public float x;

        /// <summary>
        /// Y component of the vector.
        /// </summary>
        public float y;

        /// <summary>
        /// Z component of the vector.
        /// </summary>
        public float z;

        public Vector3(float x, float y)
        {
            this.x = x; this.y = y; this.z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x; this.y = y; this.z = z;
        }

        public Vector3(Vector2 rhs)
        {
            this.x = rhs.x; this.y = rhs.y; this.z = 0;
        }

#if COMBAT_CLIENT
        public Vector3(UnityEngine.Vector3 rhs)
        {
            this.x = rhs.x;
            this.y = rhs.y;
            this.z = rhs.z;
        }

        public static implicit operator UnityEngine.Vector3(Vector3 v) { return new UnityEngine.Vector3(v.x, v.y, v.z); }
        public static implicit operator Vector3(UnityEngine.Vector3 v) { return new Vector3(v); }
#endif

#if COMBAT_CLIENT
        [Obsolete("为了转移到java, 屏蔽这个函数", true)]
        public static bool operator ==(Vector3 lhs, Vector3 rhs) { return false; }
        [Obsolete("为了转移到java, 屏蔽这个函数", true)]
        public static bool operator !=(Vector3 lhs, Vector3 rhs) { return false; }
        [Obsolete("为了转移到java, 屏蔽这个函数", true)]
        public override bool Equals(object obj) { return false; }
#endif
        public bool IsEqual(Vector3 lhs) { return IsEqual(lhs, LywGames.Math.EPSILON); }
        public bool IsEqual(Vector3 lhs, float tolerance)
        {
            return LywGames.Math.IsEqualFloat(x, lhs.x, tolerance)
                && LywGames.Math.IsEqualFloat(y, lhs.y, tolerance)
                && LywGames.Math.IsEqualFloat(z, lhs.z, tolerance);
        }

        public override string ToString()
        {
            return ToString("({0},{1},{2})");
        }

        /// <summary>
        /// Returns a nicely formatted string for this vector.
        /// </summary>
        public string ToString(string format)
        {
            return string.Format(format, x, y, z);
        }

        /// <summary>
        /// Returns the length of this vector (Read Only).
        /// </summary>
        public float Magnitude() { return (float)Math.Sqrt(Dot(this)); }
        public float SqrMagnitude() { return Dot(this); }

        /// <summary>
        /// Returns this vector with a magnitude of 1 (Read Only).
        /// </summary>
        public Vector3 Normalize()
        {
            float s = this.Magnitude();
            return s == 0 ? Vector3.zero : new Vector3(x / s, y / s, z / s);
        }

        public float NormalizeSelf()
        {
            float s = this.Magnitude();
            if (s != 0)
            {
                x /= s; y /= s; z /= s;
            }
            else
            {
                x = y = z = 0;
            }
            return s;
        }

        public Vector3 Add(Vector3 rhs) { return new Vector3(x + rhs.x, y + rhs.y, z + rhs.z); }
        public Vector3 Sub(Vector3 rhs) { return new Vector3(x - rhs.x, y - rhs.y, z - rhs.z); }
        public Vector3 Scale(float a) { return new Vector3(x * a, y * a, z * a); }
        public Vector3 Scale(Vector3 rhs) { return new Vector3(x * rhs.x, y * rhs.y, z * rhs.z); }
        public float Dot(Vector3 rhs) { return this.x * rhs.x + this.y * rhs.y + this.z * rhs.z; }
        public Vector3 Cross(Vector3 rhs) { return new Vector3(y * rhs.z - z * rhs.y, x * rhs.z - z * rhs.x, x * rhs.y - y * rhs.x); }

        public Vector3 MultiplyElements(Vector3 rhs) { return new Vector3(x * rhs.x, y * rhs.y, z * rhs.z); }
        public Vector3 Negate() { return new Vector3(-x, -y, -z); }
        public Vector2 ProjectToXZ() { return new Vector2(x, z); }

        public static Vector3 FromXZVector(Vector2 v) { return new Vector3(v.x, 0, v.y); }
        public static Vector3 FromXZVector(Vector2 v, float height) { return new Vector3(v.x, height, v.y); }

        public static Vector3 Parse(string str)
        {
            str = str.TrimStart('(');
            str = str.TrimEnd(')');

            string[] vecs = str.Split(',');
            if (vecs.Length < 3)
                return Vector3.zero;

            Vector3 ret = new Vector3();
            ret.x = LywGames.StrParser.ParseFloat(vecs[0]);
            ret.y = LywGames.StrParser.ParseFloat(vecs[1]);
            ret.z = LywGames.StrParser.ParseFloat(vecs[2]);
            return ret;
        }

        /// <summary>
		/// Returns the angle in degrees between from and to.
		/// </summary>
		public static float Angle(Vector3 from, Vector3 to)
        {
            return (float)Math.Acos(LywGames.Math.Clamp(from.Normalize().Dot(to.Normalize()), -1f, 1f)) * 57.29578f;
        }

        /// <summary>
		/// Returns the distance between a and b.
		/// </summary>
		public static float Distance(Vector3 a, Vector3 b)
        {
            return (float)Math.Sqrt(SqrDistance(a, b));
        }

        public static float SqrDistance(Vector3 a, Vector3 b)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            float dz = a.z - b.z;
            return dx * dx + dy * dy + dz * dz;
        }

        /// <summary>
        /// Linearly interpolates between two vectors.
        /// </summary>
        public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
        {
            t = LywGames.Math.Clamp(t, 0f, 1f);
            return new Vector3(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t);
        }

    }
}
