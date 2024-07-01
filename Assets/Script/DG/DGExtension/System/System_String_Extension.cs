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
	public static class System_String_Extension
	{
		/// <summary>
		/// fullFilePath-pathRoot
		/// </summary>
		/// <param name="fullFilePath"></param>
		/// <param name="rootPath"></param>
		/// <returns></returns>
		public static string WithoutRootPath(this string fullFilePath, string rootPath,
			char slash = CharConst.CHAR_SLASH)
		{
			return StringUtil.WithoutRootPath(fullFilePath, rootPath, slash);
		}

		/// <summary>
		/// pathRoot+relativeFilePath
		/// </summary>
		/// <param name="relativeFilePath"></param>
		/// <param name="rootPath"></param>
		/// <returns></returns>
		public static string WithRootPath(this string relativeFilePath, string rootPath,
			char slash = CharConst.CHAR_SLASH)
		{
			return StringUtil.WithRootPath(relativeFilePath, rootPath, slash);
		}


		public static string LocalURL(this string path)
		{
			return StringUtil.LocalURL(path);
		}

		public static GUIContent ToGUIContent(this string self)
		{
			return StringUtil.ToGUIContent(self);
		}

		#region Equals

		public static bool EqualsIgnoreCase(this string self, string s2)
		{
			return StringUtil.EqualsIgnoreCase(self, s2);
		}

		#endregion

		public static bool IsNumber(this string self)
		{
			return StringUtil.IsNumber(self);
		}

		#region 判断是否为null或Empty或WhiteSpace

		/// <summary>
		///   判断是否为null或Empty
		/// </summary>
		public static bool IsNullOrWhiteSpace(this string self)
		{
			return StringUtil.IsNullOrWhiteSpace(self);
		}

		public static bool IsNullOrEmpty(this string self)
		{
			return StringUtil.IsNullOrEmpty(self);
		}

		#endregion

		#region 加密

		public static string Encrypt(this string self)
		{
			return StringUtil.Encrypt(self);
		}

		#endregion

		/// <summary>
		///   将s重复n次返回
		/// </summary>
		public static string Join(this string self, int n)
		{
			return StringUtil.Join(self, n);
		}

//		public static void RemoveFiles(this string self)
//		{
//			StringUtil.RemoveFiles(self);
//		}

//		public static string Random(this string self, int count, bool isUnique)
//		{
//			return StringUtil.Random(self, count, isUnique);
//		}

		public static int GetSubStringCount(this string self, string subString)
		{
			return StringUtil.GetSubStringCount(self, subString);
		}

#if UNITY_EDITOR
		/// <summary>
		///   "xx11" comes after "xx2".
		/// </summary>
		/// <param name="self"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static int CompareTo2(this string self, string other)
		{
			return StringUtil.CompareTo2(self, other);
		}
#endif
		//跟上面这个EditorUtility.NaturalCompare效果是一样的
		public static int AlphanumCompareTo(this string self, string other)
		{
			return StringUtil.AlphanumCompareTo(self, other);
		}

		public static string FileName(this string filePath, string lastSeparator = StringConst.STRING_SLASH)
		{
			return StringUtil.FileName(filePath, lastSeparator);
		}

		public static string DirPath(this string filePath, string lastSeparator = StringConst.STRING_SLASH)
		{
			return StringUtil.DirPath(filePath, lastSeparator);
		}


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

		public static bool IsNetURL(this string self)
		{
			return StringUtil.IsNetURL(self);
		}

		public static string WWWURLHandle(this string self)
		{
			return StringUtil.WWWURLHandle(self);
		}

		public static string ReplaceDirectorySeparatorChar(this string self, char separator = CharConst.CHAR_SLASH)
		{
			return StringUtil.ReplaceDirectorySeparatorChar(self, separator);
		}

		public static string WithoutAllSuffix(this string self)
		{
			return StringUtil.WithoutAllSuffix(self);
		}

		public static string WithoutSuffix(this string self)
		{
			return StringUtil.WithoutSuffix(self);
		}

		public static string ToLuaRequirePath(this string self)
		{
			return StringUtil.ToLuaRequirePath(self);
		}


//		public static bool ContainTags(this string self, params string[] checkTags)
//		{
//			return StringUtil.ContainTags(self, checkTags);
//		}

		public static string GetMainAssetPath(this string self)
		{
			return StringUtil.GetMainAssetPath(self);
		}

		public static string GetSubAssetPath(this string self)
		{
			return StringUtil.GetSubAssetPath(self);
		}

		public static AssetPathInfo GetAssetPathInfo(this string self)
		{
			return StringUtil.GetAssetPathInfo(self);
		}

		public static string GetPreString(this string self, string splitContent)
		{
			return StringUtil.GetPreString(self, splitContent);
		}

		public static string GetPostString(this string self, string splitContent)
		{
			return StringUtil.GetPostString(self, splitContent);
		}

		public static string Format(this string format, params object[] args)
		{
			return StringUtil.Format(format, args);
		}

		public static (string mainPart, string cfgPart) GetLineAutoGenLineInfoParts(this string mainPart, string cfgPartStartsWith)
		{
			return StringUtil.GetLineAutoGenLineInfoParts(mainPart, cfgPartStartsWith);
		}

//		public static AutoGenLineInfo ToAutoGenLineInfo(this string mainPart, string cfgPartStartsWith = null, string uniqueKey = null, bool isDeleteIfNotExist = false)
//		{
//			return StringUtil.ToAutoGenLineInfo(mainPart, cfgPartStartsWith, uniqueKey, isDeleteIfNotExist);
//		}
//
//		public static AutoGenLineInfo ParseAutoGenLineInfo(this string line, string cfgPartStartsWith)
//		{
//			return StringUtil.ParseAutoGenLineInfo(line, cfgPartStartsWith);
//		}
//
//		public static AutoGenLineCfgInfo ParseAutoGenLineCfgInfo(this string cfgPart, string cfgPartStartsWith)
//		{
//			return StringUtil.ParseAutoGenLineCfgInfo(cfgPart, cfgPartStartsWith);
//		}

		#region RichText

		public static void SetRichTextColor(this string self, Color color)
		{
			StringUtil.SetRichTextColor(self, color);
		}

		public static void SetRichTextIsBold(this string self)
		{
			StringUtil.SetRichTextIsBold(self);
		}

		public static void SetIsItalic(this string self)
		{
			StringUtil.SetIsItalic(self);
		}

		public static void SetRichTextFontSize(this string self, int fontSize)
		{
			StringUtil.SetRichTextFontSize(self, fontSize);
		}

		#endregion

		#region 编码

		/// <summary>
		///   字符串的bytes，默认编码是当前系统编码
		/// </summary>
		public static byte[] GetBytes(this string self, Encoding encoding = null)
		{
			return StringUtil.GetBytes(self, encoding);
		}

		/// <summary>
		///   从字符串的X进制编码的字符串转为十进制的数字（long类型）
		/// </summary>
		public static long ToLong(this string self, int fromBase)
		{
			return StringUtil.ToLong(self, fromBase);
		}

		#endregion


		#region 各种转换 ToXX

		public static Vector2 ToVector2(this string self, string split = StringConst.STRING_COMMA,
			string trimLeft = StringConst.STRING_LEFT_ROUND_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_ROUND_BRACKETS)
		{
			return StringUtil.ToVector2(self, split, trimLeft, trimRight);
		}

		public static Vector2 ToVector2OrDefault(this string self, string toDefaultString = null,
			Vector2 defaultValue = default)
		{
			return StringUtil.ToVector2OrDefault(self, toDefaultString, defaultValue);
		}

		public static Vector3 ToVector3(this string s, string split = StringConst.STRING_COMMA,
			string trimLeft = StringConst.STRING_LEFT_ROUND_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_ROUND_BRACKETS)
		{
			return StringUtil.ToVector3(s, split, trimLeft, trimRight);
		}

		public static Vector2Int ToVector2IntOrDefault(this string self, string toDefaultString = null,
			Vector2Int defaultValue = default)
		{
			return StringUtil.ToVector2IntOrDefault(self, toDefaultString, defaultValue);
		}


		public static Vector3 ToVector3OrDefault(this string self, string toDefaultString = null,
			Vector3 defaultValue = default)
		{
			return StringUtil.ToVector3OrDefault(self, toDefaultString, defaultValue);
		}

		public static Vector3Int ToVector3IntOrDefault(this string self, string toDefaultString = null,
			Vector3Int defaultValue = default)
		{
			return StringUtil.ToVector3IntOrDefault(self, toDefaultString, defaultValue);
		}

		public static Vector4 ToVector4(this string self, string split = StringConst.STRING_COMMA,
			string trimLeft = StringConst.STRING_LEFT_ROUND_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_ROUND_BRACKETS)
		{
			return StringUtil.ToVector4(self, split, trimLeft, trimRight);
		}

		public static Vector3 ToVector4OrDefault(this string self, string toDefaultString = null,
			Vector4 defaultValue = default)
		{
			return StringUtil.ToVector4OrDefault(self, toDefaultString, defaultValue);
		}

		public static Matrix4x4 ToMatrix4x4(this string self, string split = StringConst.STRING_SLASH_N,
			string trimLeft = StringConst.STRING_LEFT_ROUND_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_ROUND_BRACKETS)
		{
			return StringUtil.ToMatrix4x4(self, trimLeft, trimRight);
		}

		public static Matrix4x4 ToMatrix4x4OrDefault(this string self, string toDefaultString = null,
			Matrix4x4 defaultValue = default(Matrix4x4))
		{
			return StringUtil.ToMatrix4x4OrDefault(self, toDefaultString, defaultValue);
		}


		public static float ToFloat(this string self)
		{
			return StringUtil.ToFloat(self);
		}

		public static int ToInt(this string self)
		{
		return StringUtil.ToInt(self);
			}

		public static Quaternion ToQuaternion(this string self, string split = StringConst.STRING_COMMA,
			string trimLeft = StringConst.STRING_LEFT_ROUND_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_ROUND_BRACKETS)
		{
			return StringUtil.ToQuaternion(self, split, trimLeft, trimRight);
		}

		/// <summary>
		/// split 忽略ignore_left,ignore_right包裹的东西
		/// </summary>
		/// <param name="self"></param>
		/// <param name="split"></param>
		/// <param name="ignoreLeft">注意转移字符，需要加上\,例如忽略",则需要输入\\\"</param>
		/// <param name="ignoreRight"></param>
		/// <returns></returns>
		public static string[] SplitIgnore(this string self, string split = StringConst.STRING_COMMA,
			string ignoreLeft = StringConst.STRING_REGEX_DOUBLE_QUOTES,
			string ignoreRight = null)
		{
			return StringUtil.SplitIgnore(self, split, ignoreLeft, ignoreRight);
		}

		public static List<T> ToList<T>(this string self, string split = StringConst.STRING_COMMA,
			string trimLeft = StringConst.STRING_LEFT_SQUARE_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_SQUARE_BRACKETS)
		{
			return StringUtil.ToList<T>(self, split, trimLeft, trimRight);
		}

		public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this string self,
			string split = StringConst.STRING_COMMA,
			string subSeparator = StringConst.STRING_COLON, string trimLeft = StringConst.STRING_LEFT_CURLY_BRACKETS,
			string trimRight = StringConst.STRING_RIGHT_CURLY_BRACKETS,
			string elementIgnoreLeft = StringConst.STRING_REGEX_DOUBLE_QUOTES,
			string elementIgnoreRight = null)
		{
			return StringUtil.ToDictionary<TKey, TValue>(self, split, subSeparator, trimLeft, trimRight, elementIgnoreLeft, elementIgnoreRight);
		}


		/// <summary>
		///   将s用pattern模式转换为DateTime，转换失败时返回默认值dv
		/// </summary>
		public static DateTime ToDateTime(this string self, string pattern)
		{
			return StringUtil.ToDateTime(self, pattern);
		}

		/// <summary>
		///   形如:
		///   #FF00FF00或者FF00FF00  含Alpha
		///   或者#FF00FF或者FF00FF 不含Alpha
		/// </summary>
		public static Color ToColor(this string self, string trimLeft = StringConst.STRING_NUMBER_SIGN)
		{
			return StringUtil.ToColor(self, trimLeft);
		}

		public static Color ToColorOrDefault(this string self, string toDefaultString = null,
			Color defaultValue = default)
		{
			return StringUtil.ToColorOrDefault(self, toDefaultString, defaultValue);
		}

		#endregion

		#region Enum

		public static T ToEnum<T>(this string self)
		{
			return StringUtil.ToEnum<T>(self);
		}

		public static bool IsEnum<T>(this string self)
		{
			return StringUtil.IsEnum<T>(self);
		}

		#endregion

		#region ToBytes

		/// <summary>
		///   将s转换为bytes（bytes长度为len[当len比s转换出来的bytes更少的时候，用更少那个]）
		/// </summary>
		public static byte[] ToBytes(this string self, int len, Encoding encoding = null)
		{
			return StringUtil.ToBytes(self, len, encoding);
		}

		public static byte[] ToBytes(this string self, Encoding encoding = null)
		{
			return StringUtil.ToBytes(self, encoding);
		}

		#endregion

		#region Split

		/// <summary>
		///   将s按sliceLen长度进行分割
		/// </summary>
		public static string[] Split(this string self, int sliceLength)
		{
			return StringUtil.Split(self, sliceLength);
		}

		/// <summary>
		///   将s按字符串sep分隔符分割
		/// </summary>
		public static string[] Split(this string self, string sep)
		{
			return StringUtil.Split(self, sep);
		}

		#endregion

		#region Warp

		/// <summary>
		///   将对象列表的每一项成员用左右字符串括起来
		/// </summary>
		public static string Wrap(this string self, string leftWrap, string rightWrap)
		{
			return StringUtil.Wrap(self, leftWrap, rightWrap);
		}

		public static string WarpBoth(this string self, string wrap)
		{
			return StringUtil.WarpBoth(self, wrap);
		}

		public static string WarpWithDoubleQuotes(this string self) //双引号
		{
			return StringUtil.WarpWithDoubleQuotes(self);
		}

		//pos在left之后第一个字母的index
		public static int WrapEndIndex(this string s, string left, string right, int pos = 0)
		{
			return StringUtil.WrapEndIndex(s, left, right, pos);
		}

		#endregion

		#region Trim

		/// <summary>
		///   整理字符串,去掉两边的指定字符（trimLeftChars，trimRightChars）
		/// </summary>
		public static string Trim(this string self, string trimLeft, string trimRight,
			bool isTrimAll = true)
		{
			return StringUtil.Trim(self, trimLeft, trimRight, isTrimAll);
		}

		/// <summary>
		///   整理字符串,去掉两边的指定字符trimChars
		/// </summary>
		public static string Trim(this string self, string trimString)
		{
			return StringUtil.Trim(self, trimString);
		}

		/// <summary>
		///   整理字符串,去掉左边的指定字符trimChars
		/// </summary>
		public static string TrimLeft(this string self, string trimString)
		{
			return StringUtil.TrimLeft(self, trimString);
		}

		/// <summary>
		///   整理字符串,去掉右边的指定字符trimChars
		/// </summary>
		public static string TrimRight(this string self, string trimString)
		{
			return StringUtil.TrimRight(self, trimString);
		}

		#endregion

		#region 大小写第一个字母

		public static string UpperFirstLetter(this string self)
		{
			return StringUtil.UpperFirstLetter(self);
		}

		public static string LowerFirstLetter(this string self)
		{
			return StringUtil.LowerFirstLetter(self);
		}

		public static bool IsFirstLetterUpper(this string self)
		{
			return StringUtil.IsFirstLetterUpper(self);
		}

		public static bool IsFirstLetterLower(this string self)
		{
			return StringUtil.IsFirstLetterLower(self);
		}

		#endregion

		#region FillHead/FillEnd

		/// <summary>
		///   前补齐字符串.若src长度不足len，则在src前面用c补足len长度，否则直接返回src
		/// </summary>
		public static string FillHead(this string self, int len, char c)
		{
			return StringUtil.FillHead(self, len, c);
		}

		/// <summary>
		///   后补齐字符串.若src长度不足len，则在src后面用c补足len长度，否则直接返回src
		/// </summary>
		public static string FillEnd(this string self, int len, char c)
		{
			return StringUtil.FillEnd(self, len, c);
		}

		#endregion

		#region GetDigit

		/// <summary>
		///   获取src的第一个数字（可能由多个字符组成）
		///   如：123df58f，则返回"123";abc123则返回""
		/// </summary>
		public static string GetDigitStart(this string self)
		{
			return StringUtil.GetDigitStart(self);
		}

		/// <summary>
		///   获取src的第一个数字（可能由多个字符组成）
		///   如：123df58f，则返回123;abc123则返回dv
		/// </summary>
		public static long GetDigitStart(this string self, long defaultValue)
		{
			return StringUtil.GetDigitStart(self, defaultValue);
		}

		/// <summary>
		///   由末尾向前，获取src的第一个数字（可能由多个字符组成）
		///   如：fg125abc456，则得出来是"456";fg125abc456fd，则得出来是""
		/// </summary>
		public static string GetDigitEnd(this string self)
		{
			return StringUtil.GetDigitEnd(self);
		}

		/// <summary>
		///   由末尾向前，获取src的第一个数字（可能由多个字符组成）
		///   如：fg125abc456，则得出来是456;fg125abc456fd，则得出来是dv
		/// </summary>
		public static long GetDigitEnd(this string self, long defaultValue)
		{
			return StringUtil.GetDigitEnd(self, defaultValue);
		}


		public static string ToGuid(this string self, object o)
		{
			return StringUtil.ToGuid(self, o);
		}

		public static string GetId(this string self)
		{
			return StringUtil.GetId(self);
		}

		public static string GetUITextOfColorAndFontSize(this string self, Color? color = null, int? fontSize = null)
		{
			return StringUtil.GetUITextOfColorAndFontSize(self, color, fontSize);
		}

		public static string ReplaceAll(this string s, string pattern, Func<string, string> replaceFunc = null)
		{
			return StringUtil.ReplaceAll(s, pattern, replaceFunc);
		}

		#endregion


	}
}