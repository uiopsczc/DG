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
	}
}