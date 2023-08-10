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
//libgdx
public class DGInterpolationPow : DGInterpolation
{
	protected  DGFixedPoint power;

	public DGInterpolationPow(DGFixedPoint power)
	{
		this.power = power;
	}

	public override DGFixedPoint Apply(DGFixedPoint a)
	{
		if (a <= (DGFixedPoint)0.5f) return DGMath.Pow(a * (DGFixedPoint)2, power) / (DGFixedPoint)2;
		return DGMath.Pow((a - (DGFixedPoint)1) * (DGFixedPoint)2, power) / (power % (DGFixedPoint)2 == (DGFixedPoint)0 ? (DGFixedPoint)(- 2) : (DGFixedPoint)2) + (DGFixedPoint)1;
	}

}
