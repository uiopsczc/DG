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
    public class FPInterpolationPowOut : FPInterpolationPow
    {
        public FPInterpolationPowOut(FP power) : base(power)
        {
        }

        public override FP Apply(FP a)
        {
            return FPMath.Pow(a - 1, power) * (power % 2 == 0 ? (-1) : 1) + 1;
        }
    }
}