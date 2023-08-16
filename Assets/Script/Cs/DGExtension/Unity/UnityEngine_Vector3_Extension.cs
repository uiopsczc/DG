using UnityEngine;

public static class UnityEngine_Vector3_Extension
{
	public static string DGToString(this Vector3 v)
	{
		return string.Format("x:{0},y:{1},z:{2}", v.x, v.y, v.z);
	}

	public static System.Numerics.Vector3 To_System_Numerics_Vector3(this Vector3 v)
	{
		return new System.Numerics.Vector3(v.x, v.y, v.z);
	}
}
