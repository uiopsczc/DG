using System;
using System.Collections;
using System.Collections.Generic;

namespace DG
{
	public static class CollectionUtil
	{
		public static bool IsNullOrEmpty(ICollection collection)
		{
			return collection == null || collection.Count == 0;
		}

		public static T[] ToArray<T>(ICollection collection)
		{
			T[] result = new T[collection.Count];
			int curIndex = -1;
			var iterator = collection.GetEnumerator();
			while (iterator.MoveNext(ref curIndex))
				result[curIndex] = (T)iterator.Current;
			return result;
		}

		public static List<T> ToList<T>(ICollection collection)
		{
			List<T> result = new List<T>(collection.Count);
			int curIndex = -1;
			var iterator = collection.GetEnumerator();
			while (iterator.MoveNext(ref curIndex))
				result.Add((T)iterator.Current);
			return result;
		}
	}
}