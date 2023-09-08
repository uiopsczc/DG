using System.IO;
using System.Text;

namespace DG
{
	public static class FileInfoUtil
	{
		/// <summary>
		///   ��׺
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <returns></returns>
		public static string Suffix(FileInfo fileInfo)
		{
			return fileInfo.Extension;
		}

		/// <summary>
		///   ������׺��name
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <returns></returns>
		public static string NameWithoutSuffix(FileInfo fileInfo)
		{
			return fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf(CharConst.Char_Dot));
		}

		/// <summary>
		///   ������׺��name��ȫ·����
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <returns></returns>
		public static string FullNameWithoutSuffix(FileInfo fileInfo)
		{
			return fileInfo.FullName.Substring(0, fileInfo.FullName.LastIndexOf(CharConst.Char_Dot));
		}


		/// <summary>
		///   ��dataд���ļ�file��(append:�Ƿ�׷�ӵ��ļ�ĩβ)
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <param name="data"></param>
		/// <param name="isAppend">�Ƿ�׷�ӵ��ļ�ĩβ</param>
		/// <returns></returns>
		public static void WriteFile(FileInfo fileInfo, byte[] data, bool isAppend)
		{
			var fos = new FileStream(fileInfo.FullName, isAppend ? FileMode.Append : FileMode.Truncate, FileAccess.Write);
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
		///   ��ȡ�ļ�file������
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <returns></returns>
		public static byte[] ReadBytes(FileInfo fileInfo)
		{
			var fis = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
			try
			{
				var data = new byte[(int)fileInfo.Length];
				fis.Read(data, 0, data.Length);
				return data;
			}
			finally
			{
				fis.Close();
			}
		}


		/// <summary>
		///   ���ļ�fileд��content����(append:�Ƿ�׷�ӵ��ļ�ĩβ)
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <param name="content"></param>
		/// <param name="isAppend">�Ƿ�׷�ӵ��ļ�ĩβ</param>
		/// <returns></returns>
		public static void WriteTextFile(FileInfo fileInfo, string content, bool isWriteLine, bool isAppend)
		{
			var fw = new StreamWriter(fileInfo.FullName, isAppend);
			try
			{
				if (!isWriteLine)
					fw.Write(content);
				else
					fw.WriteLine(content);
			}
			finally
			{
				fw.Close();
			}
		}


		/// <summary>
		///   ��ȡ�ļ�file�������ַ�������
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <returns></returns>
		public static string ReadTextFile(FileInfo fileInfo)
		{
			var fr = new StreamReader(fileInfo.FullName);

			var stringBuilder = new StringBuilder();
			var chars = new char[1024];
			try
			{
				int n;
				while ((n = fr.Read(chars, 0, chars.Length)) != 0)
					stringBuilder.Append(chars, 0, n);
				return stringBuilder.ToString();
			}
			finally
			{
				fr.Close();
			}
		}

		/// <summary>
		///   ��dataд���ļ�file��
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static void WriteFile(FileInfo fileInfo, byte[] data)
		{
			fileInfo.WriteFile(data, false);
		}
	}
}