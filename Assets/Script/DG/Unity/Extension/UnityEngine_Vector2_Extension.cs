using System;
using UnityEngine;

namespace DG
{
    public static class UnityEngine_Vector2_Extension
    {
        public static string DGToString(this Vector2 v)
        {
            return Vector2Util.DGToString(v);
        }

        public static System.Numerics.Vector2 To_System_Numerics_Vector2(this Vector2 v)
        {
            return Vector2Util.To_System_Numerics_Vector2(v);
        }

        public static string ToStringDetail(this Vector2 self, string separator = StringConst.STRING_COMMA)
        {
            return Vector2Util.ToStringDetail(self, separator);
        }


        /// <summary>
        /// 叉乘
        /// </summary>
        /// <param name="self"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float Cross(this Vector2 self, Vector2 v2)
        {
            return Vector2Util.Cross(self, v2);
        }

        /// <summary>
        /// 变成Vector3
        /// </summary>
        /// <param name="self"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(this Vector2 self, string format = StringConst.STRING_X_Y_0)
        {
            return Vector2Util.ToVector3(self, format);
        }

        /// <summary>
        /// 两个值是否相等 小于或等于
        /// </summary>
        /// <param name="self"></param>
        /// <param name="v2"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static bool EqualsEPSILON(this Vector2 self, Vector2 v2, float epsilon = float.Epsilon)
        {
            return Vector2Util.EqualsEPSILON(self, v2, epsilon);
        }

        public static Vector2 Average(this Vector2[] selfs)
        {
            return Vector2Util.Average(selfs);
        }

        public static Vector2 SetX(this Vector2 self, float args)
        {
            return Vector2Util.SetX(self, args);
        }

        public static Vector2 SetY(this Vector2 self, float args)
        {
            return Vector2Util.SetY(self, args);
        }

        public static Vector2 AddX(this Vector2 self, float args)
        {
            return Vector2Util.AddX(self, args);
        }

        public static Vector2 AddY(this Vector2 self, float args)
        {
            return Vector2Util.AddY(self, args);
        }

        public static Vector2 Set(this Vector2 self, string format, params float[] args)
        {
            return Vector2Util.Set(self, format, args);
        }

        public static Vector2 Abs(this Vector2 self)
        {
            return Vector2Util.Abs(self);
        }

        //将v Round四舍五入snap_size的倍数的值
        //Rounds value to the closest multiple of snap_size.
        public static Vector2 Snap(this Vector2 self, Vector2 snapSize)
        {
            return Vector2Util.Snap(self, snapSize);
        }

        public static Vector2 Snap2(this Vector2 self, Vector2 snapSize)
        {
            return Vector2Util.Snap2(self, snapSize);
        }

        public static Vector2 ConvertElement(this Vector2 self, Func<float, float> convertElementFunc)
        {
            return Vector2Util.ConvertElement(self, convertElementFunc);
        }

        public static Vector2Int ToVector2Int(this Vector2 self)
        {
            return Vector2Util.ToVector2Int(self);
        }

        public static string ToStringOrDefault(this Vector2 self, string toDefaultString = null,
            Vector2 defaultValue = default(Vector2))
        {
            return Vector2Util.ToStringOrDefault(self, toDefaultString, defaultValue);
        }

        public static bool IsDefault(this Vector2 self, bool isMin = false)
        {
            return Vector2Util.IsDefault(self);
        }


        public static bool IsZero(this Vector2 self)
        {
            return Vector2Util.IsZero(self);
        }


        public static bool IsOne(this Vector2 self)
        {
            return Vector2Util.IsOne(self);
        }
    }
}