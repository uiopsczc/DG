using UnityEditor;
using UnityEngine;

namespace DG
{
	public partial class DGToolMenu
	{
		[MenuItem(DGToolConst.Menu_Root + "/EditorMode/设置为EditorMode")]
		public static void SetToEditorMode()
		{
			EditorModeConst.IsEditorMode = true;
		}

		[MenuItem(DGToolConst.Menu_Root + "/EditorMode/设置为EditorMode", true)]
		public static bool CanSetToEditorMode()
		{
			return !EditorModeConst.IsEditorMode;
		}

		[MenuItem(DGToolConst.Menu_Root + "/EditorMode/设置为SimulationMode")]
		public static void SetToSimulationMode()
		{
			EditorModeConst.IsEditorMode = false;
		}

		[MenuItem(DGToolConst.Menu_Root + "/EditorMode/设置为SimulationMode", true)]
		public static bool CanSetToSimulationMode()
		{
			return EditorModeConst.IsEditorMode;
		}
	}
}