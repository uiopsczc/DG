using UnityEngine;

namespace DG
{
	public static class UnityEngine_Quaternion_Extension
	{
		public static string DGToString(this Quaternion v)
		{
			return QuaternionUtil.DGToString(v);
		}

		public static System.Numerics.Quaternion To_System_Numerics_Quaternion(this Quaternion v)
		{
			return QuaternionUtil.To_System_Numerics_Quaternion(v);
		}
	}

}
