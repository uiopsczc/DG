using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DG.Tweening;

namespace DG
{
	public static class ObjectUtil
	{
		/// <summary>
		/// o1是否和o2相等
		/// </summary>
		public new static bool Equals(object o1, object o2)
		{
			return o1?.Equals(o2) ?? o2 == null;
		}

		public static bool Equals<T>(T o1, T o2)
		{
			return o1?.Equals(o2) ?? o2 == null;
		}

	    public static bool EqualsArray<T>(T[] o1, T[] o2)
	    {
	        if (o1 == null && o2 == null)
	            return true;
	        if (o1 == null && o2 != null)
	            return false;
	        if (o1 != null && o2 == null)
	            return false;

	        if (o1.Length == o2.Length)
	        {
	            for (int i = 0; i < o1.Length; i++)
	            {
	                if (!Equals(o1[i], o2[i]))
	                    return false;
	            }
	            return true;
	        }

	        return false;
        }

	    public static bool EqualsList<T>(List<T> o1, List<T> o2)
	    {
	        if (o1 == null && o2 == null)
	            return true;
	        if (o1 == null && o2 != null)
	            return false;
	        if (o1 != null && o2 == null)
	            return false;

	        if (o1.Count == o2.Count)
	        {
	            for (int i = 0; i < o1.Count; i++)
	            {
	                if (!Equals(o1[i], o2[i]))
	                    return false;
	            }
	            return true;
	        }

	        return false;
	    }

        /// <summary>
        /// o1和o2比较大小
        /// </summary>
        public static int Compares(object o1, object o2)
		{
			if (o1 == o2)
				return 0;
			if (o1 != null && o2 == null)
				return 1;
			switch (o1)
			{
				case null when o2 != null:
					return -1;
				case IComparable comparable:
					return comparable.CompareTo(o2);
			}

			return o2 is IComparable comparable1 ? comparable1.CompareTo(o1) : o1.ToString().CompareTo(o2.ToString());
		}


		public static int GetHashCode(params object[] objs)
		{
			int result = int.MinValue;
			bool isFoundFirstNotNullObject = false;
			for (var i = 0; i < objs.Length; i++)
			{
				var obj = objs[i];
				if (obj == null) continue;
				if (isFoundFirstNotNullObject)
					result ^= obj.GetHashCode();
				else
				{
					result = obj.GetHashCode();
					isFoundFirstNotNullObject = true;
				}
			}

			return result;
		}

		public static int GetHashCode<T>(params T[] objs)
		{
			int result = int.MinValue;
			bool isFoundFirstNotNullObject = false;
			for (var i = 0; i < objs.Length; i++)
			{
				var obj = objs[i];
				if (obj == null) continue;
				if (isFoundFirstNotNullObject)
					result ^= obj.GetHashCode();
				else
				{
					result = obj.GetHashCode();
					isFoundFirstNotNullObject = true;
				}
			}

			return result;
		}

		/// <summary>
		/// 交换两个object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static void Swap<T>(ref T a, ref T b)
		{
			T c = b;
			b = a;
			a = c;
		}

		public static string ToString(params object[] objs)
		{
			var stringBuilder = new StringBuilder();
			for (int i = 0; i < objs.Length; i++)
			{
				var obj = objs[i];
				if (i == objs.Length - 1)
					stringBuilder.Append(obj);
				else
					stringBuilder.Append(obj + StringConst.STRING_SPACE);
			}

			return stringBuilder.ToString();
		}

		public static string DGToString(params object[] objs)
		{
			var stringBuilder = new StringBuilder();
			for (int i = 0; i < objs.Length; i++)
			{
				var obj = objs[i];
				if (i == objs.Length - 1)
					stringBuilder.Append(obj.DGToString());
				else
					stringBuilder.Append(obj.DGToString() + StringConst.STRING_SPACE);
			}

			return stringBuilder.ToString();
		}

		public static bool IsValidObject(object obj)
		{
			return obj != null && !obj.Equals(null);
		}

		#region DOTween

		public static DOTweenId DOTweenId(object obj)
		{
			return DOTweenUtil.GetDOTweenId(obj);
		}

		public static void DOKillByDOTweenId(object obj)
		{
			DOTween.Kill(obj.DOTweenId());
		}

		#endregion

		#region ToString2 ToLinkedHashtable2

		/// <summary>
		///   将Object中的真实内容以字符串的形式输出
		/// </summary>
		public static string DGToString(object obj, bool isFillStringWithDoubleQuote = false)
		{
			return DGToStringExtension.DGToString(obj, isFillStringWithDoubleQuote);
		}


		public static object ToLinkedHashtable2(object obj)
		{
			return IsToLinkedHashtable2.ToLinkedHashtable2(obj);
		}

		#endregion

		#region 类型判断

		/// <summary>
		/// 是否是IsIDictionary
		/// </summary>
		public static bool IsDictionary<T, V>(object boj)
		{
			return boj is IDictionary<T, V>;
		}

		/// <summary>
		/// 是否是数组
		/// </summary>
		public static bool IsArray(object obj)
		{
			return (obj != null && obj.GetType().IsArray);
		}

		/// <summary>
		/// 是否是IList
		/// </summary>
		public static bool IsList<T>(object obj)
		{
			return obj is IList<T>;
		}

		/// <summary>
		/// 是否是String
		/// </summary>
		public static bool IsString(object obj)
		{
			return (obj is string);
		}

		/// <summary>
		/// 是否是boolean
		/// </summary>
		public static bool IsBool(object obj)
		{
			return (obj is bool);
		}

		/// <summary>
		/// 是否是数字
		/// </summary>
		public static bool IsNumber(object obj)
		{
			return (IsByte(obj) || obj.IsShort() || obj.IsInt() || obj.IsLong() || obj.IsFloat() ||
					obj.IsDouble());
		}

		/// <summary>
		/// 是否是整数类型，即非小数类型
		/// </summary>
		public static bool IsIntegral(object obj)
		{
			return (IsByte(obj) || obj.IsShort() || obj.IsInt() || obj.IsLong());
		}

		/// <summary>
		/// 是否是小数类型，即非整数类型
		/// </summary>
		public static bool IsFloating(object obj)
		{
			return (obj.IsFloat() || obj.IsDouble());
		}

		/// <summary>
		/// 是否是byte
		/// </summary>
		public static bool IsByte(object obj)
		{
			return (obj is byte);
		}

		/// <summary>
		/// 是否是short
		/// </summary>
		public static bool IsShort(object obj)
		{
			return (obj is short);
		}

		/// <summary>
		/// 是否是char
		/// </summary>
		public static bool IsChar(object obj)
		{
			return (obj is char);
		}

		/// <summary>
		/// 是否是int
		/// </summary>
		public static bool IsInt(object obj)
		{
			return (obj is int);
		}

		/// <summary>
		/// 是否是long类型
		/// </summary>
		public static bool IsLong(object obj)
		{
			return (obj is long);
		}

		/// <summary>
		/// 是否是float类型
		/// </summary>
		public static bool IsFloat(object obj)
		{
			return (obj is float);
		}

		/// <summary>
		/// 是否是double类型
		/// </summary>
		public static bool IsDouble(object obj)
		{
			return (obj is double);
		}

		/// <summary>
		/// 是否是DateTime
		/// </summary>
		public static bool IsDateTime(object obj)
		{
			return (obj is DateTime);
		}

		/// <summary>
		/// 是否是bytes
		/// </summary>
		public static bool IsBytes(object obj)
		{
			return (obj is byte[]);
		}

		/// <summary>
		/// 是否是chars
		/// </summary>
		public static bool IsChars(object self)
		{
			return (self is char[]);
		}

		/// <summary>
		/// 是否是Class
		/// </summary>
		public static bool IsClass(object obj)
		{
			return (obj.GetType().IsClass);
		}

		/// <summary>
		/// 是否是方法
		/// </summary>
		public static bool IsMethod(object obj)
		{
			return (obj is MethodBase);
		}

		#endregion

		#region 各种ToXX

		/// <summary>
		/// 将o转化为boolean，失败时返回dv
		/// </summary>
		public static bool ToBoolOrToDefault(object obj, bool defaultValue = false)
		{
			if (obj == null)
				return defaultValue;
			if (obj.IsBool())
				return (bool)obj;
			if (obj.IsNumber())
				return ToIntOrToDefault(obj, 0) != 0;
			if (obj.IsString())
			{
				if (StringConst.STRING_TRUE.Equals(((string)obj).ToLower()))
					return true;
				if (StringConst.STRING_FALSE.Equals(((string)obj).ToUpper()))
					return false;
				try
				{
					return double.Parse((string)obj) != 0.0D;
				}
				catch
				{
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为byte，失败时返回dv
		/// </summary>
		public static byte ToByteOrToDefault(object obj, byte defaultValue = 0)
		{
			if (obj == null)
				return defaultValue;
			if (obj.IsBool())
				return (bool)obj ? (byte)1 : (byte)0;

			if (obj.IsByte())
				return ((byte)obj);
			if (obj.IsShort())
				return ((byte)(short)obj);
			if (obj.IsInt())
				return ((byte)(int)obj);
			if (obj.IsLong())
				return ((byte)(long)obj);
			if (obj.IsFloat())
				return ((byte)(float)obj);
			if (obj.IsDouble())
				return ((byte)(double)obj);
			if (obj.IsString())
			{
				try
				{
					return byte.Parse((string)obj);
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为short，失败时返回dv
		/// </summary>
		public static short ToShortOrToDefault(object obj, short defaultValue = 0)
		{
			if (obj == null)
				return defaultValue;
			if (obj.IsBool())
				return ToByteOrToDefault(obj, (byte)defaultValue);
			if (obj.IsByte())
				return ((byte)obj);
			if (obj.IsShort())
				return ((short)obj);
			if (obj.IsInt())
				return ((short)(int)obj);
			if (obj.IsLong())
				return ((short)(long)obj);
			if (obj.IsFloat())
				return ((short)(float)obj);
			if (obj.IsDouble())
				return ((short)(double)obj);
			if (obj.IsString())
			{
				try
				{
					return short.Parse((string)obj);
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为char，失败时返回dv
		/// </summary>
		public static char ToCharOrToDefault(object obj, char defaultValue = (char)0x0)
		{
			if (obj == null)
				return defaultValue;
			if (IsChar(obj))
				return ((char)obj);
			if (obj.IsByte())
				return (char)((byte)obj);
			if (obj.IsShort())
				return (char)((short)obj);
			if (obj.IsInt())
				return (char)((int)obj);
			if (obj.IsLong())
				return (char)((long)obj);
			if (obj.IsFloat())
				return (char)((float)obj);
			if (obj.IsDouble())
				return (char)((double)obj);
			if (obj.IsString())
			{
				var s = (string)obj;
				if (s.Length == 1)
					return s[0];
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为int，失败时返回dv
		/// </summary>
		public static int ToIntOrToDefault(object obj, int defaultValue = 0)
		{
			if (obj == null)
				return defaultValue;
			if (obj.IsBool())
				return ToByteOrToDefault(obj, 0);
			if (IsByte(obj))
				return ((byte)obj);
			if (obj.IsShort())
				return ((short)obj);
			if (obj.IsInt())
				return ((int)obj);
			if (obj.IsLong())
				return (int)((long)obj);
			if (obj.IsFloat())
				return (int)((float)obj);
			if (obj.IsDouble())
				return (int)((double)obj);
			if (obj.IsString())
			{
				try
				{
					return int.Parse((string)obj);
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为long，失败时返回dv
		/// </summary>
		public static long ToLongOrToDefault(object obj, long defaultValue = 0)
		{
			if (obj == null)
				return defaultValue;
			if (obj.IsBool())
				return ToByteOrToDefault(obj, 0);
			if (obj.IsByte())
				return ((byte)obj);
			if (obj.IsShort())
				return ((short)obj);
			if (obj.IsInt())
				return ((int)obj);
			if (obj.IsLong())
				return ((long)obj);
			if (obj.IsFloat())
				return (long)((float)obj);
			if (obj.IsDouble())
				return (long)((double)obj);
			if (obj.IsString())
			{
				try
				{
					return long.Parse((string)obj);
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为float，失败时返回dv
		/// </summary>
		public static float ToFloatOrToDefault(object obj, float defaultValue = 0)
		{
			if (obj == null)
				return defaultValue;
			if (obj.IsBool())
				return ToByteOrToDefault(obj, 0);
			if (obj.IsByte())
				return ((byte)obj);
			if (obj.IsShort())
				return ((short)obj);
			if (obj.IsInt())
				return ((int)obj);
			if (obj.IsLong())
				return ((long)obj);
			if (obj.IsFloat())
				return ((float)obj);
			if (obj.IsDouble())
				return (float)((double)obj);
			if (obj.IsString())
			{
				try
				{
					return float.Parse((string)obj);
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为double，失败时返回dv
		/// </summary>
		public static double ToDoubleOrToDefault(object obj, double defaultValue = 0)
		{
			if (obj == null)
				return defaultValue;
			if (obj.IsBool())
				return ToByteOrToDefault(obj, 0);
			if (IsByte(obj))
				return ((byte)obj);
			if (obj.IsShort())
				return ((short)obj);
			if (obj.IsInt())
				return ((int)obj);
			if (obj.IsLong())
				return ((long)obj);
			if (obj.IsFloat())
				return ((float)obj);
			if (obj.IsDouble())
				return ((double)obj);
			if (obj.IsDateTime())
				return ((DateTime)obj).Ticks;
			if (obj.IsString())
			{
				try
				{
					return double.Parse((string)obj);
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为DateTime（如果o是string类型，按照pattern来转换）失败时返回dv
		/// </summary>
		public static DateTime ToDateTimeOrToDefault(object obj, string pattern, DateTime defaultValue = default)
		{
			if (obj == null)
				return defaultValue;
			if (obj.IsLong())
				return new DateTime((long)obj);
			if (obj.IsDateTime())
				return (DateTime)obj;
			return obj.IsString() ? ((string)obj).ToDateTime(pattern) : defaultValue;
		}

		/// <summary>
		///将o转化为DateTime（如果o是string类型，按照yyyy-MM-dd HH:mm:ss来转换）失败时返回dv
		/// </summary>
		public static DateTime ToDateTimOrToDefault(object obj, DateTime defaultValue = default)
		{
			return ToDateTimeOrToDefault(obj, StringConst.STRING_yyyy_MM_dd_HH_mm_ss, defaultValue);
		}

		/// <summary>
		/// 将o转化为DateTime（如果o是string类型，按照yyyy-MM-dd来转换）失败时返回dv
		/// </summary>
		public static DateTime ToDateOrToDefault(object obj, DateTime dv = default)
		{
			return ToDateTimeOrToDefault(obj, StringConst.STRING_yyyy_MM_dd, dv);
		}

		/// <summary>
		/// 将o转化为DateTime（如果o是string类型，按照HH:mm:ss来转换）失败时返回dv
		/// </summary>
		public static DateTime ToTimeOrToDefault(object obj, DateTime defaultValue = default)
		{
			return ToDateTimeOrToDefault(obj, StringConst.STRING_HH_mm_ss, defaultValue);
		}

		/// <summary>
		/// 将o转化为String,失败时返回dv
		/// </summary>
		public static string ToStringOrToDefault(object obj, string defaultValue = null)
		{
			return obj?.ToString() ?? defaultValue;
		}

		/// <summary>
		/// 将o转化为IList,失败时返回dv
		/// </summary>
		public static List<T> ToListOrToDefault<T>(object obj, List<T> defaultValue = null)
		{
			return IsList<T>(obj) ? (List<T>)obj : defaultValue;
		}

		/// <summary>
		/// 将o转化为IDictionary,失败时返回dv
		/// </summary>
		public static Dictionary<T, V> ToDictionaryOrToDefault<T, V>(object obj, Dictionary<T, V> defaultValue = null)
		{
			return IsDictionary<T, V>(obj) ? (Dictionary<T, V>)obj : defaultValue;
		}

		/// <summary>
		/// 将o转化为Booleans,失败时返回dv
		/// </summary>
		public static bool[] ToBoolsOrToDefault(object obj, bool[] defaultValue = null)
		{
			if (obj is bool[] booleans)
				return booleans;
			if (obj.IsArray())
			{
				var array = (Array)obj;
				int len = array.Length;
				defaultValue = new bool[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = array.GetValue(i).ToBoolOrToDefault();
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Bytes,失败时返回dv
		/// </summary>
		public static byte[] ToBytesOrToDefault(object obj, byte[] defaultValue = null)
		{
			if (obj is byte[] bytes)
				return bytes;
			if (obj.IsArray())
			{
				var array = (Array)obj;
				int len = array.Length;
				defaultValue = new byte[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = array.GetValue(i).ToByteOrToDefault(0);
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Shorts,失败时返回dv
		/// </summary>
		public static short[] ToShortsOrToDefault(object obj, short[] defaultValue = null)
		{
			if (obj is short[] shorts)
				return shorts;
			if (obj.IsArray())
			{
				var array = (Array)obj;
				int len = array.Length;
				defaultValue = new short[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = array.GetValue(i).ToShortOrToDefault(0);
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Ints,失败时返回dv
		/// </summary>
		public static int[] ToIntsOrToDefault(object obj, int[] defaultValue = null)
		{
			if (obj is int[] ints)
				return ints;
			if (obj.IsArray())
			{
				var array = (Array)obj;
				int len = array.Length;
				defaultValue = new int[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = array.GetValue(i).ToIntOrToDefault(0);
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Longs,失败时返回dv
		/// </summary>
		public static long[] ToLongsOrToDefault(object obj, long[] defaultValue = null)
		{
			var longs = obj as long[];
			if (longs != null)
				return longs;
			if (obj.IsArray())
			{
				var array = (Array)obj;
				int len = array.Length;
				defaultValue = new long[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = ToLongOrToDefault(array.GetValue(i), 0L);
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Floats,失败时返回dv
		/// </summary>
		public static float[] ToFloatsOrToDefault(object obj, float[] defaultValue = null)
		{
			if (obj is float[] floats)
				return floats;
			if (obj.IsArray())
			{
				var array = (Array)obj;
				int len = array.Length;
				defaultValue = new float[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = ToFloatOrToDefault(array.GetValue(i));
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Doubles,失败时返回dv
		/// </summary>
		public static double[] ToDoublesOrToDefault(object obj, double[] defaultValue = null)
		{
			if (obj is double[] doubles)
				return doubles;
			if (obj.IsArray())
			{
				var array = (Array)obj;
				int len = array.Length;
				defaultValue = new double[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = ToDoubleOrToDefault(array.GetValue(i), 0.0d);
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Strings,失败时返回dv
		/// </summary>
		public static string[] ToStringsOrToDefault(object obj, string[] defaultValue = null)
		{
			if (obj is string[] strings)
				return strings;
			if (obj.IsArray())
			{
				var array = (Array)obj;
				int len = array.Length;
				defaultValue = new string[len];

				for (int i = 0; i < len; i++)
					defaultValue[i] = ToStringOrToDefault(array.GetValue(i));
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为ILists,失败时返回dv
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static List<T>[] ToListsOrToDefault<T>(object obj, List<T>[] defaultValue = null)
		{
			var iLists = obj as List<T>[];
			return iLists ?? defaultValue;
		}

		/// <summary>
		/// 将o转化为IDictionarys,失败时返回dv
		/// </summary>
		public static Dictionary<T, V>[] ToDictionarysOrToDefault<T, V>(object obj, Dictionary<T, V>[] defaultValue = null)
		{
			var dictionarys = obj as Dictionary<T, V>[];
			return dictionarys ?? defaultValue;
		}

		/// <summary>
		/// 将o转化为Objects,失败时返回dv
		/// </summary>
		public static Array ToArrayOrToDefault(object obj, Array defaultValue = null)
		{
			if (obj is object[] objects)
				return objects;
			if (obj.IsArray())
			{
				var array = (Array)obj;
				Array dvArray = null;
				int len = array.Length;
				if (obj is bool[])
					dvArray = new bool[len];
				if (obj is byte[])
					dvArray = new byte[len];
				if (obj is char[])
					dvArray = new char[len];
				if (obj is short[])
					dvArray = new short[len];
				if (obj is int[])
					dvArray = new int[len];
				if (obj is long[])
					dvArray = new long[len];
				if (obj is float[])
					dvArray = new float[len];
				if (obj is double[])
					dvArray = new double[len];

				for (int i = 0; i < len; i++)
					dvArray?.SetValue(array.GetValue(i), i);

				return dvArray;
			}

			return defaultValue;
		}

		public static T To<T>(object obj)
		{
			return (T)Convert.ChangeType(obj, typeof(T));
		}

		public static object To(object obj, Type type)
		{
			return Convert.ChangeType(obj, type);
		}

		public static T As<T>(object obj) where T : class
		{
			return obj as T;
		}

		/// <summary>
		/// 将o转化为string
		/// </summary>
		public static string ObjectToString(object obj)
		{
			if (obj == null)
				return StringConst.STRING_NULL;
			if (obj.IsString())
				return (string)obj;
			if (obj.IsDateTime())
				return ((DateTime)obj).ToString(StringConst.STRING_yyyy_MM_dd);
			if (IsChar(obj))
				return ((char)obj).ToString();
			if (IsChars(obj))
				return new string((char[])obj);
			return IsBytes(obj) ? ByteUtil.ToString((byte[])obj, 0, ((byte[])obj).Length) : obj.ToString();
		}

		#endregion

		#region 反射

		public static bool IsHasMethod(object obj, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return GetMethodInfo2(obj, methodName, bindingFlags) != null;
		}

		public static MethodInfo GetMethodInfo2(object obj, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetReflectionType(obj).GetMethodInfo2(methodName, bindingFlags);
		}

		public static MethodInfo GetMethodInfo(object obj, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.All, params Type[] sourceParameterTypes)
		{
			return GetMethodInfo(ReflectionUtil.GetReflectionType(obj), methodName, bindingFlags, sourceParameterTypes);
		}

		/*************************************************************************************
		* 模块描述:GenericMethod
		*************************************************************************************/
		public static MethodInfo GetGenericMethodInfo2(object obj, string methodName, Type[] genericTypes,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetReflectionType(obj)
				.GetGenericMethodInfo2(methodName, genericTypes, bindingFlags);
		}

		public static MethodInfo GetGenericMethodInfo(object obj, string methodName, Type[] genericTypes,
			BindingFlags bindingFlags = BindingFlagsConst.All, params Type[] sourceParameterTypes)
		{
			return ReflectionUtil.GetReflectionType(obj)
				.GetGenericMethodInfo(methodName, genericTypes, bindingFlags, sourceParameterTypes);
		}
		/*************************************************************************************
		* 模块描述:ExtensionMethod
		*************************************************************************************/
		public static bool IsHasExtensionMethod(object obj, string methodName)
		{
			return GetExtensionMethodInfo2(obj, methodName) != null;
		}

		public static MethodInfo GetExtensionMethodInfo2(object obj, string methodName)
		{
			return ReflectionUtil.GetReflectionType(obj).GetExtensionMethodInfo2(methodName);
		}

		public static MethodInfo GetExtensionMethodInfo(object obj, string methodName,
			params Type[] sourceParameterTypes)
		{
			return ReflectionUtil.GetReflectionType(obj).GetExtensionMethodInfo(methodName, sourceParameterTypes);
		}

		#region FiledValue

		public static FieldInfo GetFieldInfo(object obj, string fieldName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetReflectionType(obj).GetFieldInfo(fieldName, bindingFlags);
		}

		public static void SetFieldValue(object obj, string fieldName, object value,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			GetFieldInfo(obj, fieldName, bindingFlags).SetValue(ReflectionUtil.GetReflectionObject(obj), value);
		}

		public static T GetFieldValue<T>(object obj, string fieldName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return (T)GetFieldValue(obj, fieldName, bindingFlags);
		}

		public static object GetFieldValue(object obj, string fieldName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetReflectionType(obj).GetFieldInfo(fieldName, bindingFlags).GetValue(obj);
		}

		#endregion

		#region PropertyValue

		public static PropertyInfo GetPropertyInfo(object obj, string propertyName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetReflectionType(obj).GetPropertyInfo(propertyName, bindingFlags);
		}

		public static void SetPropertyValue(object obj, string propertyName, object value, object[] index = null)
		{
			GetPropertyInfo(obj, propertyName)
				.SetValue(ReflectionUtil.GetReflectionObject(obj), value, index);
		}

		public static T GetPropertyValue<T>(object obj, string propertyName, object[] index = null)
		{
			return (T)GetPropertyValue(obj, propertyName, index);
		}

		public static object GetPropertyValue(object obj, string propertyName, object[] index = null)
		{
			return ReflectionUtil.GetReflectionType(obj).GetPropertyInfo(propertyName).GetValue(obj, index);
		}

		#endregion

		#region Invoke

		public static T InvokeMethod<T>(object obj, MethodInfo methodInfo, params object[] parameters)
		{
			return ReflectionUtil.Invoke<T>(obj, methodInfo, parameters);
		}

		public static T InvokeMethod<T>(object obj, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ReflectionUtil.Invoke<T>(obj, methodName, isMissNotInvoke, parameters);
		}

		public static void InvokeMethod(object obj, MethodInfo methodInfo, params object[] parameters)
		{
			ReflectionUtil.Invoke(obj, methodInfo, parameters);
		}

		public static void InvokeMethod(object obj, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			ReflectionUtil.Invoke<object>(obj, methodName, isMissNotInvoke, parameters);
		}
		/*************************************************************************************
		* 模块描述:Generic
		*************************************************************************************/
		public static T InvokeGenericMethod<T>(object obj, string methodName, Type[] genericTypes,
			bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ReflectionUtil.InvokeGeneric<T>(obj, methodName, genericTypes, isMissNotInvoke, parameters);
		}

		public static object InvokeGenericMethod(object obj, string methodName, Type[] genericTypes,
			bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ReflectionUtil.InvokeGeneric<object>(obj, methodName, genericTypes, isMissNotInvoke, parameters);
		}
		/*************************************************************************************
		* 模块描述:ExtensionMethod
		*************************************************************************************/
		public static T InvokeExtensionMethod<T>(object obj, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ExtensionUtil.InvokeExtension<T>(obj, methodName, isMissNotInvoke, parameters);
		}

		public static void InvokeExtensionMethod(object obj, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			ExtensionUtil.InvokeExtension<object>(obj, methodName, isMissNotInvoke, parameters);
		}
		/*************************************************************************************
		* 模块描述:Generic
		*************************************************************************************/
		public static T InvokeExtensionGenericMethod<T>( object obj, string methodName, Type[] genericTypes,
			bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ExtensionUtil.InvokeExtensionGeneric<T>(obj, methodName, genericTypes, isMissNotInvoke,
				parameters);
		}

		public static void InvokeExtensionGenericMethod(object obj, string methodName, Type[] genericTypes,
			bool isMissNotInvoke = true,
			params object[] parameters)
		{
			ExtensionUtil.InvokeExtensionGeneric<object>(obj, methodName, genericTypes, isMissNotInvoke,
				parameters);
		}

		#endregion

		#endregion

		#region SetColor

		public static void SetColorR(System.Object obj, float v, string memberName = StringConst.STRING_COLOR)
		{
			ColorUtil.SetColor(obj, memberName, ColorMode.R, v);
		}

		public static void SetColorG(System.Object obj, float v, string memberName = StringConst.STRING_COLOR)
		{
			ColorUtil.SetColor(obj, memberName, ColorMode.G, v);
		}

		public static void SetColorB(System.Object obj, float v, string memberName = StringConst.STRING_COLOR)
		{
			ColorUtil.SetColor(obj, memberName, ColorMode.B, v);
		}

		public static void SetColorA(System.Object obj, float v, string memberName = StringConst.STRING_COLOR)
		{
			ColorUtil.SetColor(obj, memberName, ColorMode.A, v);
		}

		public static void SetColor(System.Object obj, ColorMode rgbaMode, params float[] rgba)
		{
			ColorUtil.SetColor(obj, rgbaMode, rgba);
		}

		public static void SetColor(System.Object obj, string memberName, ColorMode rgbaMode, params float[] rgba)
		{
			ColorUtil.SetColor(obj, memberName, rgbaMode, rgba);
		}

		#endregion


		/// <summary>
		///用法
		/// stirng s;
		/// s=s.GetOrSetObject("kk");
		/// 采用延迟调用Func
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="defaultObjectFunc"></param>
		/// <returns></returns>
		public static T GetOrSetDefault<T>(T obj, Func<T> defaultObjectFunc = null)
		{
			if (obj == null)
				obj = defaultObjectFunc == null ? default : defaultObjectFunc();

			return obj;
		}

		//a=a.swap(ref b);
		public static T Swap<T>(T obj, ref T b)
		{
			T c = b;
			b = obj;
			return c;
		}


		public static bool IsNull(object obj)
		{
			return obj == null;
		}

		public static T CloneDeep<T>(T obj)
		{
			return CloneUtil.CloneDeep(obj);
		}

		public static T Clone<T>(T self)
		{
			return CloneUtil.Clone(self);
		}

//		public static void Despawn(object obj)
//		{
//			if (obj is IDespawn spawn)
//			{
//				spawn.Despawn();
//			}
//		}

		public static object GetNotNullKey(object obj)
		{
			return obj ?? NullUtil.GetDefaultString();
		}

		public static object GetNullableKey(object obj)
		{
			return obj.Equals(NullUtil.GetDefaultString()) ? null : obj;
		}
	}
}