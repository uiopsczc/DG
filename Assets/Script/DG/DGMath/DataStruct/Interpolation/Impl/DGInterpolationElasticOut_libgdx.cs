

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

