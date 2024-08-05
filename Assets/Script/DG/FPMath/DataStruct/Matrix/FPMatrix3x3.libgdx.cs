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

namespace DG
{
    /// <summary>
    /// 3 row, 3 column matrix.
    /// </summary>
    public partial struct FPMatrix3x3
    {
        public const int M00Index = 0;
        public const int M01Index = 3;
        public const int M02Index = 6;
        public const int M10Index = 1;
        public const int M11Index = 4;
        public const int M12Index = 7;
        public const int M20Index = 2;
        public const int M21Index = 5;
        public const int M22Index = 8;
        private const int Count = 9;


        public FP m00;
        public FP m01;
        public FP m02;
        public FP m10;
        public FP m11;
        public FP m12;
        public FP m20;
        public FP m21;
        public FP m22;


        public static FPMatrix3x3 default2
        {
            get
            {
                FPMatrix3x3 result = default;
                result.m00 = 1f;
                result.m11 = 1f;
                result.m22 = 1f;
                return result;
            }
        }

        public FP[] val
        {
            get
            {
                var result = new FP[Count];
                result[M00Index] = m00;
                result[M01Index] = m01;
                result[M02Index] = m02;
                result[M10Index] = m10;
                result[M11Index] = m11;
                result[M12Index] = m12;
                result[M20Index] = m20;
                result[M21Index] = m21;
                result[M22Index] = m22;
                return result;
            }
        }


        private FP[] _NewTmp()
        {
            var result = new FP[Count];
            result[M22Index] = 1;
            return result;
        }


        /** Constructs a matrix from the given float array. The array must have at least 9 elements; the first 9 will be copied.
         * @param values The float array to copy. Remember that this matrix is in
         *           <a href="http://en.wikipedia.org/wiki/Row-major_order#Column-major_order">column major</a> order. (The float array
         *           is not modified.) */
        public FPMatrix3x3(FP[] values)
        {
            m00 = values[M00Index];
            m01 = values[M01Index];
            m02 = values[M02Index];
            m10 = values[M10Index];
            m11 = values[M11Index];
            m12 = values[M12Index];
            m20 = values[M20Index];
            m21 = values[M21Index];
            m22 = values[M22Index];
        }

        /** Sets this matrix to the identity matrix
         * @return This matrix for the purpose of chaining operations. */
        public FPMatrix3x3 idt()
        {
            m00 = 1;
            m10 = 0;
            m20 = 0;
            m01 = 0;
            m11 = 1;
            m21 = 0;
            m02 = 0;
            m12 = 0;
            m22 = 1;
            return this;
        }

        /** Postmultiplies this matrix with the provided matrix and stores the result in this matrix. For example:
         *
         * <pre>
         * A.mul(B) results in A := AB
         * </pre>
         *
         * @param m Matrix to multiply by.
         * @return This matrix for the purpose of chaining operations together. */
        public FPMatrix3x3 mul(FPMatrix3x3 m)
        {
            FP v00 = m00 * m.m00 + m01 * m.m10 + m02 * m.m20;
            FP v01 = m00 * m.m01 + m01 * m.m11 + m02 * m.m21;
            FP v02 = m00 * m.m02 + m01 * m.m12 + m02 * m.m22;

            FP v10 = m10 * m.m00 + m11 * m.m10 + m12 * m.m20;
            FP v11 = m10 * m.m01 + m11 * m.m11 + m12 * m.m21;
            FP v12 = m10 * m.m02 + m11 * m.m12 + m12 * m.m22;

            FP v20 = m20 * m.m00 + m21 * m.m10 + m22 * m.m20;
            FP v21 = m20 * m.m01 + m21 * m.m11 + m22 * m.m21;
            FP v22 = m20 * m.m02 + m21 * m.m12 + m22 * m.m22;

            m00 = v00;
            m10 = v10;
            m20 = v20;
            m01 = v01;
            m11 = v11;
            m21 = v21;
            m02 = v02;
            m12 = v12;
            m22 = v22;

            return this;
        }

        /** Premultiplies this matrix with the provided matrix and stores the result in this matrix. For example:
         *
         * <pre>
         * A.mulLeft(B) results in A := BA
         * </pre>
         *
         * @param m The other Matrix to multiply by
         * @return This matrix for the purpose of chaining operations. */
        public FPMatrix3x3 mulLeft(FPMatrix3x3 m)
        {
            FP v00 = m.m00 * m00 + m.m01 * m10 + m.m02 * m20;
            FP v01 = m.m00 * m01 + m.m01 * m11 + m.m02 * m21;
            FP v02 = m.m00 * m02 + m.m01 * m12 + m.m02 * m22;

            FP v10 = m.m10 * m00 + m.m11 * m10 + m.m12 * m20;
            FP v11 = m.m10 * m01 + m.m11 * m11 + m.m12 * m21;
            FP v12 = m.m10 * m02 + m.m11 * m12 + m.m12 * m22;

            FP v20 = m.m20 * m00 + m.m21 * m10 + m.m22 * m20;
            FP v21 = m.m20 * m01 + m.m21 * m11 + m.m22 * m21;
            FP v22 = m.m20 * m02 + m.m21 * m12 + m.m22 * m22;

            m00 = v00;
            m10 = v10;
            m20 = v20;
            m01 = v01;
            m11 = v11;
            m21 = v21;
            m02 = v02;
            m12 = v12;
            m22 = v22;

            return this;
        }

        /** Sets this matrix to a rotation matrix that will rotate any vector in counter-clockwise direction around the z-axis.
         * @param degrees the angle in degrees.
         * @return This matrix for the purpose of chaining operations. */
        public FPMatrix3x3 setToRotation(FP degrees)
        {
            return setToRotationRad(FPMath.DEG2RAD * degrees);
        }

        /** Sets this matrix to a rotation matrix that will rotate any vector in counter-clockwise direction around the z-axis.
         * @param radians the angle in radians.
         * @return This matrix for the purpose of chaining operations. */
        public FPMatrix3x3 setToRotationRad(FP radians)
        {
            FP cos = FPMath.Cos(radians);
            FP sin = FPMath.Sin(radians);

            m00 = cos;
            m10 = sin;
            m20 = 0;

            m01 = -sin;
            m11 = cos;
            m21 = 0;

            m02 = 0;
            m12 = 0;
            m22 = 1;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis">Axis around which to rotate. need nomalized</param>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public FPMatrix3x3 setToRotation(FPVector3 axis, FP degrees)
        {
            return setToRotation(axis, FPMath.CosDeg(degrees), FPMath.SinDeg(degrees));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis">Axis around which to rotate. need nomalized</param>
        /// <param name="cos"></param>
        /// <param name="sin"></param>
        /// <returns></returns>
        public FPMatrix3x3 setToRotation(FPVector3 axis, FP cos, FP sin)
        {
            FP oc = 1.0f - cos;
            m00 = oc * axis.x * axis.x + cos;
            m01 = oc * axis.x * axis.y - axis.z * sin;
            m02 = oc * axis.z * axis.x + axis.y * sin;
            m10 = oc * axis.x * axis.y + axis.z * sin;
            m11 = oc * axis.y * axis.y + cos;
            m12 = oc * axis.y * axis.z - axis.x * sin;
            m20 = oc * axis.z * axis.x - axis.y * sin;
            m21 = oc * axis.y * axis.z + axis.x * sin;
            m22 = oc * axis.z * axis.z + cos;
            return this;
        }

        /** Sets this matrix to a translation matrix.
         * @param x the translation in x
         * @param y the translation in y
         * @return This matrix for the purpose of chaining operations. */
        public FPMatrix3x3 setToTranslation(FP x, FP y)
        {
            m00 = 1;
            m10 = 0;
            m20 = 0;

            m01 = 0;
            m11 = 1;
            m21 = 0;

            m02 = x;
            m12 = y;
            m22 = 1;

            return this;
        }

        /** Sets this matrix to a translation matrix.
         * @param translation The translation vector.
         * @return This matrix for the purpose of chaining operations. */
        public FPMatrix3x3 setToTranslation(FPVector2 translation)
        {
            m00 = 1;
            m10 = 0;
            m20 = 0;

            m01 = 0;
            m11 = 1;
            m21 = 0;

            m02 = translation.x;
            m12 = translation.y;
            m22 = 1;

            return this;
        }

        /** Sets this matrix to a scaling matrix.
         *
         * @param scaleX the scale in x
         * @param scaleY the scale in y
         * @return This matrix for the purpose of chaining operations. */
        public FPMatrix3x3 setToScaling(FP scaleX, FP scaleY)
        {
            m00 = scaleX;
            m10 = 0;
            m20 = 0;
            m01 = 0;
            m11 = scaleY;
            m21 = 0;
            m02 = 0;
            m12 = 0;
            m22 = 1;
            return this;
        }

        /** Sets this matrix to a scaling matrix.
         * @param scale The scale vector.
         * @return This matrix for the purpose of chaining operations. */
        public FPMatrix3x3 setToScaling(FPVector2 scale)
        {
            m00 = scale.x;
            m10 = 0;
            m20 = 0;
            m01 = 0;
            m11 = scale.y;
            m21 = 0;
            m02 = 0;
            m12 = 0;
            m22 = 1;
            return this;
        }

        public override string ToString()
        {
            return "[" + m00 + "|" + m01 + "|" + m02 + "]\n" //
                   + "[" + m10 + "|" + m11 + "|" + m12 + "]\n" //
                   + "[" + m20 + "|" + m21 + "|" + m22 + "]";
        }

        /** @return The determinant of this matrix */
        public FP det()
        {
            return m00 * m11 * m22 + m01 * m12 * m20 + m02 * m10 * m21
                   - m00 * m12 * m21 - m01 * m10 * m22 - m02 * m11 * m20;
        }

        /** Inverts this matrix given that the determinant is != 0.
         * @return This matrix for the purpose of chaining operations.
         * @throws GdxRuntimeException if the matrix is singular (not invertible) */
        public FPMatrix3x3 inv()
        {
            FP det = this.det();
            if (det == 0) throw new Exception("Can't invert a singular matrix");

            FP inv_det = 1.0f / det;

            FP v00 = m11 * m22 - m21 * m12;
            FP v10 = m20 * m12 - m10 * m22;
            FP v20 = m10 * m21 - m20 * m11;
            FP v01 = m21 * m02 - m01 * m22;
            FP v11 = m00 * m22 - m20 * m02;
            FP v21 = m20 * m01 - m00 * m21;
            FP v02 = m01 * m12 - m11 * m02;
            FP v12 = m10 * m02 - m00 * m12;
            FP v22 = m00 * m11 - m10 * m01;

            m00 = inv_det * v00;
            m10 = inv_det * v10;
            m20 = inv_det * v20;
            m01 = inv_det * v01;
            m11 = inv_det * v11;
            m21 = inv_det * v21;
            m02 = inv_det * v02;
            m12 = inv_det * v12;
            m22 = inv_det * v22;

            return this;
        }

        /** Copies the values from the provided matrix to this matrix.
         * @param mat The matrix to copy.
         * @return This matrix for the purposes of chaining. */
        public FPMatrix3x3 set(FPMatrix3x3 mat)
        {
            m00 = mat.m00;
            m01 = mat.m01;
            m02 = mat.m02;
            m10 = mat.m10;
            m11 = mat.m11;
            m12 = mat.m12;
            m20 = mat.m20;
            m21 = mat.m21;
            m22 = mat.m22;
            return this;
        }

        /** Copies the values from the provided affine matrix to this matrix. The last row is set to (0, 0, 1).
         * @param affine The affine matrix to copy.
         * @return This matrix for the purposes of chaining. */
        public FPMatrix3x3 set(FPAffine2 affine)
        {
            m00 = affine.m00;
            m10 = affine.m10;
            m20 = 0;
            m01 = affine.m01;
            m11 = affine.m11;
            m21 = 0;
            m02 = affine.m02;
            m12 = affine.m12;
            m22 = 1;

            return this;
        }

        /** Sets this 3x3 matrix to the top left 3x3 corner of the provided 4x4 matrix.
         * @param mat The matrix whose top left corner will be copied. This matrix will not be modified.
         * @return This matrix for the purpose of chaining operations. */
        public FPMatrix3x3 set(FPMatrix4x4 mat)
        {
            m00 = mat.m00;
            m10 = mat.m10;
            m20 = mat.m20;
            m01 = mat.m01;
            m11 = mat.m11;
            m21 = mat.m21;
            m02 = mat.m02;
            m12 = mat.m12;
            m22 = mat.m22;
            return this;
        }

        /** Sets the matrix to the given matrix as a float array. The float array must have at least 9 elements; the first 9 will be
         * copied.
         *
         * @param values The matrix, in float form, that is to be copied. Remember that this matrix is in
         *           <a href="http://en.wikipedia.org/wiki/Row-major_order#Column-major_order">column major</a> order.
         * @return This matrix for the purpose of chaining methods together. */
        public FPMatrix3x3 set(FP[] values)
        {
            m00 = values[M00Index];
            m01 = values[M01Index];
            m02 = values[M02Index];
            m10 = values[M10Index];
            m11 = values[M11Index];
            m12 = values[M12Index];
            m20 = values[M20Index];
            m21 = values[M21Index];
            m22 = values[M22Index];
            return this;
        }

        /** Adds a translational component to the matrix in the 3rd column. The other columns are untouched.
         * @param vector The translation vector.
         * @return This matrix for the purpose of chaining. */
        public FPMatrix3x3 trn(FPVector2 vector)
        {
            m02 += vector.x;
            m12 += vector.y;
            return this;
        }

        /** Adds a translational component to the matrix in the 3rd column. The other columns are untouched.
         * @param x The x-component of the translation vector.
         * @param y The y-component of the translation vector.
         * @return This matrix for the purpose of chaining. */
        public FPMatrix3x3 trn(FP x, FP y)
        {
            m02 += x;
            m12 += y;
            return this;
        }

        /** Adds a translational component to the matrix in the 3rd column. The other columns are untouched.
         * @param vector The translation vector. (The z-component of the vector is ignored because this is a 3x3 matrix)
         * @return This matrix for the purpose of chaining. */
        public FPMatrix3x3 trn(FPVector3 vector)
        {
            m02 += vector.x;
            m12 += vector.y;
            return this;
        }

        /** Postmultiplies this matrix by a translation matrix. Postmultiplication is also used by OpenGL ES' 1.x
         * glTranslate/glRotate/glScale.
         * @param x The x-component of the translation vector.
         * @param y The y-component of the translation vector.
         * @return This matrix for the purpose of chaining. */
        public FPMatrix3x3 translate(FP x, FP y)
        {
            var tmp = default2;
            tmp.m02 = x;
            tmp.m12 = y;
            mul(tmp);
            return this;
        }

        /** Postmultiplies this matrix by a translation matrix. Postmultiplication is also used by OpenGL ES' 1.x
         * glTranslate/glRotate/glScale.
         * @param translation The translation vector.
         * @return This matrix for the purpose of chaining. */
        public FPMatrix3x3 translate(FPVector2 translation)
        {
            var tmp = default2;
            tmp.m02 = translation.x;
            tmp.m12 = translation.y;
            mul(tmp);
            return this;
        }

        /** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
         * glTranslate/glRotate/glScale.
         * @param degrees The angle in degrees
         * @return This matrix for the purpose of chaining. */
        public FPMatrix3x3 rotate(FP degrees)
        {
            return rotateRad(FPMath.DEG2RAD * degrees);
        }

        /** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
         * glTranslate/glRotate/glScale.
         * @param radians The angle in radians
         * @return This matrix for the purpose of chaining. */
        public FPMatrix3x3 rotateRad(FP radians)
        {
            if (radians == 0) return this;
            FP cos = FPMath.Cos(radians);
            FP sin = FPMath.Sin(radians);

            var tmp = default2;

            tmp.m00 = cos;
            tmp.m10 = sin;
            // tmp.m20 = 0;

            tmp.m01 = -sin;
            tmp.m11 = cos;
            // tmp.m21 = 0;

            tmp.m02 = 0;
            tmp.m12 = 0;
            // tmp.m22 = 1;

            mul(tmp);
            return this;
        }

        /** Postmultiplies this matrix with a scale matrix. Postmultiplication is also used by OpenGL ES' 1.x
         * glTranslate/glRotate/glScale.
         * @param scaleX The scale in the x-axis.
         * @param scaleY The scale in the y-axis.
         * @return This matrix for the purpose of chaining. */
        public FPMatrix3x3 scale(FP scaleX, FP scaleY)
        {
            var tmp = default2;
            tmp.m00 = scaleX;
            tmp.m10 = 0;
            // tmp.m20 = 0;

            tmp.m01 = 0;
            tmp.m11 = scaleY;
            // tmp[M21] = 0;

            tmp.m02 = 0;
            tmp.m12 = 0;
            // tmp[M22] = 1;

            mul(tmp);
            return this;
        }

        /** Postmultiplies this matrix with a scale matrix. Postmultiplication is also used by OpenGL ES' 1.x
         * glTranslate/glRotate/glScale.
         * @param scale The vector to scale the matrix by.
         * @return This matrix for the purpose of chaining. */
        public FPMatrix3x3 scale(FPVector2 scale)
        {
            var tmp = default2;

            tmp.m00 = scale.x;
            tmp.m10 = 0;
            // tmp.m20 = 0;

            tmp.m01 = 0;
            tmp.m11 = scale.y;
            // tmp.m21 = 0;

            tmp.m02 = 0;
            tmp.m12 = 0;
            // tmp.m22 = 1;

            mul(tmp);
            return this;
        }

        /** Get the values in this matrix.
         * @return The float values that make up this matrix in column-major order. */
        public FP[] getValues()
        {
            return val;
        }

        public FPVector2 getTranslation(FPVector2 position)
        {
            position.x = m02;
            position.y = m12;
            return position;
        }

        /** @param scale The vector which will receive the (non-negative) scale components on each axis.
         * @return The provided vector for chaining. */
        public FPVector2 getScale(FPVector2 scale)
        {
            scale.x = FPMath.Sqrt(m00 * m00 + m01 * m01);
            scale.y = FPMath.Sqrt(m10 * m10 + m11 * m11);
            return scale;
        }

        public FP getRotation()
        {
            return FPMath.Rad2Deg * FPMath.Atan2(m10, m00);
        }

        public FP getRotationRad()
        {
            return FPMath.Atan2(m10, m00);
        }

        /** Scale the matrix in the both the x and y components by the scalar value.
         * @param scale The single value that will be used to scale both the x and y components.
         * @return This matrix for the purpose of chaining methods together. */
        public FPMatrix3x3 scl(FP scale)
        {
            m00 *= scale;
            m11 *= scale;
            return this;
        }

        /** Scale this matrix using the x and y components of the vector but leave the rest of the matrix alone.
         * @param scale The {@link Vector3} to use to scale this matrix.
         * @return This matrix for the purpose of chaining methods together. */
        public FPMatrix3x3 scl(FPVector2 scale)
        {
            m00 *= scale.x;
            m11 *= scale.y;
            return this;
        }

        /** Scale this matrix using the x and y components of the vector but leave the rest of the matrix alone.
         * @param scale The {@link Vector3} to use to scale this matrix. The z component will be ignored.
         * @return This matrix for the purpose of chaining methods together. */
        public FPMatrix3x3 scl(FPVector3 scale)
        {
            m00 *= scale.x;
            m11 *= scale.y;
            return this;
        }

        /** Transposes the current matrix.
         * @return This matrix for the purpose of chaining methods together. */
        public FPMatrix3x3 transpose()
        {
            // Where MXY you do not have to change MXX
            FP v01 = m10;
            FP v02 = m20;
            FP v10 = m01;
            FP v12 = m21;
            FP v20 = m02;
            FP v21 = m12;
            m01 = v01;
            m02 = v02;
            m10 = v10;
            m12 = v12;
            m20 = v20;
            m21 = v21;
            return this;
        }

        /** Multiplies matrix a with matrix b in the following manner:
         *
         * <pre>
         * mul(A, B) => A := AB
         * </pre>
         *
         * @param mata The float array representing the first matrix. Must have at least 9 elements.
         * @param matb The float array representing the second matrix. Must have at least 9 elements. */
        private static void mul(FP[] mata, FP[] matb)
        {
            FP v00 = mata[M00Index] * matb[M00Index] + mata[M01Index] * matb[M10Index] +
                     mata[M02Index] * matb[M20Index];
            FP v01 = mata[M00Index] * matb[M01Index] + mata[M01Index] * matb[M11Index] +
                     mata[M02Index] * matb[M21Index];
            FP v02 = mata[M00Index] * matb[M02Index] + mata[M01Index] * matb[M12Index] +
                     mata[M02Index] * matb[M22Index];

            FP v10 = mata[M10Index] * matb[M00Index] + mata[M11Index] * matb[M10Index] +
                     mata[M12Index] * matb[M20Index];
            FP v11 = mata[M10Index] * matb[M01Index] + mata[M11Index] * matb[M11Index] +
                     mata[M12Index] * matb[M21Index];
            FP v12 = mata[M10Index] * matb[M02Index] + mata[M11Index] * matb[M12Index] +
                     mata[M12Index] * matb[M22Index];

            FP v20 = mata[M20Index] * matb[M00Index] + mata[M21Index] * matb[M10Index] +
                     mata[M22Index] * matb[M20Index];
            FP v21 = mata[M20Index] * matb[M01Index] + mata[M21Index] * matb[M11Index] +
                     mata[M22Index] * matb[M21Index];
            FP v22 = mata[M20Index] * matb[M02Index] + mata[M21Index] * matb[M12Index] +
                     mata[M22Index] * matb[M22Index];

            mata[M00Index] = v00;
            mata[M10Index] = v10;
            mata[M20Index] = v20;
            mata[M01Index] = v01;
            mata[M11Index] = v11;
            mata[M21Index] = v21;
            mata[M02Index] = v02;
            mata[M12Index] = v12;
            mata[M22Index] = v22;
        }
    }
}