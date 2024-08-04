using System;
using System.Collections;
using System.Collections.Generic;

namespace DG
{
	public static class ICollectionTUtil
	{
		/// <summary>
		/// 转化为ToLinkedHashtable
		/// </summary>
		/// <param name="collection"></param>
		/// <returns></returns>
		public static object ToLinkedHashtable2<T>(ICollection<T> collection)
		{
			if (collection is IDictionary dictionary)
			{
				LinkedHashtable linkedHashtable = new LinkedHashtable();
				foreach (var key in dictionary.Keys)
				{
					var value = dictionary[key];
					linkedHashtable.Put(key, value.ToLinkedHashtable2());
				}

				return linkedHashtable;
			}

			ArrayList list = new ArrayList();
			foreach (object o in collection)
				list.Add(o.ToLinkedHashtable2());

			return list;
		}

		/// <summary>
		/// 用于不同类型转换
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		/// <returns></returns>
		public static List<T> ToList<T>(ICollection<T> collection)
		{
			List<T> result = new List<T>(collection.Count);
			result.AddRange(collection);
			return result;
		}

		/// <summary>
		/// 用于不同类型转换
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		/// <returns></returns>
		public static T[] ToArray<T>(this ICollection<T> collection)
		{
			T[] result = new T[collection.Count];
			collection.CopyTo(result, 0);
			return result;
		}


		public static T[] ToArray<U, T>(this ICollection<U> collection, Func<U, T> covertElementFunc)
		{
			T[] result = new T[collection.Count];
			var iterator = collection.GetEnumerator();
			var curIndex = -1;
			while (iterator.MoveNext(ref curIndex))
				result[curIndex] = covertElementFunc(iterator.Current);
			return result;
		}

	}
}

