using System.Collections.Generic;
using System.Reflection.Emit;

namespace DG
{
	public static class ModuleBuilderUtil
	{
		private static Dictionary<AssemblyBuilder, ModuleBuilder> _moduleBuilderDict = new();

		public static ModuleBuilder GetModuleBuilder(AssemblyBuilder assemblyBuilder = null)
		{
			if (assemblyBuilder == null)
				assemblyBuilder = AssemblyBuilderUtil.GetAssemblyBuilder();
			if (_moduleBuilderDict.TryGetValue(assemblyBuilder, out var result))
				return result;
			result = assemblyBuilder.DefineDynamicModule(assemblyBuilder.GetName().Name);
			_moduleBuilderDict[assemblyBuilder] = result;
			return result;
		}
	}
}

