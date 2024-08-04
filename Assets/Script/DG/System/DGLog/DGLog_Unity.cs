/*************************************************************************************
 * 描    述:
 * 创 建 者:  czq
 * 创建时间:  2023/8/15
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
 *************************************************************************************/

using System;
using System.IO;
using System.Reflection;

namespace DG
{
    public static partial class DGLog
    {
        public static void ClearLogs()
        {
            Assembly assembly = AssemblyUtil.GetAssembly("UnityEditor");
            Type type = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo methodInfo = type.GetMethodInfo2("Clear");
            methodInfo.Invoke(new object(), null);
        }
    }
}