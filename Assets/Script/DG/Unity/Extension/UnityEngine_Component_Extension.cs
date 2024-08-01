using System;
using UnityEngine;
using UnityEngine.UI;

namespace DG
{
	public static class UnityEngine_Component_Extension
	{
		public static RectTransform RectTransform(this Component self)
		{
			return ComponentUtil.RectTransform(self);
		}

		public static GameObject NewChildGameObject(this Component self, string path = null)
		{
			return ComponentUtil.NewChildGameObject(self, path);
		}

		public static Component NewChildWithComponent(this Component self, Type componentType, string path = null)
		{
			return ComponentUtil.NewChildWithComponent(self, componentType, path);
		}

		public static T NewChildWithComponent<T>(this Component self, string path = null) where T : Component
		{
			return ComponentUtil.NewChildWithComponent<T>(self, path);
		}

		public static RectTransform NewChildWithRectTransform(this Component self, string path = null)
		{
			return ComponentUtil.NewChildWithRectTransform(self, path);
		}

		public static Image NewChildWithImage(this Component self, string path = null)
		{
			return ComponentUtil.NewChildWithImage(self, path);
		}

		public static Text NewChildWithText(this Component self, string path = null, string content = null,
			int fontSize = 20, Color? color = null, TextAnchor? alignment = null, Font font = null)
		{
			return ComponentUtil.NewChildWithText(self, path, content, fontSize, color, alignment, font);
		}

		public static GameObject GetOrNewGameObject(this Component self, string path)
		{
			return GameObjectUtil.GetOrNewGameObject(path, self.gameObject);
		}
	}
}