using UnityEngine;

namespace DG
{
	public class RectUtil
	{
		/// <summary>
		/// ����Rect
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="scaleFactor"></param>
		/// <param name="pivotPointOffset">���ĵ�ƫ�ƣ�Ĭ��(0,0)��������</param>
		/// <returns></returns>
		public static Rect ScaleBy(Rect rect, float scaleFactor, Vector2 pivotPointOffset = default)
		{
			Rect result = rect;
			result.x -= pivotPointOffset.x;
			result.y -= pivotPointOffset.y;
			result.xMin *= scaleFactor;
			result.xMax *= scaleFactor;
			result.yMin *= scaleFactor;
			result.yMax *= scaleFactor;
			result.x += pivotPointOffset.x;
			result.y += pivotPointOffset.y;
			return result;
		}
		

		/// <summary>
		/// ��ȡ�������εĽ���
		/// </summary>
		public static Rect GetIntersection(Rect rect, Rect other)
		{
			Rect result = new Rect(other.position, other.size);
			if (rect.xMin > result.xMin) result.xMin = rect.xMin;
			if (rect.xMax < result.xMax) result.xMax = rect.xMax;
			if (rect.yMin > result.yMin) result.yMin = rect.yMin;
			if (rect.yMax < result.yMax) result.yMax = rect.yMax;
			return result;
		}

		#region Padding

		public static Rect Padding(Rect rect, float left, float right, float top, float bottom)
		{
			rect.position += new Vector2(left, top);
			rect.size -= new Vector2(left + right, top + bottom);
			return rect;
		}

		public static Rect Padding(Rect rect, Vector2 horizontal, Vector2 vertical)
		{
			return rect.Padding(horizontal.x, horizontal.y, vertical.x, vertical.y);
		}

		public static Rect Padding(Rect rect, float horizontal, float vertical)
		{
			return rect.Padding(horizontal, horizontal, vertical, vertical);
		}

		public static Rect Padding(Rect rect, Vector2 padding)
		{
			return rect.Padding(padding.x, padding.x, padding.y, padding.y);
		}

		public static Rect Padding(Rect rect, float padding)
		{
			return rect.Padding(padding, padding, padding, padding);
		}

		#endregion
	}
}