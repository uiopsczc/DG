using System.Reflection;

namespace DG
{
    public static class ICustomAttributeProviderUtil
    {
        public static T GetCustomAttribute<T>(ICustomAttributeProvider provider, int index = 0,
            bool isContainInherit = false)
        {
            var customAttributes = provider.GetCustomAttributes(typeof(T), isContainInherit);
            return customAttributes.Length > index ? (T)customAttributes[index] : default;
        }
    }
}