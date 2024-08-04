#if UNITY_EDITOR
using System;
using UnityEditor;

namespace DG
{
	public class EditorWindowBeginWindowsScope : IDisposable
	{
		private readonly EditorWindow _self;

		public EditorWindowBeginWindowsScope(EditorWindow self)
		{
			_self = self;
			self.BeginWindows();
		}

		public void Dispose()
		{
			_self.EndWindows();
		}
	}
}
#endif