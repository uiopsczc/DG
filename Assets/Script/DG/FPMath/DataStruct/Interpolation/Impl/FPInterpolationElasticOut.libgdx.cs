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
    public class FPInterpolationElasticOut : FPInterpolationElastic
    {
        public FPInterpolationElasticOut(FP value, FP power, FP bounces, FP scale) : base(value, power, bounces, scale)
        {
        }

        public override FP Apply(FP a)
        {
            if (a == 0) return 0;
            a = 1 - a;
            return (1 - FPMath.Pow(value, power * (a - 1)) * FPMath.Sin(a * bounces) * scale);
        }
    }
}