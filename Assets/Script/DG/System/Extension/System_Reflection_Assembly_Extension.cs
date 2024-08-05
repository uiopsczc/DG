using System;
using System.Reflection;

namespace DG
{
    public static class System_Reflection_Assembly_Extension
    {
        /// <summary>
        /// 获得NameSpace下的所有类,例如：Assembly.GetExecutingAssembly().GetClassesOfNameSpace("cat.io");
        /// </summary>
        public static Type[] GetTypesOfNameSpace(this Assembly self, string targetNamespace)
        {
            return AssemblyUtil.GetTypesOfNameSpace(self, targetNamespace);
        }


        public static MemberInfo[] GetCustomAttributeMemberInfos<T>(this Assembly self)
        {
            return AssemblyUtil.GetCustomAttributeMemberInfos<T>(self);
        }
    }
}