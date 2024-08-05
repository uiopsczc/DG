namespace DG
{
    public class DGArray<P0, P1> : DGArray<P0>
    {
        public P1 data1;

        public DGArray(P0 data0, P1 data1) : base(data0)
        {
            this.data1 = data1;
        }
    }
}