using System;
using Object = UnityEngine.Object;

namespace DG
{
	public class DGUnityObjectPool<T> : DGPool<T> where T: Object
	{
		private T _prefab;

		public DGUnityObjectPool(string poolName, T prefab) : base(poolName)
		{
			this._poolName = poolName ?? DGPoolManagerUtil.GetPrefabPoolDefaultName(prefab);
			this._prefab = prefab;
		}
		

		public T GetPrefab()
		{
			return _prefab;
		}

		protected override T _Spawn()
		{
			T clone = Object.Instantiate(_prefab);
			clone.name = _prefab.name;
			return clone;
		}

		protected override void _OnDestroy(T value)
		{
			value.Destroy();
			base._OnDestroy(value);
		}
	}
}

