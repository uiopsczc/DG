using System;
using UnityEngine;

namespace DG
{
	public class GUIFontSizeScope : IDisposable
	{
		private readonly GUIStyle[] _guiStyles;
		private readonly int[] _sizes;


		public GUIFontSizeScope(float size, params GUIStyle[] guiStyles)
		{
			if (guiStyles == null || guiStyles.Length == 0)
				_guiStyles = new[] { GUI.skin.label, GUI.skin.button, GUI.skin.toggle };
			else
			{
				_guiStyles = new GUIStyle[guiStyles.Length];
				Array.Copy(guiStyles, _guiStyles, guiStyles.Length);
			}

			_sizes = new int[_guiStyles.Length];
			for (var i = 0; i < _guiStyles.Length; ++i)
			{
				_sizes[i] = _guiStyles[i].fontSize;
				_guiStyles[i].fontSize = (int)size;
			}
		}


		public void Dispose()
		{
			for (var i = 0; i < _guiStyles.Length; ++i) _guiStyles[i].fontSize = _sizes[i];
		}
	}
}