/*************************************************************************************
 * 描    述:  dll内部使用
 * 创 建 者:  czq
 * 创建时间:  2023/5/8
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/

using System;

internal static class DGFixedPointConstInternal
{
	internal const long MAX_VALUE = long.MaxValue;
	internal const long MIN_VALUE = long.MinValue;
	internal const int NUM_BIT_COUNT = 64;
	internal const int MOVE_BIT_COUNT = 32;
	internal const long PRECISION = 1L;
	internal const long SCALED_EPSILON = (long)(1.17549435E-38f * SCALED_ONE);//UnityEngine.Mathf.Epsilon

	internal const long SCALED_ONE = 1L << MOVE_BIT_COUNT;
	internal const long SCALED_NEGATIVE_ONE = -1L << MOVE_BIT_COUNT;
	
	internal const ulong SCALED_HALF_ONE = 0x80000000;
	internal const ulong SCALED_HALF_QUARTER = 0x40000000;
	internal const long SCALED_PI = (long) (Math.PI * SCALED_ONE);
	internal const long SCALED_TWO_PI = (long)(Math.PI * (1L << (MOVE_BIT_COUNT + 1)));
	internal const long SCALED_HALF_PI = (long) (Math.PI * (1L << (MOVE_BIT_COUNT - 1)));
	internal const long SCALED_QUARTER_PI = (long)(Math.PI * (1L << (MOVE_BIT_COUNT - 2)));
	public static readonly long SCALED_E = (long)(Math.E * SCALED_ONE);
	public static readonly long SCALED_DEG2RAD = (long)(0.01745329f * SCALED_ONE);//Mathf.Deg2Rad == PI * 2F / 360F
	public static readonly long SCALED_RAD2DEG = (long)(57.29578f*SCALED_ONE);//Mathf.Rad2Deg == 1F / Deg2Rad


	internal const long SCALED_LARGE_PI = 7244019458077122842;//跟这个差不多(long)(Math.PI * (1L << (NUM_BIT_COUNT - MOVE_BIT_COUNT - 3 + MOVE_BIT_COUNT))) == 7244019458077122560; 但是还是最后三位有区别，不然有误差
	internal const long SCALED_LN2 = 0xB17217F7; //(long)(Math.Log(2) * SCALED_ONE)   Math.Log(2)是以E为底的2的对数
	internal const long SCALED_LOG2MAX = 0x1F00000000;
	internal const long SCALED_LOG2MIN = -0x2000000000;

	internal const ulong SCALED_INTEGRAL_PART_MASK = 0xFFFFFFFF00000000; //整数部分的mask
	internal const uint SCALED_FRACTIONAL_PART_MASK = 0x00000000FFFFFFFF; //小数部分的mask
	internal const uint SCALED_ROUND_FRACTIONAL_PART_MASK = 0x80000000; //小数四舍五入部分的mask
	internal const int INTEGRAL_PART_ALL_ZERO = 0; //整数部分全0
	internal const int INTEGRAL_PART_ALL_ONE = -1; //整数部分全1
	internal const ulong COUNT_LEADING_ZERO_ROUGH_Mask = 0xF000000000000000; //在统计前置0个数时粗略的mask，即先4个4个的检测
	internal const ulong COUNT_LEADING_ZERO_MASK = 0x8000000000000000; //在统计前置0个数时精细的mask,即1个1个的检测
	internal const ulong ALL_ONE = 0xFFFFFFFFFFFFFFFF; //

	internal const int LUT_SIZE = (int) (SCALED_HALF_PI >> 15);

	
}