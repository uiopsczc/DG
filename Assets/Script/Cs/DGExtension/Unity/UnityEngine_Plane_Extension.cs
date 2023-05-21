using UnityEngine;

public static class UnityEngine_Plane_Extension
{
	public static string ToString2(this Plane v)
	{
		return "{normal:" + v.normal.ToString2() + ", distance:" + v.distance + "}";
	}
}