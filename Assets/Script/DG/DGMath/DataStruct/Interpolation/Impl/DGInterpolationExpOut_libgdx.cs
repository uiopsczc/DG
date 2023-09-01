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
	public class DGInterpolationExpOut : DGInterpolationExp
	{
		public DGInterpolationExpOut(DGFixedPoint value, DGFixedPoint power) : base(value, power)
		{
		}

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			return (DGFixedPoint)1 - (DGMath.Pow(value, -power * a) - min) * scale;
		}

	}
}

