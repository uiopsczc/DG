using System;

namespace DG
{
	public static class IntUtil
	{
		// >>>�޷�������
		public static int RightShift3(int value, int shiftAmount)
		{
			//�ƶ� 0 λʱֱ�ӷ���ԭֵ
			if (shiftAmount != 0)
			{
				// int.MaxValue = 0x7FFFFFFF �������ֵ
				int mask = int.MaxValue;
				//�޷����������λ����ʾ�����������������з��ŵģ��з���������1λ������ʱ��λ��0������ʱ��λ��1
				value = value >> 1;
				//���������ֵ�����߼������㣬�����Ľ��Ϊ���Ա�ʾ����ֵ�����λ
				value = value & mask;
				//�߼�������ֵ�޷��ţ����޷��ŵ�ֱֵ�����������㣬����ʣ�µ�λ
				value = value >> shiftAmount - 1;
			}
			return value;
		}

		public static T ToEnum<T>(int value)
		{
			return (T)Enum.ToObject(typeof(T), value);
		}


		#region ����

		/// <summary>
		///   10����תΪ16�����ַ���
		/// </summary>
		public static string ToHexString(int v)
		{
			return v.ToString(StringConst.String_X);
		}

		#endregion

		#region bytes

		/// <summary>
		///   ������ת��Ϊbytes
		/// </summary>
		public static byte[] ToBytes(int v, bool isNetOrder = false)
		{
			return ByteUtil.ToBytes(v & 0xFFFFFFFF, 4, isNetOrder);
		}

		#endregion

//		/// <summary>
//		///   ���һ��total���ڵĶ��У����������Ԫ�ز����ظ���
//		/// </summary>
//		/// <param name="self"></param>
//		/// <param name="isIncludeTotal">�Ƿ����total</param>
//		/// <param name="isZeroBase">�Ƿ��0��ʼ</param>
//		/// <returns></returns>
//		public static List<int> Random(this int self, float outCount, bool isUnique, bool isIncludeTotal = false,
//			bool isZeroBase = true, RandomManager randomManager = null)
//		{
//			randomManager = randomManager ?? Client.instance.randomManager;
//			var result = new List<int>();
//			var toRandomList = new List<int>(); //Ҫ�������List
//
//			for (var i = isZeroBase ? 0 : 1; i < (isIncludeTotal ? self + 1 : self); i++)
//				toRandomList.Add(i);
//
//			for (var i = 0; i < outCount; i++)
//			{
//				var index = randomManager.RandomInt(0, toRandomList.Count);
//				result.Add(isUnique ? toRandomList.RemoveAt2(index) : toRandomList[index]);
//			}
//
//			return result;
//		}

		//�Ƿ���defalut, Ĭ������float.MaxValue�Ƚ�
		public static bool IsDefault(int v, bool isMin = false)
		{
			return isMin ? v == int.MinValue : v == int.MaxValue;
		}

		public static bool IsInRange(int v, int minValue, int maxValue, bool isMinValueIncluded = false,
			bool isMaxValueIncluded = false)
		{
			return v >= minValue && v <= maxValue &&
				   ((v != minValue || isMinValueIncluded) && (v != maxValue || isMaxValueIncluded));
		}

		public static int Minimum(int v, int minimum)
		{
			return Math.Max(v, minimum);
		}

		public static int Maximum(int v, int maximum)
		{
			return Math.Min(v, maximum);
		}

		public static string ToStringWithComma(int v)
		{
			return string.Format(StringConst.String_Format_NumberWithComma, v);
		}
	}
}

