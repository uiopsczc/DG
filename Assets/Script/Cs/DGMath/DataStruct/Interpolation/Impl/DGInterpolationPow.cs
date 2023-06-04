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
public class DGInterpolationPow : DGInterpolation
{
	protected  FP power;

	public DGInterpolationPow(FP power)
	{
		this.power = power;
	}

	public override FP Apply(FP a)
	{
		if (a <= (FP)0.5f) return DGMath.Pow(a * (FP)2, power) / (FP)2;
		return DGMath.Pow((a - (FP)1) * (FP)2, power) / (power % (FP)2 == (FP)0 ? (FP)(- 2) : (FP)2) + (FP)1;
	}

}
