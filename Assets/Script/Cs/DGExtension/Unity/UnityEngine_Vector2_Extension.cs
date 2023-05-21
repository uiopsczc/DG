using UnityEngine;

public static class UnityEngine_Vector2_Extension
{
	public static string ToString2(this Vector2 v)
	{
		return string.Format("x:{0},y:{1}", v.x, v.y);
	}
}
