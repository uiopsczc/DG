using System;
using System.Reflection;

namespace DG
{
	public static class System_Reflection_Assembly_Extension
	{
		/// <summary>
		/// ���NameSpace�µ�������,���磺Assembly.GetExecutingAssembly().GetClassesOfNameSpace("cat.io");
		/// </summary>
		public static Type[] GetTypesOfNameSpace(this Assembly self, string targetNamespace)
		{
			return AssemblyUtil.GetTypesOfNameSpace(self, targetNamespace);
		}


		public static MemberInfo[] GetCustomAttributeMemberInfos<T>(this  Assembly self)
		{
			return AssemblyUtil.GetCustomAttributeMemberInfos<T>(self);
		}
	}
}