using System;
using System.Collections.Generic;
using System.Reflection;

namespace DG
{
	public static class MethodBaseUtil
	{
		public static Type[] GetParameterTypes(MethodBase methodBase)
		{
			var parameterInfos = methodBase.GetParameters();
			var result = new List<Type>(parameterInfos.Length);
			for (var i = 0; i < parameterInfos.Length; i++)
			{
				var parameterInfo = parameterInfos[i];
				result.Add(parameterInfo.ParameterType);
			}

			return result.ToArray();
		}

	}
}

