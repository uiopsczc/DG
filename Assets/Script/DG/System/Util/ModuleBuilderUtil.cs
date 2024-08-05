using System.Collections.Generic;
using System.Reflection.Emit;

namespace DG
{
    public static class ModuleBuilderUtil
    {
        private static readonly Dictionary<AssemblyBuilder, ModuleBuilder> _assemblyBuilder2ModuleBuilder = new();

        public static ModuleBuilder GetModuleBuilder(AssemblyBuilder assemblyBuilder = null)
        {
            if (assemblyBuilder == null)
                assemblyBuilder = AssemblyBuilderUtil.GetAssemblyBuilder();
            if (_assemblyBuilder2ModuleBuilder.TryGetValue(assemblyBuilder, out var result))
                return result;
            result = assemblyBuilder.DefineDynamicModule(assemblyBuilder.GetName().Name);
            _assemblyBuilder2ModuleBuilder[assemblyBuilder] = result;
            return result;
        }
    }
}