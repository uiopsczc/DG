using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Type = System.Type;

namespace DG
{
	public partial class ReflectionUtil
	{
		static string _GetGenericTypesString(Type[] genericTypes)
		{
			int count = genericTypes.Length;
			StringBuilder stringBuilder = new StringBuilder(count * 6);
			for (var i = 0; i < count; i++)
			{
				var genericType = genericTypes[i];
				stringBuilder.Append(genericType);
				if (i != count - 1)
					stringBuilder.Append(_splitString);
			}
			return stringBuilder.ToString();
		}

		#region MethodInfoCache
		public static bool IsContainsGenericMethodInfoCache(Type type, string methodName, Type[] genericTypes,
			params Type[] parameterTypes)
		{
			if (!_cacheOfMethodInfoDict.TryGetValue(type, out var value1))
				return false;
			string mainKey = _methodInfoString + _splitString + methodName + _splitString +
							 _GetGenericTypesString(genericTypes);
			if (!value1.TryGetValue(mainKey, out var value2))
				return false;
			var subKey = new Args<Type>(parameterTypes);
			return value2.ContainsKey(subKey);
		}

		public static void SetGenericMethodInfoCache(Type type, string methodName, Type[] genericTypes,
			Type[] parameterTypes, MethodInfo methodInfo)
		{
			if (!_cacheOfMethodInfoDict.TryGetValue(type, out var value1))
			{
				value1 = new Dictionary<string, Dictionary<Args<Type>, MethodInfo>>();
				_cacheOfMethodInfoDict[type] = value1;
			}

			string mainKey = _methodInfoString + _splitString + methodName + _splitString +
							 _GetGenericTypesString(genericTypes);
			if (!value1.TryGetValue(mainKey, out var value2))
			{
				value2 = new Dictionary<Args<Type>, MethodInfo>();
				value1[mainKey] = value2;
			}

			var subKey = new Args<Type>(parameterTypes);
			value2[subKey] = methodInfo;
		}

		public static MethodInfo GetGenericMethodInfoCache(Type type, string methodName, Type[] genericTypes,
			params Type[] parameterTypes)
		{
			if (!_cacheOfMethodInfoDict.TryGetValue(type, out var value1))
				return null;
			string mainKey = _methodInfoString + _splitString + methodName + _splitString +
							 _GetGenericTypesString(genericTypes);
			if (!value1.TryGetValue(mainKey, out var value2))
				return null;
			var subKey = new Args<Type>(parameterTypes);
			if (!value2.TryGetValue(subKey, out var value3))
				return null;
			return value3;
		}

		public static bool IsContainsGenericMethodInfoCache2(Type type, string methodName, Type[] genericTypes)
		{
			if (!_cacheOfMethodInfoDict2.TryGetValue(type, out var value1))
				return false;
			string mainKey = _methodInfoString + _splitString + methodName + _splitString +
							 _GetGenericTypesString(genericTypes);
			return value1.ContainsKey(mainKey);
		}

		public static void SetGenericMethodInfoCache2(Type type, string methodName, Type[] genericTypes,
			MethodInfo methodInfo)
		{
			if (!_cacheOfMethodInfoDict2.TryGetValue(type, out var value1))
			{
				value1 = new Dictionary<string, MethodInfo>();
				_cacheOfMethodInfoDict2[type] = value1;
			}

			string mainKey = _methodInfoString + _splitString + methodName + _splitString +
							 _GetGenericTypesString(genericTypes);
			value1[mainKey] = methodInfo;
		}

		public static MethodInfo GetGenericMethodInfoCache2(Type type, string methodName, Type[] genericTypes)
		{
			if (!_cacheOfMethodInfoDict2.TryGetValue(type, out var value1))
				return null;
			string mainKey = _methodInfoString + _splitString + methodName + _splitString +
							 _GetGenericTypesString(genericTypes);
			if (!value1.TryGetValue(mainKey, out var value2))
				return null;
			return value2;
		}
		#endregion

		#region FieldInfoCache
		public static bool IsContainsGenericFieldInfoCache(Type type, string fieldName, Type[] genericTypes)
		{
			if (!_cacheOfFieldInfoDict.TryGetValue(type, out var value1))
				return false;
			string mainKey = _filedInfoString + _splitString + fieldName + _splitString +
			                 _GetGenericTypesString(genericTypes);
			return value1.ContainsKey(mainKey);
		}

		public static void SetFieldInfoCache(Type type, string fieldName, Type[] genericTypes, FieldInfo fieldInfo)
		{
			if (!_cacheOfFieldInfoDict.TryGetValue(type, out var value1))
			{
				value1 = new Dictionary<string, FieldInfo>();
				_cacheOfFieldInfoDict[type] = value1;
			}

			string mainKey = _filedInfoString + _splitString + fieldName + _splitString +
			                 _GetGenericTypesString(genericTypes);
			value1[mainKey] = fieldInfo;
		}

		public static FieldInfo GetFieldInfoCache(Type type, string fieldName, Type[] genericTypes)
		{
			if (!_cacheOfFieldInfoDict.TryGetValue(type, out var value1))
				return null;
			string mainKey = _filedInfoString + _splitString + fieldName + _splitString +
			                 _GetGenericTypesString(genericTypes);
			if (!value1.TryGetValue(mainKey, out var value2))
				return null;
			return value2;
		}
		#endregion

		#region PropertyInfoCache
		public static bool IsContainsPropertyInfoCache(Type type, string propertyName, Type[] genericTypes)
		{
			if (!_cacheOfPropertyInfoDict.TryGetValue(type, out var value1))
				return false;
			string mainKey = _propertyInfoString + _splitString + propertyName + _splitString +
			                 _GetGenericTypesString(genericTypes);
			return value1.ContainsKey(mainKey);
		}

		public static void SetPropertyInfoCache(Type type, string propertyName, Type[] genericTypes,
			PropertyInfo propertyInfo)
		{
			if (!_cacheOfPropertyInfoDict.TryGetValue(type, out var value1))
			{
				value1 = new Dictionary<string, PropertyInfo>();
				_cacheOfPropertyInfoDict[type] = value1;
			}

			string mainKey = _propertyInfoString + _splitString + propertyName + _splitString +
			                 _GetGenericTypesString(genericTypes);
			value1[mainKey] = propertyInfo;
		}

		public static PropertyInfo GetPropertyInfoCache(Type type, string propertyName, Type[] genericTypes)
		{
			if (!_cacheOfPropertyInfoDict.TryGetValue(type, out var value1))
				return null;
			string mainKey = _propertyInfoString + _splitString + propertyName + _splitString +
			                 _GetGenericTypesString(genericTypes);
			if (!value1.TryGetValue(mainKey, out var value2))
				return null;
			return value2;
		}
		#endregion
	}
}