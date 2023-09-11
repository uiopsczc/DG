namespace CsCat
{
	public class DGArray<P0, P1, P2> : DGArray<P0, P1>
	{
		public P2 data2;

		public DGArray(P0 data0, P1 data1, P2 data2) : base(data0, data1)
		{
			this.data2 = data2;
		}
	}
}