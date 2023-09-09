using UnityEngine;

namespace DG
{
	public static partial class UnityEngine_BoxCollider_Extension
	{
		/// <summary>
		/// 世界坐标（中点）
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector3 WorldCenter(this BoxCollider self)
		{
			return BoxColliderUtil.WorldCenter(self);
		}

		/// <summary>
		/// 世界坐标（前面 左上角）
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector3 WorldFrontTopLeft(this BoxCollider self)
		{
			return BoxColliderUtil.WorldFrontTopLeft(self);
		}

		/// <summary>
		/// 世界坐标（前面  右上角）
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector3 WorldFrontTopRight(this BoxCollider self)
		{
			return BoxColliderUtil.WorldFrontTopRight(self);
		}

		/// <summary>
		/// 世界坐标（前面  左下角）
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector3 WorldFrontBottomLeft(this BoxCollider self)
		{
			return BoxColliderUtil.WorldFrontBottomLeft(self);
		}

		/// <summary>
		/// 世界坐标（前面  左下角）
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector3 WorldFrontBottomRight(this BoxCollider self)
		{
			return BoxColliderUtil.WorldFrontBottomRight(self);
		}

		/// <summary>
		/// 世界坐标（后面 左上角）
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector3 WorldBackTopLeft(this BoxCollider self)
		{
			return BoxColliderUtil.WorldBackTopLeft(self);
		}

		/// <summary>
		/// 世界坐标（后面 右上角）
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector3 WorldBackTopRight(this BoxCollider self)
		{
			return BoxColliderUtil.WorldBackTopRight(self);
		}

		/// <summary>
		/// 世界坐标（后面 左下角）
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector3 WorldBackBottomLeft(this BoxCollider self)
		{
			return BoxColliderUtil.WorldBackBottomLeft(self);
		}

		/// <summary>
		/// 世界坐标（后面 右下角）
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector3 WorldBackBottomRight(this BoxCollider self)
		{
			return BoxColliderUtil.WorldBackBottomRight(self);
		}

		/// <summary>
		/// 所有的世界坐标
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Vector3[] WorldPoints(this BoxCollider self)
		{
			return BoxColliderUtil.WorldPoints(self);
		}

		public static Vector3 WorldSize(this BoxCollider self)
		{
			return BoxColliderUtil.WorldSize(self);
		}

	}
}


