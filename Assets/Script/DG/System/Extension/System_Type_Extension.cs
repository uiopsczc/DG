using System;
using System.Reflection;

namespace DG
{
	public static partial class System_Type_Extension
	{
		/// <summary>
		///   type是否是subType的父类
		/// </summary>
		public static bool IsSuperTypeOf(this Type self, Type subType)
		{
			return TypeUtil.IsSuperTypeOf(self, subType);
		}

		/// <summary>
		///   type是否是parentType的子类
		/// </summary>
		/// <param name="self"></param>
		/// <param name="parentType"></param>
		/// <returns></returns>
		public static bool IsSubTypeOf(this Type self, Type parentType)
		{
			return TypeUtil.IsSubTypeOf(self, parentType);
		}


		public static string GetDescription(this Type self, string fieldName)
		{
			return TypeUtil.GetDescription(self, fieldName);
		}

		public static string GetDescription(this Type self, int enumValue)
		{
			return TypeUtil.GetDescription(self, enumValue);
		}


		public static string GetLastName(this Type self)
		{
			return TypeUtil.GetLastName(self);
		}


		public static object DefaultValue(this Type self)
		{
			return TypeUtil.DefaultValue(self);
		}


		#region 反射相关

		public static MethodInfo GetMethodInfo2(this Type self, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.ALL)
		{
			return TypeUtil.GetMethodInfo2(self, methodName, bindingFlags);
		}

		public static MethodInfo GetMethodInfo(this Type self, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.ALL, params Type[] sourceParameterTypes)
		{
			return TypeUtil.GetMethodInfo(self, methodName, bindingFlags, sourceParameterTypes);
		}

		public static MethodInfo GetGenericMethodInfo2(this Type self, string methodName, Type[] genericTypes,
			BindingFlags bindingFlags = BindingFlagsConst.ALL)
		{
			return TypeUtil.GetGenericMethodInfo2(self, methodName, genericTypes, bindingFlags);
		}

		public static MethodInfo GetGenericMethodInfo(this Type self, string methodName, Type[] genericTypes,
			BindingFlags bindingFlags = BindingFlagsConst.ALL, params Type[] sourceParameterTypes)
		{
			return TypeUtil.GetGenericMethodInfo(self, methodName, genericTypes, bindingFlags, sourceParameterTypes);
		}

		/*************************************************************************************
		* 模块描述:ExtensionMethod
		*************************************************************************************/
		public static MethodInfo GetExtensionMethodInfo2(this Type self, string methodName)
		{
			return TypeUtil.GetExtensionMethodInfo2(self, methodName);
		}

		public static MethodInfo GetExtensionMethodInfo(this Type self, string methodName,
			params Type[] sourceParameterTypes)
		{
			return TypeUtil.GetExtensionMethodInfo(self, methodName, sourceParameterTypes);
		}

		public static MethodInfo GetExtensionGenericMethodInfo2(this Type self, string methodName,
			Type[] genericTypes)
		{
			return TypeUtil.GetExtensionGenericMethodInfo2(self, methodName, genericTypes);
		}

		public static MethodInfo GetExtensionGenericMethodInfo(this Type self, string methodName, Type[] genericTypes,
			params Type[] sourceParameterTypes)
		{
			return TypeUtil.GetExtensionGenericMethodInfo(self, methodName, genericTypes, sourceParameterTypes);
		}


		public static PropertyInfo GetPropertyInfo(this Type self, string propertyName,
			BindingFlags bindingFlags = BindingFlagsConst.ALL)
		{
			return TypeUtil.GetPropertyInfo(self, propertyName, bindingFlags);
		}

		public static FieldInfo GetFieldInfo(this Type self, string fieldName,
			BindingFlags bindingFlags = BindingFlagsConst.ALL)
		{
			return TypeUtil.GetFieldInfo(self, fieldName, bindingFlags);
		}

		public static object CreateInstance(this Type self, params object[] args)
		{
			return TypeUtil.CreateInstance(self, args);
		}

		public static T CreateInstance<T>(this Type self, params object[] args)
		{
			return TypeUtil.CreateInstance<T>(self, args);
		}

		#endregion
	}
}