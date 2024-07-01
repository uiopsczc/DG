/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/8/16
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/

namespace DG
{
	public partial class DGCumulativeValue<T>
	{
		public T value;
		public DGFixedPoint frequency;
		public DGFixedPoint interval;

		public DGCumulativeValue(T value, DGFixedPoint frequency, DGFixedPoint interval)
		{
			this.value = value;
			this.frequency = frequency;
			this.interval = interval;
		}
	}
}
