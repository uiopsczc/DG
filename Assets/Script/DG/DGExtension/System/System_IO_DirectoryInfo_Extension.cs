using System;
using System.Collections.Generic;
using System.IO;

namespace DG
{
	public static class System_IO_DirectoryInfo_Extension
	{
		// <summary>
		/// 获取DirectoryInfo目录下的fileName的路径
		/// </summary>
		public static string SubPath(this DirectoryInfo self, string fileName)
		{
			return DirectoryInfoUtil.SubPath(self, fileName);
		}

		/// <summary>
		/// 移除文件夹dir
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static void ClearDir(this DirectoryInfo self)
		{
			DirectoryInfoUtil.ClearDir(self);
		}

		/// <summary>
		/// 搜索文件夹dir下符合过滤条件filter中的文件，将文件添加到results中
		/// </summary>
		/// <param name="self"></param>
		/// <param name="filter"></param>
		/// <param name="results"></param>
		/// <returns></returns>
		public static List<FileSystemInfo> SearchFiles(this DirectoryInfo self, Func<FileSystemInfo, bool> filter)
		{
			return DirectoryInfoUtil.SearchFiles(self, filter);
		}
	}
}