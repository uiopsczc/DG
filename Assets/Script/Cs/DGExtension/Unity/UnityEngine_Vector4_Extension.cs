using UnityEngine;

public static class UnityEngine_Vector4_Extension
{
	public static string ToString2(this Vector4 v)
	{
		return string.Format("x:{0},y:{1},z:{2},w:{3}", v.x, v.y, v.z, v.w);
	}

	public static System.Numerics.Vector4 To_System_Numerics_Vector4(this Vector4 v)
	{
		return new System.Numerics.Vector4(v.x, v.y, v.z, v.w);
	}
}
