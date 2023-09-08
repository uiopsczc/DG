using System;
using System.IO;

namespace DG
{
	public static class System_IO_FileInfo_Extension
	{
		/// <summary>
		///   后缀
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static string Suffix(this FileInfo self)
		{
			return FileInfoUtil.Suffix(self);
		}

		/// <summary>
		///   不带后缀的name
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static string NameWithoutSuffix(this FileInfo self)
		{
			return FileInfoUtil.NameWithoutSuffix(self);
		}

		/// <summary>
		///   不带后缀的name（全路径）
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static string FullNameWithoutSuffix(this FileInfo self)
		{
			return FileInfoUtil.FullNameWithoutSuffix(self);
		}


		/// <summary>
		///   将data写入文件file中(append:是否追加到文件末尾)
		/// </summary>
		/// <param name="self"></param>
		/// <param name="data"></param>
		/// <param name="isAppend">是否追加到文件末尾</param>
		/// <returns></returns>
		public static void WriteFile(this FileInfo self, byte[] data, bool isAppend)
		{
			FileInfoUtil.WriteFile(self, data, isAppend);
		}

		/// <summary>
		///   读取文件file的内容
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static byte[] ReadBytes(this FileInfo self)
		{
			return FileInfoUtil.ReadBytes(self);
		}


		/// <summary>
		///   向文件file写入content内容(append:是否追加到文件末尾)
		/// </summary>
		/// <param name="self"></param>
		/// <param name="content"></param>
		/// <param name="isAppend">是否追加到文件末尾</param>
		/// <returns></returns>
		public static void WriteTextFile(this FileInfo self, string content, bool isWriteLine, bool isAppend)
		{
			FileInfoUtil.WriteTextFile(self, content, isWriteLine, isAppend);
		}


		/// <summary>
		///   读取文件file，返回字符串内容
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static string ReadTextFile(this FileInfo self)
		{
			return FileInfoUtil.ReadTextFile(self);
		}

		/// <summary>
		///   将data写入文件file中
		/// </summary>
		/// <param name="self"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static void WriteFile(this FileInfo self, byte[] data)
		{
			FileInfoUtil.WriteFile(self, data);
		}
	}
}