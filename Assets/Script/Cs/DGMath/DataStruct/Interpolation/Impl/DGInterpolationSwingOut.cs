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
public class DGInterpolationSwingOut : DGInterpolationSwing
{

	public DGInterpolationSwingOut(FP scale):base(scale)
	{
		this.scale = scale;
	}

	public override FP Apply(FP a)
	{
		a = a - (FP)1;
		return a * a * ((scale + (FP)1) * a + scale) + (FP)1;
	}

}
