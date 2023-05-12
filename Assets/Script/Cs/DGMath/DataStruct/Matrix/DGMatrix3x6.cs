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
using FPMatrix3x3 = DGMatrix3x3;

static class Matrix3x6
{
	[ThreadStatic] private static FP[,] Matrix;

	public static bool Gauss(FP[,] M, int m, int n)
	{
		// Perform Gauss-Jordan elimination
		for (int k = 0; k < m; k++)
		{
			FP maxValue = DGMath.Abs(M[k, k]);
			int iMax = k;
			for (int i = k + 1; i < m; i++)
			{
				FP value = FP.Abs(M[i, k]);
				if (value >= maxValue)
				{
					maxValue = value;
					iMax = i;
				}
			}
			if (maxValue == (FP)0)
				return false;
			// Swap rows k, iMax
			if (k != iMax)
			{
				for (int j = 0; j < n; j++)
				{
					FP temp = M[k, j];
					M[k, j] = M[iMax, j];
					M[iMax, j] = temp;
				}
			}

			// Divide row by pivot
			FP pivotInverse = (FP)1 / M[k, k];

			M[k, k] = (FP)1;
			for (int j = k + 1; j < n; j++)
			{
				M[k, j] *= pivotInverse;
			}

			// Subtract row k from other rows
			for (int i = 0; i < m; i++)
			{
				if (i == k)
					continue;
				FP f = M[i, k];
				for (int j = k + 1; j < n; j++)
				{
					M[i, j] = M[i, j] - M[k, j] * f;
				}
				M[i, k] = (FP)0;
			}
		}
		return true;
	}

	public static bool Invert(FPMatrix3x3 m, out FPMatrix3x3 r)
	{
		if (Matrix == null)
			Matrix = new FP[3, 6];
		FP[,] M = Matrix;

		// Initialize temporary matrix
		M[0, 0] = m.M11;
		M[0, 1] = m.M12;
		M[0, 2] = m.M13;
		M[1, 0] = m.M21;
		M[1, 1] = m.M22;
		M[1, 2] = m.M23;
		M[2, 0] = m.M31;
		M[2, 1] = m.M32;
		M[2, 2] = m.M33;

		M[0, 3] = (FP)1;
		M[0, 4] = (FP)0;
		M[0, 5] = (FP)0;
		M[1, 3] = (FP)0;
		M[1, 4] = (FP)1;
		M[1, 5] = (FP)0;
		M[2, 3] = (FP)0;
		M[2, 4] = (FP)0;
		M[2, 5] = (FP)1;

		if (!Gauss(M, 3, 6))
		{
			r = new FPMatrix3x3();
			return false;
		}
		r = new FPMatrix3x3(
			// m11...m13
			M[0, 3],
			M[0, 4],
			M[0, 5],

			// m21...m23
			M[1, 3],
			M[1, 4],
			M[1, 5],

			// m31...m33
			M[2, 3],
			M[2, 4],
			M[2, 5]
			);
		return true;
	}
}
