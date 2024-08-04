using System;

namespace DG
{
	public partial class DGPool<T>
	{
		protected virtual T _Spawn()
		{
			return _spawnFunc != null ? _spawnFunc() : (T)Activator.CreateInstance(typeof(T));
		}

		public virtual (DGPoolItem<T> poolItem, DGPoolItemIndex<T> poolItemIndex) Spawn()
		{
			return this.Spawn(null);
		}

		public virtual (DGPoolItem<T> poolItem, DGPoolItemIndex<T> poolItemIndex) Spawn(Action<T> onSpawnCallback = null)
		{
			DGPoolItem<T> poolItem;
			DGPoolItemIndex<T> poolItemIndex;
			for (var i = 0; i < _poolItemList.Count; i++)
			{
				poolItem = _poolItemList[i];
				if (poolItem.IsDeSpawned())
				{
					poolItem.SetIsDeSpawned(false);
					onSpawnCallback?.Invoke(poolItem.GetValue());
					poolItemIndex = new DGPoolItemIndex<T>(this, i);
					return (poolItem, poolItemIndex);
				}
			}
			int index = _poolItemList.Count;
			T value = _Spawn();
			poolItem = new DGPoolItem<T>(this, value, false);
			onSpawnCallback?.Invoke(poolItem.GetValue());
			_poolItemList.Add(poolItem);
			poolItemIndex = new DGPoolItemIndex<T>(this, index);
			return (poolItem, poolItemIndex);
		}

		public virtual T SpawnValue(Action<T> onSpawnCallback = null)
		{
			var (poolItem, poolItemIndex) = this.Spawn(onSpawnCallback);
			return poolItem.GetValue();
		}
	}
}