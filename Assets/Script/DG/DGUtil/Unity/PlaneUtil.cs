using UnityEngine;

namespace DG
{
	public class PlaneUtil
	{
		public static string DGToString(Plane v)
		{
			return "{normal:" + v.normal.DGToString() + ", distance:" + v.distance + "}";
		}

		public static System.Numerics.Plane To_System_Numerics_Plane(Plane v)
		{
			return new System.Numerics.Plane(v.normal.To_System_Numerics_Vector3(), v.distance);
		}
	}
}