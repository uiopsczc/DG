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
public class DGInterpolationCircle : DGInterpolation
{

	public override FP Apply(FP a)
	{
		if (a <= (FP)0.5f)
		{
			a *= (FP)2;
			return ((FP)1 - DGMath.Sqrt((FP)1 - a * a)) / (FP)2;
		}
		a= a -(FP)1;
		a *= (FP)2;
		return (DGMath.Sqrt((FP)1 - a * a) + (FP)1) / (FP)2;
	}

}
