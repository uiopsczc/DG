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
public class DGInterpolationPowOut : DGInterpolationPow
{
	public DGInterpolationPowOut(DGFixedPoint power):base(power)
	{
	}

	public override DGFixedPoint Apply(DGFixedPoint a)
	{
		return DGMath.Pow(a - (DGFixedPoint)1, power) * (power % (DGFixedPoint)2 == (DGFixedPoint)0 ? (DGFixedPoint)(- 1) : (DGFixedPoint)1) + (DGFixedPoint)1;
	}

}