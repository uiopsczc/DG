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
		///   ��Ϊ��Ӧ��ArrayList
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
		// Diff���
		//////////////////////////////////////////////////////////////////////
		// �����ApplyDiffʹ��
		// ��newΪ��׼����ȡnew�����old��һ���Ĳ���
		// local diff = table.GetDiff(old, new)
		//  table.ApplyDiff(old, diff)
		// ����old�ľͱ�ɺ�newһģһ��������
		public static LinkedHashtable GetDiff(this IList oldList, IList newList)
		{
			return IListUtil.GetDiff(oldList, newList);
		}

		// table.ApplyDiff(old, diff)
		// ��diff�еĶ���Ӧ�õ�old��
		// ��Ҫ����ΪArray��ʱ����Ҫ���¸�ֵ��List��ʱ�򣬿��Բ���Ҫ���¸�ֵ
		public static IList ApplyDiff(this IList oldList, LinkedHashtable diffDict)
		{
			return IListUtil.ApplyDiff(oldList, diffDict);
		}

		// �����ApplyDiffʹ��
		// ��newΪ��׼����ȡnew���У���old��û�е�
		// local diff = table.GetNotExist(old, new)
		// table.ApplyDiff(old, diff)
		// ����old����new�е��ֶ�
		public static LinkedHashtable GetNotExist(this IList oldList, IList newList)
		{
			return IListUtil.GetNotExist(oldList, newList);
		}

		//����table�Ƿ�һ��
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