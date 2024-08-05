using System;
using System.Reflection;
using System.Reflection.Emit;

namespace DG
{
    public static class TypeBuilderUtil
    {
        //TypeBuilder.CreateType即可生成type
        public static TypeBuilder GetTypeBuilder(string typeName = null,
            TypeAttributes typeAttributes = TypeAttributes.Public | TypeAttributes.Class, Type parentType = null,
            Type[] interfaceTypes = null, ModuleBuilder moduleBuilder = null)
        {
            if (moduleBuilder == null)
                moduleBuilder = ModuleBuilderUtil.GetModuleBuilder();
            typeName ??= Guid.NewGuid().ToString().Replace(StringConst.STRING_MINUS, StringConst.STRING_EMPTY);
            return moduleBuilder.DefineType(typeName, typeAttributes, parentType, interfaceTypes);
        }
    }
}