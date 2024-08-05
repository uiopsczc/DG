using System;
using System.Collections.Generic;

namespace DG
{
    public static class IDictionaryKVUtil
    {
        public static V Get<K, V>(IDictionary<K, V> dict, K key)
        {
            if (dict.TryGetValue(key, out var result))
                return result;
            return default;
        }

        public static V GetOrGetDefault<K, V>(IDictionary<K, V> dict, K key, V defaultValue = default)
        {
            if (dict.TryGetValue(key, out var result))
                return result;
            return defaultValue;
        }

        public static V GetOrGetByDefaultFunc<K, V>(IDictionary<K, V> dict, K key, Func<V> defaultFunc = null)
        {
            if (dict.TryGetValue(key, out var result))
                return result;
            return defaultFunc != null ? defaultFunc() : default;
        }

        public static V GetOrGetNew<K, V>(IDictionary<K, V> dict, K key) where V : new()
        {
            if (dict.TryGetValue(key, out var result))
                return result;
            return new V();
        }

        public static V GetOrGetByNewFunc<K, V>(IDictionary<K, V> dict, K key, Func<V> newFunc = null) where V : new()
        {
            if (dict.TryGetValue(key, out var result))
                return result;
            return newFunc != null ? newFunc() : new V();
        }

        public static V GetOrAddDefault<K, V>(IDictionary<K, V> dict, K key, V defaultValue = default)
        {
            if (dict.TryGetValue(key, out var result))
                return result;
            dict[key] = defaultValue;
            return defaultValue;
        }

        public static V GetOrAddByDefaultFunc<K, V>(IDictionary<K, V> dict, K key, Func<V> defaultFunc = null)
        {
            if (dict.TryGetValue(key, out var result))
                return result;
            result = defaultFunc != null ? defaultFunc() : default;
            dict[key] = result;
            return result;
        }

        public static V GetOrAddNew<K, V>(IDictionary<K, V> dict, K key) where V : new()
        {
            if (dict.TryGetValue(key, out var result))
                return result;
            result = new V();
            dict[key] = result;
            return result;
        }

        public static V GetOrAddByNewFunc<K, V>(IDictionary<K, V> dict, K key, Func<V> newFunc = null) where V : new()
        {
            if (dict.TryGetValue(key, out var result))
                return result;
            result = newFunc != null ? newFunc() : new V();
            dict[key] = result;
            return result;
        }

        public static void RemoveByFunc<K, V>(IDictionary<K, V> dict, Func<K, V, bool> func)
        {
            List<K> toRemoveKeyList = new List<K>(dict.Count / 2);
            foreach (var keyValue in dict)
            {
                var key = keyValue.Key;
                var value = keyValue.Value;
                if (func(key, value))
                    toRemoveKeyList.Add(key);
            }

            for (var i = 0; i < toRemoveKeyList.Count; i++)
            {
                var toRemoveKey = toRemoveKeyList[i];
                dict.Remove(toRemoveKey);
            }
        }

        public static void RemoveByValue<K, V>(IDictionary<K, V> dict, V value, bool isAll = false)
        {
            bool isHasRemoveKey = false;
            if (isAll == false)
            {
                K toRemoveKey = default;
                foreach (var keyValue in dict)
                {
                    if (!ObjectUtil.Equals(keyValue.Value, value)) continue;
                    isHasRemoveKey = true;
                    toRemoveKey = keyValue.Key;
                    break;
                }

                if (isHasRemoveKey)
                    dict.Remove(toRemoveKey);
                return;
            }

            List<K> toRemoveKeyList = new List<K>(dict.Count / 2);
            foreach (var keyValue in dict)
            {
                if (ObjectUtil.Equals(keyValue.Value, value))
                    toRemoveKeyList.Add(keyValue.Key);
            }

            for (var i = 0; i < toRemoveKeyList.Count; i++)
            {
                var toRemoveKey = toRemoveKeyList[i];
                dict.Remove(toRemoveKey);
            }
        }

        public static void RemoveAllAndClear<K, V>(IDictionary<K, V> dict, Action<K, V> onRemoveAction)
        {
            foreach (var keyValue in dict)
                onRemoveAction(keyValue.Key, keyValue.Value);
            dict.Clear();
        }


        public static V Remove2<K, V>(IDictionary<K, V> dict, K key)
        {
            if (dict.TryGetValue(key, out var result))
            {
                dict.Remove(key);
                return result;
            }

            return default;
        }

        public static void Combine<K, V>(IDictionary<K, V> dict, IDictionary<K, V> another)
        {
            foreach (var anotherKeyValue in another)
            {
                var anotherKey = anotherKeyValue.Key;
                if (!dict.ContainsKey(anotherKey))
                    dict[anotherKey] = another[anotherKey];
            }
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
            foreach (var anotherKV in another)
            {
                var anotherKey = anotherKV.Key;
                var anotherValue = anotherKV.Value;
                if (!self.ContainsKey(anotherKey))
                    self[anotherKey] = anotherValue;
                else //重复
                {
                    var key = anotherKey;
                    var selfValue = self[anotherKey];
                    self[anotherKey] = combineCallback(key, selfValue, anotherValue);
                }
            }

            return self;
        }


        public static List<T> RandomList<T>(IDictionary<T, float> self, int outCount, bool isUnique,
            RandomManager randomManager)
        {
            return randomManager.RandomList(self, outCount, isUnique);
        }

        public static T Random<T>(IDictionary<T, float> self, RandomManager randomManager)
        {
            return randomManager.RandomList(self, 1, false)[0];
        }

        public static K FindKey<K, V>(IDictionary<K, V> dict, K key)
        {
            foreach (var keyValue in dict)
            {
                if (keyValue.Key.Equals(key))
                    return keyValue.Key;
            }

            return default;
        }

        public static IDictionary<K, V> EmptyIfNull<K, V>(IDictionary<K, V> dict)
        {
            return dict ?? Activator.CreateInstance<IDictionary<K, V>>();
        }
    }
}