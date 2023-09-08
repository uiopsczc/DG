using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace DG
{
	public static class AssemblyBuilderUtil
	{
		private static readonly Dictionary<(string, AssemblyBuilderAccess), AssemblyBuilder> _assemblyBuilderDict =
			new Dictionary<(string, AssemblyBuilderAccess), AssemblyBuilder>();

		public static AssemblyBuilder GetAssemblyBuilder(string assemblyNameString = null,
			AssemblyBuilderAccess assemblyBuilderAccess = AssemblyBuilderAccess.Run)
		{
			var infos = (assemblyNameString, assemblyBuilderAccess);
			if (_assemblyBuilderDict.TryGetValue(infos, out var result))
				return result;

			(string, AssemblyBuilderAccess) infos2 = default;
			if (assemblyNameString == null)
			{
				assemblyNameString = Guid.NewGuid().ToString()
					.Replace(StringConst.String_Minus, StringConst.String_Empty);
				infos2 = (assemblyNameString, assemblyBuilderAccess);
				if (_assemblyBuilderDict.TryGetValue(infos2, out result))
					return result;
			}

			var assemblyName = new AssemblyName(assemblyNameString);
			var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, assemblyBuilderAccess);
			_assemblyBuilderDict[infos] = assembly;
			if (infos2!=default)
				_assemblyBuilderDict[infos2] = assembly;
			return assembly;
		}
	}
}

