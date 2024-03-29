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
	public class DGInterpolationSwingOut : DGInterpolationSwing
	{

		public DGInterpolationSwingOut(DGFixedPoint scale) : base(scale)
		{
			this.scale = scale;
		}

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			a = a - (DGFixedPoint)1;
			return a * a * ((scale + (DGFixedPoint)1) * a + scale) + (DGFixedPoint)1;
		}

	}
}

