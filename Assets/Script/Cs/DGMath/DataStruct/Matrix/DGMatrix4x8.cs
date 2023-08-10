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

static class DGMatrix4x8
{
	[ThreadStatic] private static DGFixedPoint[,] Matrix;

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
	public static bool Invert(DGMatrix4x4 m, out DGMatrix4x4 r)
	{
		if (Matrix == null)
			Matrix = new DGFixedPoint[4, 8];
		DGFixedPoint[,] M = Matrix;

		M[0, 0] = m.SM11;
		M[0, 1] = m.SM12;
		M[0, 2] = m.SM13;
		M[0, 3] = m.SM14;
		M[1, 0] = m.SM21;
		M[1, 1] = m.SM22;
		M[1, 2] = m.SM23;
		M[1, 3] = m.SM24;
		M[2, 0] = m.SM31;
		M[2, 1] = m.SM32;
		M[2, 2] = m.SM33;
		M[2, 3] = m.SM34;
		M[3, 0] = m.SM41;
		M[3, 1] = m.SM42;
		M[3, 2] = m.SM43;
		M[3, 3] = m.SM44;

		M[0, 4] = (DGFixedPoint) 1;
		M[0, 5] = (DGFixedPoint) 0;
		M[0, 6] = (DGFixedPoint) 0;
		M[0, 7] = (DGFixedPoint) 0;
		M[1, 4] = (DGFixedPoint) 0;
		M[1, 5] = (DGFixedPoint) 1;
		M[1, 6] = (DGFixedPoint) 0;
		M[1, 7] = (DGFixedPoint) 0;
		M[2, 4] = (DGFixedPoint) 0;
		M[2, 5] = (DGFixedPoint) 0;
		M[2, 6] = (DGFixedPoint) 1;
		M[2, 7] = (DGFixedPoint) 0;
		M[3, 4] = (DGFixedPoint) 0;
		M[3, 5] = (DGFixedPoint) 0;
		M[3, 6] = (DGFixedPoint) 0;
		M[3, 7] = (DGFixedPoint) 1;


		if (!Matrix3x6.Gauss(M, 4, 8))
		{
			r = new DGMatrix4x4();
			return false;
		}

		r = new DGMatrix4x4(
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