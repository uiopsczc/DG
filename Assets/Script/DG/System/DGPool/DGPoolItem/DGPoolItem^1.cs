using System.Runtime.CompilerServices;

namespace DG
{
	public class DGPoolItem<T>:IDGPoolItem
	{
		private readonly DGPool<T> _pool;
		private readonly T _value;
		private bool _isDeSpawned;//是否回收了

		public DGPoolItem(DGPool<T> pool, T value, bool isDeSpawned)
		{
			this._pool = pool;
			this._value = value;
			this._isDeSpawned = isDeSpawned;
		}

		public T GetValue()
		{
			return this._value;
		}
		
		public void DeSpawn()
		{
			_pool.DeSpawn(this);
		}
		
		
		public void SetIsDeSpawned(bool isDeSpawned)
		{
			this._isDeSpawned = isDeSpawned;
		}

		public bool IsDeSpawned()
		{
			return this._isDeSpawned;
		}
	}
}