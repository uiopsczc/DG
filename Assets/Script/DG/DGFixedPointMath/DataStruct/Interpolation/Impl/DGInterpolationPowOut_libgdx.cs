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
	public class DGInterpolationPowOut : DGInterpolationPow
	{
		public DGInterpolationPowOut(DGFixedPoint power) : base(power)
		{
		}

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			return DGFixedPointMath.Pow(a - (DGFixedPoint)1, power) * (power % (DGFixedPoint)2 == (DGFixedPoint)0 ? (DGFixedPoint)(-1) : (DGFixedPoint)1) + (DGFixedPoint)1;
		}

	}
}

