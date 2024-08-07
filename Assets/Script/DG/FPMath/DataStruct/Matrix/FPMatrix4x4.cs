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
#if UNITY_STANDALONE
using UnityEngine;

#endif

namespace DG
{
    /// <summary>
    /// github:Bepu
    /// System.Numerics.Matrix4x4 是右手坐标系
    /// DGMatrix4xt是左手坐标系，与Unity的Matrix4x4相同，同为左手坐标系
    /// </summary>
    public partial struct FPMatrix4x4
    {
        /// <summary>
        /// Value at row 1, column 1 of the matrix.
        /// </summary>
        public FP sm11
        {
            get => m00;
            set => m00 = value;
        }

        /// <summary>
        /// Value at row 1, column 2 of the matrix.
        /// </summary>
        public FP sm12
        {
            get => m01;
            set => m01 = value;
        }

        /// <summary>
        /// Value at row 1, column 3 of the matrix.
        /// </summary>
        public FP sm13
        {
            get => m02;
            set => m02 = value;
        }

        /// <summary>
        /// Value at row 1, column 4 of the matrix.
        /// </summary>
        public FP sm14
        {
            get => m03;
            set => m03 = value;
        }

        /// <summary>
        /// Value at row 2, column 1 of the matrix.
        /// </summary>
        public FP sm21
        {
            get => m10;
            set => m10 = value;
        }

        /// <summary>
        /// Value at row 2, column 2 of the matrix.
        /// </summary>
        public FP sm22
        {
            get => m11;
            set => m11 = value;
        }

        /// <summary>
        /// Value at row 2, column 3 of the matrix.
        /// </summary>
        public FP sm23
        {
            get => m12;
            set => m12 = value;
        }

        /// <summary>
        /// Value at row 2, column 4 of the matrix.
        /// </summary>
        public FP sm24
        {
            get => m13;
            set => m13 = value;
        }

        /// <summary>
        /// Value at row 3, column 1 of the matrix.
        /// </summary>
        public FP sm31
        {
            get => m20;
            set => m20 = value;
        }

        /// <summary>
        /// Value at row 3, column 2 of the matrix.
        /// </summary>
        public FP sm32
        {
            get => m21;
            set => m21 = value;
        }

        /// <summary>
        /// Value at row 3, column 3 of the matrix.
        /// </summary>
        public FP sm33
        {
            get => m22;
            set => m22 = value;
        }

        /// <summary>
        /// Value at row 3, column 4 of the matrix.
        /// </summary>
        public FP sm34
        {
            get => m23;
            set => m23 = value;
        }

        /// <summary>
        /// Value at row 4, column 1 of the matrix.
        /// </summary>
        public FP sm41
        {
            get => m30;
            set => m30 = value;
        }

        /// <summary>
        /// Value at row 4, column 2 of the matrix.
        /// </summary>
        public FP sm42
        {
            get => m31;
            set => m31 = value;
        }

        /// <summary>
        /// Value at row 4, column 3 of the matrix.
        /// </summary>
        public FP sm43
        {
            get => m32;
            set => m32 = value;
        }

        /// <summary>
        /// Value at row 4, column 4 of the matrix.
        /// </summary>
        public FP sm44
        {
            get => m33;
            set => m33 = value;
        }


        /// <summary>
        /// Gets the 4x4 identity matrix.
        /// </summary>
        public static FPMatrix4x4 Identity => default2;

        /// <summary>
        /// Gets or sets the translation component of the transform.
        /// </summary>
        public FPVector3 Translation
        {
            get => new()
            {
                x = sm14,
                y = sm24,
                z = sm34
            };
            set
            {
                sm14 = value.x;
                sm24 = value.y;
                sm34 = value.z;
            }
        }


        /// <summary>
        /// Gets or sets the backward vector of the matrix.
        /// </summary>
        public FPVector3 Backward
        {
            get
            {
                var x = -sm13;
                var y = -sm23;
                var z = -sm33;
                return new FPVector3(x, y, z);
            }
            set
            {
                sm13 = -value.x;
                sm23 = -value.y;
                sm33 = -value.z;
            }
        }

        /// <summary>
        /// Gets or sets the down vector of the matrix.
        /// </summary>
        public FPVector3 Down
        {
            get
            {
                var x = -sm12;
                var y = -sm22;
                var z = -sm32;
                return new FPVector3(x, y, z);
            }
            set
            {
                sm12 = -value.x;
                sm22 = -value.y;
                sm32 = -value.z;
            }
        }

        /// <summary>
        /// Gets or sets the forward vector of the matrix.
        /// </summary>
        public FPVector3 Forward
        {
            get
            {
                var x = sm13;
                var y = sm23;
                var z = sm33;
                return new FPVector3(x, y, z);
            }
            set
            {
                sm13 = value.x;
                sm23 = value.y;
                sm33 = value.z;
            }
        }

        /// <summary>
        /// Gets or sets the left vector of the matrix.
        /// </summary>
        public FPVector3 Left
        {
            get
            {
                var x = -sm11;
                var y = -sm21;
                var z = -sm31;
                return new FPVector3(x, y, z);
            }
            set
            {
                sm11 = -value.x;
                sm21 = -value.y;
                sm31 = -value.z;
            }
        }

        /// <summary>
        /// Gets or sets the right vector of the matrix.
        /// </summary>
        public FPVector3 Right
        {
            get
            {
                var x = sm11;
                var y = sm21;
                var z = sm31;
                return new FPVector3(x, y, z);
            }
            set
            {
                sm11 = value.x;
                sm21 = value.y;
                sm31 = value.z;
            }
        }

        /// <summary>
        /// Gets or sets the up vector of the matrix.
        /// </summary>
        public FPVector3 Up
        {
            get
            {
                var x = sm12;
                var y = sm22;
                var z = sm32;
                return new FPVector3(x, y, z);
            }
            set
            {
                sm12 = value.x;
                sm22 = value.y;
                sm32 = value.z;
            }
        }


        public FPMatrix4x4 inverse => Invert(this);
        public FP determinant => Determinant();

        /// <summary>
        /// Constructs a new 4 row, 4 column matrix.
        /// </summary>
        /// <param name="sm11">Value at row 1, column 1 of the matrix.</param>
        /// <param name="sm12">Value at row 1, column 2 of the matrix.</param>
        /// <param name="sm13">Value at row 1, column 3 of the matrix.</param>
        /// <param name="sm14">Value at row 1, column 4 of the matrix.</param>
        /// <param name="sm21">Value at row 2, column 1 of the matrix.</param>
        /// <param name="sm22">Value at row 2, column 2 of the matrix.</param>
        /// <param name="sm23">Value at row 2, column 3 of the matrix.</param>
        /// <param name="sm24">Value at row 2, column 4 of the matrix.</param>
        /// <param name="sm31">Value at row 3, column 1 of the matrix.</param>
        /// <param name="sm32">Value at row 3, column 2 of the matrix.</param>
        /// <param name="sm33">Value at row 3, column 3 of the matrix.</param>
        /// <param name="sm34">Value at row 3, column 4 of the matrix.</param>
        /// <param name="sm41">Value at row 4, column 1 of the matrix.</param>
        /// <param name="sm42">Value at row 4, column 2 of the matrix.</param>
        /// <param name="sm43">Value at row 4, column 3 of the matrix.</param>
        /// <param name="sm44">Value at row 4, column 4 of the matrix.</param>
        public FPMatrix4x4(FP sm11, FP sm12, FP sm13, FP sm14,
            FP sm21, FP sm22, FP sm23, FP sm24,
            FP sm31, FP sm32, FP sm33, FP sm34,
            FP sm41, FP sm42, FP sm43, FP sm44)
        {
            m00 = sm11;
            m01 = sm12;
            m02 = sm13;
            m03 = sm14;


            m10 = sm21;
            m11 = sm22;
            m12 = sm23;
            m13 = sm24;

            m20 = sm31;
            m21 = sm32;
            m22 = sm33;
            m23 = sm34;

            m30 = sm41;
            m31 = sm42;
            m32 = sm43;
            m33 = sm44;
        }

#if UNITY_STANDALONE
        public FPMatrix4x4(Matrix4x4 matrix)
        {
            m00 = matrix.m00;
            m01 = matrix.m01;
            m02 = matrix.m02;
            m03 = matrix.m03;


            m10 = matrix.m10;
            m11 = matrix.m11;
            m12 = matrix.m12;
            m13 = matrix.m13;

            m20 = matrix.m20;
            m21 = matrix.m21;
            m22 = matrix.m22;
            m23 = matrix.m23;

            m30 = matrix.m30;
            m31 = matrix.m31;
            m32 = matrix.m32;
            m33 = matrix.m33;
        }
#endif

        public FPMatrix4x4(System.Numerics.Matrix4x4 matrix)
        {
            m00 = matrix.M11;
            m01 = matrix.M12;
            m02 = matrix.M13;
            m03 = matrix.M14;

            m10 = matrix.M21;
            m11 = matrix.M22;
            m12 = matrix.M23;
            m13 = matrix.M24;

            m20 = matrix.M31;
            m21 = matrix.M32;
            m22 = matrix.M33;
            m23 = matrix.M34;

            m30 = matrix.M41;
            m31 = matrix.M42;
            m32 = matrix.M43;
            m33 = matrix.M44;
        }

        /*************************************************************************************
         * 模块描述:算数运算符
         *************************************************************************************/
        /// <summary>
        /// Multiplies two matrices together.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <returns>Combined transformation.</returns>
        public static FPMatrix4x4 operator *(FPMatrix4x4 a, FPMatrix4x4 b)
        {
            return Multiply(a, b);
        }

        /// <summary>
        /// Scales all components of the matrix by the given value.
        /// </summary>
        /// <param name="m">First matrix to multiply.</param>
        /// <param name="f">Scaling value to apply to all components of the matrix.</param>
        /// <returns>Product of the multiplication.</returns>
        public static FPMatrix4x4 operator *(FPMatrix4x4 m, FP f)
        {
            return Multiply(m, f);
        }

        /// <summary>
        /// Scales all components of the matrix by the given value.
        /// </summary>
        /// <param name="m">First matrix to multiply.</param>
        /// <param name="f">Scaling value to apply to all components of the matrix.</param>
        /// <returns>Product of the multiplication.</returns>
        public static FPMatrix4x4 operator *(FP f, FPMatrix4x4 m)
        {
            return Multiply(m, f);
        }

        public static FPVector4 operator *(FPMatrix4x4 lhs, FPVector4 vector)
        {
            FPVector4 res;
            res.x = lhs.sm11 * vector.x + lhs.sm12 * vector.y + lhs.sm13 * vector.z + lhs.sm14 * vector.w;
            res.y = lhs.sm21 * vector.x + lhs.sm22 * vector.y + lhs.sm23 * vector.z + lhs.sm24 * vector.w;
            res.z = lhs.sm31 * vector.x + lhs.sm32 * vector.y + lhs.sm33 * vector.z + lhs.sm34 * vector.w;
            res.w = lhs.sm41 * vector.x + lhs.sm42 * vector.y + lhs.sm43 * vector.z + lhs.sm44 * vector.w;
            return res;
        }

        /*************************************************************************************
         * 模块描述:StaticUtil
         *************************************************************************************/
        public static FPMatrix4x4 TRS(FPVector3 pos, FPQuaternion rotation, FPVector3 scale)
        {
            return Translate(pos) * Rotate(rotation) * Scale(scale);
        }

        /// <summary>
        /// Linearly interpolates between the corresponding values of two matrices.
        /// </summary>
        /// <param name="matrix1">The first source matrix.</param>
        /// <param name="matrix2">The second source matrix.</param>
        /// <param name="amount">The relative weight of the second source matrix.</param>
        /// <returns>The interpolated matrix.</returns>
        public static FPMatrix4x4 Lerp(FPMatrix4x4 matrix1, FPMatrix4x4 matrix2, FP amount)
        {
            FPMatrix4x4 result = default2;

            // First row
            result.sm11 = matrix1.sm11 + (matrix2.sm11 - matrix1.sm11) * amount;
            result.sm12 = matrix1.sm12 + (matrix2.sm12 - matrix1.sm12) * amount;
            result.sm13 = matrix1.sm13 + (matrix2.sm13 - matrix1.sm13) * amount;
            result.sm14 = matrix1.sm14 + (matrix2.sm14 - matrix1.sm14) * amount;

            // Second row
            result.sm21 = matrix1.sm21 + (matrix2.sm21 - matrix1.sm21) * amount;
            result.sm22 = matrix1.sm22 + (matrix2.sm22 - matrix1.sm22) * amount;
            result.sm23 = matrix1.sm23 + (matrix2.sm23 - matrix1.sm23) * amount;
            result.sm24 = matrix1.sm24 + (matrix2.sm24 - matrix1.sm24) * amount;

            // Third row
            result.sm31 = matrix1.sm31 + (matrix2.sm31 - matrix1.sm31) * amount;
            result.sm32 = matrix1.sm32 + (matrix2.sm32 - matrix1.sm32) * amount;
            result.sm33 = matrix1.sm33 + (matrix2.sm33 - matrix1.sm33) * amount;
            result.sm34 = matrix1.sm34 + (matrix2.sm34 - matrix1.sm34) * amount;

            // Fourth row
            result.sm41 = matrix1.sm41 + (matrix2.sm41 - matrix1.sm41) * amount;
            result.sm42 = matrix1.sm42 + (matrix2.sm42 - matrix1.sm42) * amount;
            result.sm43 = matrix1.sm43 + (matrix2.sm43 - matrix1.sm43) * amount;
            result.sm44 = matrix1.sm44 + (matrix2.sm44 - matrix1.sm44) * amount;

            return result;
        }

        /// <summary>
        /// Creates a matrix representing the given axis and angle rotation.
        /// </summary>
        /// <param name="axis">Axis around which to rotate. need nomalized</param>
        /// <param name="angle">Angle to rotate around the axis.</param>
        /// <param name="result">Matrix created from the axis and angle.</param>MultiplyPoint3x4
        public static FPMatrix4x4 CreateFromAxisAngle(FPVector3 axis, FP angle)
        {
            return CreateFromAxisAngleRad(axis, angle * FPMath.DEG2RAD);
        }

        public static FPMatrix4x4 CreateFromAxisAngleRad(FPVector3 axis, FP radians)
        {
            FP xx = axis.x * axis.x;
            FP yy = axis.y * axis.y;
            FP zz = axis.z * axis.z;
            FP xy = axis.x * axis.y;
            FP xz = axis.x * axis.z;
            FP yz = axis.y * axis.z;

            FP sin = FPMath.Sin(radians);
            FP cos = FPMath.Cos(radians);

            FP oc = 1 - cos;

            FPMatrix4x4 result = default2;
            result.sm11 = oc * xx + cos;
            result.sm21 = oc * xy + axis.z * sin;
            result.sm31 = oc * xz - axis.y * sin;
            result.sm41 = 0;

            result.sm12 = oc * xy - axis.z * sin;
            result.sm22 = oc * yy + cos;
            result.sm32 = oc * yz + axis.x * sin;
            result.sm42 = 0;

            result.sm13 = oc * xz + axis.y * sin;
            result.sm23 = oc * yz - axis.x * sin;
            result.sm33 = oc * zz + cos;
            result.sm43 = 0;

            result.sm14 = 0;
            result.sm24 = 0;
            result.sm34 = 0;
            result.sm44 = 1;

            return result;
        }

        /// <summary>
        /// Creates a rotation matrix from a quaternion.
        /// </summary>
        /// <param name="quaternion">Quaternion to convert.</param>
        /// <param name="result">Rotation matrix created from the quaternion.</param>
        public static FPMatrix4x4 CreateFromQuaternion(FPQuaternion quaternion)
        {
            FP qX2 = quaternion.x + quaternion.x;
            FP qY2 = quaternion.y + quaternion.y;
            FP qZ2 = quaternion.z + quaternion.z;
            FP XX = qX2 * quaternion.x;
            FP YY = qY2 * quaternion.y;
            FP ZZ = qZ2 * quaternion.z;
            FP XY = qX2 * quaternion.y;
            FP XZ = qX2 * quaternion.z;
            FP XW = qX2 * quaternion.w;
            FP YZ = qY2 * quaternion.z;
            FP YW = qY2 * quaternion.w;
            FP ZW = qZ2 * quaternion.w;

            FPMatrix4x4 result = default2;

            result.sm11 = 1 - YY - ZZ;
            result.sm12 = XY - ZW;
            result.sm13 = XZ + YW;


            result.sm21 = XY + ZW;
            result.sm22 = 1 - XX - ZZ;
            result.sm23 = YZ - XW;

            result.sm31 = XZ - YW;
            result.sm32 = YZ + XW;
            result.sm33 = 1 - XX - YY;

            result.sm14 = 0;
            result.sm24 = 0;
            result.sm34 = 0;
            result.sm44 = 1;

            result.sm41 = 0;
            result.sm42 = 0;
            result.sm43 = 0;

            return result;
        }

        /// <summary>
        /// Multiplies two matrices together.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Combined transformation.</param>
        public static FPMatrix4x4 Multiply(FPMatrix4x4 a, FPMatrix4x4 b)
        {
            FP resultM11 = a.sm11 * b.sm11 + a.sm12 * b.sm21 + a.sm13 * b.sm31 + a.sm14 * b.sm41;
            FP resultM12 = a.sm11 * b.sm12 + a.sm12 * b.sm22 + a.sm13 * b.sm32 + a.sm14 * b.sm42;
            FP resultM13 = a.sm11 * b.sm13 + a.sm12 * b.sm23 + a.sm13 * b.sm33 + a.sm14 * b.sm43;
            FP resultM14 = a.sm11 * b.sm14 + a.sm12 * b.sm24 + a.sm13 * b.sm34 + a.sm14 * b.sm44;

            FP resultM21 = a.sm21 * b.sm11 + a.sm22 * b.sm21 + a.sm23 * b.sm31 + a.sm24 * b.sm41;
            FP resultM22 = a.sm21 * b.sm12 + a.sm22 * b.sm22 + a.sm23 * b.sm32 + a.sm24 * b.sm42;
            FP resultM23 = a.sm21 * b.sm13 + a.sm22 * b.sm23 + a.sm23 * b.sm33 + a.sm24 * b.sm43;
            FP resultM24 = a.sm21 * b.sm14 + a.sm22 * b.sm24 + a.sm23 * b.sm34 + a.sm24 * b.sm44;

            FP resultM31 = a.sm31 * b.sm11 + a.sm32 * b.sm21 + a.sm33 * b.sm31 + a.sm34 * b.sm41;
            FP resultM32 = a.sm31 * b.sm12 + a.sm32 * b.sm22 + a.sm33 * b.sm32 + a.sm34 * b.sm42;
            FP resultM33 = a.sm31 * b.sm13 + a.sm32 * b.sm23 + a.sm33 * b.sm33 + a.sm34 * b.sm43;
            FP resultM34 = a.sm31 * b.sm14 + a.sm32 * b.sm24 + a.sm33 * b.sm34 + a.sm34 * b.sm44;

            FP resultM41 = a.sm41 * b.sm11 + a.sm42 * b.sm21 + a.sm43 * b.sm31 + a.sm44 * b.sm41;
            FP resultM42 = a.sm41 * b.sm12 + a.sm42 * b.sm22 + a.sm43 * b.sm32 + a.sm44 * b.sm42;
            FP resultM43 = a.sm41 * b.sm13 + a.sm42 * b.sm23 + a.sm43 * b.sm33 + a.sm44 * b.sm43;
            FP resultM44 = a.sm41 * b.sm14 + a.sm42 * b.sm24 + a.sm43 * b.sm34 + a.sm44 * b.sm44;

            FPMatrix4x4 result = default2;
            result.sm11 = resultM11;
            result.sm12 = resultM12;
            result.sm13 = resultM13;
            result.sm14 = resultM14;

            result.sm21 = resultM21;
            result.sm22 = resultM22;
            result.sm23 = resultM23;
            result.sm24 = resultM24;

            result.sm31 = resultM31;
            result.sm32 = resultM32;
            result.sm33 = resultM33;
            result.sm34 = resultM34;

            result.sm41 = resultM41;
            result.sm42 = resultM42;
            result.sm43 = resultM43;
            result.sm44 = resultM44;
            return result;
        }


        /// <summary>
        /// Scales all components of the matrix.
        /// </summary>
        /// <param name="matrix">Matrix to scale.</param>
        /// <param name="scale">Amount to scale.</param>
        /// <param name="result">Scaled matrix.</param>
        public static FPMatrix4x4 Multiply(FPMatrix4x4 matrix, FP scale)
        {
            FPMatrix4x4 result = default2;

            result.sm11 = matrix.sm11 * scale;
            result.sm12 = matrix.sm12 * scale;
            result.sm13 = matrix.sm13 * scale;
            result.sm14 = matrix.sm14 * scale;

            result.sm21 = matrix.sm21 * scale;
            result.sm22 = matrix.sm22 * scale;
            result.sm23 = matrix.sm23 * scale;
            result.sm24 = matrix.sm24 * scale;

            result.sm31 = matrix.sm31 * scale;
            result.sm32 = matrix.sm32 * scale;
            result.sm33 = matrix.sm33 * scale;
            result.sm34 = matrix.sm34 * scale;

            result.sm41 = matrix.sm41 * scale;
            result.sm42 = matrix.sm42 * scale;
            result.sm43 = matrix.sm43 * scale;
            result.sm44 = matrix.sm44 * scale;

            return result;
        }


        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static FPVector4 Transform(FPVector4 v, FPMatrix4x4 matrix)
        {
            FP vX = v.x;
            FP vY = v.y;
            FP vZ = v.z;
            FP vW = v.w;
            FPVector4 result = default;
            result.x = vX * matrix.sm11 + vY * matrix.sm21 + vZ * matrix.sm31 + vW * matrix.sm41;
            result.y = vX * matrix.sm12 + vY * matrix.sm22 + vZ * matrix.sm32 + vW * matrix.sm42;
            result.z = vX * matrix.sm13 + vY * matrix.sm23 + vZ * matrix.sm33 + vW * matrix.sm43;
            result.w = vX * matrix.sm14 + vY * matrix.sm24 + vZ * matrix.sm34 + vW * matrix.sm44;
            return result;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static FPVector4 TransformTranspose(FPVector4 v, FPMatrix4x4 matrix)
        {
            FP vX = v.x;
            FP vY = v.y;
            FP vZ = v.z;
            FP vW = v.w;
            FPVector4 result = default;
            result.x = vX * matrix.sm11 + vY * matrix.sm21 + vZ * matrix.sm31 + vW * matrix.sm41;
            result.y = vX * matrix.sm12 + vY * matrix.sm22 + vZ * matrix.sm32 + vW * matrix.sm42;
            result.z = vX * matrix.sm13 + vY * matrix.sm23 + vZ * matrix.sm33 + vW * matrix.sm43;
            result.w = vX * matrix.sm14 + vY * matrix.sm24 + vZ * matrix.sm34 + vW * matrix.sm44;
            return result;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static FPVector4 Transform(FPVector3 v, FPMatrix4x4 matrix)
        {
            FPVector4 result = default;
            result.x = v.x * matrix.sm11 + v.y * matrix.sm12 + v.z * matrix.sm13 + matrix.sm14;
            result.y = v.x * matrix.sm21 + v.y * matrix.sm22 + v.z * matrix.sm23 + matrix.sm24;
            result.z = v.x * matrix.sm31 + v.y * matrix.sm32 + v.z * matrix.sm33 + matrix.sm34;
            result.w = v.x * matrix.sm41 + v.y * matrix.sm42 + v.z * matrix.sm43 + matrix.sm44;
            return result;
        }


        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static FPVector3 TransformTranspose(FPVector3 v, FPMatrix4x4 matrix)
        {
            FP vX = v.x;
            FP vY = v.y;
            FP vZ = v.z;
            FPVector3 result = default;
            result.x = vX * matrix.sm11 + vY * matrix.sm12 + vZ * matrix.sm13 + matrix.sm14;
            result.y = vX * matrix.sm21 + vY * matrix.sm22 + vZ * matrix.sm23 + matrix.sm24;
            result.z = vX * matrix.sm31 + vY * matrix.sm32 + vZ * matrix.sm33 + matrix.sm34;
            return result;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static FPVector3 TransformNormal(FPVector3 v, FPMatrix4x4 matrix)
        {
            FP vX = v.x;
            FP vY = v.y;
            FP vZ = v.z;
            FPVector3 result = default;
            result.x = vX * matrix.sm11 + vY * matrix.sm12 + vZ * matrix.sm13;
            result.y = vX * matrix.sm21 + vY * matrix.sm22 + vZ * matrix.sm23;
            result.z = vX * matrix.sm31 + vY * matrix.sm32 + vZ * matrix.sm33;

            return result;
        }


        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static FPVector3 TransformNormalTranspose(FPVector3 v, FPMatrix4x4 matrix)
        {
            FP vX = v.x;
            FP vY = v.y;
            FP vZ = v.z;
            FPVector3 result = default;
            result.x = vX * matrix.sm11 + vY * matrix.sm21 + vZ * matrix.sm31;
            result.y = vX * matrix.sm12 + vY * matrix.sm22 + vZ * matrix.sm32;
            result.z = vX * matrix.sm13 + vY * matrix.sm23 + vZ * matrix.sm33;
            return result;
        }


        /// <summary>
        /// Transposes the matrix.
        /// </summary>
        /// <param name="m">Matrix to transpose.</param>
        /// <param name="transposed">Matrix to transpose.</param>
        public static FPMatrix4x4 Transpose(FPMatrix4x4 m)
        {
            FPMatrix4x4 transposed = default2;
            FP m01 = m.sm12;
            FP m02 = m.sm13;
            FP m03 = m.sm14;
            FP m12 = m.sm23;
            FP m13 = m.sm24;
            FP m23 = m.sm34;

            transposed.sm11 = m.sm11;
            transposed.sm12 = m.sm21;
            transposed.sm13 = m.sm31;
            transposed.sm14 = m.sm41;

            transposed.sm21 = m01;
            transposed.sm22 = m.sm22;
            transposed.sm23 = m.sm32;
            transposed.sm24 = m.sm42;

            transposed.sm31 = m02;
            transposed.sm32 = m12;
            transposed.sm33 = m.sm33;
            transposed.sm34 = m.sm43;

            transposed.sm41 = m03;
            transposed.sm42 = m13;
            transposed.sm43 = m23;
            transposed.sm44 = m.sm44;

            return transposed;
        }

        /// <summary>
        /// Inverts the matrix.
        /// </summary>
        /// <param name="m">Matrix to invert.</param>
        /// <param name="inverted">Inverted version of the matrix.</param>
        public static FPMatrix4x4 Invert(FPMatrix4x4 m)
        {
            FPMatrix4x4 inverted = default2;
            FPMatrix4x8.Invert(m, out inverted);
            return inverted;
        }


        /// <summary>
        /// Inverts the matrix using a process that only works for rigid transforms.
        /// </summary>
        /// <param name="m">Matrix to invert.</param>
        /// <param name="inverted">Inverted version of the matrix.</param>
        public static FPMatrix4x4 InvertRigid(FPMatrix4x4 m)
        {
            FPMatrix4x4 inverted = default2;
            //Invert (transpose) the upper left 3x3 rotation.
            FP intermediate = m.sm12;
            inverted.sm12 = m.sm21;
            inverted.sm21 = intermediate;

            intermediate = m.sm13;
            inverted.sm13 = m.sm31;
            inverted.sm31 = intermediate;

            intermediate = m.sm23;
            inverted.sm23 = m.sm32;
            inverted.sm32 = intermediate;

            inverted.sm11 = m.sm11;
            inverted.sm22 = m.sm22;
            inverted.sm33 = m.sm33;

            //Translation component
            var vX = m.sm41;
            var vY = m.sm42;
            var vZ = m.sm43;
            inverted.sm41 = -(vX * inverted.sm11 + vY * inverted.sm21 + vZ * inverted.sm31);
            inverted.sm42 = -(vX * inverted.sm12 + vY * inverted.sm22 + vZ * inverted.sm32);
            inverted.sm43 = -(vX * inverted.sm13 + vY * inverted.sm23 + vZ * inverted.sm33);

            //Last chunk.
            inverted.sm14 = 0;
            inverted.sm24 = 0;
            inverted.sm34 = 0;
            inverted.sm44 = 1;

            return inverted;
        }

        public static FPMatrix4x4 CreateRotationX(FP radians)
        {
            FPMatrix4x4 r = default2;
            FP cos = FPMath.Cos(radians);
            FP sin = FPMath.Sin(radians);
            r.sm11 = 1;
            r.sm21 = 0;
            r.sm31 = 0;
            r.sm41 = 0;
            r.sm12 = 0;
            r.sm22 = cos;
            r.sm32 = sin;
            r.sm42 = 0;
            r.sm13 = 0;
            r.sm23 = -sin;
            r.sm33 = cos;
            r.sm43 = 0;
            r.sm14 = 0;
            r.sm24 = 0;
            r.sm34 = 0;
            r.sm44 = 1;
            return r;
        }

        public static FPMatrix4x4 CreateRotationY(FP radians)
        {
            FPMatrix4x4 r = default2;
            FP cos = FPMath.Cos(radians);
            FP sin = FPMath.Sin(radians);
            r.sm11 = cos;
            r.sm21 = 0;
            r.sm31 = -sin;
            r.sm41 = 0;
            r.sm12 = 0;
            r.sm22 = 1;
            r.sm32 = 0;
            r.sm42 = 0;
            r.sm13 = sin;
            r.sm23 = 0;
            r.sm33 = cos;
            r.sm43 = 0;
            r.sm14 = 0;
            r.sm24 = 0;
            r.sm34 = 0;
            r.sm44 = 1;
            return r;
        }

        public static FPMatrix4x4 CreateRotationZ(FP radians)
        {
            FPMatrix4x4 r = default2;
            FP cos = FPMath.Cos(radians);
            FP sin = FPMath.Sin(radians);
            r.sm11 = cos;
            r.sm21 = sin;
            r.sm31 = 0;
            r.sm41 = 0;
            r.sm12 = -sin;
            r.sm22 = cos;
            r.sm32 = 0;
            r.sm42 = 0;
            r.sm13 = 0;
            r.sm23 = 0;
            r.sm33 = 1;
            r.sm43 = 0;
            r.sm14 = 0;
            r.sm24 = 0;
            r.sm34 = 0;
            r.sm44 = 1;
            return r;
        }

        // Angle of rotation, in radians. Angles are measured anti-clockwise when viewed from the rotation axis (positive side) toward the origin.
        public static FPMatrix4x4 CreateFromYawPitchRoll(FP yaw, FP pitch, FP roll)
        {
            FPQuaternion quaternion = FPQuaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
            FPMatrix4x4 r = CreateFromQuaternion(quaternion);
            return r;
        }

        // Axes must be pair-wise perpendicular and have unit length.
        public static FPMatrix4x4 CreateFromCartesianAxes(FPVector3 right, FPVector3 up, FPVector3 forward)
        {
            FPMatrix4x4 r = default2;
            r.sm11 = right.x;
            r.sm21 = right.y;
            r.sm31 = right.z;
            r.sm41 = 0;
            r.sm12 = up.x;
            r.sm22 = up.y;
            r.sm32 = up.z;
            r.sm42 = 0;
            r.sm13 = forward.x;
            r.sm23 = forward.y;
            r.sm33 = forward.z;
            r.sm43 = 0;
            r.sm14 = 0;
            r.sm24 = 0;
            r.sm34 = 0;
            r.sm44 = 1;
            return r;
        }

        /// <summary>
        /// Creates an orthographic perspective matrix from the given view volume dimensions.
        /// </summary>
        /// <param name="width">Width of the view volume.</param>
        /// <param name="height">Height of the view volume.</param>
        /// <param name="zNearPlane">Minimum Z-value of the view volume.</param>
        /// <param name="zFarPlane">Maximum Z-value of the view volume.</param>
        /// <returns>The orthographic projection matrix.</returns>
        public static FPMatrix4x4 CreateOrthographic(FP width, FP height, FP zNearPlane, FP zFarPlane)
        {
            var left = -width / 2;
            var right = width / 2;
            var bottom = -height / 2;
            var top = height / 2;

            return CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
        }

        /// <summary>
        /// Creates a right handed orthographic projection.
        /// </summary>
        /// <param name="left">Leftmost coordinate of the projected area.</param>
        /// <param name="right">Rightmost coordinate of the projected area.</param>
        /// <param name="bottom">Bottom coordinate of the projected area.</param>
        /// <param name="top">Top coordinate of the projected area.</param>
        /// <param name="zNear">Near plane of the projection.</param>
        /// <param name="zFar">Far plane of the projection.</param>
        /// <param name="projection">The resulting orthographic projection matrix.</param>
        public static FPMatrix4x4 CreateOrthographicOffCenter(FP left, FP right, FP bottom, FP top, FP zNear, FP zFar)
        {
            FPMatrix4x4 projection = default2;
            var xOrth = 2 / (right - left);
            var yOrth = 2 / (top - bottom);
            var zOrth = (-2) / (zFar - zNear);

            var tx = -(right + left) / (right - left);
            var ty = -(top + bottom) / (top - bottom);
            var tz = -(zFar + zNear) / (zFar - zNear);

            projection.sm11 = xOrth;
            projection.sm21 = 0;
            projection.sm31 = 0;
            projection.sm41 = 0;
            projection.sm12 = 0;
            projection.sm22 = yOrth;
            projection.sm32 = 0;
            projection.sm42 = 0;
            projection.sm13 = 0;
            projection.sm23 = 0;
            projection.sm33 = zOrth;
            projection.sm43 = 0;
            projection.sm14 = tx;
            projection.sm24 = ty;
            projection.sm34 = tz;
            projection.sm44 = 1;
            return projection;
        }

        /// <summary>
        /// Creates a perspective projection matrix from the given view volume dimensions.
        /// </summary>
        /// <param name="width">Width of the view volume at the near view plane.</param>
        /// <param name="height">Height of the view volume at the near view plane.</param>
        /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
        /// <param name="farPlaneDistance">Distance to the far view plane.</param>
        /// <returns>The perspective projection matrix.</returns>
        public static FPMatrix4x4 CreatePerspective(FP width, FP height, FP nearPlaneDistance, FP farPlaneDistance)
        {
            var left = -width / 2;
            var right = width / 2;
            var bottom = -height / 2;
            var top = height / 2;
            return CreatePerspectiveOffCenter(left, right, bottom, top, nearPlaneDistance, farPlaneDistance);
        }

        /// <summary>
        /// Creates a left-handed perspective matrix.
        /// </summary>
        /// <param name="fieldOfViewRadian">Field of view of the perspective in radians.</param>
        /// <param name="aspectRatio">Width of the viewport over the height of the viewport.</param>
        /// <param name="nearClip">Near clip plane of the perspective.</param>
        /// <param name="farClip">Far clip plane of the perspective.</param>
        /// <param name="perspective">Resulting perspective matrix.</param>
        public static FPMatrix4x4 CreatePerspectiveFieldOfViewRad(FP fieldOfViewRadian, FP aspectRatio, FP nearClip,
            FP farClip)
        {
            FPMatrix4x4 perspective = default2;
            perspective.SetIdentity();
            var fd = 1.0 / FPMath.Tan(fieldOfViewRadian / 2.0);
            var a1 = (farClip + nearClip) / (nearClip - farClip);
            var a2 = (2 * farClip * nearClip) / (nearClip - farClip);
            perspective.sm11 = fd / aspectRatio;
            perspective.sm21 = 0;
            perspective.sm31 = 0;
            perspective.sm41 = 0;
            perspective.sm12 = 0;
            perspective.sm22 = fd;
            perspective.sm32 = 0;
            perspective.sm42 = 0;
            perspective.sm13 = 0;
            perspective.sm23 = 0;
            perspective.sm33 = a1;
            perspective.sm43 = (-1);
            perspective.sm14 = 0;
            perspective.sm24 = 0;
            perspective.sm34 = a2;
            perspective.sm44 = 0;
            return perspective;
        }

        /// <summary>
        /// Creates a customized, perspective projection matrix.
        /// </summary>
        /// <param name="left">Minimum x-value of the view volume at the near view plane.</param>
        /// <param name="right">Maximum x-value of the view volume at the near view plane.</param>
        /// <param name="bottom">Minimum y-value of the view volume at the near view plane.</param>
        /// <param name="top">Maximum y-value of the view volume at the near view plane.</param>
        /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
        /// <param name="farPlaneDistance">Distance to of the far view plane.</param>
        /// <returns>The perspective projection matrix.</returns>
        public static FPMatrix4x4 CreatePerspectiveOffCenter(FP left, FP right, FP bottom, FP top, FP nearPlaneDistance,
            FP farPlaneDistance)
        {
            if (nearPlaneDistance <= 0.0f)
                throw new ArgumentOutOfRangeException("nearPlaneDistance");

            if (farPlaneDistance <= 0.0f)
                throw new ArgumentOutOfRangeException("farPlaneDistance");

            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException("nearPlaneDistance");

            FPMatrix4x4 result = default2;

            var x = 2.0f * nearPlaneDistance / (right - left);
            var y = 2.0f * nearPlaneDistance / (top - bottom);
            var a = (right + left) / (right - left);
            var b = (top + bottom) / (top - bottom);
            var a1 = (farPlaneDistance + nearPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            var a2 = (2 * farPlaneDistance * nearPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            result.sm11 = x;
            result.sm21 = 0;
            result.sm31 = 0;
            result.sm41 = 0;
            result.sm12 = 0;
            result.sm22 = y;
            result.sm32 = 0;
            result.sm42 = 0;
            result.sm13 = a;
            result.sm23 = b;
            result.sm33 = a1;
            result.sm43 = (-1);
            result.sm14 = 0;
            result.sm24 = 0;
            result.sm34 = a2;
            result.sm44 = 0;
            return result;
        }

        /// <summary>
        /// Creates a Matrix that reflects the coordinate system about a specified Plane.
        /// </summary>
        /// <param name="value">The Plane about which to create a reflection.</param>
        /// <returns>A new matrix expressing the reflection.</returns>
        public static FPMatrix4x4 CreateReflection(FPPlane value)
        {
            value = FPPlane.Normalize(value);

            FP a = value.normal.x;
            FP b = value.normal.y;
            FP c = value.normal.z;

            FP fa = (-2.0f) * a;
            FP fb = (-2.0f) * b;
            FP fc = (-2.0f) * c;

            FPMatrix4x4 result = default2;

            result.sm11 = fa * a + 1.0f;
            result.sm12 = fb * a;
            result.sm13 = fc * a;
            result.sm14 = 0.0f;

            result.sm21 = fa * b;
            result.sm22 = fb * b + 1.0f;
            result.sm23 = fc * b;
            result.sm24 = 0.0f;

            result.sm31 = fa * c;
            result.sm32 = fb * c;
            result.sm33 = fc * c + 1.0f;
            result.sm34 = 0.0f;

            result.sm41 = fa * value.d;
            result.sm42 = fb * value.d;
            result.sm43 = fc * value.d;
            result.sm44 = 1.0f;

            return result;
        }

        /// <summary>
        /// Creates a Matrix that flattens geometry into a specified Plane as if casting a shadow from a specified light source.
        /// </summary>
        /// <param name="lightDirection">The direction from which the light that will cast the shadow is coming.</param>
        /// <param name="plane">The Plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
        /// <returns>A new Matrix that can be used to flatten geometry onto the specified plane from the specified direction.</returns>
        public static FPMatrix4x4 CreateShadow(FPVector3 lightDirection, FPPlane plane)
        {
            FPPlane p = FPPlane.Normalize(plane);

            FP dot = p.normal.x * lightDirection.x + p.normal.y * lightDirection.y + p.normal.z * lightDirection.z;
            FP a = -p.normal.x;
            FP b = -p.normal.y;
            FP c = -p.normal.z;
            FP d = -p.d;

            FPMatrix4x4 result = default2;

            result.sm11 = a * lightDirection.x + dot;
            result.sm21 = b * lightDirection.x;
            result.sm31 = c * lightDirection.x;
            result.sm41 = d * lightDirection.x;

            result.sm12 = a * lightDirection.y;
            result.sm22 = b * lightDirection.y + dot;
            result.sm32 = c * lightDirection.y;
            result.sm42 = d * lightDirection.y;

            result.sm13 = a * lightDirection.z;
            result.sm23 = b * lightDirection.z;
            result.sm33 = c * lightDirection.z + dot;
            result.sm43 = d * lightDirection.z;

            result.sm14 = 0.0f;
            result.sm24 = 0.0f;
            result.sm34 = 0.0f;
            result.sm44 = dot;

            return result;
        }

        public static FPMatrix4x4 LookAt(FPVector3 position, FPVector3 target, FPVector3 upVector)
        {
            return TRS(position, FPQuaternion.LookRotation(target - position, upVector), FPVector3.one);
        }

        /// <summary>
        /// Creates a view matrix pointing from a position to a target with the given up vector.
        /// </summary>
        /// <param name="position">Position of the camera.</param>
        /// <param name="target">Target of the camera.</param>
        /// <param name="upVector">Up vector of the camera.</param>
        /// <param name="viewMatrix">Look at matrix.</param>
        public static FPMatrix4x4 CreateLookAt(FPVector3 position, FPVector3 target, FPVector3 upVector)
        {
            FPVector3 forward = target - position;
            return CreateViewRH(position, forward, upVector);
        }


        /// <summary>
        /// Creates a view matrix pointing in a direction with a given up vector.
        /// </summary>
        /// <param name="position">Position of the camera.</param>
        /// <param name="forward">Forward direction of the camera.</param>
        /// <param name="upVector">Up vector of the camera.</param>
        /// <param name="viewMatrix">Look at matrix.</param>
        public static FPMatrix4x4 CreateViewRH(FPVector3 position, FPVector3 forward, FPVector3 upVector)
        {
            FPVector3 z = forward.normalized;
            FPVector3 x = FPVector3.Cross(upVector, z).normalized;
            FPVector3 y = -FPVector3.Cross(x, z);
            FPMatrix4x4 viewMatrix = default2;
            viewMatrix.sm11 = x.x;
            viewMatrix.sm12 = y.x;
            viewMatrix.sm13 = z.x;
            viewMatrix.sm14 = 0;
            viewMatrix.sm21 = x.y;
            viewMatrix.sm22 = y.y;
            viewMatrix.sm23 = z.y;
            viewMatrix.sm24 = 0;
            viewMatrix.sm31 = x.z;
            viewMatrix.sm32 = y.z;
            viewMatrix.sm33 = z.z;
            viewMatrix.sm34 = 0;
            viewMatrix.sm41 = -FPVector3.Dot(x, position);
            viewMatrix.sm42 = -FPVector3.Dot(y, position);
            viewMatrix.sm43 = -FPVector3.Dot(z, position);
            viewMatrix.sm44 = 1;
            return viewMatrix;
        }


        /// <summary>
        /// Creates a world matrix pointing from a position to a target with the given up vector.
        /// </summary>
        /// <param name="position">Position of the transform.</param>
        /// <param name="forward">Forward direction of the transformation.</param>
        /// <param name="upVector">Up vector which is crossed against the forward vector to compute the transform's basis.</param>
        /// <param name="worldMatrix">World matrix.</param>
        public static FPMatrix4x4 CreateWorld(FPVector3 position, FPVector3 forward, FPVector3 upVector)
        {
            FPMatrix4x4 worldMatrix = default2;

            FPVector3 z = forward.normalized;
            FPVector3 x = FPVector3.Cross(upVector, z).normalized;
            FPVector3 y = -FPVector3.Cross(x, z);

            worldMatrix.sm11 = x.x;
            worldMatrix.sm21 = x.y;
            worldMatrix.sm31 = x.z;
            worldMatrix.sm41 = 0;

            worldMatrix.sm12 = y.x;
            worldMatrix.sm22 = y.y;
            worldMatrix.sm32 = y.z;
            worldMatrix.sm42 = 0;

            worldMatrix.sm13 = z.x;
            worldMatrix.sm23 = z.y;
            worldMatrix.sm33 = z.z;
            worldMatrix.sm43 = 0;

            worldMatrix.sm14 = position.x;
            worldMatrix.sm24 = position.y;
            worldMatrix.sm34 = position.z;
            worldMatrix.sm44 = 1;

            return worldMatrix;
        }


        /// <summary>
        /// Creates a matrix representing a translation.
        /// </summary>
        /// <param name="translation">Translation to be represented by the matrix.</param>
        /// <param name="translationMatrix">Matrix representing the given translation.</param>
        public static FPMatrix4x4 CreateTranslation(FPVector3 translation)
        {
            FPMatrix4x4 translationMatrix = default2;
            translationMatrix.sm11 = 1;
            translationMatrix.sm22 = 1;
            translationMatrix.sm33 = 1;
            translationMatrix.sm44 = 1;
            translationMatrix.sm14 = translation.x;
            translationMatrix.sm24 = translation.y;
            translationMatrix.sm34 = translation.z;

            return translationMatrix;
        }


        /// <summary>
        /// Creates a matrix representing the given axis aligned scale.
        /// </summary>
        /// <param name="scale">Scale to be represented by the matrix.</param>
        /// <param name="scaleMatrix">Matrix representing the given scale.</param>
        public static FPMatrix4x4 CreateScale(FPVector3 scale)
        {
            FPMatrix4x4 scaleMatrix = default2;
            scaleMatrix.sm11 = scale.x;
            scaleMatrix.sm22 = scale.y;
            scaleMatrix.sm33 = scale.z;
            scaleMatrix.sm44 = 1;
            return scaleMatrix;
        }

        /// <summary>
        /// Creates a matrix representing the given axis aligned scale.
        /// </summary>
        /// <param name="x">Scale along the x axis.</param>
        /// <param name="y">Scale along the y axis.</param>
        /// <param name="z">Scale along the z axis.</param>
        /// <param name="scaleMatrix">Matrix representing the given scale.</param>
        public static FPMatrix4x4 CreateScale(FP x, FP y, FP z)
        {
            FPMatrix4x4 scaleMatrix = default2;
            scaleMatrix.sm11 = x;
            scaleMatrix.sm22 = y;
            scaleMatrix.sm33 = z;
            scaleMatrix.sm44 = 1;
            return scaleMatrix;
        }

        public static FPMatrix4x4 Scale(FPVector3 scale)
        {
            return CreateScale(scale);
        }

        public static FPMatrix4x4 Translate(FPVector3 vector)
        {
            FPMatrix4x4 m = default2;
            m.sm11 = 1F;
            m.sm12 = 0F;
            m.sm13 = 0F;
            m.sm14 = vector.x;
            m.sm21 = 0F;
            m.sm22 = 1F;
            m.sm23 = 0F;
            m.sm24 = vector.y;
            m.sm31 = 0F;
            m.sm32 = 0F;
            m.sm33 = 1F;
            m.sm34 = vector.z;
            m.sm41 = 0F;
            m.sm42 = 0F;
            m.sm43 = 0F;
            m.sm44 = 1F;
            return m;
        }


        // Creates a rotation matrix. Note: Assumes unit quaternion
        public static FPMatrix4x4 Rotate(FPQuaternion q)
        {
            // Precalculate coordinate products
            FP x = q.x * 2.0F;
            FP y = q.y * 2.0F;
            FP z = q.z * 2.0F;
            FP xx = q.x * x;
            FP yy = q.y * y;
            FP zz = q.z * z;
            FP xy = q.x * y;
            FP xz = q.x * z;
            FP yz = q.y * z;
            FP wx = q.w * x;
            FP wy = q.w * y;
            FP wz = q.w * z;

            // Calculate 3x3 matrix from orthonormal basis
            FPMatrix4x4 m = default2;
            m.sm11 = 1.0f - (yy + zz);
            m.sm21 = xy + wz;
            m.sm31 = xz - wy;
            m.sm41 = 0.0F;
            m.sm12 = xy - wz;
            m.sm22 = 1.0f - (xx + zz);
            m.sm32 = yz + wx;
            m.sm42 = 0.0F;
            m.sm13 = xz + wy;
            m.sm23 = yz - wx;
            m.sm33 = 1.0f - (xx + yy);
            m.sm43 = 0.0F;
            m.sm14 = 0.0F;
            m.sm24 = 0.0F;
            m.sm34 = 0.0F;
            m.sm44 = 1.0F;
            return m;
        }

        /*************************************************************************************
         * 模块描述:Util
         *************************************************************************************/
        /// <summary>
        /// Creates a spherical billboard that rotates around a specified object position.
        /// </summary>
        /// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
        /// <param name="cameraPosition">Position of the camera.</param>
        /// <param name="cameraUpVector">The up vector of the camera.</param>
        /// <param name="cameraForwardVector">The forward vector of the camera.</param>
        /// <returns>The created billboard matrix</returns>
        public static FPMatrix4x4 CreateBillboard(FPVector3 objectPosition, FPVector3 cameraPosition,
            FPVector3 cameraUpVector, FPVector3 cameraForwardVector)
        {
            FPVector3 zaxis = new FPVector3(
                objectPosition.x - cameraPosition.x,
                objectPosition.y - cameraPosition.y,
                objectPosition.z - cameraPosition.z);

            FP norm = zaxis.sqrMagnitude;

            if (norm < FPMath.EPSILION)
                zaxis = -cameraForwardVector;
            else
                zaxis = zaxis * 1.0f / FPMath.Sqrt(norm);

            FPVector3 xaxis = FPVector3.Normalize(FPVector3.Cross(cameraUpVector, zaxis));

            FPVector3 yaxis = -FPVector3.Cross(xaxis, zaxis);

            FPMatrix4x4 result = default2;

            result.sm11 = xaxis.x;
            result.sm21 = xaxis.y;
            result.sm31 = xaxis.z;
            result.sm41 = 0.0f;
            result.sm12 = yaxis.x;
            result.sm22 = yaxis.y;
            result.sm32 = yaxis.z;
            result.sm42 = 0.0f;
            result.sm13 = zaxis.x;
            result.sm23 = zaxis.y;
            result.sm33 = zaxis.z;
            result.sm43 = 0.0f;

            result.sm14 = objectPosition.x;
            result.sm24 = objectPosition.y;
            result.sm34 = objectPosition.z;
            result.sm44 = 1.0f;

            return result;
        }

        /// <summary>
        /// Creates a cylindrical billboard that rotates around a specified axis.
        /// </summary>
        /// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
        /// <param name="cameraPosition">Position of the camera.</param>
        /// <param name="rotateAxis">Axis to rotate the billboard around.</param>
        /// <param name="cameraForwardVector">Forward vector of the camera.</param>
        /// <param name="objectForwardVector">Forward vector of the object.</param>
        /// <returns>The created billboard matrix.</returns>
        public static FPMatrix4x4 CreateConstrainedBillboard(FPVector3 objectPosition, FPVector3 cameraPosition,
            FPVector3 rotateAxis, FPVector3 cameraForwardVector, FPVector3 objectForwardVector)
        {
            FP minAngle = 1.0f - (0.1f * (FPMath.PI / 180.0f)); // 0.1 degrees

            // Treat the case when object and camera positions are too close.
            FPVector3 faceDir = new FPVector3(
                objectPosition.x - cameraPosition.x,
                objectPosition.y - cameraPosition.y,
                objectPosition.z - cameraPosition.z);

            FP norm = faceDir.sqrMagnitude;

            if (norm < FPMath.EPSILION)
                faceDir = -cameraForwardVector;
            else
                faceDir *= (1.0f / FPMath.Sqrt(norm));

            FPVector3 yaxis = rotateAxis;
            FPVector3 xaxis;
            FPVector3 zaxis;

            // Treat the case when angle between faceDir and rotateAxis is too close to 0.
            FP dot = FPVector3.Dot(rotateAxis, faceDir);

            if (FPMath.Abs(dot) > minAngle)
            {
                zaxis = objectForwardVector;

                // Make sure passed values are useful for compute.
                dot = FPVector3.Dot(rotateAxis, zaxis);

                if (FPMath.Abs(dot) > minAngle)
                {
                    zaxis = (FPMath.Abs(rotateAxis.z) > minAngle) ? new FPVector3(1, 0, 0) : new FPVector3(0, 0, -1);
                }

                xaxis = FPVector3.Normalize(FPVector3.Cross(rotateAxis, zaxis));
                zaxis = FPVector3.Normalize(FPVector3.Cross(xaxis, rotateAxis));
            }
            else
            {
                xaxis = FPVector3.Normalize(FPVector3.Cross(rotateAxis, faceDir));
                zaxis = FPVector3.Normalize(FPVector3.Cross(xaxis, yaxis));
            }

            FPMatrix4x4 result = default2;

            result.sm11 = xaxis.x;
            result.sm21 = xaxis.y;
            result.sm31 = xaxis.z;
            result.sm41 = 0.0f;
            result.sm12 = yaxis.x;
            result.sm22 = yaxis.y;
            result.sm32 = yaxis.z;
            result.sm42 = 0.0f;
            result.sm13 = zaxis.x;
            result.sm23 = zaxis.y;
            result.sm33 = zaxis.z;
            result.sm43 = 0.0f;

            result.sm14 = objectPosition.x;
            result.sm24 = objectPosition.y;
            result.sm34 = objectPosition.z;
            result.sm44 = 1.0f;

            return result;
        }

        // Transforms a position by this matrix, with a perspective divide. (generic)
        public FPVector3 MultiplyPoint(FPVector3 point)
        {
            FPVector3 res;
            FP w;
            res.x = sm11 * point.x + sm12 * point.y + sm13 * point.z + sm14;
            res.y = sm21 * point.x + sm22 * point.y + sm23 * point.z + sm24;
            res.z = sm31 * point.x + sm32 * point.y + sm33 * point.z + sm34;
            w = sm41 * point.x + sm42 * point.y + sm43 * point.z + sm44;

            w = 1F / w;
            res.x *= w;
            res.y *= w;
            res.z *= w;
            return res;
        }

        /// <summary>
        /// Computes the determinant of the matrix.
        /// </summary>
        /// <returns></returns>
        public FP Determinant()
        {
            //Compute the re-used 2x2 determinants.
            FP det1 = sm33 * sm44 - sm34 * sm43;
            FP det2 = sm32 * sm44 - sm34 * sm42;
            FP det3 = sm32 * sm43 - sm33 * sm42;
            FP det4 = sm31 * sm44 - sm34 * sm41;
            FP det5 = sm31 * sm43 - sm33 * sm41;
            FP det6 = sm31 * sm42 - sm32 * sm41;
            return
                (sm11 * ((sm22 * det1 - sm23 * det2) + sm24 * det3)) -
                (sm12 * ((sm21 * det1 - sm23 * det4) + sm24 * det5)) +
                (sm13 * ((sm21 * det2 - sm22 * det4) + sm24 * det6)) -
                (sm14 * ((sm21 * det3 - sm22 * det5) + sm23 * det6));
        }

        /// <summary>
        /// Transposes the matrix in-place.
        /// </summary>
        public void Transpose()
        {
            FP intermediate = sm12;
            sm12 = sm21;
            sm21 = intermediate;

            intermediate = sm13;
            sm13 = sm31;
            sm31 = intermediate;

            intermediate = sm14;
            sm14 = sm41;
            sm41 = intermediate;

            intermediate = sm23;
            sm23 = sm32;
            sm32 = intermediate;

            intermediate = sm24;
            sm24 = sm42;
            sm42 = intermediate;

            intermediate = sm34;
            sm34 = sm43;
            sm43 = intermediate;
        }


        // Transforms a position by this matrix, without a perspective divide. (fast)
        public FPVector3 MultiplyPoint3x4(FPVector3 point)
        {
            FPVector3 res = default;
            res.x = sm11 * point.x + sm12 * point.y + sm13 * point.z + sm14;
            res.y = sm21 * point.x + sm22 * point.y + sm23 * point.z + sm24;
            res.z = sm31 * point.x + sm32 * point.y + sm33 * point.z + sm34;


            return res;
        }

        // Transforms a direction by this matrix.
        public FPVector3 MultiplyVector(FPVector3 vector)
        {
            FPVector3 res = default;
            res.x = sm11 * vector.x + sm12 * vector.y + sm13 * vector.z;
            res.y = sm21 * vector.x + sm22 * vector.y + sm23 * vector.z;
            res.z = sm31 * vector.x + sm32 * vector.y + sm33 * vector.z;
            return res;
        }

        public void SetIdentity()
        {
            this = Identity;
        }
    }
}