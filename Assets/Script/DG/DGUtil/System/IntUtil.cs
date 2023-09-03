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
	}
}

