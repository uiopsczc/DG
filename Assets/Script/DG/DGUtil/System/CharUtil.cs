using System;

namespace DG
{
	public static class CharUtil
	{
		public static char[] GetCharsAll()
		{
			var count = CharConst.Chars_Big.Length + CharConst.Chars_Small.Length;
			var result = new char[count];
			Array.Copy(CharConst.Chars_Big, result, CharConst.Chars_Big.Length);
			Array.Copy(CharConst.Chars_Small, 0, result, CharConst.Chars_Big.Length, CharConst.Chars_Small.Length);
			return result;
		}

		public static char[] GetDigitsAndCharsBig()
		{
			var count = CharConst.Digits.Length + CharConst.Chars_Big.Length;
			var result = new char[count];
			Array.Copy(CharConst.Digits, result, CharConst.Digits.Length);
			Array.Copy(CharConst.Chars_Big, 0, result, CharConst.Digits.Length, CharConst.Chars_Big.Length);
			return result;
		}

		public static char[] GetDigitsAndCharsSmall()
		{
			var count = CharConst.Digits.Length + CharConst.Chars_Small.Length;
			var result = new char[count];
			Array.Copy(CharConst.Digits, result, CharConst.Digits.Length);
			Array.Copy(CharConst.Chars_Small, 0, result, CharConst.Digits.Length, CharConst.Chars_Small.Length);
			return result;
		}

		public static char[] GetDigitsAndCharsAll()
		{
			var count = CharConst.Digits.Length + CharConst.CharsAll.Length;
			var result = new char[count];
			Array.Copy(CharConst.Digits, result, CharConst.Digits.Length);
			Array.Copy(CharConst.CharsAll, 0, result, CharConst.Digits.Length, CharConst.CharsAll.Length);
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
			return c >= CharConst.Char_A && c <= CharConst.Char_Z;
		}

		public static bool IsLower(char c)
		{
			return c >= CharConst.Char_a && c <= CharConst.Char_z;
		}
	}
}

