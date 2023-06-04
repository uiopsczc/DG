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
public class DGInterpolationExpOut : DGInterpolationExp
{
	public DGInterpolationExpOut(FP value, FP power):base(value, power)
	{
	}

	public override FP Apply(FP a)
	{
		return (FP)1 - (DGMath.Pow(value, -power * a) - min) * scale;
	}

}
