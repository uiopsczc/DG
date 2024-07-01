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
	public class DGInterpolationSwing : DGInterpolation
	{

		protected DGFixedPoint scale;

		public DGInterpolationSwing(DGFixedPoint scale)
		{
			this.scale = scale;
		}

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			if (a <= (DGFixedPoint)0.5f)
			{
				a *= (DGFixedPoint)2;
				return a * a * ((scale + (DGFixedPoint)1) * a - scale) / (DGFixedPoint)2;
			}
			a = a - (DGFixedPoint)1;
			a *= (DGFixedPoint)2;
			return a * a * ((scale + (DGFixedPoint)1) * a + scale) / (DGFixedPoint)2 + (DGFixedPoint)1;
		}

	}
}


