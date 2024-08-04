using UnityEngine;

namespace DG
{
	public static class DGPoolGameObjectTest
	{
		public static void Test(GameObject prefab)
		{
			var pool = DGPoolManager.Default.GetOrAddGameObjectPool(null, prefab);
			var clone = pool.SpawnValue();
			pool = DGPoolManager.Default.GetGameObjectPool(prefab);
			var poolItem = clone.GetCache<DGPoolItem<GameObject>>(DGPoolConst.POOL_ITEM);
			poolItem.DeSpawn();
			pool.SpawnValue();
			
			// DGLog.Warn(pool.GetPoolName());
		}
	}
}