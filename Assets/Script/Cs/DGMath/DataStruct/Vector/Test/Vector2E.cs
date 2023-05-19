/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/5/12
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/

using System;
using FP = DGFixedPoint;
using FPVector3 = DGVector3;
using FPVector4 = DGVector4;
#if UNITY_5_3_OR_NEWER
using UnityEngine;

#endif

public static class Vector2E
{
	public static string ToString2(this Vector2 v)
	{
		return string.Format("x:{0},y:{1}", v.x, v.y);
	}

	public static string ToString2(this Vector3 v)
	{
		return string.Format("x:{0},y:{1},z:{2}", v.x, v.y, v.z);
	}

	public static string ToString2(this Vector4 v)
	{
		return string.Format("x:{0},y:{1},z:{2},w:{3}", v.x, v.y, v.z, v.w);
	}

	public static string ToString2(this Quaternion v)
	{
		return string.Format("x:{0},y:{1},z:{2},w:{3}", v.x, v.y, v.z, v.w);
	}

	public static string ToString2(this Matrix4x4 v)
	{
		return "{" + v.m00 + ", " + v.m01 + ", " + v.m02 + ", " + v.m03 + "} \n" +
		       "{" + v.m10 + ", " + v.m11 + ", " + v.m12 + ", " + v.m13 + "} \n" +
		       "{" + v.m20 + ", " + v.m21 + ", " + v.m22 + ", " + v.m23 + "} \n" +
		       "{" + v.m30 + ", " + v.m31 + ", " + v.m32 + ", " + v.m33 + "} \n";
	}

	public static string ToString2(this System.Numerics.Matrix4x4 v)
	{
		return "{" + v.M11 + ", " + v.M12 + ", " + v.M13 + ", " + v.M14 + "} \n" +
		       "{" + v.M21 + ", " + v.M22 + ", " + v.M23 + ", " + v.M24 + "} \n" +
		       "{" + v.M31 + ", " + v.M32 + ", " + v.M33 + ", " + v.M34 + "} \n" +
		       "{" + v.M41 + ", " + v.M42 + ", " + v.M43 + ", " + v.M44 + "} \n";
	}
}