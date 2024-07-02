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
	/// Encapsulates a <a href="http://en.wikipedia.org/wiki/Row-major_order#Column-major_order">column major</a> 4 by 4 matrix. Like
	/// the {@link Vector3} class it allows the chaining of methods by returning a reference to itself. For example:
	/// Matrix4 mat = new Matrix4().trn(position).mul(camera.combined);
	/// @author badlogicgames@gmail.com
	/// </summary>
	public partial struct FPMatrix4x4
	{
		/** XX: Typically the unrotated X component for scaling, also the cosine of the angle when rotated on the Y and/or Z axis. On
		 * Vector3 multiplication this value is multiplied with the source X component and added to the target X component. */
		public const int M00Index = 0;

		/** XY: Typically the negative sine of the angle when rotated on the Z axis. On Vector3 multiplication this value is multiplied
		 * with the source Y component and added to the target X component. */
		public const int M10Index = 1;


		/** XZ: Typically the sine of the angle when rotated on the Y axis. On Vector3 multiplication this value is multiplied with the
		 * source Z component and added to the target X component. */
		public const int M20Index = 2;


		/** XW: Typically the translation of the X component. On Vector3 multiplication this value is added to the target X
		 * component. */
		public const int M30Index = 3;


		/** YX: Typically the sine of the angle when rotated on the Z axis. On Vector3 multiplication this value is multiplied with the
		 * source X component and added to the target Y component. */
		public const int M01Index = 4;

		/** YY: Typically the unrotated Y component for scaling, also the cosine of the angle when rotated on the X and/or Z axis. On
		 * Vector3 multiplication this value is multiplied with the source Y component and added to the target Y component. */
		public const int M11Index = 5;

		/** YZ: Typically the negative sine of the angle when rotated on the X axis. On Vector3 multiplication this value is multiplied
		 * with the source Z component and added to the target Y component. */
		public const int M21Index = 6;


		/** YW: Typically the translation of the Y component. On Vector3 multiplication this value is added to the target Y
		 * component. */
		public const int M31Index = 7;


		/** ZX: Typically the negative sine of the angle when rotated on the Y axis. On Vector3 multiplication this value is multiplied
		 * with the source X component and added to the target Z component. */
		public const int M02Index = 8;

		/** ZY: Typical the sine of the angle when rotated on the X axis. On Vector3 multiplication this value is multiplied with the
		 * source Y component and added to the target Z component. */
		public const int M12Index = 9;

		/** ZZ: Typically the unrotated Z component for scaling, also the cosine of the angle when rotated on the X and/or Y axis. On
		 * Vector3 multiplication this value is multiplied with the source Z component and added to the target Z component. */
		public const int M22Index = 10;

		/** ZW: Typically the translation of the Z component. On Vector3 multiplication this value is added to the target Z
		 * component. */
		public const int M32Index = 11;


		/** WX: Typically the value zero. On Vector3 multiplication this value is ignored. */
		public const int M03Index = 12;

		/** WY: Typically the value zero. On Vector3 multiplication this value is ignored. */
		public const int M13Index = 13;

		/** WZ: Typically the value zero. On Vector3 multiplication this value is ignored. */
		public const int M23Index = 14;

		/** WW: Typically the value one. On Vector3 multiplication this value is ignored. */
		public const int M33Index = 15;

		private const int Count = 16;
		static FPQuaternion quat = FPQuaternion.default2;
		static FPQuaternion quat2 = FPQuaternion.default2;
		static FPVector3 l_vez = new FPVector3();
		static FPVector3 l_vex = new FPVector3();
		static FPVector3 l_vey = new FPVector3();
		static FPVector3 tmpVec = new FPVector3();
		static FPMatrix4x4 tmpMat = FPMatrix4x4.default2;
		static FPVector3 right = new FPVector3();
		static FPVector3 tmpForward = new FPVector3();
		static FPVector3 tmpUp = new FPVector3();

		public FP m00;
		public FP m10;
		public FP m20;
		public FP m30;

		public FP m01;
		public FP m11;
		public FP m21;
		public FP m31;

		public FP m02;
		public FP m12;
		public FP m22;
		public FP m32;

		public FP m03;
		public FP m13;
		public FP m23;
		public FP m33;

		public FP[] val
		{
			get
			{
				var result = new FP[Count];
				result[M00Index] = m00;
				result[M01Index] = m01;
				result[M02Index] = m02;
				result[M03Index] = m03;

				result[M10Index] = m10;
				result[M11Index] = m11;
				result[M12Index] = m12;
				result[M13Index] = m13;

				result[M20Index] = m20;
				result[M21Index] = m21;
				result[M22Index] = m22;
				result[M23Index] = m23;

				result[M30Index] = m30;
				result[M31Index] = m31;
				result[M32Index] = m32;
				result[M33Index] = m33;

				return result;
			}
		}

		/** Constructs an identity matrix */
		public static FPMatrix4x4 default2
		{
			get
			{
				FPMatrix4x4 result = default;
				result.m00 = 1f;
				result.m11 = 1f;
				result.m22 = 1f;
				result.m33 = 1f;
				return result;
			}
		}

		/** Constructs a matrix from the given matrix.
		 * @param matrix The matrix to copy. (This matrix is not modified) */
		public FPMatrix4x4(FPMatrix4x4 matrix)
		{
			this.m00 = matrix.m00;
			this.m10 = matrix.m10;
			this.m20 = matrix.m20;
			this.m30 = matrix.m30;

			this.m01 = matrix.m01;
			this.m11 = matrix.m11;
			this.m21 = matrix.m21;
			this.m31 = matrix.m31;

			this.m02 = matrix.m02;
			this.m12 = matrix.m12;
			this.m22 = matrix.m22;
			this.m32 = matrix.m32;

			this.m03 = matrix.m03;
			this.m13 = matrix.m13;
			this.m23 = matrix.m23;
			this.m33 = matrix.m33;
		}

		/** Constructs a matrix from the given float array. The array must have at least 16 elements; the first 16 will be copied.
		 * @param values The float array to copy. Remember that this matrix is in
		 *           <a href="http://en.wikipedia.org/wiki/Row-major_order">column major</a> order. (The float array is not
		 *           modified) */
		public FPMatrix4x4(FP[] values)
		{
			this.m00 = values[M00Index];
			this.m10 = values[M10Index];
			this.m20 = values[M20Index];
			this.m30 = values[M30Index];

			this.m01 = values[M01Index];
			this.m11 = values[M11Index];
			this.m21 = values[M21Index];
			this.m31 = values[M31Index];

			this.m02 = values[M02Index];
			this.m12 = values[M12Index];
			this.m22 = values[M22Index];
			this.m32 = values[M32Index];

			this.m03 = values[M03Index];
			this.m13 = values[M13Index];
			this.m23 = values[M23Index];
			this.m33 = values[M33Index];
		}

		/** Constructs a rotation matrix from the given {@link Quaternion}.
		 * @param quaternion The quaternion to be copied. (The quaternion is not modified) */
		public FPMatrix4x4(FPQuaternion quaternion)
		{
			FP translationX = 0;
			FP translationY = 0;
			FP translationZ = 0;
			FP quaternionX = quaternion.x;
			FP quaternionY = quaternion.y;
			FP quaternionZ = quaternion.z;
			FP quaternionW = quaternion.w;

			FP xs = quaternionX * 2f,
				ys = quaternionY * 2f,
				zs = quaternionZ * 2f;
			FP wx = quaternionW * xs, wy = quaternionW * ys, wz = quaternionW * zs;
			FP xx = quaternionX * xs, xy = quaternionX * ys, xz = quaternionX * zs;
			FP yy = quaternionY * ys, yz = quaternionY * zs, zz = quaternionZ * zs;

			this.m00 = 1f - (yy + zz);
			this.m01 = xy - wz;
			this.m02 = xz + wy;
			this.m03 = translationX;

			this.m10 = xy + wz;
			this.m11 = 1f - (xx + zz);
			this.m12 = yz - wx;
			this.m13 = translationY;

			this.m20 = xz - wy;
			this.m21 = yz + wx;
			this.m22 = 1f - (xx + yy);
			this.m23 = translationZ;

			this.m30 = 0f;
			this.m31 = 0f;
			this.m32 = 0f;
			this.m33 = 1f;
		}

		/** Construct a matrix from the given translation, rotation and scale.
		 * @param position The translation
		 * @param rotation The rotation, must be normalized
		 * @param scale The scale */
		public FPMatrix4x4(FPVector3 position, FPQuaternion rotation, FPVector3 scale)
		{
			FP translationX = position.x;
			FP translationY = position.y;
			FP translationZ = position.z;
			FP quaternionX = rotation.x;
			FP quaternionY = rotation.y;
			FP quaternionZ = rotation.z;
			FP quaternionW = rotation.w;
			FP scaleX = scale.x;
			FP scaleY = scale.y;
			FP scaleZ = scale.z;

			FP xs = quaternionX * 2f,
				ys = quaternionY * 2f,
				zs = quaternionZ * 2f;
			FP wx = quaternionW * xs, wy = quaternionW * ys, wz = quaternionW * zs;
			FP xx = quaternionX * xs, xy = quaternionX * ys, xz = quaternionX * zs;
			FP yy = quaternionY * ys, yz = quaternionY * zs, zz = quaternionZ * zs;

			this.m00 = scaleX * (1.0f - (yy + zz));
			this.m01 = scaleY * (xy - wz);
			this.m02 = scaleZ * (xz + wy);
			this.m03 = translationX;

			this.m10 = scaleX * (xy + wz);
			this.m11 = scaleY * (1.0f - (xx + zz));
			this.m12 = scaleZ * (yz - wx);
			this.m13 = translationY;

			this.m20 = scaleX * (xz - wy);
			this.m21 = scaleY * (yz + wx);
			this.m22 = scaleZ * (1.0f - (xx + yy));
			this.m23 = translationZ;

			this.m30 = 0f;
			this.m31 = 0f;
			this.m32 = 0f;
			this.m33 = 1f;
		}

		/** Sets the matrix to the given matrix.
		 * @param matrix The matrix that is to be copied. (The given matrix is not modified)
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 set(FPMatrix4x4 matrix)
		{
			this.m00 = matrix.m00;
			this.m10 = matrix.m10;
			this.m20 = matrix.m20;
			this.m30 = matrix.m30;

			this.m01 = matrix.m01;
			this.m11 = matrix.m11;
			this.m21 = matrix.m21;
			this.m31 = matrix.m31;

			this.m02 = matrix.m02;
			this.m12 = matrix.m12;
			this.m22 = matrix.m22;
			this.m32 = matrix.m32;

			this.m03 = matrix.m03;
			this.m13 = matrix.m13;
			this.m23 = matrix.m23;
			this.m33 = matrix.m33;

			return this;
		}

		/** Sets the matrix to the given matrix as a float array. The float array must have at least 16 elements; the first 16 will be
		 * copied.
		 * 
		 * @param values The matrix, in float form, that is to be copied. Remember that this matrix is in
		 *           <a href="http://en.wikipedia.org/wiki/Row-major_order">column major</a> order.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 set(FP[] values)
		{
			this.m00 = values[M00Index];
			this.m10 = values[M10Index];
			this.m20 = values[M20Index];
			this.m30 = values[M30Index];

			this.m01 = values[M01Index];
			this.m11 = values[M11Index];
			this.m21 = values[M21Index];
			this.m31 = values[M31Index];

			this.m02 = values[M02Index];
			this.m12 = values[M12Index];
			this.m22 = values[M22Index];
			this.m32 = values[M32Index];

			this.m03 = values[M03Index];
			this.m13 = values[M13Index];
			this.m23 = values[M23Index];
			this.m33 = values[M33Index];
			return this;
		}

		/** Sets the matrix to a rotation matrix representing the quaternion.
		 * @param quaternion The quaternion that is to be used to set this matrix.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 set(FPQuaternion quaternion)
		{
			return set(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
		}

		/** Sets the matrix to a rotation matrix representing the quaternion.
		 * 
		 * @param quaternionX The X component of the quaternion that is to be used to set this matrix.
		 * @param quaternionY The Y component of the quaternion that is to be used to set this matrix.
		 * @param quaternionZ The Z component of the quaternion that is to be used to set this matrix.
		 * @param quaternionW The W component of the quaternion that is to be used to set this matrix.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 set(FP quaternionX, FP quaternionY, FP quaternionZ,
			FP quaternionW)
		{
			return set(0f, 0f, 0f, quaternionX, quaternionY, quaternionZ,
				quaternionW);
		}

		/** Set this matrix to the specified translation and rotation.
		 * @param position The translation
		 * @param orientation The rotation, must be normalized
		 * @return This matrix for chaining */
		public FPMatrix4x4 set(FPVector3 position, FPQuaternion orientation)
		{
			return set(position.x, position.y, position.z, orientation.x, orientation.y, orientation.z, orientation.w);
		}

		/** Sets the matrix to a rotation matrix representing the translation and quaternion.
		 * @param translationX The X component of the translation that is to be used to set this matrix.
		 * @param translationY The Y component of the translation that is to be used to set this matrix.
		 * @param translationZ The Z component of the translation that is to be used to set this matrix.
		 * @param quaternionX The X component of the quaternion that is to be used to set this matrix.
		 * @param quaternionY The Y component of the quaternion that is to be used to set this matrix.
		 * @param quaternionZ The Z component of the quaternion that is to be used to set this matrix.
		 * @param quaternionW The W component of the quaternion that is to be used to set this matrix.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 set(FP translationX, FP translationY, FP translationZ,
			FP quaternionX, FP quaternionY,
			FP quaternionZ, FP quaternionW)
		{
			FP xs = quaternionX * 2f,
				ys = quaternionY * 2f,
				zs = quaternionZ * 2f;
			FP wx = quaternionW * xs, wy = quaternionW * ys, wz = quaternionW * zs;
			FP xx = quaternionX * xs, xy = quaternionX * ys, xz = quaternionX * zs;
			FP yy = quaternionY * ys, yz = quaternionY * zs, zz = quaternionZ * zs;

			this.m00 = 1f - (yy + zz);
			this.m01 = xy - wz;
			this.m02 = xz + wy;
			this.m03 = translationX;

			this.m10 = xy + wz;
			this.m11 = 1f - (xx + zz);
			this.m12 = yz - wx;
			this.m13 = translationY;

			this.m20 = xz - wy;
			this.m21 = yz + wx;
			this.m22 = 1f - (xx + yy);
			this.m23 = translationZ;

			this.m30 = 0f;
			this.m31 = 0f;
			this.m32 = 0f;
			this.m33 = 1f;
			return this;
		}

		/** Set this matrix to the specified translation, rotation and scale.
		 * @param position The translation
		 * @param orientation The rotation, must be normalized
		 * @param scale The scale
		 * @return This matrix for chaining */
		public FPMatrix4x4 set(FPVector3 position, FPQuaternion orientation, FPVector3 scale)
		{
			return set(position.x, position.y, position.z, orientation.x, orientation.y, orientation.z, orientation.w,
				scale.x, scale.y,
				scale.z);
		}

		/** Sets the matrix to a rotation matrix representing the translation and quaternion.
		 * @param translationX The X component of the translation that is to be used to set this matrix.
		 * @param translationY The Y component of the translation that is to be used to set this matrix.
		 * @param translationZ The Z component of the translation that is to be used to set this matrix.
		 * @param quaternionX The X component of the quaternion that is to be used to set this matrix.
		 * @param quaternionY The Y component of the quaternion that is to be used to set this matrix.
		 * @param quaternionZ The Z component of the quaternion that is to be used to set this matrix.
		 * @param quaternionW The W component of the quaternion that is to be used to set this matrix.
		 * @param scaleX The X component of the scaling that is to be used to set this matrix.
		 * @param scaleY The Y component of the scaling that is to be used to set this matrix.
		 * @param scaleZ The Z component of the scaling that is to be used to set this matrix.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 set(FP translationX, FP translationY, FP translationZ,
			FP quaternionX, FP quaternionY,
			FP quaternionZ, FP quaternionW, FP scaleX, FP scaleY,
			FP scaleZ)
		{
			FP xs = quaternionX * 2f,
				ys = quaternionY * 2f,
				zs = quaternionZ * 2f;
			FP wx = quaternionW * xs, wy = quaternionW * ys, wz = quaternionW * zs;
			FP xx = quaternionX * xs, xy = quaternionX * ys, xz = quaternionX * zs;
			FP yy = quaternionY * ys, yz = quaternionY * zs, zz = quaternionZ * zs;

			this.m00 = scaleX * (1.0f - (yy + zz));
			this.m01 = scaleY * (xy - wz);
			this.m02 = scaleZ * (xz + wy);
			this.m03 = translationX;

			this.m10 = scaleX * (xy + wz);
			this.m11 = scaleY * (1.0f - (xx + zz));
			this.m12 = scaleZ * (yz - wx);
			this.m13 = translationY;

			this.m20 = scaleX * (xz - wy);
			this.m21 = scaleY * (yz + wx);
			this.m22 = scaleZ * (1.0f - (xx + yy));
			this.m23 = translationZ;

			this.m30 = 0f;
			this.m31 = 0f;
			this.m32 = 0f;
			this.m33 = 1f;
			return this;
		}


		/** Sets the four columns of the matrix which correspond to the x-, y- and z-axis of the vector space this matrix creates as
		 * well as the 4th column representing the translation of any point that is multiplied by this matrix.
		 * @param xAxis The x-axis.
		 * @param yAxis The y-axis.
		 * @param zAxis The z-axis.
		 * @param pos The translation vector. */
		public FPMatrix4x4 set(FPVector3 xAxis, FPVector3 yAxis, FPVector3 zAxis, FPVector3 pos)
		{
			this.m00 = xAxis.x;
			this.m10 = xAxis.y;
			this.m20 = xAxis.z;
			this.m01 = yAxis.x;
			this.m11 = yAxis.y;
			this.m21 = yAxis.z;
			this.m02 = zAxis.x;
			this.m12 = zAxis.y;
			this.m22 = zAxis.z;
			this.m03 = pos.x;
			this.m13 = pos.y;
			this.m23 = pos.z;
			this.m30 = 0f;
			this.m31 = 0f;
			this.m32 = 0f;
			this.m33 = 1f;
			return this;
		}

		/** @return a copy of this matrix */
		public FPMatrix4x4 cpy()
		{
			return new FPMatrix4x4(this);
		}

		/** Adds a translational component to the matrix in the 4th column. The other columns are untouched.
		 * @param vector The translation vector to add to the current matrix. (This vector is not modified)
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 trn(FPVector3 vector)
		{
			this.m03 += vector.x;
			this.m13 += vector.y;
			this.m23 += vector.z;
			return this;
		}

		/** Adds a translational component to the matrix in the 4th column. The other columns are untouched.
		 * @param x The x-component of the translation vector.
		 * @param y The y-component of the translation vector.
		 * @param z The z-component of the translation vector.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 trn(FP x, FP y, FP z)
		{
			this.m03 += x;
			this.m13 += y;
			this.m23 += z;
			return this;
		}

		/** @return the backing float array */
		public FP[] getValues()
		{
			return val;
		}

		/** Postmultiplies this matrix with the given matrix, storing the result in this matrix. For example:
		 * 
		 * <pre>
		 * A.mul(B) results in A := AB.
		 * </pre>
		 * 
		 * @param matrix The other matrix to multiply by.
		 * @return This matrix for the purpose of chaining operations together. */
		public FPMatrix4x4 mul(FPMatrix4x4 matrix)
		{
			FP m00 = this.m00 * matrix.m00 + this.m01 * matrix.m10 +
							   this.m02 * matrix.m20 + this.m03 * matrix.m30;
			FP m01 = this.m00 * matrix.m01 + this.m01 * matrix.m11 +
							   this.m02 * matrix.m21 + this.m03 * matrix.m31;
			FP m02 = this.m00 * matrix.m02 + this.m01 * matrix.m12 +
							   this.m02 * matrix.m22 + this.m03 * matrix.m32;
			FP m03 = this.m00 * matrix.m03 + this.m01 * matrix.m13 +
							   this.m02 * matrix.m23 + this.m03 * matrix.m33;
			FP m10 = this.m10 * matrix.m00 + this.m11 * matrix.m10 +
							   this.m12 * matrix.m20 + this.m13 * matrix.m30;
			FP m11 = this.m10 * matrix.m01 + this.m11 * matrix.m11 +
							   this.m12 * matrix.m21 + this.m13 * matrix.m31;
			FP m12 = this.m10 * matrix.m02 + this.m11 * matrix.m12 +
							   this.m12 * matrix.m22 + this.m13 * matrix.m32;
			FP m13 = this.m10 * matrix.m03 + this.m11 * matrix.m13 +
							   this.m12 * matrix.m23 + this.m13 * matrix.m33;
			FP m20 = this.m20 * matrix.m00 + this.m21 * matrix.m10 +
							   this.m22 * matrix.m20 + this.m23 * matrix.m30;
			FP m21 = this.m20 * matrix.m01 + this.m21 * matrix.m11 +
							   this.m22 * matrix.m21 + this.m23 * matrix.m31;
			FP m22 = this.m20 * matrix.m02 + this.m21 * matrix.m12 +
							   this.m22 * matrix.m22 + this.m23 * matrix.m32;
			FP m23 = this.m20 * matrix.m03 + this.m21 * matrix.m13 +
							   this.m22 * matrix.m23 + this.m23 * matrix.m33;
			FP m30 = this.m30 * matrix.m00 + this.m31 * matrix.m10 +
							   this.m32 * matrix.m20 + this.m33 * matrix.m30;
			FP m31 = this.m30 * matrix.m01 + this.m31 * matrix.m11 +
							   this.m32 * matrix.m21 + this.m33 * matrix.m31;
			FP m32 = this.m30 * matrix.m02 + this.m31 * matrix.m12 +
							   this.m32 * matrix.m22 + this.m33 * matrix.m32;
			FP m33 = this.m30 * matrix.m03 + this.m31 * matrix.m13 +
							   this.m32 * matrix.m23 + this.m33 * matrix.m33;
			this.m00 = m00;
			this.m10 = m10;
			this.m20 = m20;
			this.m30 = m30;
			this.m01 = m01;
			this.m11 = m11;
			this.m21 = m21;
			this.m31 = m31;
			this.m02 = m02;
			this.m12 = m12;
			this.m22 = m22;
			this.m32 = m32;
			this.m03 = m03;
			this.m13 = m13;
			this.m23 = m23;
			this.m33 = m33;
			return this;
		}

		/** Premultiplies this matrix with the given matrix, storing the result in this matrix. For example:
		 * 
		 * <pre>
		 * A.mulLeft(B) results in A := BA.
		 * </pre>
		 * 
		 * @param matrix The other matrix to multiply by.
		 * @return This matrix for the purpose of chaining operations together. */
		public FPMatrix4x4 mulLeft(FPMatrix4x4 matrix)
		{
			tmpMat.set(matrix);
			tmpMat.mul(this);
			return set(tmpMat);
		}

		/** Transposes the matrix.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 tra()
		{
			FP m01 = this.m01;
			FP m02 = this.m02;
			FP m03 = this.m03;
			FP m12 = this.m12;
			FP m13 = this.m13;
			FP m23 = this.m23;
			this.m01 = this.m10;
			this.m02 = this.m20;
			this.m03 = this.m30;
			this.m10 = m01;
			this.m12 = this.m21;
			this.m13 = this.m31;
			this.m20 = m02;
			this.m21 = m12;
			this.m23 = this.m32;
			this.m30 = m03;
			this.m31 = m13;
			this.m32 = m23;
			return this;
		}

		/** Sets the matrix to an identity matrix.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 idt()
		{
			this.m00 = 1f;
			this.m01 = 0f;
			this.m02 = 0f;
			this.m03 = 0f;
			this.m10 = 0f;
			this.m11 = 1f;
			this.m12 = 0f;
			this.m13 = 0f;
			this.m20 = 0f;
			this.m21 = 0f;
			this.m22 = 1f;
			this.m23 = 0f;
			this.m30 = 0f;
			this.m31 = 0f;
			this.m32 = 0f;
			this.m33 = 1f;
			return this;
		}

		/** Inverts the matrix. Stores the result in this matrix.
		 * @return This matrix for the purpose of chaining methods together.
		 * @throws RuntimeException if the matrix is singular (not invertible) */
		public FPMatrix4x4 inv()
		{
			FP l_det = this.m30 * this.m21 * this.m12 * this.m03 - this.m20 * this.m31 * this.m12 * this.m03
																		   - this.m30 * this.m11 * this.m22 *
																		   this.m03 + this.m10 *
																					this.m31 * this.m22 * this.m03
																					+ this.m20 *
																					this.m11 *
																					this.m32 * this.m03 - this.m10 *
																										this.m21 *
																										this.m32 *
																										this.m03
																										- this.m30 *
																										this.m21 *
																										this.m02 *
																										this
																											.m13 + this
																													 .m20 *
																												 this.m31 *
																												 this.m02 *
																												 this.m13
																												 + this
																													 .m30 *
																												 this.m01 *
																												 this.m22 *
																												 this
																													 .m13 - this
																															  .m00 *
																														  this
																															  .m31 *
																														  this
																															  .m22 *
																														  this
																															  .m13
																														  - this
																															  .m20 *
																														  this
																															  .m01 *
																														  this
																															  .m32 *
																														  this
																															  .m13 + this
																																	   .m00 *
																																   this
																																	   .m21 *
																																   this
																																	   .m32 *
																																   this
																																	   .m13
																																   + this
																																	   .m30 *
																																   this
																																	   .m11 *
																																   this
																																	   .m02 *
																																   this
																																	   .m23 - this
																																				.m10 *
																																			this
																																				.m31 *
																																			this
																																				.m02 *
																																			this
																																				.m23
																																			- this
																																				.m30 *
																																			this
																																				.m01 *
																																			this
																																				.m12 *
																																			this
																																				.m23 + this
																																						 .m00 *
																																					 this
																																						 .m31 *
																																					 this
																																						 .m12 *
																																					 this
																																						 .m23
																																					 + this
																																						 .m10 *
																																					 this
																																						 .m01 *
																																					 this
																																						 .m32 *
																																					 this
																																						 .m23 - this
																																								  .m00 *
																																							  this
																																								  .m11 *
																																							  this
																																								  .m32 *
																																							  this
																																								  .m23
																																							  - this
																																								  .m20 *
																																							  this
																																								  .m11 *
																																							  this
																																								  .m02 *
																																							  this
																																								  .m33 + this
																																										   .m10 *
																																									   this
																																										   .m21 *
																																									   this
																																										   .m02 *
																																									   this
																																										   .m33
																																									   +
																																									   this
																																										   .m20 *
																																									   this
																																										   .m01 *
																																									   this
																																										   .m12 *
																																									   this
																																										   .m33 - this
																																													.m00 *
																																												this
																																													.m21 *
																																												this
																																													.m12 *
																																												this
																																													.m33
																																												-
																																												this
																																													.m10 *
																																												this
																																													.m01 *
																																												this
																																													.m22 *
																																												this
																																													.m33 +
								 this.m00 * this.m11 * this.m22 * this.m33;
			if (l_det == 0f) throw new Exception("non-invertible matrix");
			FP m00 = this.m12 * this.m23 * this.m31 - this.m13 * this.m22 * this.m31 +
							   this.m13 * this.m21 * this.m32
							   - this.m11 * this.m23 * this.m32 - this.m12 * this.m21 * this.m33 +
							   this.m11 * this.m22 * this.m33;
			FP m01 = this.m03 * this.m22 * this.m31 - this.m02 * this.m23 * this.m31 -
							   this.m03 * this.m21 * this.m32
							   + this.m01 * this.m23 * this.m32 + this.m02 * this.m21 * this.m33 -
							   this.m01 * this.m22 * this.m33;
			FP m02 = this.m02 * this.m13 * this.m31 - this.m03 * this.m12 * this.m31 +
							   this.m03 * this.m11 * this.m32
							   - this.m01 * this.m13 * this.m32 - this.m02 * this.m11 * this.m33 +
							   this.m01 * this.m12 * this.m33;
			FP m03 = this.m03 * this.m12 * this.m21 - this.m02 * this.m13 * this.m21 -
							   this.m03 * this.m11 * this.m22
							   + this.m01 * this.m13 * this.m22 + this.m02 * this.m11 * this.m23 -
							   this.m01 * this.m12 * this.m23;
			FP m10 = this.m13 * this.m22 * this.m30 - this.m12 * this.m23 * this.m30 -
							   this.m13 * this.m20 * this.m32
							   + this.m10 * this.m23 * this.m32 + this.m12 * this.m20 * this.m33 -
							   this.m10 * this.m22 * this.m33;
			FP m11 = this.m02 * this.m23 * this.m30 - this.m03 * this.m22 * this.m30 +
							   this.m03 * this.m20 * this.m32
							   - this.m00 * this.m23 * this.m32 - this.m02 * this.m20 * this.m33 +
							   this.m00 * this.m22 * this.m33;
			FP m12 = this.m03 * this.m12 * this.m30 - this.m02 * this.m13 * this.m30 -
							   this.m03 * this.m10 * this.m32
							   + this.m00 * this.m13 * this.m32 + this.m02 * this.m10 * this.m33 -
							   this.m00 * this.m12 * this.m33;
			FP m13 = this.m02 * this.m13 * this.m20 - this.m03 * this.m12 * this.m20 +
							   this.m03 * this.m10 * this.m22
							   - this.m00 * this.m13 * this.m22 - this.m02 * this.m10 * this.m23 +
							   this.m00 * this.m12 * this.m23;
			FP m20 = this.m11 * this.m23 * this.m30 - this.m13 * this.m21 * this.m30 +
							   this.m13 * this.m20 * this.m31
							   - this.m10 * this.m23 * this.m31 - this.m11 * this.m20 * this.m33 +
							   this.m10 * this.m21 * this.m33;
			FP m21 = this.m03 * this.m21 * this.m30 - this.m01 * this.m23 * this.m30 -
							   this.m03 * this.m20 * this.m31
							   + this.m00 * this.m23 * this.m31 + this.m01 * this.m20 * this.m33 -
							   this.m00 * this.m21 * this.m33;
			FP m22 = this.m01 * this.m13 * this.m30 - this.m03 * this.m11 * this.m30 +
							   this.m03 * this.m10 * this.m31
							   - this.m00 * this.m13 * this.m31 - this.m01 * this.m10 * this.m33 +
							   this.m00 * this.m11 * this.m33;
			FP m23 = this.m03 * this.m11 * this.m20 - this.m01 * this.m13 * this.m20 -
							   this.m03 * this.m10 * this.m21
							   + this.m00 * this.m13 * this.m21 + this.m01 * this.m10 * this.m23 -
							   this.m00 * this.m11 * this.m23;
			FP m30 = this.m12 * this.m21 * this.m30 - this.m11 * this.m22 * this.m30 -
							   this.m12 * this.m20 * this.m31
							   + this.m10 * this.m22 * this.m31 + this.m11 * this.m20 * this.m32 -
							   this.m10 * this.m21 * this.m32;
			FP m31 = this.m01 * this.m22 * this.m30 - this.m02 * this.m21 * this.m30 +
							   this.m02 * this.m20 * this.m31
							   - this.m00 * this.m22 * this.m31 - this.m01 * this.m20 * this.m32 +
							   this.m00 * this.m21 * this.m32;
			FP m32 = this.m02 * this.m11 * this.m30 - this.m01 * this.m12 * this.m30 -
							   this.m02 * this.m10 * this.m31
							   + this.m00 * this.m12 * this.m31 + this.m01 * this.m10 * this.m32 -
							   this.m00 * this.m11 * this.m32;
			FP m33 = this.m01 * this.m12 * this.m20 - this.m02 * this.m11 * this.m20 +
							   this.m02 * this.m10 * this.m21
							   - this.m00 * this.m12 * this.m21 - this.m01 * this.m10 * this.m22 +
							   this.m00 * this.m11 * this.m22;
			FP inv_det = 1.0f / l_det;
			this.m00 = m00 * inv_det;
			this.m10 = m10 * inv_det;
			this.m20 = m20 * inv_det;
			this.m30 = m30 * inv_det;
			this.m01 = m01 * inv_det;
			this.m11 = m11 * inv_det;
			this.m21 = m21 * inv_det;
			this.m31 = m31 * inv_det;
			this.m02 = m02 * inv_det;
			this.m12 = m12 * inv_det;
			this.m22 = m22 * inv_det;
			this.m32 = m32 * inv_det;
			this.m03 = m03 * inv_det;
			this.m13 = m13 * inv_det;
			this.m23 = m23 * inv_det;
			this.m33 = m33 * inv_det;
			return this;
		}

		/** @return The determinant of this matrix */
		public FP det()
		{
			return this.m30 * this.m21 * this.m12 * this.m03 - this.m20 * this.m31 * this.m12 * this.m03
															 - this.m30 * this.m11 * this.m22 * this.m03 + this.m10 *
																										 this.m31 *
																										 this.m22 * this.m03
																										 + this.m20 *
																										 this.m11 *
																										 this.m32 *
																										 this
																											 .m03 - this
																													  .m10 *
																												  this.m21 *
																												  this.m32 *
																												  this.m03
																												  - this
																													  .m30 *
																												  this.m21 *
																												  this.m02 *
																												  this
																													  .m13 + this
																															   .m20 *
																														   this
																															   .m31 *
																														   this
																															   .m02 *
																														   this
																															   .m13
																														   + this
																															   .m30 *
																														   this
																															   .m01 *
																														   this
																															   .m22 *
																														   this
																															   .m13 - this
																																		.m00 *
																																	this
																																		.m31 *
																																	this
																																		.m22 *
																																	this
																																		.m13
																																	- this
																																		.m20 *
																																	this
																																		.m01 *
																																	this
																																		.m32 *
																																	this
																																		.m13 + this
																																				 .m00 *
																																			 this
																																				 .m21 *
																																			 this
																																				 .m32 *
																																			 this
																																				 .m13
																																			 + this
																																				 .m30 *
																																			 this
																																				 .m11 *
																																			 this
																																				 .m02 *
																																			 this
																																				 .m23 - this
																																						  .m10 *
																																					  this
																																						  .m31 *
																																					  this
																																						  .m02 *
																																					  this
																																						  .m23
																																					  - this
																																						  .m30 *
																																					  this
																																						  .m01 *
																																					  this
																																						  .m12 *
																																					  this
																																						  .m23 + this
																																								   .m00 *
																																							   this
																																								   .m31 *
																																							   this
																																								   .m12 *
																																							   this
																																								   .m23
																																							   + this
																																								   .m10 *
																																							   this
																																								   .m01 *
																																							   this
																																								   .m32 *
																																							   this
																																								   .m23 - this
																																											.m00 *
																																										this
																																											.m11 *
																																										this
																																											.m32 *
																																										this
																																											.m23
																																										- this
																																											.m20 *
																																										this
																																											.m11 *
																																										this
																																											.m02 *
																																										this
																																											.m33 + this
																																													 .m10 *
																																												 this
																																													 .m21 *
																																												 this
																																													 .m02 *
																																												 this
																																													 .m33
																																												 +
																																												 this
																																													 .m20 *
																																												 this
																																													 .m01 *
																																												 this
																																													 .m12 *
																																												 this
																																													 .m33 - this
																																															  .m00 *
																																														  this
																																															  .m21 *
																																														  this
																																															  .m12 *
																																														  this
																																															  .m33
																																														  -
																																														  this
																																															  .m10 *
																																														  this
																																															  .m01 *
																																														  this
																																															  .m22 *
																																														  this
																																															  .m33 +
				   this.m00 * this.m11 * this.m22 * this.m33;
		}

		/** @return The determinant of the 3x3 upper left matrix */
		public FP det3x3()
		{
			return this.m00 * this.m11 * this.m22 + this.m01 * this.m12 * this.m20 + this.m02 * this.m10 * this.m21
				   - this.m00 * this.m12 * this.m21 - this.m01 * this.m10 * this.m22 - this.m02 * this.m11 * this.m20;
		}

		//http://gsteph.blogspot.com/2012/05/world-view-and-projection-matrix.html
		/** Sets the matrix to a projection matrix with a near- and far plane, a field of view in degrees and an aspect ratio. Note
		 * that the field of view specified is the angle in degrees for the height, the field of view for the width will be calculated
		 * according to the aspect ratio.
		 * @param near The near plane
		 * @param far The far plane
		 * @param fovy The field of view of the height in degrees
		 * @param aspectRatio The "width over height" aspect ratio
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToProjection(FP near, FP far, FP fovy, FP aspectRatio)
		{
			idt();
			FP l_fd = 1.0 /
								FPMath.Tan((fovy * (FPMath.PI / 180)) / 2.0);
			FP l_a1 = (far + near) / (near - far);
			FP l_a2 = (2 * far * near) / (near - far);
			this.m00 = l_fd / aspectRatio;
			this.m10 = 0;
			this.m20 = 0;
			this.m30 = 0;
			this.m01 = 0;
			this.m11 = l_fd;
			this.m21 = 0;
			this.m31 = 0;
			this.m02 = 0;
			this.m12 = 0;
			this.m22 = l_a1;
			this.m32 = (-1);
			this.m03 = 0;
			this.m13 = 0;
			this.m23 = l_a2;
			this.m33 = 0;
			return this;
		}

		/** Sets the matrix to a projection matrix with a near/far plane, and left, bottom, right and top specifying the points on the
		 * near plane that are mapped to the lower left and upper right corners of the viewport. This allows to create projection
		 * matrix with off-center vanishing point.
		 * @param left
		 * @param right
		 * @param bottom
		 * @param top
		 * @param near The near plane
		 * @param far The far plane
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToProjection(FP left, FP right, FP bottom, FP top,
			FP near, FP far)
		{
			FP x = 2.0f * near / (right - left);
			FP y = 2.0f * near / (top - bottom);
			FP a = (right + left) / (right - left);
			FP b = (top + bottom) / (top - bottom);
			FP l_a1 = (far + near) / (near - far);
			FP l_a2 = (2 * far * near) / (near - far);
			this.m00 = x;
			this.m10 = 0;
			this.m20 = 0;
			this.m30 = 0;
			this.m01 = 0;
			this.m11 = y;
			this.m21 = 0;
			this.m31 = 0;
			this.m02 = a;
			this.m12 = b;
			this.m22 = l_a1;
			this.m32 = (-1);
			this.m03 = 0;
			this.m13 = 0;
			this.m23 = l_a2;
			this.m33 = 0;
			return this;
		}

		/** Sets this matrix to an orthographic projection matrix with the origin at (x,y) extending by width and height. The near
		 * plane is set to 0, the far plane is set to 1.
		 * @param x The x-coordinate of the origin
		 * @param y The y-coordinate of the origin
		 * @param width The width
		 * @param height The height
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToOrtho2D(FP x, FP y, FP width, FP height)
		{
			setToOrtho(x, x + width, y, y + height, 0, 1);
			return this;
		}

		/** Sets this matrix to an orthographic projection matrix with the origin at (x,y) extending by width and height, having a near
		 * and far plane.
		 * @param x The x-coordinate of the origin
		 * @param y The y-coordinate of the origin
		 * @param width The width
		 * @param height The height
		 * @param near The near plane
		 * @param far The far plane
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToOrtho2D(FP x, FP y, FP width, FP height,
			FP near, FP far)
		{
			setToOrtho(x, x + width, y, y + height, near, far);
			return this;
		}

		/** Sets the matrix to an orthographic projection like glOrtho (http://www.opengl.org/sdk/docs/man/xhtml/glOrtho.xml) following
		 * the OpenGL equivalent
		 * @param left The left clipping plane
		 * @param right The right clipping plane
		 * @param bottom The bottom clipping plane
		 * @param top The top clipping plane
		 * @param near The near clipping plane
		 * @param far The far clipping plane
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToOrtho(FP left, FP right, FP bottom, FP top,
			FP near, FP far)
		{
			FP x_orth = 2 / (right - left);
			FP y_orth = 2 / (top - bottom);
			FP z_orth = (-2) / (far - near);

			FP tx = -(right + left) / (right - left);
			FP ty = -(top + bottom) / (top - bottom);
			FP tz = -(far + near) / (far - near);

			this.m00 = x_orth;
			this.m10 = 0;
			this.m20 = 0;
			this.m30 = 0;
			this.m01 = 0;
			this.m11 = y_orth;
			this.m21 = 0;
			this.m31 = 0;
			this.m02 = 0;
			this.m12 = 0;
			this.m22 = z_orth;
			this.m32 = 0;
			this.m03 = tx;
			this.m13 = ty;
			this.m23 = tz;
			this.m33 = 1;
			return this;
		}

		/** Sets the 4th column to the translation vector.
		 * @param vector The translation vector
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setTranslation(FPVector3 vector)
		{
			this.m03 = vector.x;
			this.m13 = vector.y;
			this.m23 = vector.z;
			return this;
		}

		/** Sets the 4th column to the translation vector.
		 * @param x The X coordinate of the translation vector
		 * @param y The Y coordinate of the translation vector
		 * @param z The Z coordinate of the translation vector
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setTranslation(FP x, FP y, FP z)
		{
			this.m03 = x;
			this.m13 = y;
			this.m23 = z;
			return this;
		}

		/** Sets this matrix to a translation matrix, overwriting it first by an identity matrix and then setting the 4th column to the
		 * translation vector.
		 * @param vector The translation vector
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToTranslation(FPVector3 vector)
		{
			idt();
			this.m03 = vector.x;
			this.m13 = vector.y;
			this.m23 = vector.z;
			return this;
		}

		/** Sets this matrix to a translation matrix, overwriting it first by an identity matrix and then setting the 4th column to the
		 * translation vector.
		 * @param x The x-component of the translation vector.
		 * @param y The y-component of the translation vector.
		 * @param z The z-component of the translation vector.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToTranslation(FP x, FP y, FP z)
		{
			idt();
			this.m03 = x;
			this.m13 = y;
			this.m23 = z;
			return this;
		}

		/** Sets this matrix to a translation and scaling matrix by first overwriting it with an identity and then setting the
		 * translation vector in the 4th column and the scaling vector in the diagonal.
		 * @param translation The translation vector
		 * @param scaling The scaling vector
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToTranslationAndScaling(FPVector3 translation, FPVector3 scaling)
		{
			idt();
			this.m03 = translation.x;
			this.m13 = translation.y;
			this.m23 = translation.z;
			this.m00 = scaling.x;
			this.m11 = scaling.y;
			this.m22 = scaling.z;
			return this;
		}

		/** Sets this matrix to a translation and scaling matrix by first overwriting it with an identity and then setting the
		 * translation vector in the 4th column and the scaling vector in the diagonal.
		 * @param translationX The x-component of the translation vector
		 * @param translationY The y-component of the translation vector
		 * @param translationZ The z-component of the translation vector
		 * @param scalingX The x-component of the scaling vector
		 * @param scalingY The x-component of the scaling vector
		 * @param scalingZ The x-component of the scaling vector
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToTranslationAndScaling(FP translationX, FP translationY,
			FP translationZ, FP scalingX,
			FP scalingY, FP scalingZ)
		{
			idt();
			this.m03 = translationX;
			this.m13 = translationY;
			this.m23 = translationZ;
			this.m00 = scalingX;
			this.m11 = scalingY;
			this.m22 = scalingZ;
			return this;
		}

		/** Sets the matrix to a rotation matrix around the given axis.
		 * @param axis The axis
		 * @param degrees The angle in degrees
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToRotation(FPVector3 axis, FP degrees)
		{
			if (degrees == 0)
			{
				idt();
				return this;
			}

			return set(quat.set(axis, degrees));
		}

		/** Sets the matrix to a rotation matrix around the given axis.
		 * @param axis The axis
		 * @param radians The angle in radians
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToRotationRad(FPVector3 axis, FP radians)
		{
			if (radians == 0)
			{
				idt();
				return this;
			}

			return set(quat.setFromAxisRad(axis, radians));
		}

		/** Sets the matrix to a rotation matrix around the given axis.
		 * @param axisX The x-component of the axis
		 * @param axisY The y-component of the axis
		 * @param axisZ The z-component of the axis
		 * @param degrees The angle in degrees
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToRotation(FP axisX, FP axisY, FP axisZ, FP degrees)
		{
			if (degrees == 0)
			{
				idt();
				return this;
			}

			return set(quat.setFromAxis(axisX, axisY, axisZ, degrees));
		}

		/** Sets the matrix to a rotation matrix around the given axis.
		 * @param axisX The x-component of the axis
		 * @param axisY The y-component of the axis
		 * @param axisZ The z-component of the axis
		 * @param radians The angle in radians
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToRotationRad(FP axisX, FP axisY, FP axisZ,
			FP radians)
		{
			if (radians == 0)
			{
				idt();
				return this;
			}

			return set(quat.setFromAxisRad(axisX, axisY, axisZ, radians));
		}

		/** Set the matrix to a rotation matrix between two vectors.
		 * @param v1 The base vector
		 * @param v2 The target vector
		 * @return This matrix for the purpose of chaining methods together */
		public FPMatrix4x4 setToRotation(FPVector3 v1, FPVector3 v2)
		{
			return set(quat.setFromCross(v1, v2));
		}

		/** Set the matrix to a rotation matrix between two vectors.
		 * @param x1 The base vectors x value
		 * @param y1 The base vectors y value
		 * @param z1 The base vectors z value
		 * @param x2 The target vector x value
		 * @param y2 The target vector y value
		 * @param z2 The target vector z value
		 * @return This matrix for the purpose of chaining methods together */
		public FPMatrix4x4 setToRotation(FP x1, FP y1, FP z1, FP x2,
			FP y2, FP z2)
		{
			return set(quat.setFromCross(x1, y1, z1, x2, y2, z2));
		}

		/** Sets this matrix to a rotation matrix from the given euler angles.
		 * @param yaw the yaw in degrees
		 * @param pitch the pitch in degrees
		 * @param roll the roll in degrees
		 * @return This matrix */
		public FPMatrix4x4 setFromEulerAngles(FP yaw, FP pitch, FP roll)
		{
			quat.setEulerAngles(yaw, pitch, roll);
			return set(quat);
		}

		/** Sets this matrix to a rotation matrix from the given euler angles.
		 * @param yaw the yaw in radians
		 * @param pitch the pitch in radians
		 * @param roll the roll in radians
		 * @return This matrix */
		public FPMatrix4x4 setFromEulerAnglesRad(FP yaw, FP pitch, FP roll)
		{
			quat.setEulerAnglesRad(yaw, pitch, roll);
			return set(quat);
		}

		/** Sets this matrix to a scaling matrix
		 * @param vector The scaling vector
		 * @return This matrix for chaining. */
		public FPMatrix4x4 setToScaling(FPVector3 vector)
		{
			idt();
			this.m00 = vector.x;
			this.m11 = vector.y;
			this.m22 = vector.z;
			return this;
		}

		/** Sets this matrix to a scaling matrix
		 * @param x The x-component of the scaling vector
		 * @param y The y-component of the scaling vector
		 * @param z The z-component of the scaling vector
		 * @return This matrix for chaining. */
		public FPMatrix4x4 setToScaling(FP x, FP y, FP z)
		{
			idt();
			this.m00 = x;
			this.m11 = y;
			this.m22 = z;
			return this;
		}

		/** Sets the matrix to a look at matrix with a direction and an up vector. Multiply with a translation matrix to get a camera
		 * model view matrix.
		 * @param direction The direction vector
		 * @param up The up vector
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 setToLookAt(FPVector3 direction, FPVector3 up)
		{
			l_vez = l_vez.set(direction).nor();
			l_vex = -l_vex.set(l_vez).crs(up).nor();
			l_vey = -l_vey.set(l_vex).crs(l_vez).nor();
			idt();
			this.m00 = l_vex.x;
			this.m10 = l_vex.y;
			this.m20 = l_vex.z;
			this.m01 = l_vey.x;
			this.m11 = l_vey.y;
			this.m21 = l_vey.z;
			this.m02 = l_vez.x;
			this.m12 = l_vez.y;
			this.m22 = l_vez.z;
			return this;
		}

		/** Sets this matrix to a look at matrix with the given position, target and up vector.
		 * @param position the position
		 * @param target the target
		 * @param up the up vector
		 * @return This matrix */
		public FPMatrix4x4 setToLookAt(FPVector3 position, FPVector3 target, FPVector3 up)
		{
			tmpVec = tmpVec.set(target).sub(position);
			setToLookAt(tmpVec, up);
			mulLeft(tmpMat.setToTranslation(position.x, position.y, position.z));
			return this;
		}

		//http://gsteph.blogspot.com/2012/05/world-view-and-projection-matrix.html
		public FPMatrix4x4 setToWorld(FPVector3 position, FPVector3 forward, FPVector3 up)
		{
			//		UnityEngine.Debug.LogWarning($"forword:{forward.x}  {forward.y}  {forward.z} position:{position.x} {position.y} {position.z}");
			tmpForward = tmpForward.set(forward).nor();
			//		UnityEngine.Debug.LogWarning(tmpForward);
			right = -right.set(tmpForward).crs(up).nor();
			//		UnityEngine.Debug.LogWarning(right);
			tmpUp = -tmpUp.set(right).crs(tmpForward).nor();
			//		UnityEngine.Debug.LogWarning($"right:{right.x}  {right.y}  {right.z} tmpUp:{tmpUp.x}  {tmpUp.y}  {tmpUp.z} tmpForward:{tmpForward.x}  {tmpForward.y}  {tmpForward.z}");
			//		set(right, tmpUp, tmpForward.scl(-(DGFixedPoint) 1), position);
			set(right, tmpUp, tmpForward, position);
			return this;
		}

		/** Linearly interpolates between this matrix and the given matrix mixing by alpha
		 * @param matrix the matrix
		 * @param alpha the alpha value in the range [0,1]
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 lerp(FPMatrix4x4 matrix, FP alpha)
		{
			this.m00 = this.m00 * (1 - alpha) + matrix.m00 * alpha;
			this.m10 = this.m10 * (1 - alpha) + matrix.m10 * alpha;
			this.m20 = this.m20 * (1 - alpha) + matrix.m20 * alpha;
			this.m30 = this.m30 * (1 - alpha) + matrix.m30 * alpha;

			this.m01 = this.m01 * (1 - alpha) + matrix.m01 * alpha;
			this.m11 = this.m11 * (1 - alpha) + matrix.m11 * alpha;
			this.m21 = this.m21 * (1 - alpha) + matrix.m21 * alpha;
			this.m31 = this.m31 * (1 - alpha) + matrix.m31 * alpha;

			this.m02 = this.m02 * (1 - alpha) + matrix.m02 * alpha;
			this.m12 = this.m12 * (1 - alpha) + matrix.m12 * alpha;
			this.m22 = this.m22 * (1 - alpha) + matrix.m22 * alpha;
			this.m32 = this.m32 * (1 - alpha) + matrix.m32 * alpha;

			this.m03 = this.m03 * (1 - alpha) + matrix.m03 * alpha;
			this.m13 = this.m13 * (1 - alpha) + matrix.m13 * alpha;
			this.m23 = this.m23 * (1 - alpha) + matrix.m23 * alpha;
			this.m33 = this.m33 * (1 - alpha) + matrix.m33 * alpha;

			return this;
		}

		/** Averages the given transform with this one and stores the result in this matrix. Translations and scales are lerped while
		 * rotations are slerped.
		 * @param other The other transform
		 * @param w Weight of this transform; weight of the other transform is (1 - w)
		 * @return This matrix for chaining */
		public FPMatrix4x4 avg(FPMatrix4x4 other, FP w)
		{
			getScale(ref tmpVec);
			other.getScale(ref tmpForward);

			getRotation(ref quat);
			other.getRotation(ref quat2);

			getTranslation(ref tmpUp);
			other.getTranslation(ref right);

			setToScaling(tmpVec.scl(w).add(tmpForward.scl(1 - w)));
			rotate(quat.slerp(quat2, 1 - w));
			setTranslation(tmpUp.scl(w).add(right.scl(1 - w)));
			return this;
		}

		/** Averages the given transforms and stores the result in this matrix. Translations and scales are lerped while rotations are
		 * slerped. Does not destroy the data contained in t.
		 * @param t List of transforms
		 * @return This matrix for chaining */
		public FPMatrix4x4 avg(FPMatrix4x4[] t)
		{
			FP w = 1.0f / t.Length;

			tmpVec = tmpVec.set(t[0].getScale(ref tmpUp).scl(w));
			quat.set(t[0].getRotation(ref quat2).exp(w));
			tmpForward = tmpForward.set(t[0].getTranslation(ref tmpUp).scl(w));

			for (int i = 1; i < t.Length; i++)
			{
				tmpVec = tmpVec.add(t[i].getScale(ref tmpUp).scl(w));
				quat = quat.mul(t[i].getRotation(ref quat2).exp(w));
				tmpForward = tmpForward.add(t[i].getTranslation(ref tmpUp).scl(w));
			}

			quat = quat.nor();

			setToScaling(tmpVec);
			rotate(quat);
			setTranslation(tmpForward);
			return this;
		}

		/** Averages the given transforms with the given weights and stores the result in this matrix. Translations and scales are
		 * lerped while rotations are slerped. Does not destroy the data contained in t or w; Sum of w_i must be equal to 1, or
		 * unexpected results will occur.
		 * @param t List of transforms
		 * @param w List of weights
		 * @return This matrix for chaining */
		public FPMatrix4x4 avg(FPMatrix4x4[] t, FP[] w)
		{
			tmpVec = tmpVec.set(t[0].getScale(ref tmpUp).scl(w[0]));
			quat = quat.set(t[0].getRotation(ref quat2).exp(w[0]));
			tmpForward = tmpForward.set(t[0].getTranslation(ref tmpUp).scl(w[0]));

			for (int i = 1; i < t.Length; i++)
			{
				tmpVec = tmpVec.add(t[i].getScale(ref tmpUp).scl(w[i]));
				quat = quat.mul(t[i].getRotation(ref quat2).exp(w[i]));
				tmpForward = tmpForward.add(t[i].getTranslation(ref tmpUp).scl(w[i]));
			}

			quat = quat.nor();

			setToScaling(tmpVec);
			rotate(quat);
			setTranslation(tmpForward);
			return this;
		}

		/** Sets this matrix to the given 3x3 matrix. The third column of this matrix is set to (0,0,1,0).
		 * @param mat the matrix */
		public FPMatrix4x4 set(FPMatrix3x3 mat)
		{

			m00 = mat.m00;
			m01 = mat.m01;
			m02 = mat.m02;
			m03 = 0;

			m10 = mat.m10;
			m11 = mat.m11;
			m12 = mat.m12;
			m13 = 0;

			m20 = mat.m20;
			m21 = mat.m21;
			m22 = mat.m22;
			m23 = 0;

			m30 = 0;
			m31 = 0;
			m32 = 0;
			m33 = 1;
			return this;
		}

		/** Sets this matrix to the given affine matrix. The values are mapped as follows:
		 *
		 * <pre>
		 *      [  M00  M01   0   M02  ]
		 *      [  M10  M11   0   M12  ]
		 *      [   0    0    1    0   ]
		 *      [   0    0    0    1   ]
		 * </pre>
		 * 
		 * @param affine the affine matrix
		 * @return This matrix for chaining */
		public FPMatrix4x4 set(FPAffine2 affine)
		{
			this.m00 = affine.m00;
			this.m10 = affine.m10;
			this.m20 = 0;
			this.m30 = 0;
			this.m01 = affine.m01;
			this.m11 = affine.m11;
			this.m21 = 0;
			this.m31 = 0;
			this.m02 = 0;
			this.m12 = 0;
			this.m22 = 1;
			this.m32 = 0;
			this.m03 = affine.m02;
			this.m13 = affine.m12;
			this.m23 = 0;
			this.m33 = 1;
			return this;
		}

		/** Assumes that this matrix is a 2D affine transformation, copying only the relevant components. The values are mapped as
		 * follows:
		 *
		 * <pre>
		 *      [  M00  M01   _   M02  ]
		 *      [  M10  M11   _   M12  ]
		 *      [   _    _    _    _   ]
		 *      [   _    _    _    _   ]
		 * </pre>
		 * 
		 * @param affine the source matrix
		 * @return This matrix for chaining */
		public FPMatrix4x4 setAsAffine(FPAffine2 affine)
		{
			this.m00 = affine.m00;
			this.m10 = affine.m10;
			this.m01 = affine.m01;
			this.m11 = affine.m11;
			this.m03 = affine.m02;
			this.m13 = affine.m12;
			return this;
		}

		/** Assumes that both matrices are 2D affine transformations, copying only the relevant components. The copied values are:
		 *
		 * <pre>
		 *      [  M00  M01   _   M03  ]
		 *      [  M10  M11   _   M13  ]
		 *      [   _    _    _    _   ]
		 *      [   _    _    _    _   ]
		 * </pre>
		 * 
		 * @param mat the source matrix
		 * @return This matrix for chaining */
		public FPMatrix4x4 setAsAffine(FPMatrix4x4 mat)
		{
			this.m00 = mat.m00;
			this.m10 = mat.m10;
			this.m01 = mat.m01;
			this.m11 = mat.m11;
			this.m03 = mat.m03;
			this.m13 = mat.m13;
			return this;
		}

		public FPMatrix4x4 scl(FPVector3 scale)
		{
			this.m00 *= scale.x;
			this.m11 *= scale.y;
			this.m22 *= scale.z;
			return this;
		}

		public FPMatrix4x4 scl(FP x, FP y, FP z)
		{
			this.m00 *= x;
			this.m11 *= y;
			this.m22 *= z;
			return this;
		}

		public FPMatrix4x4 scl(FP scale)
		{
			this.m00 *= scale;
			this.m11 *= scale;
			this.m22 *= scale;
			return this;
		}

		public FPVector3 getTranslation(ref FPVector3 position)
		{
			position.x = this.m03;
			position.y = this.m13;
			position.z = this.m23;
			return position;
		}

		/** Gets the rotation of this matrix.
		 * @param rotation The {@link Quaternion} to receive the rotation
		 * @param normalizeAxes True to normalize the axes, necessary when the matrix might also include scaling.
		 * @return The provided {@link Quaternion} for chaining. */
		public FPQuaternion getRotation(ref FPQuaternion rotation, bool normalizeAxes)
		{
			return rotation.setFromMatrix(normalizeAxes, this);
		}

		/** Gets the rotation of this matrix.
		 * @param rotation The {@link Quaternion} to receive the rotation
		 * @return The provided {@link Quaternion} for chaining. */
		public FPQuaternion getRotation(ref FPQuaternion rotation)
		{
			return rotation.setFromMatrix(this);
		}

		/** @return the squared scale factor on the X axis */
		public FP getScaleXSquared()
		{
			return this.m00 * this.m00 + this.m10 * this.m10 + this.m20 * this.m20;
		}

		/** @return the squared scale factor on the Y axis */
		public FP getScaleYSquared()
		{
			return this.m01 * this.m01 + this.m11 * this.m11 + this.m21 * this.m21;
		}

		/** @return the squared scale factor on the Z axis */
		public FP getScaleZSquared()
		{
			return this.m02 * this.m02 + this.m12 * this.m12 + this.m22 * this.m22;
		}

		/** @return the scale factor on the X axis (non-negative) */
		public FP getScaleX()
		{
			//		UnityEngine.Debug.LogWarning((DGFixedPointMath.IsZero(val[M01]) && DGFixedPointMath.IsZero(val[M02]))
			//			? DGFixedPointMath.Abs(val[M00])
			//			: DGFixedPointMath.Sqrt(getScaleXSquared()));
			return (FPMath.IsZero(this.m10) && FPMath.IsZero(this.m20))
				? FPMath.Abs(this.m00)
				: FPMath.Sqrt(getScaleXSquared());
		}

		/** @return the scale factor on the Y axis (non-negative) */
		public FP getScaleY()
		{
			return (FPMath.IsZero(this.m01) && FPMath.IsZero(this.m21))
				? FPMath.Abs(this.m11)
				: FPMath.Sqrt(getScaleYSquared());
		}

		/** @return the scale factor on the X axis (non-negative) */
		public FP getScaleZ()
		{
			return (FPMath.IsZero(this.m02) && FPMath.IsZero(this.m12))
				? FPMath.Abs(this.m22)
				: FPMath.Sqrt(getScaleZSquared());
		}

		/** @param scale The vector which will receive the (non-negative) scale components on each axis.
		 * @return The provided vector for chaining. */
		public FPVector3 getScale(ref FPVector3 scale)
		{
			return scale.set(getScaleX(), getScaleY(), getScaleZ());
		}

		/** removes the translational part and transposes the matrix. */
		public FPMatrix4x4 toNormalMatrix()
		{
			this.m03 = 0;
			this.m13 = 0;
			this.m23 = 0;
			return inv().tra();
		}

		public override string ToString()
		{
			return "[" + this.m00 + "|" + this.m01 + "|" + this.m02 + "|" + this.m03 + "]\n" //
				   + "[" + this.m10 + "|" + this.m11 + "|" + this.m12 + "|" + this.m13 + "]\n" //
				   + "[" + this.m20 + "|" + this.m21 + "|" + this.m22 + "|" + this.m23 + "]\n" //
				   + "[" + this.m30 + "|" + this.m31 + "|" + this.m32 + "|" + this.m33 + "]\n";
		}

		static void matrix4_mul(FP[] mata, FP[] matb)
		{
			FP[] tmp = new FP[Count];

			tmp[FPMatrix4x4.M00Index] = mata[FPMatrix4x4.M00Index] * matb[FPMatrix4x4.M00Index] + mata[FPMatrix4x4.M01Index] * matb[FPMatrix4x4.M10Index] + mata[FPMatrix4x4.M02Index] * matb[FPMatrix4x4.M20Index] + mata[FPMatrix4x4.M03Index] * matb[FPMatrix4x4.M30Index];
			tmp[FPMatrix4x4.M01Index] = mata[FPMatrix4x4.M00Index] * matb[FPMatrix4x4.M01Index] + mata[FPMatrix4x4.M01Index] * matb[FPMatrix4x4.M11Index] + mata[FPMatrix4x4.M02Index] * matb[FPMatrix4x4.M21Index] + mata[FPMatrix4x4.M03Index] * matb[FPMatrix4x4.M31Index];
			tmp[FPMatrix4x4.M02Index] = mata[FPMatrix4x4.M00Index] * matb[FPMatrix4x4.M02Index] + mata[FPMatrix4x4.M01Index] * matb[FPMatrix4x4.M12Index] + mata[FPMatrix4x4.M02Index] * matb[FPMatrix4x4.M22Index] + mata[FPMatrix4x4.M03Index] * matb[FPMatrix4x4.M32Index];
			tmp[FPMatrix4x4.M03Index] = mata[FPMatrix4x4.M00Index] * matb[FPMatrix4x4.M03Index] + mata[FPMatrix4x4.M01Index] * matb[FPMatrix4x4.M13Index] + mata[FPMatrix4x4.M02Index] * matb[FPMatrix4x4.M23Index] + mata[FPMatrix4x4.M03Index] * matb[FPMatrix4x4.M33Index];
			tmp[FPMatrix4x4.M10Index] = mata[FPMatrix4x4.M10Index] * matb[FPMatrix4x4.M00Index] + mata[FPMatrix4x4.M11Index] * matb[FPMatrix4x4.M10Index] + mata[FPMatrix4x4.M12Index] * matb[FPMatrix4x4.M20Index] + mata[FPMatrix4x4.M13Index] * matb[FPMatrix4x4.M30Index];
			tmp[FPMatrix4x4.M11Index] = mata[FPMatrix4x4.M10Index] * matb[FPMatrix4x4.M01Index] + mata[FPMatrix4x4.M11Index] * matb[FPMatrix4x4.M11Index] + mata[FPMatrix4x4.M12Index] * matb[FPMatrix4x4.M21Index] + mata[FPMatrix4x4.M13Index] * matb[FPMatrix4x4.M31Index];
			tmp[FPMatrix4x4.M12Index] = mata[FPMatrix4x4.M10Index] * matb[FPMatrix4x4.M02Index] + mata[FPMatrix4x4.M11Index] * matb[FPMatrix4x4.M12Index] + mata[FPMatrix4x4.M12Index] * matb[FPMatrix4x4.M22Index] + mata[FPMatrix4x4.M13Index] * matb[FPMatrix4x4.M32Index];
			tmp[FPMatrix4x4.M13Index] = mata[FPMatrix4x4.M10Index] * matb[FPMatrix4x4.M03Index] + mata[FPMatrix4x4.M11Index] * matb[FPMatrix4x4.M13Index] + mata[FPMatrix4x4.M12Index] * matb[FPMatrix4x4.M23Index] + mata[FPMatrix4x4.M13Index] * matb[FPMatrix4x4.M33Index];
			tmp[FPMatrix4x4.M20Index] = mata[FPMatrix4x4.M20Index] * matb[FPMatrix4x4.M00Index] + mata[FPMatrix4x4.M21Index] * matb[FPMatrix4x4.M10Index] + mata[FPMatrix4x4.M22Index] * matb[FPMatrix4x4.M20Index] + mata[FPMatrix4x4.M23Index] * matb[FPMatrix4x4.M30Index];
			tmp[FPMatrix4x4.M21Index] = mata[FPMatrix4x4.M20Index] * matb[FPMatrix4x4.M01Index] + mata[FPMatrix4x4.M21Index] * matb[FPMatrix4x4.M11Index] + mata[FPMatrix4x4.M22Index] * matb[FPMatrix4x4.M21Index] + mata[FPMatrix4x4.M23Index] * matb[FPMatrix4x4.M31Index];
			tmp[FPMatrix4x4.M22Index] = mata[FPMatrix4x4.M20Index] * matb[FPMatrix4x4.M02Index] + mata[FPMatrix4x4.M21Index] * matb[FPMatrix4x4.M12Index] + mata[FPMatrix4x4.M22Index] * matb[FPMatrix4x4.M22Index] + mata[FPMatrix4x4.M23Index] * matb[FPMatrix4x4.M32Index];
			tmp[FPMatrix4x4.M23Index] = mata[FPMatrix4x4.M20Index] * matb[FPMatrix4x4.M03Index] + mata[FPMatrix4x4.M21Index] * matb[FPMatrix4x4.M13Index] + mata[FPMatrix4x4.M22Index] * matb[FPMatrix4x4.M23Index] + mata[FPMatrix4x4.M23Index] * matb[FPMatrix4x4.M33Index];
			tmp[FPMatrix4x4.M30Index] = mata[FPMatrix4x4.M30Index] * matb[FPMatrix4x4.M00Index] + mata[FPMatrix4x4.M31Index] * matb[FPMatrix4x4.M10Index] + mata[FPMatrix4x4.M32Index] * matb[FPMatrix4x4.M20Index] + mata[FPMatrix4x4.M33Index] * matb[FPMatrix4x4.M30Index];
			tmp[FPMatrix4x4.M31Index] = mata[FPMatrix4x4.M30Index] * matb[FPMatrix4x4.M01Index] + mata[FPMatrix4x4.M31Index] * matb[FPMatrix4x4.M11Index] + mata[FPMatrix4x4.M32Index] * matb[FPMatrix4x4.M21Index] + mata[FPMatrix4x4.M33Index] * matb[FPMatrix4x4.M31Index];
			tmp[FPMatrix4x4.M32Index] = mata[FPMatrix4x4.M30Index] * matb[FPMatrix4x4.M02Index] + mata[FPMatrix4x4.M31Index] * matb[FPMatrix4x4.M12Index] + mata[FPMatrix4x4.M32Index] * matb[FPMatrix4x4.M22Index] + mata[FPMatrix4x4.M33Index] * matb[FPMatrix4x4.M32Index];
			tmp[FPMatrix4x4.M33Index] = mata[FPMatrix4x4.M30Index] * matb[FPMatrix4x4.M03Index] + mata[FPMatrix4x4.M31Index] * matb[FPMatrix4x4.M13Index] + mata[FPMatrix4x4.M32Index] * matb[FPMatrix4x4.M23Index] + mata[FPMatrix4x4.M33Index] * matb[FPMatrix4x4.M33Index];
			Array.Copy(tmp, 0, mata, 0, Count);
		}


		public static void prj(FP[] mat, FP[] vecs, int offset, int numVecs, int stride)
		{
			var vecsStartIndex = offset;
			for (int i = 0; i < numVecs; i++)
			{
				matrix4_proj(mat, vecs, vecsStartIndex);
				vecsStartIndex += stride;
			}
		}

		public static void matrix4_proj(FP[] mat, FP[] vec, int vecStartIndex)
		{
			int index0 = vecStartIndex + 0;
			int index1 = vecStartIndex + 1;
			int index2 = vecStartIndex + 2;

			FP inv_w = 1.0f /
								 (vec[index0] * mat[M30Index] + vec[index1] * mat[M31Index] + vec[index2] * mat[M32Index] + mat[M33Index]);
			FP x = (vec[index0] * mat[M00Index] + vec[index1] * mat[M01Index] + vec[index2] * mat[M02Index] + mat[M03Index]) *
							 inv_w;
			FP y = (vec[index0] * mat[M10Index] + vec[index1] * mat[M11Index] + vec[index2] * mat[M12Index] + mat[M13Index]) *
							 inv_w;
			FP z = (vec[index0] * mat[M20Index] + vec[index1] * mat[M21Index] + vec[index2] * mat[M22Index] + mat[M23Index]) *
							 inv_w;
			vec[index0] = x;
			vec[index1] = y;
			vec[index2] = z;
		}

		/** Multiplies the vectors with the given matrix. The matrix array is assumed to hold a 4x4 column major matrix as you can get
		 * from {@link Matrix4#val}. The vectors array is assumed to hold 3-component vectors. Offset specifies the offset into the
		 * array where the x-component of the first vector is located. The numVecs parameter specifies the number of vectors stored in
		 * the vectors array. The stride parameter specifies the number of floats between subsequent vectors and must be >= 3. This is
		 * the same as {@link Vector3#mul(Matrix4)} applied to multiple vectors.
		 * @param mat the matrix
		 * @param vecs the vectors
		 * @param offset the offset into the vectors array
		 * @param numVecs the number of vectors
		 * @param stride the stride between vectors in floats */
		public static void mulVec(FP[] mat, FP[] vecs, int offset, int numVecs, int stride)
		{
			int vecStartIndex = offset;
			for (int i = 0; i < numVecs; i++)
			{
				matrix4_mulVec(mat, vecs, vecStartIndex);
				vecStartIndex += stride;
			}
		}

		static void matrix4_mulVec(FP[] mat, FP[] vec, int vecStartIndex)
		{
			int index0 = vecStartIndex + 0;
			int index1 = vecStartIndex + 1;
			int index2 = vecStartIndex + 2;

			FP x = vec[index0] * mat[FPMatrix4x4.M00Index] + vec[index1] * mat[FPMatrix4x4.M01Index] + vec[index2] * mat[FPMatrix4x4.M02Index] + mat[FPMatrix4x4.M03Index];
			FP y = vec[index0] * mat[FPMatrix4x4.M10Index] + vec[index1] * mat[FPMatrix4x4.M11Index] + vec[index2] * mat[FPMatrix4x4.M12Index] + mat[FPMatrix4x4.M13Index];
			FP z = vec[index0] * mat[FPMatrix4x4.M20Index] + vec[index1] * mat[FPMatrix4x4.M21Index] + vec[index2] * mat[FPMatrix4x4.M22Index] + mat[FPMatrix4x4.M23Index];
			vec[index0] = x;
			vec[index1] = y;
			vec[index2] = z;
		}

		/** Multiplies the vectors with the top most 3x3 sub-matrix of the given matrix. The matrix array is assumed to hold a 4x4
		 * column major matrix as you can get from {@link Matrix4#val}. The vectors array is assumed to hold 3-component vectors.
		 * Offset specifies the offset into the array where the x-component of the first vector is located. The numVecs parameter
		 * specifies the number of vectors stored in the vectors array. The stride parameter specifies the number of floats between
		 * subsequent vectors and must be >= 3. This is the same as {@link Vector3#rot(Matrix4)} applied to multiple vectors.
		 * @param mat the matrix
		 * @param vecs the vectors
		 * @param offset the offset into the vectors array
		 * @param numVecs the number of vectors
		 * @param stride the stride between vectors in floats */
		public static void rot(FP[] mat, FP[] vecs, int offset, int numVecs, int stride)
		{
			int vecsStartIndex = offset;
			for (int i = 0; i < numVecs; i++)
			{
				matrix4_rot(mat, vecs, vecsStartIndex);
				vecsStartIndex += stride;
			}
		}

		static void matrix4_rot(FP[] mat, FP[] vec, int vecStartIndex)
		{
			int index0 = vecStartIndex + 0;
			int index1 = vecStartIndex + 1;
			int index2 = vecStartIndex + 2;

			FP x = vec[index0] * mat[FPMatrix4x4.M00Index] + vec[index1] * mat[FPMatrix4x4.M01Index] + vec[index2] * mat[FPMatrix4x4.M02Index];
			FP y = vec[index0] * mat[FPMatrix4x4.M10Index] + vec[index1] * mat[FPMatrix4x4.M11Index] + vec[index2] * mat[FPMatrix4x4.M12Index];
			FP z = vec[index0] * mat[FPMatrix4x4.M20Index] + vec[index1] * mat[FPMatrix4x4.M21Index] + vec[index2] * mat[FPMatrix4x4.M22Index];
			vec[index0] = x;
			vec[index1] = y;
			vec[index2] = z;
		}

		/*************************************************************************************
		* 模块描述:
		*************************************************************************************/

		public static void mul(FP[] mata, FP[] matb)
		{
			FP m00 = mata[M00Index] * matb[M00Index] + mata[M01Index] * matb[M10Index] +
							   mata[M02Index] * matb[M20Index] + mata[M03Index] * matb[M30Index];
			FP m01 = mata[M00Index] * matb[M01Index] + mata[M01Index] * matb[M11Index] +
							   mata[M02Index] * matb[M21Index] + mata[M03Index] * matb[M31Index];
			FP m02 = mata[M00Index] * matb[M02Index] + mata[M01Index] * matb[M12Index] +
							   mata[M02Index] * matb[M22Index] + mata[M03Index] * matb[M32Index];
			FP m03 = mata[M00Index] * matb[M03Index] + mata[M01Index] * matb[M13Index] +
							   mata[M02Index] * matb[M23Index] + mata[M03Index] * matb[M33Index];
			FP m10 = mata[M10Index] * matb[M00Index] + mata[M11Index] * matb[M10Index] +
							   mata[M12Index] * matb[M20Index] + mata[M13Index] * matb[M30Index];
			FP m11 = mata[M10Index] * matb[M01Index] + mata[M11Index] * matb[M11Index] +
							   mata[M12Index] * matb[M21Index] + mata[M13Index] * matb[M31Index];
			FP m12 = mata[M10Index] * matb[M02Index] + mata[M11Index] * matb[M12Index] +
							   mata[M12Index] * matb[M22Index] + mata[M13Index] * matb[M32Index];
			FP m13 = mata[M10Index] * matb[M03Index] + mata[M11Index] * matb[M13Index] +
							   mata[M12Index] * matb[M23Index] + mata[M13Index] * matb[M33Index];
			FP m20 = mata[M20Index] * matb[M00Index] + mata[M21Index] * matb[M10Index] +
							   mata[M22Index] * matb[M20Index] + mata[M23Index] * matb[M30Index];
			FP m21 = mata[M20Index] * matb[M01Index] + mata[M21Index] * matb[M11Index] +
							   mata[M22Index] * matb[M21Index] + mata[M23Index] * matb[M31Index];
			FP m22 = mata[M20Index] * matb[M02Index] + mata[M21Index] * matb[M12Index] +
							   mata[M22Index] * matb[M22Index] + mata[M23Index] * matb[M32Index];
			FP m23 = mata[M20Index] * matb[M03Index] + mata[M21Index] * matb[M13Index] +
							   mata[M22Index] * matb[M23Index] + mata[M23Index] * matb[M33Index];
			FP m30 = mata[M30Index] * matb[M00Index] + mata[M31Index] * matb[M10Index] +
							   mata[M32Index] * matb[M20Index] + mata[M33Index] * matb[M30Index];
			FP m31 = mata[M30Index] * matb[M01Index] + mata[M31Index] * matb[M11Index] +
							   mata[M32Index] * matb[M21Index] + mata[M33Index] * matb[M31Index];
			FP m32 = mata[M30Index] * matb[M02Index] + mata[M31Index] * matb[M12Index] +
							   mata[M32Index] * matb[M22Index] + mata[M33Index] * matb[M32Index];
			FP m33 = mata[M30Index] * matb[M03Index] + mata[M31Index] * matb[M13Index] +
							   mata[M32Index] * matb[M23Index] + mata[M33Index] * matb[M33Index];
			mata[M00Index] = m00;
			mata[M10Index] = m10;
			mata[M20Index] = m20;
			mata[M30Index] = m30;
			mata[M01Index] = m01;
			mata[M11Index] = m11;
			mata[M21Index] = m21;
			mata[M31Index] = m31;
			mata[M02Index] = m02;
			mata[M12Index] = m12;
			mata[M22Index] = m22;
			mata[M32Index] = m32;
			mata[M03Index] = m03;
			mata[M13Index] = m13;
			mata[M23Index] = m23;
			mata[M33Index] = m33;
		}

		/** Multiplies the vector with the given matrix. The matrix array is assumed to hold a 4x4 column major matrix as you can get
		 * from {@link Matrix4#val}. The vector array is assumed to hold a 3-component vector, with x being the first element, y being
		 * the second and z being the last component. The result is stored in the vector array. This is the same as
		 * {@link Vector3#mul(Matrix4)}.
		 * @param mat the matrix
		 * @param vec the vector. */
		public static void mulVec(FP[] mat, FP[] vec)
		{
			FP x = vec[0] * mat[M00Index] + vec[1] * mat[M01Index] + vec[2] * mat[M02Index] + mat[M03Index];
			FP y = vec[0] * mat[M10Index] + vec[1] * mat[M11Index] + vec[2] * mat[M12Index] + mat[M13Index];
			FP z = vec[0] * mat[M20Index] + vec[1] * mat[M21Index] + vec[2] * mat[M22Index] + mat[M23Index];
			vec[0] = x;
			vec[1] = y;
			vec[2] = z;
		}

		/** Multiplies the vector with the given matrix, performing a division by w. The matrix array is assumed to hold a 4x4 column
		 * major matrix as you can get from {@link Matrix4#val}. The vector array is assumed to hold a 3-component vector, with x being
		 * the first element, y being the second and z being the last component. The result is stored in the vector array. This is the
		 * same as {@link Vector3#prj(Matrix4)}.
		 * @param mat the matrix
		 * @param vec the vector. */
		public static void prj(FP[] mat, FP[] vec)
		{
			FP inv_w = 1.0f /
								 (vec[0] * mat[M30Index] + vec[1] * mat[M31Index] + vec[2] * mat[M32Index] + mat[M33Index]);
			FP x = (vec[0] * mat[M00Index] + vec[1] * mat[M01Index] + vec[2] * mat[M02Index] + mat[M03Index]) *
							 inv_w;
			FP y = (vec[0] * mat[M10Index] + vec[1] * mat[M11Index] + vec[2] * mat[M12Index] + mat[M13Index]) *
							 inv_w;
			FP z = (vec[0] * mat[M20Index] + vec[1] * mat[M21Index] + vec[2] * mat[M22Index] + mat[M23Index]) *
							 inv_w;
			vec[0] = x;
			vec[1] = y;
			vec[2] = z;
		}




		/** Multiplies the vector with the top most 3x3 sub-matrix of the given matrix. The matrix array is assumed to hold a 4x4
		 * column major matrix as you can get from {@link Matrix4#val}. The vector array is assumed to hold a 3-component vector, with
		 * x being the first element, y being the second and z being the last component. The result is stored in the vector array. This
		 * is the same as {@link Vector3#rot(Matrix4)}.
		 * @param mat the matrix
		 * @param vec the vector. */
		public static void rot(FP[] mat, FP[] vec)
		{
			FP x = vec[0] * mat[M00Index] + vec[1] * mat[M01Index] + vec[2] * mat[M02Index];
			FP y = vec[0] * mat[M10Index] + vec[1] * mat[M11Index] + vec[2] * mat[M12Index];
			FP z = vec[0] * mat[M20Index] + vec[1] * mat[M21Index] + vec[2] * mat[M22Index];
			vec[0] = x;
			vec[1] = y;
			vec[2] = z;
		}

		/** Computes the inverse of the given matrix. The matrix array is assumed to hold a 4x4 column major matrix as you can get from
		 * {@link Matrix4#val}.
		 * @param values the matrix values.
		 * @return false in case the inverse could not be calculated, true otherwise. */
		public static bool inv(FP[] values)
		{
			FP l_det = det(values);
			if (l_det == 0) return false;
			FP m00 = values[M12Index] * values[M23Index] * values[M31Index] -
							   values[M13Index] * values[M22Index] * values[M31Index]
							   + values[M13Index] * values[M21Index] * values[M32Index] - values[M11Index] *
																						values[M23Index] * values[M32Index]
																						- values[M12Index] *
																						values[M21Index] *
																						values[M33Index] +
							   values[M11Index] * values[M22Index] * values[M33Index];
			FP m01 =
				values[M03Index] * values[M22Index] * values[M31Index] - values[M02Index] * values[M23Index] *
																	   values[M31Index]
																	   - values[M03Index] * values[M21Index] *
																	   values[M32Index] + values[M01Index] *
																						values[M23Index] *
																						values[M32Index]
																						+ values[M02Index] *
																						values[M21Index] *
																						values[M33Index] -
				values[M01Index] * values[M22Index] * values[M33Index];
			FP m02 = values[M02Index] * values[M13Index] * values[M31Index] -
							   values[M03Index] * values[M12Index] * values[M31Index]
							   + values[M03Index] * values[M11Index] * values[M32Index] - values[M01Index] *
																						values[M13Index] * values[M32Index]
																						- values[M02Index] *
																						values[M11Index] *
																						values[M33Index] +
							   values[M01Index] * values[M12Index] * values[M33Index];
			FP m03 =
				values[M03Index] * values[M12Index] * values[M21Index] - values[M02Index] * values[M13Index] *
																	   values[M21Index]
																	   - values[M03Index] * values[M11Index] *
																	   values[M22Index] + values[M01Index] *
																						values[M13Index] *
																						values[M22Index]
																						+ values[M02Index] *
																						values[M11Index] *
																						values[M23Index] -
				values[M01Index] * values[M12Index] * values[M23Index];
			FP m10 =
				values[M13Index] * values[M22Index] * values[M30Index] - values[M12Index] * values[M23Index] *
																	   values[M30Index]
																	   - values[M13Index] * values[M20Index] *
																	   values[M32Index] + values[M10Index] *
																						values[M23Index] *
																						values[M32Index]
																						+ values[M12Index] *
																						values[M20Index] *
																						values[M33Index] -
				values[M10Index] * values[M22Index] * values[M33Index];
			FP m11 = values[M02Index] * values[M23Index] * values[M30Index] -
							   values[M03Index] * values[M22Index] * values[M30Index]
							   + values[M03Index] * values[M20Index] * values[M32Index] - values[M00Index] *
																						values[M23Index] * values[M32Index]
																						- values[M02Index] *
																						values[M20Index] *
																						values[M33Index] +
							   values[M00Index] * values[M22Index] * values[M33Index];
			FP m12 =
				values[M03Index] * values[M12Index] * values[M30Index] - values[M02Index] * values[M13Index] *
																	   values[M30Index]
																	   - values[M03Index] * values[M10Index] *
																	   values[M32Index] + values[M00Index] *
																						values[M13Index] *
																						values[M32Index]
																						+ values[M02Index] *
																						values[M10Index] *
																						values[M33Index] -
				values[M00Index] * values[M12Index] * values[M33Index];
			FP m13 = values[M02Index] * values[M13Index] * values[M20Index] -
							   values[M03Index] * values[M12Index] * values[M20Index]
							   + values[M03Index] * values[M10Index] * values[M22Index] - values[M00Index] *
																						values[M13Index] * values[M22Index]
																						- values[M02Index] *
																						values[M10Index] *
																						values[M23Index] +
							   values[M00Index] * values[M12Index] * values[M23Index];
			FP m20 = values[M11Index] * values[M23Index] * values[M30Index] -
							   values[M13Index] * values[M21Index] * values[M30Index]
							   + values[M13Index] * values[M20Index] * values[M31Index] - values[M10Index] *
																						values[M23Index] * values[M31Index]
																						- values[M11Index] *
																						values[M20Index] *
																						values[M33Index] +
							   values[M10Index] * values[M21Index] * values[M33Index];
			FP m21 =
				values[M03Index] * values[M21Index] * values[M30Index] - values[M01Index] * values[M23Index] *
																	   values[M30Index]
																	   - values[M03Index] * values[M20Index] *
																	   values[M31Index] + values[M00Index] *
																						values[M23Index] *
																						values[M31Index]
																						+ values[M01Index] *
																						values[M20Index] *
																						values[M33Index] -
				values[M00Index] * values[M21Index] * values[M33Index];
			FP m22 = values[M01Index] * values[M13Index] * values[M30Index] -
							   values[M03Index] * values[M11Index] * values[M30Index]
							   + values[M03Index] * values[M10Index] * values[M31Index] - values[M00Index] *
																						values[M13Index] * values[M31Index]
																						- values[M01Index] *
																						values[M10Index] *
																						values[M33Index] +
							   values[M00Index] * values[M11Index] * values[M33Index];
			FP m23 =
				values[M03Index] * values[M11Index] * values[M20Index] - values[M01Index] * values[M13Index] *
																	   values[M20Index]
																	   - values[M03Index] * values[M10Index] *
																	   values[M21Index] + values[M00Index] *
																						values[M13Index] *
																						values[M21Index]
																						+ values[M01Index] *
																						values[M10Index] *
																						values[M23Index] -
				values[M00Index] * values[M11Index] * values[M23Index];
			FP m30 =
				values[M12Index] * values[M21Index] * values[M30Index] - values[M11Index] * values[M22Index] *
																	   values[M30Index]
																	   - values[M12Index] * values[M20Index] *
																	   values[M31Index] + values[M10Index] *
																						values[M22Index] *
																						values[M31Index]
																						+ values[M11Index] *
																						values[M20Index] *
																						values[M32Index] -
				values[M10Index] * values[M21Index] * values[M32Index];
			FP m31 = values[M01Index] * values[M22Index] * values[M30Index] -
							   values[M02Index] * values[M21Index] * values[M30Index]
							   + values[M02Index] * values[M20Index] * values[M31Index] - values[M00Index] *
																						values[M22Index] * values[M31Index]
																						- values[M01Index] *
																						values[M20Index] *
																						values[M32Index] +
							   values[M00Index] * values[M21Index] * values[M32Index];
			FP m32 =
				values[M02Index] * values[M11Index] * values[M30Index] - values[M01Index] * values[M12Index] *
																	   values[M30Index]
																	   - values[M02Index] * values[M10Index] *
																	   values[M31Index] + values[M00Index] *
																						values[M12Index] *
																						values[M31Index]
																						+ values[M01Index] *
																						values[M10Index] *
																						values[M32Index] -
				values[M00Index] * values[M11Index] * values[M32Index];
			FP m33 = values[M01Index] * values[M12Index] * values[M20Index] -
							   values[M02Index] * values[M11Index] * values[M20Index]
							   + values[M02Index] * values[M10Index] * values[M21Index] - values[M00Index] *
																						values[M12Index] * values[M21Index]
																						- values[M01Index] *
																						values[M10Index] *
																						values[M22Index] +
							   values[M00Index] * values[M11Index] * values[M22Index];
			FP inv_det = 1.0f / l_det;
			values[M00Index] = m00 * inv_det;
			values[M10Index] = m10 * inv_det;
			values[M20Index] = m20 * inv_det;
			values[M30Index] = m30 * inv_det;
			values[M01Index] = m01 * inv_det;
			values[M11Index] = m11 * inv_det;
			values[M21Index] = m21 * inv_det;
			values[M31Index] = m31 * inv_det;
			values[M02Index] = m02 * inv_det;
			values[M12Index] = m12 * inv_det;
			values[M22Index] = m22 * inv_det;
			values[M32Index] = m32 * inv_det;
			values[M03Index] = m03 * inv_det;
			values[M13Index] = m13 * inv_det;
			values[M23Index] = m23 * inv_det;
			values[M33Index] = m33 * inv_det;
			return true;
		}

		/** Computes the determinante of the given matrix. The matrix array is assumed to hold a 4x4 column major matrix as you can get
		 * from {@link Matrix4#val}.
		 * @param values the matrix values.
		 * @return the determinante. */
		public static FP det(FP[] values)
		{
			return values[M30Index] * values[M21Index] * values[M12Index] * values[M03Index] - values[M20Index] *
																							 values[M31Index] *
																							 values[M12Index] *
																							 values[M03Index]
																							 - values[M30Index] *
																							 values[M11Index] *
																							 values[M22Index] *
																							 values[M03Index] + values[
																												  M10Index] *
																											  values[
																												  M31Index] *
																											  values[
																												  M22Index] *
																											  values[
																												  M03Index]
																											  + values[
																												  M20Index] *
																											  values[
																												  M11Index] *
																											  values[
																												  M32Index] *
																											  values[
																												  M03Index] - values
																															[
																																M10Index] *
																															values
																																[M21Index] *
																															values
																																[M32Index] *
																															values
																																[M03Index]
																															- values
																																[M30Index] *
																															values
																																[M21Index] *
																															values
																																[M02Index] *
																															values
																																[M13Index] + values
																																		   [
																																			   M20Index] *
																																		   values
																																			   [M31Index] *
																																		   values
																																			   [M02Index] *
																																		   values
																																			   [M13Index]
																																		   + values
																																			   [M30Index] *
																																		   values
																																			   [M01Index] *
																																		   values
																																			   [M22Index] *
																																		   values
																																			   [M13Index] - values
																																						  [
																																							  M00Index] *
																																						  values
																																							  [M31Index] *
																																						  values
																																							  [M22Index] *
																																						  values
																																							  [M13Index]
																																						  - values
																																							  [M20Index] *
																																						  values
																																							  [M01Index] *
																																						  values
																																							  [M32Index] *
																																						  values
																																							  [M13Index] + values
																																										 [
																																											 M00Index] *
																																										 values
																																											 [M21Index] *
																																										 values
																																											 [M32Index] *
																																										 values
																																											 [M13Index]
																																										 + values
																																											 [M30Index] *
																																										 values
																																											 [M11Index] *
																																										 values
																																											 [M02Index] *
																																										 values
																																											 [M23Index] - values
																																														[
																																															M10Index] *
																																														values
																																															[M31Index] *
																																														values
																																															[M02Index] *
																																														values
																																															[M23Index]
																																														- values
																																															[M30Index] *
																																														values
																																															[M01Index] *
																																														values
																																															[M12Index] *
																																														values
																																															[M23Index] + values
																																																	   [
																																																		   M00Index] *
																																																	   values
																																																		   [M31Index] *
																																																	   values
																																																		   [M12Index] *
																																																	   values
																																																		   [M23Index]
																																																	   + values
																																																		   [M10Index] *
																																																	   values
																																																		   [M01Index] *
																																																	   values
																																																		   [M32Index] *
																																																	   values
																																																		   [M23Index] - values
																																																					  [
																																																						  M00Index] *
																																																					  values
																																																					  [
																																																						  M11Index] *
																																																					  values
																																																					  [
																																																						  M32Index] *
																																																					  values
																																																					  [
																																																						  M23Index]
																																																					  - values
																																																					  [
																																																						  M20Index] *
																																																					  values
																																																					  [
																																																						  M11Index] *
																																																					  values
																																																					  [
																																																						  M02Index] *
																																																					  values
																																																					  [
																																																						  M33Index] + values
																																																									[
																																																										M10Index] *
																																																									values
																																																									[
																																																										M21Index] *
																																																									values
																																																									[
																																																										M02Index] *
																																																									values
																																																									[
																																																										M33Index]
																																																									+
																																																									values
																																																									[
																																																										M20Index] *
																																																									values
																																																									[
																																																										M01Index] *
																																																									values
																																																									[
																																																										M12Index] *
																																																									values
																																																									[
																																																										M33Index] - values
																																																												  [
																																																													  M00Index] *
																																																												  values
																																																												  [
																																																													  M21Index] *
																																																												  values
																																																												  [
																																																													  M12Index] *
																																																												  values
																																																												  [
																																																													  M33Index]
																																																												  -
																																																												  values
																																																												  [
																																																													  M10Index] *
																																																												  values
																																																												  [
																																																													  M01Index] *
																																																												  values
																																																												  [
																																																													  M22Index] *
																																																												  values
																																																												  [
																																																													  M33Index] +
				   values[M00Index] * values[M11Index] * values[M22Index] * values[M33Index];
		}

		/** Postmultiplies this matrix by a translation matrix. Postmultiplication is also used by OpenGL ES'
		 * glTranslate/glRotate/glScale
		 * @param translation
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 translate(FPVector3 translation)
		{
			return translate(translation.x, translation.y, translation.z);
		}

		/** Postmultiplies this matrix by a translation matrix. Postmultiplication is also used by OpenGL ES' 1.x
		 * glTranslate/glRotate/glScale.
		 * @param x Translation in the x-axis.
		 * @param y Translation in the y-axis.
		 * @param z Translation in the z-axis.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 translate(FP x, FP y, FP z)
		{
			this.m03 += this.m00 * x + this.m01 * y + this.m02 * z;
			this.m13 += this.m10 * x + this.m11 * y + this.m12 * z;
			this.m23 += this.m20 * x + this.m21 * y + this.m22 * z;
			this.m33 += this.m30 * x + this.m31 * y + this.m32 * z;
			return this;
		}

		/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
		 * glTranslate/glRotate/glScale.
		 * @param axis The vector axis to rotate around.
		 * @param degrees The angle in degrees.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 rotate(FPVector3 axis, FP degrees)
		{
			if (degrees == 0) return this;
			quat.set(axis, degrees);
			return rotate(quat);
		}

		/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
		 * glTranslate/glRotate/glScale.
		 * @param axis The vector axis to rotate around.rotateTowardDirection
		 * @param radians The angle in radians.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 rotateRad(FPVector3 axis, FP radians)
		{
			if (radians == 0) return this;
			quat.setFromAxisRad(axis, radians);
			return rotate(quat);
		}

		/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
		 * glTranslate/glRotate/glScale
		 * @param axisX The x-axis component of the vector to rotate around.
		 * @param axisY The y-axis component of the vector to rotate around.
		 * @param axisZ The z-axis component of the vector to rotate around.
		 * @param degrees The angle in degrees
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 rotate(FP axisX, FP axisY, FP axisZ, FP degrees)
		{
			if (degrees == 0) return this;
			quat.setFromAxis(axisX, axisY, axisZ, degrees);
			return rotate(quat);
		}

		/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
		 * glTranslate/glRotate/glScale
		 * @param axisX The x-axis component of the vector to rotate around.
		 * @param axisY The y-axis component of the vector to rotate around.
		 * @param axisZ The z-axis component of the vector to rotate around.
		 * @param radians The angle in radians
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 rotateRad(FP axisX, FP axisY, FP axisZ, FP radians)
		{
			if (radians == 0) return this;
			quat.setFromAxisRad(axisX, axisY, axisZ, radians);
			return rotate(quat);
		}

		/** Postmultiplies this matrix with a (counter-clockwise) rotation matrix. Postmultiplication is also used by OpenGL ES' 1.x
		 * glTranslate/glRotate/glScale.
		 * @param rotation
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 rotate(FPQuaternion rotation)
		{
			FP x = rotation.x;
			FP y = rotation.y;
			FP z = rotation.z;
			FP w = rotation.w;
			FP xx = x * x;
			FP xy = x * y;
			FP xz = x * z;
			FP xw = x * w;
			FP yy = y * y;
			FP yz = y * z;
			FP yw = y * w;
			FP zz = z * z;
			FP zw = z * w;
			// Set matrix from quaternion
			FP r00 = 1 - 2 * (yy + zz);
			FP r01 = 2 * (xy - zw);
			FP r02 = 2 * (xz + yw);
			FP r10 = 2 * (xy + zw);
			FP r11 = 1 - 2 * (xx + zz);
			FP r12 = 2 * (yz - xw);
			FP r20 = 2 * (xz - yw);
			FP r21 = 2 * (yz + xw);
			FP r22 = 1 - 2 * (xx + yy);
			FP m00 = this.m00 * r00 + this.m01 * r10 + this.m02 * r20;
			FP m01 = this.m00 * r01 + this.m01 * r11 + this.m02 * r21;
			FP m02 = this.m00 * r02 + this.m01 * r12 + this.m02 * r22;
			FP m10 = this.m10 * r00 + this.m11 * r10 + this.m12 * r20;
			FP m11 = this.m10 * r01 + this.m11 * r11 + this.m12 * r21;
			FP m12 = this.m10 * r02 + this.m11 * r12 + this.m12 * r22;
			FP m20 = this.m20 * r00 + this.m21 * r10 + this.m22 * r20;
			FP m21 = this.m20 * r01 + this.m21 * r11 + this.m22 * r21;
			FP m22 = this.m20 * r02 + this.m21 * r12 + this.m22 * r22;
			FP m30 = this.m30 * r00 + this.m31 * r10 + this.m32 * r20;
			FP m31 = this.m30 * r01 + this.m31 * r11 + this.m32 * r21;
			FP m32 = this.m30 * r02 + this.m31 * r12 + this.m32 * r22;
			this.m00 = m00;
			this.m10 = m10;
			this.m20 = m20;
			this.m30 = m30;
			this.m01 = m01;
			this.m11 = m11;
			this.m21 = m21;
			this.m31 = m31;
			this.m02 = m02;
			this.m12 = m12;
			this.m22 = m22;
			this.m32 = m32;
			return this;
		}

		/** Postmultiplies this matrix by the rotation between two vectors.
		 * @param v1 The base vector
		 * @param v2 The target vector
		 * @return This matrix for the purpose of chaining methods together */
		public FPMatrix4x4 rotate(FPVector3 v1, FPVector3 v2)
		{
			return rotate(quat.setFromCross(v1, v2));
		}

		/** Post-multiplies this matrix by a rotation toward a direction.
		 * @param direction direction to rotate toward
		 * @param up up vector
		 * @return This matrix for chaining */
		public FPMatrix4x4 rotateTowardDirection(FPVector3 direction, FPVector3 up)
		{
			l_vez = l_vez.set(direction).nor();
			l_vex = -l_vex.set(l_vez).crs(up).nor();
			l_vey = -l_vey.set(l_vex).crs(l_vez).nor();

			FP m00 = this.m00 * l_vex.x + this.m01 * l_vex.y + this.m02 * l_vex.z;
			FP m01 = this.m00 * l_vey.x + this.m01 * l_vey.y + this.m02 * l_vey.z;
			FP m02 = this.m00 * l_vez.x + this.m01 * l_vez.y + this.m02 * l_vez.z;
			FP m10 = this.m10 * l_vex.x + this.m11 * l_vex.y + this.m12 * l_vex.z;
			FP m11 = this.m10 * l_vey.x + this.m11 * l_vey.y + this.m12 * l_vey.z;
			FP m12 = this.m10 * l_vez.x + this.m11 * l_vez.y + this.m12 * l_vez.z;
			FP m20 = this.m20 * l_vex.x + this.m21 * l_vex.y + this.m22 * l_vex.z;
			FP m21 = this.m20 * l_vey.x + this.m21 * l_vey.y + this.m22 * l_vey.z;
			FP m22 = this.m20 * l_vez.x + this.m21 * l_vez.y + this.m22 * l_vez.z;
			FP m30 = this.m30 * l_vex.x + this.m31 * l_vex.y + this.m32 * l_vex.z;
			FP m31 = this.m30 * l_vey.x + this.m31 * l_vey.y + this.m32 * l_vey.z;
			FP m32 = this.m30 * l_vez.x + this.m31 * l_vez.y + this.m32 * l_vez.z;
			this.m00 = m00;
			this.m10 = m10;
			this.m20 = m20;
			this.m30 = m30;
			this.m01 = m01;
			this.m11 = m11;
			this.m21 = m21;
			this.m31 = m31;
			this.m02 = m02;
			this.m12 = m12;
			this.m22 = m22;
			this.m32 = m32;
			return this;
		}

		/** Post-multiplies this matrix by a rotation toward a target.
		 * @param target the target to rotate to
		 * @param up the up vector
		 * @return This matrix for chaining */
		public FPMatrix4x4 rotateTowardTarget(FPVector3 target, FPVector3 up)
		{
			tmpVec.set(target.x - this.m03, target.y - this.m13, target.z - this.m23);
			return rotateTowardDirection(tmpVec, up);
		}

		/** Postmultiplies this matrix with a scale matrix. Postmultiplication is also used by OpenGL ES' 1.x
		 * glTranslate/glRotate/glScale.
		 * @param scaleX The scale in the x-axis.
		 * @param scaleY The scale in the y-axis.
		 * @param scaleZ The scale in the z-axis.
		 * @return This matrix for the purpose of chaining methods together. */
		public FPMatrix4x4 scale(FP scaleX, FP scaleY, FP scaleZ)
		{
			this.m00 *= scaleX;
			this.m01 *= scaleY;
			this.m02 *= scaleZ;
			this.m10 *= scaleX;
			this.m11 *= scaleY;
			this.m12 *= scaleZ;
			this.m20 *= scaleX;
			this.m21 *= scaleY;
			this.m22 *= scaleZ;
			this.m30 *= scaleX;
			this.m31 *= scaleY;
			this.m32 *= scaleZ;
			return this;
		}

		/** Copies the 4x3 upper-left sub-matrix into float array. The destination array is supposed to be a column major matrix.
		 * @param dst the destination matrix */
		public void extract4x3Matrix(FP[] dst)
		{
			dst[0] = this.m00;
			dst[1] = this.m10;
			dst[2] = this.m20;
			dst[3] = this.m01;
			dst[4] = this.m11;
			dst[5] = this.m21;
			dst[6] = this.m02;
			dst[7] = this.m12;
			dst[8] = this.m22;
			dst[9] = this.m03;
			dst[10] = this.m13;
			dst[11] = this.m23;
		}

		/** @return True if this matrix has any rotation or scaling, false otherwise */
		public bool hasRotationOrScaling()
		{
			return !(FPMath.IsEqual(this.m00, 1) && FPMath.IsEqual(this.m11, 1) &&
					 FPMath.IsEqual(this.m22, 1)
					 && FPMath.IsZero(this.m01) && FPMath.IsZero(this.m02) && FPMath.IsZero(this.m10) &&
					 FPMath.IsZero(this.m12)
					 && FPMath.IsZero(this.m20) && FPMath.IsZero(this.m21));
		}
	}
}


