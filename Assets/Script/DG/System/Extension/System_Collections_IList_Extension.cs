using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;

namespace DG
{
	public static class System_Collections_IList_Extension
	{
		public static bool ContainsIndex(this IList self, int index)
		{
			return IListUtil.ContainsIndex(self, index);
		}

		/// <summary>
		///   变为对应的ArrayList
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ArrayList ToArrayList(this IList self)
		{
			return IListUtil.ToArrayList(self);
		}

		public static void BubbleSort(this IList self, Func<object, object, bool> compareFunc)
		{
			IListUtil.BubbleSort(self, compareFunc);
		}

		public static void BubbleSortWithCompareRules(this IList self, IList<Comparison<object>> compareRules)
		{
			IListUtil.BubbleSortWithCompareRules(self, compareRules);
		}

		public static void MergeSort(this IList self, Func<object, object, bool> compareFunc)
		{
			IListUtil.MergeSort(self, compareFunc);
		}

		public static void MergeSortWithCompareRules(this IList self, IList<Comparison<object>> compareRules)
		{
			IListUtil.MergeSortWithCompareRules(self, compareRules);
		}

		public static void QuickSortWithCompareRules(this IList self, IList<Comparison<object>> compareRules)
		{
			IListUtil.QuickSortWithCompareRules(self, compareRules);
		}

		public static void QuickSort(this IList self, Func<object, object, bool> compareFunc)
		{
			IListUtil.QuickSort(self, compareFunc);
		}


		//////////////////////////////////////////////////////////////////////
		// Diff相关
		//////////////////////////////////////////////////////////////////////
		// 必须和ApplyDiff使用
		// 以new为基准，获取new相对于old不一样的部分
		// local diff = table.GetDiff(old, new)
		//  table.ApplyDiff(old, diff)
		// 这样old的就变成和new一模一样的数据
		public static LinkedHashtable GetDiff(this IList oldList, IList newList)
		{
			return IListUtil.GetDiff(oldList, newList);
		}

		// table.ApplyDiff(old, diff)
		// 将diff中的东西应用到old中
		// 重要：当为Array的时候，需要重新赋值；List的时候，可以不需要重新赋值
		public static IList ApplyDiff(this IList oldList, LinkedHashtable diffDict)
		{
			return IListUtil.ApplyDiff(oldList, diffDict);
		}

		// 必须和ApplyDiff使用
		// 以new为基准，获取new中有，但old中没有的
		// local diff = table.GetNotExist(old, new)
		// table.ApplyDiff(old, diff)
		// 这样old就有new中的字段
		public static LinkedHashtable GetNotExist(this IList oldList, IList newList)
		{
			return IListUtil.GetNotExist(oldList, newList);
		}

		//两个table是否不一样
		public static bool IsDiff(this IList oldList, IList newList)
		{
			return IListUtil.IsDiff(oldList, newList);
		}

#if UNITY_EDITOR
//		public static void ToReorderableList(this IList toReorderList, ref ReorderableList reorderableList)
//		{
//			IListUtil.ToReorderableList(toReorderList, ref reorderableList);
//		}
#endif
	}
}