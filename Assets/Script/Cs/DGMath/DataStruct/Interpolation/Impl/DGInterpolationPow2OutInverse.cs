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
public class DGInterpolationPow2OutInverse : DGInterpolation
{
	public override DGFixedPoint Apply(DGFixedPoint a)
	{
		if (a < DGMath.Epsilon) return (DGFixedPoint)0;
		if (a > (DGFixedPoint)1) return (DGFixedPoint)1;
		return (DGFixedPoint)1 - DGMath.Sqrt(-(a - (DGFixedPoint)1));
	}

}
