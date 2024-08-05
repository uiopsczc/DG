using UnityEngine;

namespace DG
{
    public static class UnityEngine_Vector2Int_Extension
    {
        public static bool IsDefault(this Vector2Int self, bool isMin = false)
        {
            return Vector2IntUtil.IsDefault(self, isMin);
        }

        public static string ToStringOrDefault(this Vector2Int self, string toDefaultString = null,
            Vector2Int defaultValue = default)
        {
            return Vector2IntUtil.ToStringOrDefault(self, toDefaultString, defaultValue);
        }

        public static Vector2Int Abs(this Vector2Int self)
        {
            return Vector2IntUtil.Abs(self);
        }


        public static bool IsZero(this Vector2Int self)
        {
            return Vector2IntUtil.IsZero(self);
        }


        public static bool IsOne(this Vector2Int self)
        {
            return Vector2IntUtil.IsOne(self);
        }
    }
}