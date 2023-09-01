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
	public class DGInterpolationSwing : DGInterpolation
	{

		protected DGFixedPoint scale;

		public DGInterpolationSwing(DGFixedPoint scale)
		{
			this.scale = scale;
		}

		public override DGFixedPoint Apply(DGFixedPoint a)
		{
			if (a <= (DGFixedPoint)0.5f)
			{
				a *= (DGFixedPoint)2;
				return a * a * ((scale + (DGFixedPoint)1) * a - scale) / (DGFixedPoint)2;
			}
			a = a - (DGFixedPoint)1;
			a *= (DGFixedPoint)2;
			return a * a * ((scale + (DGFixedPoint)1) * a + scale) / (DGFixedPoint)2 + (DGFixedPoint)1;
		}

	}
}


