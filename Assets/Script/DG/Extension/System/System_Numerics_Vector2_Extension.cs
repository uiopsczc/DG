using System.Numerics;

namespace DG
{
	public static class System_Numerics_Vector2_Extension
	{
		public static string DGToString(this Vector2 v)
		{
			return string.Format("x:{0},y:{1}", v.X, v.Y);
		}
	}
}

