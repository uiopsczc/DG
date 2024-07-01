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
namespace DG
{
	public class DGInterpolationSwingIn : DGInterpolationSwing
	{

		public DGInterpolationSwingIn(DGFixedPoint scale) : base(scale)
		{
			this.scale = scale;
		}

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			return a * a * ((scale + (DGFixedPoint)1) * a - scale);
		}

	}
}

