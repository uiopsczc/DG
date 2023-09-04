using System;

namespace DG
{
	public static class FloatUtil
	{
		/// <summary>
		///   �Ƚ�����float�Ƿ���ȣ��������FloatConst.EPSILON���ж�Ϊ��ȣ������ж�Ϊ�����
		/// </summary>
		/// <param name="v"></param>
		/// <param name="float2"></param>
		/// <param name="epsilon"></param>
		/// <returns></returns>
		public static bool EqualsEpsilon(float v, float float2, float epsilon = float.Epsilon)
		{
			return Math.Abs(v - float2) < epsilon;
		}

		/// <summary>
		///   תΪbytes
		/// </summary>
		/// <param name="v"></param>
		/// <param name="isNetOrder">�Ƿ�������˳������˳�����෴��</param>
		/// <returns></returns>
		public static byte[] ToBytes(float v, bool isNetOrder = false)
		{
			var data = BitConverter.GetBytes(v);
			if (isNetOrder)
				Array.Reverse(data);
			return data;
		}


		//�Ƿ���defalut, Ĭ������float.MaxValue�Ƚ�
		public static bool IsDefault(float v, bool isMin = false)
		{
			return isMin ? v == float.MinValue : v == float.MaxValue;
		}

		//�õ��ٷֱ�
		public static float GetPercent(float value, float minValue, float maxValue, bool isClamp = true)
		{
			if (isClamp)
			{
				if (value < minValue)
					value = minValue;
				else if (value > maxValue)
					value = maxValue;
			}

			float offset = value - minValue;
			return offset / (maxValue - minValue);
		}

		public static bool IsInRange(float value, float minValue, float maxValue, bool isMinValueInclude = false,
			bool isMaxValueInclude = false)
		{
			return !(value < minValue) && !(value > maxValue) &&
				   ((value != minValue || isMinValueInclude) && (value != maxValue || isMaxValueInclude));
		}

		/// <summary>
		/// �ٷֱ�  ����0.1,���10%
		/// </summary>
		/// <param name="pct"></param>
		/// <returns></returns>
		public static string ToPctString(float pct)
		{
			return string.Format(StringConst.String_Format_Pct, pct * 100);
		}

		//��v Round��������snap_soze�ı�����ֵ
		//Rounds value to the closest multiple of snap_size.
		public static float Snap(float self, float snapSize)
		{
			return (float)(Math.Round(self / snapSize) * snapSize);
		}

		public static float Snap2(float self, float snapSize)
		{
			return (float)(Math.Round(self * snapSize) / snapSize);
		}

		public static float Minimum(float v, float minimum)
		{
			return Math.Max(v, minimum);
		}

		public static float Maximum(float v, float maximum)
		{
			return Math.Min(v, maximum);
		}
	}
}

