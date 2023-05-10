/*************************************************************************************
 * ��    ��:  dll�ڲ�ʹ��
 * �� �� ��:  czq
 * ����ʱ��:  2023/5/8
 * ======================================
 * ��ʷ���¼�¼
 * �汾:V          �޸�ʱ��:         �޸���:
 * �޸�����:
 * ======================================
*************************************************************************************/

using System;

internal static class DGFixedPointConstInternal
{
	internal const long MAX_VALUE = long.MaxValue;
	internal const long MIN_VALUE = long.MinValue;
	internal const int MOVE_BIT_COUNT = 32;
	internal const int NUM_BIT_COUNT = 64;
	internal const long PRECISION = 1L;

	internal const long SCALED_ONE = 1L << MOVE_BIT_COUNT;
	internal const ulong SCALED_HALF_ONE = 0x80000000;
	internal const long SCALED_PI = (long) (Math.PI * SCALED_ONE);
	internal const long SCALED_HALF_PI = (long) (Math.PI * SCALED_ONE / 2);
	internal const long SCALED_TWO_PI = (long) (Math.PI * SCALED_ONE * 2);
	// SCALED_LARGE_PI
	// This is (2^29)*PI, where 29 is the largest N such that (2^N)*PI < MaxValue.
	// The idea is that this number contains way more precision than PI_TIMES_2,
	// and (((x % (2^29*PI)) % (2^28*PI)) % ... (2^1*PI) = x % (2 * PI)
	// In practice this gives us an error of about 1,25e-9 in the worst case scenario (Sin(MaxValue))
	// Whereas simply doing x % PI_TIMES_2 is the 2e-3 range.
	internal const long SCALED_LARGE_PI = SCALED_PI * (1L << (NUM_BIT_COUNT - MOVE_BIT_COUNT - 3));
	internal const long SCALED_LN2 = 0xB17217F7; //(long)(Math.Log(2) * SCALED_ONE)
	internal const long SCALED_LOG2MAX = 0x1F00000000;
	internal const long SCALED_LOG2MIN = -0x2000000000;

	internal const ulong SCALED_INTEGRAL_PART_MASK = 0xFFFFFFFF00000000; //�������ֵ�mask
	internal const uint SCALED_FRACTIONAL_PART_MASK = 0x00000000FFFFFFFF; //С�����ֵ�mask
	internal const uint SCALED_ROUND_FRACTIONAL_PART_MASK = 0x80000000; //С���������벿�ֵ�mask
	internal const int INTEGRAL_PART_ALL_ZERO = 0; //��������ȫ0
	internal const int INTEGRAL_PART_ALL_ONE = -1; //��������ȫ1
	internal const ulong COUNT_LEADING_ZERO_ROUGH_Mask = 0xF000000000000000; //��ͳ��ǰ��0����ʱ���Ե�mask������4��4���ļ��
	internal const ulong COUNT_LEADING_ZERO_MASK = 0x8000000000000000; //��ͳ��ǰ��0����ʱ��ϸ��mask,��1��1���ļ��
	internal const ulong ALL_ONE = 0xFFFFFFFFFFFFFFFF; //

	internal const int LUT_SIZE = (int) (SCALED_HALF_PI >> 15);
}