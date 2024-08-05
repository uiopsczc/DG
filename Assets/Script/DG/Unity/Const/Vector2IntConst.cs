using UnityEngine;

namespace DG
{
    public static class Vector2IntConst
    {
        public static Vector2Int MAX = new(int.MaxValue, int.MaxValue);

        public static Vector2Int MIN = new(int.MinValue, int.MinValue);

        public static Vector2Int DEFAULT_MAX = MAX;

        public static Vector2Int DEFAULT_MIN = MIN;

        public static Vector2Int DEFAULT = DEFAULT_MAX;

        public static Vector2Int LEFT_TOP = new(-1, 1);
        public static Vector2Int TOP = new(0, 1);
        public static Vector2Int RIGHT_TOP = new(1, 1);
        public static Vector2Int LEFT = new(-1, 0);
        public static Vector2Int CENTER = new(0, 0);
        public static Vector2Int RIGHT = new(0, 1);
        public static Vector2Int LEFT_BOTTOM = new(-1, -1);
        public static Vector2Int BOTTOM = new(0, -1);
        public static Vector2Int RIGHT_BOTTOM = new(1, -1);
    }
}