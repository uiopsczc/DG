

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
	public class DGInterpolationElasticOut : DGInterpolationElastic
	{
		public DGInterpolationElasticOut(DGFixedPoint value, DGFixedPoint power, DGFixedPoint bounces, DGFixedPoint scale) : base(value, power, bounces, scale)
		{
		}

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			if (a == (DGFixedPoint)0) return (DGFixedPoint)0;
			a = (DGFixedPoint)1 - a;
			return ((DGFixedPoint)1 - DGMath.Pow(value, power * (a - (DGFixedPoint)1)) * DGMath.Sin(a * bounces) * scale);
		}

	}
}

