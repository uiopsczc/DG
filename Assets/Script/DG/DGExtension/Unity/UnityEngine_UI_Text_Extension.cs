using UnityEngine;
using UnityEngine.UI;

namespace DG
{
	public static class UnityEngine_UI_Text_Extension
	{
		public static void SetIsGray(this Text self, bool isGray)
		{
			TextUtil.SetIsGray(self, isGray);
		}

		public static void SetAlpha(this Text self, float alpha)
		{
			TextUtil.SetAlpha(self, alpha);
		}

		public static void SetColor(this Text self, Color color, bool isNotUseColorAlpha = false)
		{
			TextUtil.SetColor(self, color, isNotUseColorAlpha);
		}
	}
}