using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DG
{
	public static class IListTUtil
	{
		/// <summary>
		/// ��s��fromIndex��ʼ,��toIndex��������toIndex��������Ԫ��תΪ�ַ�������������Ԫ��֮����n���ӣ�����Ԫ�غ��治��n��
		/// ���磺object[] s={"aa","bb","cc"} split="DD" return "aaDDbbDDcc"
		/// </summary>
		public static string Concat<T>(IList<T> list, int fromIndex, int toIndex, string separator)
		{
			if (fromIndex < 0 || toIndex > list.Count || toIndex - fromIndex < 0) throw new IndexOutOfRangeException();
			var stringBuilder = new StringBuilder();
			if (toIndex - fromIndex <= 0)
				return stringBuilder.ToString();
			for (int i = fromIndex; i < list.Count && i <= toIndex; i++)
			{
				string value = list[i].ToString();
				if (i == fromIndex)
					stringBuilder.Append(value);
				else
					stringBuilder.Append(separator + value);
			}
			return stringBuilder.ToString();
		}

		public static string Concat<T>(IList<T> list, string separator)
		{
			return list.Concat(0, list.Count - 1, separator);
		}

		public static void SortWithCompareRules<T>(IList<T> list, IList<Comparison<T>> compareRules)
		{
			SortUtil.MergeSortWithCompareRules(list, compareRules);
		}


		public static void BubbleSortWithCompareRules<T>(IList<T> list, IList<Comparison<T>> compareRules)
		{
			SortUtil.BubbleSortWithCompareRules(list, compareRules);
		}


		//�磺list.MergeSort((a, b)=>return a.count <= b.count)
		//���ǽ�count��С��������ע��Ƚϴ�Сʱ��Ҫ©�����ںţ��������ʱҲ���������������ȶ�
		public static void MergeSort<T>(IList<T> list, Func<T, T, bool> compareFunc)
		{
			SortUtil.MergeSort(list, compareFunc);
		}


		public static void MergeSortWithCompareRules<T>(IList<T> list, IList<Comparison<T>> compareRules)
		{
			SortUtil.MergeSortWithCompareRules(list, compareRules);
		}


		//�磺list.QuickSort((a, b)=>return a.count <= b.count)
		//���ǽ�count��С��������ע��Ƚϴ�Сʱ��Ҫ©�����ںţ��������ʱҲ���������������ȶ�
		public static void QuickSort<T>(IList<T> list, Func<T, T, bool> compareFunc)
		{
			SortUtil.QuickSort(list, compareFunc);
		}


		public static void QuickSortWithCompareRules<T>(IList<T> list, IList<Comparison<T>> compareRules)
		{
			SortUtil.QuickSortWithCompareRules(list, compareRules);
		}

		public static int BinarySearchCat<T>(IList<T> list, T targetValue,
			IndexOccurType indexOccurType = IndexOccurType.Any_Index, IList<Comparison<T>> compareRules = null)
		{
			return SortedListSearchUtil.BinarySearchCat(list, targetValue, indexOccurType, compareRules);
		}

		public static ListSortedType GetListSortedType<T>(IList<T> list, IList<Comparison<T>> compareRules)
		{
			T firstValue = list[0];
			T lastValue = list[list.Count - 1];
			return CompareUtil.CompareWithRules(firstValue, lastValue, compareRules) <= 0
				? ListSortedType.Increase
				: ListSortedType.Decrease;
		}

		/// <summary>
		///   ��list1[a]��list2[b]����
		/// </summary>
		public static void Swap<T>(IList<T> list1, int index1, IList<T> list2, int index2)
		{
			var c = list1[index1];
			list1[index1] = list2[index2];
			list2[index2] = c;
		}

		public static IList<T> EmptyIfNull<T>(IList<T> list)
		{
			return list ?? new List<T>();
		}

		public static void RemoveEmpty<T>(IList<T> list)
		{
			for (var i = list.Count - 1; i >= 0; i--)
				if (list[i] == null || list[i].Equals(default(T)))
					list.RemoveAt(i);
		}

		//����index��������0��ѭ��index����
		public static T GetByLoopIndex<T>(IList<T> list, int index)
		{
			if (index < 0)
				index = list.Count - Math.Abs(index) % list.Count;
			else if (index >= list.Count)
				index %= list.Count;
			return list[index];
		}

		//����index��������0��ѭ��index������
		public static void SetByLoopIndex<T>(IList<T> list, int index, T value)
		{
			if (index < 0)
				index = list.Count - Math.Abs(index) % list.Count;
			else if (index >= list.Count) index %= list.Count;
			list[index] = value;
		}

		public static bool ContainsIndex<T>(IList<T> list, int index)
		{
			return index < list.Count && index >= 0;
		}

		/// <summary>
		///   ʹ����Ԫ�ص�һ
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static void Unique<T>(IList<T> list)
		{
			var hashSet = new HashSet<T>();
			for (int i = 0; i < list.Count; i++)
			{
				if (!hashSet.Add(list[i]))
				{
					list.RemoveAt(i);
					i--;
				}
			}
		}


		public static IList<T> Combine<T>(IList<T> list, IList<T> another, bool isUnique = false)
		{
			for (var i = 0; i < another.Count; i++)
			{
				var toAdd = another[i];
				list.Add(toAdd);
			}
			if (isUnique)
				list.Unique();
			return list;
		}

		public static void Push<T>(IList<T> list, T t)
		{
			list.Add(t);
		}

		public static T Peek<T>(IList<T> list)
		{
			return list.Last();
		}

		public static T Pop<T>(IList<T> list)
		{
			return list.RemoveLast();
		}


		#region ����

		/// <summary>
		///   ��һ��item
		///   ��linq
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static T First<T>(IList<T> list)
		{
			return list[0];
		}

		/// <summary>
		///   ���һ��item
		///   ��linq
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static T Last<T>(IList<T> list)
		{
			return list[list.Count - 1];
		}

		/// <summary>
		///   ��list����sublist�Ŀ�ʼλ��
		/// </summary>
		/// <returns>-1��ʾû�ҵ�</returns>
		public static int IndexOfSub<T>(IList<T> list, IList<T> subList)
		{
			var resultFromIndex = -1; //sublist��list�еĿ�ʼλ��
			for (var i = 0; i < list.Count; i++)
			{
				object o = list[i];
				if (!ObjectUtil.Equals(o, subList[0])) continue;
				var isEquals = true;
				for (var j = 1; j < subList.Count; j++)
				{
					var o1 = subList[j];
					var o2 = i + j > list.Count - 1 ? default : list[i + j];
					if (ObjectUtil.Equals(o1, o2)) continue;
					isEquals = false;
					break;
				}

				if (!isEquals) continue;
				resultFromIndex = i;
				break;
			}

			return resultFromIndex;
		}

		/// <summary>
		///   ��list��ֻ����sublist�е�Ԫ��
		/// </summary>
		public static bool RetainElementsOfSub<T>(IList<T> list, IList<T> subList)
		{
			var isModify = false;
			for (var i = list.Count - 1; i >= 0; i--)
				if (!subList.Contains(list[i]))
				{
					list.RemoveAt(i);
					isModify = true;
				}

			return isModify;
		}

		/// <summary>
		///   ����fromIndx
		/// </summary>
		public static IList<T> Sub<T>(IList<T> inList, int fromIndex, int length)
		{
			length = Math.Min(length, inList.Count - fromIndex + 1);
			var cloneList = new List<T>(inList);
			inList.Clear();
			for (int i = 0; i < length; i++)
				inList.Add(cloneList[fromIndex + i]);
			return cloneList;
		}


		/// <summary>
		///   ����fromIndx��ĩβ
		/// </summary>
		public static IList<T> Sub<T>(IList<T> list, int fromIndex)
		{
			var length = list.Count - fromIndex + 1;
			return list.Sub(fromIndex, length);
		}

		#endregion

		#region ����ɾ������

		/// <summary>
		///   ��set��ʹ�ã�����ֻ��һ��
		/// </summary>
		public static IList<T> Add<T>(IList<T> list, T element, bool isUnique = false)
		{
			if (isUnique && list.Contains(element))
				return list;
			list.Add(element);
			return list;
		}

		public static IList<T> AddRangeUnique<T>(IList<T> list, IList<T> toAddList)
		{
			for (var i = 0; i < toAddList.Count; i++)
			{
				var element = toAddList[i];
				if (list.Contains(element))
					continue;
				list.Add(element);
			}

			return list;
		}

		public static IList<T> AddRangeUnique<T>(IList<T> list, T[] toAddArray)
		{
			for (var i = 0; i < toAddArray.Length; i++)
			{
				var element = toAddArray[i];
				if (list.Contains(element))
					continue;
				list.Add(element);
			}

			return list;
		}

		public static IList<T> AddFirst<T>(IList<T> list, T o,
			bool isUnique = false)
		{
			if (isUnique && list.Contains(o))
				return list;
			list.Insert(0, o);
			return list;
		}

		public static IList<T> AddLast<T>(IList<T> list, T o,
			bool isUnique = false)
		{
			if (isUnique && list.Contains(o))
				return list;
			list.Insert(list.Count, o);
			return list;
		}


		public static IList<T> AddUnique<T>(IList<T> list, T o)
		{
			return AddLast(list, o, true);
		}


		public static T RemoveFirst<T>(IList<T> list)
		{
			var t = list[0];
			list.RemoveAt(0);
			return t;
		}


		public static T RemoveLast<T>(IList<T> list)
		{
			var o = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
			return o;
		}

		/// <summary>
		///   ��RemoveAtһ����ֻ���з���ֵ
		/// </summary>
		public static T RemoveAt2<T>(IList<T> list, int index)
		{
			var t = list[index];
			list.RemoveAt(index);
			return t;
		}


		/// <summary>
		///   ��Removeһ����ֻ���з���ֵ(�Ƿ�ɾ����)
		/// </summary>
		/// <param name="list"></param>
		/// <param name="o"></param>
		/// <returns></returns>
		public static bool Remove2<T>(IList<T> list, T o)
		{
			if (!list.Contains(o))
				return false;
			list.Remove(o);
			return true;
		}

		/// <summary>
		///   ɾ��list�е�subList��subList����Ҫȫ����list�У�
		/// </summary>
		public static bool RemoveSub<T>(IList<T> list, IList<T> subList)
		{
			var fromIndex = list.IndexOfSub(subList);
			if (fromIndex == -1)
				return false;
			var isModify = false;
			for (var i = fromIndex + subList.Count - 1; i >= fromIndex; i--)
			{
				list.RemoveAt(i);
				if (!isModify)
					isModify = true;
			}

			return isModify;
		}

		/// <summary>
		///   ��RemoveRangeһ����������ɾ����Ԫ��List
		/// </summary>
		public static IList<T> RemoveRange2<T>(IList<T> list, int index, int length)
		{
			var lastIndex = index + length - 1 <= list.Count - 1 ? index + length - 1 : list.Count - 1;
			var resultList = new List<T>(list.Count);
			for (var i = lastIndex; i >= index; i--)
				resultList.Add(list[i]);
			resultList.Reverse();
			list.Clear();
			for (var i = 0; i < resultList.Count; i++)
			{
				var e = resultList[i];
				list.Add(e);
			}
			return list;
		}


		/// <summary>
		///   ��list��ɾ��subList�г��ֵ�Ԫ��
		/// </summary>
		public static bool RemoveElementsOfSub<T>(IList<T> list, IList<T> subList)
		{
			var isModify = false;
			for (var i = list.Count - 1; i >= 0; i++)
				if (subList.Contains(list[i]))
				{
					list.Remove(list[i]);
					isModify = true;
				}

			return isModify;
		}

		#endregion

		#region Random ���
//		public static T Random<T>(this IList<T> list)
//		{
//			return RandomUtil.Random(list);
//		}
//
//		/// <summary>
//		/// ���list�����Ԫ��count��
//		/// </summary>
//		/// <typeparam name="T"></typeparam>
//		/// <param name="list"></param>
//		/// <param name="count">����</param>
//		/// <param name="isUnique">�Ƿ�Ψһ</param>
//		/// <param name="weights">Ȩ������</param>
//		/// <returns></returns>
//		public static IList<T> RandomList<T>(this IList<T> list, int count, bool isUnique, IList<float> weights = null)
//		{
//			return RandomUtil.RandomList(list, count, isUnique, weights);
//		}
//
//		public static T[] RandomArray<T>(this IList<T> list, int count, bool isUnique, IList<float> weights = null)
//		{
//			return RandomUtil.RandomArray(list, count, isUnique, weights);
//		}

		#endregion


		public static void CopyTo<T>(IList<T> sourceList, IList<T> destList, params object[] constructArgs)
			where T : ICopyable
		{
			destList.Clear();
			for (var i = 0; i < sourceList.Count; i++)
			{
				var element = sourceList[i];
				var destElement = typeof(T).CreateInstance<T>(constructArgs);
				destList.Add(destElement);
				element.CopyTo(destElement);
			}
		}

		public static void CopyFrom<T>(IList<T> destList, IList<T> sourceList, params object[] constructArgs)
			where T : ICopyable
		{
			destList.Clear();
			for (var i = 0; i < sourceList.Count; i++)
			{
				var element = sourceList[i];
				var selfElement = typeof(T).CreateInstance<T>(constructArgs);
				destList.Add(selfElement);
				selfElement.CopyFrom(element);
			}
		}

		public static ArrayList DoSaveList<T>(IList<T> list, Action<T, Hashtable> doSaveCallback)
		{
			ArrayList result = new ArrayList();
			for (var i = 0; i < list.Count; i++)
			{
				var element = list[i];
				Hashtable elementDict = new Hashtable();
				result.Add(elementDict);
				doSaveCallback(element, elementDict);
			}

			return result;
		}

		public static void DoRestoreList<T>(IList<T> list, ArrayList arrayList,
			Func<Hashtable, T> doRestoreCallback)
		{
			for (var i = 0; i < arrayList.Count; i++)
			{
				var element = arrayList[i];
				var elementDict = element as Hashtable;
				T elementT = doRestoreCallback(elementDict);
				list.Add(elementT);
			}
		}
	}
}

