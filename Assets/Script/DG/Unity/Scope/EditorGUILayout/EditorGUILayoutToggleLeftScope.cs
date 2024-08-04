#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace DG
{
	public class EditorGUILayoutToggleLeftScope
	{
		public static bool ToggleLeft(string label, ref bool value)
		{
			value = EditorGUILayout.ToggleLeft(label, value, EditorStyles.label);
			return value;
		}

		public static bool ToggleLeft(string label, ref bool value, GUIStyle labelStyle)
		{
			value = EditorGUILayout.ToggleLeft(label, value, labelStyle);
			return value;
		}

		public static bool ToggleLeft(GUIContent label, ref bool value, GUIStyle labelStyle)
		{
			value = EditorGUILayout.ToggleLeft(label, value, labelStyle);
			return value;
		}

		public static bool ToggleLeft(GUIContent label, ref bool value)
		{
			value = EditorGUILayout.ToggleLeft(label, value, EditorStyles.label);
			return value;
		}
	}
}
#endif