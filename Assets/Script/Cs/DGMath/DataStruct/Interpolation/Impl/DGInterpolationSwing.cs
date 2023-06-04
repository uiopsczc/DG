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
public class DGInterpolationSwing : DGInterpolation
{

	protected FP scale;

	public DGInterpolationSwing(FP scale)
	{
		this.scale = scale;
	}

	public override FP Apply(FP a)
	{
		if (a <= (FP)0.5f)
		{
			a *= (FP)2;
			return a * a * ((scale + (FP)1) * a - scale) / (FP)2;
		}
		a = a - (FP)1;
		a *= (FP)2;
		return a * a * ((scale + (FP)1) * a + scale) / (FP)2 + (FP)1;
	}

}
