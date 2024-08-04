
namespace DG
{
	/// <summary>
	/// Encapsulates a 3D vector. Allows chaining operations by returning a reference to itself in all modification methods.
	/// </summary>
	public partial struct FPVector3
	{
		/** the x-component of this vector **/
		public FP x;

		/** the y-component of this vector **/
		public FP y;

		/** the z-component of this vector **/
		public FP z;

		public static FPVector3 X = new(1, 0, 0);
		public static FPVector3 Y = new(0, 1, 0);
		public static FPVector3 Z = new(0, 0, 1);
		public static FPVector3 Zero = new(0, 0, 0);

		private static FPMatrix4x4 tmpMat = FPMatrix4x4.default2;


		/** Creates a vector with the given components
		 * @param x The x-component
		 * @param y The y-component
		 * @param z The z-component */
		public FPVector3(FP x, FP y, FP z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		/** Creates a vector from the given vector
		 * @param vector The vector */
		public FPVector3(FPVector3 vector)
		{
			x = vector.x;
			y = vector.y;
			z = vector.z;
		}

		/** Creates a vector from the given array. The array must have at least 3 elements.
		 *
		 * @param values The array */
		public FPVector3(FP[] values)
		{
			x = values[0];
			y = values[1];
			z = values[2];
		}

		/** Creates a vector from the given vector and z-component
		 *
		 * @param vector The vector
		 * @param z The z-component */
		public FPVector3(FPVector2 vector, FP z)
		{
			x = vector.x;
			y = vector.y;
			this.z = z;
		}

		/** Sets the vector to the given components
		 *
		 * @param x The x-component
		 * @param y The y-component
		 * @param z The z-component
		 * @return this vector for chaining */
		public FPVector3 set(FP x, FP y, FP z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			return this;
		}

		public FPVector3 set(FPVector3 vector)
		{
			return set(vector.x, vector.y, vector.z);
		}

		/** Sets the components from the array. The array must have at least 3 elements
		 *
		 * @param values The array
		 * @return this vector for chaining */
		public FPVector3 set(FP[] values)
		{
			return set(values[0], values[1], values[2]);
		}

		/** Sets the components of the given vector and z-component
		 *
		 * @param vector The vector
		 * @param z The z-component
		 * @return This vector for chaining */
		public FPVector3 set(FPVector2 vector, FP z)
		{
			return set(vector.x, vector.y, z);
		}

		/** Sets the components from the given spherical coordinate
		 * @param azimuthalAngle The angle between x-axis in radians [0, 2pi]
		 * @param polarAngle The angle between z-axis in radians [0, pi]
		 * @return This vector for chaining */
		public FPVector3 setFromSpherical(FP azimuthalAngle, FP polarAngle)
		{
			FP cosPolar = FPMath.Cos(polarAngle);
			FP sinPolar = FPMath.Sin(polarAngle);

			FP cosAzim = FPMath.Cos(azimuthalAngle);
			FP sinAzim = FPMath.Sin(azimuthalAngle);

			return set(cosAzim * sinPolar, sinAzim * sinPolar, cosPolar);
		}


		public FPVector3 cpy()
		{
			return new FPVector3(this);
		}

		public FPVector3 add(FPVector3 vector)
		{
			return add(vector.x, vector.y, vector.z);
		}

		/** Adds the given vector to this component
		 * @param x The x-component of the other vector
		 * @param y The y-component of the other vector
		 * @param z The z-component of the other vector
		 * @return This vector for chaining. */
		public FPVector3 add(FP x, FP y, FP z)
		{
			return set(this.x + x, this.y + y, this.z + z);
		}

		/** Adds the given value to all three components of the vector.
		 *
		 * @param values The value
		 * @return This vector for chaining */
		public FPVector3 add(FP values)
		{
			return set(x + values, y + values, z + values);
		}

		public FPVector3 sub(FPVector3 a_vec)
		{
			return sub(a_vec.x, a_vec.y, a_vec.z);
		}

		/** Subtracts the other vector from this vector.
		 *
		 * @param x The x-component of the other vector
		 * @param y The y-component of the other vector
		 * @param z The z-component of the other vector
		 * @return This vector for chaining */
		public FPVector3 sub(FP x, FP y, FP z)
		{
			return set(this.x - x, this.y - y, this.z - z);
		}

		/** Subtracts the given value from all components of this vector
		 *
		 * @param value The value
		 * @return This vector for chaining */
		public FPVector3 sub(FP value)
		{
			return set(x - value, y - value, z - value);
		}

		public FPVector3 scl(FP scalar)
		{
			return set(x * scalar, y * scalar, z * scalar);
		}

		public FPVector3 scl(FPVector3 other)
		{
			return set(x * other.x, y * other.y, z * other.z);
		}

		/** Scales this vector by the given values
		 * @param vx X value
		 * @param vy Y value
		 * @param vz Z value
		 * @return This vector for chaining */
		public FPVector3 scl(FP vx, FP vy, FP vz)
		{
			return set(x * vx, y * vy, z * vz);
		}

		public FPVector3 mulAdd(FPVector3 vec, FP scalar)
		{
			x += vec.x * scalar;
			y += vec.y * scalar;
			z += vec.z * scalar;
			return this;
		}

		public FPVector3 mulAdd(FPVector3 vec, FPVector3 mulVec)
		{
			x += vec.x * mulVec.x;
			y += vec.y * mulVec.y;
			z += vec.z * mulVec.z;
			return this;
		}

		/** @return The euclidean length */
		public static FP len(FP x, FP y, FP z)
		{
			return FPMath.Sqrt(x * x + y * y + z * z);
		}

		public FP len()
		{
			return FPMath.Sqrt(x * x + y * y + z * z);
		}

		/** @return The squared euclidean length */
		public static FP len2(FP x, FP y, FP z)
		{
			return x * x + y * y + z * z;
		}

		public FP len2()
		{
			return x * x + y * y + z * z;
		}

		/** @param vector The other vector
		 * @return Whether this and the other vector are equal */
		public bool idt(FPVector3 vector)
		{
			return x == vector.x && y == vector.y && z == vector.z;
		}

		/** @return The euclidean distance between the two specified vectors */
		public static FP dst(FP x1, FP y1, FP z1, FP x2, FP y2,
			FP z2)
		{
			FP a = x2 - x1;
			FP b = y2 - y1;
			FP c = z2 - z1;
			return FPMath.Sqrt(a * a + b * b + c * c);
		}

		public FP dst(FPVector3 vector)
		{
			FP a = vector.x - x;
			FP b = vector.y - y;
			FP c = vector.z - z;
			return FPMath.Sqrt(a * a + b * b + c * c);
		}

		/** @return the distance between this point and the given point */
		public FP dst(FP x, FP y, FP z)
		{
			FP a = x - this.x;
			FP b = y - this.y;
			FP c = z - this.z;
			return FPMath.Sqrt(a * a + b * b + c * c);
		}

		/** @return the squared distance between the given points */
		public static FP dst2(FP x1, FP y1, FP z1, FP x2, FP y2,
			FP z2)
		{
			FP a = x2 - x1;
			FP b = y2 - y1;
			FP c = z2 - z1;
			return a * a + b * b + c * c;
		}

		public FP dst2(FPVector3 point)
		{
			FP a = point.x - x;
			FP b = point.y - y;
			FP c = point.z - z;
			return a * a + b * b + c * c;
		}

		/** Returns the squared distance between this point and the given point
		 * @param x The x-component of the other point
		 * @param y The y-component of the other point
		 * @param z The z-component of the other point
		 * @return The squared distance */
		public FP dst2(FP x, FP y, FP z)
		{
			FP a = x - this.x;
			FP b = y - this.y;
			FP c = z - this.z;
			return a * a + b * b + c * c;
		}

		public FPVector3 nor()
		{
			FP len2 = this.len2();
			if (len2 == 0f || len2 == 1f) return this;
			return scl(1f / FPMath.Sqrt(len2));
		}

		/** @return The dot product between the two vectors */
		public static FP dot(FP x1, FP y1, FP z1, FP x2, FP y2,
			FP z2)
		{
			return x1 * x2 + y1 * y2 + z1 * z2;
		}

		public FP dot(FPVector3 vector)
		{
			return x * vector.x + y * vector.y + z * vector.z;
		}

		/** Returns the dot product between this and the given vector.
		 * @param x The x-component of the other vector
		 * @param y The y-component of the other vector
		 * @param z The z-component of the other vector
		 * @return The dot product */
		public FP dot(FP x, FP y, FP z)
		{
			return this.x * x + this.y * y + this.z * z;
		}

		/** Sets this vector to the cross product between it and the other vector.
		 * @param vector The other vector
		 * @return This vector for chaining */
		public FPVector3 crs(FPVector3 vector)
		{
			return set(y * vector.z - z * vector.y, z * vector.x - x * vector.z, x * vector.y - y * vector.x);
		}

		/** Sets this vector to the cross product between it and the other vector.
		 * @param x The x-component of the other vector
		 * @param y The y-component of the other vector
		 * @param z The z-component of the other vector
		 * @return This vector for chaining */
		public FPVector3 crs(FP x, FP y, FP z)
		{
			return set(this.y * z - this.z * y, this.z * x - this.x * z, this.x * y - this.y * x);
		}

		/** Left-multiplies the vector by the given 4x3 column major matrix. The matrix should be composed by a 3x3 matrix representing
		 * rotation and scale plus a 1x3 matrix representing the translation.
		 * @param matrix The matrix
		 * @return This vector for chaining */
		public FPVector3 mul4x3(FP[] matrix)
		{
			return set(x * matrix[0] + y * matrix[3] + z * matrix[6] + matrix[9],
				x * matrix[1] + y * matrix[4] + z * matrix[7] + matrix[10],
				x * matrix[2] + y * matrix[5] + z * matrix[8] + matrix[11]);
		}

		/** Left-multiplies the vector by the given matrix, assuming the fourth (w) component of the vector is 1.
		 * @param matrix The matrix
		 * @return This vector for chaining */
		public FPVector3 mul(FPMatrix4x4 matrix)
		{
			return set(
				x * matrix.m00 + y * matrix.m01 + z * matrix.m02 +
				matrix.m03,
				x * matrix.m10 + y * matrix.m11 + z * matrix.m12 +
				matrix.m13,
				x * matrix.m20 + y * matrix.m21 + z * matrix.m22 +
				matrix.m23);
		}

		/** Multiplies the vector by the transpose of the given matrix, assuming the fourth (w) component of the vector is 1.
		 * @param matrix The matrix
		 * @return This vector for chaining */
		public FPVector3 traMul(FPMatrix4x4 matrix)
		{
			return set(
				x * matrix.m00 + y * matrix.m10 + z * matrix.m20 +
				matrix.m30,
				x * matrix.m01 + y * matrix.m11 + z * matrix.m21 +
				matrix.m31,
				x * matrix.m02 + y * matrix.m12 + z * matrix.m22 +
				matrix.m32);
		}


		/** Left-multiplies the vector by the given matrix.
		 * @param matrix The matrix
		 * @return This vector for chaining */
		public FPVector3 mul(FPMatrix3x3 matrix)
		{
			return set(x * matrix.m00 + y * matrix.m01 + z * matrix.m02,
				x * matrix.m10 + y * matrix.m11 + z * matrix.m12,
				x * matrix.m20 + y * matrix.m21 + z * matrix.m22);
		}

		/** Multiplies the vector by the transpose of the given matrix.
		 * @param matrix The matrix
		 * @return This vector for chaining */
		public FPVector3 traMul(FPMatrix3x3 matrix)
		{
			return set(x * matrix.m00 + y * matrix.m10 + z * matrix.m20,
				x * matrix.m01 + y * matrix.m11 + z * matrix.m21,
				x * matrix.m02 + y * matrix.m12 + z * matrix.m22);
		}

		/** Multiplies the vector by the given {@link Quaternion}.
		 * @return This vector for chaining */
		public FPVector3 mul(FPQuaternion quat)
		{
			return quat.transform(this);
		}

		/** Multiplies this vector by the given matrix dividing by w, assuming the fourth (w) component of the vector is 1. This is
		 * mostly used to project/unproject vectors via a perspective projection matrix.
		 *
		 * @param matrix The matrix.
		 * @return This vector for chaining */
		public FPVector3 prj(FPMatrix4x4 matrix)
		{
			FP l_w = 1f / (x * matrix.m30 + y * matrix.m31 +
													z * matrix.m32 +
													matrix.m33);
			return set(
				(x * matrix.m00 + y * matrix.m01 + z * matrix.m02 +
				 matrix.m03) * l_w,
				(x * matrix.m10 + y * matrix.m11 + z * matrix.m12 +
				 matrix.m13) * l_w,
				(x * matrix.m20 + y * matrix.m21 + z * matrix.m22 +
				 matrix.m23) * l_w);
		}

		/** Multiplies this vector by the first three columns of the matrix, essentially only applying rotation and scaling.
		 *
		 * @param matrix The matrix
		 * @return This vector for chaining */
		public FPVector3 rot(FPMatrix4x4 matrix)
		{
			return set(x * matrix.m00 + y * matrix.m01 + z * matrix.m02,
				x * matrix.m10 + y * matrix.m11 + z * matrix.m12,
				x * matrix.m20 + y * matrix.m21 + z * matrix.m22);
		}

		/** Multiplies this vector by the transpose of the first three columns of the matrix. Note: only works for translation and
		 * rotation, does not work for scaling. For those, use {@link #rot(Matrix4)} with {@link Matrix4#inv()}.
		 * @param matrix The transformation matrix
		 * @return The vector for chaining */
		public FPVector3 unrotate(FPMatrix4x4 matrix)
		{
			return set(x * matrix.m00 + y * matrix.m10 + z * matrix.m20,
				x * matrix.m01 + y * matrix.m11 + z * matrix.m21,
				x * matrix.m02 + y * matrix.m12 + z * matrix.m22);
		}

		/** Translates this vector in the direction opposite to the translation of the matrix and the multiplies this vector by the
		 * transpose of the first three columns of the matrix. Note: only works for translation and rotation, does not work for
		 * scaling. For those, use {@link #mul(Matrix4)} with {@link Matrix4#inv()}.
		 * @param matrix The transformation matrix
		 * @return The vector for chaining */
		public FPVector3 untransform(FPMatrix4x4 matrix)
		{
			x -= matrix.m03;
			y -= matrix.m13;
			z -= matrix.m23;
			return set(x * matrix.m00 + y * matrix.m10 + z * matrix.m20,
				x * matrix.m01 + y * matrix.m11 + z * matrix.m21,
				x * matrix.m02 + y * matrix.m12 + z * matrix.m22);
		}

		/** Rotates this vector by the given angle in degrees around the given axis.
		 *
		 * @param degrees the angle in degrees
		 * @param axisX the x-component of the axis
		 * @param axisY the y-component of the axis
		 * @param axisZ the z-component of the axis
		 * @return This vector for chaining */
		public FPVector3 rotate(FP degrees, FP axisX, FP axisY, FP axisZ)
		{
			return mul(tmpMat.setToRotation(axisX, axisY, axisZ, degrees));
		}

		/** Rotates this vector by the given angle in radians around the given axis.
		 *
		 * @param radians the angle in radians
		 * @param axisX the x-component of the axis
		 * @param axisY the y-component of the axis
		 * @param axisZ the z-component of the axis
		 * @return This vector for chaining */
		public FPVector3 rotateRad(FP radians, FP axisX, FP axisY, FP axisZ)
		{
			return mul(tmpMat.setToRotationRad(axisX, axisY, axisZ, radians));
		}

		/** Rotates this vector by the given angle in degrees around the given axis.
		 *
		 * @param axis the axis
		 * @param degrees the angle in degrees
		 * @return This vector for chaining */
		public FPVector3 rotate(FPVector3 axis, FP degrees)
		{
			tmpMat = tmpMat.setToRotation(axis, degrees);
			return mul(tmpMat);
		}

		/** Rotates this vector by the given angle in radians around the given axis.
		 *
		 * @param axis the axis
		 * @param radians the angle in radians
		 * @return This vector for chaining */
		public FPVector3 rotateRad(FPVector3 axis, FP radians)
		{
			tmpMat = tmpMat.setToRotationRad(axis, radians);
			return mul(tmpMat);
		}

		public bool isUnit()
		{
			return isUnit(0.000000001f);
		}

		public bool isUnit(FP margin)
		{
			return FPMath.Abs(len2() - 1f) < margin;
		}

		public bool isZero()
		{
			return x == 0 && y == 0 && z == 0;
		}

		public bool isZero(FP margin)
		{
			return len2() < margin;
		}

		public bool isOnLine(FPVector3 other, FP epsilon)
		{
			return len2(y * other.z - z * other.y, z * other.x - x * other.z, x * other.y - y * other.x) <= epsilon;
		}

		public bool isOnLine(FPVector3 other)
		{
			return len2(y * other.z - z * other.y, z * other.x - x * other.z,
					   x * other.y - y * other.x) <= FPMath.EPSILION;
		}

		public bool isCollinear(FPVector3 other, FP epsilon)
		{
			return isOnLine(other, epsilon) && hasSameDirection(other);
		}

		public bool isCollinear(FPVector3 other)
		{
			return isOnLine(other) && hasSameDirection(other);
		}

		public bool isCollinearOpposite(FPVector3 other, FP epsilon)
		{
			return isOnLine(other, epsilon) && hasOppositeDirection(other);
		}

		public bool isCollinearOpposite(FPVector3 other)
		{
			return isOnLine(other) && hasOppositeDirection(other);
		}

		public bool isPerpendicular(FPVector3 vector)
		{
			return FPMath.IsZero(dot(vector));
		}

		public bool isPerpendicular(FPVector3 vector, FP epsilon)
		{
			return FPMath.IsZero(dot(vector), epsilon);
		}

		public bool hasSameDirection(FPVector3 vector)
		{
			return dot(vector) > 0;
		}

		public bool hasOppositeDirection(FPVector3 vector)
		{
			return dot(vector) < 0;
		}

		public FPVector3 lerp(FPVector3 target, FP alpha)
		{
			x += alpha * (target.x - x);
			y += alpha * (target.y - y);
			z += alpha * (target.z - z);
			return this;
		}

		public FPVector3 interpolate(FPVector3 target, FP alpha, FPInterpolation interpolator)
		{
			return lerp(target, interpolator.Apply(0f, 1f, alpha));
		}

		/** Spherically interpolates between this vector and the target vector by alpha which is in the range [0,1]. The result is
		 * stored in this vector.
		 *
		 * @param target The target vector
		 * @param alpha The interpolation coefficient
		 * @return This vector for chaining. */
		public FPVector3 slerp(FPVector3 target, FP alpha)
		{
			//		DGFixedPoint dot = this.dot(target);
			//		// If the inputs are too close for comfort, simply linearly interpolate.
			//		if (dot > (DGFixedPoint) 0.9995 || dot < (DGFixedPoint) (-0.9995)) return lerp(target, alpha);
			//
			//		// theta0 = angle between input vectors
			//		DGFixedPoint theta0 = DGFixedPointMath.Acos(dot);
			//		// theta = angle between this vector and result
			//		DGFixedPoint theta = theta0 * alpha;
			//
			//		DGFixedPoint st = DGFixedPointMath.Sin(theta);
			//		DGFixedPoint tx = target.x - x * dot;
			//		DGFixedPoint ty = target.y - y * dot;
			//		DGFixedPoint tz = target.z - z * dot;
			//		DGFixedPoint l2 = tx * tx + ty * ty + tz * tz;
			//		DGFixedPoint dl =
			//			st * ((l2 < (DGFixedPoint) 0.0001f) ? (DGFixedPoint) 1f : (DGFixedPoint) 1f / DGFixedPointMath.Sqrt(l2));
			//
			//		return scl(DGFixedPointMath.Cos(theta)).add(tx * dl, ty * dl, tz * dl).nor();

			//https://stackoverflow.com/questions/67919193/how-does-unity-implements-vector3-slerp-exactly
			// Dot product - the cosine of the angle between 2 vectors.
			FP dot = Dot(cpy().normalized, target.cpy().normalized);

			// Clamp it to be in the range of Acos()
			// This may be unnecessary, but floating point
			// precision can be a fickle mistress.
			dot = FPMath.Clamp(dot, (-1.0f), 1.0f);

			// Acos(dot) returns the angle between start and end,
			// And multiplying that by percent returns the angle between
			// start and the final result.
			FP theta = FPMath.Acos(dot) * alpha;
			FPVector3 relativeVec = target - this * dot;
			relativeVec.Normalize();

			// Orthonormal basis
			// The final result.
			return (this * FPMath.Cos(theta)) + (relativeVec * FPMath.Sin(theta));
		}

		/** Converts this {@code Vector3} to a string in the format {@code (x,y,z)}.
		 * @return a string representation of this object. */
		public override string ToString()
		{
			return "(" + x + "," + y + "," + z + ")";
		}


		public FPVector3 limit(FP limit)
		{
			return limit2(limit * limit);
		}

		public FPVector3 limit2(FP limit2)
		{
			FP len2 = this.len2();
			if (len2 > limit2)
			{
				scl(FPMath.Sqrt(limit2 / len2));
			}

			return this;
		}

		public FPVector3 setLength(FP len)
		{
			return setLength2(len * len);
		}

		public FPVector3 setLength2(FP len2)
		{
			FP oldLen2 = this.len2();
			return (oldLen2 == 0 || oldLen2 == len2) ? this : scl(FPMath.Sqrt(len2 / oldLen2));
		}

		public FPVector3 clamp(FP min, FP max)
		{
			FP len2 = this.len2();
			if (len2 == 0f) return this;
			FP max2 = max * max;
			if (len2 > max2) return scl(FPMath.Sqrt(max2 / len2));
			FP min2 = min * min;
			if (len2 < min2) return scl(FPMath.Sqrt(min2 / len2));
			return this;
		}


		public bool epsilonEquals(FPVector3 other, FP epsilon)
		{
			if (FPMath.Abs(other.x - x) > epsilon) return false;
			if (FPMath.Abs(other.y - y) > epsilon) return false;
			if (FPMath.Abs(other.z - z) > epsilon) return false;
			return true;
		}

		/** Compares this vector with the other vector, using the supplied epsilon for fuzzy equality testing.
		 * @return whether the vectors are the same. */
		public bool epsilonEquals(FP x, FP y, FP z, FP epsilon)
		{
			if (FPMath.Abs(x - this.x) > epsilon) return false;
			if (FPMath.Abs(y - this.y) > epsilon) return false;
			if (FPMath.Abs(z - this.z) > epsilon) return false;
			return true;
		}

		/** Compares this vector with the other vector using MathUtils.FLOAT_ROUNDING_ERROR for fuzzy equality testing
		 *
		 * @param other other vector to compare
		 * @return true if vector are equal, otherwise false */
		public bool epsilonEquals(FPVector3 other)
		{
			return epsilonEquals(other, FPMath.EPSILION);
		}

		/** Compares this vector with the other vector using MathUtils.FLOAT_ROUNDING_ERROR for fuzzy equality testing
		 *
		 * @param x x component of the other vector to compare
		 * @param y y component of the other vector to compare
		 * @param z z component of the other vector to compare
		 * @return true if vector are equal, otherwise false */
		public bool epsilonEquals(FP x, FP y, FP z)
		{
			return epsilonEquals(x, y, z, FPMath.EPSILION);
		}

		public FPVector3 setZero()
		{
			x = 0;
			y = 0;
			z = 0;
			return this;
		}
	}
}
