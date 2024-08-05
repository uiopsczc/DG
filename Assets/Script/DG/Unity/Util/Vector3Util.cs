using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    public class Vector3Util
    {
        public static string DGToString(Vector3 v)
        {
            return string.Format("x:{0},y:{1},z:{2}", v.x, v.y, v.z);
        }

        public static System.Numerics.Vector3 To_System_Numerics_Vector3(Vector3 v)
        {
            return new System.Numerics.Vector3(v.x, v.y, v.z);
        }

        /// <summary>
        /// X的值为0
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Vector3 ZeroX(Vector3 vector3)
        {
            vector3.x = 0;
            return vector3;
        }

        /// <summary>
        /// y的值为0
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Vector3 ZeroY(Vector3 vector3)
        {
            vector3.y = 0;
            return vector3;
        }

        /// <summary>
        /// Z的值为0
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Vector3 ZeroZ(Vector3 vector3)
        {
            vector3.z = 0;
            return vector3;
        }


        public static Vector2 ToVector2(Vector3 vector3, string format = StringConst.String_X_Y)
        {
            string[] formats = format.Split(CharConst.CHAR_COMMA);
            float x = GetFormat(vector3, formats[0]);
            float y = GetFormat(vector3, formats[1]);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Vector3.ToString只保留小数后2位，看起来会卡，所以需要ToStringDetail
        /// </summary>
        public static string ToStringDetail(Vector3 vector3, string separator = StringConst.STRING_COMMA)
        {
            return vector3.x + separator + vector3.y + separator + vector3.z;
        }

        /// <summary>
        /// 将逗号改成对应的separator
        /// </summary>
        public static string ToStringReplaceSeparator(Vector3 vector3, string separator = StringConst.STRING_COMMA)
        {
            return vector3.ToString().Replace(StringConst.STRING_COMMA, separator);
        }

        public static Dictionary<string, float> ToDictionary(Vector3 vector3) //
        {
            Dictionary<string, float> ret = new Dictionary<string, float>
            {
                [StringConst.STRING_x] = vector3.x,
                [StringConst.STRING_y] = vector3.y,
                [StringConst.STRING_z] = vector3.z
            };
            return ret;
        }


        /// <summary>
        /// v1乘v2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3 Multiply(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        //将v Round四舍五入snap_size的倍数的值
        //Rounds value to the closest multiple of snap_size.
        public static Vector3 Snap(Vector3 v, Vector3 snapSize)
        {
            return new Vector3(v.x.Snap(snapSize.x), v.y.Snap(snapSize.y), v.z.Snap(snapSize.z));
        }

        public static Vector3 Snap2(Vector3 v, Vector3 snapSize)
        {
            return new Vector3(v.x.Snap2(snapSize.x), v.y.Snap2(snapSize.y), v.z.Snap2(snapSize.z));
        }

        public static Vector3 ConvertElement(Vector3 v, Func<float, float> convertElementFunc)
        {
            return new Vector3(convertElementFunc(v.x), convertElementFunc(v.y), convertElementFunc(v.z));
        }


        public static float GetFormat(Vector3 vector3, string format)
        {
            format = format.ToLower();
            if (format.Equals(StringConst.STRING_x))
                return vector3.x;
            if (format.Equals(StringConst.STRING_y))
                return vector3.y;
            if (format.Equals(StringConst.STRING_z))
                return vector3.z;
            bool flag = float.TryParse(format, out var result);
            return flag ? result : throw new Exception("错误的格式");
        }

        #region ZeroX/Y/Z;

        /// <summary>
        /// X的值为0
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Vector3 SetZeroX(Vector3 v)
        {
            return ZeroX(v);
        }

        /// <summary>
        /// y的值为0
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Vector3 SetZeroY(Vector3 v)
        {
            return ZeroY(v);
        }

        /// <summary>
        /// Z的值为0
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Vector3 SetZeroZ(Vector3 v)
        {
            return ZeroZ(v);
        }

        #endregion

        public static Vector2 XY(Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }


        public static Vector2 YZ(Vector3 v)
        {
            return new Vector2(v.y, v.z);
        }


        public static Vector2 XZ(Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }


        public static Vector3 Average(Vector3[] vs)
        {
            Vector3 total = Vector3.zero;
            for (var i = 0; i < vs.Length; i++)
            {
                Vector3 v = vs[i];
                total += v;
            }

            return vs.Length == 0 ? Vector3.zero : total / vs.Length;
        }


        public static Vector3 SetX(Vector3 v, float args)
        {
            return v.Set(StringConst.STRING_x, args);
        }

        public static Vector3 SetY(Vector3 v, float args)
        {
            return v.Set(StringConst.STRING_y, args);
        }

        public static Vector3 SetZ(Vector3 v, float args)
        {
            return v.Set(StringConst.STRING_z, args);
        }

        public static Vector3 AddX(Vector3 v, float args)
        {
            return v.Set(StringConst.STRING_x, v.x + args);
        }

        public static Vector3 AddY(Vector3 v, float args)
        {
            return v.Set(StringConst.STRING_y, v.y + args);
        }

        public static Vector3 AddZ(Vector3 v, float args)
        {
            return v.Set(StringConst.STRING_z, v.z + args);
        }


        public static Vector3 Set(Vector3 v, string format, params float[] args)
        {
            string[] formats = format.Split(CharConst.CHAR_VERTICAL);
            float x = v.x;
            float y = v.y;
            float z = v.z;

            int i = 0;
            for (var index = 0; index < formats.Length; index++)
            {
                string f = formats[index];
                switch (f.ToLower())
                {
                    case StringConst.STRING_x:
                        x = args[i];
                        break;
                    case StringConst.STRING_y:
                        y = args[i];
                        break;
                    case StringConst.STRING_z:
                        z = args[i];
                        break;
                }

                i++;
            }

            return new Vector3(x, y, z);
        }

        public static Vector3 Abs(Vector3 v)
        {
            return new Vector3(Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z));
        }


        public static bool IsDefault(Vector3 v, bool isMin = false)
        {
            return isMin ? v == Vector3Const.DEFAULT_MIN : v == Vector3Const.DEFAULT_MAX;
        }


        public static Vector3 Clamp(Vector3 v, Vector3 minPosition, Vector3 maxPosition)
        {
            return new Vector3(Mathf.Clamp(v.x, minPosition.x, maxPosition.x),
                Mathf.Clamp(v.z, minPosition.z, maxPosition.z),
                Mathf.Clamp(v.z, minPosition.z, maxPosition.z));
        }

        public static Vector3Position ToVector3Position(Vector3 v)
        {
            return new Vector3Position(v);
        }


        public static Vector3Int ToVector3Int(Vector3 v)
        {
            return new Vector3Int((int)v.x, (int)v.y, (int)v.z);
        }


        public static string ToStringOrDefault(Vector3 v, string toDefaultString = null,
            Vector3 defaultValue = default)
        {
            return ObjectUtil.Equals(v, defaultValue) ? toDefaultString : v.ToString();
        }

        public static bool IsZero(Vector3 v)
        {
            return v.Equals(Vector3.zero);
        }

        public static bool IsOne(Vector3 v)
        {
            return v.Equals(Vector3.one);
        }
    }
}