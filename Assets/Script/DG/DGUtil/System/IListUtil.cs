using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
#if UNITY_EDITOR
using UnityEditorInternal;

#endif

namespace DG
{
	public static class IListUtil
	{
		public static bool ContainsIndex(IList list, int index)
		{
			return index < list.Count && index >= 0;
		}

		/// <summary>
		///   变为对应的ArrayList
		/// </summary>
		/// <param name="inList"></param>
		/// <returns></returns>
		public static ArrayList ToArrayList(IList inList)
		{
			var list = new ArrayList();
			for (int i = 0; i < inList.Count; i++)
				list.Add(inList[i]);
			return list;
		}

		public static void BubbleSort(IList list, Func<object, object, bool> compareFunc)
		{
			SortUtil.BubbleSort(list, compareFunc);
		}

		public static void BubbleSortWithCompareRules(IList list, IList<Comparison<object>> compareRules)
		{
			SortUtil.BubbleSortWithCompareRules(list, compareRules);
		}

		public static void MergeSort(IList list, Func<object, object, bool> compareFunc)
		{
			SortUtil.MergeSort(list, compareFunc);
		}

		public static void MergeSortWithCompareRules(IList list, IList<Comparison<object>> compareRules)
		{
			SortUtil.MergeSortWithCompareRules(list, compareRules);
		}

		public static void QuickSortWithCompareRules(IList list, IList<Comparison<object>> compareRules)
		{
			SortUtil.QuickSortWithCompareRules(list, compareRules);
		}

		public static void QuickSort(IList list, Func<object, object, bool> compareFunc)
		{
			SortUtil.QuickSort(list, compareFunc);
		}


		//////////////////////////////////////////////////////////////////////
		// Diff相关
		//////////////////////////////////////////////////////////////////////
		// 必须和ApplyDiff使用
		// 以new为基准，获取new相对于old不一样的部分
		// local diff = table.GetDiff(old, new)
		//  table.ApplyDiff(old, diff)
		// 这样old的就变成和new一模一样的数据
		public static LinkedHashtable GetDiff(IList oldList, IList newList)
		{
			var diff = new LinkedHashtable();
			for (int i = newList.Count - 1; i >= 0; i--)
			{
				var newKey = i;
				var newValue = newList[i];
				switch (newValue)
				{
					case IList newValueListIList:
					{
						if (newValueListIList.Count == 0 && (!oldList.ContainsIndex(newKey) ||
						                                     oldList[newKey].GetType() != newValue.GetType() ||
						                                     (oldList[newKey] is IList oldListIList &&
						                                      oldListIList.Count != 0)))
							diff[newKey] = StringConst.STRING_NEW_IN_TABLE + newValue.GetType();
						else if (oldList.ContainsIndex(newKey) && oldList[newKey] is IList oldListIList2)
							diff[newKey] = GetDiff(oldListIList2, newValueListIList);
						else if (!oldList.ContainsIndex(newKey) || !newValue.Equals(oldList[newKey]))
							diff[newKey] = CloneUtil.CloneDeep(newValue);
						break;
					}
					case IDictionary newValueDict
						when oldList.ContainsIndex(newKey) && oldList[newKey] is IDictionary oldListDict:
						diff[newKey] = IDictionaryUtil.GetDiff(oldListDict, newValueDict);
						break;
					default:
					{
						if (!oldList.ContainsIndex(newKey) || !newValue.Equals(oldList[newKey]))
							diff[newKey] = newValue;
						break;
					}
				}
			}

			for (int i = 0; i < oldList.Count; i++)
			{
				if (!newList.ContainsIndex(i))
					diff[i] = StringConst.STRING_NIL_IN_TABLE;
			}

			diff.Sort((a, b) => a.To<int>() >= b.To<int>());
			if (diff.Count == 0)
				diff = null;
			return diff;
		}

		// table.ApplyDiff(old, diff)
		// 将diff中的东西应用到old中
		// 重要：当为Array的时候，需要重新赋值；List的时候，可以不需要重新赋值
		public static IList ApplyDiff(IList oldList, LinkedHashtable diffDict)
		{
			if (diffDict == null)
				return oldList;

			int oldListCount = oldList.Count;
			foreach (DictionaryEntry dictionaryEntry in diffDict)
			{
				var key = dictionaryEntry.Key.To<int>();
				var value = dictionaryEntry.Value;
				var valueString = value.ToString();
				if (StringConst.STRING_NIL_IN_TABLE.Equals(valueString))
				{
					if (oldList is Array oldListArray)
						oldList = oldListArray.RemoveAt_Array(key);
					else
						oldList.RemoveAt(key);
				}
				else if (valueString.StartsWith(StringConst.STRING_NEW_IN_TABLE))
				{
					string typeString = value.ToString().Substring(StringConst.STRING_NEW_IN_TABLE.Length);
					Type type = TypeUtil.GetType(typeString);
					var newValue = type.CreateInstance<object>();
					if (key < oldListCount)
						oldList[key] = newValue;
					else
					{
						if (oldList is Array oldListArray)
							oldList = oldListArray.Insert_Array(oldListCount, value);
						else
							oldList.Insert(oldListCount, value);
					}
				}
				else if (oldList.ContainsIndex(key) && oldList[key] is IList oldListIList &&
				         value is LinkedHashtable hashtable)
					ApplyDiff(oldListIList, hashtable);
				else if (oldList.ContainsIndex(key) && oldList[key] is IDictionary oldListDict &&
				         value is LinkedHashtable linkedHashtable)
					IDictionaryUtil.ApplyDiff(oldListDict, linkedHashtable);
				else
				{
					if (key < oldListCount)
						oldList[key] = value;
					else
					{
						if (oldList is Array oldListArray)
							oldList = oldListArray.Insert_Array(oldListCount, value);
						else
							oldList.Insert(oldListCount, value);
					}
				}
			}

			return oldList;
		}

		// 必须和ApplyDiff使用
		// 以new为基准，获取new中有，但old中没有的
		// local diff = table.GetNotExist(old, new)
		// table.ApplyDiff(old, diff)
		// 这样old就有new中的字段
		public static LinkedHashtable GetNotExist(IList oldList, IList newList)
		{
			var diff = new LinkedHashtable();
			for (int i = newList.Count - 1; i >= 0; i--)
			{
				var newKey = i;
				var newValue = newList[i];

				if (!oldList.ContainsIndex(i))
					diff[newKey] = newKey;
				else
				{
					var oldValue = oldList[newKey];
					switch (oldValue)
					{
						case IList oldValueList when newValue is IList list:
							diff[newKey] = GetDiff(oldValueList, list);
							break;
						case IDictionary oldValueDict when newValue is IDictionary dictionary:
							diff[newKey] = IDictionaryUtil.GetNotExist(oldValueDict, dictionary);
							break;
					}

					//其他情况不用处理
				}
			}

			diff.Sort((a, b) => a.To<int>() >= b.To<int>());
			return diff;
		}

		//两个table是否不一样
		public static bool IsDiff(IList oldList, IList newList)
		{
			if (oldList.Count != newList.Count)
				return true;
			for (int newKey = 0; newKey < newList.Count; newKey++)
			{
				var newValue = newList[newKey];
				var oldValue = oldList[newKey];
				switch (newValue)
				{
					case IList _ when !(oldValue is IList):
						return true;
					case IList list when IsDiff(oldValue as IList, list):
						return true;
					case IList _:
						break;
					case IDictionary _ when !(oldValue is IDictionary):
					case IDictionary _
						when IDictionaryUtil.IsDiff(oldValue as IDictionary, newValue as IDictionary):
						return true;
					case IDictionary _:
						break;
					default:
					{
						if (!newValue.Equals(oldValue))
							return true;
						break;
					}
				}
			}

			return false;
		}

#if UNITY_EDITOR
//		public static void ToReorderableList(IList toReorderList, ref ReorderableList reorderableList)
//		{
//			ReorderableListUtil.ToReorderableList(toReorderList, ref reorderableList);
//		}
#endif
	}
}

