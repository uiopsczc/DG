namespace DG
{
    public class TestJsonData : ISingleton
    {
        public void Init()
        {
        }

        public TestJsonData()
        {
            Data = new DGJsonData();
            Data.Init(filePath);
        }


        public static TestJsonData Instance => SingletonFactory.instance.Get<TestJsonData>();


        public DGJsonData Data;


        private readonly string filePath = "Data/TestJsonData";
    }
}