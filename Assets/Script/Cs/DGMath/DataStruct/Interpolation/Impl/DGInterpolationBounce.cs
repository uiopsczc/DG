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

using FP = DGFixedPoint;

//libgdx
public class DGInterpolationBounce : DGInterpolationBounceOut
{
	public DGInterpolationBounce(FP[] widths, FP[] heights) : base(widths, heights)
	{
	}

	public DGInterpolationBounce(int bounces) : base(bounces)
	{
	}

	private FP Out(FP a)
	{
		FP test = a + widths[0] / (FP) 2;
		if (test < widths[0]) return test / (widths[0] / (FP) 2) - (FP) 1;
		return base.Apply(a);
	}

	public override FP Apply(FP a)
	{
		if (a <= (FP) 0.5f) return ((FP) 1 - Out((FP) 1 - a * (FP) 2)) / (FP) 2;
		return Out(a * (FP) 2 - (FP) 1) / (FP) 2 + (FP) 0.5f;
	}
}