using UnityEngine;

namespace DG
{
	public static class Vector3IntConst
	{
		public static Vector3Int MAX = new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);

		public static Vector3Int MIN = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);

		public static Vector3Int DEFAULT_MAX = MAX;

		public static Vector3Int DEFAULT_MIN = MIN;

		public static Vector3Int DEFAULT = DEFAULT_MAX;
	}
}