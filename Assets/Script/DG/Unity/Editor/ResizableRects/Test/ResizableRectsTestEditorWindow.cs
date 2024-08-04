using UnityEditor;
using UnityEngine;

namespace DG
{
	public class ResizableRectsTestEditorWindow : EditorWindow
	{
		private HorizontalResizableRects _resizableRects;


		void Awake()
		{
			_resizableRects =
				new HorizontalResizableRects(() => new Rect(0, 0, position.width, position.height), null,
					new[] {0.3f, 0.6f});
		}


		void OnGUI()
		{
			_resizableRects.OnGUI();
			for (int i = 0; i < _resizableRects.rects.Length; i++)
			{
				if (i == 0)
					EditorGUI.DrawRect(_resizableRects.rects[i], Color.red);
				if (i == 1)
					EditorGUI.DrawRect(_resizableRects.rects[i], Color.green);
				if (i == 2)
					EditorGUI.DrawRect(_resizableRects.rects[i], Color.blue);
			}

			Repaint();
		}
	}
}