using UnityEngine;

namespace DG
{
    public class DGUIGameObjectPool : DGGameObjectPool
    {
        private readonly Cache _cache = new();

        private RectTransform _prefabRectTransform => _cache.GetOrAddByDefaultFunc("_prefabRectTransform",
            () => GetPrefab().GetComponent<RectTransform>());

        public DGUIGameObjectPool(string poolName, GameObject prefab, string category = null) : base(poolName, prefab,
            category)
        {
        }

        public override void InitParentTransform(GameObject prefab, string category)
        {
            base.InitParentTransform(prefab, category);
            _rootTransform = GameObjectUtil.GetOrNewGameObject("UIPools", null).transform;
            _rootTransform.gameObject.AddComponent<Canvas>();
            _categoryTransform = _rootTransform.GetOrNewGameObject(category).transform;
        }
    }
}