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
				value = value >> 1;
				//���������ֵ�����߼������㣬�����Ľ��Ϊ���Ա�ʾ����ֵ�����λ
				value = value & mask;
				//�߼�������ֵ�޷��ţ����޷��ŵ�ֱֵ�����������㣬����ʣ�µ�λ
				value = value >> shiftAmount - 1;
			}
			return value;
		}
	}
}

