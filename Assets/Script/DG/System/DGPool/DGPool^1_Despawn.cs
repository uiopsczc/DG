using UnityEngine;

namespace DG
{
	public partial class DGPool<T>
	{
		public virtual void DeSpawn(DGPoolItem<T> poolItem)
		{
			poolItem.SetIsDeSpawned(true);
			var value = poolItem.GetValue();
			_DeSpawn(value);
		}

		void _DeSpawn(T value)
		{
			IDeSpawn spawnable = value as IDeSpawn;
			spawnable?.OnDeSpawn();
		}

		public virtual void DeSpawnValue(T value)
		{
			for (int i = 0; i < _poolItemList.Count; i++)
			{
				var poolItem = _poolItemList[i];
				if (poolItem.GetValue().Equals(value))
				{
					DeSpawn(poolItem);
					break;
				}
			}
		}


		public void DeSpawnAll()
		{
			for (int i = 0; i < _poolItemList.Count; i++)
			{
				var poolItem = _poolItemList[i];
				if (!poolItem.IsDeSpawned())
					DeSpawn(poolItem);
			}
		}
	}
}