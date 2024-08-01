using System;

namespace DG
{
	public static class UIntUtil
	{
		public static bool IsContains(uint value, uint beContainedValue)
		{
			return (value & beContainedValue) == beContainedValue;
		}
	}
}

