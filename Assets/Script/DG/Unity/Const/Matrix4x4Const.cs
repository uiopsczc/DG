using UnityEngine;

namespace DG
{
	public static class Matrix4x4Const
	{
		/// <summary>
		///   xy平面到xz平面的旋转矩阵
		/// </summary>
		public static Matrix4x4 XY_TO_XZ_MATRIX = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));

		/// <summary>
		///   xz平面到xy平面的旋转矩阵
		/// </summary>
		public static Matrix4x4 XZ_TO_XY_MATRIX = Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0));

		/// <summary>
		///   xy平面到yz平面的旋转矩阵
		/// </summary>
		public static Matrix4x4 XY_TO_YZ_MATRIX = Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0));

		/// <summary>
		///   yz平面到xy平面的旋转矩阵
		/// </summary>
		public static Matrix4x4 YZ_TO_XY_MATRIX = Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0));
	}
}