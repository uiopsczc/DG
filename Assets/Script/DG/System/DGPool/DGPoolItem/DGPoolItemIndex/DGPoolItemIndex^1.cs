namespace DG
{
	public class DGPoolItemIndex<T>:IDGPoolItemIndex
	{
		private readonly DGPool<T> _pool;
		private readonly int _index;
		public DGPoolItemIndex(DGPool<T> pool, int index)
		{
			this._pool = pool;
			this._index = index;
		}

		public DGPool<T> GetPool()
		{
			return this._pool;
		}

		public IDGPool GetIPool()
		{
			return this._pool;
		}

		
		public DGPoolItem<T> GetPoolItem()
		{
			return this._pool.GetPoolItemAtIndex(this._index);
		}

		
		public T GetValue()
		{
			return this.GetPoolItem().GetValue();
		}

		public T2 GetValue<T2>() where  T2:class 
		{
			return GetValue() as T2;
		}

		
		public int GetIndex()
		{
			return this._index;
		}

		public void DeSpawn()
		{
			var poolItem = this.GetPoolItem();
			poolItem.DeSpawn();
		}

		object IDGPoolItemIndex.GetValue()
		{
			return this.GetValue();
		}
	}
}