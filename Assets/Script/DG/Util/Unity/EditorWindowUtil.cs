#if UNITY_EDITOR
using System;
using UnityEditor;

namespace DG
{
	public class EditorWindowUtil
	{
		public static void ShowNotificationAndLog(EditorWindow self, params object[] args)
		{
			_ShowNotificationAndCallback(self, () => DGLog.Info(args), args);
		}

		public static void ShowNotificationAndWarn(EditorWindow self, params object[] args)
		{
			_ShowNotificationAndCallback(self, () => DGLog.Warn(args), args);
		}

		public static void ShowNotificationAndError(EditorWindow self, params object[] args)
		{
			_ShowNotificationAndCallback(self, () => DGLog.Error(args), args);
		}

		public static void ShowNotification2(EditorWindow self, params object[] args)
		{
			self.ShowNotification(DGLog.GetLogString(false, args).ToGUIContent());
		}

		private static void _ShowNotificationAndCallback(EditorWindow self, Action action, params object[] args)
		{
			self.ShowNotification2(args);
			action();
		}
	}
}
#endif