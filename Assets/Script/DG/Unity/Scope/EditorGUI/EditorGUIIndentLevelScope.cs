#if UNITY_EDITOR
using System;
using UnityEditor;

namespace DG
{
	public class EditorGUIIndentLevelScope : IDisposable
	{
		private readonly int _add;

		public EditorGUIIndentLevelScope(int add = 1)
		{
			_add = add;
			EditorGUI.indentLevel += add;
		}

		public void Dispose()
		{
			EditorGUI.indentLevel -= _add;
		}
	}
}
#endif