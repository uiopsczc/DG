
/// <summary>
/// Encapsulates a 3D vector. Allows chaining operations by returning a reference to itself in all modification methods.
/// </summary>
public partial struct DGVector3
{
	/** the x-component of this vector **/
	public DGFixedPoint x;

	/** the y-component of this vector **/
	public DGFixedPoint y;

	/** the z-component of this vector **/
	public DGFixedPoint z;

	public static DGVector3 X = new DGVector3((DGFixedPoint) 1, (DGFixedPoint) 0, (DGFixedPoint) 0);
	public static DGVector3 Y = new DGVector3((DGFixedPoint) 0, (DGFixedPoint) 1, (DGFixedPoint) 0);
	public static DGVector3 Z = new DGVector3((DGFixedPoint) 0, (DGFixedPoint) 0, (DGFixedPoint) 1);
	public static DGVector3 Zero = new DGVector3((DGFixedPoint) 0, (DGFixedPoint) 0, (DGFixedPoint) 0);

	private static DGMatrix4x4 tmpMat = DGMatrix4x4.default2;


	/** Creates a vector with the given components
	 * @param x The x-component
	 * @param y The y-component
	 * @param z The z-component */
	public DGVector3(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	/** Creates a vector from the given vector
	 * @param vector The vector */
	public DGVector3(DGVector3 vector)
	{
		this.x = vector.x;
		this.y = vector.y;
		this.z = vector.z;
	}

	/** Creates a vector from the given array. The array must have at least 3 elements.
	 *
	 * @param values The array */
	public DGVector3(DGFixedPoint[] values)
	{
		this.x = values[0];
		this.y = values[1];
		this.z = values[2];
	}

	/** Creates a vector from the given vector and z-component
	 *
	 * @param vector The vector
	 * @param z The z-component */
	public DGVector3(DGVector2 vector, DGFixedPoint z)
	{
		this.x = vector.x;
		this.y = vector.y;
		this.z = z;
	}

	/** Sets the vector to the given components
	 *
	 * @param x The x-component
	 * @param y The y-component
	 * @param z The z-component
	 * @return this vector for chaining */
	public DGVector3 set(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		return this;
	}

	public DGVector3 set(DGVector3 vector)
	{
		return this.set(vector.x, vector.y, vector.z);
	}

	/** Sets the components from the array. The array must have at least 3 elements
	 *
	 * @param values The array
	 * @return this vector for chaining */
	public DGVector3 set(DGFixedPoint[] values)
	{
		return this.set(values[0], values[1], values[2]);
	}

	/** Sets the components of the given vector and z-component
	 *
	 * @param vector The vector
	 * @param z The z-component
	 * @return This vector for chaining */
	public DGVector3 set(DGVector2 vector, DGFixedPoint z)
	{
		return this.set(vector.x, vector.y, z);
	}

	/** Sets the components from the given spherical coordinate
	 * @param azimuthalAngle The angle between x-axis in radians [0, 2pi]
	 * @param polarAngle The angle between z-axis in radians [0, pi]
	 * @return This vector for chaining */
	public DGVector3 setFromSpherical(DGFixedPoint azimuthalAngle, DGFixedPoint polarAngle)
	{
		DGFixedPoint cosPolar = DGMath.Cos(polarAngle);
		DGFixedPoint sinPolar = DGMath.Sin(polarAngle);

		DGFixedPoint cosAzim = DGMath.Cos(azimuthalAngle);
		DGFixedPoint sinAzim = DGMath.Sin(azimuthalAngle);

		return this.set(cosAzim * sinPolar, sinAzim * sinPolar, cosPolar);
	}


	public DGVector3 cpy()
	{
		return new DGVector3(this);
	}

	public DGVector3 add(DGVector3 vector)
	{
		return this.add(vector.x, vector.y, vector.z);
	}

	/** Adds the given vector to this component
	 * @param x The x-component of the other vector
	 * @param y The y-component of the other vector
	 * @param z The z-component of the other vector
	 * @return This vector for chaining. */
	public DGVector3 add(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		return this.set(this.x + x, this.y + y, this.z + z);
	}

	/** Adds the given value to all three components of the vector.
	 *
	 * @param values The value
	 * @return This vector for chaining */
	public DGVector3 add(DGFixedPoint values)
	{
		return this.set(this.x + values, this.y + values, this.z + values);
	}

	public DGVector3 sub(DGVector3 a_vec)
	{
		return this.sub(a_vec.x, a_vec.y, a_vec.z);
	}

	/** Subtracts the other vector from this vector.
	 *
	 * @param x The x-component of the other vector
	 * @param y The y-component of the other vector
	 * @param z The z-component of the other vector
	 * @return This vector for chaining */
	public DGVector3 sub(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		return this.set(this.x - x, this.y - y, this.z - z);
	}

	/** Subtracts the given value from all components of this vector
	 *
	 * @param value The value
	 * @return This vector for chaining */
	public DGVector3 sub(DGFixedPoint value)
	{
		return this.set(this.x - value, this.y - value, this.z - value);
	}

	public DGVector3 scl(DGFixedPoint scalar)
	{
		return this.set(this.x * scalar, this.y * scalar, this.z * scalar);
	}

	public DGVector3 scl(DGVector3 other)
	{
		return this.set(x * other.x, y * other.y, z * other.z);
	}

	/** Scales this vector by the given values
	 * @param vx X value
	 * @param vy Y value
	 * @param vz Z value
	 * @return This vector for chaining */
	public DGVector3 scl(DGFixedPoint vx, DGFixedPoint vy, DGFixedPoint vz)
	{
		return this.set(this.x * vx, this.y * vy, this.z * vz);
	}

	public DGVector3 mulAdd(DGVector3 vec, DGFixedPoint scalar)
	{
		this.x += vec.x * scalar;
		this.y += vec.y * scalar;
		this.z += vec.z * scalar;
		return this;
	}

	public DGVector3 mulAdd(DGVector3 vec, DGVector3 mulVec)
	{
		this.x += vec.x * mulVec.x;
		this.y += vec.y * mulVec.y;
		this.z += vec.z * mulVec.z;
		return this;
	}

	/** @return The euclidean length */
	public static DGFixedPoint len(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		return DGMath.Sqrt(x * x + y * y + z * z);
	}

	public DGFixedPoint len()
	{
		return DGMath.Sqrt(x * x + y * y + z * z);
	}

	/** @return The squared euclidean length */
	public static DGFixedPoint len2(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		return x * x + y * y + z * z;
	}

	public DGFixedPoint len2()
	{
		return x * x + y * y + z * z;
	}

	/** @param vector The other vector
	 * @return Whether this and the other vector are equal */
	public bool idt(DGVector3 vector)
	{
		return x == vector.x && y == vector.y && z == vector.z;
	}

	/** @return The euclidean distance between the two specified vectors */
	public static DGFixedPoint dst(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint z1, DGFixedPoint x2, DGFixedPoint y2,
		DGFixedPoint z2)
	{
		DGFixedPoint a = x2 - x1;
		DGFixedPoint b = y2 - y1;
		DGFixedPoint c = z2 - z1;
		return DGMath.Sqrt(a * a + b * b + c * c);
	}

	public DGFixedPoint dst(DGVector3 vector)
	{
		DGFixedPoint a = vector.x - x;
		DGFixedPoint b = vector.y - y;
		DGFixedPoint c = vector.z - z;
		return DGMath.Sqrt(a * a + b * b + c * c);
	}

	/** @return the distance between this point and the given point */
	public DGFixedPoint dst(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		DGFixedPoint a = x - this.x;
		DGFixedPoint b = y - this.y;
		DGFixedPoint c = z - this.z;
		return DGMath.Sqrt(a * a + b * b + c * c);
	}

	/** @return the squared distance between the given points */
	public static DGFixedPoint dst2(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint z1, DGFixedPoint x2, DGFixedPoint y2,
		DGFixedPoint z2)
	{
		DGFixedPoint a = x2 - x1;
		DGFixedPoint b = y2 - y1;
		DGFixedPoint c = z2 - z1;
		return a * a + b * b + c * c;
	}

	public DGFixedPoint dst2(DGVector3 point)
	{
		DGFixedPoint a = point.x - x;
		DGFixedPoint b = point.y - y;
		DGFixedPoint c = point.z - z;
		return a * a + b * b + c * c;
	}

	/** Returns the squared distance between this point and the given point
	 * @param x The x-component of the other point
	 * @param y The y-component of the other point
	 * @param z The z-component of the other point
	 * @return The squared distance */
	public DGFixedPoint dst2(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		DGFixedPoint a = x - this.x;
		DGFixedPoint b = y - this.y;
		DGFixedPoint c = z - this.z;
		return a * a + b * b + c * c;
	}

	public DGVector3 nor()
	{
		DGFixedPoint len2 = this.len2();
		if (len2 == (DGFixedPoint) 0f || len2 == (DGFixedPoint) 1f) return this;
		return this.scl((DGFixedPoint) 1f / DGMath.Sqrt(len2));
	}

	/** @return The dot product between the two vectors */
	public static DGFixedPoint dot(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint z1, DGFixedPoint x2, DGFixedPoint y2,
		DGFixedPoint z2)
	{
		return x1 * x2 + y1 * y2 + z1 * z2;
	}

	public DGFixedPoint dot(DGVector3 vector)
	{
		return x * vector.x + y * vector.y + z * vector.z;
	}

	/** Returns the dot product between this and the given vector.
	 * @param x The x-component of the other vector
	 * @param y The y-component of the other vector
	 * @param z The z-component of the other vector
	 * @return The dot product */
	public DGFixedPoint dot(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		return this.x * x + this.y * y + this.z * z;
	}

	/** Sets this vector to the cross product between it and the other vector.
	 * @param vector The other vector
	 * @return This vector for chaining */
	public DGVector3 crs(DGVector3 vector)
	{
		return this.set(y * vector.z - z * vector.y, z * vector.x - x * vector.z, x * vector.y - y * vector.x);
	}

	/** Sets this vector to the cross product between it and the other vector.
	 * @param x The x-component of the other vector
	 * @param y The y-component of the other vector
	 * @param z The z-component of the other vector
	 * @return This vector for chaining */
	public DGVector3 crs(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		return this.set(this.y * z - this.z * y, this.z * x - this.x * z, this.x * y - this.y * x);
	}

	/** Left-multiplies the vector by the given 4x3 column major matrix. The matrix should be composed by a 3x3 matrix representing
	 * rotation and scale plus a 1x3 matrix representing the translation.
	 * @param matrix The matrix
	 * @return This vector for chaining */
	public DGVector3 mul4x3(DGFixedPoint[] matrix)
	{
		return set(x * matrix[0] + y * matrix[3] + z * matrix[6] + matrix[9],
			x * matrix[1] + y * matrix[4] + z * matrix[7] + matrix[10],
			x * matrix[2] + y * matrix[5] + z * matrix[8] + matrix[11]);
	}

	/** Left-multiplies the vector by the given matrix, assuming the fourth (w) component of the vector is 1.
	 * @param matrix The matrix
	 * @return This vector for chaining */
	public DGVector3 mul(DGMatrix4x4 matrix)
	{
		return this.set(
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
	public DGVector3 traMul(DGMatrix4x4 matrix)
	{
		return this.set(
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
	public DGVector3 mul(DGMatrix3x3 matrix)
	{
		return set(x * matrix.m00 + y * matrix.m01 + z * matrix.m02,
			x * matrix.m10 + y * matrix.m11 + z * matrix.m12,
			x * matrix.m20 + y * matrix.m21 + z * matrix.m22);
	}

	/** Multiplies the vector by the transpose of the given matrix.
	 * @param matrix The matrix
	 * @return This vector for chaining */
	public DGVector3 traMul(DGMatrix3x3 matrix)
	{
		return set(x * matrix.m00 + y * matrix.m10 + z * matrix.m20,
			x * matrix.m01 + y * matrix.m11 + z * matrix.m21,
			x * matrix.m02 + y * matrix.m12 + z * matrix.m22);
	}

	/** Multiplies the vector by the given {@link Quaternion}.
	 * @return This vector for chaining */
	public DGVector3 mul(DGQuaternion quat)
	{
		return quat.transform(this);
	}

	/** Multiplies this vector by the given matrix dividing by w, assuming the fourth (w) component of the vector is 1. This is
	 * mostly used to project/unproject vectors via a perspective projection matrix.
	 *
	 * @param matrix The matrix.
	 * @return This vector for chaining */
	public DGVector3 prj(DGMatrix4x4 matrix)
	{
		DGFixedPoint l_w = (DGFixedPoint) 1f / (x * matrix.m30 + y * matrix.m31 +
		                                        z * matrix.m32 +
		                                        matrix.m33);
		return this.set(
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
	public DGVector3 rot(DGMatrix4x4 matrix)
	{
		return this.set(x * matrix.m00 + y * matrix.m01 + z * matrix.m02,
			x * matrix.m10 + y * matrix.m11 + z * matrix.m12,
			x * matrix.m20 + y * matrix.m21 + z * matrix.m22);
	}

	/** Multiplies this vector by the transpose of the first three columns of the matrix. Note: only works for translation and
	 * rotation, does not work for scaling. For those, use {@link #rot(Matrix4)} with {@link Matrix4#inv()}.
	 * @param matrix The transformation matrix
	 * @return The vector for chaining */
	public DGVector3 unrotate(DGMatrix4x4 matrix)
	{
		return this.set(x * matrix.m00 + y * matrix.m10 + z * matrix.m20,
			x * matrix.m01 + y * matrix.m11 + z * matrix.m21,
			x * matrix.m02 + y * matrix.m12 + z * matrix.m22);
	}

	/** Translates this vector in the direction opposite to the translation of the matrix and the multiplies this vector by the
	 * transpose of the first three columns of the matrix. Note: only works for translation and rotation, does not work for
	 * scaling. For those, use {@link #mul(Matrix4)} with {@link Matrix4#inv()}.
	 * @param matrix The transformation matrix
	 * @return The vector for chaining */
	public DGVector3 untransform(DGMatrix4x4 matrix)
	{
		x -= matrix.m03;
		y -= matrix.m13;
		z -= matrix.m23;
		return this.set(x * matrix.m00 + y * matrix.m10 + z * matrix.m20,
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
	public DGVector3 rotate(DGFixedPoint degrees, DGFixedPoint axisX, DGFixedPoint axisY, DGFixedPoint axisZ)
	{
		return this.mul(tmpMat.setToRotation(axisX, axisY, axisZ, degrees));
	}

	/** Rotates this vector by the given angle in radians around the given axis.
	 *
	 * @param radians the angle in radians
	 * @param axisX the x-component of the axis
	 * @param axisY the y-component of the axis
	 * @param axisZ the z-component of the axis
	 * @return This vector for chaining */
	public DGVector3 rotateRad(DGFixedPoint radians, DGFixedPoint axisX, DGFixedPoint axisY, DGFixedPoint axisZ)
	{
		return this.mul(tmpMat.setToRotationRad(axisX, axisY, axisZ, radians));
	}

	/** Rotates this vector by the given angle in degrees around the given axis.
	 *
	 * @param axis the axis
	 * @param degrees the angle in degrees
	 * @return This vector for chaining */
	public DGVector3 rotate(DGVector3 axis, DGFixedPoint degrees)
	{
		tmpMat = tmpMat.setToRotation(axis, degrees);
		return this.mul(tmpMat);
	}

	/** Rotates this vector by the given angle in radians around the given axis.
	 *
	 * @param axis the axis
	 * @param radians the angle in radians
	 * @return This vector for chaining */
	public DGVector3 rotateRad(DGVector3 axis, DGFixedPoint radians)
	{
		tmpMat = tmpMat.setToRotationRad(axis, radians);
		return this.mul(tmpMat);
	}

	public bool isUnit()
	{
		return isUnit((DGFixedPoint) 0.000000001f);
	}

	public bool isUnit(DGFixedPoint margin)
	{
		return DGMath.Abs(len2() - (DGFixedPoint) 1f) < margin;
	}

	public bool isZero()
	{
		return x == (DGFixedPoint) 0 && y == (DGFixedPoint) 0 && z == (DGFixedPoint) 0;
	}

	public bool isZero(DGFixedPoint margin)
	{
		return len2() < margin;
	}

	public bool isOnLine(DGVector3 other, DGFixedPoint epsilon)
	{
		return len2(y * other.z - z * other.y, z * other.x - x * other.z, x * other.y - y * other.x) <= epsilon;
	}

	public bool isOnLine(DGVector3 other)
	{
		return len2(y * other.z - z * other.y, z * other.x - x * other.z,
			       x * other.y - y * other.x) <= DGMath.Epsilon;
	}

	public bool isCollinear(DGVector3 other, DGFixedPoint epsilon)
	{
		return isOnLine(other, epsilon) && hasSameDirection(other);
	}

	public bool isCollinear(DGVector3 other)
	{
		return isOnLine(other) && hasSameDirection(other);
	}

	public bool isCollinearOpposite(DGVector3 other, DGFixedPoint epsilon)
	{
		return isOnLine(other, epsilon) && hasOppositeDirection(other);
	}

	public bool isCollinearOpposite(DGVector3 other)
	{
		return isOnLine(other) && hasOppositeDirection(other);
	}

	public bool isPerpendicular(DGVector3 vector)
	{
		return DGMath.IsZero(dot(vector));
	}

	public bool isPerpendicular(DGVector3 vector, DGFixedPoint epsilon)
	{
		return DGMath.IsZero(dot(vector), epsilon);
	}

	public bool hasSameDirection(DGVector3 vector)
	{
		return dot(vector) > (DGFixedPoint) 0;
	}

	public bool hasOppositeDirection(DGVector3 vector)
	{
		return dot(vector) < (DGFixedPoint) 0;
	}

	public DGVector3 lerp(DGVector3 target, DGFixedPoint alpha)
	{
		x += alpha * (target.x - x);
		y += alpha * (target.y - y);
		z += alpha * (target.z - z);
		return this;
	}

	public DGVector3 interpolate(DGVector3 target, DGFixedPoint alpha, DGInterpolation interpolator)
	{
		return lerp(target, interpolator.Apply((DGFixedPoint) 0f, (DGFixedPoint) 1f, alpha));
	}

	/** Spherically interpolates between this vector and the target vector by alpha which is in the range [0,1]. The result is
	 * stored in this vector.
	 *
	 * @param target The target vector
	 * @param alpha The interpolation coefficient
	 * @return This vector for chaining. */
	public DGVector3 slerp(DGVector3 target, DGFixedPoint alpha)
	{
		//		DGFixedPoint dot = this.dot(target);
		//		// If the inputs are too close for comfort, simply linearly interpolate.
		//		if (dot > (DGFixedPoint) 0.9995 || dot < (DGFixedPoint) (-0.9995)) return lerp(target, alpha);
		//
		//		// theta0 = angle between input vectors
		//		DGFixedPoint theta0 = DGMath.Acos(dot);
		//		// theta = angle between this vector and result
		//		DGFixedPoint theta = theta0 * alpha;
		//
		//		DGFixedPoint st = DGMath.Sin(theta);
		//		DGFixedPoint tx = target.x - x * dot;
		//		DGFixedPoint ty = target.y - y * dot;
		//		DGFixedPoint tz = target.z - z * dot;
		//		DGFixedPoint l2 = tx * tx + ty * ty + tz * tz;
		//		DGFixedPoint dl =
		//			st * ((l2 < (DGFixedPoint) 0.0001f) ? (DGFixedPoint) 1f : (DGFixedPoint) 1f / DGMath.Sqrt(l2));
		//
		//		return scl(DGMath.Cos(theta)).add(tx * dl, ty * dl, tz * dl).nor();

		//https://stackoverflow.com/questions/67919193/how-does-unity-implements-vector3-slerp-exactly
		// Dot product - the cosine of the angle between 2 vectors.
		DGFixedPoint dot = Dot(this.cpy().normalized, target.cpy().normalized);

		// Clamp it to be in the range of Acos()
		// This may be unnecessary, but floating point
		// precision can be a fickle mistress.
		dot = DGMath.Clamp(dot, (DGFixedPoint)(-1.0f), (DGFixedPoint)1.0f);

		// Acos(dot) returns the angle between start and end,
		// And multiplying that by percent returns the angle between
		// start and the final result.
		DGFixedPoint theta = DGMath.Acos(dot) * alpha;
		DGVector3 relativeVec = target - this * dot;
		relativeVec.Normalize();

		// Orthonormal basis
		// The final result.
		return (this * DGMath.Cos(theta)) + (relativeVec * DGMath.Sin(theta));
	}

	/** Converts this {@code Vector3} to a string in the format {@code (x,y,z)}.
	 * @return a string representation of this object. */
	public override string ToString()
	{
		return "(" + x + "," + y + "," + z + ")";
	}


	public DGVector3 limit(DGFixedPoint limit)
	{
		return limit2(limit * limit);
	}

	public DGVector3 limit2(DGFixedPoint limit2)
	{
		DGFixedPoint len2 = this.len2();
		if (len2 > limit2)
		{
			scl(DGMath.Sqrt(limit2 / len2));
		}

		return this;
	}

	public DGVector3 setLength(DGFixedPoint len)
	{
		return setLength2(len * len);
	}

	public DGVector3 setLength2(DGFixedPoint len2)
	{
		DGFixedPoint oldLen2 = this.len2();
		return (oldLen2 == (DGFixedPoint) 0 || oldLen2 == len2) ? this : scl(DGMath.Sqrt(len2 / oldLen2));
	}

	public DGVector3 clamp(DGFixedPoint min, DGFixedPoint max)
	{
		DGFixedPoint len2 = this.len2();
		if (len2 == (DGFixedPoint) 0f) return this;
		DGFixedPoint max2 = max * max;
		if (len2 > max2) return scl(DGMath.Sqrt(max2 / len2));
		DGFixedPoint min2 = min * min;
		if (len2 < min2) return scl(DGMath.Sqrt(min2 / len2));
		return this;
	}


	public bool epsilonEquals(DGVector3 other, DGFixedPoint epsilon)
	{
		if (DGMath.Abs(other.x - x) > epsilon) return false;
		if (DGMath.Abs(other.y - y) > epsilon) return false;
		if (DGMath.Abs(other.z - z) > epsilon) return false;
		return true;
	}

	/** Compares this vector with the other vector, using the supplied epsilon for fuzzy equality testing.
	 * @return whether the vectors are the same. */
	public bool epsilonEquals(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint epsilon)
	{
		if (DGMath.Abs(x - this.x) > epsilon) return false;
		if (DGMath.Abs(y - this.y) > epsilon) return false;
		if (DGMath.Abs(z - this.z) > epsilon) return false;
		return true;
	}

	/** Compares this vector with the other vector using MathUtils.FLOAT_ROUNDING_ERROR for fuzzy equality testing
	 *
	 * @param other other vector to compare
	 * @return true if vector are equal, otherwise false */
	public bool epsilonEquals(DGVector3 other)
	{
		return epsilonEquals(other, DGMath.Epsilon);
	}

	/** Compares this vector with the other vector using MathUtils.FLOAT_ROUNDING_ERROR for fuzzy equality testing
	 *
	 * @param x x component of the other vector to compare
	 * @param y y component of the other vector to compare
	 * @param z z component of the other vector to compare
	 * @return true if vector are equal, otherwise false */
	public bool epsilonEquals(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
	{
		return epsilonEquals(x, y, z, DGMath.Epsilon);
	}

	public DGVector3 setZero()
	{
		this.x = (DGFixedPoint) 0;
		this.y = (DGFixedPoint) 0;
		this.z = (DGFixedPoint) 0;
		return this;
	}
}