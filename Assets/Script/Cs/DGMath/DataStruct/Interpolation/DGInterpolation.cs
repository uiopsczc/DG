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


//libgdx
/** Takes a linear value in the range of 0-1 and outputs a (usually) non-linear, interpolated value.
 * @author Nathan Sweet */
public abstract class DGInterpolation
{
	/** @param a Alpha value between 0 and 1. */
	public abstract DGFixedPoint Apply(DGFixedPoint a);

	/** @param a Alpha value between 0 and 1. */
	public DGFixedPoint Apply(DGFixedPoint start, DGFixedPoint end, DGFixedPoint a)
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

	public static DGInterpolationPow Pow2 = new DGInterpolationPow((DGFixedPoint) 2);

	/** Slow, then fast. */
	public static DGInterpolationPowIn Pow2In = new DGInterpolationPowIn((DGFixedPoint) 2);

	public static DGInterpolationPowIn SlowFast = Pow2In;

	/** Fast, then slow. */
	public static DGInterpolationPowOut Pow2Out = new DGInterpolationPowOut((DGFixedPoint) 2);
	public static DGInterpolationPowOut FastSlow = Pow2Out;
	public static DGInterpolationPow2InInverse Pow2InInverse = new DGInterpolationPow2InInverse();
	public static DGInterpolationPow2OutInverse Pow2OutInverse = new DGInterpolationPow2OutInverse();

	public static DGInterpolationPow Pow3 = new DGInterpolationPow((DGFixedPoint) 3);
	public static DGInterpolationPowIn Pow3In = new DGInterpolationPowIn((DGFixedPoint) 3);
	public static DGInterpolationPowOut Pow3Out = new DGInterpolationPowOut((DGFixedPoint) 3);
	public static DGInterpolationPow3InInverse Pow3InInverse = new DGInterpolationPow3InInverse();
	public static DGInterpolationPow3OutInverse pow3OutInverse = new DGInterpolationPow3OutInverse();
	public static DGInterpolationPow Pow4 = new DGInterpolationPow((DGFixedPoint) 4);
	public static DGInterpolationPowIn Pow4In = new DGInterpolationPowIn((DGFixedPoint) 4);
	public static DGInterpolationPowOut Pow4Out = new DGInterpolationPowOut((DGFixedPoint) 4);
	public static DGInterpolationPow Pow5 = new DGInterpolationPow((DGFixedPoint) 5);
	public static DGInterpolationPowIn Pow5In = new DGInterpolationPowIn((DGFixedPoint) 5);
	public static DGInterpolationPowOut Pow5Out = new DGInterpolationPowOut((DGFixedPoint) 5);
	public static DGInterpolationSine Sine = new DGInterpolationSine();
	public static DGInterpolationSineIn SineIn = new DGInterpolationSineIn();
	public static DGInterpolationSineOut SineOut = new DGInterpolationSineOut();
	public static DGInterpolationExp Exp10 = new DGInterpolationExp((DGFixedPoint) 2, (DGFixedPoint) 10);
	public static DGInterpolationExpIn Exp10In = new DGInterpolationExpIn((DGFixedPoint) 2, (DGFixedPoint) 10);
	public static DGInterpolationExpOut Exp10Out = new DGInterpolationExpOut((DGFixedPoint) 2, (DGFixedPoint) 10);
	public static DGInterpolationExp Exp5 = new DGInterpolationExp((DGFixedPoint) 2, (DGFixedPoint) 5);
	public static DGInterpolationExpIn Exp5In = new DGInterpolationExpIn((DGFixedPoint) 2, (DGFixedPoint) 5);
	public static DGInterpolationExpOut Exp5Out = new DGInterpolationExpOut((DGFixedPoint) 2, (DGFixedPoint) 5);
	public static DGInterpolationCircle Circle = new DGInterpolationCircle();
	public static DGInterpolationCircleIn CircleIn = new DGInterpolationCircleIn();
	public static DGInterpolationCircleOut CircleOut = new DGInterpolationCircleOut();
	public static DGInterpolationElastic Elastic = new DGInterpolationElastic((DGFixedPoint) 2, (DGFixedPoint) 10, (DGFixedPoint) 7, (DGFixedPoint) 1);
	public static DGInterpolationElasticIn ElasticIn = new DGInterpolationElasticIn((DGFixedPoint) 2, (DGFixedPoint) 10, (DGFixedPoint) 6, (DGFixedPoint) 1);
	public static DGInterpolationElasticOut ElasticOut = new DGInterpolationElasticOut((DGFixedPoint) 2, (DGFixedPoint) 10, (DGFixedPoint) 7, (DGFixedPoint) 1);
	public static DGInterpolationSwing Swing = new DGInterpolationSwing((DGFixedPoint) 1.5f);
	public static DGInterpolationSwingIn SwingIn = new DGInterpolationSwingIn((DGFixedPoint) 2f);
	public static DGInterpolationSwingOut SwingOut = new DGInterpolationSwingOut((DGFixedPoint) 2f);
	public static DGInterpolationBounce Bounce = new DGInterpolationBounce(4);
	public static DGInterpolationBounceIn BounceIn = new DGInterpolationBounceIn(4);
	public static DGInterpolationBounceOut BounceOut = new DGInterpolationBounceOut(4);
}