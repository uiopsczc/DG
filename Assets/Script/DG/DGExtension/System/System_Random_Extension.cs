using System;

namespace DG
{
	public static class System_Random_Extension
	{
		public static long NextLong(this Random self)
		{
			var result = ((long)self.Next(32) << 32) + self.Next(32);
			return result;
		}
	}
}