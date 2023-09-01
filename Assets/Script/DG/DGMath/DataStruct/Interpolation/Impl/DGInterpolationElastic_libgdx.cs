/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/5/12
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
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


