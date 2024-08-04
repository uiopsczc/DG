namespace DG
{
	public static class System_Char_Extension
	{
		/// <summary>
		/// 转化为bytes
		/// </summary>
		/// <param name="self"></param>
		/// <param name="isNetOrder">是否是网络顺序</param>
		/// <returns></returns>
		public static byte[] ToBytes(this char self, bool isNetOrder = false)
		{
			return CharUtil.ToBytes(self, isNetOrder);
		}

		public static bool IsUpper(this char self)
		{
			return CharUtil.IsUpper(self);
		}

		public static bool IsLower(this char self)
		{
			return CharUtil.IsLower(self);
		}
	}
}