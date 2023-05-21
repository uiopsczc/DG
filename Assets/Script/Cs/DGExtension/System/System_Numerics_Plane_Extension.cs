using System.Numerics;

public static class System_Numerics_Plane_Extension
{
	public static string ToString2(this Plane v)
	{
		return "{normal:" + v.Normal.ToString2() + ", distance:" + v.D + "}";
	}
}
