using System;
using System.IO;

namespace DG
{
	public static class FileSystemInfoUtil
	{
		/// <summary>
		///   是否是目录
		/// </summary>
		public static bool IsDirectory( FileSystemInfo fileSystemInfo)
		{
			return (fileSystemInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
		}

		/// <summary>
		///   是否是文件
		/// </summary>
		public static bool IsFile( FileSystemInfo fileSystemInfo)
		{
			return !fileSystemInfo.IsDirectory();
		}

		/// <summary>
		///   父目录
		/// </summary>
		public static DirectoryInfo Parent( FileSystemInfo fileSystemInfo)
		{
			return fileSystemInfo.IsFile() ? ((FileInfo)fileSystemInfo).Directory : ((DirectoryInfo)fileSystemInfo).Parent;
		}

		/// <summary>
		///   将src的内容复制到dst中（src可以是文件夹）
		/// </summary>
		/// <param name="fileSystemInfo"></param>
		/// <param name="dst"></param>
		/// <returns></returns>
		public static void CopyFileTo( FileSystemInfo fileSystemInfo, FileSystemInfo dst)
		{
			if (fileSystemInfo.IsDirectory())
			{
				var str1 = fileSystemInfo.FullName.ToLower();
				var str2 = dst.FullName.ToLower();
				if (str2.StartsWith(str1)) throw new IOException("重叠递归复制" + str1 + "->" + str2);
				var dir2 = new DirectoryInfo(dst.FullName + Path.DirectorySeparatorChar + fileSystemInfo.Name);
				dir2.Create();
				if (!dir2.IsDirectory())
					throw new IOException("无法创建目录" + dir2);

				var srcs = ((DirectoryInfo)fileSystemInfo).GetFileSystemInfos();
				foreach (var t in srcs)
					CopyFileTo(t, dir2);
			}
			else
			{
				var fis = new FileStream(fileSystemInfo.FullName, FileMode.Open, FileAccess.Read);
				try
				{
					FileInfo dstInfo;
					if (dst.IsDirectory())
					{
						dstInfo = new FileInfo(((DirectoryInfo)dst).SubPath(fileSystemInfo.Name));
						dstInfo.Create().Close();
					}
					else
					{
						var pdir2 = ((FileInfo)dst).Directory; // 目标文件dst的父级目录
						pdir2?.Create();
						if (pdir2 == null || !pdir2.Exists)
							throw new IOException("无法创建目录:" + pdir2);
						dstInfo = (FileInfo)dst;
						dstInfo.Create().Close();
					}

					if (!fileSystemInfo.Equals(dstInfo))
					{
						var fos = new FileStream(dst.FullName, FileMode.Truncate, FileAccess.Write);
						try
						{
							fis.CopyToStream(fos);
						}
						finally
						{
							fos.Close();
						}
					}
				}
				finally
				{
					fis.Close();
				}
			}
		}


		/// <summary>
		///   移除文件file（file可以是文件夹）
		/// </summary>
		/// <param name="fileSystemInfo"></param>
		/// <returns></returns>
		public static void RemoveFiles(FileSystemInfo fileSystemInfo)
		{
			if (fileSystemInfo.IsDirectory())
				((DirectoryInfo)fileSystemInfo).ClearDir();
			fileSystemInfo.Delete();
		}

		public static string FullName(FileSystemInfo fileSystemInfo, char separator = CharConst.Char_Slash)
		{
			return fileSystemInfo.FullName.ReplaceDirectorySeparatorChar(separator);
		}
	}
}

