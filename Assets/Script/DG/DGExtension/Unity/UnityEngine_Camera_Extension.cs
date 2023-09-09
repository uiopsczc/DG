using UnityEngine;

namespace DG
{
	public static class UnityEngine_Camera_Extension
	{
		public static bool IsPosInViewPort(this Camera self, Vector3 worldPosition)
		{
			return CameraUtil.IsPosInViewPort(self, worldPosition);
		}

		public static Vector2 GetRectSizeByDistance(this Camera self, float distance)
		{
			return CameraUtil.GetRectSizeByDistance(self, distance);
		}
	}
}


