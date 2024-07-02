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
	public class FPInterpolationPow : FPInterpolation
	{
		protected FP power;

		public FPInterpolationPow(FP power)
		{
			this.power = power;
		}

		public override FP Apply(FP a)
		{
			if (a <= 0.5f) return FPMath.Pow(a * 2, power) / 2;
			return FPMath.Pow((a - 1) * 2, power) / (power % 2 == 0 ? (-2) : 2) + 1;
		}

	}

}
