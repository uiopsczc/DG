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
	public class DGInterpolationPow2InInverse : DGInterpolation
	{
		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			if (a < DGFixedPointMath.Epsilon) return (DGFixedPoint)0;
			return DGFixedPointMath.Sqrt(a);
		}

	}
}

