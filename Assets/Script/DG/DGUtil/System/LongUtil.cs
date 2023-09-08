using System;

namespace DG
{
	public static class LongUtil
	{
		// >>>无符号右移
		public static long RightShift3(long value, int shiftAmount)
		{
			//移动 0 位时直接返回原值
			if (shiftAmount != 0)
			{
				// int.MaxValue = 0x7FFFFFFF 整数最大值
				long mask = long.MaxValue;
				//无符号整数最高位不表示正负但操作数还是有符号的，有符号数右移1位，正数时高位补0，负数时高位补1
				value = value >> 1;
				//和整数最大值进行逻辑与运算，运算后的结果为忽略表示正负值的最高位
				value = value & mask;
				//逻辑运算后的值无符号，对无符号的值直接做右移运算，计算剩下的位
				value = value >> shiftAmount - 1;
			}
			return value;
		}

		/// <summary>
		///   long转化为指定进制（16进制或者8进制等）
		/// </summary>
		public static string ToString(long v, int xbase)
		{
			return _H2X(v, xbase);
		}

//		public static string GetAssetPathByRefId(long refId)
//		{
//			return AssetPathRefManager.instance.GetAssetPathByRefId(refId);
//		}

		#region bytes

		/// <summary>
		///   将数字转化为bytes
		/// </summary>
		public static byte[] ToBytes(long v, bool isNetOrder = false)
		{
			return ByteUtil.ToBytes(v, 8, isNetOrder);
		}

		#endregion


		public static long Minimum(long v, long minimum)
		{
			return Math.Max(v, minimum);
		}

		public static long Maximum(long v, long maximum)
		{
			return Math.Min(v, maximum);
		}

		public static string ToStringWithComma(long v)
		{
			return string.Format(StringConst.String_Format_NumberWithComma, v);
		}

		#region 私有方法

		/// <summary>
		///   long转化为toBase进制
		/// </summary>
		private static string _H2X(long value, int toBase)
		{
			int digitIndex;
			var longPositive = Math.Abs(value);
			var radix = toBase;
			var outDigits = new char[63];
			var constChars = CharConst.DigitsAndCharsBig;

			for (digitIndex = 0; digitIndex <= 64; digitIndex++)
			{
				if (longPositive == 0) break;

				outDigits[outDigits.Length - digitIndex - 1] =
					constChars[longPositive % radix];
				longPositive /= radix;
			}

			return new string(outDigits, outDigits.Length - digitIndex, digitIndex);
		}

		#endregion
	}
}

