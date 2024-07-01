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

		public static bool IsDefault(Quaternion quaternion, bool isMin = false)
		{
			return isMin ? quaternion == QuaternionConst.DEFAULT_MIN : quaternion == QuaternionConst.DEFAULT_MAX;
		}

		public static Quaternion Inverse(Quaternion quaternion)
		{
			return Quaternion.Inverse(quaternion);
		}

		public static Vector3 Forward(Quaternion quaternion)
		{
			return quaternion * Vector3.forward;
		}

		public static Vector3 Back(Quaternion quaternion)
		{
			return quaternion * Vector3.back;
		}

		public static Vector3 Up(Quaternion quaternion)
		{
			return quaternion * Vector3.up;
		}

		public static Vector3 Down(Quaternion quaternion)
		{
			return quaternion * Vector3.down;
		}

		public static Vector3 Left(Quaternion quaternion)
		{
			return quaternion * Vector3.left;
		}

		public static Vector3 Right(Quaternion quaternion)
		{
			return quaternion * Vector3.right;
		}

		public static bool IsZero(Quaternion quaternion)
		{
			return quaternion.x == 0 && quaternion.y == 0 && quaternion.z == 0;
		}

		public static Quaternion GetNotZero(Quaternion quaternion, Quaternion? defaultValue = null)
		{
			defaultValue = defaultValue ?? Quaternion.identity;
			return quaternion.IsZero() ? defaultValue.Value : quaternion;
		}
	}
}