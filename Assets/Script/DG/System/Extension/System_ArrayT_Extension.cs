using System;
using System.Collections.Generic;

namespace DG
{
    public static class System_ArrayT_Extension
    {
        public static T[] GetArrayByIndexList<T>(this T[] self, List<int> indexList)
        {
            return ArrayTUtil.GetArrayByIndexList(self, indexList);
        }

        public static T[] GetArrayByIndexes<T>(this T[] self, int[] indexes, int? indexesLength)
        {
            return ArrayTUtil.GetArrayByIndexes(self, indexes, indexesLength);
        }

        /// <summary>
        /// 将数组转化为List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this T[] self)
        {
            return ArrayTUtil.ToList(self);
        }

        public static T[] EmptyIfNull<T>(this T[] self)
        {
            return ArrayTUtil.EmptyIfNull(self);
        }

        public static T[] RemoveEmpty<T>(this T[] self)
        {
            return ArrayTUtil.RemoveEmpty(self);
        }

        public static void Swap<T>(this T[] self, int index1, int index2)
        {
            ArrayTUtil.Swap(self, index1, index2);
        }

        //超过index或者少于0的循环index表获得
        public static T GetByLoopIndex<T>(this T[] self, int index)
        {
            return ArrayTUtil.GetByLoopIndex(self, index);
        }

        //超过index或者少于0的循环index表设置
        public static void SetByLoopIndex<T>(this T[] self, int index, T value)
        {
            ArrayTUtil.SetByLoopIndex(self, index, value);
        }

        public static bool Contains<T>(this T[] self, T target)
        {
            return ArrayTUtil.Contains(self, target);
        }

        public static bool ContainsIndex<T>(this T[] self, int index)
        {
            return ArrayTUtil.ContainsIndex(self, index);
        }

        /// <summary>
        /// 使其内元素单一
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        /// <returns></returns>
        public static T[] Unique<T>(this T[] self)
        {
            return ArrayTUtil.Unique(self);
        }

        public static T[] Combine<T>(this T[] self, T[] another)
        {
            return ArrayTUtil.Combine(self, another);
        }

        /// <summary>
        /// 将多个数组合成一个数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="arrs"></param>
        /// <returns></returns>
        public static T[] Combine<T>(this T[] self, bool isUnique = false, params T[][] arrs)
        {
            return ArrayTUtil.Combine(self, isUnique, arrs);
        }

        public static T[] Combine<T>(this T[] self, params T[][] arrs)
        {
            return ArrayTUtil.Combine(self, arrs);
        }


        public static T[] Push<T>(this T[] self, T t)
        {
            return ArrayTUtil.Push(self, t);
        }

        public static T Peek<T>(this T[] self)
        {
            return ArrayTUtil.Peek(self);
        }

        public static (T element, T[] array) Pop<T>(this T[] self)
        {
            return ArrayTUtil.Pop(self);
        }

        public static T First<T>(this T[] self)
        {
            return ArrayTUtil.First(self);
        }

        public static T Last<T>(this T[] self)
        {
            return ArrayTUtil.Last(self);
        }

        /// <summary>
        ///   在self中找subArray的开始位置
        /// </summary>
        /// <returns>-1表示没找到</returns>
        public static int IndexOfSub<T>(this T[] self, T[] subArray)
        {
            return ArrayTUtil.IndexOfSub(self, subArray);
        }

        /// <summary>
        ///   在self中只保留subArray中的元素
        /// </summary>
        public static T[] RetainElementsOfSub<T>(this T[] self, T[] subArray)
        {
            return ArrayTUtil.RetainElementsOfSub(self, subArray);
        }

        /// <summary>
        /// </summary>
        public static T[] Sub<T>(this T[] self, int fromIndex, int length)
        {
            return ArrayTUtil.Sub(self, fromIndex, length);
        }

        /// <summary>
        /// 包含fromIndx到末尾
        /// </summary>
        public static T[] Sub<T>(this T[] self, int fromIndex)
        {
            return ArrayTUtil.Sub(self, fromIndex);
        }

        /// <summary>
        /// 当set来使用，保持只有一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        public static T[] Add<T>(this T[] self, T element, bool isUnique = false)
        {
            return ArrayTUtil.Add(self, element, isUnique);
        }

        //当isUnique==true的情况下,默认toAddArray里面的元素是不重复的
        public static T[] AddRange<T>(this T[] self, T[] toAddArray, bool isUnique = false)
        {
            return ArrayTUtil.AddRange(self, toAddArray, isUnique);
        }

        public static T[] AddRange<T>(this T[] self, List<T> toAddList, bool isUnique = false)
        {
            return ArrayTUtil.AddRange(self, toAddList, isUnique);
        }


        public static T[] AddFirst<T>(this T[] self, T element, bool isUnique = false)
        {
            return ArrayTUtil.AddFirst(self, element, isUnique);
        }

        public static T[] AddLast<T>(this T[] self, T element, bool isUnique = false)
        {
            return ArrayTUtil.AddLast(self, element, isUnique);
        }

        public static T[] AddUnique<T>(this T[] self, T o)
        {
            return ArrayTUtil.AddUnique(self, o);
        }

        public static T[] RemoveFirst<T>(this T[] self)
        {
            return ArrayTUtil.RemoveFirst(self);
        }

        public static (T element, T[] array) RemoveFirst2<T>(this T[] self)
        {
            return ArrayTUtil.RemoveFirst2(self);
        }

        public static T[] RemoveLast<T>(this T[] self)
        {
            return ArrayTUtil.RemoveLast(self);
        }

        public static (T element, T[] array) RemoveLast2<T>(this T[] self)
        {
            return ArrayTUtil.RemoveLast2(self);
        }

        public static (T element, T[] array) RemoveAt2<T>(this T[] self, int removeIndex)
        {
            return ArrayTUtil.RemoveAt2(self, removeIndex);
        }

        public static T[] RemoveAt<T>(this T[] self, int removeIndex)
        {
            return ArrayTUtil.RemoveAt(self, removeIndex);
        }

        /// <summary>
        ///   删除list中的subList（subList必须要全部在list中）
        /// </summary>
        public static T[] RemoveSub<T>(this T[] self, T[] subArray)
        {
            return ArrayTUtil.RemoveSub(self, subArray);
        }

        //elements:移除掉的元素
        //array:self被移除后的数组
        public static (T[] elements, T[] array) RemoveRange2<T>(this T[] self, int removeFromIndex)
        {
            return ArrayTUtil.RemoveRange2(self, removeFromIndex);
        }

        //elements:移除掉的元素
        //array:self被移除后的数组
        public static (T[] elements, T[] array) RemoveRange2<T>(this T[] self, int removeFromIndex, int length)
        {
            return ArrayTUtil.RemoveRange2(self, removeFromIndex, length);
        }

        public static T[] RemoveRange<T>(this T[] self, int removeFromIndex)
        {
            return ArrayTUtil.RemoveRange(self, removeFromIndex);
        }

        public static T[] RemoveRange<T>(this T[] self, int removeFromIndex, int length)
        {
            return ArrayTUtil.RemoveRange(self, removeFromIndex, length);
        }

        /// <summary>
        /// 在list中删除subList中出现的元素
        /// </summary>
        public static T[] RemoveElements<T>(this T[] self, HashSet<T> hashSet)
        {
            return ArrayTUtil.RemoveElements(self, hashSet);
        }


        public static void Reverse<T>(this T[] self)
        {
            ArrayTUtil.Reverse(self);
        }

        public static void Foreach<T>(this T[] self, Action<T> action)
        {
            ArrayTUtil.Foreach(self, action);
        }

        public static T[] Clone<T>(this T[] self)
        {
            return ArrayTUtil.Clone(self);
        }


        /// <summary>
        /// 数组中target的Index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] self, T target)
        {
            return ArrayTUtil.IndexOf(self, target);
        }


        public static T[] Clone<T>(this T[] self, int startIndex, int len)
        {
            return ArrayTUtil.Clone(self, startIndex, len);
        }

        /// <summary>
        ///扩展数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        public static T[] EnlargeCapacity<T>(this T[] self, float enlargeFactor = 2f)
        {
            return ArrayTUtil.EnlargeCapacity(self, enlargeFactor);
        }

        public static T[] AddCapacity<T>(this T[] self, int add, bool isAppend = true)
        {
            return ArrayTUtil.AddCapacity(self, add, isAppend);
        }


        public static T[] Insert<T>(this T[] self, int insertIndex, T element)
        {
            return ArrayTUtil.Insert(self, insertIndex, element);
        }

        public static T[] InsertRange<T>(this T[] self, int insertIndex, T[] toAddArray)
        {
            return ArrayTUtil.InsertRange(self, insertIndex, toAddArray);
        }

        public static T[] InsertRange<T>(this T[] self, int insertIndex, List<T> toAddList)
        {
            return ArrayTUtil.InsertRange(self, insertIndex, toAddList);
        }


        #region Random 随机

        public static T Random<T>(this T[] self)
        {
            return RandomUtil.Random(self);
        }

        /// <summary>
        /// 随机list里面的元素count次
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count">个数</param>
        /// <param name="isUnique">是否唯一</param>
        /// <param name="weights">权重数组</param>
        /// <returns></returns>
        public static List<T> RandomList<T>(this T[] self, int count, bool isUnique, IList<float> weights = null)
        {
            return RandomUtil.RandomList(self, count, isUnique, weights);
        }

        public static T[] RandomArray<T>(this T[] self, int count, bool isUnique, IList<float> weights = null)
        {
            return RandomUtil.RandomArray(self, count, isUnique, weights);
        }

        #endregion


        //将self初始化为[height][width]的数组
        public static T[][] InitArrays<T>(this T[][] self, int height, int width, T defaultValue = default)
        {
            return ArrayTUtil.InitArrays(self, height, width, defaultValue);
        }

        //转为左下为原点的坐标系，x增加是向右，y增加是向上（与unity的坐标系一致）
        public static T[][] ToLeftBottomBaseArrays<T>(this T[][] self)
        {
            return ArrayTUtil.ToLeftBottomBaseArrays(self);
        }

        public static void SortWithCompareRules<T>(this T[] self, IList<Comparison<T>> compareRules)
        {
            ArrayTUtil.SortWithCompareRules(self, compareRules);
        }

        public static Dictionary<T, ElementValueType> ToDictionary<T, ElementValueType>(this T[] self,
            ElementValueType defaultElementValue = default)
        {
            return ArrayTUtil.ToDictionary(self, defaultElementValue);
        }
    }
}