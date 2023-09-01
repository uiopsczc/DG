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

	public class DGInterpolationSineIn : DGInterpolation
	{

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			return (DGFixedPoint)1 - DGMath.Cos(a * DGMath.HalfPi);
		}

	}
}

