using System;
using System.Collections.Generic;
using System.Reflection;

namespace DG
{
	public partial class ExtensionUtil
	{
		public static MethodInfo[] GetExtensionMethodInfos(Type extendedType, Assembly assembly = null)
		{
			if (assembly == null)
				assembly = Assembly.GetAssembly(typeof(ExtensionUtil));
			var list = new List<MethodInfo>();
			var types = assembly.GetTypes();
			for (var i = 0; i < types.Length; i++)
			{
				var type = types[i];
				if (type.IsGenericType || type.IsNested) continue;
				for (var j = 0;
					j < type.GetMethods(BindingFlags.Static
											| BindingFlags.Public | BindingFlags.NonPublic).Length;
					j++)
				{
					var methodInfo = type.GetMethods(BindingFlags.Static
													 | BindingFlags.Public | BindingFlags.NonPublic)[j];
					if (methodInfo.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false) &&
						methodInfo.GetParameters()[0].ParameterType == extendedType)
						list.Add(methodInfo);
				}
			}

			return list.ToArray();
		}


		public static MethodInfo GetExtensionMethodInfo2(Type type, string methodName)
		{
			return ReflectionUtil.GetMethodInfo2(type, methodName, BindingFlagsConst.All,
				() =>
				{
					var methodInfos = GetExtensionMethodInfos(type);
					if (methodInfos.IsNullOrEmpty()) return null;
					for (var i = 0; i < methodInfos.Length; i++)
					{
						var methodInfo = methodInfos[i];
						if (methodInfo.Name.Equals(methodName))
							return methodInfo;
					}

					return null;
				});
		}

		public static MethodInfo GetExtensionMethodInfo(Type type, string methodName,
			params Type[] sourceParameterTypes)
		{
			return ReflectionUtil.GetMethodInfo(type, methodName, BindingFlagsConst.All,
				() => GetExtensionMethodInfos(type), sourceParameterTypes);
		}


		public static MethodInfo GetExtensionMethodInfo(Type type, string methodName,
			params object[] sourceParameters)
		{
			return ReflectionUtil.GetMethodInfo(type, methodName, BindingFlagsConst.All,
				() => GetExtensionMethodInfos(type), sourceParameters);
		}

		public static MethodInfo GetExtensionMethodInfo(string fullClassPath, string methodName,
			string dllName = null, params object[] sourceParameters)
		{
			return ReflectionUtil.GetMethodInfo(fullClassPath, methodName, BindingFlagsConst.All,
				() => GetExtensionMethodInfos(TypeUtil.GetType(fullClassPath, dllName)), dllName,
				sourceParameters);
		}

		public static MethodInfo GetExtensionMethodInfo(string fullClassPath, string methodName,
			Assembly assembly = null, params object[] sourceParameters)
		{
			Type type = null;
			type = assembly != null ? assembly.GetType(fullClassPath) : TypeUtil.GetType(fullClassPath);
			return type == null
				? null
				: ReflectionUtil.GetMethodInfo(fullClassPath, methodName, BindingFlagsConst.All,
					() => GetExtensionMethodInfos(type), assembly, sourceParameters);
		}

		public static T InvokeExtension<T>(object obj, string fullClassPath, string methodInfoString,
			bool isMissNotInvoke = true, params object[] parameters)
		{
			object[] parameters2 = new object[parameters.Length + 1];
			parameters2[0] = obj;
			Array.Copy(parameters, 0, parameters2, 1, parameters.Length);
			MethodInfo methodInfo = GetExtensionMethodInfo(fullClassPath, methodInfoString,
				ReflectionUtil.GetReflectionType(obj).Assembly, parameters2);
			return methodInfo == null && isMissNotInvoke
				? default(T)
				: ReflectionUtil.Invoke<T>(obj, methodInfo, parameters2);
		}

		public static T InvokeExtension<T>(object obj, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return InvokeExtension<T>(obj, ReflectionUtil.GetReflectionType(obj).FullName, methodName,
				isMissNotInvoke,
				parameters);
		}

		public static void InvokeExtension(object obj, string fullClassPath, string methodInfoString,
			bool isMissNotInvoke = true, params object[] parameters)
		{
			object[] parameters2 = new object[parameters.Length + 1];
			parameters2[0] = obj;
			Array.Copy(parameters, 0, parameters2, 1, parameters.Length);
			MethodInfo methodInfo = GetExtensionMethodInfo(fullClassPath, methodInfoString,
				ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters2);
			if (methodInfo == null && isMissNotInvoke)
				return;
			ReflectionUtil.Invoke<object>(obj, methodInfo, parameters2);
		}

		public static void InvokeExtension(object obj, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			InvokeExtension(obj, ReflectionUtil.GetReflectionType(obj).FullName, methodName, isMissNotInvoke,
				ReflectionUtil.GetReflectionType(obj).Assembly.FullName, parameters);
		}
	}
}