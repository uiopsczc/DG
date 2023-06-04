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
public class DGInterpolationBounceIn : DGInterpolationBounceOut
{
	public DGInterpolationBounceIn(FP[] widths, FP[] heights) : base(widths, heights)
	{
	}

	public DGInterpolationBounceIn(int bounces) : base(bounces)
	{
	}

	public override FP Apply(FP a)
	{
		return (FP)1 - base.Apply((FP)1 - a);
	}
}