using System;
using System.Collections.Generic;
using System.IO;

namespace DG
{
	public static class DirectoryInfoUtil
	{
		// <summary>
		/// 获取DirectoryInfo目录下的fileName的路径
		/// </summary>
		public static string SubPath(DirectoryInfo directoryInfo, string fileName)
		{
			return directoryInfo.FullName + Path.DirectorySeparatorChar + fileName;
		}

		/// <summary>
		/// 移除文件夹dir
		/// </summary>
		/// <param name="directoryInfo"></param>
		/// <returns></returns>
		public static void ClearDir(DirectoryInfo directoryInfo)
		{
			if (!directoryInfo.IsDirectory()) return;
			FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
			for (var i = 0; i < fileSystemInfos.Length; i++)
			{
				FileSystemInfo fileSystemInfo = fileSystemInfos[i];
				fileSystemInfo.RemoveFiles();
			}
		}

		/// <summary>
		/// 搜索文件夹dir下符合过滤条件filter中的文件，将文件添加到results中
		/// </summary>
		/// <param name="directoryInfo"></param>
		/// <param name="filter"></param>
		/// <param name="results"></param>
		/// <returns></returns>
		public static List<FileSystemInfo> SearchFiles(DirectoryInfo directoryInfo, Func<FileSystemInfo, bool> filter)
		{
			FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
			List<FileSystemInfo> result = new List<FileSystemInfo>();
			for (var i = 0; i < fileSystemInfos.Length; i++)
			{
				FileSystemInfo fileSystemInfo = fileSystemInfos[i];
				if (filter(fileSystemInfo))
					result.Add(fileSystemInfo);
				if (!fileSystemInfo.IsDirectory()) continue;
				var subResultList = SearchFiles((DirectoryInfo)fileSystemInfo, filter);
				if (subResultList.Count > 0)
					result.AddRange(subResultList);
			}

			return result;
		}
	}
}