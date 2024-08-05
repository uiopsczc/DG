using UnityEngine;

namespace DG
{
    public class Vector4Util
    {
        public static string DGToString(Vector4 v)
        {
            return string.Format("x:{0},y:{1},z:{2},w:{3}", v.x, v.y, v.z, v.w);
        }

        public static System.Numerics.Vector4 To_System_Numerics_Vector4(Vector4 v)
        {
            return new System.Numerics.Vector4(v.x, v.y, v.z, v.w);
        }

        public static string ToStringOrDefault(Vector4 v, string toDefaultString = null,
            Vector4 defaultValue = default)
        {
            return ObjectUtil.Equals(v, defaultValue) ? toDefaultString : v.ToString();
        }
    }
}