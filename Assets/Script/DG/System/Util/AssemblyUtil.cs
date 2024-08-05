using System;
using System.Collections.Generic;
using System.Reflection;

namespace DG
{
    public static class AssemblyUtil
    {
        /// <summary>
        /// 获得NameSpace下的所有类,例如：Assembly.GetExecutingAssembly().GetClassesOfNameSpace("cat.io");
        /// </summary>
        public static Type[] GetTypesOfNameSpace(Assembly assembly, string targetNamespace)
        {
            List<Type> typeList = new List<Type>();
            var types = assembly.GetTypes();
            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (targetNamespace.Equals(type.Namespace))
                    typeList.Add(type);
            }

            return typeList.ToArray();
        }


        public static MemberInfo[] GetCustomAttributeMemberInfos<T>(Assembly assembly)
        {
            List<MemberInfo> result = new List<MemberInfo>();
            var types = assembly.GetTypes();
            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];
                for (var j = 0; j < type.GetMembers(BindingFlagsConst.ALL).Length; j++)
                {
                    var memberInfo = type.GetMembers(BindingFlagsConst.ALL)[j];
                    if (memberInfo.GetCustomAttribute<T>() == null) continue;
                    result.Add(memberInfo);
                }
            }

            return result.ToArray();
        }


        public static Assembly GetAssembly(string assemblyName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (var i = 0; i < assemblies.Length; i++)
            {
                var assembly = assemblies[i];
                if (assembly.GetName().Name.Equals(assemblyName))
                    return assembly;
            }

            return null;
        }
    }
}