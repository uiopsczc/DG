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
public class DGInterpolationPow2OutInverse : DGInterpolation
{
	public override FP Apply(FP a)
	{
		if (a < DGMath.Epsilon) return (FP)0;
		if (a > (FP)1) return (FP)1;
		return (FP)1 - DGMath.Sqrt(-(a - (FP)1));
	}

}
