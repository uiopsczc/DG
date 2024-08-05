using System;
using System.Diagnostics;
using System.Reflection;
using UnityEditor;

namespace DG
{
    /// <summary>
    ///   CZM工具菜单
    /// </summary>
    public partial class DGToolMenu
    {
        [MenuItem(DGToolConst.Menu_Root + "Log/Open Folder")]
        public static void LogOpenFolder()
        {
            Process.Start("explorer.exe", DGLogConst.LOG_BASE_PATH.Replace("/", "\\") + "");
        }

        [MenuItem(DGToolConst.Menu_Root + "Log/Clear Log")]
        public static void LogClear()
        {
            Assembly assembly = AssemblyUtil.GetAssembly("UnityEditor");
            Type type = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo methodInfo = type.GetMethodInfo2("Clear");
            methodInfo.Invoke(new object(), null);
            StdioUtil.ClearDir(DGLogConst.LOG_BASE_PATH);
            DGLog.Info(string.Format("Clear Finished Dir:{0}", DGLogConst.LOG_BASE_PATH));
        }
    }
}