using UnityEngine;

namespace DG
{
    public partial class DGPoolManager
    {
        public DGGameObjectPool AddGameObjectPool(string poolName, GameObject prefab, string category = null)
        {
            DGGameObjectPool pool = null;
            if (!prefab.IsHasComponent<RectTransform>())
                pool = new DGNormalGameObjectPool(poolName, prefab, category);
            else
                pool = new DGUIGameObjectPool(poolName, prefab, category);
            AddPool(poolName, pool);
            return pool;
        }

        public DGGameObjectPool GetGameObjectPool(string poolName)
        {
            return GetPool(poolName) as DGGameObjectPool;
        }

        public DGGameObjectPool GetGameObjectPool(GameObject prefab)
        {
            var poolName = DGPoolManagerUtil.GetPrefabPoolDefaultName(prefab);
            return GetGameObjectPool(poolName);
        }

        public DGGameObjectPool GetOrAddGameObjectPool(string poolName, GameObject prefab,
            string category = null)
        {
            poolName ??= DGPoolManagerUtil.GetPrefabPoolDefaultName(prefab);
            if (TryGetPool(poolName, out var pool))
                return pool as DGGameObjectPool;
            return AddPool(poolName, AddGameObjectPool(poolName, prefab, category)) as DGGameObjectPool;
        }
    }
}