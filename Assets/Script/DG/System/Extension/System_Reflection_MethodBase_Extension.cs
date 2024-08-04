using System;
using System.Reflection;

namespace DG
{
	public static class System_Reflection_MethodBase_Extension
	{
		public static Type[] GetParameterTypes(this MethodBase self)
		{
			return MethodBaseUtil.GetParameterTypes(self);
		}
	}
}