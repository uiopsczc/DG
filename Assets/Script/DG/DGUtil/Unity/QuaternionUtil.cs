using UnityEngine;

namespace DG
{
	public class QuaternionUtil
	{
		public static string DGToString(Quaternion v)
		{
			return string.Format("x:{0},y:{1},z:{2},w:{3}", v.x, v.y, v.z, v.w);
		}

		public static System.Numerics.Quaternion To_System_Numerics_Quaternion(Quaternion v)
		{
			return new System.Numerics.Quaternion(v.x, v.y, v.z, v.w);
		}
	}
}