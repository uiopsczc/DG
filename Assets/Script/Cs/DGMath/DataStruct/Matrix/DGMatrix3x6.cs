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

static class Matrix3x6
{
	[ThreadStatic] private static DGFixedPoint[,] Matrix;

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
	public static bool Gauss(DGFixedPoint[,] M, int m, int n)
	{
		// Perform Gauss-Jordan elimination
		for (int k = 0; k < m; k++)
		{
			DGFixedPoint maxValue = DGMath.Abs(M[k, k]);
			int iMax = k;
			for (int i = k + 1; i < m; i++)
			{
				DGFixedPoint value = DGFixedPoint.Abs(M[i, k]);
				if (value >= maxValue)
				{
					maxValue = value;
					iMax = i;
				}
			}

			if (maxValue == (DGFixedPoint) 0)
				return false;
			// Swap rows k, iMax
			if (k != iMax)
			{
				for (int j = 0; j < n; j++)
				{
					DGFixedPoint temp = M[k, j];
					M[k, j] = M[iMax, j];
					M[iMax, j] = temp;
				}
			}

			// Divide row by pivot
			DGFixedPoint pivotInverse = (DGFixedPoint) 1 / M[k, k];

			M[k, k] = (DGFixedPoint) 1;
			for (int j = k + 1; j < n; j++)
			{
				M[k, j] *= pivotInverse;
			}

			// Subtract row k from other rows
			for (int i = 0; i < m; i++)
			{
				if (i == k)
					continue;
				DGFixedPoint f = M[i, k];
				for (int j = k + 1; j < n; j++)
				{
					M[i, j] = M[i, j] - M[k, j] * f;
				}

				M[i, k] = (DGFixedPoint) 0;
			}
		}

		return true;
	}

	public static bool Invert(DGMatrix3x3 m, out DGMatrix3x3 r)
	{
		if (Matrix == null)
			Matrix = new DGFixedPoint[3, 6];
		DGFixedPoint[,] M = Matrix;

		// Initialize temporary matrix
		M[0, 0] = m.SM11;
		M[0, 1] = m.SM12;
		M[0, 2] = m.SM13;
		M[1, 0] = m.SM21;
		M[1, 1] = m.SM22;
		M[1, 2] = m.SM23;
		M[2, 0] = m.SM31;
		M[2, 1] = m.SM32;
		M[2, 2] = m.SM33;

		M[0, 3] = (DGFixedPoint) 1;
		M[0, 4] = (DGFixedPoint) 0;
		M[0, 5] = (DGFixedPoint) 0;
		M[1, 3] = (DGFixedPoint) 0;
		M[1, 4] = (DGFixedPoint) 1;
		M[1, 5] = (DGFixedPoint) 0;
		M[2, 3] = (DGFixedPoint) 0;
		M[2, 4] = (DGFixedPoint) 0;
		M[2, 5] = (DGFixedPoint) 1;

		if (!Gauss(M, 3, 6))
		{
			r = default;
			return false;
		}

		r = new DGMatrix3x3(
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