using System.Numerics;

public static class System_Numerics_Plane_Extension
{
	public static string DGToString(this Plane v)
	{
		return "{normal:" + v.Normal.DGToString() + ", distance:" + v.D + "}";
	}
}
