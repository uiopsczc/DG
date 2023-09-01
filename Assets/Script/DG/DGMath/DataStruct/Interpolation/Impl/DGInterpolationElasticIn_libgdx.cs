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
	public class DGInterpolationElasticIn : DGInterpolationElastic
	{
		public DGInterpolationElasticIn(DGFixedPoint value, DGFixedPoint power, DGFixedPoint bounces, DGFixedPoint scale) : base(value, power, bounces, scale)
		{
		}

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			if (a >= (DGFixedPoint)0.99) return (DGFixedPoint)1;
			return DGMath.Pow(value, power * (a - (DGFixedPoint)1)) * DGMath.Sin(a * bounces) * scale;
		}

	}
}

