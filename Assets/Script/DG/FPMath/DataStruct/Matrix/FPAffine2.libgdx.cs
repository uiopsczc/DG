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
    /// Matrix2x3
    /// A specialized 3x3 matrix that can represent sequences of 2D translations, scales, flips, rotations, and shears.
    /// <a href="http://en.wikipedia.org/wiki/Affine_transformation">Affine transformations</a> preserve straight lines, and parallel
    /// lines remain parallel after the transformation. Operations on affine matrices are faster because the last row can always be
    /// assumed (0, 0, 1).
    /// </summary>
    public struct FPAffine2
    {
        public FP m00;
        public FP m01;
        public FP m02;
        public FP m10;
        public FP m11;
        public FP m12;

        // constant: m21 = 0, m21 = 1, m22 = 1

        public static FPAffine2 default2
        {
            get
            {
                FPAffine2 result = default;
                result.m00 = 1;
                result.m11 = 1;
                return result;
            }
        }

        public FPAffine2(FP m00, FP m01, FP m02, FP m10, FP m11, FP m12)
        {
            this.m00 = m00;
            this.m01 = m01;
            this.m02 = m02;
            this.m10 = m10;
            this.m11 = m11;
            this.m12 = m12;
        }

        /** Constructs a matrix from the given affine matrix.
         *
         * @param other The affine matrix to copy. This matrix will not be modified. */
        public FPAffine2(FPAffine2 other)
        {
            m00 = other.m00;
            m01 = other.m01;
            m02 = other.m02;
            m10 = other.m10;
            m11 = other.m11;
            m12 = other.m12;
        }

        /** Sets this matrix to the identity matrix
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 idt()
        {
            m00 = 1;
            m01 = 0;
            m02 = 0;
            m10 = 0;
            m11 = 1;
            m12 = 0;
            return this;
        }

        /** Copies the values from the provided affine matrix to this matrix.
         * @param other The affine matrix to copy.
         * @return This matrix for the purposes of chaining. */
        public FPAffine2 set(FPAffine2 other)
        {
            m00 = other.m00;
            m01 = other.m01;
            m02 = other.m02;
            m10 = other.m10;
            m11 = other.m11;
            m12 = other.m12;
            return this;
        }

        /** Copies the values from the provided matrix to this matrix.
         * @param matrix The matrix to copy, assumed to be an affine transformation.
         * @return This matrix for the purposes of chaining. */
        public FPAffine2 set(FPMatrix3x3 matrix)
        {
            m00 = matrix.m00;
            m01 = matrix.m01;
            m02 = matrix.m02;
            m10 = matrix.m10;
            m11 = matrix.m11;
            m12 = matrix.m12;
            return this;
        }

        /** Copies the 2D transformation components from the provided 4x4 matrix. The values are mapped as follows:
         *
         * <pre>
         *      [  M00  M01  M03  ]
         *      [  M10  M11  M13  ]
         *      [   0    0    1   ]
         * </pre>
         *
         * @param matrix The source matrix, assumed to be an affine transformation within XY plane. This matrix will not be modified.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 set(FPMatrix4x4 matrix)
        {
            m00 = matrix.m00;
            m01 = matrix.m01;
            m02 = matrix.m03;
            m10 = matrix.m10;
            m11 = matrix.m11;
            m12 = matrix.m13;
            return this;
        }

        /** Sets this matrix to a translation matrix.
         * @param x The translation in x
         * @param y The translation in y
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToTranslation(FP x, FP y)
        {
            m00 = 1;
            m01 = 0;
            m02 = x;
            m10 = 0;
            m11 = 1;
            m12 = y;
            return this;
        }

        /** Sets this matrix to a translation matrix.
         * @param trn The translation vector.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToTranslation(FPVector2 trn)
        {
            return setToTranslation(trn.x, trn.y);
        }

        /** Sets this matrix to a scaling matrix.
         * @param scaleX The scale in x.
         * @param scaleY The scale in y.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToScaling(FP scaleX, FP scaleY)
        {
            m00 = scaleX;
            m01 = 0;
            m02 = 0;
            m10 = 0;
            m11 = scaleY;
            m12 = 0;
            return this;
        }

        /** Sets this matrix to a scaling matrix.
         * @param scale The scale vector.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToScaling(FPVector2 scale)
        {
            return setToScaling(scale.x, scale.y);
        }

        /** Sets this matrix to a rotation matrix that will rotate any vector in counter-clockwise direction around the z-axis.
         * @param degrees The angle in degrees.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToRotation(FP degrees)
        {
            FP cos = FPMath.CosDeg(degrees);
            FP sin = FPMath.SinDeg(degrees);

            m00 = cos;
            m01 = -sin;
            m02 = 0;
            m10 = sin;
            m11 = cos;
            m12 = 0;
            return this;
        }

        /** Sets this matrix to a rotation matrix that will rotate any vector in counter-clockwise direction around the z-axis.
         * @param radians The angle in radians.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToRotationRad(FP radians)
        {
            FP cos = FPMath.Cos(radians);
            FP sin = FPMath.Sin(radians);

            m00 = cos;
            m01 = -sin;
            m02 = 0;
            m10 = sin;
            m11 = cos;
            m12 = 0;
            return this;
        }

        /** Sets this matrix to a rotation matrix that will rotate any vector in counter-clockwise direction around the z-axis.
         * @param cos The angle cosine.
         * @param sin The angle sine.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToRotation(FP cos, FP sin)
        {
            m00 = cos;
            m01 = -sin;
            m02 = 0;
            m10 = sin;
            m11 = cos;
            m12 = 0;
            return this;
        }

        /** Sets this matrix to a shearing matrix.
         * @param shearX The shear in x direction.
         * @param shearY The shear in y direction.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToShearing(FP shearX, FP shearY)
        {
            m00 = 1;
            m01 = shearX;
            m02 = 0;
            m10 = shearY;
            m11 = 1;
            m12 = 0;
            return this;
        }

        /** Sets this matrix to a shearing matrix.
         * @param shear The shear vector.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToShearing(FPVector2 shear)
        {
            return setToShearing(shear.x, shear.y);
        }

        /** Sets this matrix to a concatenation of translation, rotation and scale. It is a more efficient form for:
         * <code>idt().translate(x, y).rotate(degrees).scale(scaleX, scaleY)</code>
         * @param x The translation in x.
         * @param y The translation in y.
         * @param degrees The angle in degrees.
         * @param scaleX The scale in y.
         * @param scaleY The scale in x.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToTrnRotScl(FP x, FP y, FP degrees, FP scaleX, FP scaleY)
        {
            m02 = x;
            m12 = y;

            if (degrees == 0)
            {
                m00 = scaleX;
                m01 = 0;
                m10 = 0;
                m11 = scaleY;
            }
            else
            {
                FP sin = FPMath.SinDeg(degrees);
                FP cos = FPMath.CosDeg(degrees);

                m00 = cos * scaleX;
                m01 = -sin * scaleY;
                m10 = sin * scaleX;
                m11 = cos * scaleY;
            }

            return this;
        }

        /** Sets this matrix to a concatenation of translation, rotation and scale. It is a more efficient form for:
         * <code>idt().translate(trn).rotate(degrees).scale(scale)</code>
         * @param trn The translation vector.
         * @param degrees The angle in degrees.
         * @param scale The scale vector.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToTrnRotScl(FPVector2 trn, FP degrees, FPVector2 scale)
        {
            return setToTrnRotScl(trn.x, trn.y, degrees, scale.x, scale.y);
        }

        /** Sets this matrix to a concatenation of translation, rotation and scale. It is a more efficient form for:
         * <code>idt().translate(x, y).rotateRad(radians).scale(scaleX, scaleY)</code>
         * @param x The translation in x.
         * @param y The translation in y.
         * @param radians The angle in radians.
         * @param scaleX The scale in y.
         * @param scaleY The scale in x.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToTrnRotRadScl(FP x, FP y, FP radians, FP scaleX, FP scaleY)
        {
            m02 = x;
            m12 = y;

            if (radians == 0)
            {
                m00 = scaleX;
                m01 = 0;
                m10 = 0;
                m11 = scaleY;
            }
            else
            {
                FP sin = FPMath.Sin(radians);
                FP cos = FPMath.Cos(radians);

                m00 = cos * scaleX;
                m01 = -sin * scaleY;
                m10 = sin * scaleX;
                m11 = cos * scaleY;
            }

            return this;
        }

        /** Sets this matrix to a concatenation of translation, rotation and scale. It is a more efficient form for:
         * <code>idt().translate(trn).rotateRad(radians).scale(scale)</code>
         * @param trn The translation vector.
         * @param radians The angle in radians.
         * @param scale The scale vector.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToTrnRotRadScl(FPVector2 trn, FP radians, FPVector2 scale)
        {
            return setToTrnRotRadScl(trn.x, trn.y, radians, scale.x, scale.y);
        }

        /** Sets this matrix to a concatenation of translation and scale. It is a more efficient form for:
         * <code>idt().translate(x, y).scale(scaleX, scaleY)</code>
         * @param x The translation in x.
         * @param y The translation in y.
         * @param scaleX The scale in y.
         * @param scaleY The scale in x.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToTrnScl(FP x, FP y, FP scaleX, FP scaleY)
        {
            m00 = scaleX;
            m01 = 0;
            m02 = x;
            m10 = 0;
            m11 = scaleY;
            m12 = y;
            return this;
        }

        /** Sets this matrix to a concatenation of translation and scale. It is a more efficient form for:
         * <code>idt().translate(trn).scale(scale)</code>
         * @param trn The translation vector.
         * @param scale The scale vector.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToTrnScl(FPVector2 trn, FPVector2 scale)
        {
            return setToTrnScl(trn.x, trn.y, scale.x, scale.y);
        }

        /** Sets this matrix to the product of two matrices.
         * @param l Left matrix.
         * @param r Right matrix.
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 setToProduct(FPAffine2 l, FPAffine2 r)
        {
            m00 = l.m00 * r.m00 + l.m01 * r.m10;
            m01 = l.m00 * r.m01 + l.m01 * r.m11;
            m02 = l.m00 * r.m02 + l.m01 * r.m12 + l.m02;
            m10 = l.m10 * r.m00 + l.m11 * r.m10;
            m11 = l.m10 * r.m01 + l.m11 * r.m11;
            m12 = l.m10 * r.m02 + l.m11 * r.m12 + l.m12;
            return this;
        }

        /** Inverts this matrix given that the determinant is != 0.
         * @return This matrix for the purpose of chaining operations.
         * @throws GdxRuntimeException if the matrix is singular (not invertible) */
        public FPAffine2 inv()
        {
            FP det = this.det();
            if (det == 0) throw new Exception("Can't invert a singular affine matrix");

            FP invDet = 1.0f / det;

            FP tmp00 = m11;
            FP tmp01 = -m01;
            FP tmp02 = m01 * m12 - m11 * m02;
            FP tmp10 = -m10;
            FP tmp11 = m00;
            FP tmp12 = m10 * m02 - m00 * m12;

            m00 = invDet * tmp00;
            m01 = invDet * tmp01;
            m02 = invDet * tmp02;
            m10 = invDet * tmp10;
            m11 = invDet * tmp11;
            m12 = invDet * tmp12;
            return this;
        }

        /** Postmultiplies this matrix with the provided matrix and stores the result in this matrix. For example:
         *
         * <pre>
         * A.mul(B) results in A := AB
         * </pre>
         *
         * @param other Matrix to multiply by.
         * @return This matrix for the purpose of chaining operations together. */
        public FPAffine2 mul(FPAffine2 other)
        {
            FP tmp00 = m00 * other.m00 + m01 * other.m10;
            FP tmp01 = m00 * other.m01 + m01 * other.m11;
            FP tmp02 = m00 * other.m02 + m01 * other.m12 + m02;
            FP tmp10 = m10 * other.m00 + m11 * other.m10;
            FP tmp11 = m10 * other.m01 + m11 * other.m11;
            FP tmp12 = m10 * other.m02 + m11 * other.m12 + m12;

            m00 = tmp00;
            m01 = tmp01;
            m02 = tmp02;
            m10 = tmp10;
            m11 = tmp11;
            m12 = tmp12;
            return this;
        }

        /** Premultiplies this matrix with the provided matrix and stores the result in this matrix. For example:
         *
         * <pre>
         * A.preMul(B) results in A := BA
         * </pre>
         *
         * @param other The other Matrix to multiply by
         * @return This matrix for the purpose of chaining operations. */
        public FPAffine2 preMul(FPAffine2 other)
        {
            FP tmp00 = other.m00 * m00 + other.m01 * m10;
            FP tmp01 = other.m00 * m01 + other.m01 * m11;
            FP tmp02 = other.m00 * m02 + other.m01 * m12 + other.m02;
            FP tmp10 = other.m10 * m00 + other.m11 * m10;
            FP tmp11 = other.m10 * m01 + other.m11 * m11;
            FP tmp12 = other.m10 * m02 + other.m11 * m12 + other.m12;

            m00 = tmp00;
            m01 = tmp01;
            m02 = tmp02;
            m10 = tmp10;
            m11 = tmp11;
            m12 = tmp12;
            return this;
        }

        /** Postmultiplies this matrix by a translation matrix.
         * @param x The x-component of the translation vector.
         * @param y The y-component of the translation vector.
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 translate(FP x, FP y)
        {
            m02 += m00 * x + m01 * y;
            m12 += m10 * x + m11 * y;
            return this;
        }

        /** Postmultiplies this matrix by a translation matrix.
         * @param trn The translation vector.
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 translate(FPVector2 trn)
        {
            return translate(trn.x, trn.y);
        }

        /** Premultiplies this matrix by a translation matrix.
         * @param x The x-component of the translation vector.
         * @param y The y-component of the translation vector.
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 preTranslate(FP x, FP y)
        {
            m02 += x;
            m12 += y;
            return this;
        }

        /** Premultiplies this matrix by a translation matrix.
         * @param trn The translation vector.
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 preTranslate(FPVector2 trn)
        {
            return preTranslate(trn.x, trn.y);
        }

        /** Postmultiplies this matrix with a scale matrix.
         * @param scaleX The scale in the x-axis.
         * @param scaleY The scale in the y-axis.
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 scale(FP scaleX, FP scaleY)
        {
            m00 *= scaleX;
            m01 *= scaleY;
            m10 *= scaleX;
            m11 *= scaleY;
            return this;
        }

        /** Postmultiplies this matrix with a scale matrix.
         * @param scale The scale vector.
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 scale(FPVector2 scale)
        {
            return this.scale(scale.x, scale.y);
        }

        /** Premultiplies this matrix with a scale matrix.
         * @param scaleX The scale in the x-axis.
         * @param scaleY The scale in the y-axis.
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 preScale(FP scaleX, FP scaleY)
        {
            m00 *= scaleX;
            m01 *= scaleX;
            m02 *= scaleX;
            m10 *= scaleY;
            m11 *= scaleY;
            m12 *= scaleY;
            return this;
        }

        /** Premultiplies this matrix with a scale matrix.
         * @param scale The scale vector.
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 preScale(FPVector2 scale)
        {
            return preScale(scale.x, scale.y);
        }

        /** Postmultiplies this matrix with a (counter-clockwise) rotation matrix.
         * @param degrees The angle in degrees
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 rotate(FP degrees)
        {
            if (degrees == 0) return this;

            FP cos = FPMath.CosDeg(degrees);
            FP sin = FPMath.SinDeg(degrees);

            FP tmp00 = m00 * cos + m01 * sin;
            FP tmp01 = m00 * -sin + m01 * cos;
            FP tmp10 = m10 * cos + m11 * sin;
            FP tmp11 = m10 * -sin + m11 * cos;

            m00 = tmp00;
            m01 = tmp01;
            m10 = tmp10;
            m11 = tmp11;
            return this;
        }

        /** Postmultiplies this matrix with a (counter-clockwise) rotation matrix.
         * @param radians The angle in radians
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 rotateRad(FP radians)
        {
            if (radians == 0) return this;

            FP cos = FPMath.Cos(radians);
            FP sin = FPMath.Sin(radians);

            FP tmp00 = m00 * cos + m01 * sin;
            FP tmp01 = m00 * -sin + m01 * cos;
            FP tmp10 = m10 * cos + m11 * sin;
            FP tmp11 = m10 * -sin + m11 * cos;

            m00 = tmp00;
            m01 = tmp01;
            m10 = tmp10;
            m11 = tmp11;
            return this;
        }

        /** Premultiplies this matrix with a (counter-clockwise) rotation matrix.
         * @param degrees The angle in degrees
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 preRotate(FP degrees)
        {
            if (degrees == 0) return this;

            FP cos = FPMath.CosDeg(degrees);
            FP sin = FPMath.SinDeg(degrees);

            FP tmp00 = cos * m00 - sin * m10;
            FP tmp01 = cos * m01 - sin * m11;
            FP tmp02 = cos * m02 - sin * m12;
            FP tmp10 = sin * m00 + cos * m10;
            FP tmp11 = sin * m01 + cos * m11;
            FP tmp12 = sin * m02 + cos * m12;

            m00 = tmp00;
            m01 = tmp01;
            m02 = tmp02;
            m10 = tmp10;
            m11 = tmp11;
            m12 = tmp12;
            return this;
        }

        /** Premultiplies this matrix with a (counter-clockwise) rotation matrix.
         * @param radians The angle in radians
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 preRotateRad(FP radians)
        {
            if (radians == 0) return this;

            FP cos = FPMath.Cos(radians);
            FP sin = FPMath.Sin(radians);

            FP tmp00 = cos * m00 - sin * m10;
            FP tmp01 = cos * m01 - sin * m11;
            FP tmp02 = cos * m02 - sin * m12;
            FP tmp10 = sin * m00 + cos * m10;
            FP tmp11 = sin * m01 + cos * m11;
            FP tmp12 = sin * m02 + cos * m12;

            m00 = tmp00;
            m01 = tmp01;
            m02 = tmp02;
            m10 = tmp10;
            m11 = tmp11;
            m12 = tmp12;
            return this;
        }

        /** Postmultiplies this matrix by a shear matrix.
         * @param shearX The shear in x direction.
         * @param shearY The shear in y direction.
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 shear(FP shearX, FP shearY)
        {
            FP tmp0 = m00 + shearY * m01;
            FP tmp1 = m01 + shearX * m00;
            m00 = tmp0;
            m01 = tmp1;

            tmp0 = m10 + shearY * m11;
            tmp1 = m11 + shearX * m10;
            m10 = tmp0;
            m11 = tmp1;
            return this;
        }

        /** Postmultiplies this matrix by a shear matrix.
         * @param shear The shear vector.
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 shear(FPVector2 shear)
        {
            return this.shear(shear.x, shear.y);
        }

        /** Premultiplies this matrix by a shear matrix.
         * @param shearX The shear in x direction.
         * @param shearY The shear in y direction.
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 preShear(FP shearX, FP shearY)
        {
            FP tmp00 = m00 + shearX * m10;
            FP tmp01 = m01 + shearX * m11;
            FP tmp02 = m02 + shearX * m12;
            FP tmp10 = m10 + shearY * m00;
            FP tmp11 = m11 + shearY * m01;
            FP tmp12 = m12 + shearY * m02;

            m00 = tmp00;
            m01 = tmp01;
            m02 = tmp02;
            m10 = tmp10;
            m11 = tmp11;
            m12 = tmp12;
            return this;
        }

        /** Premultiplies this matrix by a shear matrix.
         * @param shear The shear vector.
         * @return This matrix for the purpose of chaining. */
        public FPAffine2 preShear(FPVector2 shear)
        {
            return preShear(shear.x, shear.y);
        }

        /** Calculates the determinant of the matrix.
         * @return The determinant of this matrix. */
        public FP det()
        {
            return m00 * m11 - m01 * m10;
        }

        /** Get the x-y translation component of the matrix.
         * @param position Output vector.
         * @return Filled position. */
        public FPVector2 getTranslation(FPVector2 position)
        {
            position.x = m02;
            position.y = m12;
            return position;
        }

        /** Check if the this is a plain translation matrix.
         * @return True if scale is 1 and rotation is 0. */
        public bool isTranslation()
        {
            return (m00 == 1 && m11 == 1 && m01 == 0 && m10 == 0);
        }

        /** Check if this is an indentity matrix.
         * @return True if scale is 1 and rotation is 0. */
        public bool isIdt()
        {
            return (m00 == 1 && m02 == 0 && m12 == 0 && m11 == 1 && m01 == 0 && m10 == 0);
        }

        /** Applies the affine transformation on a vector. */
        public void applyTo(ref FPVector2 point)
        {
            FP x = point.x;
            FP y = point.y;
            point.x = m00 * x + m01 * y + m02;
            point.y = m10 * x + m11 * y + m12;
        }

        public override string ToString()
        {
            return "[" + m00 + "|" + m01 + "|" + m02 + "]\n[" + m10 + "|" + m11 + "|" + m12 + "]\n[0|0|1]";
        }
    }
}