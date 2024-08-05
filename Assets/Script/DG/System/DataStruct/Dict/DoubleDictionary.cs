using System;
using System.Collections.Generic;

namespace DG
{
    public class DoubleDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private readonly Dictionary<TValue, TKey> _value2Key = new();

        public new TValue this[TKey key]
        {
            get => base[key];
            set
            {
                base[key] = value;
                _value2Key[value] = key;
            }
        }

        public new void Add(TKey key, TValue value)
        {
            this[key] = value;
        }

        public new void Clear()
        {
            base.Clear();
            _value2Key.Clear();
        }

        public bool Remove(TKey key, TValue value)
        {
            return base.Remove(key) && _value2Key.Remove(value);
        }

        public bool RemoveByKey(TKey key)
        {
            return Remove(key, this[key]);
        }

        public bool RemoveByValue(TValue value)
        {
            return Remove(_value2Key[value], value);
        }

        public bool Contains(TKey key, TValue value)
        {
            return ContainsKey(key) && _value2Key.ContainsKey(value);
        }

        public new bool ContainsValue(TValue value)
        {
            return _value2Key.ContainsKey(value);
        }


        public void ForeachKeyValue(Action<TKey, TValue> action)
        {
            foreach (var key in Keys)
                action(key, this[key]);
        }

        public void ForeachValueKey(Action<TValue, TKey> action)
        {
            foreach (var key in _value2Key.Keys)
                action(key, _value2Key[key]);
        }

        public TValue GetValueByKey(TKey key)
        {
            return this[key];
        }

        public TKey GetKeyByValue(TValue value)
        {
            return _value2Key[value];
        }
    }
}