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
	public class FPInterpolationExpOut : FPInterpolationExp
	{
		public FPInterpolationExpOut(FP value, FP power) : base(value, power)
		{
		}

		public override FP Apply(FP a)
		{
			return 1 - (FPMath.Pow(value, -power * a) - min) * scale;
		}

	}
}

