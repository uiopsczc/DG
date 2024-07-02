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
	public class FPInterpolationBounceIn : FPInterpolationBounceOut
	{
		public FPInterpolationBounceIn(FP[] widths, FP[] heights) : base(widths, heights)
		{
		}

		public FPInterpolationBounceIn(int bounces) : base(bounces)
		{
		}

		public override FP Apply(FP a)
		{
			return 1 - base.Apply(1 - a);
		}
	}
}
