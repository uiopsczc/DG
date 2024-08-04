using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using UnityEditor.PackageManager;

namespace DG
{
	public static class StdioUtil
	{
		public static readonly IFileSystemInfoFilter DIR_FILTER = new DirFilter();


//		/// <summary>
//		/// dirĿ¼�´���prefix+yyyy+suffix�ļ�
//		/// </summary>
//		/// <param name="dir"></param>
//		/// <param name="prefix"></param>
//		/// <param name="suffix"></param>
//		/// <param name="len"></param>
//		/// <returns></returns>
//		public static FileInfo CreateYearlyFile(DirectoryInfo dir, string prefix, string suffix,
//			RandomManager randomManager = null)
//		{
//			return CreateTimelyFile(dir, prefix, suffix, 4, randomManager);
//		}
//
//		/// <summary>
//		/// dirĿ¼�´���prefix+yyyyMM+suffix�ļ�
//		/// </summary>
//		/// <param name="dir"></param>
//		/// <param name="prefix"></param>
//		/// <param name="suffix"></param>
//		/// <param name="len"></param>
//		/// <returns></returns>
//		public static FileInfo CreateMonthlyFile(DirectoryInfo dir, string prefix, string suffix,
//			RandomManager randomManager = null)
//		{
//			return CreateTimelyFile(dir, prefix, suffix, 6, randomManager);
//		}
//
//		/// <summary>
//		/// dirĿ¼�´���prefix+yyyyMMdd+suffix�ļ�
//		/// </summary>
//		/// <param name="dir"></param>
//		/// <param name="prefix"></param>
//		/// <param name="suffix"></param>
//		/// <param name="len"></param>
//		/// <returns></returns>
//		public static FileInfo CreateDailyFile(DirectoryInfo dir, string prefix, string suffix,
//			RandomManager randomManager = null)
//		{
//			return CreateTimelyFile(dir, prefix, suffix, 8, randomManager);
//		}
//
//		/// <summary>
//		/// dirĿ¼�´���prefix+yyyyMMddHH+suffix�ļ�
//		/// </summary>
//		/// <param name="dir"></param>
//		/// <param name="prefix"></param>
//		/// <param name="suffix"></param>
//		/// <param name="len"></param>
//		/// <returns></returns>
//		public static FileInfo CreateHourlyFile(DirectoryInfo dir, string prefix, string suffix,
//			RandomManager randomManager = null)
//		{
//			return CreateTimelyFile(dir, prefix, suffix, 10, randomManager);
//		}
//
//		/// <summary>
//		/// dirĿ¼�´���prefix+yyyyMMddHHmmss+(�����3λ)+suffix�ļ�
//		/// </summary>
//		/// <param name="dir"></param>
//		/// <param name="prefix"></param>
//		/// <param name="suffix"></param>
//		/// <returns></returns>
//		public static FileInfo CreateTimeSliceFile(DirectoryInfo dir, string prefix, string suffix,
//			RandomManager randomManager = null)
//		{
//			randomManager = randomManager ?? Client.instance.randomManager;
//			int i = 0;
//			do
//			{
//				string dateTime = DateTimeUtil.NowDateTime().ToString(StringConst.STRING_yyyyMMddHHmmssSSS);
//				string rand = new StringBuilder(randomManager.RandomInt(0, 1000) + "").ToString();
//				string stem = dateTime
//							  + rand.FillHead(3, CharConst.CHAR_c);
//				string fileName = prefix + stem + suffix;
//
//				var file = new FileInfo(dir.SubPath(fileName));
//				if (file.Exists)
//					continue;
//				file.Create().Close();
//				return file;
//			} while (i++ < 10000);
//
//			throw new IOException(dir.FullName + "���޷�����Ψһ�ļ�");
//		}

		/// <summary>
		/// ����ļ����Ƶĺ�׺��
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string GetExtName(string name)
		{
			string s = name.Replace(CharConst.CHAR_BACK_SLASH, CharConst.CHAR_SLASH);
			int pos1 = s.LastIndexOf(CharConst.CHAR_SLASH);
			int pos = s.LastIndexOf(CharConst.CHAR_DOT);
			if (pos == -1 || pos < pos1)
				return StringConst.STRING_EMPTY;
			return name.Substring(pos);
		}

		/// <summary>
		/// �Ƴ��ļ���׺��
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string RemoveExtName(string name)
		{
			string s = name.Replace(CharConst.CHAR_BACK_SLASH, CharConst.CHAR_SLASH);
			int pos1 = s.LastIndexOf(CharConst.CHAR_SLASH);
			int pos = s.LastIndexOf(CharConst.CHAR_DOT);
			if (pos == -1 || pos < pos1)
				return name;
			return name.Substring(0, pos);
		}

		/// <summary>
		/// �����ļ���׺��
		/// </summary>
		/// <param name="name"></param>
		/// <param name="extName"></param>
		/// <returns></returns>
		public static string ChangeExtName(string name, string extName)
		{
			if (!extName.StartsWith(StringConst.STRING_DOT))
				extName = StringConst.STRING_DOT + extName;
			return RemoveExtName(name) + extName;
		}

		/// <summary>
		/// ��ȡins��buf��
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public static int ReadStream(Stream inStream, byte[] buffer)
		{
			return ReadStream(inStream, buffer, 0, buffer.Length);
		}

		/// <summary>
		/// ��ȡ�������е�����,ֱ����������
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static int ReadStream(Stream inStream, byte[] buffer, int offset, int length)
		{
			int k = 0;
			do
			{
				int j = inStream.Read(buffer, offset + k, length - k);
				if (j > 0)
				{
					k += j;
					if (k >= length)
						break;
					continue;
				}

				break;
			} while (true);

			return k;
		}

		/// <summary>
		/// ��Stream��ȡlen���ȵ�����
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static byte[] ReadStream(Stream inStream, int length)
		{
			var buffer = new byte[length];
			length = ReadStream(inStream, buffer);
			if (length < buffer.Length)
				return ByteUtil.SubBytes(buffer, 0, length);
			return buffer;
		}

		/// <summary>
		/// ��ȡins��ȫ������
		/// </summary>
		/// <param name="inStream"></param>
		/// <returns></returns>
		public static byte[] ReadStream(Stream inStream)
		{
			var outStream = new MemoryStream();
			CopyStream(inStream, outStream);
			return outStream.ToArray();
		}

		/// <summary>
		/// ��ȡins��ȫ�����ݵ�outs��
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="outStream"></param>
		/// <returns></returns>
		public static void CopyStream(Stream inStream, Stream outStream)
		{
			var data = new byte[4096];
			int len;
			do
			{
				len = ReadStream(inStream, data);
				if (len > 0)
					outStream.Write(data, 0, len);
			} while (len >= data.Length); //һ��������ǵ��ڣ������ʱ��������
		}

		/// <summary>
		/// ��dataд���ļ�fileName��
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static void WriteFile(string fileName, byte[] data)
		{
			WriteFile(new FileInfo(fileName), data);
		}

		/// <summary>
		///  ��dataд���ļ�file��
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static void WriteFile(FileInfo fileInfo, byte[] data)
		{
			WriteFile(fileInfo, data, false);
		}

		/// <summary>
		/// ��dataд���ļ�fileName��(append:�Ƿ�׷�ӵ��ļ�ĩβ)
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="data"></param>
		/// <param name="isAppend">�Ƿ�׷�ӵ��ļ�ĩβ</param>
		/// <returns></returns>
		public static void WriteFile(string fileName, byte[] data, bool isAppend)
		{
			WriteFile(new FileInfo(fileName), data, isAppend);
		}

		/// <summary>
		///  ��dataд���ļ�file��(append:�Ƿ�׷�ӵ��ļ�ĩβ)
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <param name="data"></param>
		/// <param name="isAppend">�Ƿ�׷�ӵ��ļ�ĩβ</param>
		/// <returns></returns>
		public static void WriteFile(FileInfo fileInfo, byte[] data, bool isAppend)
		{
			CreateFileIfNotExist(fileInfo.FullName);
			var fos = new FileStream(fileInfo.FullName, isAppend ? FileMode.Append : FileMode.Truncate,
				FileAccess.Write);
			try
			{
				fos.Write(data, 0, data.Length);
			}
			finally
			{
				fos.Close();
			}
		}

		/// <summary>
		/// ��ȡ�ļ�fileName������
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static byte[] ReadFile(string fileName)
		{
			return ReadFile(new FileInfo(fileName));
		}

		/// <summary>
		/// ��ȡ�ļ�file������
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <returns></returns>
		public static byte[] ReadFile(FileInfo fileInfo)
		{
			var inFileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
			try
			{
				var data = new byte[(int)fileInfo.Length];
				inFileStream.Read(data, 0, data.Length);
				return data;
			}
			finally
			{
				inFileStream.Close();
			}
		}

		/// <summary>
		/// ���ļ�fileNameд��content����(append:�Ƿ�׷�ӵ��ļ�ĩβ)
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="content"></param>
		/// <param name="isAppend">�Ƿ�׷�ӵ��ļ�ĩβ</param>
		/// <returns></returns>
		public static void WriteTextFile(string fileName, string content, bool isWriteLine = false,
			bool isAppend = false)
		{
			WriteTextFile(new FileInfo(fileName), content, isWriteLine, isAppend);
		}

		public static void WriteTextFile(string fileName, List<string> contentList, bool isAppend = false)
		{
			WriteTextFile(new FileInfo(fileName), contentList, isAppend);
		}

		/// <summary>
		/// ���ļ�fileд��content����(append:�Ƿ�׷�ӵ��ļ�ĩβ)
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <param name="content"></param>
		/// <param name="isAppend">�Ƿ�׷�ӵ��ļ�ĩβ</param>
		/// <returns></returns>
		public static void WriteTextFile(FileInfo fileInfo, string content, bool isWriteLine, bool isAppend)
		{
			CreateFileIfNotExist(fileInfo.FullName);
			var streamWriter = new StreamWriter(fileInfo.FullName, isAppend);
			try
			{
				if (!isWriteLine)
					streamWriter.Write(content);
				else
					streamWriter.WriteLine(content);
				streamWriter.Flush();
			}
			finally
			{
				streamWriter.Close();
			}
		}

		public static void WriteTextFile(FileInfo fileInfo, List<string> contentList, bool isAppend)
		{
			CreateFileIfNotExist(fileInfo.FullName);
			var streamWriter = new StreamWriter(fileInfo.FullName, isAppend);
			try
			{
				for (var i = 0; i < contentList.Count; i++)
				{
					var content = contentList[i];
					streamWriter.WriteLine(content);
				}

				streamWriter.Flush();
			}
			finally
			{
				streamWriter.Close();
			}
		}


		/// <summary>
		/// ��ȡ�ļ�fileName�������ַ�������
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static string ReadTextFile(string fileName)
		{
			return ReadTextFile(new FileInfo(fileName));
		}

		public static List<string> ReadAsLineList(string fileName)
		{
			FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
			StreamReader reader = new StreamReader(fileStream);
			List<string> lineList = new List<string>();
			string line = reader.ReadLine();
			while (line != null)
			{
				lineList.Add(line);
				line = reader.ReadLine();
			}
			reader.Close();
			fileStream.Close();
			return lineList;
		}

		/// <summary>
		/// ��ȡ�ļ�file�������ַ�������
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <returns></returns>
		public static string ReadTextFile(FileInfo fileInfo)
		{
			var streamReader = new StreamReader(fileInfo.FullName);
			var stringBuilder = new StringBuilder();
			var chars = new char[1024];
			try
			{
				int n;
				while ((n = streamReader.Read(chars, 0, chars.Length)) != 0)
					stringBuilder.Append(chars, 0, n);
				return stringBuilder.ToString();
			}
			finally
			{
				streamReader.Close();
			}
		}

		/// <summary>
		/// ��ȡurl������
		/// </summary>
		/// <param name="url"></param>
		/// <param name="retryCount">����ʧ�������������</param>
		/// <param name="errWaitDuration">����ʧ�ܺ�ȴ�ʱ��</param>
		/// <returns></returns>
		public static byte[] ReadUrl(string url, int retryCount, int errWaitDuration)
		{
			int i = 0;
			WebRequest request = WebRequest.Create(url);
			while (true)
			{
				try
				{
					Stream inStream = request.GetResponse().GetResponseStream();
					try
					{
						byte[] data = ReadStream(inStream);
						return data;
					}
					finally
					{
						inStream?.Close();
					}
				}
				catch
				{
					i++;
					if (i > retryCount)
						throw new IOException(string.Format("���¶�ȡ����{0}��", retryCount));
					try
					{
						if (errWaitDuration > 0L)
							Thread.Sleep(errWaitDuration);
					}
					catch
					{
						// ignored
					}
				}
			}
		}

		/// <summary>
		/// ��src�����ݸ��Ƶ�dst�У�src�������ļ��У�
		/// </summary>
		/// <param name="srcFileSystemInfo"></param>
		/// <param name="dstFileSystemInfo"></param>
		/// <returns></returns>
		public static void CopyFile(FileSystemInfo srcFileSystemInfo, FileSystemInfo dstFileSystemInfo)
		{
			if (srcFileSystemInfo.IsDirectory())
			{
				string srcFileFullName = srcFileSystemInfo.FullName.ToLower();
				string dstFileFullName = dstFileSystemInfo.FullName.ToLower();
				if (dstFileFullName.StartsWith(srcFileFullName))
					throw new IOException(string.Format("�ص��ݹ鸴��{0}->{1}", srcFileFullName, dstFileFullName));

				var dstDirectoryInfo = new DirectoryInfo(
					KeyUtil.GetCombinedKey(Path.DirectorySeparatorChar, dstFileSystemInfo.FullName,
						srcFileSystemInfo.Name));
				dstDirectoryInfo.Create();
				if (!dstDirectoryInfo.IsDirectory())
					throw new IOException(string.Format("�޷�����Ŀ¼{0}", dstDirectoryInfo));

				FileSystemInfo[] srcFileSystemInfos = ((DirectoryInfo)srcFileSystemInfo).GetFileSystemInfos();
				foreach (FileSystemInfo fileSystemInfo in srcFileSystemInfos)
					CopyFile(fileSystemInfo, dstDirectoryInfo);
			}
			else
			{
				var srcFileStream = new FileStream(srcFileSystemInfo.FullName, FileMode.Open, FileAccess.Read);
				try
				{
					FileInfo dstFileInfo;
					if (dstFileSystemInfo.IsDirectory())
					{
						dstFileInfo = new FileInfo(((DirectoryInfo)dstFileSystemInfo).SubPath(srcFileSystemInfo.Name));
						dstFileInfo.Create().Close();
					}
					else
					{
						DirectoryInfo dstParentDirectoryInfo = ((FileInfo)dstFileSystemInfo).Directory; // Ŀ���ļ�dst�ĸ���Ŀ¼
						dstParentDirectoryInfo?.Create();
						if (dstParentDirectoryInfo == null || !dstParentDirectoryInfo.Exists)
							throw new IOException(string.Format("�޷�����Ŀ¼:{0}", dstParentDirectoryInfo));
						dstFileInfo = (FileInfo)dstFileSystemInfo;
						dstFileInfo.Create().Close();
					}

					if (!(srcFileSystemInfo).Equals(dstFileInfo))
					{
						var dstFileStream = new FileStream(dstFileSystemInfo.FullName, FileMode.Truncate,
							FileAccess.Write);
						try
						{
							CopyStream(srcFileStream, dstFileStream);
						}
						finally
						{
							dstFileStream.Close();
						}
					}
				}
				finally
				{
					srcFileStream.Close();
				}
			}
		}

		/// <summary>
		/// �Ƴ��ļ�path��path�������ļ��У�
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static void RemoveFiles(string filePath)
		{
			var directoryInfo = new DirectoryInfo(filePath);
			var fileInfo = new FileInfo(filePath);
			if (directoryInfo.Exists)
				RemoveFile(directoryInfo);
			else
				RemoveFile(fileInfo);
		}

		/// <summary>
		/// �Ƴ��ļ�file��file�������ļ��У�
		/// </summary>
		/// <param name="fileSystemInfo"></param>
		/// <returns></returns>
		public static void RemoveFile(FileSystemInfo fileSystemInfo)
		{
			if (!fileSystemInfo.Exists)
				return;
			if (fileSystemInfo.IsDirectory())
				ClearDir((DirectoryInfo)fileSystemInfo);
			fileSystemInfo.Delete();
		}

		/// <summary>
		/// �Ƴ��ļ���dir
		/// </summary>
		/// <param name="dirPath"></param>
		/// <returns></returns>
		public static void ClearDir(string dirPath)
		{
			ClearDir(new DirectoryInfo(dirPath));
		}

		/// <summary>
		/// �Ƴ��ļ���dir
		/// </summary>
		/// <param name="dir"></param>
		/// <returns></returns>
		public static void ClearDir(DirectoryInfo dir)
		{
			if (dir.Exists && dir.IsDirectory())
			{
				FileSystemInfo[] fileSystemInfos = dir.GetFileSystemInfos();
				for (var i = 0; i < fileSystemInfos.Length; i++)
				{
					FileSystemInfo fileSystemInfo = fileSystemInfos[i];
					RemoveFile(fileSystemInfo);
				}
			}
		}

		/// <summary>
		/// �����ļ���dir�·��Ϲ�������filter�е��ļ������ļ���ӵ�results��
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="filter"></param>
		/// <param name="results"></param>
		/// <returns></returns>
		public static void SearchFiles(DirectoryInfo dir, IFileSystemInfoFilter filter, IList results)
		{
			FileSystemInfo[] fileSystemInfos = dir.GetFileSystemInfos();
			var list = new ArrayList();
			for (var i = 0; i < fileSystemInfos.Length; i++)
			{
				FileSystemInfo fileSystemInfo = fileSystemInfos[i];
				if (filter.Accept(fileSystemInfo))
					list.Add(fileSystemInfo);
			}

			fileSystemInfos = new FileSystemInfo[list.Count];
			list.CopyTo(fileSystemInfos);
			for (var i = 0; i < fileSystemInfos.Length; i++)
			{
				FileSystemInfo fileSystemInfo = fileSystemInfos[i];
				if (fileSystemInfo.IsDirectory())
					SearchFiles((DirectoryInfo)fileSystemInfo, filter, results);
				else
					results.Add(fileSystemInfo);
			}
		}

		public static void CreateDirectoryIfNotExist(string path)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
		}

		public static void CreateFileIfNotExist(string filePath)
		{
			if (!File.Exists(filePath))
			{
				string dirPath = Path.GetDirectoryName(filePath);
				CreateDirectoryIfNotExist(dirPath);
				File.Create(filePath).Dispose();
			}
		}


//		/// <summary>
//		/// dirĿ¼�´���prefix+yyyyMMddHHmmssSSS(len����ȡyyyyMMddHHmmssSSS�Ķ���λ)+suffix�ļ�
//		/// </summary>
//		/// <param name="dir">Ŀ¼</param>
//		/// <param name="prefix">ǰ׺</param>
//		/// <param name="suffix">��׺</param>
//		/// <param name="length"></param>
//		/// <returns></returns>
//		private static FileInfo CreateTimelyFile(DirectoryInfo dir, string prefix, string suffix, int length,
//			RandomManager randomManager = null)
//		{
//			randomManager = randomManager ?? Client.instance.randomManager;
//			int i = 0;
//			do
//			{
//				string dateTime = DateTimeUtil.NowDateTime().ToString(StringConst.STRING_yyyyMMddHHmmssSSS);
//				string rand = new StringBuilder(randomManager.RandomInt(0, 1000).ToString()).ToString();
//				string stem = (dateTime + rand.FillHead(3, CharConst.CHAR_0))
//					.Substring(0, length);
//
//				DirectoryInfo timelyDirectoryInfo = dir.CreateSubdirectory(stem);
//				timelyDirectoryInfo.Create();
//				if (timelyDirectoryInfo.Exists)
//				{
//					string fileName = prefix + stem + suffix;
//					var fileInfo = new FileInfo(timelyDirectoryInfo.SubPath(fileName));
//					if (fileInfo.Exists)
//						continue;
//					fileInfo.Create().Close();
//					return fileInfo;
//				}
//
//				throw new IOException(string.Format("�޷�����Ŀ¼:", timelyDirectoryInfo.FullName));
//			} while (i++ < 10000);
//
//			throw new IOException(string.Format("{0}���޷�����Ψһ�ļ�", dir.FullName));
//		}
	}
}

