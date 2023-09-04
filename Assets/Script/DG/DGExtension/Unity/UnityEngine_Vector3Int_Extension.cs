using UnityEngine;

namespace DG
{
	public static class UnityEngine_Vector3Int_Extension
	{
		public static string ToStringOrDefault(this Vector3Int self, string toDefaultString = null,
			Vector3Int defaultValue = default)
		{
			return Vector3IntUtil.ToStringOrDefault(self, toDefaultString, defaultValue);
		}

		public static bool IsDefault(this Vector3Int self, bool isMin = false)
		{
			return Vector3IntUtil.IsDefault(self, isMin);
		}


		public static Vector3Int Abs(this Vector3Int self)
		{
			return Vector3IntUtil.Abs(self);
		}


		public static bool IsZero(this Vector3Int self)
		{
			return Vector3IntUtil.IsZero(self);
		}

		public static bool IsOne(this Vector3Int self)
		{
			return Vector3IntUtil.IsOne(self);
		}
	}
}


