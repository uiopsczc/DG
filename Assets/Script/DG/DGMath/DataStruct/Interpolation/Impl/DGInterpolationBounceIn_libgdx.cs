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
	public class DGInterpolationBounceIn : DGInterpolationBounceOut
	{
		public DGInterpolationBounceIn(DGFixedPoint[] widths, DGFixedPoint[] heights) : base(widths, heights)
		{
		}

		public DGInterpolationBounceIn(int bounces) : base(bounces)
		{
		}

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			return (DGFixedPoint)1 - base.Apply((DGFixedPoint)1 - a);
		}
	}
}
