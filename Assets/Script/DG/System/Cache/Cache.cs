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

        protected readonly Dictionary<object, object> _dict = new();

        #endregion

        public object this[object key]
        {
            get => _dict[key];
            set => _dict[key] = value;
        }

        public void Remove(object key)
        {
            _dict.Remove(key);
        }

        public object Get(object key)
        {
            return _dict[key];
        }

        public T Get<T>(object key)
        {
            return (T)_dict[key];
        }

        public bool TryGetValue(object key, out object value)
        {
            var hasValue = _dict.TryGetValue(key, out value);
            return hasValue;
        }

        public bool ContainsKey(object key)
        {
            return _dict.ContainsKey(key);
        }

        public bool ContainsKey<T>()
        {
            return _dict.ContainsKey(typeof(T).FullName);
        }

        public bool ContainsValue(object value)
        {
            return _dict.ContainsValue(value);
        }


        public T GetOrGetDefault<T>(object key, T defaultValue = default)
        {
            return _dict.GetOrGetDefault(key, defaultValue);
        }

        public T GetOrGetDefault<T>(T defaultValue = default)
        {
            return GetOrGetDefault(typeof(T).FullName, defaultValue);
        }

        public T GetOrGetByDefaultFunc<T>(object key, Func<T> defaultFunc)
        {
            return _dict.GetOrGetByDefaultFunc(key, defaultFunc);
        }

        public T GetOrGetByDefaultFunc<T>(Func<T> defaultFunc)
        {
            return GetOrGetByDefaultFunc(typeof(T).FullName, defaultFunc);
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
            return _dict.GetOrGetByNewFunc(key, newFunc);
        }

        public T GetOrGetByNewFunc<T>(Func<T> newFunc) where T : new()
        {
            return GetOrGetByNewFunc(typeof(T).FullName, newFunc);
        }

        public T GetOrAddDefault<T>(object key, T defaultValue = default)
        {
            return _dict.GetOrAddDefault(key, defaultValue);
        }

        public T GetOrAddDefault<T>(T defaultValue = default)
        {
            return GetOrAddDefault(typeof(T).FullName, defaultValue);
        }

        public T GetOrAddByDefaultFunc<T>(object key, Func<T> defaultFunc)
        {
            return _dict.GetOrAddByDefaultFunc(key, defaultFunc);
        }

        public T GetOrAddByDefaultFunc<T>(Func<T> defaultFunc)
        {
            return GetOrAddByDefaultFunc(typeof(T).FullName, defaultFunc);
        }

        public T GetOrAddNew<T>(object key) where T : new()
        {
            return _dict.GetOrAddNew<T>(key);
        }

        public T GetOrAddNew<T>() where T : new()
        {
            return GetOrAddNew<T>(typeof(T).FullName);
        }

        public T GetOrAddByNewFunc<T>(object key, Func<T> newFunc) where T : new()
        {
            return _dict.GetOrAddByNewFunc(key, newFunc);
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

        public void OnDeSpawn()
        {
            Clear();
        }
    }
}