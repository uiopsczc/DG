using System;
using System.Collections.Generic;

namespace DG
{
	/// <summary>
	/// 缓存
	/// </summary>
	public class Cache : IDespawn
	{
		#region field

		public Dictionary<object, object> dict = new Dictionary<object, object>();

		#endregion

		public object this[object key]
		{
			get => dict[key];
			set => dict[key] = value;
		}

		public void Remove(object key)
		{
			this.dict.Remove(key);
		}

		public object Get(object key)
		{
			return this.dict[key];
		}

		public T Get<T>(object key)
		{
			return (T)this.dict[key];
		}

		public bool ContainsKey(object key)
		{
			return this.dict.ContainsKey(key);
		}

		public bool ContainsKey<T>()
		{
			return this.dict.ContainsKey(typeof(T).FullName);
		}

		public bool ContainsValue(object value)
		{
			return this.dict.ContainsValue(value);
		}


		public T GetOrGetDefault<T>(object key, T defaultValue = default)
		{
			return dict.GetOrGetDefault<T>(key, defaultValue);
		}

		public T GetOrGetDefault<T>(T defaultValue = default)
		{
			return dict.GetOrGetDefault<T>(typeof(T).FullName, defaultValue);
		}

		public T GetOrGetDefault<T>(object key, Func<T> defaultFunc)
		{
			return dict.GetOrGetDefault<T>(key, defaultFunc);
		}

		public T GetOrGetDefault<T>(Func<T> defaultFunc)
		{
			return dict.GetOrGetDefault<T>(typeof(T).FullName, defaultFunc);
		}

		public T GetOrGetNew<T>(object key) where T : new()
		{
			return dict.GetOrGetNew<T>(key);
		}

		public T GetOrGetNew<T>() where T : new()
		{
			return dict.GetOrGetNew<T>(typeof(T).FullName);
		}


		public T GetOrGetNew<T>(object key, Func<T> newFunc) where T : new()
		{
			return dict.GetOrGetNew<T>(key, newFunc);
		}

		public T GetOrGetNew<T>(Func<T> newFunc) where T : new()
		{
			return dict.GetOrGetNew<T>(typeof(T).FullName, newFunc);
		}

		public T GetOrAddDefault<T>(object key, T defaultValue = default)
		{
			return dict.GetOrAddDefault<T>(key, defaultValue);
		}

		public T GetOrAddDefault<T>(T defaultValue = default)
		{
			return dict.GetOrAddDefault<T>(typeof(T).FullName, defaultValue);
		}

		public T GetOrAddDefault<T>(object key, Func<T> defaultFunc)
		{
			return dict.GetOrAddDefault<T>(key, defaultFunc);
		}

		public T GetOrAddDefault<T>(Func<T> defaultFunc)
		{
			return dict.GetOrAddDefault<T>(typeof(T).FullName, defaultFunc);
		}

		public T GetOrAddNew<T>(object key) where T : new()
		{
			return dict.GetOrAddNew<T>(key);
		}

		public T GetOrAddNew<T>() where T : new()
		{
			return dict.GetOrAddNew<T>(typeof(T).FullName);
		}

		public T GetOrAddNew<T>(object key, Func<T> newFunc) where T : new()
		{
			return dict.GetOrAddNew<T>(key, newFunc);
		}

		public T GetOrAddNew<T>(Func<T> newFunc) where T : new()
		{
			return dict.GetOrAddNew<T>(typeof(T).FullName);
		}

		public object Remove2(object key)
		{
			return Remove2<object>(key);
		}

		public T Remove2<T>(object key)
		{
			return dict.Remove2<T>(key);
		}

		public void Clear()
		{
			dict.Clear();
		}

		public void Despawn()
		{
			Clear();
		}
	}
}