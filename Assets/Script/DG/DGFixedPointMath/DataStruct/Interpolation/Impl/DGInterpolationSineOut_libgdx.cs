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

	public class DGInterpolationSineOut : DGInterpolation
	{

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			return DGFixedPointMath.Sin(a * DGFixedPointMath.HalfPi);
		}

	}
}

