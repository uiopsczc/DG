using System.Numerics;

public static class System_Numerics_Matrix3x2_Extension
{
	public static string DGToString(this Matrix3x2 m)
	{
		return "{" + m.M11 + ", " + m.M12 + "} \n" +
		       "{" + m.M21 + ", " + m.M22 + "} \n" +
		       "{" + m.M31 + ", " + m.M32 + "}\n";
	}
}
