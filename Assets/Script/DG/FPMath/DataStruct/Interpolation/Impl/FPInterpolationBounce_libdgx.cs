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
	public class FPInterpolationBounce : FPInterpolationBounceOut
	{
		public FPInterpolationBounce(FP[] widths, FP[] heights) : base(widths, heights)
		{
		}

		public FPInterpolationBounce(int bounces) : base(bounces)
		{
		}

		private FP Out(FP a)
		{
			FP test = a + widths[0] / 2;
			if (test < widths[0]) return test / (widths[0] / 2) - 1;
			return base.Apply(a);
		}

		public override FP Apply(FP a)
		{
			if (a <= 0.5f) return (1 - Out(1 - a * 2)) / 2;
			return Out(a * 2 - 1) / 2 + 0.5f;
		}
	}
}
