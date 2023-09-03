using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DG
{
	public static class StringUtil
	{
		public static string[] SplitIgnore(string self, string split = StringConst.String_Comma,
			string ignoreLeft = StringConst.String_Regex_DoubleQuotes,
			string ignoreRight = null)
		{
			if (ignoreRight == null)
				ignoreRight = ignoreLeft;
			var resultList = new List<string>();
			//https://blog.csdn.net/scrOUT/article/details/90517304
			//    var regex = new Regex("(" + split + ")" + "(?=([^\\\"]*\\\"[^\\\"]*\\\")*[^\\\"]*$)"); //双引号内的逗号不分割  双引号外的逗号进行分割
			string pattern = string.Format(StringConst.String_Regex_Format_SplitIgnore, split, ignoreLeft, ignoreLeft,
				ignoreRight, ignoreRight, ignoreRight);
			var regex = new Regex(pattern);
			var startIndex = -1;
			var matchCollection = regex.Matches(self);
			for (var i = 0; i < matchCollection.Count; i++)
			{
				Match match = matchCollection[i];
				var element = self.Substring(startIndex + 1, match.Index - (startIndex + 1));
				resultList.Add(element);
				startIndex = match.Index;
			}

			if (startIndex <= self.Length - 1)
			{
				if (self.Length == 0)
					resultList.Add(StringConst.String_Empty);
				else if (startIndex == self.Length - split.Length && self.Substring(startIndex).Equals(split))
					resultList.Add(StringConst.String_Empty);
				else
					resultList.Add(self.Substring(startIndex + 1));
			}

			return resultList.ToArray();
		}

		public static bool IsNumber(string self)
		{
			return Regex.IsMatch(self, StringConst.String_Regex_Integer) ||
			       Regex.IsMatch(self, StringConst.String_Regex_Float);
		}

		public static string LinkString(string split, params string[] args)
		{
			var stringBuilder = new StringBuilder(args.Length * 4);
			for (int i = 0; i < args.Length; i++)
			{
				string arg = args[i];
				if (i == args.Length - 1)
					stringBuilder.Append(arg);
				else
					stringBuilder.Append(arg + split);
			}

			return stringBuilder.ToString();
		}

		public static string LinkStringWithCommon(params string[] args)
		{
			if (args == null || args.Length == 0)
				return null;

			switch (args.Length)
			{
				case 1:
					return args.ToString();
				case 2:
					return string.Format(StringConst.String_Format_LinkComma_2, args);
				case 3:
					return string.Format(StringConst.String_Format_LinkComma_3, args);
				case 4:
					return string.Format(StringConst.String_Format_LinkComma_4, args);
				case 5:
					return string.Format(StringConst.String_Format_LinkComma_5, args);
				case 6:
					return string.Format(StringConst.String_Format_LinkComma_6, args);
				case 7:
					return string.Format(StringConst.String_Format_LinkComma_7, args);
				case 8:
					return string.Format(StringConst.String_Format_LinkComma_8, args);
				case 9:
					return string.Format(StringConst.String_Format_LinkComma_9, args);
				case 10:
					return string.Format(StringConst.String_Format_LinkComma_10, args);
				default:
					throw new Exception("Exception:ArgsTooLong");
			}
		}

		public static string LinkStringWithUnderLine(params string[] args)
		{
			if (args == null || args.Length == 0)
				return null;

			switch (args.Length)
			{
				case 1:
					return args.ToString();
				case 2:
					return string.Format(StringConst.String_Format_LinkUnderLine_2, args);
				case 3:
					return string.Format(StringConst.String_Format_LinkUnderLine_3, args);
				case 4:
					return string.Format(StringConst.String_Format_LinkUnderLine_4, args);
				case 5:
					return string.Format(StringConst.String_Format_LinkUnderLine_5, args);
				case 6:
					return string.Format(StringConst.String_Format_LinkUnderLine_6, args);
				case 7:
					return string.Format(StringConst.String_Format_LinkUnderLine_7, args);
				case 8:
					return string.Format(StringConst.String_Format_LinkUnderLine_8, args);
				case 9:
					return string.Format(StringConst.String_Format_LinkUnderLine_9, args);
				case 10:
					return string.Format(StringConst.String_Format_LinkUnderLine_10, args);
				default:
					throw new Exception("Exception:ArgsTooLong");
			}
		}

		public static string RoundBrackets(string arg)
		{
			return string.Format(StringConst.String_Format_RoundBrackets, arg);
		}


		public static int CheckInsertLine(string content, int startCheckInsertIndex, List<string> lineList)
		{
			content = content.Trim(new[] {'\r', '\n'});
			int insertLineIndex = lineList.IndexOf(content, startCheckInsertIndex);
			//如果lineList中没有content的内容的行，则直接插入
			if (insertLineIndex < 0)
			{
				lineList.Insert(startCheckInsertIndex, content);
				insertLineIndex = startCheckInsertIndex;
			}

			return insertLineIndex;
		}

		public static int IndexEndOf(string content, string value, int startIndex, int count)
		{
			count = startIndex + count > content.Length ? content.Length - startIndex : count;
			var index = content.IndexOf(value, startIndex, count);
			return index == -1 ? index : index + value.Length - 1;
		}

		public static int IndexEndOf(string content, string value, int startIndex = 0)
		{
			return IndexEndOf(content, value, startIndex, content.Length - startIndex);
		}

		public static int LastIndexEndOf(string content, string value, int startIndex, int count)
		{
			count = startIndex + count > content.Length ? content.Length - startIndex : count;
			var index = content.LastIndexOf(value, startIndex, count);
			return index == -1 ? index : index + value.Length - 1;
		}

		public static int LastIndexEndOf(string content, string value, int startIndex = 0)
		{
			return LastIndexEndOf(content, value, startIndex, content.Length - startIndex);
		}
	}
}