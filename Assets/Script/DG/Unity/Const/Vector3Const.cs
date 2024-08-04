using UnityEngine;

namespace DG
{
	public static class Vector3Const
	{
		public static Vector3 MAX = new(float.MaxValue, float.MaxValue, float.MaxValue);

		public static Vector3 MIN = new(float.MinValue, float.MinValue, float.MinValue);

		public static Vector3 DEFAULT_MAX = MAX;

		public static Vector3 DEFAULT_MIN = MIN;

		public static Vector3 DEFAULT = DEFAULT_MAX;

		public static Vector3 HALF = new(0.5f, 0.5f, 0.5f);
	}
}