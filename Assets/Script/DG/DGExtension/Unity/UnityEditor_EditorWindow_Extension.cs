#if UNITY_EDITOR
using UnityEditor;

namespace DG
{
	public static class UnityEditor_EditorWindow_Extension
	{
		public static void ShowNotificationAndLog(this EditorWindow self, params object[] args)
		{
			EditorWindowUtil.ShowNotificationAndLog(self, args);
		}

		public static void ShowNotificationAndWarn(this EditorWindow self, params object[] args)
		{
			EditorWindowUtil.ShowNotificationAndWarn(self, args);
		}

		public static void ShowNotificationAndError(this EditorWindow self, params object[] args)
		{
			EditorWindowUtil.ShowNotificationAndError(self, args);
		}

		public static void ShowNotification2(this EditorWindow self, params object[] args)
		{
			EditorWindowUtil.ShowNotification2(self, args);
		}
	}
}
#endif

