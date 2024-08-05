namespace DG
{
    public class DGPoolItem<T> : IDGPoolItem
    {
        private readonly DGPool<T> _pool;
        private readonly T _value;
        private bool _isDeSpawned; //是否回收了

        public DGPoolItem(DGPool<T> pool, T value, bool isDeSpawned)
        {
            _pool = pool;
            _value = value;
            _isDeSpawned = isDeSpawned;
        }

        public T GetValue()
        {
            return _value;
        }

        public void DeSpawn()
        {
            _pool.DeSpawn(this);
        }


        public void SetIsDeSpawned(bool isDeSpawned)
        {
            _isDeSpawned = isDeSpawned;
        }

        public bool IsDeSpawned()
        {
            return _isDeSpawned;
        }
    }
}