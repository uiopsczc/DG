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

using System;
using FP = DGFixedPoint;
using FPMatrix4x4 = DGMatrix4x4;

static class DGMatrix4x8
{
	[ThreadStatic] private static FP[,] Matrix;

	public static bool Invert(FPMatrix4x4 m, out FPMatrix4x4 r)
	{
		if (Matrix == null)
			Matrix = new FP[4, 8];
		FP[,] M = Matrix;

		M[0, 0] = m.M11;
		M[0, 1] = m.M12;
		M[0, 2] = m.M13;
		M[0, 3] = m.M14;
		M[1, 0] = m.M21;
		M[1, 1] = m.M22;
		M[1, 2] = m.M23;
		M[1, 3] = m.M24;
		M[2, 0] = m.M31;
		M[2, 1] = m.M32;
		M[2, 2] = m.M33;
		M[2, 3] = m.M34;
		M[3, 0] = m.M41;
		M[3, 1] = m.M42;
		M[3, 2] = m.M43;
		M[3, 3] = m.M44;

		M[0, 4] = (FP)1;
		M[0, 5] = (FP)0;
		M[0, 6] = (FP)0;
		M[0, 7] = (FP)0;
		M[1, 4] = (FP)0;
		M[1, 5] = (FP)1;
		M[1, 6] = (FP)0;
		M[1, 7] = (FP)0;
		M[2, 4] = (FP)0;
		M[2, 5] = (FP)0;
		M[2, 6] = (FP)1;
		M[2, 7] = (FP)0;
		M[3, 4] = (FP)0;
		M[3, 5] = (FP)0;
		M[3, 6] = (FP)0;
		M[3, 7] = (FP)1;


		if (!Matrix3x6.Gauss(M, 4, 8))
		{
			r = new FPMatrix4x4();
			return false;
		}
		r = new FPMatrix4x4(
			// m11...m14
			M[0, 4],
			M[0, 5],
			M[0, 6],
			M[0, 7],

			// m21...m24				
			M[1, 4],
			M[1, 5],
			M[1, 6],
			M[1, 7],

			// m31...m34
			M[2, 4],
			M[2, 5],
			M[2, 6],
			M[2, 7],

			// m41...m44
			M[3, 4],
			M[3, 5],
			M[3, 6],
			M[3, 7]
		);
		return true;
	}
}
