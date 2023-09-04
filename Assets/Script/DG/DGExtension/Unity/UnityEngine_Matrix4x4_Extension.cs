using UnityEngine;

namespace DG
{
	public static class UnityEngine_Matrix4x4_Extension
	{
		public static string DGToString(this Matrix4x4 v)
		{
			return Matrix4x4Util.DGToString(v);
		}

		public static System.Numerics.Matrix4x4 To_System_Numerics_Matrix4x4(this Matrix4x4 matrix)
		{
			return Matrix4x4Util.To_System_Numerics_Matrix4x4(matrix);
		}
	}
}