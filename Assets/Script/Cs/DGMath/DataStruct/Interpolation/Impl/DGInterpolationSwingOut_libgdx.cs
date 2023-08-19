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
public class DGInterpolationSwingOut : DGInterpolationSwing
{

	public DGInterpolationSwingOut(DGFixedPoint scale):base(scale)
	{
		this.scale = scale;
	}

	public override DGFixedPoint Apply(DGFixedPoint a)
	{
		a = a - (DGFixedPoint)1;
		return a * a * ((scale + (DGFixedPoint)1) * a + scale) + (DGFixedPoint)1;
	}

}
