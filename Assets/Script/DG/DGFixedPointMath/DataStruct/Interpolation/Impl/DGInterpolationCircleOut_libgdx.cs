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
	public class DGInterpolationCircleOut : DGInterpolation
	{

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			a = a - (DGFixedPoint)1;
			return DGFixedPointMath.Sqrt((DGFixedPoint)1 - a * a);
		}

	}
}

