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
public class DGInterpolationElastic : DGInterpolation
{

	protected FP value, power, scale, bounces;

	public DGInterpolationElastic(FP value, FP power, FP bounces, FP scale)
	{
		this.value = value;
		this.power = power;
		this.scale = scale;
		this.bounces = bounces * DGMath.PI * (bounces % (FP)2 == (FP)0 ? (FP)1 : (FP)(-1));
	}

	public override FP Apply(FP a)
	{
		if (a <= (FP)0.5f)
		{
			a *= (FP)2;
			return DGMath.Pow(value, power * (a - (FP)1)) * DGMath.Sin(a * bounces) * scale / (FP)2;
		}
		a = (FP)1 - a;
		a *= (FP)2;
		return (FP)1 - DGMath.Pow(value, power * (a - (FP)1)) * DGMath.Sin((a) * bounces) * scale / (FP)2;
	}

}
