using System.IO;
using UnityEditor;
using UnityEngine;

namespace DG
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu : MonoBehaviour
	{
		[MenuItem(DGToolConst.Menu_Root + "退出所有的线程")]
		public static void AbortAllThreads()
		{
			ThreadManager.instance.Abort();
			DGLog.Info("退出所有线程完成");
		}
	}
}