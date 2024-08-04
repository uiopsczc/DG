using System;

namespace DG
{
	public static class LongUtil
	{
		// >>>�޷�������
		public static long RightShift3(long value, int shiftAmount)
		{
			//�ƶ� 0 λʱֱ�ӷ���ԭֵ
			if (shiftAmount != 0)
			{
				// int.MaxValue = 0x7FFFFFFF �������ֵ
				long mask = long.MaxValue;
				//�޷����������λ����ʾ�����������������з��ŵģ��з���������1λ������ʱ��λ��0������ʱ��λ��1
				value >>= 1;
				//���������ֵ�����߼������㣬�����Ľ��Ϊ���Ա�ʾ����ֵ�����λ
				value &= mask;
				//�߼�������ֵ�޷��ţ����޷��ŵ�ֱֵ�����������㣬����ʣ�µ�λ
				value >>= shiftAmount - 1;
			}
			return value;
		}

		/// <summary>
		///   longת��Ϊָ�����ƣ�16���ƻ���8���Ƶȣ�
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
		///   ������ת��Ϊbytes
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
			return string.Format(StringConst.STRING_FORMAT_NUMBER_WITH_COMMA, v);
		}

		#region ˽�з���

		/// <summary>
		///   longת��ΪtoBase����
		/// </summary>
		private static string _H2X(long value, int toBase)
		{
			int digitIndex;
			var longPositive = Math.Abs(value);
			var radix = toBase;
			var outDigits = new char[63];
			var constChars = CharConst.DIGITS_AND_CHARS_BIG;

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

