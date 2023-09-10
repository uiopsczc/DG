using UnityEngine;

namespace DG
{
	public static class UnityEngine_Renderer_Extension
	{
		public static Material Material(this Renderer self)
		{
			return RendererUtil.Material(self);
		}
	}
}

