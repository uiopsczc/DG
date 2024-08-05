using System.Collections.Generic;

namespace DG
{
    public class DGPoolItemDict<T>
    {
        private readonly DGPool<T> _pool;
        private readonly Dictionary<T, DGPoolItemIndex<T>> _value2PoolItemIndex = new();

        public DGPoolItemDict(DGPool<T> pool)
        {
            _pool = pool;
        }

        public T Get()
        {
            var (poolItem, poolItemIndex) = _pool.Spawn(null);
            var value = poolItem.GetValue();
            _value2PoolItemIndex[value] = poolItemIndex;
            return value;
        }

        public void Remove(T key)
        {
            if (_value2PoolItemIndex.TryGetValue(key, out var poolItemIndex))
            {
                _value2PoolItemIndex.Remove(key);
                poolItemIndex.DeSpawn();
            }
        }

        public void Clear()
        {
            foreach (var keyValue in _value2PoolItemIndex)
            {
                var poolItemIndex = keyValue.Value;
                poolItemIndex.DeSpawn();
            }

            _value2PoolItemIndex.Clear();
        }
    }
}