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
    /// <summary>
    /// libgdx
    /// Takes a linear value in the range of 0-1 and outputs a (usually) non-linear, interpolated value.
    /// @author Nathan Sweet
    /// </summary>
    public abstract class FPInterpolation
    {
        /** @param a Alpha value between 0 and 1. */
        public abstract FP Apply(FP a);

        /** @param a Alpha value between 0 and 1. */
        public FP Apply(FP start, FP end, FP a)
        {
            return start + (end - start) * Apply(a);
        }

        public static FPInterpolationLinear Linear = new();

        /** Aka "smoothstep". */
        public static FPInterpolationSmooth Smooth = new();

        public static FPInterpolationSmooth2 Smooth2 = new();

        /** By Ken Perlin. */
        public static FPInterpolationSmoother Smoother = new();

        public static FPInterpolationSmoother Fade = Smoother;

        public static FPInterpolationPow Pow2 = new(2);

        /** Slow, then fast. */
        public static FPInterpolationPowIn Pow2In = new(2);

        public static FPInterpolationPowIn SlowFast = Pow2In;

        /** Fast, then slow. */
        public static FPInterpolationPowOut Pow2Out = new(2);

        public static FPInterpolationPowOut FastSlow = Pow2Out;
        public static FPInterpolationPow2InInverse Pow2InInverse = new();
        public static FPInterpolationPow2OutInverse Pow2OutInverse = new();

        public static FPInterpolationPow Pow3 = new(3);
        public static FPInterpolationPowIn Pow3In = new(3);
        public static FPInterpolationPowOut Pow3Out = new(3);
        public static FPInterpolationPow3InInverse Pow3InInverse = new();
        public static FPInterpolationPow3OutInverse pow3OutInverse = new();
        public static FPInterpolationPow Pow4 = new(4);
        public static FPInterpolationPowIn Pow4In = new(4);
        public static FPInterpolationPowOut Pow4Out = new(4);
        public static FPInterpolationPow Pow5 = new(5);
        public static FPInterpolationPowIn Pow5In = new(5);
        public static FPInterpolationPowOut Pow5Out = new(5);
        public static FPInterpolationSine Sine = new();
        public static FPInterpolationSineIn SineIn = new();
        public static FPInterpolationSineOut SineOut = new();
        public static FPInterpolationExp Exp10 = new(2, 10);
        public static FPInterpolationExpIn Exp10In = new(2, 10);
        public static FPInterpolationExpOut Exp10Out = new(2, 10);
        public static FPInterpolationExp Exp5 = new(2, 5);
        public static FPInterpolationExpIn Exp5In = new(2, 5);
        public static FPInterpolationExpOut Exp5Out = new(2, 5);
        public static FPInterpolationCircle Circle = new();
        public static FPInterpolationCircleIn CircleIn = new();
        public static FPInterpolationCircleOut CircleOut = new();
        public static FPInterpolationElastic Elastic = new(2, 10, 7, 1);
        public static FPInterpolationElasticIn ElasticIn = new(2, 10, 6, 1);
        public static FPInterpolationElasticOut ElasticOut = new(2, 10, 7, 1);
        public static FPInterpolationSwing Swing = new(1.5f);
        public static FPInterpolationSwingIn SwingIn = new(2f);
        public static FPInterpolationSwingOut SwingOut = new(2f);
        public static FPInterpolationBounce Bounce = new(4);
        public static FPInterpolationBounceIn BounceIn = new(4);
        public static FPInterpolationBounceOut BounceOut = new(4);
    }
}