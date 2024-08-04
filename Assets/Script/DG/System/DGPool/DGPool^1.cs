using System;
using System.Collections.Generic;

namespace DG
{
	public partial class DGPool<T>:IDGPool
	{
		/// <summary>
		/// 存放item的List
		/// </summary>
		private readonly List<DGPoolItem<T>> _poolItemList = new();
		protected string _poolName;
		private DGPoolManager _poolManager;
		private Func<T> _spawnFunc;



		public DGPool(string poolName)
		{
			this._poolName = poolName;
		}

		public DGPool(string poolName, Func<T> spawnFunc)
		{
			this._poolName = poolName;
			this._spawnFunc = spawnFunc;
		}

		public string GetPoolName()
		{
			return this._poolName;
		}

		public void SetPoolManager(DGPoolManager poolManager)
		{
			this._poolManager = poolManager;
		}

		public DGPoolManager GetPoolManager()
		{
			return this._poolManager;
		}

		public void InitPool(int initCount = 1, Action<T> onSpawnCallback = null)
		{
			for (int i = 0; i < initCount; i++)
			{
				var (poolItem, poolItemIndex) = Spawn(onSpawnCallback);
				DeSpawn(poolItem);
			}
		}

		public DGPoolItem<T> GetPoolItemAtIndex(int index)
		{
			return this._poolItemList[index];
		}
	}
}