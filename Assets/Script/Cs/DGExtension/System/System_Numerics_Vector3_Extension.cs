using System.Numerics;

public static class System_Numerics_Vector3_Extension
{
	public static string DGToString(this Vector3 v)
	{
		return string.Format("x:{0},y:{1},z:{2}", v.X, v.Y, v.Z);
	}
}
