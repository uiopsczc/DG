using System;
using System.Reflection;

namespace DG
{
    public class AOPUtil
    {
        /// <summary>
        /// 获取目标参数方法顺序
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="sourceMethodName"></param>
        /// <param name="aopMethodType"></param>
        /// <returns></returns>
        private static string[] GetSearchTargetMethodNameOrders(Type sourceType, string sourceMethodName,
            EAOPMethodType aopMethodType)
        {
            string[] result = new string[AOPConst.Seach_Format_Target_Method_Name_Orders.Length + 1];
            for (int i = 0; i < AOPConst.Seach_Format_Target_Method_Name_Orders.Length; i++)
                result[i] = GetTargetMethodName(AOPConst.Seach_Format_Target_Method_Name_Orders[i], sourceType,
                    sourceMethodName,
                    aopMethodType);
            // 再加上默认的处理方法
            result[AOPConst.Seach_Format_Target_Method_Name_Orders.Length] = aopMethodType.ToString();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formatTargetMethodName"></param>
        /// <param name="sourceType"></param>
        /// <param name="sourceMethodName"></param>
        /// <param name="aopMethodType"></param>
        /// <returns></returns>
        private static string GetTargetMethodName(string formatTargetMethodName, Type sourceType,
            string sourceMethodName, EAOPMethodType aopMethodType)
        {
            return string.Format(formatTargetMethodName, sourceType.GetLastName(), sourceMethodName,
                aopMethodType.ToString());
        }


        /// 特殊情况调用优先顺序
        /// 1.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型(【被切面的方法的类本身】+被切面方法的参数)
        ///   1.1.被切面的方法的名称_AOPMethodType的类型（【被切面的方法的类本身】+被切面方法的参数）
        /// 2.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型(被切面方法的参数) 
        ///   2.1.被切面的方法的名称_AOPMethodType的类型（被切面方法的参数）
        /// 3.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型(【被切面的方法的类本身】)
        ///   3.1.被切面的方法的名称_AOPMethodType的类型（【被切面的方法的类本身】）
        /// 4.被切面的方法的名称_AOPMethodType的类型（）
        ///   4.1.被切面的方法的类_被切面的方法的名称_AOPMethodType的类型()
        /// 5.默认的处理方法
        public static MethodInfoProxy SearchTargetMethodInfoProxy(Type aopAttributeType, Type sourceType,
            string sourceMethodName, EAOPMethodType aopMethodType, Type[] sourceMethodArgTypes)
        {
            //从特殊到一般，注意有顺序先后的查找
            var names = GetSearchTargetMethodNameOrders(sourceType, sourceMethodName,
                aopMethodType);
            for (var i = 0; i < names.Length; i++)
            {
                string targetMethodName = names[i];
                for (var j = 0; j < AOPConst.Is_Target_Method_With_Source_ArgTypes_Orders.Length; j++)
                {
                    bool isTargetMethodWithSourceArgType = AOPConst.Is_Target_Method_With_Source_ArgTypes_Orders[j];
                    for (var k = 0; k < AOPConst.Is_Target_Method_Self_Arg_Orders.Length; k++)
                    {
                        bool isTargetMethodSelfArg = AOPConst.Is_Target_Method_Self_Arg_Orders[k];
                        MethodInfoProxy methodInfoProxy = new MethodInfoProxy(targetMethodName, aopAttributeType,
                            sourceType,
                            isTargetMethodSelfArg, isTargetMethodWithSourceArgType, sourceMethodArgTypes);
                        MethodInfo targetMethod = aopAttributeType.GetMethodInfo(targetMethodName,
                            BindingFlagsConst.ALL,
                            methodInfoProxy.methodArgTypesProxy.targetMethodArgTypes);
                        if (targetMethod != null) //命中
                            return methodInfoProxy;
                    }
                }
            }

            throw new Exception(string.Format("can not find AOPAttributeMethod of  Method:{0}->{1}  AOPAttribute:{2}",
                sourceType, sourceMethodName, aopAttributeType));
        }
    }
}