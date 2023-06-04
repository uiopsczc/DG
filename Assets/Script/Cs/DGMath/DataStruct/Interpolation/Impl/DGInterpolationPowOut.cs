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
using FP = DGFixedPoint;
//libgdx
public class DGInterpolationPowOut : DGInterpolationPow
{
	public DGInterpolationPowOut(FP power):base(power)
	{
	}

	public override FP Apply(FP a)
	{
		return DGMath.Pow(a - (FP)1, power) * (power % (FP)2 == (FP)0 ? (FP)(- 1) : (FP)1) + (FP)1;
	}

}
