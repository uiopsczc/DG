using UnityEngine;

namespace DG
{
	public static class UnityEngine_Texture2D_Extension
	{
		public static Sprite CreateSprite(this Texture2D self, float? width = null, float? height = null)
		{
			return Texture2DUtil.CreateSprite(self, width, height);
		}
	}
}

