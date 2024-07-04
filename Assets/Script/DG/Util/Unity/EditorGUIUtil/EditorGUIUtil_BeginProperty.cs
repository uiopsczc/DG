using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace DG
{
	public partial class EditorGUIUtil
	{
		public static EditorGUIBeginPropertyScope BeginProperty(Rect totalPosition, GUIContent label,
			SerializedProperty property)
		{
			return new EditorGUIBeginPropertyScope(totalPosition, label, property);
		}
	}
}
#endif