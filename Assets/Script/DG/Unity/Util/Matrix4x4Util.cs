using UnityEngine;

namespace DG
{
	public class Matrix4x4Util
	{
		public static string DGToString(Matrix4x4 v)
		{
			return "{" + v.m00 + ", " + v.m01 + ", " + v.m02 + ", " + v.m03 + "} \n" +
			       "{" + v.m10 + ", " + v.m11 + ", " + v.m12 + ", " + v.m13 + "} \n" +
			       "{" + v.m20 + ", " + v.m21 + ", " + v.m22 + ", " + v.m23 + "} \n" +
			       "{" + v.m30 + ", " + v.m31 + ", " + v.m32 + ", " + v.m33 + "} \n";
		}

		public static System.Numerics.Matrix4x4 To_System_Numerics_Matrix4x4(Matrix4x4 matrix)
		{
			return new System.Numerics.Matrix4x4(matrix.m00,
				matrix.m01,
				matrix.m02,
				matrix.m03,
				matrix.m10,
				matrix.m11,
				matrix.m12,
				matrix.m13,
				matrix.m20,
				matrix.m21,
				matrix.m22,
				matrix.m23,
				matrix.m30,
				matrix.m31,
				matrix.m32,
				matrix.m33);
		}

		/// <summary>
		/// 通过矩阵获取Rotation
		/// </summary>
		/// <param name="matrix4x4"></param>
		/// <returns></returns>
		public static Quaternion GetRotation(Matrix4x4 matrix4x4)
		{
			float qw = Mathf.Sqrt(1f + matrix4x4.m00 + matrix4x4.m11 + matrix4x4.m22) / 2;
			float w = 4 * qw;
			float qx = (matrix4x4.m21 - matrix4x4.m12) / w;
			float qy = (matrix4x4.m02 - matrix4x4.m20) / w;
			float qz = (matrix4x4.m10 - matrix4x4.m01) / w;
			return new Quaternion(qx, qy, qz, qw);
		}

		/// <summary>
		/// 通过矩阵获取Position
		/// </summary>
		/// <param name="matrix4x4"></param>
		/// <returns></returns>
		public static Vector3 GetPosition(Matrix4x4 matrix4x4)
		{
			float x = matrix4x4.m03;
			float y = matrix4x4.m13;
			float z = matrix4x4.m23;
			return new Vector3(x, y, z);
		}

		/// <summary>
		/// 通过矩阵获取Scale
		/// </summary>
		/// <param name="matrix4x4"></param>
		/// <returns></returns>
		public static Vector3 GetScale(Matrix4x4 matrix4x4)
		{
			float x = Mathf.Sqrt(
				matrix4x4.m00 * matrix4x4.m00 + matrix4x4.m01 * matrix4x4.m01 + matrix4x4.m02 * matrix4x4.m02);
			float y = Mathf.Sqrt(
				matrix4x4.m10 * matrix4x4.m10 + matrix4x4.m11 * matrix4x4.m11 + matrix4x4.m12 * matrix4x4.m12);
			float z = Mathf.Sqrt(
				matrix4x4.m20 * matrix4x4.m20 + matrix4x4.m21 * matrix4x4.m21 + matrix4x4.m22 * matrix4x4.m22);
			return new Vector3(x, y, z);
		}
	}
}