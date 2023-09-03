namespace DG
{
	public static class System_String_Extension
	{
		public static int IndexEndOf(this string self, string value, int startIndex, int count)
		{
			return StringUtil.IndexEndOf(self, value, startIndex, count);
		}

		public static int IndexEndOf(this string self, string value, int startIndex = 0)
		{
			return StringUtil.IndexEndOf(self, value, startIndex);
		}

		public static int LastIndexEndOf(this string self, string value, int startIndex, int count)
		{
			return StringUtil.LastIndexEndOf(self, value, startIndex, count);
		}

		public static int LastIndexEndOf(this string self, string value, int startIndex = 0)
		{
			return StringUtil.LastIndexEndOf(self, value, startIndex);
		}
	}
}