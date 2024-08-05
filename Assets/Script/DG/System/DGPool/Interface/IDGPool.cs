namespace DG
{
    public interface IDGPool
    {
        void Destroy();
        void DeSpawnAll();

        void SetPoolManager(DGPoolManager poolManager);

        DGPoolManager GetPoolManager();
    }
}