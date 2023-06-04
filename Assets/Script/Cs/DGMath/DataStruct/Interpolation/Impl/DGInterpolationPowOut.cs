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
public class DGInterpolationPowOut : DGInterpolationPow
{
	public DGInterpolationPowOut(FP power):base(power)
	{
	}

	public override FP Apply(FP a)
	{
		return DGMath.Pow(a - (FP)1, power) * (power % (FP)2 == (FP)0 ? (FP)(- 1) : (FP)1) + (FP)1;
	}

}
