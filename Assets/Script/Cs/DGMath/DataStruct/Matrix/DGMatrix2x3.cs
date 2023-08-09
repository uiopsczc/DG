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
using FPVector3 = DGVector3;
using FPMatrix4x4 = DGMatrix4x4;
using FPMatrix3x3 = DGMatrix3x3;
using FPMatrix3x2 = DGMatrix3x2;
using FPVector2 = DGVector2;

/// <summary>
/// 2 row, 3 column matrix.
/// </summary>
public struct DGMatrix2x3
{
	/// <summary>
	/// Value at row 1, column 1 of the matrix.
	/// </summary>
	public FP SM11;

	/// <summary>
	/// Value at row 1, column 2 of the matrix.
	/// </summary>
	public FP SM12;

	/// <summary>
	/// Value at row 1, column 2 of the matrix.
	/// </summary>
	public FP SM13;

	/// <summary>
	/// Value at row 2, column 1 of the matrix.
	/// </summary>
	public FP SM21;

	/// <summary>
	/// Value at row 2, column 2 of the matrix.
	/// </summary>
	public FP SM22;

	/// <summary>
	/// Value at row 2, column 3 of the matrix.
	/// </summary>
	public FP SM23;


	/// <summary>
	/// Constructs a new 2 row, 2 column matrix.
	/// </summary>
	/// <param name="sm11">Value at row 1, column 1 of the matrix.</param>
	/// <param name="sm12">Value at row 1, column 2 of the matrix.</param>
	/// <param name="sm13">Value at row 1, column 3 of the matrix.</param>
	/// <param name="sm21">Value at row 2, column 1 of the matrix.</param>
	/// <param name="sm22">Value at row 2, column 2 of the matrix.</param>
	/// <param name="sm23">Value at row 2, column 3 of the matrix.</param>
	public DGMatrix2x3(FP sm11, FP sm12, FP sm13, FP sm21, FP sm22, FP sm23)
	{
		SM11 = sm11;
		SM12 = sm12;
		SM13 = sm13;
		SM21 = sm21;
		SM22 = sm22;
		SM23 = sm23;
	}

	/*************************************************************************************
	* 模块描述:ToString
	*************************************************************************************/
	/// <summary>
	/// Creates a string representation of the matrix.
	/// </summary>
	/// <returns>A string representation of the matrix.</returns>
	public override string ToString()
	{
		return "{" + SM11 + ", " + SM12 + ", " + SM13 + "} \n" +
		       "{" + SM21 + ", " + SM22 + ", " + SM23 + "}";
	}

	/*************************************************************************************
	* 模块描述:StaticUtil
	*************************************************************************************/
	/// <summary>
	/// Adds the two matrices together on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to add.</param>
	/// <param name="b">Second matrix to add.</param>
	/// <param name="result">Sum of the two matrices.</param>
	public static DGMatrix2x3 Add(DGMatrix2x3 a, DGMatrix2x3 b)
	{
		DGMatrix2x3 result = default;
		FP m11 = a.SM11 + b.SM11;
		FP m12 = a.SM12 + b.SM12;
		FP m13 = a.SM13 + b.SM13;

		FP m21 = a.SM21 + b.SM21;
		FP m22 = a.SM22 + b.SM22;
		FP m23 = a.SM23 + b.SM23;

		result.SM11 = m11;
		result.SM12 = m12;
		result.SM13 = m13;

		result.SM21 = m21;
		result.SM22 = m22;
		result.SM23 = m23;

		return result;
	}


	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix2x3 Multiply(DGMatrix2x3 a, FPMatrix3x3 b)
	{
		DGMatrix2x3 result = default;
		FP resultM11 = a.SM11 * b.SM11 + a.SM12 * b.SM21 + a.SM13 * b.SM31;
		FP resultM12 = a.SM11 * b.SM12 + a.SM12 * b.SM22 + a.SM13 * b.SM32;
		FP resultM13 = a.SM11 * b.SM13 + a.SM12 * b.SM23 + a.SM13 * b.SM33;

		FP resultM21 = a.SM21 * b.SM11 + a.SM22 * b.SM21 + a.SM23 * b.SM31;
		FP resultM22 = a.SM21 * b.SM12 + a.SM22 * b.SM22 + a.SM23 * b.SM32;
		FP resultM23 = a.SM21 * b.SM13 + a.SM22 * b.SM23 + a.SM23 * b.SM33;

		result.SM11 = resultM11;
		result.SM12 = resultM12;
		result.SM13 = resultM13;

		result.SM21 = resultM21;
		result.SM22 = resultM22;
		result.SM23 = resultM23;

		return result;
	}

	/// <summary>
	/// Multiplies the two matrices.
	/// </summary>
	/// <param name="a">First matrix to multiply.</param>
	/// <param name="b">Second matrix to multiply.</param>
	/// <param name="result">Product of the multiplication.</param>
	public static DGMatrix2x3 Multiply(DGMatrix2x3 a, FPMatrix4x4 b)
	{
		DGMatrix2x3 result = default;
		FP resultM11 = a.SM11 * b.SM11 + a.SM12 * b.SM21 + a.SM13 * b.SM31;
		FP resultM12 = a.SM11 * b.SM12 + a.SM12 * b.SM22 + a.SM13 * b.SM32;
		FP resultM13 = a.SM11 * b.SM13 + a.SM12 * b.SM23 + a.SM13 * b.SM33;

		FP resultM21 = a.SM21 * b.SM11 + a.SM22 * b.SM21 + a.SM23 * b.SM31;
		FP resultM22 = a.SM21 * b.SM12 + a.SM22 * b.SM22 + a.SM23 * b.SM32;
		FP resultM23 = a.SM21 * b.SM13 + a.SM22 * b.SM23 + a.SM23 * b.SM33;

		result.SM11 = resultM11;
		result.SM12 = resultM12;
		result.SM13 = resultM13;

		result.SM21 = resultM21;
		result.SM22 = resultM22;
		result.SM23 = resultM23;

		return result;
	}

	/// <summary>
	/// Negates every element in the matrix.
	/// </summary>
	/// <param name="matrix">Matrix to negate.</param>
	/// <param name="result">Negated matrix.</param>
	public static DGMatrix2x3 Negate(DGMatrix2x3 matrix)
	{
		DGMatrix2x3 result = default;
		FP m11 = -matrix.SM11;
		FP m12 = -matrix.SM12;
		FP m13 = -matrix.SM13;

		FP m21 = -matrix.SM21;
		FP m22 = -matrix.SM22;
		FP m23 = -matrix.SM23;

		result.SM11 = m11;
		result.SM12 = m12;
		result.SM13 = m13;

		result.SM21 = m21;
		result.SM22 = m22;
		result.SM23 = m23;

		return result;
	}

	/// <summary>
	/// Subtracts the two matrices from each other on a per-element basis.
	/// </summary>
	/// <param name="a">First matrix to subtract.</param>
	/// <param name="b">Second matrix to subtract.</param>
	/// <param name="result">Difference of the two matrices.</param>
	public static DGMatrix2x3 Subtract(DGMatrix2x3 a, DGMatrix2x3 b)
	{
		DGMatrix2x3 result = default;
		FP m11 = a.SM11 - b.SM11;
		FP m12 = a.SM12 - b.SM12;
		FP m13 = a.SM13 - b.SM13;

		FP m21 = a.SM21 - b.SM21;
		FP m22 = a.SM22 - b.SM22;
		FP m23 = a.SM23 - b.SM23;

		result.SM11 = m11;
		result.SM12 = m12;
		result.SM13 = m13;

		result.SM21 = m21;
		result.SM22 = m22;
		result.SM23 = m23;

		return result;
	}


	/// <summary>
	/// Transforms the vector by the matrix.
	/// </summary>
	/// <param name="v">Vector2 to transform.  Considered to be a row vector for purposes of multiplication.</param>
	/// <param name="matrix">Matrix to use as the transformation.</param>
	/// <param name="result">Row vector product of the transformation.</param>
	public static FPVector3 Transform(FPVector2 v, DGMatrix2x3 matrix)
	{
		FPVector3 result = default;
		result.x = v.x * matrix.SM11 + v.y * matrix.SM21;
		result.y = v.x * matrix.SM12 + v.y * matrix.SM22;
		result.x = v.x * matrix.SM13 + v.y * matrix.SM23;

		return result;
	}

	/// <summary>
	/// Transforms the vector by the matrix.
	/// </summary>
	/// <param name="v">Vector2 to transform.  Considered to be a column vector for purposes of multiplication.</param>
	/// <param name="matrix">Matrix to use as the transformation.</param>
	/// <param name="result">Column vector product of the transformation.</param>
	public static FPVector2 Transform(FPVector3 v, DGMatrix2x3 matrix)
	{
		FPVector2 result = default;
		result.x = matrix.SM11 * v.x + matrix.SM12 * v.y + matrix.SM13 * v.z;
		result.y = matrix.SM21 * v.x + matrix.SM22 * v.y + matrix.SM23 * v.z;
		return result;
	}


	/// <summary>
	/// Computes the transposed matrix of a matrix.
	/// </summary>
	/// <param name="matrix">Matrix to transpose.</param>
	/// <param name="result">Transposed matrix.</param>
	public static FPMatrix3x2 Transpose(DGMatrix2x3 matrix)
	{
		FPMatrix3x2 result = default;
		result.SM11 = matrix.SM11;
		result.SM12 = matrix.SM21;

		result.SM21 = matrix.SM12;
		result.SM22 = matrix.SM22;

		result.SM31 = matrix.SM13;
		result.SM32 = matrix.SM23;

		return result;
	}
}