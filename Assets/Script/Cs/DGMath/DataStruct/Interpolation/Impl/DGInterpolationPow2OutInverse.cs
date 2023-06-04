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
public class DGInterpolationPow2OutInverse : DGInterpolation
{
	public override FP Apply(FP a)
	{
		if (a < DGMath.Epsilon) return (FP)0;
		if (a > (FP)1) return (FP)1;
		return (FP)1 - DGMath.Sqrt(-(a - (FP)1));
	}

}
