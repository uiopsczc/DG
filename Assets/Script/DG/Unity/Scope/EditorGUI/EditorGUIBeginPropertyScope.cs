#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace DG
{
	// Create a Property wrapper, useful for making regular GUI controls work with SerializedProperty.
	public class EditorGUIBeginPropertyScope : IDisposable
	{
		public EditorGUIBeginPropertyScope(Rect totalPosition, GUIContent label, SerializedProperty property)
		{
			EditorGUI.BeginProperty(totalPosition, label, property);
		}

		public void Dispose()
		{
			EditorGUI.EndProperty();
		}
	}
}
#endif