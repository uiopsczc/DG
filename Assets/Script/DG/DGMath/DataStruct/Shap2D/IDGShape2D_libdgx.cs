/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/5/21
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/

namespace DG
{
	public interface IDGShape2D
	{
		/** Returns whether the given point is contained within the shape. */
		bool contains(DGVector2 point);

		/** Returns whether a point with the given coordinates is contained within the shape. */
		bool contains(DGFixedPoint x, DGFixedPoint y);
	}
}
