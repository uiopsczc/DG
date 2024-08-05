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
    public class FPInterpolationElastic : FPInterpolation
    {
        protected FP value, power, scale, bounces;

        public FPInterpolationElastic(FP value, FP power, FP bounces, FP scale)
        {
            this.value = value;
            this.power = power;
            this.scale = scale;
            this.bounces = bounces * FPMath.PI * (bounces % 2 == 0 ? 1 : (-1));
        }

        public override FP Apply(FP a)
        {
            if (a <= 0.5f)
            {
                a *= 2;
                return FPMath.Pow(value, power * (a - 1)) * FPMath.Sin(a * bounces) * scale / 2;
            }

            a = 1 - a;
            a *= 2;
            return 1 - FPMath.Pow(value, power * (a - 1)) * FPMath.Sin((a) * bounces) * scale / 2;
        }
    }
}