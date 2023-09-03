using System.Collections.Generic;
using System.Reflection;
using Type = System.Type;

namespace DG
{
	public partial class ReflectionUtil
	{
		private static readonly Dictionary<Type, Dictionary<string, Dictionary<Args<Type>, MethodInfo>>> _cacheOfMethodInfoDict =
			new Dictionary<Type, Dictionary<string, Dictionary<Args<Type>, MethodInfo>>>();
		private static readonly Dictionary<Type, Dictionary<string, MethodInfo>> _cacheOfMethodInfoDict2 =
			new Dictionary<Type, Dictionary<string, MethodInfo>>();

		private static readonly Dictionary<Type, Dictionary<string, FieldInfo>> _cacheOfFieldInfoDict =
			new Dictionary<Type, Dictionary<string, FieldInfo>>();

		private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> _cacheOfPropertyInfoDict =
			new Dictionary<Type, Dictionary<string, PropertyInfo>>();

		private const string _methodInfoString = "methodInfo";
		private const string _filedInfoString = "fieldInfo";
		private const string _propertyInfoString = "propertyInfo";
		private const string _splitString = StringConst.String_Underline;

		#region MethodInfoCache
		public static bool IsContainsMethodInfoCache(Type type, string methodName, params Type[] parameterTypes)
		{
			if (!_cacheOfMethodInfoDict.TryGetValue(type, out var value1))
				return false;
			string mainKey = _methodInfoString + _splitString + methodName;
			if (!value1.TryGetValue(mainKey, out var value2))
				return false;
			var subKey = new Args<Type>(parameterTypes);
			return value2.ContainsKey(subKey);
		}

		public static void SetMethodInfoCache(Type type, string methodName, Type[] parameterTypes,
			MethodInfo methodInfo)
		{
			if (!_cacheOfMethodInfoDict.TryGetValue(type, out var value1))
			{
				value1 = new Dictionary<string, Dictionary<Args<Type>, MethodInfo>>();
				_cacheOfMethodInfoDict[type] = value1;
			}

			string mainKey = _methodInfoString + _splitString + methodName;
			if (!value1.TryGetValue(mainKey, out var value2))
			{
				value2 = new Dictionary<Args<Type>, MethodInfo>();
				value1[mainKey] = value2;
			}

			var subKey = new Args<Type>(parameterTypes);
			value2[subKey] = methodInfo;
		}

		public static MethodInfo GetMethodInfoCache(Type type, string methodName, params Type[] parameterTypes)
		{
			if (!_cacheOfMethodInfoDict.TryGetValue(type, out var value1))
				return null;
			string mainKey = _methodInfoString + _splitString + methodName;
			if (!value1.TryGetValue(mainKey, out var value2))
				return null;
			var subKey = new Args<Type>(parameterTypes);
			if (!value2.TryGetValue(subKey, out var value3))
				return null;
			return value3;
		}

		public static bool IsContainsMethodInfoCache2(Type type, string methodName)
		{
			if (!_cacheOfMethodInfoDict2.TryGetValue(type, out var value1))
				return false;
			string mainKey = _methodInfoString + _splitString + methodName;
			return value1.ContainsKey(mainKey);
		}

		public static void SetMethodInfoCache2(Type type, string methodName, MethodInfo methodInfo)
		{
			if (!_cacheOfMethodInfoDict2.TryGetValue(type, out var value1))
			{
				value1 = new Dictionary<string, MethodInfo>();
				_cacheOfMethodInfoDict2[type] = value1;
			}

			string mainKey = _methodInfoString + _splitString + methodName;
			value1[mainKey] = methodInfo;
		}

		public static MethodInfo GetMethodInfoCache2(Type type, string methodName)
		{
			if (!_cacheOfMethodInfoDict2.TryGetValue(type, out var value1))
				return null;
			string mainKey = _methodInfoString + _splitString + methodName;
			if (!value1.TryGetValue(mainKey, out var value2))
				return null;
			return value2;
		}
		#endregion

		#region FieldInfoCache
		public static bool IsContainsFieldInfoCache(Type type, string fieldName)
		{
			if (!_cacheOfFieldInfoDict.TryGetValue(type, out var value1))
				return false;
			string mainKey = _filedInfoString + _splitString + fieldName;
			return value1.ContainsKey(mainKey);
		}

		public static void SetFieldInfoCache(Type type, string fieldName, FieldInfo fieldInfo)
		{
			if (!_cacheOfFieldInfoDict.TryGetValue(type, out var value1))
			{
				value1 = new Dictionary<string, FieldInfo>();
				_cacheOfFieldInfoDict[type] = value1;
			}

			string mainKey = _filedInfoString + _splitString + fieldName;
			value1[mainKey] = fieldInfo;
		}

		public static FieldInfo GetFieldInfoCache(Type type, string fieldName)
		{
			if (!_cacheOfFieldInfoDict.TryGetValue(type, out var value1))
				return null;
			string mainKey = _filedInfoString + _splitString + fieldName;
			if (!value1.TryGetValue(mainKey, out var value2))
				return null;
			return value2;
		}
		#endregion

		#region PropertyInfoCache
		public static bool IsContainsPropertyInfoCache(Type type, string propertyName)
		{
			if (!_cacheOfPropertyInfoDict.TryGetValue(type, out var value1))
				return false;
			string mainKey = _propertyInfoString + _splitString + propertyName;
			return value1.ContainsKey(mainKey);
		}

		public static void SetPropertyInfoCache(Type type, string propertyName, PropertyInfo propertyInfo)
		{
			if (!_cacheOfPropertyInfoDict.TryGetValue(type, out var value1))
			{
				value1 = new Dictionary<string, PropertyInfo>();
				_cacheOfPropertyInfoDict[type] = value1;
			}

			string mainKey = _propertyInfoString + _splitString + propertyName;
			value1[mainKey] = propertyInfo;
		}

		public static PropertyInfo GetPropertyInfoCache(Type type, string propertyName)
		{
			if (!_cacheOfPropertyInfoDict.TryGetValue(type, out var value1))
				return null;
			string mainKey = _propertyInfoString + _splitString + propertyName;
			if (!value1.TryGetValue(mainKey, out var value2))
				return null;
			return value2;
		}
		#endregion
	}
}