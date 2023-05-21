using UnityEngine;

public static class UnityEngine_Matrix4x4_Extension
{
	public static string ToString2(this Matrix4x4 v)
	{
		return "{" + v.m00 + ", " + v.m01 + ", " + v.m02 + ", " + v.m03 + "} \n" +
		       "{" + v.m10 + ", " + v.m11 + ", " + v.m12 + ", " + v.m13 + "} \n" +
		       "{" + v.m20 + ", " + v.m21 + ", " + v.m22 + ", " + v.m23 + "} \n" +
		       "{" + v.m30 + ", " + v.m31 + ", " + v.m32 + ", " + v.m33 + "} \n";
	}

	public static System.Numerics.Matrix4x4 ToSystemNumericsMatrix4x4(this Matrix4x4 matrix)
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
}