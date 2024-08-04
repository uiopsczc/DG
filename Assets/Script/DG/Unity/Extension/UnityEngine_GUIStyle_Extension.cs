using System;
using UnityEngine;
using GUIStyle = UnityEngine.GUIStyle;

namespace DG
{
	public static class UnityEngine_GUIStyle_Extension
	{
		public static GUIStyle Append(this GUIStyle self, Action<GUIStyle> appendCallback)
		{
			return GUIStyleUtil.Append(self, appendCallback);
		}

		public static GUIStyle Clone(this GUIStyle self)
		{
			return GUIStyleUtil.Clone(self);
		}

		public static GUIStyle SetFontSize(this GUIStyle self, int fontSize)
		{
			return GUIStyleUtil.SetFontSize(self, fontSize);
		}

		public static GUIStyle SetFontStyle(this GUIStyle self, FontStyle fontStyle)
		{
			return GUIStyleUtil.SetFontStyle(self, fontStyle);
		}

		public static GUIStyle SetRichText(this GUIStyle self, bool isRichText)
		{
			return GUIStyleUtil.SetRichText(self, isRichText);
		}

		public static GUIStyle SetTextAnchor(this GUIStyle self, TextAnchor textAnchor)
		{
			return GUIStyleUtil.SetTextAnchor(self, textAnchor);
		}

		public static GUIStyle SetFixedHeight(this GUIStyle self, float fixedHeight)
		{
			return GUIStyleUtil.SetFixedHeight(self, fixedHeight);
		}

		public static GUIStyle SetFixedWidth(this GUIStyle self, float fixedWidth)
		{
			return GUIStyleUtil.SetFixedWidth(self, fixedWidth);
		}

		public static GUIStyle SetName(this GUIStyle self, string name)
		{
			return GUIStyleUtil.SetName(self, name);
		}

		public static GUIStyle SetName(this GUIStyle self, GUIStyle anotherStyle)
		{
			return GUIStyleUtil.SetName(self, anotherStyle);
		}

		public static GUIStyle SetPadding(this GUIStyle self, RectOffset padding)
		{
			return GUIStyleUtil.SetPadding(self, padding);
		}

		public static GUIStyle SetBorder(this GUIStyle self, RectOffset border)
		{
			return GUIStyleUtil.SetBorder(self, border);
		}

		public static GUIStyle SetClipping(this GUIStyle self, TextClipping clipping)
		{
			return GUIStyleUtil.SetClipping(self, clipping);
		}

		public static GUIStyle SetContentOffset(this GUIStyle self, Vector2 contentOffset)
		{
			return GUIStyleUtil.SetContentOffset(self, contentOffset);
		}

		public static GUIStyle SetImagePosition(this GUIStyle self, ImagePosition imagePosition)
		{
			return GUIStyleUtil.SetImagePosition(self, imagePosition);
		}

		public static GUIStyle SetMargin(this GUIStyle self, RectOffset margin)
		{
			return GUIStyleUtil.SetMargin(self, margin);
		}

		public static GUIStyle SetStretchHeight(this GUIStyle self, bool stretchHeight)
		{
			return GUIStyleUtil.SetStretchHeight(self, stretchHeight);
		}

		public static GUIStyle SetStretchWidth(this GUIStyle self, bool stretchWidth)
		{
			return GUIStyleUtil.SetStretchWidth(self, stretchWidth);
		}


		public static GUIStyle SetWordWrap(this GUIStyle self, bool wordWrap)
		{
			return GUIStyleUtil.SetWordWrap(self, wordWrap);
		}

		public static GUIStyle SetOverflow(this GUIStyle self, RectOffset overflow)
		{
			return GUIStyleUtil.SetOverflow(self, overflow);
		}

	}
}


