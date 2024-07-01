using System;
using System.Collections.Generic;
using System.Reflection;

namespace DG
{
	public static class System_Object_Extension
	{
		public static bool IsValidObject(this object self)
		{
			return ObjectUtil.IsValidObject(self);
		}

		#region DOTween

		public static DOTweenId DOTweenId(this object self)
		{
			return ObjectUtil.DOTweenId(self);
		}

		public static void DOKillByDOTweenId(this object self)
		{
			ObjectUtil.DOKillByDOTweenId(self);
		}

		#endregion

		#region ToString2 ToLinkedHashtable2

		


		public static object ToLinkedHashtable2(this object self)
		{
			return ObjectUtil.ToLinkedHashtable2(self);
		}

		#endregion

		#region 类型判断

		/// <summary>
		/// 是否是IsIDictionary
		/// </summary>
		public static bool IsDictionary<T, V>(this object self)
		{
			return ObjectUtil.IsDictionary<T,V>(self);
		}

		/// <summary>
		/// 是否是数组
		/// </summary>
		public static bool IsArray(this object self)
		{
			return ObjectUtil.IsArray(self);
		}

		/// <summary>
		/// 是否是IList
		/// </summary>
		public static bool IsList<T>(this object self)
		{
			return ObjectUtil.IsList<T>(self);
		}

		/// <summary>
		/// 是否是String
		/// </summary>
		public static bool IsString(this object self)
		{
			return ObjectUtil.IsString(self);
		}

		/// <summary>
		/// 是否是boolean
		/// </summary>
		public static bool IsBool(this object self)
		{
			return ObjectUtil.IsBool(self);
		}

		/// <summary>
		/// 是否是数字
		/// </summary>
		public static bool IsNumber(this object self)
		{
			return ObjectUtil.IsNumber(self);
		}

		/// <summary>
		/// 是否是整数类型，即非小数类型
		/// </summary>
		public static bool IsIntegral(this object self)
		{
			return ObjectUtil.IsIntegral(self);
		}

		/// <summary>
		/// 是否是小数类型，即非整数类型
		/// </summary>
		public static bool IsFloating(this object self)
		{
			return ObjectUtil.IsFloating(self);
		}

		/// <summary>
		/// 是否是byte
		/// </summary>
		public static bool IsByte(this object self)
		{
			return ObjectUtil.IsByte(self);
		}

		/// <summary>
		/// 是否是short
		/// </summary>
		public static bool IsShort(this object self)
		{
			return ObjectUtil.IsShort(self);
		}

		/// <summary>
		/// 是否是char
		/// </summary>
		public static bool IsChar(this object self)
		{
			return ObjectUtil.IsChar(self);
		}

		/// <summary>
		/// 是否是int
		/// </summary>
		public static bool IsInt(this object self)
		{
			return ObjectUtil.IsInt(self);
		}

		/// <summary>
		/// 是否是long类型
		/// </summary>
		public static bool IsLong(this object self)
		{
			return ObjectUtil.IsLong(self);
		}

		/// <summary>
		/// 是否是float类型
		/// </summary>
		public static bool IsFloat(this object self)
		{
			return ObjectUtil.IsFloat(self);
		}

		/// <summary>
		/// 是否是double类型
		/// </summary>
		public static bool IsDouble(this object self)
		{
			return ObjectUtil.IsDouble(self);
		}

		/// <summary>
		/// 是否是DateTime
		/// </summary>
		public static bool IsDateTime(this object self)
		{
			return ObjectUtil.IsDateTime(self);
		}

		/// <summary>
		/// 是否是bytes
		/// </summary>
		public static bool IsBytes(this object self)
		{
			return ObjectUtil.IsBytes(self);
		}

		/// <summary>
		/// 是否是chars
		/// </summary>
		public static bool IsChars(object self)
		{
			return ObjectUtil.IsChars(self);
		}

		/// <summary>
		/// 是否是Class
		/// </summary>
		public static bool IsClass(this object self)
		{
			return ObjectUtil.IsClass(self);
		}

		/// <summary>
		/// 是否是方法
		/// </summary>
		public static bool IsMethod(this object self)
		{
			return ObjectUtil.IsMethod(self);
		}

		#endregion

		#region 各种ToXX

		/// <summary>
		/// 将o转化为boolean，失败时返回dv
		/// </summary>
		public static bool ToBoolOrToDefault(this object self, bool defaultValue = false)
		{
			return ObjectUtil.ToBoolOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为byte，失败时返回dv
		/// </summary>
		public static byte ToByteOrToDefault(this object self, byte defaultValue = 0)
		{
			return ObjectUtil.ToByteOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为short，失败时返回dv
		/// </summary>
		public static short ToShortOrToDefault(this object self, short defaultValue = 0)
		{
			return ObjectUtil.ToShortOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为char，失败时返回dv
		/// </summary>
		public static char ToCharOrToDefault(this object self, char defaultValue = (char)0x0)
		{
			return ObjectUtil.ToCharOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为int，失败时返回dv
		/// </summary>
		public static int ToIntOrToDefault(this object self, int defaultValue = 0)
		{
			return ObjectUtil.ToIntOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为long，失败时返回dv
		/// </summary>
		public static long ToLongOrToDefault(this object self, long defaultValue = 0)
		{
			return ObjectUtil.ToLongOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为float，失败时返回dv
		/// </summary>
		public static float ToFloatOrToDefault(this object self, float defaultValue = 0)
		{
			return ObjectUtil.ToFloatOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为double，失败时返回dv
		/// </summary>
		public static double ToDoubleOrToDefault(this object self, double defaultValue = 0)
		{
			return ObjectUtil.ToDoubleOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为DateTime（如果o是string类型，按照pattern来转换）失败时返回dv
		/// </summary>
		public static DateTime ToDateTimeOrToDefault(this object self, string pattern, DateTime defaultValue = default)
		{
			return ObjectUtil.ToDateTimeOrToDefault(self, pattern, defaultValue);
		}

		/// <summary>
		///将o转化为DateTime（如果o是string类型，按照yyyy-MM-dd HH:mm:ss来转换）失败时返回dv
		/// </summary>
		public static DateTime ToDateTimOrToDefault(this object self, DateTime defaultValue = default)
		{
			return ObjectUtil.ToDateTimOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为DateTime（如果o是string类型，按照yyyy-MM-dd来转换）失败时返回dv
		/// </summary>
		public static DateTime ToDateOrToDefault(this object self, DateTime dv = default)
		{
			return ObjectUtil.ToDateOrToDefault(self, dv);
		}

		/// <summary>
		/// 将o转化为DateTime（如果o是string类型，按照HH:mm:ss来转换）失败时返回dv
		/// </summary>
		public static DateTime ToTimeOrToDefault(this object self, DateTime defaultValue = default)
		{
			return ObjectUtil.ToTimeOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为String,失败时返回dv
		/// </summary>
		public static string ToStringOrToDefault(this object self, string defaultValue = null)
		{
			return ObjectUtil.ToStringOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为IList,失败时返回dv
		/// </summary>
		public static List<T> ToListOrToDefault<T>(this object self, List<T> defaultValue = null)
		{
			return ObjectUtil.ToListOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为IDictionary,失败时返回dv
		/// </summary>
		public static Dictionary<T, V> ToDictionaryOrToDefault<T, V>(this object self, Dictionary<T, V> defaultValue = null)
		{
			return ObjectUtil.ToDictionaryOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为Booleans,失败时返回dv
		/// </summary>
		public static bool[] ToBoolsOrToDefault(this object self, bool[] defaultValue = null)
		{
			return ObjectUtil.ToBoolsOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为Bytes,失败时返回dv
		/// </summary>
		public static byte[] ToBytesOrToDefault(this object self, byte[] defaultValue = null)
		{
			return ObjectUtil.ToBytesOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为Shorts,失败时返回dv
		/// </summary>
		public static short[] ToShortsOrToDefault(this object self, short[] defaultValue = null)
		{
			return ObjectUtil.ToShortsOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为Ints,失败时返回dv
		/// </summary>
		public static int[] ToIntsOrToDefault(this object self, int[] defaultValue = null)
		{
			return ObjectUtil.ToIntsOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为Longs,失败时返回dv
		/// </summary>
		public static long[] ToLongsOrToDefault(this object self, long[] defaultValue = null)
		{
			return ObjectUtil.ToLongsOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为Floats,失败时返回dv
		/// </summary>
		public static float[] ToFloatsOrToDefault(this object self, float[] defaultValue = null)
		{
			return ObjectUtil.ToFloatsOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为Doubles,失败时返回dv
		/// </summary>
		public static double[] ToDoublesOrToDefault(this object self, double[] defaultValue = null)
		{
			return ObjectUtil.ToDoublesOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为Strings,失败时返回dv
		/// </summary>
		public static string[] ToStringsOrToDefault(this object self, string[] defaultValue = null)
		{
			return ObjectUtil.ToStringsOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为ILists,失败时返回dv
		/// </summary>
		/// <param name="self"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static List<T>[] ToListsOrToDefault<T>(this object self, List<T>[] defaultValue = null)
		{
			return ObjectUtil.ToListsOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为IDictionarys,失败时返回dv
		/// </summary>
		public static Dictionary<T, V>[] ToDictionarysOrToDefault<T, V>(this object self, Dictionary<T, V>[] defaultValue = null)
		{
			return ObjectUtil.ToDictionarysOrToDefault(self, defaultValue);
		}

		/// <summary>
		/// 将o转化为Objects,失败时返回dv
		/// </summary>
		public static Array ToArrayOrToDefault(this object self, Array defaultValue = null)
		{
			return ObjectUtil.ToArrayOrToDefault(self, defaultValue);
		}

		public static T To<T>(this object self)
		{
			return ObjectUtil.To<T>(self);
		}

		public static object To(this object self, Type type)
		{
			return ObjectUtil.To(self, type);
		}

		public static T As<T>(this object self) where T : class
		{
			return ObjectUtil.As<T>(self);
		}

		/// <summary>
		/// 将o转化为string
		/// </summary>
		public static string ObjectToString(this object self)
		{
			return ObjectUtil.ObjectToString(self);
		}

		#endregion

		#region 反射

		public static bool IsHasMethod(this object self, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.ALL)
		{
			return ObjectUtil.IsHasMethod(self, methodName , bindingFlags);
		}

		public static MethodInfo GetMethodInfo2(this object self, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.ALL)
		{
			return ObjectUtil.GetMethodInfo2(self, methodName, bindingFlags);
		}

		public static MethodInfo GetMethodInfo(this object self, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.ALL, params Type[] sourceParameterTypes)
		{
			return ObjectUtil.GetMethodInfo(self, methodName, bindingFlags, sourceParameterTypes);
		}

		/*************************************************************************************
		* 模块描述:Generic
		*************************************************************************************/
		public static MethodInfo GetGenericMethodInfo2(this object self, string methodName, Type[] genericTypes,
			BindingFlags bindingFlags = BindingFlagsConst.ALL)
		{
			return ObjectUtil.GetGenericMethodInfo2(self, methodName, genericTypes, bindingFlags);
		}

		public static MethodInfo GetGenericMethodInfo(this object self, string methodName, Type[] genericTypes,
			BindingFlags bindingFlags = BindingFlagsConst.ALL, params Type[] sourceParameterTypes)
		{
			return ObjectUtil.GetGenericMethodInfo(self, methodName, genericTypes, bindingFlags, sourceParameterTypes);
		}
		/*************************************************************************************
		* 模块描述:ExtensionMethod
		*************************************************************************************/
		public static bool IsHasExtensionMethod(this object self, string methodName)
		{
			return ObjectUtil.IsHasExtensionMethod(self, methodName);
		}

		public static MethodInfo GetExtensionMethodInfo2(this object self, string methodName)
		{
			return ObjectUtil.GetExtensionMethodInfo2(self, methodName);
		}

		public static MethodInfo GetExtensionMethodInfo(this object self, string methodName,
			params Type[] sourceParameterTypes)
		{
			return ObjectUtil.GetExtensionMethodInfo(self, methodName, sourceParameterTypes);
		}

		#region FiledValue

		public static FieldInfo GetFieldInfo(this object self, string fieldName,
			BindingFlags bindingFlags = BindingFlagsConst.ALL)
		{
			return ObjectUtil.GetFieldInfo(self, fieldName, bindingFlags);
		}

		public static void SetFieldValue(this object self, string fieldName, object value,
			BindingFlags bindingFlags = BindingFlagsConst.ALL)
		{
			ObjectUtil.SetFieldValue(self, fieldName, value, bindingFlags);
		}

		public static T GetFieldValue<T>(this object self, string fieldName,
			BindingFlags bindingFlags = BindingFlagsConst.ALL)
		{
			return ObjectUtil.GetFieldValue<T>(self, fieldName, bindingFlags);
		}

		public static object GetFieldValue(this object self, string fieldName,
			BindingFlags bindingFlags = BindingFlagsConst.ALL)
		{
			return ObjectUtil.GetFieldValue(self, fieldName, bindingFlags);
		}

		#endregion

		#region PropertyValue

		public static PropertyInfo GetPropertyInfo(this object self, string propertyName,
			BindingFlags bindingFlags = BindingFlagsConst.ALL)
		{
			return ObjectUtil.GetPropertyInfo(self, propertyName, bindingFlags);
		}

		public static void SetPropertyValue(this object self, string propertyName, object value, object[] index = null)
		{
			ObjectUtil.SetPropertyValue(self, propertyName, value, index);
		}

		public static T GetPropertyValue<T>(this object self, string propertyName, object[] index = null)
		{
			return ObjectUtil.GetPropertyValue<T>(self, propertyName, index);
		}

		public static object GetPropertyValue(this object self, string propertyName, object[] index = null)
		{
			return ObjectUtil.GetPropertyValue(self, propertyName, index);
		}

		#endregion

		#region Invoke

		public static T InvokeMethod<T>(this object self, MethodInfo methodInfo, params object[] parameters)
		{
			return ObjectUtil.InvokeMethod<T>(self, methodInfo, parameters);
		}

		public static T InvokeMethod<T>(this object self, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ObjectUtil.InvokeMethod<T>(self, methodName, isMissNotInvoke, parameters);
		}

		public static void InvokeMethod(this object self, MethodInfo methodInfo, params object[] parameters)
		{
			ObjectUtil.InvokeMethod(self, methodInfo, parameters);
		}

		public static void InvokeMethod(this object self, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			ObjectUtil.InvokeMethod(self, methodName, isMissNotInvoke, parameters);
		}
		/*************************************************************************************
		* 模块描述:Generic
		*************************************************************************************/
		public static T InvokeGenericMethod<T>(this object self, string methodName, Type[] genericTypes,
			bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ObjectUtil.InvokeGenericMethod<T>(self, methodName, genericTypes, isMissNotInvoke, parameters);
		}

		public static object InvokeGenericMethod(this object self, string methodName, Type[] genericTypes,
			bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ObjectUtil.InvokeGenericMethod(self, methodName, genericTypes, isMissNotInvoke, parameters);
		}
		/*************************************************************************************
		* 模块描述:ExtensionMethod
		*************************************************************************************/
		public static T InvokeExtensionMethod<T>(this object self, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ObjectUtil.InvokeExtensionMethod<T>(self, methodName, isMissNotInvoke, parameters);
		}

		public static void InvokeExtensionMethod(this object self, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			ObjectUtil.InvokeExtensionMethod(self, methodName, isMissNotInvoke, parameters);
		}
		/*************************************************************************************
		* 模块描述:Generic
		*************************************************************************************/
		public static T InvokeExtensionGenericMethod<T>(this object self, string methodName, Type[] genericTypes,
			bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ObjectUtil.InvokeExtensionGenericMethod<T>(self, methodName, genericTypes, isMissNotInvoke, parameters);
		}

		public static void InvokeExtensionGenericMethod(this object self, string methodName, Type[] genericTypes,
			bool isMissNotInvoke = true,
			params object[] parameters)
		{
			ObjectUtil.InvokeExtensionGenericMethod(self, methodName, genericTypes, isMissNotInvoke, parameters);
		}

		#endregion

		#endregion

		#region SetColor

		public static void SetColorR(this System.Object self, float v, string memberName = StringConst.STRING_COLOR)
		{
			ObjectUtil.SetColorR(self, v, memberName);
		}

		public static void SetColorG(this System.Object self, float v, string memberName = StringConst.STRING_COLOR)
		{
			ObjectUtil.SetColorG(self, v, memberName);
		}

		public static void SetColorB(this System.Object self, float v, string memberName = StringConst.STRING_COLOR)
		{
			ObjectUtil.SetColorB(self, v, memberName);
		}

		public static void SetColorA(this System.Object self, float v, string memberName = StringConst.STRING_COLOR)
		{
			ObjectUtil.SetColorA(self, v, memberName);
		}

		public static void SetColor(this System.Object self, ColorMode rgbaMode, params float[] rgba)
		{
			ObjectUtil.SetColor(self, rgbaMode, rgba);
		}

		public static void SetColor(System.Object self, string memberName, ColorMode rgbaMode, params float[] rgba)
		{
			ObjectUtil.SetColor(self, memberName, rgbaMode, rgba);
		}

		#endregion


		/// <summary>
		///用法
		/// stirng s;
		/// s=s.GetOrSetObject("kk");
		/// 采用延迟调用Func
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <param name="defaultObjectFunc"></param>
		/// <returns></returns>
		public static T GetOrSetDefault<T>(this T self, Func<T> defaultObjectFunc = null)
		{
			return ObjectUtil.GetOrSetDefault(self, defaultObjectFunc);
		}

		//a=a.swap(ref b);
		public static T Swap<T>(this T self, ref T b)
		{
			return ObjectUtil.Swap(self,ref b);
		}


		public static bool IsNull(object self)
		{
			return ObjectUtil.IsNull(self);
		}

		public static T CloneDeep<T>(this T self)
		{
			return ObjectUtil.CloneDeep(self);
		}

		public static T Clone<T>(this T self)
		{
			return ObjectUtil.Clone(self);
		}

//		public static void Despawn(this object self)
//		{
//			return ObjectUtil.Despawn(self);
//		}

		public static object GetNotNullKey(this object self)
		{
			return ObjectUtil.GetNotNullKey(self);
		}

		public static object GetNullableKey(this object self)
		{
			return ObjectUtil.GetNullableKey(self);
		}
	}
}