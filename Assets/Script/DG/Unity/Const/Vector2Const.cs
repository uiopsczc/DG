using UnityEngine;

namespace DG
{
    public static class Vector2Const
    {
        public static Vector2 MAX = new(float.MaxValue, float.MaxValue);

        public static Vector2 MIN = new(float.MinValue, float.MinValue);

        public static Vector2 DEFAULT_MAX = MAX;

        public static Vector2 DEFAULT_MIN = MIN;

        public static Vector2 DEFAULT = DEFAULT_MAX;

        public static Vector2 HALF = new(0.5f, 0.5f);
    }
}