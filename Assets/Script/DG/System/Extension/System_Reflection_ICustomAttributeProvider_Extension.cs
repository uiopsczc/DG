using System.Reflection;

namespace DG
{
    public static class System_Reflection_ICustomAttributeProvider_Extension
    {
        public static T GetCustomAttribute<T>(this ICustomAttributeProvider provider, int index = 0,
            bool isContainInherit = false)
        {
            return ICustomAttributeProviderUtil.GetCustomAttribute<T>(provider, index, isContainInherit);
        }
    }
}