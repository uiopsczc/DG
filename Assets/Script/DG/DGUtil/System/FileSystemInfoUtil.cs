using System;
using System.IO;

namespace DG
{
	public static class FileSystemInfoUtil
	{
		/// <summary>
		///   �Ƿ���Ŀ¼
		/// </summary>
		public static bool IsDirectory( FileSystemInfo fileSystemInfo)
		{
			return (fileSystemInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
		}

		/// <summary>
		///   �Ƿ����ļ�
		/// </summary>
		public static bool IsFile( FileSystemInfo fileSystemInfo)
		{
			return !fileSystemInfo.IsDirectory();
		}

		/// <summary>
		///   ��Ŀ¼
		/// </summary>
		public static DirectoryInfo Parent( FileSystemInfo fileSystemInfo)
		{
			return fileSystemInfo.IsFile() ? ((FileInfo)fileSystemInfo).Directory : ((DirectoryInfo)fileSystemInfo).Parent;
		}

		/// <summary>
		///   ��src�����ݸ��Ƶ�dst�У�src�������ļ��У�
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
				if (str2.StartsWith(str1)) throw new IOException("�ص��ݹ鸴��" + str1 + "->" + str2);
				var dir2 = new DirectoryInfo(dst.FullName + Path.DirectorySeparatorChar + fileSystemInfo.Name);
				dir2.Create();
				if (!dir2.IsDirectory())
					throw new IOException("�޷�����Ŀ¼" + dir2);

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
						var pdir2 = ((FileInfo)dst).Directory; // Ŀ���ļ�dst�ĸ���Ŀ¼
						pdir2?.Create();
						if (pdir2 == null || !pdir2.Exists)
							throw new IOException("�޷�����Ŀ¼:" + pdir2);
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
		///   �Ƴ��ļ�file��file�������ļ��У�
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

