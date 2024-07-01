using UnityEngine;

namespace DG
{
	public static class UnityEngine_Matrix4x4_Extension
	{
		public static string DGToString(this Matrix4x4 self)
		{
			return Matrix4x4Util.DGToString(self);
		}

		public static System.Numerics.Matrix4x4 To_System_Numerics_Matrix4x4(this Matrix4x4 self)
		{
			return Matrix4x4Util.To_System_Numerics_Matrix4x4(self);
		}

		/// <summary>
		/// 通过矩阵获取Rotation
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Quaternion GetRotation(this Matrix4x4 self)
		{
			return Matrix4x4Util.GetRotation(self);
		}

		/// <summary>
		/// 通过矩阵获取Position
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector3 GetPosition(this Matrix4x4 self)
		{
			return Matrix4x4Util.GetPosition(self);
		}

		/// <summary>
		/// 通过矩阵获取Scale
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector3 GetScale(this Matrix4x4 self)
		{
			return Matrix4x4Util.GetScale(self);
		}
	}
}