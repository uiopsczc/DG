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
public class DGInterpolationCircle : DGInterpolation
{

	public override DGFixedPoint Apply(DGFixedPoint a)
	{
		if (a <= (DGFixedPoint)0.5f)
		{
			a *= (DGFixedPoint)2;
			return ((DGFixedPoint)1 - DGMath.Sqrt((DGFixedPoint)1 - a * a)) / (DGFixedPoint)2;
		}
		a= a -(DGFixedPoint)1;
		a *= (DGFixedPoint)2;
		return (DGMath.Sqrt((DGFixedPoint)1 - a * a) + (DGFixedPoint)1) / (DGFixedPoint)2;
	}

}
