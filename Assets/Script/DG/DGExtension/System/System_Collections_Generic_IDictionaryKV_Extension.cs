using System;
using System.Collections.Generic;

namespace DG
{
	public static class System_Collections_Generic_IDictionaryKV_Extension
	{
		public static V Get<K, V>(this IDictionary<K, V> self, K key)
		{
			return IDictionaryKVUtil.Get(self, key);
		}

		public static V GetOrGetDefault<K, V>(this IDictionary<K, V> self, K key, V defaultValue = default)
		{
			return IDictionaryKVUtil.GetOrGetDefault(self, key, defaultValue);
		}

		public static V GetOrGetDefault<K, V>(this IDictionary<K, V> self, K key, Func<V> defaultFunc = null)
		{
			return IDictionaryKVUtil.GetOrGetDefault(self, key, defaultFunc);
		}
		public static V GetOrGetNew<K, V>(this IDictionary<K, V> self, K key) where V : new()
		{
			return IDictionaryKVUtil.GetOrGetNew(self, key);
		}

		public static V GetOrGetNew<K, V>(this IDictionary<K, V> self, K key, Func<V> newFunc = null) where V : new()
		{
			return IDictionaryKVUtil.GetOrGetNew(self, key, newFunc);
		}

		public static V GetOrAddDefault<K, V>(this IDictionary<K, V> self, K key, V defaultValue = default)
		{
			return IDictionaryKVUtil.GetOrAddDefault(self, key, defaultValue);
		}

		public static V GetOrAddDefault<K, V>(this IDictionary<K, V> self, K key, Func<V> defaultFunc = null)
		{
			return IDictionaryKVUtil.GetOrAddDefault(self, key, defaultFunc);
		}
		public static V GetOrAddNew<K, V>(this IDictionary<K, V> self, K key) where V : new()
		{
			return IDictionaryKVUtil.GetOrAddNew(self, key);
		}

		public static V GetOrAddNew<K, V>(this IDictionary<K, V> self, K key, Func<V> newFunc = null) where V : new()
		{
			return IDictionaryKVUtil.GetOrAddNew(self, key, newFunc);
		}

		public static void RemoveByFunc<K, V>(this IDictionary<K, V> self, Func<K, V, bool> func)
		{
			IDictionaryKVUtil.RemoveByFunc(self, func);
		}

		public static void RemoveByValue<K, V>(this IDictionary<K, V> self, V value, bool isAll = false)
		{
			IDictionaryKVUtil.RemoveByValue(self, value, isAll);
		}

		public static void RemoveAllAndClear<K, V>(this IDictionary<K, V> self, Action<K, V> onRemoveAction)
		{
			IDictionaryKVUtil.RemoveAllAndClear(self, onRemoveAction);
		}


		public static V Remove2<K, V>(this IDictionary<K, V> self, K key)
		{
			return IDictionaryKVUtil.Remove2(self, key);
		}

		public static void Combine<K, V>(this IDictionary<K, V> self, IDictionary<K, V> another)
		{
			IDictionaryKVUtil.Combine(self, another);
		}

		/// <summary>
		/// 合并两个Dictionary
		/// </summary>
		/// <typeparam name="K"></typeparam>
		/// <typeparam name="V"></typeparam>
		/// <param name="self"></param>
		/// <param name="another"></param>
		/// <param name="combineCallback">其中带一个参数是key，第二个参数是sourceValue，第三个参数是anotherValue，（返回一个V，用于发生冲突时的替换策略）</param>
		/// <returns></returns>
		public static IDictionary<K, V> Combine<K, V>(this IDictionary<K, V> self, IDictionary<K, V> another,
			Func<K, V, V, V> combineCallback)
		{
			return IDictionaryKVUtil.Combine(self, another, combineCallback);
		}

	

		//		public static List<T> RandomList<T>(IDictionary<T, float> self, int outCount, bool isUnique,
		//			RandomManager randomManager = null)
		//		{
		//			randomManager = randomManager ?? Client.instance.randomManager;
		//			return randomManager.RandomList(self, outCount, isUnique);
		//		}
		//
		//		public static T Random<T>(IDictionary<T, float> self, RandomManager randomManager = null)
		//		{
		//			return self.RandomList(1, false, randomManager)[0];
		//		}

		public static K FindKey<K, V>(this IDictionary<K, V> self, K key)
		{
			return IDictionaryKVUtil.FindKey(self, key);
		}

		public static IDictionary<K, V> EmptyIfNull<K, V>(IDictionary<K, V> dict)
		{
			return IDictionaryKVUtil.EmptyIfNull(dict);
		}

	}
}