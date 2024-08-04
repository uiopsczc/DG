using UnityEngine;

namespace DG
{
	public static class QuaternionConst
	{
		public static Quaternion MAX = new(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue);

		public static Quaternion MIN = new(float.MinValue, float.MinValue, float.MinValue, float.MaxValue);

		public static Quaternion DEFAULT_MAX = MAX;

		public static Quaternion DEFAULT_MIN = MIN;

		public static Quaternion DEFAULT = DEFAULT_MAX;
	}
}