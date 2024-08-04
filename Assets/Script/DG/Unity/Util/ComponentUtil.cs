using System;
using UnityEngine;
using UnityEngine.UI;

namespace DG
{
	public static class ComponentUtil
	{
		public static RectTransform RectTransform(Component component)
		{
			return component.GetComponent<RectTransform>();
		}

		public static GameObject NewChildGameObject(Component component, string path = null)
		{
			if (component == null || component.transform == null)
				return null;
			GameObject gameObject = new GameObject();
			if (!path.IsNullOrWhiteSpace())
			{
				int index = path.IndexOf(CharConst.CHAR_SLASH);
				if (index > 0)
				{
					var name = path.Substring(0, index);
					gameObject.name = name;
					NewChildGameObject(gameObject.transform, path.Substring(index + 1));
				}
				else
					gameObject.name = path;
			}

			gameObject.transform.SetParent(component.transform, false);
			return gameObject;
		}

		public static Component NewChildWithComponent(Component component, Type componentType, string path = null)
		{
			if (component == null)
				return null;
			GameObject gameObject = NewChildGameObject(component, path);
			return gameObject.AddComponent(componentType);
		}

		public static T NewChildWithComponent<T>(Component component, string path = null) where T : Component
		{
			return NewChildWithComponent(component, typeof(T), path) as T;
		}

		public static RectTransform NewChildWithRectTransform(Component component, string path = null)
		{
			return NewChildWithComponent<RectTransform>(component, path);
		}

		public static Image NewChildWithImage(Component component, string path = null)
		{
			return NewChildWithComponent<Image>(component, path);
		}

		public static Text NewChildWithText(Component component, string path = null, string content = null,
			int fontSize = 20, Color? color = null, TextAnchor? alignment = null, Font font = null)
		{
			Text text = NewChildWithComponent<Text>(component, path);
			if (text != null)
			{
				text.alignment = alignment.GetValueOrDefault(default);
				text.color = color.GetValueOrDefault(Color.black);
				text.font = font ?? (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
				text.fontSize = fontSize;
				text.horizontalOverflow = HorizontalWrapMode.Overflow;
				text.verticalOverflow = VerticalWrapMode.Overflow;

				if (!content.IsNullOrWhiteSpace())
					text.text = content;
			}

			return text;
		}

		public static GameObject GetOrNewGameObject(Component component, string path)
		{
			return GameObjectUtil.GetOrNewGameObject(path, component.gameObject);
		}
	}
}