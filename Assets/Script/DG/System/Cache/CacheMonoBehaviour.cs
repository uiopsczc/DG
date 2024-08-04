using System;
using UnityEngine;

namespace DG
{
	public class CacheMonoBehaviour : MonoBehaviour
	{
		private readonly Cache<string> _cache = new();

		public void Set(object obj, string key = null)
		{
			key ??= obj.GetType().FullName;
			_cache[key] = obj;
		}

		public T Get<T>(string key = null)
		{
			key ??= typeof(T).FullName;
			return _cache.Get<T>(key);
		}

		public bool TryGetValue<T>(string key, out T value)
		{
			return _cache.TryGetValue(key, out value);
		}


		public T GetOrGetDefault<T>(string key, T defaultValue = default)
		{
			key ??= typeof(T).FullName;
			return _cache.GetOrGetDefault(key, defaultValue);
		}

		public T GetOrGetByDefaultFunc<T>(string key, Func<T> defaultFunc)
		{
			key ??= typeof(T).FullName;
			return _cache.GetOrGetByDefaultFunc(key, defaultFunc);
		}

		public T GetOrGetNew<T>(string key) where T : new()
		{
			key ??= typeof(T).FullName;
			return _cache.GetOrGetNew<T>(key);
		}

		public T GetOrGetByNewFunc<T>(string key, Func<T> newFunc) where T : new()
		{
			key ??= typeof(T).FullName;
			return _cache.GetOrGetByNewFunc(key, newFunc);
		}

		public T GetOrAddDefault<T>(string key, T defaultValue = default)
		{
			key ??= typeof(T).FullName;
			return _cache.GetOrAddDefault(key, defaultValue);
		}

		public T GetOrAddByDefaultFunc<T>(string key, Func<T> defaultFunc)
		{
			key ??= typeof(T).FullName;
			return _cache.GetOrAddByDefaultFunc(key, defaultFunc);
		}

		public T GetOrAddNew<T>(string key) where T : new()
		{
			key ??= typeof(T).FullName;
			return _cache.GetOrAddNew<T>(key);
		}

		public T GetOrAddByNewFunc<T>(string key, Func<T> newFunc) where T : new()
		{
			key ??= typeof(T).FullName;
			return _cache.GetOrAddByNewFunc(key, newFunc);
		}

		public bool ContainsKey(string key)
		{
			return _cache.ContainsKey(key);
		}

		#region subKey

		public void SetSubKey(object obj, string key, string subKey)
		{
			key ??= typeof(Cache<string>).FullName;
			subKey ??= obj.GetType().FullName;
			Cache<string> subCache = _cache.GetOrAddNew<Cache<string>>(key);
			subCache[subKey] = obj;
		}

		public T GetSubKey<T>(string key, string subKey)
		{
			key ??= typeof(Cache<string>).FullName;
			subKey ??= typeof(T).FullName;
			Cache<string> subCache = Get<Cache<string>>(key);
			return (T) subCache[subKey];
		}

		public T GetSubKeyOrGetDefault<T>(string key, string subKey, T defaultValue = default)
		{
			key ??= typeof(Cache<string>).FullName;
			subKey ??= typeof(T).FullName;
			return TryGetValue<Cache<string>>(key, out var subCache) ? subCache.GetOrGetDefault(subKey, defaultValue) : defaultValue;
		}

		public T GetSubKeyOrGetByDefaultFunc<T>(string key, string subKey, Func<T> defaultFunc)
		{
			key ??= typeof(Cache<string>).FullName;
			subKey ??= typeof(T).FullName;
			if (TryGetValue<Cache<string>>(key, out var subCache))
				return subCache.GetOrGetByDefaultFunc(subKey, defaultFunc);
			return defaultFunc != null ? defaultFunc() : default;
		}

		public T GetSubKeyOrGetNew<T>(string key, string subKey) where T : new()
		{
			key ??= typeof(Cache<string>).FullName;
			subKey ??= typeof(T).FullName;
			return TryGetValue<Cache<string>>(key, out var subCache) ? subCache.GetOrGetNew<T>(subKey) : new T();
		}

		public T GetSubKeyOrGetByNewFunc<T>(string key, string subKey, Func<T> newFunc) where T : new()
		{
			key ??= typeof(Cache<string>).FullName;
			subKey ??= typeof(T).FullName;
			if (TryGetValue<Cache<string>>(key, out var subCache))
				return subCache.GetOrGetByNewFunc(subKey, newFunc);
			return newFunc != null ? newFunc() : new T();
		}

		public T GetSubKeyOrAddDefault<T>(string key, string subKey, T defaultValue = default)
		{
			key ??= typeof(Cache<string>).FullName;
			subKey ??= typeof(T).FullName;
			Cache<string> subCache = GetOrAddNew<Cache<string>>(key);
			return subCache.GetOrAddDefault(subKey, defaultValue);
		}

		public T GetSubKeyOrAddByDefaultFunc<T>(string key, string subKey, Func<T> defaultFunc)
		{
			key ??= typeof(Cache<string>).FullName;
			subKey ??= typeof(T).FullName;
			Cache<string> subCache = GetOrAddNew<Cache<string>>(key);
			return subCache.GetOrAddByDefaultFunc(subKey, defaultFunc);
		}

		public T GetSubKeyOrAddNew<T>(string key, string subKey) where T : new()
		{
			key ??= typeof(Cache<string>).FullName;
			subKey ??= typeof(T).FullName;
			Cache<string> subCache = GetOrAddNew<Cache<string>>(key);
			return subCache.GetOrAddNew<T>(subKey);
		}

		public T GetSubKeyOrAddByNewFunc<T>(string key, string subKey, Func<T> newFunc) where T : new()
		{
			key ??= typeof(Cache<string>).FullName;
			subKey ??= typeof(T).FullName;
			Cache<string> subCache = GetOrAddNew<Cache<string>>(key);
			return subCache.GetOrAddByNewFunc(subKey, newFunc);
		}

		public bool ContainsSubKey(string key, string subKey)
		{
			return TryGetValue<Cache<string>>(key, out var subCache) && subCache.ContainsKey(key);
		}

		#endregion
	}
}