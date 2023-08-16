using UnityEngine;

public static class UnityEngine_Plane_Extension
{
	public static string DGToString(this Plane v)
	{
		return "{normal:" + v.normal.DGToString() + ", distance:" + v.distance + "}";
	}

	public static System.Numerics.Plane To_System_Numerics_Plane(this Plane v)
	{
		return new System.Numerics.Plane(v.normal.To_System_Numerics_Vector3(), v.distance);
	}
}