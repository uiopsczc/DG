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
	public class DGInterpolationCircleIn : DGInterpolation
	{

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			return (DGFixedPoint)1 - DGFixedPointMath.Sqrt((DGFixedPoint)1 - a * a);
		}

	}

}
