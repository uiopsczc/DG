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
//libgdx
public class DGInterpolationExpIn : DGInterpolationExp
{


	public DGInterpolationExpIn(DGFixedPoint value, DGFixedPoint power):base(value, power)
	{
	}

	public override DGFixedPoint Apply(DGFixedPoint a)
	{
		return (DGMath.Pow(value, power * (a - (DGFixedPoint)1)) - min) * scale;
	}

}
