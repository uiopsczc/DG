using UnityEngine;

namespace DG
{
	public static class RendererUtil
	{
		public static Material Material(Renderer renderer)
		{
			return Application.isPlaying ? renderer.material : renderer.sharedMaterial;
		}
	}
}