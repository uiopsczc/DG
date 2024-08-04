using System;
using System.Collections.Generic;

namespace DG
{
	public static class ArrayTUtil
	{
		public static void Swap<T>(T[] array1, int index1, T[] array2, int index2)
		{
			(array1[index1], array2[index2]) = (array2[index2], array1[index1]);
		}

		public static T[] GetArrayByIndexList<T>(T[] array, List<int> indexList)
		{
			int indexListCount = indexList.Count;
			T[] result = new T[indexListCount];
			for (int i = 0; i < indexListCount; i++)
			{
				int index = indexList[i];
				result[i] = array[index];
			}

			return result;
		}

		public static T[] GetArrayByIndexes<T>(T[] array, int[] indexes, int? indexesLength)
		{
			int indexesLengthValue = indexesLength.GetValueOrDefault(indexes.Length);
			T[] result = new T[indexesLengthValue];
			for (int i = 0; i < indexesLengthValue; i++)
			{
				int index = indexes[i];
				result[i] = array[index];
			}

			return result;
		}

		public static T[] AddRangeByIndexes<T>(T[] array1, T[] array2, int[] array2Indexes, int? indexes2Length)
		{
			int array1Length = array1.Length;
			var indexesLengthValue = indexes2Length.GetValueOrDefault(array2Indexes.Length);
			var result = array1.AddCapacity(indexesLengthValue);
			for (int i = 0; i < indexesLengthValue; i++)
			{
				int index = array2Indexes[i];
				result[array1Length + i] = array2[index];
			}

			return result;
		}

		public static T[] AddRangeByIndexList<T>(T[] array1, List<T> list2, int[] list2Indexes, int? indexesLength)
		{
			int array1Length = array1.Length;
			var indexesLengthValue = indexesLength.GetValueOrDefault(list2Indexes.Length);
			var result = array1.AddCapacity(indexesLengthValue);
			for (int i = 0; i < indexesLengthValue; i++)
			{
				int index = list2Indexes[i];
				result[array1Length + i] = list2[index];
			}

			return result;
		}

		

		/// <summary>
		/// 将数组转化为List
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		/// <returns></returns>
		public static List<T> ToList<T>(T[] array)
		{
			if (array == null)
				return null;
			if (array.Length == 0)
				return new List<T>();
			var list = new List<T>(array);
			return list;
		}

		public static T[] EmptyIfNull<T>(T[] array)
		{
			return array ?? new T[0];
		}

		public static T[] RemoveEmpty<T>(T[] array)
		{
			int[] remainIndexes = new int[array.Length];
			int remainCount = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null || array[i].Equals(default(T)))
				{
					remainIndexes[remainCount] = i;
					remainCount++;
				}
			}

			return array.GetArrayByIndexes(remainIndexes, remainCount);
		}

		public static void Swap<T>(T[] array, int index1, int index2)
		{
			Swap(array, index1, array, index2);
		}

		//超过index或者少于0的循环index表获得
		public static T GetByLoopIndex<T>(T[] array, int index)
		{
			if (index < 0)
				index = array.Length - Math.Abs(index) % array.Length;
			else if (index >= array.Length)
				index %= array.Length;
			return array[index];
		}

		//超过index或者少于0的循环index表设置
		public static void SetByLoopIndex<T>(T[] array, int index, T value)
		{
			if (index < 0)
				index = array.Length - Math.Abs(index) % array.Length;
			else if (index >= array.Length) index %= array.Length;
			array[index] = value;
		}

		public static bool Contains<T>(T[] array, T target)
		{
			return array.IndexOf(target) != -1;
		}

		public static bool ContainsIndex<T>(T[] array, int index)
		{
			return index >= 0 && index < array.Length;
		}

		/// <summary>
		/// 使其内元素单一
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="c"></param>
		/// <returns></returns>
		public static T[] Unique<T>(T[] array)
		{
			HashSet<T> hashSet = new HashSet<T>();
			int[] addIndexes = new int[array.Length];
			int addCount = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (hashSet.Add(array[i]))
				{
					addIndexes[addCount] = i;
					addCount++;
				}
			}

			return array.GetArrayByIndexes(addIndexes, addCount);
		}

		public static T[] Combine<T>(T[] array, T[] another, bool isUnique = false)
		{
			T[] result = new T[array.Length + another.Length];
			Array.Copy(array, result, array.Length);
			Array.Copy(another, 0, result, array.Length, another.Length);
			if(isUnique)
				result = result.Unique();
			return result;
		}

		/// <summary>
		/// 将多个数组合成一个数组
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="arrs"></param>
		/// <returns></returns>
		public static T[] Combine<T>(T[] array, bool isUnique = false, params T[][] arrs)
		{
			var arrsCount = arrs.Length;
			var totalElementCount = array.Length;
			for (int i = 0; i < arrsCount; i++)
				totalElementCount += arrs[i].Length;
			T[] result = new T[totalElementCount];
			Array.Copy(array, result, array.Length);
			int curIndex = array.Length;
			for (int i = 0; i < arrsCount; i++)
			{
				var arr = arrs[i];
				Array.Copy(arr, 0, result, curIndex, arr.Length);
				curIndex += arr.Length;
			}
			if(isUnique)
				result = result.Unique();
			return result;
		}

		public static T[] Combine<T>(T[] array, params T[][] arrs)
		{
			return Combine(array, false, arrs);
		}


		public static T[] Push<T>(T[] array, T t)
		{
			return array.Add(t);
		}

		public static T Peek<T>(T[] array)
		{
			return array.Last();
		}

		public static (T element, T[] array) Pop<T>(T[] array)
		{
			return array.RemoveLast2();
		}

		public static T First<T>(T[] array)
		{
			return array[0];
		}

		public static T Last<T>(T[] array)
		{
			return array[array.Length - 1];
		}

		/// <summary>
		///   在self中找subArray的开始位置
		/// </summary>
		/// <returns>-1表示没找到</returns>
		public static int IndexOfSub<T>(T[] array, T[] subArray)
		{
			var resultFromIndex = -1; //sublist在list中的开始位置
			for (var i = 0; i < array.Length; i++)
			{
				object o = array[i];
				if (!ObjectUtil.Equals(o, subArray[0])) continue;
				var isEquals = true;
				for (var j = 1; j < subArray.Length; j++)
				{
					var o1 = subArray[j];
					var o2 = i + j > array.Length - 1 ? default : array[i + j];
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
		///   在self中只保留subArray中的元素
		/// </summary>
		public static T[] RetainElementsOfSub<T>(T[] array, T[] subArray)
		{
			int[] remainIndexes = new int[array.Length];
			int remainCount = 0;
			for (var i = 0; i < array.Length; i++)
				if (subArray.Contains(array[i]))
				{
					remainIndexes[remainCount] = i;
					remainCount++;
				}

			return array.GetArrayByIndexes(remainIndexes, remainCount);
		}

		/// <summary>
		/// </summary>
		public static T[] Sub<T>(T[] array, int fromIndex, int length)
		{
			length = Math.Min(length, array.Length - fromIndex + 1);
			return array.Clone(fromIndex, length);
		}

		/// <summary>
		/// 包含fromIndx到末尾
		/// </summary>
		public static T[] Sub<T>(T[] array, int fromIndex)
		{
			return array.Sub(fromIndex, array.Length - fromIndex + 1);
		}

		/// <summary>
		/// 当set来使用，保持只有一个
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="c"></param>
		public static T[] Add<T>(T[] array, T element, bool isUnique = false)
		{
			if (isUnique && array.Contains(element))
				return array;
			var result = array.AddCapacity(1);
			result[result.Length - 1] = element;
			return result;
		}

		//当isUnique==true的情况下,默认toAddArray里面的元素是不重复的
		public static T[] AddRange<T>(T[] array, T[] toAddArray, bool isUnique = false)
		{
			var selfLength = array.Length;
			T[] result;
			if (isUnique)
			{
				int[] toAddIndexes = new int[toAddArray.Length];
				int addCount = 0;
				for (int i = 0; i < toAddArray.Length; i++)
				{
					var element = toAddArray[i];
					if (array.Contains(element))
						continue;
					toAddIndexes[addCount] = i;
					addCount++;
				}

				return AddRangeByIndexes(array, toAddArray, toAddIndexes, addCount);
			}

			result = array.AddCapacity(toAddArray.Length);
			Array.Copy(toAddArray, 0, result, selfLength, toAddArray.Length);
			return result;
		}

		public static T[] AddRange<T>(T[] array, List<T> toAddList, bool isUnique = false)
		{
			var selfLength = array.Length;
			T[] result;
			if (isUnique)
			{
				int[] toAddIndexes = new int[toAddList.Count];
				int addCount = 0;
				for (int i = 0; i < toAddList.Count; i++)
				{
					var element = toAddList[i];
					if (array.Contains(element))
						continue;
					toAddIndexes[addCount] = i;
					addCount++;
				}

				result = AddRangeByIndexList(array, toAddList, toAddIndexes, addCount);
				return result;
			}

			result = array.AddCapacity(toAddList.Count);
			var toAddArray = toAddList.ToArray();
			Array.Copy(toAddArray, 0, result, selfLength, toAddArray.Length);
			return result;
		}


		public static T[] AddFirst<T>(T[] array, T element, bool isUnique = false)
		{
			T[] result;
			if (isUnique && array.Contains(element))
			{
				result = new T[array.Length];
				Array.Copy(array, result, array.Length);
				return result;
			}

			result = array.AddCapacity(1, false);
			result[0] = element;
			return result;
		}

		public static T[] AddLast<T>(T[] array, T element, bool isUnique = false)
		{
			T[] result;
			if (isUnique && array.Contains(element))
			{
				result = new T[array.Length];
				Array.Copy(array, result, array.Length);
				return result;
			}

			result = array.AddCapacity(1);
			result[result.Length - 1] = element;
			return result;
		}

		public static T[] AddUnique<T>(T[] array, T o)
		{
			return AddLast(array, o, true);
		}

		public static T[] RemoveFirst<T>(T[] array)
		{
			return RemoveFirst2(array).array;
		}

		public static (T element, T[] array) RemoveFirst2<T>(T[] inArray)
		{
			var element = inArray[0];
			var array = new T[inArray.Length - 1];
			if (array.Length > 0)
				Array.Copy(inArray, 1, array, 0, inArray.Length - 1);
			return (element, array);
		}

		public static T[] RemoveLast<T>( T[] array)
		{
			return RemoveLast2(array).array;
		}

		public static (T element, T[] array) RemoveLast2<T>( T[] inArray)
		{
			var element = inArray[inArray.Length - 1];
			var array = new T[inArray.Length - 1];
			if (array.Length > 0)
				Array.Copy(inArray, 0, array, 0, inArray.Length - 1);
			return (element, array);
		}

		public static (T element, T[] array) RemoveAt2<T>(T[] inArray, int removeIndex)
		{
			var element = inArray[removeIndex];
			var array = new T[inArray.Length - 1];
			int removeIndexMinus1 = removeIndex - 1;
			int removeIndexPlus1 = removeIndex + 1;
			if (removeIndexMinus1 >= 0)
				Array.Copy(inArray, 0, array, 0, removeIndexMinus1 + 1);
			if (removeIndexPlus1 < inArray.Length)
				Array.Copy(inArray, removeIndexPlus1, array, removeIndex, inArray.Length - removeIndex - 1);
			return (element, array);
		}

		public static T[] RemoveAt<T>(T[] array, int removeIndex)
		{
			return array.RemoveAt2(removeIndex).array;
		}

		/// <summary>
		///   删除list中的subList（subList必须要全部在list中）
		/// </summary>
		public static T[] RemoveSub<T>(T[] array, T[] subArray)
		{
			var fromIndex = array.IndexOfSub(subArray);
			T[] result;
			if (fromIndex == -1)
			{
				result = new T[array.Length];
				Array.Copy(array, result, array.Length);
				return result;
			}

			result = array.RemoveRange(fromIndex, subArray.Length);
			return result;
		}

		//elements:移除掉的元素
		//array:self被移除后的数组
		public static (T[] elements, T[] array) RemoveRange2<T>(T[] array, int removeFromIndex)
		{
			var length = array.Length - removeFromIndex + 1;
			return RemoveRange2(array, removeFromIndex, length);
		}

		//elements:移除掉的元素
		//array:self被移除后的数组
		public static (T[] elements, T[] array) RemoveRange2<T>(T[] inArray, int removeFromIndex, int length)
		{
			length = Math.Min(inArray.Length - removeFromIndex + 1, length);
			var elements = new T[length];
			Array.Copy(inArray, removeFromIndex, elements, 0, elements.Length);
			var array = new T[inArray.Length - length];
			int removeFromIndexMinus1 = removeFromIndex - 1;
			int removeEndIndexPlus1 = removeFromIndex + length;
			if (removeFromIndexMinus1 >= 0)
				Array.Copy(inArray, 0, array, 0, removeFromIndexMinus1 + 1);
			if (removeEndIndexPlus1 < inArray.Length)
				Array.Copy(inArray, removeEndIndexPlus1, array, removeFromIndex, inArray.Length - removeFromIndex - length);
			return (elements, array);
		}

		public static T[] RemoveRange<T>(T[] array, int removeFromIndex)
		{
			var length = array.Length - removeFromIndex + 1;
			return RemoveRange(array, removeFromIndex, length);
		}

		public static T[] RemoveRange<T>(T[] inArray, int removeFromIndex, int length)
		{
			length = Math.Min(inArray.Length - removeFromIndex + 1, length);
			var array = new T[inArray.Length - length];
			int removeFromIndexMinus1 = removeFromIndex - 1;
			int removeEndIndexPlus1 = removeFromIndex + length;
			if (removeFromIndexMinus1 >= 0)
				Array.Copy(inArray, 0, array, 0, removeFromIndexMinus1 + 1);
			if (removeEndIndexPlus1 < inArray.Length)
				Array.Copy(inArray, removeEndIndexPlus1, array, removeFromIndex, inArray.Length - removeFromIndex - length);
			return array;
		}

		/// <summary>
		/// 在list中删除subList中出现的元素
		/// </summary>
		public static T[] RemoveElements<T>(T[] array, HashSet<T> hashSet)
		{
			int[] remainIndexes = new int[array.Length];
			int remainCount = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (!hashSet.Contains(array[i]))
				{
					remainIndexes[remainCount] = i;
					remainCount++;
				}
			}

			return array.GetArrayByIndexes(remainIndexes, remainCount);
		}


		public static void Reverse<T>(T[] array)
		{
			Array.Reverse(array);
		}

		public static void Foreach<T>(T[] array, Action<T> action)
		{
			for (int i = 0; i < array.Length; i++)
				action(array[i]);
		}

		public static T[] Clone<T>(T[] array)
		{
			T[] clone = new T[array.Length];
			array.CopyTo(clone, 0);
			return clone;
		}


		/// <summary>
		/// 数组中target的Index
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static int IndexOf<T>(T[] array, T target)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (target.Equals(array[i]))
					return i;
			}

			return -1;
		}


		public static T[] Clone<T>(T[] array, int startIndex, int len)
		{
			var length = Math.Min(len, array.Length - startIndex);
			var target = new T[length];
			Array.Copy(array, startIndex, target, 0, length);
			return target;
		}

		/// <summary>
		///扩展数组
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		public static T[] EnlargeCapacity<T>(T[] array, float enlargeFactor = 2f)
		{
			int orgLength = array.Length;
			int newLength = (int)(orgLength * enlargeFactor);
			T[] newObjects = new T[newLength];
			Array.Copy(array, newObjects, orgLength);
			return newObjects;
		}

		public static T[] AddCapacity<T>(T[] array, int add, bool isAppend = true)
		{
			int selfLength = array.Length;
			int newLength = selfLength + add;
			T[] newObjects = new T[newLength];
			if (isAppend) //在后面扩充
				Array.Copy(array, newObjects, selfLength);
			else //在前面扩充
				Array.Copy(array, 0, newObjects, add, selfLength);
			return newObjects;
		}


		public static T[] Insert<T>(T[] array, int insertIndex, T element)
		{
			var result = new T[array.Length + 1];
			result[insertIndex] = element;
			int insertIndexMinus1 = insertIndex - 1;
			int insertIndexPlus1 = insertIndex + 1;
			if (insertIndexMinus1 >= 0)
				Array.Copy(array, 0, result, 0, insertIndexMinus1 + 1);
			if (insertIndexPlus1 < result.Length)
				Array.Copy(array, insertIndex, result, insertIndexPlus1, array.Length - insertIndex);
			return result;
		}

		public static T[] InsertRange<T>(T[] array, int insertIndex, T[] toAddArray)
		{
			var toAddLength = toAddArray.Length;
			var result = new T[array.Length + toAddLength];
			Array.Copy(toAddArray, 0, result, insertIndex, toAddLength);
			int insertFromIndexMinus1 = insertIndex - 1;
			int insertEndIndexPlus = insertIndex + toAddLength;
			if (insertFromIndexMinus1 >= 0)
				Array.Copy(array, 0, result, 0, insertFromIndexMinus1 + 1);
			if (insertEndIndexPlus < result.Length)
				Array.Copy(array, insertIndex, result, insertEndIndexPlus, array.Length - insertIndex);
			return result;
		}

		public static T[] InsertRange<T>(T[] array, int insertIndex, List<T> toAddList)
		{
			var toAddLength = toAddList.Count;
			var result = new T[array.Length + toAddLength];
			for (int i = 0; i < toAddList.Count; i++)
				result[insertIndex + i] = toAddList[i];
			int insertFromIndexMinus1 = insertIndex - 1;
			int insertEndIndexPlus1 = insertIndex + toAddLength;
			if (insertFromIndexMinus1 >= 0)
				Array.Copy(array, 0, result, 0, insertFromIndexMinus1 + 1);
			if (insertEndIndexPlus1 < result.Length)
				Array.Copy(array, insertIndex, result, insertEndIndexPlus1, array.Length - insertIndex);
			return result;
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
		public static T[][] InitArrays<T>(T[][] array, int height, int width, T defaultValue = default)
		{
			array = new T[height][];
			for (int i = 0; i < height; i++)
				array[i] = new T[width];
			if (ObjectUtil.Equals(defaultValue, default(T))) return array;
			for (int i = 0; i < height; i++)
				for (int j = 0; j < width; j++)
					array[i][j] = defaultValue;

			return array;
		}

		//转为左下为原点的坐标系，x增加是向右，y增加是向上（与unity的坐标系一致）
		public static T[][] ToLeftBottomBaseArrays<T>(T[][] array)
		{
			int selfHeight = array.Length;
			int selfWidth = array[0].Length;
			int resultHeight = selfWidth;
			int resultWidth = selfHeight;
			var result = InitArrays(array, resultHeight, resultWidth);
			for (int i = 0; i < resultWidth; i++)
				for (int j = 0; j < resultHeight; j++)
					result[j][resultWidth - 1 - i] = array[i][j];
			return result;
		}

		public static void SortWithCompareRules<T>(T[] array, IList<Comparison<T>> compareRules)
		{
			SortUtil.MergeSortWithCompareRules(array, compareRules);
		}

		public static Dictionary<T, ElementValueType> ToDictionary<T, ElementValueType>(T[] array, ElementValueType defaultElementValue = default)
		{
			Dictionary<T, ElementValueType> dict = new Dictionary<T, ElementValueType>();
			if (array.IsNullOrEmpty())
				return dict;
			for (var i = 0; i < array.Length; i++)
			{
				var element = array[i];
				dict[element] = defaultElementValue;
			}
			return dict;
		}

	}
}

