using System.IO;
using System.Text;

namespace DG
{
	public static class StreamUtil
	{
		public static void WriteChar(Stream stream, char i)
		{
			var data = i.ToBytes();
			Write(stream, data);
		}

		public static void WriteShort(Stream stream, short i)
		{
			var data = i.ToBytes();
			Write(stream, data);
		}

		public static void WriteInt(Stream stream, int i)
		{
			var data = i.ToBytes();
			Write(stream, data);
		}

		public static void WriteLong(Stream stream, long i)
		{
			var data = i.ToBytes();
			Write(stream, data);
		}

		public static void WriteFloat(Stream stream, float i)
		{
			var data = i.ToBytes();
			Write(stream, data);
		}

		public static void WriteDouble(Stream stream, double i)
		{
			var data = i.ToBytes();
			Write(stream, data);
		}

		private static void Write(Stream stream, byte[] data)
		{
			stream.Write(data, 0, data.Length);
		}

		private static byte[] Read(Stream stream, byte[] data)
		{
			stream.Read(data, 0, data.Length);
			return data;
		}

		public static char ReadChar(Stream stream)
		{
			var data = new byte[2];
			return ByteUtil.ToChar(Read(stream, data));
		}

		public static short ReadShort(Stream stream)
		{
			var data = new byte[2];
			return ByteUtil.ToShort(Read(stream, data));
		}

		public static int ReadInt(Stream stream)
		{
			var data = new byte[4];
			return ByteUtil.ToInt(Read(stream, data));
		}

		public static long ReadLong(Stream stream)
		{
			var data = new byte[8];
			return ByteUtil.ToLong(Read(stream, data));
		}

		public static float ReadFloat(Stream stream)
		{
			var data = new byte[4];
			return ByteUtil.ToFloat(Read(stream, data));
		}

		public static double ReadDouble(Stream stream)
		{
			var data = new byte[8];
			return ByteUtil.ToDouble(Read(stream, data));
		}

		/// <summary>
		///   读取stream中的数据到buffer中
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="buffer"></param>
		/// <returns>读到的数目</returns>
		public static int ReadBytes(Stream stream, byte[] buffer)
		{
			// Use this method is used to read all bytes from a stream.
			var offset = 0;
			var totalCount = 0;
			var readUnit = buffer.Length > 100 ? 100 : buffer.Length;
			while (true)
			{
				var bytesReadCount = stream.Read(buffer, offset,
					offset + readUnit > buffer.Length ? buffer.Length - offset : readUnit);
				if (bytesReadCount == 0)
					break;
				offset += bytesReadCount;
				totalCount += bytesReadCount;
			}

			return totalCount;
		}


		/// <summary>
		///   在Stream读取len长度的数据
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="len"></param>
		/// <returns></returns>
		public static byte[] ReadBytes(Stream stream, int len)
		{
			var buf = new byte[len];
			len = stream.ReadBytes(buf);
			if (len < buf.Length)
				return ByteUtil.SubBytes(buf, 0, len);
			return buf;
		}

		/// <summary>
		///   读取ins的全部数据
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static byte[] ReadBytes(Stream stream)
		{
			var outs = new MemoryStream();
			stream.CopyToStream(outs);
			return outs.ToArray();
		}

		/// <summary>
		///   读取ins的全部数据到outs中
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="outs"></param>
		/// <returns></returns>
		public static void CopyToStream(Stream stream, Stream outs)
		{
			var data = new byte[4096];
			int len;
			do
			{
				len = stream.ReadBytes(data);
				if (len > 0)
					outs.Write(data, 0, len);
			} while (len >= data.Length); //一般情况下是等于，读完的时候是少于
		}
	}
}