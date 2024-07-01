using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace DG
{
	public static class StringUtil
	{
		/// <summary>
		/// fullFilePath-pathRoot
		/// </summary>
		/// <param name="fullFilePath"></param>
		/// <param name="rootPath"></param>
		/// <returns></returns>
		public static string WithoutRootPath(string fullFilePath, string rootPath,
			char slash = CharConst.CHAR_SLASH)
		{
			return FileUtil.WithoutRootPath(fullFilePath, rootPath, slash);
		}

		/// <summary>
		/// pathRoot+relativeFilePath
		/// </summary>
		/// <param name="relativeFilePath"></param>
		/// <param name="rootPath"></param>
		/// <returns></returns>
		public static string WithRootPath(string relativeFilePath, string rootPath,
			char slash = CharConst.CHAR_SLASH)
		{
			return FileUtil.WithRootPath(relativeFilePath, rootPath, slash);
		}


		public static string LocalURL(string path)
		{
			return FileUtil.LocalURL(path);
		}

		public static string[] SplitIgnore(string self, string split = StringConst.STRING_COMMA,
			string ignoreLeft = StringConst.STRING_REGEX_DOUBLE_QUOTES,
			string ignoreRight = null)
		{
			if (ignoreRight == null)
				ignoreRight = ignoreLeft;
			var resultList = new List<string>();
			//https://blog.csdn.net/scrOUT/article/details/90517304
			//    var regex = new Regex("(" + split + ")" + "(?=([^\\\"]*\\\"[^\\\"]*\\\")*[^\\\"]*$)"); //双引号内的逗号不分割  双引号外的逗号进行分割
			string pattern = string.Format(StringConst.STRING_REGEX_FORMAT_SPLIT_IGNORE, split, ignoreLeft, ignoreLeft,
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
					resultList.Add(StringConst.STRING_EMPTY);
				else if (startIndex == self.Length - split.Length && self.Substring(startIndex).Equals(split))
					resultList.Add(StringConst.STRING_EMPTY);
				else
					resultList.Add(self.Substring(startIndex + 1));
			}

			return resultList.ToArray();
		}

		public static bool IsNumber(string self)
		{
			return Regex.IsMatch(self, StringConst.STRING_REGEX_INTEGER) ||
			       Regex.IsMatch(self, StringConst.STRING_REGEX_FLOAT);
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
					return string.Format(StringConst.STRING_FORMAT_LINK_COMMA_2, args);
				case 3:
					return string.Format(StringConst.STRING_FORMAT_LINK_COMMA_3, args);
				case 4:
					return string.Format(StringConst.STRING_FORMAT_LINK_COMMA_4, args);
				case 5:
					return string.Format(StringConst.STRING_FORMAT_LINK_COMMA_5, args);
				case 6:
					return string.Format(StringConst.STRING_FORMAT_LINK_COMMA_6, args);
				case 7:
					return string.Format(StringConst.STRING_FORMAT_LINK_COMMA_7, args);
				case 8:
					return string.Format(StringConst.STRING_FORMAT_LINK_COMMA_8, args);
				case 9:
					return string.Format(StringConst.STRING_FORMAT_LINK_COMMA_9, args);
				case 10:
					return string.Format(StringConst.STRING_FORMAT_LINK_COMMA_10, args);
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
					return string.Format(StringConst.STRING_FORMAT_LINK_UNDERLINE_2, args);
				case 3:
					return string.Format(StringConst.STRING_FORMAT_LINK_UNDERLINE_3, args);
				case 4:
					return string.Format(StringConst.STRING_FORMAT_LINK_UNDERLINE_4, args);
				case 5:
					return string.Format(StringConst.STRING_FORMAT_LINK_UNDERLINE_5, args);
				case 6:
					return string.Format(StringConst.STRING_FORMAT_LINK_UNDERLINE_6, args);
				case 7:
					return string.Format(StringConst.STRING_FORMAT_LINK_UNDERLINE_7, args);
				case 8:
					return string.Format(StringConst.STRING_FORMAT_LINK_UNDERLINE_8, args);
				case 9:
					return string.Format(StringConst.STRING_FORMAT_LINK_UNDERLINE_9, args);
				case 10:
					return string.Format(StringConst.STRING_FORMAT_LINK_UNDERLINE_10, args);
				default:
					throw new Exception("Exception:ArgsTooLong");
			}
		}

		public static string RoundBrackets(string arg)
		{
			return string.Format(StringConst.STRING_FORMAT_ROUND_BRACKETS, arg);
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


		public static GUIContent ToGUIContent(string content)
		{
			return new GUIContent(content);
		}

		#region Equals

		public static bool EqualsIgnoreCase(string content, string s2)
		{
			return content.ToLower().Equals(s2.ToLower());
		}

		#endregion
		

		#region 判断是否为null或Empty或WhiteSpace

		/// <summary>
		///   判断是否为null或Empty
		/// </summary>
		public static bool IsNullOrWhiteSpace(string content)
		{
			return string.IsNullOrWhiteSpace(content);
		}

		public static bool IsNullOrEmpty(string content)
		{
			return string.IsNullOrEmpty(content);
		}

		#endregion

		#region 加密

		public static string Encrypt(string content)
		{
			return MD5Util.Encrypt(content);
		}

		#endregion

		/// <summary>
		///   将s重复n次返回
		/// </summary>
		public static string Join(string content, int n)
		{
			var stringBuilder = new StringBuilder();
			for (var i = 0; i < n; i++)
				stringBuilder.Append(content);
			return stringBuilder.ToString();
		}

//		public static void RemoveFiles(string content)
//		{
//			StdioUtil.RemoveFiles(content);
//		}

//		public static string Random(string content, int count, bool isUnique)
//		{
//			return new string(content.ToCharArray().RandomArray(count, isUnique));
//		}

		public static int GetSubStringCount(string content, string subString)
		{
			return Regex.Matches(content, subString).Count;
		}

#if UNITY_EDITOR
		/// <summary>
		///   "xx11" comes after "xx2".
		/// </summary>
		/// <param name="content"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static int CompareTo2(string content, string other)
		{
			return EditorUtility.NaturalCompare(content, other);
		}
#endif
		//跟上面这个EditorUtility.NaturalCompare效果是一样的
		public static int AlphanumCompareTo(string content, string other)
		{
			return ComparatorConst.ALPHANUM_COMPARATOR.Compare(content, other);
		}

		public static string FileName(string filePath, string lastSeparator = StringConst.STRING_SLASH)
		{
			var index = filePath.LastIndexOf(lastSeparator);
			if (index < 0)
				index = 0;
			else
				index += lastSeparator.Length;
			return filePath.Substring(index);
		}

		public static string DirPath(string filePath, string lastSeparator = StringConst.STRING_SLASH)
		{
			var index = filePath.LastIndexOf(lastSeparator);
			var length = index < 0 ? filePath.Length : index + lastSeparator.Length;
			return filePath.Substring(0, length);
		}


		

		public static bool IsNetURL(string content)
		{
			return content.StartsWith(StringConst.STRING_HTTP, StringComparison.CurrentCultureIgnoreCase) ||
				   content.StartsWith(StringConst.STRING_FTP, StringComparison.CurrentCultureIgnoreCase);
		}

		public static string WWWURLHandle(string content)
		{
			return content.IsNetURL()
				? content
				: content.IndexOf(StringConst.STRING_FILE_URL_PREFIX, StringComparison.CurrentCultureIgnoreCase) == -1
					? content.WithRootPath(StringConst.STRING_FILE_URL_PREFIX)
					: content;
		}

		public static string ReplaceDirectorySeparatorChar(string content, char separator = CharConst.CHAR_SLASH)
		{
			return content.Replace(Path.DirectorySeparatorChar, separator);
		}

		public static string WithoutAllSuffix(string content)
		{
			var index = content.IndexOf(CharConst.CHAR_DOT);
			return index != -1 ? content.Substring(0, index) : content;
		}

		public static string WithoutSuffix(string content)
		{
			var index = content.LastIndexOf(CharConst.CHAR_DOT);
			return index != -1 ? content.Substring(0, index) : content;
		}

		public static string ToLuaRequirePath(string content)
		{
			return content.Replace(CharConst.CHAR_SLASH, CharConst.CHAR_DOT);
		}


//		public static bool ContainTags(string content, params string[] checkTags)
//		{
//			var tags = content.Split(MultiTagsUtil.TAG_SEPARATOR);
//			for (var i = 0; i < checkTags.Length; i++)
//			{
//				var checkTag = checkTags[i];
//				if (!tags.Contains(checkTag))
//					return false;
//			}
//
//			return true;
//		}

		public static string GetMainAssetPath(string self)
		{
			return self.GetAssetPathInfo().mainAssetPath;
		}

		public static string GetSubAssetPath(string self)
		{
			return self.GetAssetPathInfo().subAssetPath;
		}

		public static AssetPathInfo GetAssetPathInfo(string self)
		{
			return new AssetPathInfo(self);
		}

		public static string GetPreString(string content, string splitContent)
		{
			var index = content.IndexOf(splitContent);
			return index == -1 ? content : content.Substring(0, index);
		}

		public static string GetPostString(string content, string splitContent)
		{
			var index = content.IndexEndOf(splitContent);
			return index == -1 ? content : content.Substring(index + 1);
		}

		public static string Format(string format, params object[] args)
		{
			return string.Format(format, args);
		}

		public static (string mainPart, string cfgPart) GetLineAutoGenLineInfoParts(string mainPart, string cfgPartStartsWith)
		{
			if (cfgPartStartsWith == null)
				return (mainPart, null);
			var index = mainPart.IndexOf(cfgPartStartsWith);
			if (index == -1)
				return (mainPart, null);
			return (mainPart.Substring(0, index), mainPart.Substring(index));
		}

//		public static AutoGenLineInfo ToAutoGenLineInfo(string mainPart, string cfgPartStartsWith = null, string uniqueKey = null, bool isDeleteIfNotExist = false)
//		{
//			if (cfgPartStartsWith == null || uniqueKey == null)
//				return new AutoGenLineInfo(mainPart, null);
//			var cfgInfo = new AutoGenLineCfgInfo(cfgPartStartsWith, uniqueKey,
//				isDeleteIfNotExist);
//			return new AutoGenLineInfo(mainPart, cfgInfo);
//		}
//
//		public static AutoGenLineInfo ParseAutoGenLineInfo(string line, string cfgPartStartsWith)
//		{
//			(string mainPart, string cfgPart) autoGenLineInfo =
//				GetLineAutoGenLineInfoParts(line, cfgPartStartsWith);
//			return new AutoGenLineInfo(autoGenLineInfo.mainPart, autoGenLineInfo.cfgPart, cfgPartStartsWith);
//		}
//
//		public static AutoGenLineCfgInfo ParseAutoGenLineCfgInfo(string cfgPart, string cfgPartStartsWith)
//		{
//			if (cfgPart == null)
//				return null;
//			var index = cfgPart.IndexOf(cfgPartStartsWith);
//			if (index == -1)
//				return null;
//			var result = new AutoGenLineCfgInfo();
//			result.Parse(cfgPart.Substring(0, index + cfgPartStartsWith.Length), cfgPart.Substring(index + cfgPartStartsWith.Length));
//			return result;
//		}

		#region RichText

		public static void SetRichTextColor(string content, Color color)
		{
			RichTextUtil.SetColor(content, color);
		}

		public static void SetRichTextIsBold(string content)
		{
			RichTextUtil.SetIsBold(content);
		}

		public static void SetIsItalic(string content)
		{
			RichTextUtil.SetIsItalic(content);
		}

		public static void SetRichTextFontSize(string content, int fontSize)
		{
			RichTextUtil.SetFontSize(content, fontSize);
		}

		#endregion

		#region 编码

		/// <summary>
		///   字符串的bytes，默认编码是当前系统编码
		/// </summary>
		public static byte[] GetBytes(string content, Encoding encoding = null)
		{
			if (encoding == null)
				encoding = Encoding.UTF8;

			return encoding.GetBytes(content);
		}

		/// <summary>
		///   从字符串的X进制编码的字符串转为十进制的数字（long类型）
		/// </summary>
		public static long ToLong(string content, int fromBase)
		{
			return _X2H(content, fromBase);
		}

		#endregion


		#region 各种转换 ToXX

		public static Vector2 ToVector2(string content, string split = StringConst.STRING_COMMA,
			string trimLeft = StringConst.STRING_LEFT_ROUND_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_ROUND_BRACKETS)
		{
			var elementList = content.ToList<string>(split, trimLeft, trimRight);
			var x = elementList[0].To<float>();
			var y = elementList[1].To<float>();
			return new Vector2(x, y);
		}

		public static Vector2 ToVector2OrDefault(string content, string toDefaultString = null,
			Vector2 defaultValue = default)
		{
			return ObjectUtil.Equals(content, toDefaultString) ? defaultValue : content.ToVector2();
		}

		public static Vector3 ToVector3(string s, string split = StringConst.STRING_COMMA,
			string trimLeft = StringConst.STRING_LEFT_ROUND_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_ROUND_BRACKETS)
		{
			var elementList = s.ToList<string>(split, trimLeft, trimRight);
			var x = elementList[0].To<float>();
			var y = elementList[1].To<float>();
			var z = elementList[2].To<float>();
			return new Vector3(x, y, z);
		}

		public static Vector2Int ToVector2IntOrDefault(string content, string toDefaultString = null,
			Vector2Int defaultValue = default)
		{
			return content.ToVector2OrDefault(toDefaultString, defaultValue).ToVector2Int();
		}


		public static Vector3 ToVector3OrDefault(string content, string toDefaultString = null,
			Vector3 defaultValue = default)
		{
			return ObjectUtil.Equals(content, toDefaultString) ? defaultValue : content.ToVector3();
		}

		public static Vector3Int ToVector3IntOrDefault(string content, string toDefaultString = null,
			Vector3Int defaultValue = default)
		{
			return content.ToVector3OrDefault(toDefaultString, defaultValue).ToVector3Int();
		}

		public static Vector4 ToVector4(string content, string split = StringConst.STRING_COMMA,
			string trimLeft = StringConst.STRING_LEFT_ROUND_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_ROUND_BRACKETS)
		{
			var elementList = content.ToList<string>(split, trimLeft, trimRight);
			var x = elementList[0].To<float>();
			var y = elementList[1].To<float>();
			var z = elementList[2].To<float>();
			var w = elementList[3].To<float>();
			return new Vector4(x, y, z, w);
		}

		public static Vector3 ToVector4OrDefault(string content, string toDefaultString = null,
			Vector4 defaultValue = default)
		{
			return ObjectUtil.Equals(content, toDefaultString) ? defaultValue : content.ToVector4();
		}

		public static Matrix4x4 ToMatrix4x4(string content, string split = StringConst.STRING_SLASH_N,
			string trimLeft = StringConst.STRING_LEFT_ROUND_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_ROUND_BRACKETS)
		{
			content = content.Replace(StringConst.STRING_SLASH_R, StringConst.STRING_EMPTY);
			var elementList = content.ToList<string>(split, trimLeft, trimRight);
			var row0 = elementList[0].ToVector4(StringConst.STRING_TAB);
			var row1 = elementList[1].ToVector4(StringConst.STRING_TAB);
			var row2 = elementList[2].ToVector4(StringConst.STRING_TAB);
			var row3 = elementList[3].ToVector4(StringConst.STRING_TAB);

			var column0 = new Vector4(row0.x, row1.x, row2.x, row3.x);
			var column1 = new Vector4(row0.y, row1.y, row2.y, row3.y);
			var column2 = new Vector4(row0.z, row1.z, row2.z, row3.z);
			var column3 = new Vector4(row0.w, row1.w, row2.w, row3.w);
			return new Matrix4x4(column0, column1, column2, column3);
		}

		public static Matrix4x4 ToMatrix4x4OrDefault(string content, string toDefaultString = null,
			Matrix4x4 defaultValue = default(Matrix4x4))
		{
			return ObjectUtil.Equals(content, toDefaultString) ? defaultValue : content.ToMatrix4x4();
		}


		public static float ToFloat(string content)
		{
			return float.Parse(content);
		}

		public static int ToInt(string content)
		{
			return int.Parse(content);
		}

		public static Quaternion ToQuaternion(string content, string split = StringConst.STRING_COMMA,
			string trimLeft = StringConst.STRING_LEFT_ROUND_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_ROUND_BRACKETS)
		{
			var elementList = content.ToList<string>(split, trimLeft, trimRight);
			if (elementList.Count == 4)
			{
				var x = elementList[0].To<float>();
				var y = elementList[1].To<float>();
				var z = elementList[2].To<float>();
				var w = elementList[3].To<float>();
				return new Quaternion(x, y, z, w);
			}
			else //欧拉角，三个系数
			{
				var x = elementList[0].To<float>();
				var y = elementList[1].To<float>();
				var z = elementList[2].To<float>();
				return Quaternion.Euler(x, y, z);
			}
		}

		

		public static List<T> ToList<T>(string content, string split = StringConst.STRING_COMMA,
			string trimLeft = StringConst.STRING_LEFT_SQUARE_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_SQUARE_BRACKETS)
		{
			var list = new List<T>();
			if (content.StartsWith(trimLeft))
				content = content.Substring(1);
			if (content.EndsWith(trimRight))
				content = content.Substring(0, content.Length - 1);
			if (content.IsNullOrWhiteSpace())
				return list;
			var elements = content.SplitIgnore(split);
			for (var i = 0; i < elements.Length; i++)
			{
				var element = elements[i];
				list.Add(element.To<T>());
			}

			return list;
		}

		public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(string content,
			string split = StringConst.STRING_COMMA,
			string subSeparator = StringConst.STRING_COLON, string trimLeft = StringConst.STRING_LEFT_CURLY_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_CURLY_BRACKETS,
			string elementIgnoreLeft = StringConst.STRING_REGEX_DOUBLE_QUOTES,
			string elementIgnoreRight = null)
		{
			var elementList = content.ToList<string>(split, trimLeft, trimRight);
			var dict = new Dictionary<TKey, TValue>();
			for (var i = 0; i < elementList.Count; i++)
			{
				var element = elementList[i];
				var ss = element.SplitIgnore(subSeparator, elementIgnoreLeft, elementIgnoreRight);
				var keyString = ss[0];
				var valueString = ss[1];

				dict[keyString.To<TKey>()] = valueString.To<TValue>();
			}

			return dict;
		}


		/// <summary>
		///   将s用pattern模式转换为DateTime，转换失败时返回默认值dv
		/// </summary>
		public static DateTime ToDateTime(string content, string pattern)
		{
			var provider = CultureInfo.InvariantCulture;
			return DateTime.ParseExact(content, pattern, provider);
		}

		/// <summary>
		///   形如:
		///   #FF00FF00或者FF00FF00  含Alpha
		///   或者#FF00FF或者FF00FF 不含Alpha
		/// </summary>
		public static Color ToColor(string content, string trimLeft = StringConst.STRING_NUMBER_SIGN)
		{
			content = content.TrimLeft(trimLeft);
			content = content.Replace(StringConst.STRING_0x, StringConst.STRING_EMPTY);
			ColorUtility.TryParseHtmlString(content, out var color);
			return color;
			//int value = int.Parse(s, System.Globalization.NumberStyles.HexNumber);

			//if (s.Length == 6)
			//{
			//    byte R = Convert.ToByte((value >> 16) & 255);
			//    byte G = Convert.ToByte((value >> 8) & 255);
			//    byte B = Convert.ToByte((value >> 0) & 255);
			//    return new Color(R / 255f, G / 255f, B / 255f);
			//}
			//else
			//{
			//    byte R = Convert.ToByte((value >> 24) & 255);
			//    byte G = Convert.ToByte((value >> 16) & 255);
			//    byte B = Convert.ToByte((value >> 8) & 255);
			//    byte A = Convert.ToByte((value >> 0) & 255);
			//    return new Color(R / 255f, G / 255f, B / 255f,A/255f);
			//}
		}

		public static Color ToColorOrDefault(string content, string toDefaultString = null,
			Color defaultValue = default)
		{
			return ObjectUtil.Equals(content, toDefaultString) ? defaultValue : content.ToColor();
		}

		#endregion

		#region Enum

		public static T ToEnum<T>(string content)
		{
			return (T)Enum.Parse(typeof(T), content);
		}

		public static bool IsEnum<T>(string content)
		{
			return Enum.IsDefined(typeof(T), content);
		}

		#endregion

		#region ToBytes

		/// <summary>
		///   将s转换为bytes（bytes长度为len[当len比s转换出来的bytes更少的时候，用更少那个]）
		/// </summary>
		public static byte[] ToBytes(string self, int len, Encoding encoding = null)
		{
			var bb = new byte[len];
			ByteUtil.ZeroBytes(bb);
			if (self.IsNullOrWhiteSpace())
				return null;
			var tBb = self.GetBytes(encoding);
			ByteUtil.BytesCopy(tBb, bb, Math.Min(len, tBb.Length));
			return bb;
		}

		public static byte[] ToBytes(string self, Encoding encoding = null)
		{
			if (self.IsNullOrWhiteSpace())
				return null;
			var bb = self.GetBytes(encoding);
			return bb;
		}

		#endregion

		#region Split

		/// <summary>
		///   将s按sliceLen长度进行分割
		/// </summary>
		public static string[] Split(string content, int sliceLength)
		{
			if (content == null)
				return null;
			if (sliceLength <= 0 || content.Length <= sliceLength)
				return new[] { content };
			var list = new List<string>();
			for (var i = 0; i < content.Length / sliceLength; i++)
				list.Add(content.Substring(i * sliceLength, (i + 1) * sliceLength));
			if (content.Length % sliceLength != 0)
				list.Add(content.Substring(content.Length - content.Length % sliceLength));
			return list.ToArray();
		}

		/// <summary>
		///   将s按字符串sep分隔符分割
		/// </summary>
		public static string[] Split(string content, string sep)
		{
			if (content == null)
				return null;
			if (string.IsNullOrEmpty(sep))
				return new[] { content };

			var sepLength = sep.Length;
			var sepIndex = content.IndexOf(sep, StringComparison.Ordinal);
			if (sepIndex == -1) return new[] { content };
			var list = new List<string> { content.Substring(0, sepIndex) };
			var subIndex = sepIndex + sepLength;
			while ((sepIndex = content.IndexOf(sep, subIndex)) != -1)
			{
				list.Add(content.Substring(subIndex, sepIndex - subIndex));
				subIndex = sepIndex + sepLength;
			}

			list.Add(content.Substring(subIndex));


			return list.ToArray();
		}

		#endregion

		#region Warp

		/// <summary>
		///   将对象列表的每一项成员用左右字符串括起来
		/// </summary>
		public static string Wrap(string self, string leftWrap, string rightWrap)
		{
			return leftWrap + self + rightWrap;
		}

		public static string WarpBoth(string self, string wrap)
		{
			return self.Wrap(wrap, wrap);
		}

		public static string WarpWithDoubleQuotes(string self) //双引号
		{
			return self.WarpBoth(StringConst.STRING_DOUBLE_QUOTES);
		}

		//pos在left之后第一个字母的index
		public static int WrapEndIndex(string s, string left, string right, int pos = 0)
		{
			int stack = 0;
			while (pos < s.Length)
			{
				if (s.IndexOf(left, pos) == pos)
				{
					stack++;
					pos = pos + left.Length;
				}
				else if (s.IndexOf(right, pos) == pos)
				{
					if (stack == 0)
						return pos;
					stack--;
					pos = pos + right.Length;
				}
				else
					pos++;
			}

			return -1;
		}

		#endregion

		#region Trim

		/// <summary>
		///   整理字符串,去掉两边的指定字符（trimLeftChars，trimRightChars）
		/// </summary>
		public static string Trim(string content, string trimLeft, string trimRight,
			bool isTrimAll = true)
		{
			if (!trimLeft.IsNullOrWhiteSpace())
				while (true)
				{
					var beginIndex = content.IndexOf(trimLeft);
					if (beginIndex == 0)
					{
						content = content.Substring(trimLeft.Length);
						if (!isTrimAll)
							break;
					}
					else
						break;
				}

			if (trimRight.IsNullOrWhiteSpace()) return content;
			while (true)
			{
				var beginIndex = content.LastIndexOf(trimLeft);
				if (beginIndex != -1 && beginIndex + trimLeft.Length == content.Length)
				{
					content = content.Substring(0, beginIndex);
					if (!isTrimAll)
						break;
				}
				else
					break;
			}

			return content;
		}

		/// <summary>
		///   整理字符串,去掉两边的指定字符trimChars
		/// </summary>
		public static string Trim(string content, string trimString)
		{
			return Trim(content, trimString, trimString);
		}

		/// <summary>
		///   整理字符串,去掉左边的指定字符trimChars
		/// </summary>
		public static string TrimLeft(string content, string trimString)
		{
			return Trim(content, trimString, StringConst.STRING_EMPTY);
		}

		/// <summary>
		///   整理字符串,去掉右边的指定字符trimChars
		/// </summary>
		public static string TrimRight(string content, string trimString)
		{
			return Trim(content, StringConst.STRING_EMPTY, trimString);
		}

		#endregion

		#region 大小写第一个字母

		public static string UpperFirstLetter(string content)
		{
			return content.Substring(0, 1).ToUpper() + content.Substring(1);
		}

		public static string LowerFirstLetter(string content)
		{
			return content.Substring(0, 1).ToLower() + content.Substring(1);
		}

		public static bool IsFirstLetterUpper(string content)
		{
			return content[0].IsUpper();
		}

		public static bool IsFirstLetterLower(string content)
		{
			return content[0].IsLower();
		}

		#endregion

		#region FillHead/FillEnd

		/// <summary>
		///   前补齐字符串.若src长度不足len，则在src前面用c补足len长度，否则直接返回src
		/// </summary>
		public static string FillHead(string content, int len, char c)
		{
			if (content.Length >= len)
				return content;
			var stringBuilder = new StringBuilder();
			for (var i = 0; i < len - content.Length; i++)
				stringBuilder.Append(c);
			stringBuilder.Append(content);
			return stringBuilder.ToString();
		}

		/// <summary>
		///   后补齐字符串.若src长度不足len，则在src后面用c补足len长度，否则直接返回src
		/// </summary>
		public static string FillEnd(string content, int len, char c)
		{
			if (content.Length >= len)
				return content;
			var stringBuilder = new StringBuilder();
			stringBuilder.Append(content);
			for (var i = 0; i < len - content.Length; i++)
				stringBuilder.Append(c);
			return stringBuilder.ToString();
		}

		#endregion

		#region GetDigit

		/// <summary>
		///   获取src的第一个数字（可能由多个字符组成）
		///   如：123df58f，则返回"123";abc123则返回""
		/// </summary>
		public static string GetDigitStart(string content)
		{
			content = content.Trim();
			var stringBuilder = new StringBuilder();
			for (var i = 0; i < content.Length; i++)
			{
				var t = content[i];
				if (!char.IsDigit(t))
					break;
				stringBuilder.Append(t);
			}

			return stringBuilder.ToString();
		}

		/// <summary>
		///   获取src的第一个数字（可能由多个字符组成）
		///   如：123df58f，则返回123;abc123则返回dv
		/// </summary>
		public static long GetDigitStart(string content, long defaultValue)
		{
			return GetDigitStart(content).ToLongOrToDefault(defaultValue);
		}

		/// <summary>
		///   由末尾向前，获取src的第一个数字（可能由多个字符组成）
		///   如：fg125abc456，则得出来是"456";fg125abc456fd，则得出来是""
		/// </summary>
		public static string GetDigitEnd(string content)
		{
			content = content.Trim();
			var stringBuilder = new StringBuilder();
			for (var i = content.Length - 1; i >= 0; i--)
			{
				if (!char.IsDigit(content[i]))
					break;
				stringBuilder.Insert(0, content[i]);
			}

			return stringBuilder.ToString();
		}

		/// <summary>
		///   由末尾向前，获取src的第一个数字（可能由多个字符组成）
		///   如：fg125abc456，则得出来是456;fg125abc456fd，则得出来是dv
		/// </summary>
		public static long GetDigitEnd(string content, long defaultValue)
		{
			return GetDigitEnd(content).ToLongOrToDefault(defaultValue);
		}


		public static string ToGuid(string content, object o)
		{
			return content + o.GetHashCode();
		}

		public static string GetId(string content)
		{
			var index = content.IndexOf(IdConst.RID_INFIX);
			return index == -1 ? content : content.Substring(0, index);
		}

		public static string GetUITextOfColorAndFontSize(string content, Color? color = null, int? fontSize = null)
		{
			if (color.HasValue)
			{
				content = content.Replace(StringConst.STRING_REGEX_TEXT_COLOR_WARP_END, StringConst.STRING_EMPTY);
				content = Regex.Replace(content, StringConst.STRING_REGEX_TEXT_COLOR_WARP_START, StringConst.STRING_EMPTY);
				content = string.Format(StringConst.STRING_FORMAT_TEXT_COLOR, color.Value.ToHtmlStringRGBA(), content);
			}

			if (fontSize.HasValue)
			{
				content = content.Replace(StringConst.STRING_REGEX_TEXT_FONT_SIZE_WARP_END, StringConst.STRING_EMPTY);
				content = Regex.Replace(content, StringConst.STRING_REGEX_TEXT_FONT_SIZE_WARP_START, StringConst.STRING_EMPTY);
				content = string.Format(StringConst.STRING_FORMAT_TEXT_FONT_SIZE, fontSize.Value, content);
			}

			return content;
		}

		public static string ReplaceAll(string s, string pattern, Func<string, string> replaceFunc = null)
		{
			var stringBuilder = new StringBuilder();
			MatchCollection matchCollection = Regex.Matches(s, pattern);
			int lastEndIndex = -1;
			for (int i = 0; i < matchCollection.Count; i++)
			{
				int startIndex = matchCollection[i].Index;
				string value = matchCollection[i].Value;
				int endIndex = startIndex + value.Length - 1;
				value = replaceFunc == null ? value : replaceFunc(value);
				stringBuilder.Append(s.Substring(lastEndIndex + 1, startIndex - (lastEndIndex + 1)));
				stringBuilder.Append(value);
				lastEndIndex = endIndex;
			}

			if (lastEndIndex != s.Length - 1)
				stringBuilder.Append(s.Substring(lastEndIndex + 1));
			return stringBuilder.ToString();
		}

		#endregion

		#region 私有函数

		private static long _X2H(string value, int fromBase)
		{
			value = value.Trim();
			if (string.IsNullOrEmpty(value))
				return 0L;
			var constChars = CharConst.DIGITS_AND_CHARS_BIG;
			var digits = new string(constChars, 0, fromBase);
			long result = 0;
			value = value.ToUpper(); // 2


			for (var i = 0; i < value.Length; i++)
			{
				if (!digits.Contains(value[i].ToString()))
					throw new ArgumentException(string.Format("The argument \"{0}\" is not in {1} system.", value[i],
						fromBase));
				try
				{
					result += (long)Math.Pow(fromBase, i) *
							  _GetCharIndex(constChars, value[value.Length - i - 1]); //   2
				}
				catch
				{
					throw new OverflowException("运算溢出.");
				}
			}

			return result;
		}

		private static int _GetCharIndex(char[] chars, char value)
		{
			for (var i = 0; i < chars.Length; i++)
				if (chars[i] == value)
					return i;
			return 0;
		}

		#endregion
	}
}