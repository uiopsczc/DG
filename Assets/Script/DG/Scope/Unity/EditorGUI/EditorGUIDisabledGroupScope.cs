#if UNITY_EDITOR
using System;
using UnityEditor;

namespace DG
{
	public class EditorGUIDisabledGroupScope : IDisposable
	{
		public EditorGUIDisabledGroupScope(bool isDisable)
		{
			EditorGUI.BeginDisabledGroup(isDisable);
		}

		public void Dispose()
		{
			EditorGUI.EndDisabledGroup();
		}
	}
}
#endif