using System;
using System.Reflection;

namespace DG
{
    public partial class ReflectionUtil
    {
        public static object CreateGenericInstance(Type[] genericTypes, string fullClassPath, string dllName = null,
            params object[] args)
        {
            Type type = TypeUtil.GetType(fullClassPath, dllName);
            type = type.MakeGenericType(genericTypes);
            object obj = Activator.CreateInstance(type, args); //根据类型创建实例
            return obj; //类型转换并返回
        }


        public static MethodInfo GetGenericMethodInfo2(Type type, string methodName, Type[] genericTypes,
            BindingFlags bindingFlags = BindingFlagsConst.ALL, Func<MethodInfo> getMethodInfoFunc = null)
        {
            MethodInfo result = GetGenericMethodInfoCache2(type, methodName, genericTypes);
            if (result != null)
                return result;

            result = getMethodInfoFunc == null
                ? type.GetMethod(methodName, bindingFlags)
                : getMethodInfoFunc();
            result = result.MakeGenericMethod(genericTypes);
            SetGenericMethodInfoCache2(type, methodName, genericTypes, result);
            return result;
        }

        public static MethodInfo GetGenericMethodInfo(Type type, string methodName, Type[] genericTypes
            , BindingFlags bindingFlags = BindingFlagsConst.ALL, Func<MethodInfo[]> getMethodInfosFunc = null,
            params Type[] sourceParameterTypes)
        {
            MethodInfo result1 = GetGenericMethodInfoCache(type, methodName, genericTypes, sourceParameterTypes);
            if (result1 != null)
                return result1;

            MethodInfo[] methodInfos =
                getMethodInfosFunc == null ? type.GetMethods(bindingFlags) : getMethodInfosFunc();
            if (methodInfos.IsNullOrEmpty())
            {
                SetGenericMethodInfoCache(type, methodName, genericTypes, sourceParameterTypes, null);
                return null;
            }

            for (var m = 0; m < methodInfos.Length; m++)
            {
                MethodInfo methodInfo = methodInfos[m];
                if (methodInfo.Name.Equals(methodName))
                {
                    ParameterInfo[] methodInfoParameters = methodInfo.GetParameters();
                    if (methodInfoParameters.Length == 0)
                    {
                        if (sourceParameterTypes.Length > 0)
                            continue;
                        var result2 = methodInfo.MakeGenericMethod(genericTypes);
                        SetGenericMethodInfoCache(type, methodName, genericTypes, sourceParameterTypes, result2);
                        return result2;
                    }

                    ParameterInfo finalMethodInfoParameter = methodInfoParameters[methodInfoParameters.Length - 1];
                    bool isFinalParams =
                        finalMethodInfoParameter.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0;
                    bool isContinueThisRound = false;
                    int maxParametersCount = Math.Max(sourceParameterTypes.Length, methodInfoParameters.Length);
                    Type sourceParameterType;
                    Type targetParameterType;
                    ParameterInfo targetParameterInfo;
                    if (!isFinalParams && sourceParameterTypes.Length > methodInfoParameters.Length)
                        continue;
                    for (int i = 0; i < maxParametersCount; i++)
                    {
                        if (i < sourceParameterTypes.Length)
                        {
                            sourceParameterType = sourceParameterTypes[i];
                            targetParameterInfo = i < methodInfoParameters.Length - 1
                                ? methodInfoParameters[i]
                                : methodInfoParameters[methodInfoParameters.Length - 1];
                            targetParameterType = targetParameterInfo.ParameterType;
                            if (i >= methodInfoParameters.Length - 1 && isFinalParams)
                                targetParameterType = targetParameterType.GetElementType();
                            if (targetParameterType.IsGenericTypeDefinition || targetParameterType.IsGenericType ||
                                targetParameterType.IsGenericParameter ||
                                ConvertUtil.CanConvertToType(sourceParameterType, targetParameterType)) continue;
                            isContinueThisRound = true;
                            break;
                        }

                        targetParameterInfo = methodInfoParameters[i];

                        if (targetParameterInfo.IsOptional) continue;
                        if (i == methodInfoParameters.Length - 1 &&
                            (i != methodInfoParameters.Length - 1 || isFinalParams)) continue;
                        isContinueThisRound = true;
                        break;
                    }

                    if (isContinueThisRound)
                        continue;
                    var result = methodInfo.MakeGenericMethod(genericTypes);
                    SetGenericMethodInfoCache(type, methodName, genericTypes, sourceParameterTypes, result);
                    var methodInfoParameterTypes = new Type[methodInfoParameters.Length];
                    for (var h = 0; h < methodInfoParameters.Length; h++)
                    {
                        var methodInfoParameter = methodInfoParameters[h];
                        methodInfoParameterTypes[h] = methodInfoParameter.ParameterType;
                    }

                    SetGenericMethodInfoCache(type, methodName, genericTypes,
                        methodInfoParameterTypes,
                        result);
                    return result;
                }
            }

            SetGenericMethodInfoCache(type, methodName, genericTypes, sourceParameterTypes, null);
            return null;
        }


        public static MethodInfo GetGenericMethodInfo(Type type, string methodName, Type[] genericTypes,
            BindingFlags bindingFlags = BindingFlagsConst.ALL, Func<MethodInfo[]> getMethodInfosFunc = null,
            params object[] sourceParameters)
        {
            var sourceParameterCount = sourceParameters.Length;
            var sourceParameterTypes = new Type[sourceParameterCount];
            for (var i = 0; i < sourceParameterCount; i++)
            {
                var sourceParameter = sourceParameters[i];
                sourceParameterTypes[i] = sourceParameter?.GetType();
            }

            return GetGenericMethodInfo(type, methodName, genericTypes, bindingFlags,
                getMethodInfosFunc, sourceParameterTypes);
        }

        public static MethodInfo GetGenericMethodInfo(string fullClassPath, string methodName, Type[] genericTypes,
            BindingFlags bindingFlags = BindingFlagsConst.ALL, Func<MethodInfo[]> getMethodInfosFunc = null,
            string dllName = null, params object[] sourceParameters)
        {
            Type type = TypeUtil.GetType(fullClassPath, dllName);

            if (type == null)
                return null;

            return GetGenericMethodInfo(type, methodName, genericTypes, bindingFlags, getMethodInfosFunc,
                sourceParameters);
        }

        public static MethodInfo GetGenericMethodInfo(string fullClassPath, string methodName, Type[] genericTypes,
            BindingFlags bindingFlags = BindingFlagsConst.ALL, Func<MethodInfo[]> getMethodInfosFunc = null,
            Assembly assembly = null, params object[] sourceParameters)
        {
            Type type = null;
            type = assembly != null ? assembly.GetType(fullClassPath) : TypeUtil.GetType(fullClassPath);
            return type == null
                ? null
                : GetGenericMethodInfo(type, methodName, genericTypes, bindingFlags, getMethodInfosFunc,
                    sourceParameters);
        }


        public static T InvokeGeneric<T>(object obj, string fullClassPath, string methodInfoString,
            Type[] genericTypes,
            bool isMissNotInvoke = true, params object[] parameters)
        {
            MethodInfo methodInfo = GetGenericMethodInfo(fullClassPath, methodInfoString, genericTypes,
                GetReflectionObject(obj) == null ? BindingFlagsConst.STATIC : BindingFlagsConst.INSTANCE, null,
                GetReflectionType(obj).Assembly, parameters);
            if (methodInfo == null && isMissNotInvoke)
                return default(T);
            return Invoke<T>(obj, methodInfo, parameters);
        }

        public static T InvokeGeneric<T>(object obj, string methodName, Type[] genericTypes,
            bool isMissNotInvoke = true,
            params object[] parameters)
        {
            return InvokeGeneric<T>(obj, GetReflectionType(obj).FullName, methodName, genericTypes,
                isMissNotInvoke,
                parameters);
        }

        public static void InvokeGeneric(object obj, string fullClassPath, string methodInfoString,
            Type[] genericTypes,
            bool isMissNotInvoke = true, params object[] parameters)
        {
            MethodInfo methodInfo = GetGenericMethodInfo(fullClassPath, methodInfoString, genericTypes,
                GetReflectionObject(obj) == null ? BindingFlagsConst.STATIC : BindingFlagsConst.INSTANCE, null,
                GetReflectionType(obj).Assembly.FullName, parameters);
            if (methodInfo == null && isMissNotInvoke)
                return;
            Invoke<object>(obj, methodInfo, parameters);
        }

        public static void InvokeGeneric(object obj, string methodName, Type[] genericTypes,
            bool isMissNotInvoke = true,
            params object[] parameters)
        {
            InvokeGeneric(obj, GetReflectionType(obj).FullName, methodName, genericTypes,
                isMissNotInvoke,
                GetReflectionType(obj).Assembly.FullName, parameters);
        }
    }
}