using System;
using UnityEngine;

namespace DG
{
    public class Vector2Util
    {
        public static string DGToString(Vector2 v)
        {
            return string.Format("x:{0},y:{1}", v.x, v.y);
        }

        public static System.Numerics.Vector2 To_System_Numerics_Vector2(Vector2 v)
        {
            return new System.Numerics.Vector2(v.x, v.y);
        }

        /// <summary>
        /// Vector2.ToString只保留小数后2位，看起来会卡，所以需要ToStringDetail
        /// </summary>
        public static string ToStringDetail(Vector2 vector2, string separator = StringConst.STRING_COMMA)
        {
            return vector2.x + separator + vector2.y;
        }

        /// <summary>
        /// 叉乘
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float Cross(Vector2 v1, Vector2 v2)
        {
            return v1.x * v2.y - v2.x * v1.y;
        }

        /// <summary>
        /// 变成Vector3
        /// </summary>
        /// <param name="v"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(Vector2 v, string format = StringConst.STRING_X_Y_0)
        {
            string[] formats = format.Split(CharConst.CHAR_COMMA);
            float x = Vector3Util.GetFormat(v, formats[0]);
            float y = Vector3Util.GetFormat(v, formats[1]);
            float z = Vector3Util.GetFormat(v, formats[2]);
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// 两个值是否相等 小于或等于
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static bool EqualsEPSILON(Vector2 v1, Vector2 v2, float epsilon = float.Epsilon)
        {
            return (v1.x.EqualsEpsilon(v2.x, epsilon)) && (v1.y.EqualsEpsilon(v2.y, epsilon));
        }

        //将v Round四舍五入snap_size的倍数的值
        //Rounds value to the closest multiple of snap_size.
        public static Vector2 Snap(Vector2 v, Vector2 snapSize)
        {
            return new Vector2(v.x.Snap(snapSize.x), v.y.Snap(snapSize.y));
        }

        public static Vector2 Snap2(Vector2 v, Vector2 snapSize)
        {
            return new Vector2(v.x.Snap2(snapSize.x), v.y.Snap2(snapSize.y));
        }

        public static Vector2 ConvertElement(Vector2 v, Func<float, float> convertElementFunc)
        {
            return new Vector2(convertElementFunc(v.x), convertElementFunc(v.y));
        }

        public static Vector2 Average(Vector2[] vs)
        {
            Vector2 total = Vector2.zero;
            for (var i = 0; i < vs.Length; i++)
            {
                Vector2 v = vs[i];
                total += v;
            }

            return vs.Length == 0 ? Vector2.zero : total / vs.Length;
        }

        public static Vector2 SetX(Vector2 v, float args)
        {
            return v.Set("x", args);
        }

        public static Vector2 SetY(Vector2 v, float args)
        {
            return v.Set("y", args);
        }

        public static Vector2 AddX(Vector2 v, float args)
        {
            return v.Set("x", v.x + args);
        }

        public static Vector2 AddY(Vector2 v, float args)
        {
            return v.Set("y", v.y + args);
        }

        public static Vector2 Set(Vector2 v, string format, params float[] args)
        {
            string[] formats = format.Split('|');
            float x = v.x;
            float y = v.y;

            int i = 0;
            for (var index = 0; index < formats.Length; index++)
            {
                string f = formats[index];
                if (f.ToLower().Equals("x"))
                {
                    x = args[i];
                    i++;
                }

                if (f.ToLower().Equals("y"))
                {
                    y = args[i];
                    i++;
                }
            }

            return new Vector2(x, y);
        }

        public static Vector2 Abs(Vector2 v)
        {
            return new Vector2(Math.Abs(v.x), Math.Abs(v.y));
        }

        public static string ToStringOrDefault(Vector2 v, string toDefaultString = null,
            Vector2 defaultValue = default(Vector2))
        {
            if (ObjectUtil.Equals(v, defaultValue))
                return toDefaultString;
            return v.ToString();
        }

        public static Vector2Int ToVector2Int(Vector2 v)
        {
            return new Vector2Int((int)v.x, (int)v.y);
        }

        public static bool IsDefault(Vector2 v, bool isMin = false)
        {
            if (isMin)
                return v == Vector2Const.DEFAULT_MIN;
            return v == Vector2Const.DEFAULT_MAX;
        }


        public static bool IsZero(Vector2 v)
        {
            if (v.Equals(Vector2.zero))
                return true;
            return false;
        }

        public static bool IsOne(Vector2 v)
        {
            if (v.Equals(Vector2.one))
                return true;
            return false;
        }
    }
}