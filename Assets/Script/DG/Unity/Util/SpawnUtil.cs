using UnityEngine;

namespace DG
{
    public class SpawnUtil
    {
        public static GameObject Instantiate(GameObject prefab, Transform parentTransform = null)
        {
            if (prefab == null)
                return null;
            var clone = Object.Instantiate(prefab, parentTransform);
            return clone;
        }

        public static void Destroy(GameObject clone)
        {
            if (clone != null)
                clone.Destroy();
        }

        //冗余，不写lambda的原因是lambda会产生gc，这个不会
        public static void Destroy2(GameObject clone, Transform parentTransform)
        {
            Destroy(clone);
        }

        public static DGGameObjectPool GetOrAddGameObjectPool(string poolName, GameObject prefab,
            string categoryName = null)
        {
            return DGPoolManager.Default.GetOrAddGameObjectPool(poolName, prefab, categoryName);
        }

        public static GameObject SpawnGameObject(string poolName, GameObject prefab, string categoryName = null,
            Transform parentTransform = null)
        {
            if (prefab == null)
                return null;
            var pool = GetOrAddGameObjectPool(poolName, prefab, categoryName);
            var clone = pool.SpawnValue();
            clone.transform.SetParent(parentTransform);
            clone.transform.CopyFrom(pool.GetPrefab().transform);
            return clone;
        }

        public static void DespawnGameObject(GameObject clone, Transform parentTransform = null)
        {
            if (clone == null)
                return;
            clone.DeSpawn();
            if (parentTransform != null)
                clone.transform.SetParent(parentTransform);
        }
    }
}