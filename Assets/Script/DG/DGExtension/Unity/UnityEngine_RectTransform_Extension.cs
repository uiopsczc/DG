using UnityEngine;

namespace DG
{
	public static class UnityEngine_RectTransform_Extension
	{
		public static void SetAnchoredPositionX(this RectTransform self, float value)
		{
			RectTransformUtil.SetAnchoredPositionX(self, value);
		}

		public static void SetAnchoredPositionY(this RectTransform self, float value)
		{
			RectTransformUtil.SetAnchoredPositionY(self, value);
		}

		public static void SetAnchoredPositionZ(this RectTransform self, float value)
		{
			RectTransformUtil.SetAnchoredPositionZ(self, value);
		}

		public static void SetSizeDeltaX(this RectTransform self, float value)
		{
			RectTransformUtil.SetSizeDeltaX(self, value);
		}

		public static void SetSizeDeltaY(this RectTransform rectTransform, float value)
		{
			RectTransformUtil.SetSizeDeltaY(rectTransform, value);
		}


		public static void CopyTo(this RectTransform self, RectTransform target)
		{
			RectTransformUtil.CopyTo(self, target);
		}

		public static void CopyFrom(this RectTransform self, RectTransform target)
		{
			RectTransformUtil.CopyFrom(self, target);
		}

		#region


		/// <summar
		/// 设置 anchorMin.x
		/// </summary>
		public static void SetAnchorMinX(this RectTransform self, float x)
		{
			RectTransformUtil.SetAnchorMinX(self, x);
		}


		/// <summary>
		/// 设置 anchorMin.y
		/// </summary>
		public static void SetAnchorMinY(this RectTransform self, float y)
		{
			RectTransformUtil.SetAnchorMinY(self, y);
		}


		/// <summary>
		/// 设置 anchorMax.x
		/// </summary>
		public static void SetAnchorMaxX(this RectTransform self, float x)
		{
			RectTransformUtil.SetAnchorMaxX(self, x);
		}


		/// <summary>
		/// 设置 anchorMax.y
		/// </summary>
		public static void SetAnchorMaxY(this RectTransform self, float y)
		{
			RectTransformUtil.SetAnchorMaxY(self, y);
		}


		/// <summary>
		/// 设置 pivot.x
		/// </summary>
		public static void SetPivotX(this RectTransform self, float x)
		{
			RectTransformUtil.SetPivotX(self, x);
		}


		/// <summary>
		/// 设置 pivot.y
		/// </summary>
		public static void SetPivotY(this RectTransform self, float y)
		{
			RectTransformUtil.SetPivotX(self, y);
		}


		public static void SetLeft(this RectTransform self, float value)
		{
			RectTransformUtil.SetLeft(self, value);
		}

		public static void SetRight(this RectTransform self, float value)
		{
			RectTransformUtil.SetRight(self, value);
		}

		public static void SetLeftRight(this RectTransform self, float left, float right)
		{
			RectTransformUtil.SetLeftRight(self, left, right);
		}

		public static void SetBottom(this RectTransform self, float value)
		{
			RectTransformUtil.SetBottom(self, value);
		}

		public static void SetTop(this RectTransform self, float value)
		{
			RectTransformUtil.SetTop(self, value);
		}


		public static void SeBottomTop(this RectTransform self, float bottom, float top)
		{
			RectTransformUtil.SeBottomTop(self, bottom, top);
		}

		#endregion
	}
}

