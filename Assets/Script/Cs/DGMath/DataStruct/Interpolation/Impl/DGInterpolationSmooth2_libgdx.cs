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
public class DGInterpolationSmooth2 : DGInterpolation
{
	public override DGFixedPoint Apply(DGFixedPoint a)
	{
		a = a * a * ((DGFixedPoint)3 - (DGFixedPoint)2 * a);
		return a * a * ((DGFixedPoint)3 - (DGFixedPoint)2 * a);
	}

}
