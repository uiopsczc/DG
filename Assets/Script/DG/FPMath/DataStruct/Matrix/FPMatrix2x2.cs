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
    /// 2 row, 2 column matrix.
    /// </summary>
    public struct FPMatrix2x2
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
        /// Value at row 2, column 1 of the matrix.
        /// </summary>
        public FP SM21;

        /// <summary>
        /// Value at row 2, column 2 of the matrix.
        /// </summary>
        public FP SM22;

        /// <summary>
        /// Gets the 2x2 identity matrix.
        /// </summary>
        public static FPMatrix2x2 Identity => new(1, 0, 0, 1);

        /// <summary>
        /// Constructs a new 2 row, 2 column matrix.
        /// </summary>
        /// <param name="m11">Value at row 1, column 1 of the matrix.</param>
        /// <param name="m12">Value at row 1, column 2 of the matrix.</param>
        /// <param name="m21">Value at row 2, column 1 of the matrix.</param>
        /// <param name="m22">Value at row 2, column 2 of the matrix.</param>
        public FPMatrix2x2(FP m11, FP m12, FP m21, FP m22)
        {
            SM11 = m11;
            SM12 = m12;
            SM21 = m21;
            SM22 = m22;
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
            return "{" + SM11 + ", " + SM12 + "} " +
                   "{" + SM21 + ", " + SM22 + "}";
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
        public static FPMatrix2x2 Add(FPMatrix2x2 a, FPMatrix2x2 b)
        {
            FPMatrix2x2 result = default;
            FP m11 = a.SM11 + b.SM11;
            FP m12 = a.SM12 + b.SM12;

            FP m21 = a.SM21 + b.SM21;
            FP m22 = a.SM22 + b.SM22;

            result.SM11 = m11;
            result.SM12 = m12;

            result.SM21 = m21;
            result.SM22 = m22;

            return result;
        }

        /// <summary>
        /// Adds the two matrices together on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two matrices.</param>
        public static FPMatrix2x2 Add(FPMatrix4x4 a, FPMatrix2x2 b)
        {
            FPMatrix2x2 result = default;
            FP m11 = a.sm11 + b.SM11;
            FP m12 = a.sm12 + b.SM12;

            FP m21 = a.sm21 + b.SM21;
            FP m22 = a.sm22 + b.SM22;

            result.SM11 = m11;
            result.SM12 = m12;

            result.SM21 = m21;
            result.SM22 = m22;

            return result;
        }

        /// <summary>
        /// Adds the two matrices together on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two matrices.</param>
        public static FPMatrix2x2 Add(FPMatrix2x2 a, FPMatrix4x4 b)
        {
            FPMatrix2x2 result = default;
            FP m11 = a.SM11 + b.sm11;
            FP m12 = a.SM12 + b.sm12;

            FP m21 = a.SM21 + b.sm21;
            FP m22 = a.SM22 + b.sm22;

            result.SM11 = m11;
            result.SM12 = m12;

            result.SM21 = m21;
            result.SM22 = m22;

            return result;
        }

        /// <summary>
        /// Adds the two matrices together on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two matrices.</param>
        public static FPMatrix2x2 Add(FPMatrix4x4 a, FPMatrix4x4 b)
        {
            FPMatrix2x2 result = default;
            FP m11 = a.sm11 + b.sm11;
            FP m12 = a.sm12 + b.sm12;

            FP m21 = a.sm21 + b.sm21;
            FP m22 = a.sm22 + b.sm22;

            result.SM11 = m11;
            result.SM12 = m12;

            result.SM21 = m21;
            result.SM22 = m22;

            return result;
        }

        /// <summary>
        /// Constructs a uniform scaling matrix.
        /// </summary>
        /// <param name="scale">Value to use in the diagonal.</param>
        /// <param name="matrix">Scaling matrix.</param>
        public static FPMatrix2x2 CreateScale(FP scale)
        {
            FPMatrix2x2 matrix = default;
            matrix.SM11 = scale;
            matrix.SM22 = scale;

            matrix.SM12 = 0;
            matrix.SM21 = 0;

            return matrix;
        }


        /// <summary>
        /// Inverts the given matix.
        /// </summary>
        /// <param name="matrix">Matrix to be inverted.</param>
        /// <param name="result">Inverted matrix.</param>
        public static FPMatrix2x2 Invert(FPMatrix2x2 matrix)
        {
            FPMatrix2x2 result = default;
            FP determinantInverse = 1 / (matrix.SM11 * matrix.SM22 - matrix.SM12 * matrix.SM21);
            FP m11 = matrix.SM22 * determinantInverse;
            FP m12 = -matrix.SM12 * determinantInverse;

            FP m21 = -matrix.SM21 * determinantInverse;
            FP m22 = matrix.SM11 * determinantInverse;

            result.SM11 = m11;
            result.SM12 = m12;

            result.SM21 = m21;
            result.SM22 = m22;

            return result;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static FPMatrix2x2 Multiply(FPMatrix2x2 a, FPMatrix2x2 b)
        {
            FPMatrix2x2 result = default;
            FP resultM11 = a.SM11 * b.SM11 + a.SM12 * b.SM21;
            FP resultM12 = a.SM11 * b.SM12 + a.SM12 * b.SM22;

            FP resultM21 = a.SM21 * b.SM11 + a.SM22 * b.SM21;
            FP resultM22 = a.SM21 * b.SM12 + a.SM22 * b.SM22;

            result.SM11 = resultM11;
            result.SM12 = resultM12;

            result.SM21 = resultM21;
            result.SM22 = resultM22;

            return result;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static FPMatrix2x2 Multiply(FPMatrix2x2 a, FPMatrix4x4 b)
        {
            FPMatrix2x2 result = default;
            FP resultM11 = a.SM11 * b.sm11 + a.SM12 * b.sm21;
            FP resultM12 = a.SM11 * b.sm12 + a.SM12 * b.sm22;

            FP resultM21 = a.SM21 * b.sm11 + a.SM22 * b.sm21;
            FP resultM22 = a.SM21 * b.sm12 + a.SM22 * b.sm22;

            result.SM11 = resultM11;
            result.SM12 = resultM12;

            result.SM21 = resultM21;
            result.SM22 = resultM22;

            return result;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static FPMatrix2x2 Multiply(FPMatrix4x4 a, FPMatrix2x2 b)
        {
            FPMatrix2x2 result = default;
            FP resultM11 = a.sm11 * b.SM11 + a.sm12 * b.SM21;
            FP resultM12 = a.sm11 * b.SM12 + a.sm12 * b.SM22;

            FP resultM21 = a.sm21 * b.SM11 + a.sm22 * b.SM21;
            FP resultM22 = a.sm21 * b.SM12 + a.sm22 * b.SM22;

            result.SM11 = resultM11;
            result.SM12 = resultM12;

            result.SM21 = resultM21;
            result.SM22 = resultM22;

            return result;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static FPMatrix2x2 Multiply(FPMatrix2x3 a, FPMatrix3x2 b)
        {
            FPMatrix2x2 result = default;
            result.SM11 = a.SM11 * b.SM11 + a.SM12 * b.SM21 + a.SM13 * b.SM31;
            result.SM12 = a.SM11 * b.SM12 + a.SM12 * b.SM22 + a.SM13 * b.SM32;

            result.SM21 = a.SM21 * b.SM11 + a.SM22 * b.SM21 + a.SM23 * b.SM31;
            result.SM22 = a.SM21 * b.SM12 + a.SM22 * b.SM22 + a.SM23 * b.SM32;
            return result;
        }

        /// <summary>
        /// Negates every element in the matrix.
        /// </summary>
        /// <param name="matrix">Matrix to negate.</param>
        /// <param name="result">Negated matrix.</param>
        public static FPMatrix2x2 Negate(FPMatrix2x2 matrix)
        {
            FPMatrix2x2 result = default;
            FP m11 = -matrix.SM11;
            FP m12 = -matrix.SM12;

            FP m21 = -matrix.SM21;
            FP m22 = -matrix.SM22;


            result.SM11 = m11;
            result.SM12 = m12;

            result.SM21 = m21;
            result.SM22 = m22;

            return result;
        }

        /// <summary>
        /// Subtracts the two matrices from each other on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to subtract.</param>
        /// <param name="b">Second matrix to subtract.</param>
        /// <param name="result">Difference of the two matrices.</param>
        public static FPMatrix2x2 Subtract(FPMatrix2x2 a, FPMatrix2x2 b)
        {
            FPMatrix2x2 result = default;
            FP m11 = a.SM11 - b.SM11;
            FP m12 = a.SM12 - b.SM12;

            FP m21 = a.SM21 - b.SM21;
            FP m22 = a.SM22 - b.SM22;

            result.SM11 = m11;
            result.SM12 = m12;

            result.SM21 = m21;
            result.SM22 = m22;

            return result;
        }

        /// <summary>
        /// Transforms the vector by the matrix.
        /// </summary>
        /// <param name="v">Vector2 to transform.</param>
        /// <param name="matrix">Matrix to use as the transformation.</param>
        /// <param name="result">Product of the transformation.</param>
        public static FPVector2 Transform(FPVector2 v, FPMatrix2x2 matrix)
        {
            FPVector2 result = default;
            FP vX = v.x;
            FP vY = v.y;
            result.x = vX * matrix.SM11 + vY * matrix.SM21;
            result.y = vX * matrix.SM12 + vY * matrix.SM22;

            return result;
        }

        /// <summary>
        /// Computes the transposed matrix of a matrix.
        /// </summary>
        /// <param name="matrix">Matrix to transpose.</param>
        /// <param name="result">Transposed matrix.</param>
        public static FPMatrix2x2 Transpose(FPMatrix2x2 matrix)
        {
            FPMatrix2x2 result = default;
            FP m21 = matrix.SM12;

            result.SM11 = matrix.SM11;
            result.SM12 = matrix.SM21;

            result.SM21 = m21;
            result.SM22 = matrix.SM22;

            return result;
        }
        /*************************************************************************************
         * 模块描述:Util
         *************************************************************************************/


        /// <summary>
        /// Transposes the matrix in-place.
        /// </summary>
        public void Transpose()
        {
            FP m21 = SM21;
            SM21 = SM12;
            SM12 = m21;
        }


        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        /// <returns>The matrix's determinant.</returns>
        public FP Determinant()
        {
            return SM11 * SM22 - SM12 * SM21;
        }
    }
}