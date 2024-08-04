using System.Collections.Generic;

namespace DG
{
	public class DGPoolItemDict<T>
	{
		private readonly DGPool<T> _pool;
		private readonly Dictionary<T, DGPoolItemIndex<T>> _poolItemIndexDict = new();
		public DGPoolItemDict(DGPool<T> pool)
		{
			this._pool = pool;
		}

		public T Get()
		{
			var (poolItem, poolItemIndex) = this._pool.Spawn(null);
			var value = poolItem.GetValue();
			_poolItemIndexDict[value] = poolItemIndex;
			return value;
		}

		public void Remove(T key)
		{
			if (_poolItemIndexDict.TryGetValue(key, out var poolItemIndex))
			{
				_poolItemIndexDict.Remove(key);
				poolItemIndex.DeSpawn();
			}
		}

		public void Clear()
		{
			foreach (var keyValue in _poolItemIndexDict)
			{
				var poolItemIndex = keyValue.Value;
				poolItemIndex.DeSpawn();
			}
			_poolItemIndexDict.Clear();
		}
	}
}