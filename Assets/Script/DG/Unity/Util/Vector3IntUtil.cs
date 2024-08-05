using System;
using UnityEngine;

namespace DG
{
    public class Vector3IntUtil
    {
        public static string ToStringOrDefault(Vector3Int v, string toDefaultString = null,
            Vector3Int defaultValue = default)
        {
            return ObjectUtil.Equals(v, defaultValue) ? toDefaultString : v.ToString();
        }

        public static bool IsDefault(Vector3Int v, bool isMin = false)
        {
            return isMin ? v == Vector3IntConst.DEFAULT_MIN : v == Vector3IntConst.DEFAULT_MAX;
        }


        public static Vector3Int Abs(Vector3Int v)
        {
            return new Vector3Int(Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z));
        }


        public static bool IsZero(Vector3Int v)
        {
            return v.Equals(Vector3Int.zero);
        }

        public static bool IsOne(Vector3Int v)
        {
            return v.Equals(Vector3Int.one);
        }
    }
}