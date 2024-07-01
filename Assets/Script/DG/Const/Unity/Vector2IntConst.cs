using UnityEngine;

namespace DG
{
	public static class Vector2IntConst
	{
		public static Vector2Int MAX = new Vector2Int(int.MaxValue, int.MaxValue);

		public static Vector2Int MIN = new Vector2Int(int.MinValue, int.MinValue);

		public static Vector2Int DEFAULT_MAX = MAX;

		public static Vector2Int DEFAULT_MIN = MIN;

		public static Vector2Int DEFAULT = DEFAULT_MAX;

		public static Vector2Int LEFT_TOP = new Vector2Int(-1, 1);
		public static Vector2Int TOP = new Vector2Int(0, 1);
		public static Vector2Int RIGHT_TOP = new Vector2Int(1, 1);
		public static Vector2Int LEFT = new Vector2Int(-1, 0);
		public static Vector2Int CENTER = new Vector2Int(0, 0);
		public static Vector2Int RIGHT = new Vector2Int(0, 1);
		public static Vector2Int LEFT_BOTTOM = new Vector2Int(-1, -1);
		public static Vector2Int BOTTOM = new Vector2Int(0, -1);
		public static Vector2Int RIGHT_BOTTOM = new Vector2Int(1, -1);
	}
}