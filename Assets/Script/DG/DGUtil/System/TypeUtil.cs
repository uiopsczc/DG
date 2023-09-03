using System;
using System.ComponentModel;
using System.Reflection;

namespace DG
{
	public partial class TypeUtil
	{
		public static Type GetType(string classPath, string dllName = null)
		{
			// return Type.GetType(string.Format("{0}, {1}, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", className, dllName));
			if (dllName == null && GetCacheDict().TryGetValue(classPath, out var value1))
				return value1;

			string dllName1 = dllName;
			if (dllName == null)
				dllName1 = Assembly.GetExecutingAssembly().FullName;
			string typeString = StringUtil.LinkStringWithCommon(classPath, dllName1);
			if (GetCacheDict().TryGetValue(typeString, out var value2))
				return value2;

			Type result = Type.GetType(typeString);
			if (dllName == null) //查找所有assembly中的类
			{
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				for (var i = 0; i < assemblies.Length; i++)
				{
					var assembly = assemblies[i];
					result = assembly.GetType(classPath);
					if (result == null) continue;
					GetCacheDict()[StringUtil.LinkStringWithCommon(classPath, assembly.FullName)] = result;
					break;
				}
			}

			GetCacheDict()[StringUtil.LinkStringWithCommon(classPath, dllName ?? StringConst.String_Empty)] = result;
			return result;
		}

		/// <summary>
		///   type是否是subType的父类
		/// </summary>
		public static bool IsSuperTypeOf(Type t, Type subType)
		{
			return t.IsAssignableFrom(subType);
		}

		/// <summary>
		///   type是否是parentType的子类
		/// </summary>
		/// <param name="t"></param>
		/// <param name="parentType"></param>
		/// <returns></returns>
		public static bool IsSubTypeOf(Type t, Type parentType)
		{
			return parentType.IsAssignableFrom(t);
		}


		public static string GetDescription(Type t, string fieldName)
		{
			var memberInfo = t.GetMember(fieldName);
			var attributes =
				(DescriptionAttribute[])memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
			return attributes[0].Description;
		}

		public static string GetDescription(Type t, int enumValue)
		{
			var memberInfo = t.GetMember(Enum.GetName(t, enumValue));
			var attributes =
				(DescriptionAttribute[])memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
			return attributes[0].Description;
		}


		public static string GetLastName(Type t)
		{
			var type = t.ToString();
			var splitIndex = type.IndexOf(CharConst.Char_Tilde);
			if (splitIndex != -1) type = type.Substring(0, splitIndex);
			var index = type.LastIndexOf(CharConst.Char_Dot);
			if (index != -1) type = type.Substring(index + 1);

			return type;
		}


		public static object DefaultValue(Type t)
		{
			return t.IsValueType ? Activator.CreateInstance(t) : null;
		}


		#region 反射相关

		public static MethodInfo GetMethodInfo2(Type t, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetMethodInfo2(t, methodName, bindingFlags);
		}

		public static MethodInfo GetMethodInfo(Type t, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.All, params Type[] sourceParameterTypes)
		{
			return ReflectionUtil.GetMethodInfo(t, methodName, bindingFlags, null, sourceParameterTypes);
		}

		public static MethodInfo GetGenericMethodInfo2(Type t, string methodName, Type[] genericTypes,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetGenericMethodInfo2(t, methodName, genericTypes, bindingFlags);
		}

		public static MethodInfo GetGenericMethodInfo(Type t, string methodName, Type[] genericTypes,
			BindingFlags bindingFlags = BindingFlagsConst.All, params Type[] sourceParameterTypes)
		{
			return ReflectionUtil.GetGenericMethodInfo(t, methodName, genericTypes, bindingFlags, null,
				sourceParameterTypes);
		}

		public static MethodInfo GetExtensionMethodInfo2(Type type, string methodName)
		{
			return ExtensionUtil.GetExtensionMethodInfo2(type, methodName);
		}

		public static MethodInfo GetExtensionMethodInfo(Type type, string methodName,
			params Type[] sourceParameterTypes)
		{
			return ExtensionUtil.GetExtensionMethodInfo(type, methodName, sourceParameterTypes);
		}

		public static MethodInfo GetExtensionGenericMethodInfo2(Type type, string methodName,
			Type[] genericTypes)
		{
			return ExtensionUtil.GetExtensionGenericMethodInfo2(type, methodName, genericTypes);
		}

		public static MethodInfo GetExtensionGenericMethodInfo(Type type, string methodName, Type[] genericTypes,
			params Type[] sourceParameterTypes)
		{
			return ExtensionUtil.GetExtensionGenericMethodInfo(type, methodName, genericTypes, sourceParameterTypes);
		}


		public static PropertyInfo GetPropertyInfo(Type t, string propertyName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetPropertyInfo(t, propertyName, bindingFlags);
		}

		public static FieldInfo GetFieldInfo(Type t, string fieldName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetFieldInfo(t, fieldName, bindingFlags);
		}

		public static object CreateInstance(Type t, params object[] args)
		{
			var obj = Activator.CreateInstance(t, args); //根据类型创建实例
			return obj; //类型转换并返回
		}

		public static T CreateInstance<T>(Type t, params object[] args)
		{
			return (T)CreateInstance(t, args);
		}

		#endregion
	}
}