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
public class DGInterpolationPowIn : DGInterpolationPow
{
	public DGInterpolationPowIn(FP power):base(power)
	{
	}

	public override FP Apply(FP a)
	{
		return DGMath.Pow(a, power);
	}

}
