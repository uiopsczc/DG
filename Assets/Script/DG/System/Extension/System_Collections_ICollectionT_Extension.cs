using System;
using System.Collections.Generic;

namespace DG
{
    public static class System_Collections_ICollectionT_Extension
    {
        /// <summary>
        /// 转化为ToLinkedHashtable
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static object ToLinkedHashtable2<T>(this ICollection<T> self)
        {
            return ICollectionTUtil.ToLinkedHashtable2(self);
        }

        /// <summary>
        /// 用于不同类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this ICollection<T> self)
        {
            return ICollectionTUtil.ToList(self);
        }

        /// <summary>
        /// 用于不同类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this ICollection<T> self)
        {
            return ICollectionTUtil.ToArray(self);
        }


        public static T[] ToArray<U, T>(this ICollection<U> self, Func<U, T> covertElementFunc)
        {
            return ICollectionTUtil.ToArray(self, covertElementFunc);
        }
    }
}