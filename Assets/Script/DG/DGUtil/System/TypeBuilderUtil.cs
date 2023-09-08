using System;
using System.Reflection;
using System.Reflection.Emit;

namespace DG
{
	public static class TypeBuilderUtil
	{
		//TypeBuilder.CreateType¼´¿ÉÉú³Étype
		public static TypeBuilder GetTypeBuilder(string typeName = null,
			TypeAttributes typeAttributes = TypeAttributes.Public | TypeAttributes.Class, Type parentType = null,
			Type[] interfaceTypes = null, ModuleBuilder moduleBuilder = null)
		{
			if (moduleBuilder == null)
				moduleBuilder = ModuleBuilderUtil.GetModuleBuilder();
			if (typeName == null)
				typeName = Guid.NewGuid().ToString().Replace(StringConst.String_Minus, StringConst.String_Empty);
			return moduleBuilder.DefineType(typeName, typeAttributes, parentType, interfaceTypes);
		}
	}
}

