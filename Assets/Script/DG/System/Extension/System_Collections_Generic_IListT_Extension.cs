using System;
using System.Collections;
using System.Collections.Generic;

namespace DG
{
	public static class System_Collections_Generic_IListT_Extension
	{
		/// <summary>
		/// 将s从fromIndex开始,到toIndex（不包括toIndex）结束的元素转为字符串连接起来，元素之间用n连接（最后的元素后面不加n）
		/// 例如：object[] s={"aa","bb","cc"} split="DD" return "aaDDbbDDcc"
		/// </summary>
		public static string Concat<T>(this IList<T> self, int fromIndex, int toIndex, string separator)
		{
			return IListTUtil.Concat(self, fromIndex, toIndex, separator);
		}

		public static string Concat<T>(this IList<T> self, string separator)
		{
			return IListTUtil.Concat(self, separator);
		}

		public static void SortWithCompareRules<T>(this IList<T> self, IList<Comparison<T>> compareRules)
		{
			IListTUtil.SortWithCompareRules(self, compareRules);
		}


		public static void BubbleSortWithCompareRules<T>(this IList<T> self, IList<Comparison<T>> compareRules)
		{
			IListTUtil.BubbleSortWithCompareRules(self, compareRules);
		}


		//如：list.MergeSort((a, b)=>return a.count <= b.count)
		//则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
		public static void MergeSort<T>(this IList<T> self, Func<T, T, bool> compareFunc)
		{
			IListTUtil.MergeSort(self, compareFunc);
		}


		public static void MergeSortWithCompareRules<T>(this IList<T> self, IList<Comparison<T>> compareRules)
		{
			IListTUtil.MergeSortWithCompareRules(self, compareRules);
		}


		//如：list.QuickSort((a, b)=>return a.count <= b.count)
		//则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
		public static void QuickSort<T>(this IList<T> self, Func<T, T, bool> compareFunc)
		{
			IListTUtil.QuickSort(self, compareFunc);
		}


		public static void QuickSortWithCompareRules<T>(this IList<T> self, IList<Comparison<T>> compareRules)
		{
			IListTUtil.QuickSortWithCompareRules(self, compareRules);
		}

		public static int BinarySearchCat<T>(this IList<T> self, T targetValue,
			EIndexOccurType indexOccurType = EIndexOccurType.Any_Index, IList<Comparison<T>> compareRules = null)
		{
			return IListTUtil.BinarySearchCat(self, targetValue, indexOccurType, compareRules);
		}

		public static EListSortedType GetListSortedType<T>(this IList<T> self, IList<Comparison<T>> compareRules)
		{
			return IListTUtil.GetListSortedType(self, compareRules);
		}
		
		public static IList<T> EmptyIfNull<T>(this IList<T> self)
		{
			return IListTUtil.EmptyIfNull(self);
		}

		public static void RemoveEmpty<T>(this IList<T> self)
		{
			IListTUtil.RemoveEmpty(self);
		}

		/// <summary>
		///   将list[index1]和list[index2]交换
		/// </summary>
		public static void Swap<T>(this IList<T> self, int index1, int index2)
		{
			IListTUtil.Swap(self, index1, self, index2);
		}

		public static void Swap<T>(this IList<T> self, int index1, IList<T> list2, int index2)
		{
			IListTUtil.Swap(self, index1, list2, index2);
		}

		//超过index或者少于0的循环index表获得
		public static T GetByLoopIndex<T>(this IList<T> self, int index)
		{
			return IListTUtil.GetByLoopIndex(self, index);
		}
		

		//超过index或者少于0的循环index表设置
		public static void SetByLoopIndex<T>(this IList<T> self, int index, T value)
		{
			IListTUtil.SetByLoopIndex(self, index, value);
		}

		public static bool ContainsIndex<T>(this IList<T> self, int index)
		{
			return IListTUtil.ContainsIndex(self, index);
		}

		/// <summary>
		///   使其内元素单一
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static void Unique<T>(this IList<T> self)
		{
			IListTUtil.Unique(self);
		}


		public static IList<T> Combine<T>(this IList<T> self, IList<T> another, bool isUnique = false)
		{
			return IListTUtil.Combine(self, another, isUnique);
		}

		public static void Push<T>(this IList<T> self, T t)
		{
			IListTUtil.Push(self, t);
		}

		public static T Peek<T>(this IList<T> self)
		{
			return IListTUtil.Peek(self);
		}

		public static T Pop<T>(this IList<T> self)
		{
			return IListTUtil.Pop(self);
		}


		#region 查找

		/// <summary>
		///   第一个item
		///   用linq
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static T First<T>(this IList<T> self)
		{
			return IListTUtil.First(self);
		}

		/// <summary>
		///   最后一个item
		///   用linq
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static T Last<T>(this IList<T> self)
		{
			return IListTUtil.Last(self);
		}

		/// <summary>
		///   在list中找sublist的开始位置
		/// </summary>
		/// <returns>-1表示没找到</returns>
		public static int IndexOfSub<T>(this IList<T> self, IList<T> subList)
		{
			return IListTUtil.IndexOfSub(self, subList);
		}

		/// <summary>
		///   在list中只保留sublist中的元素
		/// </summary>
		public static bool RetainElementsOfSub<T>(this IList<T> self, IList<T> subList)
		{
			return IListTUtil.RetainElementsOfSub(self, subList);
		}

		/// <summary>
		///   包含fromIndx
		/// </summary>
		public static IList<T> Sub<T>(this IList<T> self, int fromIndex, int length)
		{
			return IListTUtil.Sub(self, fromIndex, length);
		}


		/// <summary>
		///   包含fromIndx到末尾
		/// </summary>
		public static IList<T> Sub<T>(this IList<T> self, int fromIndex)
		{
			return IListTUtil.Sub(self, fromIndex);
		}

		#endregion

		#region 插入删除操作

		/// <summary>
		///   当set来使用，保持只有一个
		/// </summary>
		public static IList<T> Add<T>(this IList<T> self, T element, bool isUnique = false)
		{
			return IListTUtil.Add(self, element, isUnique);
		}

		public static IList<T> AddRangeUnique<T>(this IList<T> self, IList<T> list)
		{
			return IListTUtil.AddRangeUnique(self, list);
		}

		public static IList<T> AddRangeUnique<T>(this IList<T> self, T[] array)
		{
			return IListTUtil.AddRangeUnique(self, array);
		}

		public static IList<T> AddFirst<T>(this IList<T> self, T o,
			bool isUnique = false)
		{
			return IListTUtil.AddFirst(self, o, isUnique);
		}

		public static IList<T> AddLast<T>(this IList<T> self, T o,
			bool isUnique = false)
		{
			return IListTUtil.AddLast(self, o, isUnique); ;
		}


		public static IList<T> AddUnique<T>(this IList<T> self, T o)
		{
			return IListTUtil.AddUnique(self, o);
		}


		public static T RemoveFirst<T>(this IList<T> self)
		{
			return IListTUtil.RemoveFirst(self);
		}


		public static T RemoveLast<T>(this IList<T> self)
		{
			return IListTUtil.RemoveLast(self);
		}

		/// <summary>
		///   跟RemoveAt一样，只是有返回值
		/// </summary>
		public static T RemoveAt2<T>(this IList<T> self, int index)
		{
			return IListTUtil.RemoveAt2(self, index);
		}


		/// <summary>
		///   跟Remove一样，只是有返回值(是否删除掉)
		/// </summary>
		/// <param name="self"></param>
		/// <param name="o"></param>
		/// <returns></returns>
		public static bool Remove2<T>(this IList<T> self, T o)
		{
			return IListTUtil.Remove2(self, o);
		}

		/// <summary>
		///   删除list中的subList（subList必须要全部在list中）
		/// </summary>
		public static bool RemoveSub<T>(this IList<T> self, IList<T> subList)
		{
			return IListTUtil.RemoveSub(self, subList);
		}

		/// <summary>
		///   跟RemoveRange一样，但返回删除的元素List
		/// </summary>
		public static IList<T> RemoveRange2<T>(this IList<T> self, int index, int length)
		{
			return IListTUtil.RemoveRange2(self, index, length);
		}


		/// <summary>
		///   在list中删除subList中出现的元素
		/// </summary>
		public static bool RemoveElementsOfSub<T>(this IList<T> self, IList<T> subList)
		{
			return IListTUtil.RemoveElementsOfSub(self, subList);
		}

		#endregion

		#region Random 随机
//		public static T Random<T>(this List<T> self)
//		{
//			return IListTUtil.Random(self);
//		}
//
//		/// <summary>
//		/// 随机list里面的元素count次
//		/// </summary>
//		/// <typeparam name="T"></typeparam>
//		/// <param name="list"></param>
//		/// <param name="count">个数</param>
//		/// <param name="isUnique">是否唯一</param>
//		/// <param name="weights">权重数组</param>
//		/// <returns></returns>
//		public static List<T> RandomList<T>(this List<T> self, int count, bool isUnique, IList<float> weights = null)
//		{
//			return IListTUtil.RandomList(self, count, isUnique, weights);
//		}
//
//		public static T[] RandomArray<T>(this List<T> self, int count, bool isUnique, IList<float> weights = null)
//		{
//			return IListTUtil.RandomArray(self, count, isUnique, weights);
//		}

		#endregion


		public static void CopyTo<T>(this IList<T> self, IList<T> destList, params object[] constructArgs)
			where T : ICopyable
		{
			IListTUtil.CopyTo(self, destList, constructArgs);
		}

		public static void CopyFrom<T>(this IList<T> self, IList<T> sourceList, params object[] constructArgs)
			where T : ICopyable
		{
			IListTUtil.CopyFrom(self, sourceList, constructArgs);
		}

		public static ArrayList DoSaveList<T>(this IList<T> self, Action<T, Hashtable> doSaveCallback)
		{
			return IListTUtil.DoSaveList(self, doSaveCallback);
		}

		public static void DoRestoreList<T>(this IList<T> self, ArrayList arrayList,
			Func<Hashtable, T> doRestoreCallback)
		{
			IListTUtil.DoRestoreList(self, arrayList, doRestoreCallback);
		}

	}
}