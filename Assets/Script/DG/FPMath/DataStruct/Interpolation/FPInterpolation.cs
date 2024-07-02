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

		public static FPInterpolationLinear Linear = new FPInterpolationLinear();

		/** Aka "smoothstep". */
		public static FPInterpolationSmooth Smooth = new FPInterpolationSmooth();

		public static FPInterpolationSmooth2 Smooth2 = new FPInterpolationSmooth2();

		/** By Ken Perlin. */
		public static FPInterpolationSmoother Smoother = new FPInterpolationSmoother();
		public static FPInterpolationSmoother Fade = Smoother;

		public static FPInterpolationPow Pow2 = new FPInterpolationPow(2);

		/** Slow, then fast. */
		public static FPInterpolationPowIn Pow2In = new FPInterpolationPowIn(2);

		public static FPInterpolationPowIn SlowFast = Pow2In;

		/** Fast, then slow. */
		public static FPInterpolationPowOut Pow2Out = new FPInterpolationPowOut(2);
		public static FPInterpolationPowOut FastSlow = Pow2Out;
		public static FPInterpolationPow2InInverse Pow2InInverse = new FPInterpolationPow2InInverse();
		public static FPInterpolationPow2OutInverse Pow2OutInverse = new FPInterpolationPow2OutInverse();

		public static FPInterpolationPow Pow3 = new FPInterpolationPow(3);
		public static FPInterpolationPowIn Pow3In = new FPInterpolationPowIn(3);
		public static FPInterpolationPowOut Pow3Out = new FPInterpolationPowOut(3);
		public static FPInterpolationPow3InInverse Pow3InInverse = new FPInterpolationPow3InInverse();
		public static FPInterpolationPow3OutInverse pow3OutInverse = new FPInterpolationPow3OutInverse();
		public static FPInterpolationPow Pow4 = new FPInterpolationPow(4);
		public static FPInterpolationPowIn Pow4In = new FPInterpolationPowIn(4);
		public static FPInterpolationPowOut Pow4Out = new FPInterpolationPowOut(4);
		public static FPInterpolationPow Pow5 = new FPInterpolationPow(5);
		public static FPInterpolationPowIn Pow5In = new FPInterpolationPowIn(5);
		public static FPInterpolationPowOut Pow5Out = new FPInterpolationPowOut(5);
		public static FPInterpolationSine Sine = new FPInterpolationSine();
		public static FPInterpolationSineIn SineIn = new FPInterpolationSineIn();
		public static FPInterpolationSineOut SineOut = new FPInterpolationSineOut();
		public static FPInterpolationExp Exp10 = new FPInterpolationExp(2, 10);
		public static FPInterpolationExpIn Exp10In = new FPInterpolationExpIn(2, 10);
		public static FPInterpolationExpOut Exp10Out = new FPInterpolationExpOut(2, 10);
		public static FPInterpolationExp Exp5 = new FPInterpolationExp(2, 5);
		public static FPInterpolationExpIn Exp5In = new FPInterpolationExpIn(2, 5);
		public static FPInterpolationExpOut Exp5Out = new FPInterpolationExpOut(2, 5);
		public static FPInterpolationCircle Circle = new FPInterpolationCircle();
		public static FPInterpolationCircleIn CircleIn = new FPInterpolationCircleIn();
		public static FPInterpolationCircleOut CircleOut = new FPInterpolationCircleOut();
		public static FPInterpolationElastic Elastic = new FPInterpolationElastic(2, 10, 7, 1);
		public static FPInterpolationElasticIn ElasticIn = new FPInterpolationElasticIn(2, 10, 6, 1);
		public static FPInterpolationElasticOut ElasticOut = new FPInterpolationElasticOut(2, 10, 7, 1);
		public static FPInterpolationSwing Swing = new FPInterpolationSwing(1.5f);
		public static FPInterpolationSwingIn SwingIn = new FPInterpolationSwingIn(2f);
		public static FPInterpolationSwingOut SwingOut = new FPInterpolationSwingOut(2f);
		public static FPInterpolationBounce Bounce = new FPInterpolationBounce(4);
		public static FPInterpolationBounceIn BounceIn = new FPInterpolationBounceIn(4);
		public static FPInterpolationBounceOut BounceOut = new FPInterpolationBounceOut(4);
	}
}
