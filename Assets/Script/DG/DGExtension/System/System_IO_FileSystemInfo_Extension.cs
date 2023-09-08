using System;
using System.IO;

namespace DG
{
	public static class System_IO_FileSystemInfo_Extension
	{
		/// <summary>
		///   是否是目录
		/// </summary>
		public static bool IsDirectory(this FileSystemInfo self)
		{
			return FileSystemInfoUtil.IsDirectory(self);
		}

		/// <summary>
		///   是否是文件
		/// </summary>
		public static bool IsFile(this FileSystemInfo self)
		{
			return FileSystemInfoUtil.IsFile(self);
		}

		/// <summary>
		///   父目录
		/// </summary>
		public static DirectoryInfo Parent(this FileSystemInfo self)
		{
			return FileSystemInfoUtil.Parent(self);
		}

		/// <summary>
		///   将src的内容复制到dst中（src可以是文件夹）
		/// </summary>
		/// <param name="self"></param>
		/// <param name="dst"></param>
		/// <returns></returns>
		public static void CopyFileTo(this FileSystemInfo self, FileSystemInfo dst)
		{
			FileSystemInfoUtil.CopyFileTo(self, dst);
		}


		/// <summary>
		///   移除文件file（file可以是文件夹）
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static void RemoveFiles(this FileSystemInfo self)
		{
			FileSystemInfoUtil.RemoveFiles(self);
		}

		public static string FullName(this FileSystemInfo self, char separator = CharConst.Char_Slash)
		{
			return FileSystemInfoUtil.FullName(self, separator);
		}
	}
}