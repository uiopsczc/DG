using System;
using UnityEngine;

namespace DG
{
	public class DGGameObjectPool : DGUnityObjectPool<GameObject>
	{
		protected Transform _rootTransform;
		protected Transform _categoryTransform;
		private readonly bool _isPrefabActive;

		public DGGameObjectPool(string poolName, GameObject prefab, string category = null) : base(poolName, prefab)
		{
			if (category.IsNullOrWhiteSpace())
				category = prefab.name;
			_isPrefabActive = prefab.activeSelf;
			InitParentTransform(prefab, category);
		}

		public virtual void InitParentTransform(GameObject prefab, string category)
		{
		}

		public override (DGPoolItem<GameObject>, DGPoolItemIndex<GameObject>) Spawn(Action<GameObject> onSpawnCallback = null)
		{
			var (poolItem, poolItemIndex) = base.Spawn(onSpawnCallback);
			_OnSpawn(poolItem);
			return (poolItem, poolItemIndex);
		}

		public override GameObject SpawnValue(Action<GameObject> onSpawnCallback = null)
		{
			var(poolItem, poolItemIndex) = this.Spawn(onSpawnCallback);
			return poolItem.GetValue();
		}

		protected void _OnSpawn(DGPoolItem<GameObject> poolItem)
		{
			GameObject cloneGameObject = poolItem.GetValue();
			cloneGameObject.SetCache(DGPoolConst.POOL_ITEM, poolItem);
			cloneGameObject.SetActive(_isPrefabActive);
			cloneGameObject.transform.CopyFrom(GetPrefab().transform);
		}

		public override void DeSpawn(DGPoolItem<GameObject> poolItem)
		{
			GameObject clone = poolItem.GetValue();
			var components = clone.GetComponents<Component>();
			for (var i = 0; i < components.Length; i++)
			{
				var cloneComponent = components[i];
				var spawnable = cloneComponent as IDeSpawn;
				spawnable?.OnDeSpawn();
			}
			clone.SetActive(false);
			clone.transform.SetParent(_categoryTransform);
			clone.transform.CopyFrom(GetPrefab().transform);
			base.DeSpawn(poolItem);
		}
	}
}