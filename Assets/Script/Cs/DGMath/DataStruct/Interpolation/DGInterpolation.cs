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

using FP = DGFixedPoint;

//libgdx
/** Takes a linear value in the range of 0-1 and outputs a (usually) non-linear, interpolated value.
 * @author Nathan Sweet */
public abstract class DGInterpolation
{
	/** @param a Alpha value between 0 and 1. */
	public abstract FP Apply(FP a);

	/** @param a Alpha value between 0 and 1. */
	public FP Apply(FP start, FP end, FP a)
	{
		return start + (end - start) * Apply(a);
	}

	public static DGInterpolationLinear Linear = new DGInterpolationLinear();

	/** Aka "smoothstep". */
	public static DGInterpolationSmooth Smooth = new DGInterpolationSmooth();

	public static DGInterpolationSmooth2 Smooth2 = new DGInterpolationSmooth2();

	/** By Ken Perlin. */
	public static DGInterpolationSmoother Smoother = new DGInterpolationSmoother();
	public static DGInterpolationSmoother Fade = Smoother;

	public static DGInterpolationPow Pow2 = new DGInterpolationPow((FP) 2);

	/** Slow, then fast. */
	public static DGInterpolationPowIn Pow2In = new DGInterpolationPowIn((FP) 2);

	public static DGInterpolationPowIn SlowFast = Pow2In;

	/** Fast, then slow. */
	public static DGInterpolationPowOut Pow2Out = new DGInterpolationPowOut((FP) 2);
	public static DGInterpolationPowOut FastSlow = Pow2Out;
	public static DGInterpolationPow2InInverse Pow2InInverse = new DGInterpolationPow2InInverse();
	public static DGInterpolationPow2OutInverse Pow2OutInverse = new DGInterpolationPow2OutInverse();

	public static DGInterpolationPow Pow3 = new DGInterpolationPow((FP) 3);
	public static DGInterpolationPowIn Pow3In = new DGInterpolationPowIn((FP) 3);
	public static DGInterpolationPowOut Pow3Out = new DGInterpolationPowOut((FP) 3);
	public static DGInterpolationPow3InInverse Pow3InInverse = new DGInterpolationPow3InInverse();
	public static DGInterpolationPow3OutInverse pow3OutInverse = new DGInterpolationPow3OutInverse();
	public static DGInterpolationPow Pow4 = new DGInterpolationPow((FP) 4);
	public static DGInterpolationPowIn Pow4In = new DGInterpolationPowIn((FP) 4);
	public static DGInterpolationPowOut Pow4Out = new DGInterpolationPowOut((FP) 4);
	public static DGInterpolationPow Pow5 = new DGInterpolationPow((FP) 5);
	public static DGInterpolationPowIn Pow5In = new DGInterpolationPowIn((FP) 5);
	public static DGInterpolationPowOut Pow5Out = new DGInterpolationPowOut((FP) 5);
	public static DGInterpolationSine Sine = new DGInterpolationSine();
	public static DGInterpolationSineIn SineIn = new DGInterpolationSineIn();
	public static DGInterpolationSineOut SineOut = new DGInterpolationSineOut();
	public static DGInterpolationExp Exp10 = new DGInterpolationExp((FP) 2, (FP) 10);
	public static DGInterpolationExpIn Exp10In = new DGInterpolationExpIn((FP) 2, (FP) 10);
	public static DGInterpolationExpOut Exp10Out = new DGInterpolationExpOut((FP) 2, (FP) 10);
	public static DGInterpolationExp Exp5 = new DGInterpolationExp((FP) 2, (FP) 5);
	public static DGInterpolationExpIn Exp5In = new DGInterpolationExpIn((FP) 2, (FP) 5);
	public static DGInterpolationExpOut Exp5Out = new DGInterpolationExpOut((FP) 2, (FP) 5);
	public static DGInterpolationCircle Circle = new DGInterpolationCircle();
	public static DGInterpolationCircleIn CircleIn = new DGInterpolationCircleIn();
	public static DGInterpolationCircleOut CircleOut = new DGInterpolationCircleOut();
	public static DGInterpolationElastic Elastic = new DGInterpolationElastic((FP) 2, (FP) 10, (FP) 7, (FP) 1);
	public static DGInterpolationElasticIn ElasticIn = new DGInterpolationElasticIn((FP) 2, (FP) 10, (FP) 6, (FP) 1);
	public static DGInterpolationElasticOut ElasticOut = new DGInterpolationElasticOut((FP) 2, (FP) 10, (FP) 7, (FP) 1);
	public static DGInterpolationSwing Swing = new DGInterpolationSwing((FP) 1.5f);
	public static DGInterpolationSwingIn SwingIn = new DGInterpolationSwingIn((FP) 2f);
	public static DGInterpolationSwingOut SwingOut = new DGInterpolationSwingOut((FP) 2f);
	public static DGInterpolationBounce Bounce = new DGInterpolationBounce(4);
	public static DGInterpolationBounceIn BounceIn = new DGInterpolationBounceIn(4);
	public static DGInterpolationBounceOut BounceOut = new DGInterpolationBounceOut(4);
}