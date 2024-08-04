using System;
using UnityEngine;

namespace DG
{
	public class GUIStyleUtil
	{
		public static GUIStyle Append(GUIStyle guiStyle, Action<GUIStyle> appendCallback)
		{
			appendCallback(guiStyle);
			return guiStyle;
		}

		public static GUIStyle Clone(GUIStyle guiStyle)
		{
			return new GUIStyle(guiStyle);
		}

		public static GUIStyle SetFontSize(GUIStyle guiStyle, int fontSize)
		{
			guiStyle.fontSize = fontSize;
			return guiStyle;
		}

		public static GUIStyle SetFontStyle(GUIStyle guiStyle, FontStyle fontStyle)
		{
			guiStyle.fontStyle = fontStyle;
			return guiStyle;
		}

		public static GUIStyle SetRichText(GUIStyle guiStyle, bool isRichText)
		{
			guiStyle.richText = isRichText;
			return guiStyle;
		}

		public static GUIStyle SetTextAnchor(GUIStyle guiStyle, TextAnchor textAnchor)
		{
			guiStyle.alignment = textAnchor;
			return guiStyle;
		}

		public static GUIStyle SetFixedHeight(GUIStyle guiStyle, float fixedHeight)
		{
			guiStyle.fixedHeight = fixedHeight;
			return guiStyle;
		}

		public static GUIStyle SetFixedWidth(GUIStyle guiStyle, float fixedWidth)
		{
			guiStyle.fixedWidth = fixedWidth;
			return guiStyle;
		}

		public static GUIStyle SetName(GUIStyle guiStyle, string name)
		{
			guiStyle.name = name;
			return guiStyle;
		}

		public static GUIStyle SetName(GUIStyle guiStyle, GUIStyle anotherStyle)
		{
			return SetName(guiStyle, anotherStyle.name);
		}

		public static GUIStyle SetPadding(GUIStyle guiStyle, RectOffset padding)
		{
			guiStyle.padding = padding;
			return guiStyle;
		}

		public static GUIStyle SetBorder(GUIStyle guiStyle, RectOffset border)
		{
			guiStyle.border = border;
			return guiStyle;
		}

		public static GUIStyle SetClipping(GUIStyle guiStyle, TextClipping clipping)
		{
			guiStyle.clipping = clipping;
			return guiStyle;
		}

		public static GUIStyle SetContentOffset(GUIStyle guiStyle, Vector2 contentOffset)
		{
			guiStyle.contentOffset = contentOffset;
			return guiStyle;
		}

		public static GUIStyle SetImagePosition(GUIStyle guiStyle, ImagePosition imagePosition)
		{
			guiStyle.imagePosition = imagePosition;
			return guiStyle;
		}

		public static GUIStyle SetMargin(GUIStyle guiStyle, RectOffset margin)
		{
			guiStyle.margin = margin;
			return guiStyle;
		}

		public static GUIStyle SetStretchHeight(GUIStyle guiStyle, bool stretchHeight)
		{
			guiStyle.stretchHeight = stretchHeight;
			return guiStyle;
		}

		public static GUIStyle SetStretchWidth(GUIStyle guiStyle, bool stretchWidth)
		{
			guiStyle.stretchWidth = stretchWidth;
			return guiStyle;
		}


		public static GUIStyle SetWordWrap(GUIStyle guiStyle, bool wordWrap)
		{
			guiStyle.wordWrap = wordWrap;
			return guiStyle;
		}

		public static GUIStyle SetOverflow(GUIStyle guiStyle, RectOffset overflow)
		{
			guiStyle.overflow = overflow;
			return guiStyle;
		}
	}
}