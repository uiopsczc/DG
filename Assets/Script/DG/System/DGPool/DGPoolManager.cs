using System;
using System.Collections.Generic;

namespace DG
{
	public partial class DGPoolManager
	{
		private static DGPoolManager _default;
		public static DGPoolManager Default => _default ??= new DGPoolManager();
		private readonly Dictionary<string, IDGPool> _poolDict = new();
		

		public IDGPool AddPool(string poolName, IDGPool pool)
		{
			_poolDict[poolName] = pool;
			pool.SetPoolManager(this);
			return pool;
		}

		public void RemovePool(string poolName)
		{
			if (_poolDict.TryGetValue(poolName, out var pool))
			{
				pool.Destroy();
				_poolDict.Remove(poolName);
			}
		}

		public IDGPool GetPool(string poolName)
		{
			return _poolDict[poolName];
		}

		public DGPool<T> GetPool<T>(string poolName = null)
		{
			poolName ??= typeof(T).FullName;
			return this.GetPool(poolName) as DGPool<T>;
		}

		public bool TryGetPool(string poolName, out IDGPool pool)
		{
			return this._poolDict.TryGetValue(poolName, out pool);
		}

		public bool TryGetPool<T>(string poolName, out DGPool<T> pool)
		{
			if (_poolDict.TryGetValue(poolName, out var tmpPool))
			{
				pool = tmpPool as DGPool<T>;
				return true;
			}
			pool = null;
			return false;
		}

		public bool IsContainPool(string poolName)
		{
			return _poolDict.ContainsKey(poolName);
		}

		public bool IsContainPool<T>()
		{
			return IsContainPool(typeof(T).FullName);
		}

		public IDGPool GetOrAddPool(Type poolType, params object[] poolConstructArgs)
		{
			if (poolConstructArgs.Length == 0)
				poolConstructArgs = new[] {poolType.FullName};
			string poolName = (string)poolConstructArgs[0];
			if (this.TryGetPool(poolName, out var pool))
				return pool;
			pool = poolType.CreateInstance(poolConstructArgs) as IDGPool;
			AddPool(poolName, pool);
			return pool;
		}

		public void DeSpawnAll(string poolName)
		{
			if (_poolDict.TryGetValue(poolName, out var pool))
				pool.DeSpawnAll();
		}
		

		public (IDGPoolItem poolItem, IDGPoolItemIndex poolItemIndex) Spawn(Type spawnType, string poolName = null)
		{
			return this.InvokeGenericMethod<(IDGPoolItem, IDGPoolItemIndex)>("Spawn", new[] {spawnType}, false, poolName, null, null);
		}

		public object SpawnValue(Type spawnType, string poolName = null)
		{
			return this.InvokeGenericMethod<object>("SpawnValue", new[] { spawnType }, false, poolName, null, null);
		}


		public (DGPoolItem<T> poolItem, DGPoolItemIndex<T> poolItemIndex) Spawn<T>(string poolName, Func<T> spawnFunc, Action<T> onSpawnCallback = null)
		{
			poolName ??= typeof(T).FullName;
			if (!_poolDict.TryGetValue(poolName, out var pool))
			{
				pool = new DGPool<T>(poolName, spawnFunc);
				this.AddPool(poolName, pool);
			}
			var (poolItem, poolItemIndex) = ((DGPool<T>)pool).Spawn(onSpawnCallback);
			return (poolItem, poolItemIndex);
		}

		public T SpawnValue<T>(string poolName, Func<T> spawnFunc, Action<T> onSpawnCallback = null)
		{
			poolName ??= typeof(T).FullName;
			if (!_poolDict.TryGetValue(poolName, out var pool))
			{
				pool = new DGPool<T>(poolName, spawnFunc);
				this.AddPool(poolName, pool);
			}
			var value = ((DGPool<T>)pool).SpawnValue(onSpawnCallback);
			return value;
		}

		public void DeSpawnValue<T>(T value, string poolName = null)
		{
			poolName ??= typeof(T).FullName;
			var pool = _poolDict[poolName] as DGPool<T>;
			pool.DeSpawnValue(value);
		}
	}
}