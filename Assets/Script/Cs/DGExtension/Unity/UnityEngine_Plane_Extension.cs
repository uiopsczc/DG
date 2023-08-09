using UnityEngine;

public static class UnityEngine_Plane_Extension
{
	public static string ToString2(this Plane v)
	{
		return "{normal:" + v.normal.ToString2() + ", distance:" + v.distance + "}";
	}

	public static System.Numerics.Plane To_System_Numerics_Plane(this Plane v)
	{
		return new System.Numerics.Plane(v.normal.To_System_Numerics_Vector3(), v.distance);
	}
}