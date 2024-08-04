using System.IO;

namespace DG
{
	public static class System_IO_Stream_Extension
	{
		public static void WriteChar(this Stream self, char i)
		{
			StreamUtil.WriteChar(self, i);
		}

		public static void WriteShort(this Stream self, short i)
		{
			StreamUtil.WriteShort(self, i);
		}

		public static void WriteInt(this Stream self, int i)
		{
			StreamUtil.WriteInt(self, i);
		}

		public static void WriteLong(this Stream self, long i)
		{
			StreamUtil.WriteLong(self, i);
		}

		public static void WriteFloat(this Stream self, float i)
		{
			StreamUtil.WriteFloat(self, i);
		}

		public static void WriteDouble(this Stream self, double i)
		{
			StreamUtil.WriteDouble(self, i);
		}
		

		public static char ReadChar(this Stream self)
		{
			return StreamUtil.ReadChar(self);
		}

		public static short ReadShort(this Stream self)
		{
			return StreamUtil.ReadShort(self);
		}

		public static int ReadInt(this Stream self)
		{
			return StreamUtil.ReadInt(self);
		}

		public static long ReadLong(this Stream self)
		{
			return StreamUtil.ReadLong(self);
		}

		public static float ReadFloat(this Stream self)
		{
			return StreamUtil.ReadFloat(self);
		}

		public static double ReadDouble(this Stream self)
		{
			return StreamUtil.ReadDouble(self);
		}

		/// <summary>
		///   读取stream中的数据到buffer中
		/// </summary>
		/// <param name="self"></param>
		/// <param name="buffer"></param>
		/// <returns>读到的数目</returns>
		public static int ReadBytes(this Stream self, byte[] buffer)
		{
			return StreamUtil.ReadBytes(self, buffer);
		}


		/// <summary>
		///   在Stream读取len长度的数据
		/// </summary>
		/// <param name="self"></param>
		/// <param name="len"></param>
		/// <returns></returns>
		public static byte[] ReadBytes(this Stream self, int len)
		{
			return StreamUtil.ReadBytes(self, len);
		}

		/// <summary>
		///   读取ins的全部数据
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static byte[] ReadBytes(this Stream self)
		{
			return StreamUtil.ReadBytes(self);
		}

		/// <summary>
		///   读取ins的全部数据到outs中
		/// </summary>
		/// <param name="self"></param>
		/// <param name="outs"></param>
		/// <returns></returns>
		public static void CopyToStream(this Stream self, Stream outs)
		{
			StreamUtil.CopyToStream(self, outs);
		}
	}
}