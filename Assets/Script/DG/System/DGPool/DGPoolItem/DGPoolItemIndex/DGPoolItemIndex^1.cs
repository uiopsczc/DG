namespace DG
{
    public class DGPoolItemIndex<T> : IDGPoolItemIndex
    {
        private readonly DGPool<T> _pool;
        private readonly int _index;

        public DGPoolItemIndex(DGPool<T> pool, int index)
        {
            _pool = pool;
            _index = index;
        }

        public DGPool<T> GetPool()
        {
            return _pool;
        }

        public IDGPool GetIPool()
        {
            return _pool;
        }


        public DGPoolItem<T> GetPoolItem()
        {
            return _pool.GetPoolItemAtIndex(_index);
        }


        public T GetValue()
        {
            return GetPoolItem().GetValue();
        }

        public T2 GetValue<T2>() where T2 : class
        {
            return GetValue() as T2;
        }


        public int GetIndex()
        {
            return _index;
        }

        public void DeSpawn()
        {
            var poolItem = GetPoolItem();
            poolItem.DeSpawn();
        }

        object IDGPoolItemIndex.GetValue()
        {
            return GetValue();
        }
    }
}