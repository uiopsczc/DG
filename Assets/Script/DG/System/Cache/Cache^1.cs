using System;
using System.Collections.Generic;

namespace DG
{
    /// <summary>
    /// 缓存
    /// </summary>
    public class Cache<K> : IDeSpawn
    {
        #region field

        protected readonly Dictionary<K, object> _dict = new();

        #endregion

        public object this[K key]
        {
            get => _dict[key];
            set => _dict[key] = value;
        }

        public void Remove(K key)
        {
            _dict.Remove(key);
        }

        public object Get(K key)
        {
            return _dict[key];
        }

        public T Get<T>(K key)
        {
            return (T)_dict[key];
        }

        public bool TryGetValue<T>(K key, out T value)
        {
            var hasValue = _dict.TryGetValue(key, out var result);
            value = hasValue ? (T)result : default;
            return hasValue;
        }

        public bool ContainsKey(K key)
        {
            return _dict.ContainsKey(key);
        }

        public bool ContainsKey<T>()
        {
            return _dict.ContainsKey(typeof(T).FullName.To<K>());
        }

        public bool ContainsValue(object value)
        {
            return _dict.ContainsValue(value);
        }


        public T GetOrGetDefault<T>(K key, T defaultValue = default)
        {
            if (_dict.TryGetValue(key, out var result))
                return (T)result;
            return defaultValue;
        }

        public T GetOrGetDefault<T>(T defaultValue = default)
        {
            return GetOrGetDefault(typeof(T).FullName.To<K>(), defaultValue);
        }

        public T GetOrGetByDefaultFunc<T>(K key, Func<T> defaultFunc)
        {
            if (_dict.TryGetValue(key, out var result))
                return (T)result;
            return defaultFunc != null ? defaultFunc() : default;
        }

        public T GetOrGetByDefaultFunc<T>(Func<T> defaultFunc)
        {
            return GetOrGetByDefaultFunc(typeof(T).FullName.To<K>(), defaultFunc);
        }

        public T GetOrGetNew<T>(K key) where T : new()
        {
            if (_dict.TryGetValue(key, out var result))
                return (T)result;
            return new T();
        }

        public T GetOrGetNew<T>() where T : new()
        {
            return GetOrGetNew<T>(typeof(T).FullName.To<K>());
        }


        public T GetOrGetByNewFunc<T>(K key, Func<T> newFunc) where T : new()
        {
            if (_dict.TryGetValue(key, out var result))
                return (T)result;
            return newFunc != null ? newFunc() : new T();
        }

        public T GetOrGetByNewFunc<T>(Func<T> newFunc) where T : new()
        {
            return GetOrGetByNewFunc(typeof(T).FullName.To<K>(), newFunc);
        }

        public T GetOrAddDefault<T>(K key, T defaultValue = default)
        {
            if (_dict.TryGetValue(key, out var result))
                return (T)result;
            _dict[key] = defaultValue;
            return defaultValue;
        }

        public T GetOrAddDefault<T>(T defaultValue = default)
        {
            return GetOrAddDefault(typeof(T).FullName.To<K>(), defaultValue);
        }

        public T GetOrAddByDefaultFunc<T>(K key, Func<T> defaultFunc)
        {
            if (_dict.TryGetValue(key, out var result))
                return (T)result;
            result = defaultFunc != null ? defaultFunc() : default;
            _dict[key] = result;
            return (T)result;
        }

        public T GetOrAddByDefaultFunc<T>(Func<T> defaultFunc)
        {
            return GetOrAddByDefaultFunc(typeof(T).FullName.To<K>(), defaultFunc);
        }

        public T GetOrAddNew<T>(K key) where T : new()
        {
            if (_dict.TryGetValue(key, out var result))
                return (T)result;
            result = new T();
            _dict[key] = result;
            return (T)result;
        }

        public T GetOrAddNew<T>() where T : new()
        {
            return GetOrAddNew<T>(typeof(T).FullName.To<K>());
        }

        public T GetOrAddByNewFunc<T>(K key, Func<T> newFunc) where T : new()
        {
            if (_dict.TryGetValue(key, out var result))
                return (T)result;
            result = newFunc != null ? newFunc() : new T();
            _dict[key] = result;
            return (T)result;
        }

        public T GetOrAddNew<T>(Func<T> newFunc) where T : new()
        {
            return GetOrAddByNewFunc(typeof(T).FullName.To<K>(), newFunc);
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