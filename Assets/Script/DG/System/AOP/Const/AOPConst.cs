namespace DG
{
    public class AOPConst
    {
        ///维度1 目标参数方法名维度
        ///处理两种方法名的情况
        ///		1.方法名：被切面的方法的类_被切面的方法的名称_AOPMethodType的类型 
        ///     2.方法名：被切面的方法的名称_AOPMethodType的类型 
        public static readonly string[] Seach_Format_Target_Method_Name_Orders =
        {
            "{0}_{1}_{2}",
            "{1}_{2}",
        };

        ///维度2 目标函数的参数维度-是否 目标函数的参数列表 带有 原函数的参数列表
        ///处理两种方法名的情况
        ///		1.参数列表：带有原函数的参数列表
        ///     2.参数列表：不带有原函数的参数列表
        public static bool[] Is_Target_Method_With_Source_ArgTypes_Orders =
        {
            true,
            false
        };

        ///维度3 目标函数的参数维度-是否 目标函数的参数列表 带有 原函数所在类的实例引用self
        ///处理两种方法名的情况
        ///		1.参数列表：带原函数所在类的实例引用self
        ///     2.参数列表：不原函数所在类的实例引用self
        public static bool[] Is_Target_Method_Self_Arg_Orders =
        {
            true,
            false
        };
    }
}