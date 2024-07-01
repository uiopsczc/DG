using UnityEngine;

namespace DG
{
	public static class UnityEngine_Vector4_Extension
	{
		public static string DGToString(this Vector4 v)
		{
			return Vector4Util.DGToString(v);
		}

		public static System.Numerics.Vector4 To_System_Numerics_Vector4(this Vector4 v)
		{
			return Vector4Util.To_System_Numerics_Vector4(v);
		}

		public static string ToStringOrDefault(this Vector4 self, string toDefaultString = null,
			Vector4 defaultValue = default)
		{
			return Vector4Util.ToStringOrDefault(self, toDefaultString, defaultValue);
		}
	}
}

