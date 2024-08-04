using UnityEngine;

namespace DG
{
	public class RectTransformUtil
	{
		public static void SetAnchoredPositionX(RectTransform rectTransform, float value)
		{
			rectTransform.anchoredPosition = new Vector2(value, rectTransform.anchoredPosition.y);
		}

		public static void SetAnchoredPositionY(RectTransform rectTransform, float value)
		{
			rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, value);
		}

		public static void SetAnchoredPositionZ(RectTransform rectTransform, float value)
		{
			rectTransform.anchoredPosition3D =
				new Vector3(rectTransform.anchoredPosition3D.x, rectTransform.anchoredPosition3D.y, value);
		}

		public static void SetSizeDeltaX(RectTransform rectTransform, float value)
		{
			rectTransform.sizeDelta = new Vector2(value, rectTransform.sizeDelta.y);
		}

		public static void SetSizeDeltaY(RectTransform rectTransform, float value)
		{
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, value);
		}


		public static void CopyTo(RectTransform source, RectTransform target)
		{
			target.anchoredPosition = source.anchoredPosition;
			target.anchoredPosition3D = source.anchoredPosition3D;
			target.anchorMax = source.anchorMax;
			target.anchorMin = source.anchorMin;
			target.offsetMax = source.offsetMax;
			target.offsetMin = source.offsetMin;
			target.pivot = source.pivot;
			target.sizeDelta = source.sizeDelta;
			target.localRotation = source.localRotation;
			target.localScale = source.localScale;
		}

		public static void CopyFrom(RectTransform source, RectTransform target)
		{
			CopyTo(target, source);
		}

		#region


		/// <summary>
		/// 设置 anchorMin.x
		/// </summary>
		public static void SetAnchorMinX(RectTransform rectTransform, float x)
		{
			var anchorMin = rectTransform.anchorMin;
			anchorMin.x = x;
			rectTransform.anchorMin = anchorMin;
		}


		/// <summary>
		/// 设置 anchorMin.y
		/// </summary>
		public static void SetAnchorMinY(RectTransform rectTransform, float y)
		{
			var anchorMin = rectTransform.anchorMin;
			anchorMin.y = y;
			rectTransform.anchorMin = anchorMin;
		}


		/// <summary>
		/// 设置 anchorMax.x
		/// </summary>
		public static void SetAnchorMaxX(RectTransform rectTransform, float x)
		{
			var anchorMax = rectTransform.anchorMax;
			anchorMax.x = x;
			rectTransform.anchorMax = anchorMax;
		}


		/// <summary>
		/// 设置 anchorMax.y
		/// </summary>
		public static void SetAnchorMaxY(RectTransform rectTransform, float y)
		{
			var anchorMax = rectTransform.anchorMax;
			anchorMax.y = y;
			rectTransform.anchorMax = anchorMax;
		}


		/// <summary>
		/// 设置 pivot.x
		/// </summary>
		public static void SetPivotX(RectTransform rectTransform, float x)
		{
			var pivot = rectTransform.pivot;
			pivot.x = x;
			rectTransform.pivot = pivot;
		}


		/// <summary>
		/// 设置 pivot.y
		/// </summary>
		public static void SetPivotY(RectTransform rectTransform, float y)
		{
			var pivot = rectTransform.pivot;
			pivot.y = y;
			rectTransform.pivot = pivot;
		}


		public static void SetLeft(RectTransform rectTransform, float value)
		{
			rectTransform.offsetMin = new Vector2(value, rectTransform.offsetMin.y);
		}

		public static void SetRight(RectTransform rectTransform, float value)
		{
			rectTransform.offsetMax = new Vector2(value, rectTransform.offsetMax.y);
		}

		public static void SetLeftRight(RectTransform rectTransform, float left, float right)
		{
			rectTransform.SetLeft(left);
			rectTransform.SetRight(right);
		}

		public static void SetBottom(RectTransform rectTransform, float value)
		{
			rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, value);
		}

		public static void SetTop(RectTransform rectTransform, float value)
		{
			rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, value);
		}


		public static void SeBottomTop(RectTransform rectTransform, float bottom, float top)
		{
			rectTransform.SetBottom(bottom);
			rectTransform.SetTop(top);
		}

		#endregion
	
	}
}