using System;

namespace DG
{
	public static class Long_Extension
	{
		// >>>无符号右移
		public static long RightShift3(this long self, int shiftAmount)
		{
			return LongUtil.RightShift3(self, shiftAmount);
		}

		/// <summary>
		///   long转化为指定进制（16进制或者8进制等）
		/// </summary>
		public static string ToString(this long self, int xbase)
		{
			return LongUtil.ToString(self, xbase);
		}

		//		public static string GetAssetPathByRefId(long refId)
		//		{
		//			return AssetPathRefManager.instance.GetAssetPathByRefId(refId);
		//		}

		#region bytes

		/// <summary>
		///   将数字转化为bytes
		/// </summary>
		public static byte[] ToBytes(this long self, bool isNetOrder = false)
		{
			return LongUtil.ToBytes(self, isNetOrder);
		}

		#endregion


		public static long Minimum(this long self, long minimum)
		{
			return LongUtil.Minimum(self, minimum);
		}

		public static long Maximum(this long self, long maximum)
		{
			return LongUtil.Minimum(self, maximum);
		}

		public static string ToStringWithComma(this long self)
		{
			return LongUtil.ToStringWithComma(self);
		}
	}
}