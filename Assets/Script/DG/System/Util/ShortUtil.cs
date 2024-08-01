using System;

namespace DG
{
	public static class ShortUtil
	{
		#region bytes

		/// <summary>
		///   将数字转化为bytes
		/// </summary>
		public static byte[] ToBytes(short v, bool isNetOrder = false)
		{
			return ByteUtil.ToBytes(v & 0xFFFF, 2, isNetOrder);
		}

		#endregion

		public static short Minimum(short v, short minimum)
		{
			return Math.Max(v, minimum);
		}

		public static short Maximum(short v, short maximum)
		{
			return Math.Min(v, maximum);
		}
	}
}