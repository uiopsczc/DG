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
    public class FPInterpolationBounceIn : FPInterpolationBounceOut
    {
        public FPInterpolationBounceIn(FP[] widths, FP[] heights) : base(widths, heights)
        {
        }

        public FPInterpolationBounceIn(int bounces) : base(bounces)
        {
        }

        public override FP Apply(FP a)
        {
            return 1 - base.Apply(1 - a);
        }
    }
}