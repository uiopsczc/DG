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
	}
}

