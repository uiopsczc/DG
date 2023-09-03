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
	public class DGInterpolationSmoother : DGInterpolation
	{
		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			return a * a * a * (a * (a * (DGFixedPoint)6 - (DGFixedPoint)15) + (DGFixedPoint)10);
		}

	}
}
