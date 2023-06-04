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
public class DGInterpolationExp : DGInterpolation
{

	protected FP value, power, min, scale;

	public DGInterpolationExp(FP value, FP power)
	{
		this.value = value;
		this.power = power;
		min = DGMath.Pow(value, -power);
		scale = (FP)1 / ((FP)1 - min);
	}

	public override FP Apply(FP a)
	{
		if (a <= (FP)0.5f) return (DGMath.Pow(value, power * (a * (FP)2 - (FP)1)) - min) * scale / (FP)2;
		return ((FP)2 - (DGMath.Pow(value, -power * (a * (FP)2 - (FP)1)) - min) * scale) / (FP)2;
	}

}
