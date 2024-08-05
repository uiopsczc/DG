using System.Collections;
using System.Collections.Generic;

namespace DG
{
    public static class System_Collections_ICollection_Extension
    {
        public static bool IsNullOrEmpty(this ICollection self)
        {
            return ICollectionUtil.IsNullOrEmpty(self);
        }

        public static T[] ToArray<T>(this ICollection self)
        {
            return ICollectionUtil.ToArray<T>(self);
        }

        public static List<T> ToList<T>(this ICollection self)
        {
            return ICollectionUtil.ToList<T>(self);
        }


        #region DGToString

        public static string DGToString(this ICollection self, bool isFillStringWithDoubleQuote = false)
        {
            return ICollectionUtil.DGToString(self, isFillStringWithDoubleQuote);
        }

        #endregion
    }
}