using FP = DGFixedPoint;
using FPMatrix3x3 = DGMatrix3x3;
using FPInterpolation = DGInterpolation;

public partial struct DGVector2
{
	public static DGVector2 X = new DGVector2((FP) 1, (FP) 0);
	public static DGVector2 Y = new DGVector2((FP) 0, (FP) 1);
	public static DGVector2 Zero = new DGVector2((FP) 0, (FP) 0);

	/** the x-component of this vector **/
	public FP x;

	/** the y-component of this vector **/
	public FP y;

	/** Constructs a vector with the given components
	 * @param x The x-component
	 * @param y The y-component */
	public DGVector2(FP x, FP y)
	{
		this.x = x;
		this.y = y;
	}

	/** Constructs a vector from the given vector
	 * @param v The vector */
	public DGVector2(DGVector2 v)
	{
		this.x = v.x;
		this.y = v.y;
	}

	public DGVector2 cpy()
	{
		return new DGVector2(this);
	}

	public static FP len(FP x, FP y)
	{
		return DGMath.Sqrt(x * x + y * y);
	}

	public FP len()
	{
		return DGMath.Sqrt(x * x + y * y);
	}

	public static FP len2(FP x, FP y)
	{
		return x * x + y * y;
	}

	public FP len2()
	{
		return x * x + y * y;
	}

	public DGVector2 set(DGVector2 v)
	{
		x = v.x;
		y = v.y;
		return this;
	}

	/** Sets the components of this vector
	 * @param x The x-component
	 * @param y The y-component
	 * @return This vector for chaining */
	public DGVector2 set(FP x, FP y)
	{
		this.x = x;
		this.y = y;
		return this;
	}

	public DGVector2 sub(DGVector2 v)
	{
		x -= v.x;
		y -= v.y;
		return this;
	}

	/** Substracts the other vector from this vector.
	 * @param x The x-component of the other vector
	 * @param y The y-component of the other vector
	 * @return This vector for chaining */
	public DGVector2 sub(FP x, FP y)
	{
		this.x -= x;
		this.y -= y;
		return this;
	}

	public DGVector2 nor()
	{
		FP len = this.len();
		if (len != (FP) 0)
		{
			x /= len;
			y /= len;
		}

		return this;
	}

	public DGVector2 add(DGVector2 v)
	{
		x += v.x;
		y += v.y;
		return this;
	}

	/** Adds the given components to this vector
	 * @param x The x-component
	 * @param y The y-component
	 * @return This vector for chaining */
	public DGVector2 add(FP x, FP y)
	{
		this.x += x;
		this.y += y;
		return this;
	}

	public static FP dot(FP x1, FP y1, FP x2, FP y2)
	{
		return x1 * x2 + y1 * y2;
	}

	public FP dot(DGVector2 v)
	{
		return x * v.x + y * v.y;
	}

	public FP dot(FP ox, FP oy)
	{
		return x * ox + y * oy;
	}

	public DGVector2 scl(FP scalar)
	{
		x *= scalar;
		y *= scalar;
		return this;
	}

	/** Multiplies this vector by a scalar
	 * @return This vector for chaining */
	public DGVector2 scl(FP x, FP y)
	{
		this.x *= x;
		this.y *= y;
		return this;
	}

	public DGVector2 scl(DGVector2 v)
	{
		this.x *= v.x;
		this.y *= v.y;
		return this;
	}

	public DGVector2 mulAdd(DGVector2 vec, FP scalar)
	{
		this.x += vec.x * scalar;
		this.y += vec.y * scalar;
		return this;
	}

	public DGVector2 mulAdd(DGVector2 vec, DGVector2 mulVec)
	{
		this.x += vec.x * mulVec.x;
		this.y += vec.y * mulVec.y;
		return this;
	}

	public static FP dst(FP x1, FP y1, FP x2, FP y2)
	{
		FP x_d = x2 - x1;
		FP y_d = y2 - y1;
		return DGMath.Sqrt(x_d * x_d + y_d * y_d);
	}

	public FP dst(DGVector2 v)
	{
		FP x_d = v.x - x;
		FP y_d = v.y - y;
		return DGMath.Sqrt(x_d * x_d + y_d * y_d);
	}

	/** @param x The x-component of the other vector
	 * @param y The y-component of the other vector
	 * @return the distance between this and the other vector */
	public FP dst(FP x, FP y)
	{
		FP x_d = x - this.x;
		FP y_d = y - this.y;
		return DGMath.Sqrt(x_d * x_d + y_d * y_d);
	}

	public static FP dst2(FP x1, FP y1, FP x2, FP y2)
	{
		FP x_d = x2 - x1;
		FP y_d = y2 - y1;
		return x_d * x_d + y_d * y_d;
	}

	public FP dst2(DGVector2 v)
	{
		FP x_d = v.x - x;
		FP y_d = v.y - y;
		return x_d * x_d + y_d * y_d;
	}

	/** @param x The x-component of the other vector
	 * @param y The y-component of the other vector
	 * @return the squared distance between this and the other vector */
	public FP dst2(FP x, FP y)
	{
		FP x_d = x - this.x;
		FP y_d = y - this.y;
		return x_d * x_d + y_d * y_d;
	}


	public DGVector2 limit(FP limit)
	{
		return limit2(limit * limit);
	}


	public DGVector2 limit2(FP limit2)
	{
		FP len2 = this.len2();
		if (len2 > limit2)
		{
			return scl(DGMath.Sqrt(limit2 / len2));
		}

		return this;
	}

	public DGVector2 clamp(FP min, FP max)
	{
		FP len2 = this.len2();
		if (len2 == (FP) 0f) return this;
		FP max2 = max * max;
		if (len2 > max2) return scl(DGMath.Sqrt(max2 / len2));
		FP min2 = min * min;
		if (len2 < min2) return scl(DGMath.Sqrt(min2 / len2));
		return this;
	}

	public DGVector2 setLength(FP len)
	{
		return setLength2(len * len);
	}

	public DGVector2 setLength2(FP len2)
	{
		FP oldLen2 = this.len2();
		return (oldLen2 == (FP) 0 || oldLen2 == len2) ? this : scl(DGMath.Sqrt(len2 / oldLen2));
	}

	/** Converts this {@code Vector2} to a string in the format {@code (x,y)}.
	 * @return a string representation of this object. */
	public override string ToString()
	{
		return "(" + x + "," + y + ")";
	}


	/** Left-multiplies this vector by the given matrix
	 * @param mat the matrix
	 * @return this vector */
	public DGVector2 mul(FPMatrix3x3 mat)
	{
		FP x = this.x * mat.val[0] + this.y * mat.val[3] + mat.val[6];
		FP y = this.x * mat.val[1] + this.y * mat.val[4] + mat.val[7];
		this.x = x;
		this.y = y;
		return this;
	}

	/** Calculates the 2D cross product between this and the given vector.
	 * @param v the other vector
	 * @return the cross product */
	public FP crs(DGVector2 v)
	{
		return this.x * v.y - this.y * v.x;
	}

	/** Calculates the 2D cross product between this and the given vector.
	 * @param x the x-coordinate of the other vector
	 * @param y the y-coordinate of the other vector
	 * @return the cross product */
	public FP crs(FP x, FP y)
	{
		return this.x * y - this.y * x;
	}

	/** @return the angle in degrees of this vector (point) relative to the x-axis. Angles are towards the positive y-axis
	 *         (typically counter-clockwise) and between 0 and 360.
	 * @deprecated use {@link #angleDeg()} instead. */
	public FP angle()
	{
		FP angle = DGMath.Atan2(y, x) * DGMath.Rad2Deg;
		if (angle < (FP) 0) angle += (FP) 360;
		return angle;
	}

	/** @return the angle in degrees of this vector (point) relative to the given vector. Angles are towards the negative y-axis
	 *         (typically clockwise) between -180 and +180
	 * @deprecated use {@link #angleDeg(Vector2)} instead. Be ware of the changes in returned angle to counter-clockwise and the
	 *             range. */
	public FP angle(DGVector2 reference)
	{
		return DGMath.Atan2(crs(reference), dot(reference)) * DGMath.Rad2Deg;
	}

	/** @return the angle in degrees of this vector (point) relative to the x-axis. Angles are towards the positive y-axis
	 *         (typically counter-clockwise) and in the [0, 360) range. */
	public FP angleDeg()
	{
		FP angle = DGMath.Atan2(y, x) * DGMath.Rad2Deg;
		if (angle < (FP) 0) angle += (FP) 360;
		return angle;
	}

	/** @return the angle in degrees of this vector (point) relative to the given vector. Angles are towards the positive y-axis
	 *         (typically counter-clockwise.) in the [0, 360) range */
	public FP angleDeg(DGVector2 reference)
	{
		FP angle = DGMath.Atan2(reference.crs(this), reference.dot(this)) * DGMath.Rad2Deg;
		if (angle < (FP) 0) angle += (FP) 360;
		return angle;
	}

	/** @return the angle in radians of this vector (point) relative to the x-axis. Angles are towards the positive y-axis.
	 *         (typically counter-clockwise) */
	public FP angleRad()
	{
		return DGMath.Atan2(y, x);
	}

	/** @return the angle in radians of this vector (point) relative to the given vector. Angles are towards the positive y-axis.
	 *         (typically counter-clockwise.) */
	public FP angleRad(DGVector2 reference)
	{
		return DGMath.Atan2(reference.crs(this), reference.dot(this));
	}

	/** Sets the angle of the vector in degrees relative to the x-axis, towards the positive y-axis (typically counter-clockwise).
	 * @param degrees The angle in degrees to set.
	 * @deprecated use {@link #setAngleDeg(float)} instead. */
	public DGVector2 setAngle(FP degrees)
	{
		return setAngleRad(degrees * DGMath.Deg2Rad);
	}

	/** Sets the angle of the vector in degrees relative to the x-axis, towards the positive y-axis (typically counter-clockwise).
	 * @param degrees The angle in degrees to set. */
	public DGVector2 setAngleDeg(FP degrees)
	{
		return setAngleRad(degrees * DGMath.Deg2Rad);
	}

	/** Sets the angle of the vector in radians relative to the x-axis, towards the positive y-axis (typically counter-clockwise).
	 * @param radians The angle in radians to set. */
	public DGVector2 setAngleRad(FP radians)
	{
		this.set(len(), (FP) 0f);
		this.rotateRad(radians);

		return this;
	}

	/** Rotates the Vector2 by the given angle, counter-clockwise assuming the y-axis points up.
	 * @param degrees the angle in degrees
	 * @deprecated use {@link #rotateDeg(float)} instead. */
	public DGVector2 rotate(FP degrees)
	{
		return rotateRad(degrees * DGMath.Deg2Rad);
	}

	/** Rotates the Vector2 by the given angle around reference vector, counter-clockwise assuming the y-axis points up.
	 * @param degrees the angle in degrees
	 * @param reference center Vector2
	 * @deprecated use {@link #rotateAroundDeg(Vector2, float)} instead. */
	public DGVector2 rotateAround(DGVector2 reference, FP degrees)
	{
		return this.sub(reference).rotateDeg(degrees).add(reference);
	}

	/** Rotates the Vector2 by the given angle, counter-clockwise assuming the y-axis points up.
	 * @param degrees the angle in degrees */
	public DGVector2 rotateDeg(FP degrees)
	{
		return rotateRad(degrees * DGMath.Deg2Rad);
	}

	/** Rotates the Vector2 by the given angle, counter-clockwise assuming the y-axis points up.
	 * @param radians the angle in radians */
	public DGVector2 rotateRad(FP radians)
	{
		FP cos = DGMath.Cos(radians);
		FP sin = DGMath.Sin(radians);

		FP newX = this.x * cos - this.y * sin;
		FP newY = this.x * sin + this.y * cos;

		this.x = newX;
		this.y = newY;

		return this;
	}

	/** Rotates the Vector2 by the given angle around reference vector, counter-clockwise assuming the y-axis points up.
	 * @param degrees the angle in degrees
	 * @param reference center Vector2 */
	public DGVector2 rotateAroundDeg(DGVector2 reference, FP degrees)
	{
		return this.sub(reference).rotateDeg(degrees).add(reference);
	}

	/** Rotates the Vector2 by the given angle around reference vector, counter-clockwise assuming the y-axis points up.
	 * @param radians the angle in radians
	 * @param reference center Vector2 */
	public DGVector2 rotateAroundRad(DGVector2 reference, FP radians)
	{
		return this.sub(reference).rotateRad(radians).add(reference);
	}

	/** Rotates the Vector2 by 90 degrees in the specified direction, where >= 0 is counter-clockwise and < 0 is clockwise. */
	public DGVector2 rotate90(int dir)
	{
		FP x = this.x;
		if (dir >= 0)
		{
			this.x = -y;
			y = (FP) x;
		}
		else
		{
			this.x = y;
			y = -x;
		}

		return this;
	}

	public DGVector2 lerp(DGVector2 target, FP alpha)
	{
		FP invAlpha = (FP) 1.0f - alpha;
		this.x = (x * invAlpha) + (target.x * alpha);
		this.y = (y * invAlpha) + (target.y * alpha);
		return this;
	}

	public DGVector2 interpolate(DGVector2 target, FP alpha, FPInterpolation interpolation)
	{
		return lerp(target, interpolation.Apply(alpha));
	}


	public bool epsilonEquals(DGVector2 other, FP epsilon)
	{
		if (DGMath.Abs(other.x - x) > epsilon) return false;
		if (DGMath.Abs(other.y - y) > epsilon) return false;
		return true;
	}

	/** Compares this vector with the other vector, using the supplied epsilon for fuzzy equality testing.
	 * @return whether the vectors are the same. */
	public bool epsilonEquals(FP x, FP y, FP epsilon)
	{
		if (DGMath.Abs(x - this.x) > epsilon) return false;
		if (DGMath.Abs(y - this.y) > epsilon) return false;
		return true;
	}

	/** Compares this vector with the other vector using MathUtils.FLOAT_ROUNDING_ERROR for fuzzy equality testing
	 * @param other other vector to compare
	 * @return true if vector are equal, otherwise false */
	public bool epsilonEquals(DGVector2 other)
	{
		return epsilonEquals(other, DGMath.Epsilon);
	}

	/** Compares this vector with the other vector using MathUtils.FLOAT_ROUNDING_ERROR for fuzzy equality testing
	 * @param x x component of the other vector to compare
	 * @param y y component of the other vector to compare
	 * @return true if vector are equal, otherwise false */
	public bool epsilonEquals(FP x, FP y)
	{
		return epsilonEquals(x, y, DGMath.Epsilon);
	}

	public bool isUnit()
	{
		return isUnit((FP) 0.000000001f);
	}

	public bool isUnit(FP margin)
	{
		return DGMath.Abs(len2() - (FP) 1f) < margin;
	}

	public bool isZero()
	{
		return x == (FP) 0 && y == (FP) 0;
	}

	public bool isZero(FP margin)
	{
		return len2() < margin;
	}

	public bool isOnLine(DGVector2 other)
	{
		return DGMath.IsZero(x * other.y - y * other.x);
	}

	public bool isOnLine(DGVector2 other, FP epsilon)
	{
		return DGMath.IsZero(x * other.y - y * other.x, epsilon);
	}

	public bool isCollinear(DGVector2 other, FP epsilon)
	{
		return isOnLine(other, epsilon) && dot(other) > (FP) 0f;
	}

	public bool isCollinear(DGVector2 other)
	{
		return isOnLine(other) && dot(other) > (FP) 0f;
	}

	public bool isCollinearOpposite(DGVector2 other, FP epsilon)
	{
		return isOnLine(other, epsilon) && dot(other) < (FP) 0f;
	}

	public bool isCollinearOpposite(DGVector2 other)
	{
		return isOnLine(other) && dot(other) < (FP) 0f;
	}

	public bool isPerpendicular(DGVector2 vector)
	{
		return DGMath.IsZero(dot(vector));
	}

	public bool isPerpendicular(DGVector2 vector, FP epsilon)
	{
		return DGMath.IsZero(dot(vector), epsilon);
	}

	public bool hasSameDirection(DGVector2 vector)
	{
		return dot(vector) > (FP) 0;
	}

	public bool hasOppositeDirection(DGVector2 vector)
	{
		return dot(vector) < (FP) 0;
	}

	public DGVector2 setZero()
	{
		this.x = (FP) 0;
		this.y = (FP) 0;
		return this;
	}
}