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
public class DGInterpolationPowIn : DGInterpolationPow
{
	public DGInterpolationPowIn(DGFixedPoint power):base(power)
	{
	}

	public override DGFixedPoint Apply(DGFixedPoint a)
	{
		return DGMath.Pow(a, power);
	}

}
