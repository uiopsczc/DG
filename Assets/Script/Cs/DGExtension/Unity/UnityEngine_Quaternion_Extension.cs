using UnityEngine;

public static class UnityEngine_Quaternion_Extension
{
	public static string ToString2(this Quaternion v)
	{
		return string.Format("x:{0},y:{1},z:{2},w:{3}", v.x, v.y, v.z, v.w);
	}
}
