using UnityEngine;

namespace DG
{
	public static class UnityEngine_Rect_Extension
	{
		/// <summary>
		/// 缩放Rect
		/// </summary>
		/// <param name="self"></param>
		/// <param name="scaleFactor"></param>
		/// <param name="pivotPointOffset">中心点偏移，默认(0,0)是在中心</param>
		/// <returns></returns>
		public static Rect ScaleBy(this Rect self, float scaleFactor, Vector2 pivotPointOffset = default)
		{
			return RectUtil.ScaleBy(self, scaleFactor, pivotPointOffset);
		}


		/// <summary>
		/// 获取两个矩形的交集
		/// </summary>
		public static Rect GetIntersection(this Rect self, Rect other)
		{
			return RectUtil.GetIntersection(self, other);
		}

		#region Padding

		public static Rect Padding(this Rect self, float left, float right, float top, float bottom)
		{
			return RectUtil.Padding(self, left, right, top, bottom);
		}

		public static Rect Padding(this Rect self, Vector2 horizontal, Vector2 vertical)
		{
			return RectUtil.Padding(self, horizontal, vertical);
		}

		public static Rect Padding(this Rect self, float horizontal, float vertical)
		{
			return RectUtil.Padding(self, horizontal, vertical);
		}

		public static Rect Padding(this Rect self, Vector2 padding)
		{
			return RectUtil.Padding(self, padding);
		}

		public static Rect Padding(this Rect self, float padding)
		{
			return RectUtil.Padding(self, padding);
		}

		#endregion
	}
}

