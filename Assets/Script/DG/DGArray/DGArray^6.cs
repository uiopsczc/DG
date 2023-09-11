namespace CsCat
{
	public class DGArray<P0, P1, P2, P3, P4, P5> : DGArray<P0, P1, P2, P3, P4>
	{
		public P5 data5;

		public DGArray(P0 data0, P1 data1, P2 data2, P3 data3, P4 data4, P5 data5) : base(data0, data1, data2, data3,
			data4)
		{
			this.data5 = data5;
		}
	}
}