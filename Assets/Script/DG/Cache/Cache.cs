using System;
using System.Collections.Generic;

namespace DG
{
	/// <summary>
	/// 缓存
	/// </summary>
	public class Cache : IDeSpawn
	{
		#region field

		protected Dictionary<object, object> _dict = new Dictionary<object, object>();

		#endregion

		public object this[object key]
		{
			get => _dict[key];
			set => _dict[key] = value;
		}

		public void Remove(object key)
		{
			this._dict.Remove(key);
		}

		public object Get(object key)
		{
			return this._dict[key];
		}

		public T Get<T>(object key)
		{
			return (T)this._dict[key];
		}

		public bool ContainsKey(object key)
		{
			return this._dict.ContainsKey(key);
		}

		public bool ContainsKey<T>()
		{
			return this._dict.ContainsKey(typeof(T).FullName);
		}

		public bool ContainsValue(object value)
		{
			return this._dict.ContainsValue(value);
		}


		public T GetOrGetDefault<T>(object key, T defaultValue = default)
		{
			return _dict.GetOrGetDefault<T>(key, defaultValue);
		}

		public T GetOrGetDefault<T>(T defaultValue = default)
		{
			return GetOrGetDefault<T>(typeof(T).FullName, defaultValue);
		}

		public T GetOrGetByDefaultFunc<T>(object key, Func<T> defaultFunc)
		{
			return _dict.GetOrGetByDefaultFunc<T>(key, defaultFunc);
		}

		public T GetOrGetByDefaultFunc<T>(Func<T> defaultFunc)
		{
			return GetOrGetByDefaultFunc<T>(typeof(T).FullName, defaultFunc);
		}

		public T GetOrGetNew<T>(object key) where T : new()
		{
			return _dict.GetOrGetNew<T>(key);
		}

		public T GetOrGetNew<T>() where T : new()
		{
			return GetOrGetNew<T>(typeof(T).FullName);
		}


		public T GetOrGetByNewFunc<T>(object key, Func<T> newFunc) where T : new()
		{
			return _dict.GetOrGetByNewFunc<T>(key, newFunc);
		}

		public T GetOrGetByNewFunc<T>(Func<T> newFunc) where T : new()
		{
			return GetOrGetByNewFunc<T>(typeof(T).FullName, newFunc);
		}

		public T GetOrAddDefault<T>(object key, T defaultValue = default)
		{
			return _dict.GetOrAddDefault<T>(key, defaultValue);
		}

		public T GetOrAddDefault<T>(T defaultValue = default)
		{
			return GetOrAddDefault<T>(typeof(T).FullName, defaultValue);
		}

		public T GetOrAddByDefaultFunc<T>(object key, Func<T> defaultFunc)
		{
			return _dict.GetOrAddByDefaultFunc<T>(key, defaultFunc);
		}

		public T GetOrAddByDefaultFunc<T>(Func<T> defaultFunc)
		{
			return GetOrAddByDefaultFunc<T>(typeof(T).FullName, defaultFunc);
		}

		public T GetOrAddNew<T>(object key) where T : new()
		{
			return _dict.GetOrAddByNew<T>(key);
		}

		public T GetOrAddNew<T>() where T : new()
		{
			return GetOrAddNew<T>(typeof(T).FullName);
		}

		public T GetOrAddByNewFunc<T>(object key, Func<T> newFunc) where T : new()
		{
			return _dict.GetOrAddByNewFunc<T>(key, newFunc);
		}

		public T GetOrAddByNewFunc<T>(Func<T> newFunc) where T : new()
		{
			return GetOrAddByNewFunc(typeof(T).FullName, newFunc);
		}

		public object Remove2(object key)
		{
			return Remove2<object>(key);
		}

		public T Remove2<T>(object key)
		{
			return _dict.Remove2<T>(key);
		}

		public void Clear()
		{
			_dict.Clear();
		}

		public void DeSpawn()
		{
			Clear();
		}
	}
}