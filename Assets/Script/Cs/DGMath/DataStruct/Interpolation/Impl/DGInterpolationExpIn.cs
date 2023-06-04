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
public class DGInterpolationExpIn : DGInterpolationExp
{


	public DGInterpolationExpIn(FP value, FP power):base(value, power)
	{
	}

	public override FP Apply(FP a)
	{
		return (DGMath.Pow(value, power * (a - (FP)1)) - min) * scale;
	}

}
