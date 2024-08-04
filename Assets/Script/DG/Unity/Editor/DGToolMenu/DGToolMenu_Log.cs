using System.Diagnostics;
using System.IO;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace DG
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu
	{
		[MenuItem(DGToolConst.Menu_Root + "Log/Open Folder")]
		public static void LogOpenFolder()
		{
			Process.Start("explorer.exe", DGLogConst.LogBasePath.Replace("/", "\\") + "");
		}

		[MenuItem(DGToolConst.Menu_Root + "Log/Clear Log")]
		public static void LogClear()
		{
			DGLog.ClearLogs();
			StdioUtil.ClearDir(DGLogConst.LogBasePath);
			DGLog.Info(string.Format("Clear Finished Dir:{0}", DGLogConst.LogBasePath));
		}
	}
}