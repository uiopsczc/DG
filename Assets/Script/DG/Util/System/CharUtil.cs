using System;

namespace DG
{
	public static class CharUtil
	{
		public static char[] GetCharsAll()
		{
			var count = CharConst.CHARS_BIG.Length + CharConst.CHARS_SMALL.Length;
			var result = new char[count];
			Array.Copy(CharConst.CHARS_BIG, result, CharConst.CHARS_BIG.Length);
			Array.Copy(CharConst.CHARS_SMALL, 0, result, CharConst.CHARS_BIG.Length, CharConst.CHARS_SMALL.Length);
			return result;
		}

		public static char[] GetDigitsAndCharsBig()
		{
			var count = CharConst.DIGITS.Length + CharConst.CHARS_BIG.Length;
			var result = new char[count];
			Array.Copy(CharConst.DIGITS, result, CharConst.DIGITS.Length);
			Array.Copy(CharConst.CHARS_BIG, 0, result, CharConst.DIGITS.Length, CharConst.CHARS_BIG.Length);
			return result;
		}

		public static char[] GetDigitsAndCharsSmall()
		{
			var count = CharConst.DIGITS.Length + CharConst.CHARS_SMALL.Length;
			var result = new char[count];
			Array.Copy(CharConst.DIGITS, result, CharConst.DIGITS.Length);
			Array.Copy(CharConst.CHARS_SMALL, 0, result, CharConst.DIGITS.Length, CharConst.CHARS_SMALL.Length);
			return result;
		}

		public static char[] GetDigitsAndCharsAll()
		{
			var count = CharConst.DIGITS.Length + CharConst.CHARS_ALL.Length;
			var result = new char[count];
			Array.Copy(CharConst.DIGITS, result, CharConst.DIGITS.Length);
			Array.Copy(CharConst.CHARS_ALL, 0, result, CharConst.DIGITS.Length, CharConst.CHARS_ALL.Length);
			return result;
		}

		/// <summary>
		/// 转化为bytes
		/// </summary>
		/// <param name="c"></param>
		/// <param name="isNetOrder">是否是网络顺序</param>
		/// <returns></returns>
		public static byte[] ToBytes(char c, bool isNetOrder = false)
		{
			byte[] data = BitConverter.GetBytes(c);
			if (isNetOrder)
				Array.Reverse(data);
			return data;
		}

		public static bool IsUpper(char c)
		{
			return c >= CharConst.CHAR_A && c <= CharConst.CHAR_Z;
		}

		public static bool IsLower(char c)
		{
			return c >= CharConst.CHAR_a && c <= CharConst.CHAR_z;
		}
	}
}

