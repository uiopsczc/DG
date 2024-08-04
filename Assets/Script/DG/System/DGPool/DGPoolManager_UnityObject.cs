using System;
using Object = UnityEngine.Object;

namespace DG
{
	public partial class DGPoolManager
	{
		public DGUnityObjectPool<T> AddUnityObjectPool<T>(string poolName, T prefab) where T : Object
		{
			var pool = new DGUnityObjectPool<T>(poolName, prefab);
			this.AddPool(pool.GetPoolName(), pool);
			return pool;
		}

		public DGUnityObjectPool<T> GetOrAddUnityObjectPool<T>(string poolName, T prefab) where T : Object
		{
			poolName ??= DGPoolManagerUtil.GetPrefabPoolDefaultName(prefab);
			if (this.TryGetPool(poolName, out var pool))
				return pool as DGUnityObjectPool<T>;
			return AddUnityObjectPool(poolName, prefab);
		}
		
		public  DGUnityObjectPool<T> GetUnityObjectPool<T>(string poolName) where T:Object
		{
			return this.GetPool(poolName) as DGUnityObjectPool<T>;
		}
		
		public  DGUnityObjectPool<T> GetUnityObjectPool<T>(T prefab)where T:Object
		{
			var poolName = DGPoolManagerUtil.GetPrefabPoolDefaultName(prefab);
			return GetUnityObjectPool<T>(poolName);
		}
	}
}