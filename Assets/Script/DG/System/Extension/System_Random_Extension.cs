using System;

namespace DG
{
	public static class System_Random_Extension
	{
		/// <summary>
		///   随机长整数
		/// </summary>
		public static long RandomLong(this Random self, EDigitSign sign = EDigitSign.All)
		{
			var bytes = new byte[8];
			self.NextBytes(bytes);
			if (sign == EDigitSign.Positive)
				bytes[0] = 0; //非负
			else if (sign == EDigitSign.Negative)
				bytes[0] = 1; //非负
			return ByteUtil.ToLong(bytes);
		}

		public static bool RandomBool(this Random self)
		{
			return self.Next(2) != 0;
		}
	}
}