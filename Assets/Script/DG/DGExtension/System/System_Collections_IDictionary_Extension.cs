using System;
using System.Collections;

namespace DG
{
	public static class System_Collections_IDictionary_Extension
	{
		//////////////////////////////////////////////////////////////////////
		// Diff相关
		//////////////////////////////////////////////////////////////////////
		// 必须和ApplyDiff使用
		// 以new为基准，获取new相对于old不一样的部分
		// local diff = table.GetDiff(old, new)
		//  table.ApplyDiff(old, diff)
		// 这样old的就变成和new一模一样的数据
		public static LinkedHashtable GetDiff(this IDictionary self, IDictionary newDict)
		{
			return IDictionaryUtil.GetDiff(self, newDict);
		}

		// table.ApplyDiff(old, diff)
		// 将diff中的东西应用到old中
		public static void ApplyDiff(this IDictionary self, LinkedHashtable diffDict)
		{
			IDictionaryUtil.ApplyDiff(self, diffDict);
		}

		// 必须和ApplyDiff使用
		// 以new为基准，获取new中有，但old中没有的
		// local diff = table.GetNotExist(old, new)
		// table.ApplyDiff(old, diff)
		// 这样old就有new中的字段
		public static LinkedHashtable GetNotExist(this IDictionary self, IDictionary newDict)
		{
			return IDictionaryUtil.GetNotExist(self, newDict);
		}

		//两个table是否不一样
		public static bool IsDiff(this IDictionary self, IDictionary newDict)
		{
			return IDictionaryUtil.IsDiff(self, newDict);
		}


		public static T Get<T>(this IDictionary self, object key)
		{
			return IDictionaryUtil.Get<T>(self, key);
		}

		public static T GetOrGetDefault<T>(this IDictionary self, object key, T defaultValue = default)
		{
			return IDictionaryUtil.GetOrGetDefault<T>(self, key, defaultValue);
		}

		public static T GetOrGetDefault<T>(this IDictionary self, object key, Func<T> defaultFunc = null)
		{
			return IDictionaryUtil.GetOrGetDefault<T>(self, key, defaultFunc);
		}
		public static T GetOrGetNew<T>(this IDictionary self, object key) where T : new()
		{
			return IDictionaryUtil.GetOrGetNew<T>(self, key);
		}

		public static T GetOrGetNew<T>(this IDictionary self, object key, Func<T> newFunc = null) where T : new()
		{
			return IDictionaryUtil.GetOrGetNew<T>(self, key, newFunc);
		}

		public static T GetOrAddDefault<T>(this IDictionary self, object key, T defaultValue = default)
		{
			return IDictionaryUtil.GetOrAddDefault<T>(self, key, defaultValue);
		}

		public static T GetOrAddDefault<T>(this IDictionary self, object key, Func<T> defaultFunc = null)
		{
			return IDictionaryUtil.GetOrAddDefault<T>(self, key, defaultFunc);
		}
		public static T GetOrAddNew<T>(this IDictionary self, object key) where T : new()
		{
			return IDictionaryUtil.GetOrAddNew<T>(self, key);
		}

		public static T GetOrAddNew<T>(this IDictionary self, object key, Func<T> newFunc = null) where T : new()
		{
			return IDictionaryUtil.GetOrAddNew<T>(self, key, newFunc);
		}


		public static V Remove2<V>(this IDictionary self, object key)
		{
			return IDictionaryUtil.Remove2<V>(self, key);
		}



		//删除值为null值、0数值、false逻辑值、空字符串、空集合等数据项
		public static void Trim(this IDictionary self)
		{
			IDictionaryUtil.Trim(self);
		}

		//删除值为null值、0数值、false逻辑值、空字符串、空集合等数据项
		public static Hashtable ToHashtable(this IDictionary self)
		{
			return IDictionaryUtil.ToHashtable(self);
		}

		public static void Combine(this IDictionary self, IDictionary another)
		{
			IDictionaryUtil.Combine(self, another);
		}

		public static void RemoveByFunc(this IDictionary self, Func<object, object, bool> func)
		{
			IDictionaryUtil.RemoveByFunc(self, func);
		}
	}
}