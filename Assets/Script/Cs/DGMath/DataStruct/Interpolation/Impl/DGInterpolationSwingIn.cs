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
public class DGInterpolationSwingIn : DGInterpolationSwing
{

	public DGInterpolationSwingIn(FP scale):base(scale)
	{
		this.scale = scale;
	}

	public override FP Apply(FP a)
	{
		return a * a * ((scale + (FP)1) * a - scale);
	}

}
