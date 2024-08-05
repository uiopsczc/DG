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
    public class FPInterpolationExp : FPInterpolation
    {
        protected FP value, power, min, scale;

        public FPInterpolationExp(FP value, FP power)
        {
            this.value = value;
            this.power = power;
            min = FPMath.Pow(value, -power);
            scale = 1 / (1 - min);
        }

        public override FP Apply(FP a)
        {
            if (a <= 0.5f) return (FPMath.Pow(value, power * (a * 2 - 1)) - min) * scale / 2;
            return (2 - (FPMath.Pow(value, -power * (a * 2 - 1)) - min) * scale) / 2;
        }
    }
}