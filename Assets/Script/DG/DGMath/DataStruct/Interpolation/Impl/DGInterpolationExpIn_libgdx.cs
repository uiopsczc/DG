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
	public class DGInterpolationExpIn : DGInterpolationExp
	{


		public DGInterpolationExpIn(DGFixedPoint value, DGFixedPoint power) : base(value, power)
		{
		}

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			return (DGMath.Pow(value, power * (a - (DGFixedPoint)1)) - min) * scale;
		}

	}
}

