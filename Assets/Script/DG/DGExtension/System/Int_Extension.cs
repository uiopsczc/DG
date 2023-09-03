using System;

namespace DG
{
	public static class Int_Extension
	{
		public static int RightShift3(this int self, int shiftAmount)
		{
			return IntUtil.RightShift3(self, shiftAmount);
		}

		public static T ToEnum<T>(this int self)
		{
			return IntUtil.ToEnum<T>(self);
		}
	}
}