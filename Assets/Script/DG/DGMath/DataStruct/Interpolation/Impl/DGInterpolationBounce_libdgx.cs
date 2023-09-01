/*************************************************************************************
 * ��    ��:  
 * �� �� ��:  czq
 * ����ʱ��:  2023/5/12
 * ======================================
 * ��ʷ���¼�¼
 * �汾:V          �޸�ʱ��:         �޸���:
 * �޸�����:
 * ======================================
*************************************************************************************/

namespace DG
{
	public class DGInterpolationBounce : DGInterpolationBounceOut
	{
		public DGInterpolationBounce(DGFixedPoint[] widths, DGFixedPoint[] heights) : base(widths, heights)
		{
		}

		public DGInterpolationBounce(int bounces) : base(bounces)
		{
		}

		private DGFixedPoint Out(DGFixedPoint a)
		{
			DGFixedPoint test = a + widths[0] / (DGFixedPoint)2;
			if (test < widths[0]) return test / (widths[0] / (DGFixedPoint)2) - (DGFixedPoint)1;
			return base.Apply(a);
		}

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			if (a <= (DGFixedPoint)0.5f) return ((DGFixedPoint)1 - Out((DGFixedPoint)1 - a * (DGFixedPoint)2)) / (DGFixedPoint)2;
			return Out(a * (DGFixedPoint)2 - (DGFixedPoint)1) / (DGFixedPoint)2 + (DGFixedPoint)0.5f;
		}
	}
}
