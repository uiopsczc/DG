using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
	public class CacheMonoBehaviour : MonoBehaviour
	{
		public Dictionary<string, object> dict = new Dictionary<string, object>();

		public void Set(object obj, string key = null)
		{
			if (key == null)
				key = obj.GetType().FullName;
			dict[key] = obj;
		}

		public void Set(object obj, string key, string subKey)
		{
			if (key == null)
				key = obj.GetType().FullName;
			Dictionary<string, object> subDict = dict.GetOrAddNew<Dictionary<string, object>>(key);
			subDict[subKey] = obj;
		}

		public T Get<T>(string key = null)
		{
			if (key == null)
				key = typeof(T).FullName;
			return dict.Get<T>(key);
		}

		public T GetOrGetDefault<T>(string key, T defaultValue = default)
		{
			if (key == null)
				key = typeof(T).FullName;
			return dict.GetOrGetDefault<T>(key, defaultValue);
		}

		public T GetOrGetByDefaultFunc<T>(string key, Func<T> defaultFunc)
		{
			if (key == null)
				key = typeof(T).FullName;
			return dict.GetOrGetByDefaultFunc<T>(key, defaultFunc);
		}

		public T GetOrGetNew<T>(string key) where T : new()
		{
			if (key == null)
				key = typeof(T).FullName;
			return dict.GetOrGetNew<T>(key);
		}

		public T GetOrGetByNewFunc<T>(string key, Func<T> newFunc) where T : new()
		{
			if (key == null)
				key = typeof(T).FullName;
			return dict.GetOrGetByNewFunc(key, newFunc);
		}

		public T GetOrAddDefault<T>(string key, T defaultValue = default)
		{
			if (key == null)
				key = typeof(T).FullName;
			return dict.GetOrAddDefault<T>(key, defaultValue);
		}

		public T GetOrAddByDefaultFunc<T>(string key, Func<T> defaultFunc)
		{
			if (key == null)
				key = typeof(T).FullName;
			return dict.GetOrAddByDefaultFunc<T>(key, defaultFunc);
		}

		public T GetOrAddNew<T>(string key) where T : new()
		{
			if (key == null)
				key = typeof(T).FullName;
			return dict.GetOrAddNew<T>(key);
		}

		public T GetOrAddByNewFunc<T>(string key, Func<T> newFunc) where T : new()
		{
			if (key == null)
				key = typeof(T).FullName;
			return dict.GetOrAddByNewFunc(key, newFunc);
		}

		public T Get<T>(string key, string subKey)
		{
			Dictionary<string, object> subDict = Get<Dictionary<string, object>>(key);
			return (T) subDict[key];
		}

		public T GetOrAddByDefaultFunc<T>(string key, string subKey, Func<T> defaultFunc)
		{
			Dictionary<string, object> subDict = GetOrAddNew<Dictionary<string, object>>(key);
			return subDict.GetOrAddByDefaultFunc<T>(subKey, defaultFunc);
		}
	}
}