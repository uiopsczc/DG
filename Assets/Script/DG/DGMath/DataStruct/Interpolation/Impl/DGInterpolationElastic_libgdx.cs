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
	public class DGInterpolationElastic : DGInterpolation
	{

		protected DGFixedPoint value, power, scale, bounces;

		public DGInterpolationElastic(DGFixedPoint value, DGFixedPoint power, DGFixedPoint bounces, DGFixedPoint scale)
		{
			this.value = value;
			this.power = power;
			this.scale = scale;
			this.bounces = bounces * DGMath.PI * (bounces % (DGFixedPoint)2 == (DGFixedPoint)0 ? (DGFixedPoint)1 : (DGFixedPoint)(-1));
		}

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			if (a <= (DGFixedPoint)0.5f)
			{
				a *= (DGFixedPoint)2;
				return DGMath.Pow(value, power * (a - (DGFixedPoint)1)) * DGMath.Sin(a * bounces) * scale / (DGFixedPoint)2;
			}
			a = (DGFixedPoint)1 - a;
			a *= (DGFixedPoint)2;
			return (DGFixedPoint)1 - DGMath.Pow(value, power * (a - (DGFixedPoint)1)) * DGMath.Sin((a) * bounces) * scale / (DGFixedPoint)2;
		}

	}
}


