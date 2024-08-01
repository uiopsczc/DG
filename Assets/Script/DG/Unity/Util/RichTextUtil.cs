using UnityEngine;

namespace DG
{
	public class RichTextUtil
	{
		public static string SetColor(string s, Color color)
		{
			return string.Format(StringConst.STRING_FORMAT_TEXT_COLOR, ColorUtility.ToHtmlStringRGB(color), s);
		}

		public static string SetIsBold(string s)
		{
			return string.Format(StringConst.STRING_FORMAT_TEXT_BOLD, s);
		}

		public static string SetIsItalic(string s)
		{
			return string.Format(StringConst.STRING_FORMAT_TEXT_ITALIC, s);
		}

		public static string SetFontSize(string s, int fontSize)
		{
			return string.Format(StringConst.STRING_FORMAT_TEXT_FONT_SIZE, fontSize, s);
		}
	}
}