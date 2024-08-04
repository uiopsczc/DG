using System;

namespace DG
{
	public class SpawnPoolScope<T> : PoolScope
	{
		private T _spawn;
		private DGPoolItem<T> _poolItem;
		private Action<T> _onSpawnCallback;

		public T spawn
		{
			get
			{
				if (_spawn == null)
				{
					(_poolItem,_) = DGPoolManager.Default.Spawn(null, null, _onSpawnCallback);
					_spawn = _poolItem.GetValue();
				}

				return _spawn;
			}
		}


		public SpawnPoolScope(Action<T> onSpawnCallback = null)
		{
			this._onSpawnCallback = onSpawnCallback;
		}

		public override void Dispose()
		{
			DGPoolManager.Default.GetPool<T>().DeSpawn(_poolItem);
			_spawn = default;
			_poolItem = null;
			this._onSpawnCallback = null;
			base.Dispose();
		}
	}
}