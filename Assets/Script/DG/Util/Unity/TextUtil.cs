using UnityEngine;
using UnityEngine.UI;

namespace DG
{
	public class TextUtil
	{
		private const string _TEXT_IS_GRAY_KEY = "Text_Is_Gray";
		private const string _TEXT_ORIGIN_COLOR_OF_GRAY_KEY = "Text_Origin_Color_Of_Gray";

		public static void SetIsGray(Text text, bool isGray)
		{
			if (text == null)
				return;
			var monoBehaviourCache = text.GetMonoBehaviourCache();
			bool textIsGray = monoBehaviourCache.GetOrGetByDefaultFunc(_TEXT_IS_GRAY_KEY, () => false);
			if (textIsGray == isGray) return;
			if (isGray)
			{
				monoBehaviourCache[_TEXT_ORIGIN_COLOR_OF_GRAY_KEY] = text.color;
				text.color = text.color.ToGray();
			}
			else
				text.color = monoBehaviourCache.Get<Color>(_TEXT_ORIGIN_COLOR_OF_GRAY_KEY);

			monoBehaviourCache[_TEXT_IS_GRAY_KEY] = isGray;
		}

		public static void SetAlpha(Text text, float alpha)
		{
			text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
		}

		public static void SetColor(Text text, Color color, bool isNotUseColorAlpha = false)
		{
			text.color = new Color(color.r, color.g, color.b, isNotUseColorAlpha ? text.color.a : color.a);
		}
	}
}