/*************************************************************************************
 * ��    ��:  
 * �� �� ��:  czq
 * ����ʱ��:  2023/5/12
 * ======================================
 * ��ʷ���¼�¼
 * �汾:V          �޸�ʱ��:         �޸���:
 * �޸�����:
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
}