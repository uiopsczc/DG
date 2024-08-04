#if UNITY_EDITOR
using System.Collections;
using UnityEditorInternal;

namespace DG
{
	public class ReorderableListUtil
	{
		public static void ToReorderableList(IList toReorderList, ref ReorderableList reorderableList)
		{
			if (reorderableList != null) return;
			reorderableList =
				new ReorderableList(toReorderList, toReorderList.GetType().GetElementType(), true, false, true,
					true);
			reorderableList.headerHeight = 0;
		}

		public static void DrawGUI(ReorderableList reorderableList, GUIToggleTween toggleTween, string title)
		{
			using (new GUILayoutToggleAreaScope(toggleTween, title))
			{
				reorderableList.DoLayoutList();
			}
		}

		public static void SetElementHeight(ReorderableList reorderableList, float elementHeight)
		{
			reorderableList.elementHeight =
				reorderableList.count == 0 ? EditorConst.SINGLE_LINE_HEIGHT : elementHeight;
		}
	}
}
#endif