

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
public class DGInterpolationElasticOut : DGInterpolationElastic
{
	public DGInterpolationElasticOut(FP value, FP power, FP bounces, FP scale):base(value, power, bounces, scale)
	{
	}

	public override FP Apply(FP a)
	{
		if (a == (FP)0) return (FP)0;
		a = (FP)1 - a;
		return ((FP)1 - DGMath.Pow(value, power * (a - (FP)1)) * DGMath.Sin(a * bounces) * scale);
	}

}
