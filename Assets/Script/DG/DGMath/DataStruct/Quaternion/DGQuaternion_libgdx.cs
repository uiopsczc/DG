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
	/// A simple quaternion class.
	/// @see <a href="http://en.wikipedia.org/wiki/Quaternion">http://en.wikipedia.org/wiki/Quaternion</a>
	/// </summary>
	public partial struct DGQuaternion
	{
		private static DGQuaternion tmp1 = new DGQuaternion((DGFixedPoint)0, (DGFixedPoint)0, (DGFixedPoint)0, (DGFixedPoint)0);
		private static DGQuaternion tmp2 = new DGQuaternion((DGFixedPoint)0, (DGFixedPoint)0, (DGFixedPoint)0, (DGFixedPoint)0);

		public DGFixedPoint x;
		public DGFixedPoint y;
		public DGFixedPoint z;
		public DGFixedPoint w;

		public static DGQuaternion default2
		{
			get
			{
				DGQuaternion result = default;
				result.w = (DGFixedPoint)1;
				return result;
			}
		}

		/** Constructor, sets the four components of the quaternion.
		 * @param x The x-component
		 * @param y The y-component
		 * @param z The z-component
		 * @param w The w-component */
		public DGQuaternion(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		/** Constructor, sets the quaternion components from the given quaternion.
		 * 
		 * @param quaternion The quaternion to copy. */
		public DGQuaternion(DGQuaternion quaternion)
		{
			this.x = quaternion.x;
			this.y = quaternion.y;
			this.z = quaternion.z;
			this.w = quaternion.w;
		}

		/** Constructor, sets the quaternion from the given axis vector and the angle around that axis in degrees.
		 * 
		 * @param axis The axis
		 * @param angle The angle in degrees. */
		public DGQuaternion(DGVector3 axis, DGFixedPoint angle)
		{
			DGFixedPoint x = axis.x;
			DGFixedPoint y = axis.y;
			DGFixedPoint z = axis.z;
			DGFixedPoint radians = angle * DGMath.Deg2Rad;


			DGFixedPoint d = DGVector3.len(x, y, z);
			if (d == (DGFixedPoint)0f)
			{
				this.x = (DGFixedPoint)0;
				this.y = (DGFixedPoint)0;
				this.z = (DGFixedPoint)0;
				this.w = (DGFixedPoint)1;
			}
			else
			{
				d = (DGFixedPoint)1f / d;
				DGFixedPoint l_ang = radians < (DGFixedPoint)0 ? DGMath.TwoPI - (-radians % DGMath.TwoPI) : radians % DGMath.TwoPI;
				DGFixedPoint l_sin = DGMath.Sin(l_ang / (DGFixedPoint)2);
				DGFixedPoint l_cos = DGMath.Cos(l_ang / (DGFixedPoint)2);
				DGQuaternion result = new DGQuaternion(d * x * l_sin, d * y * l_sin, d * z * l_sin, l_cos);
				result = result.nor();
				this.x = result.x;
				this.y = result.y;
				this.z = result.z;
				this.w = result.w;
			}
		}

		/** Sets the components of the quaternion
		 * @param x The x-component
		 * @param y The y-component
		 * @param z The z-component
		 * @param w The w-component
		 * @return This quaternion for chaining */
		public DGQuaternion set(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
			return this;
		}

		/** Sets the quaternion components from the given quaternion.
		 * @param quaternion The quaternion.
		 * @return This quaternion for chaining. */
		public DGQuaternion set(DGQuaternion quaternion)
		{
			return this.set(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
		}

		/** Sets the quaternion components from the given axis and angle around that axis.
		 * 
		 * @param axis The axis
		 * @param angle The angle in degrees
		 * @return This quaternion for chaining. */
		public DGQuaternion set(DGVector3 axis, DGFixedPoint angle)
		{
			return setFromAxis(axis.x, axis.y, axis.z, angle);
		}

		/** @return a copy of this quaternion */
		public DGQuaternion cpy()
		{
			return new DGQuaternion(this);
		}

		/** @return the euclidean length of the specified quaternion */
		public static DGFixedPoint len(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint w)
		{
			return DGMath.Sqrt(x * x + y * y + z * z + w * w);
		}

		/** @return the euclidean length of this quaternion */
		public DGFixedPoint len()
		{
			return DGMath.Sqrt(x * x + y * y + z * z + w * w);
		}

		public override string ToString()
		{
			return "[" + x + "|" + y + "|" + z + "|" + w + "]";
		}

		/** Sets the quaternion to the given euler angles in degrees.
		 * @param yaw the rotation around the y axis in degrees
		 * @param pitch the rotation around the x axis in degrees
		 * @param roll the rotation around the z axis degrees
		 * @return this quaternion */
		public DGQuaternion setEulerAngles(DGFixedPoint yaw, DGFixedPoint pitch, DGFixedPoint roll)
		{
			return setEulerAnglesRad(yaw * DGMath.Deg2Rad, pitch * DGMath.Deg2Rad,
				roll * DGMath.Deg2Rad);
		}

		/** Sets the quaternion to the given euler angles in radians.
		 * @param yaw the rotation around the y axis in radians
		 * @param pitch the rotation around the x axis in radians
		 * @param roll the rotation around the z axis in radians
		 * @return this quaternion */
		public DGQuaternion setEulerAnglesRad(DGFixedPoint yaw, DGFixedPoint pitch, DGFixedPoint roll)
		{
			DGFixedPoint hr = roll * (DGFixedPoint)0.5f;
			DGFixedPoint shr = DGMath.Sin(hr);
			DGFixedPoint chr = DGMath.Cos(hr);
			DGFixedPoint hp = pitch * (DGFixedPoint)0.5f;
			DGFixedPoint shp = DGMath.Sin(hp);
			DGFixedPoint chp = DGMath.Cos(hp);
			DGFixedPoint hy = yaw * (DGFixedPoint)0.5f;
			DGFixedPoint shy = DGMath.Sin(hy);
			DGFixedPoint chy = DGMath.Cos(hy);
			DGFixedPoint chy_shp = chy * shp;
			DGFixedPoint shy_chp = shy * chp;
			DGFixedPoint chy_chp = chy * chp;
			DGFixedPoint shy_shp = shy * shp;

			x = (chy_shp * chr) +
				(shy_chp * shr); // cos(yaw/2) * sin(pitch/2) * cos(roll/2) + sin(yaw/2) * cos(pitch/2) * sin(roll/2)
			y = (shy_chp * chr) -
				(chy_shp * shr); // sin(yaw/2) * cos(pitch/2) * cos(roll/2) - cos(yaw/2) * sin(pitch/2) * sin(roll/2)
			z = (chy_chp * shr) -
				(shy_shp * chr); // cos(yaw/2) * cos(pitch/2) * sin(roll/2) - sin(yaw/2) * sin(pitch/2) * cos(roll/2)
			w = (chy_chp * chr) +
				(shy_shp * shr); // cos(yaw/2) * cos(pitch/2) * cos(roll/2) + sin(yaw/2) * sin(pitch/2) * sin(roll/2)
			return this;
		}

		/** Get the pole of the gimbal lock, if any.
		 * @return positive (+1) for north pole, negative (-1) for south pole, zero (0) when no gimbal lock */
		public DGFixedPoint getGimbalPole()
		{
			DGFixedPoint t = y * x + z * w;
			return t > (DGFixedPoint)0.499f ? (DGFixedPoint)1 : (t < (DGFixedPoint)(-0.499f) ? (DGFixedPoint)(-1) : (DGFixedPoint)0);
		}

		/** Get the roll euler angle in radians, which is the rotation around the z axis. Requires that this quaternion is normalized.
		 * @return the rotation around the z axis in radians (between -PI and +PI) */
		public DGFixedPoint getRollRad()
		{
			DGFixedPoint pole = getGimbalPole();
			return pole == (DGFixedPoint)0
				? DGMath.Atan2((DGFixedPoint)2f * (w * z + y * x), (DGFixedPoint)1f - (DGFixedPoint)2f * (x * x + z * z))
				: pole * (DGFixedPoint)2f * DGMath.Atan2(y, w);
		}

		/** Get the roll euler angle in degrees, which is the rotation around the z axis. Requires that this quaternion is normalized.
		 * @return the rotation around the z axis in degrees (between -180 and +180) */
		public DGFixedPoint getRoll()
		{
			return getRollRad() * DGMath.Rad2Deg;
		}

		/** Get the pitch euler angle in radians, which is the rotation around the x axis. Requires that this quaternion is normalized.
		 * @return the rotation around the x axis in radians (between -(PI/2) and +(PI/2)) */
		public DGFixedPoint getPitchRad()
		{
			DGFixedPoint pole = getGimbalPole();
			return pole == (DGFixedPoint)0
				? DGMath.Asin(DGMath.Clamp((DGFixedPoint)2f * (w * x - z * y), (DGFixedPoint)(-1f), (DGFixedPoint)1f))
				: pole * DGMath.PI * (DGFixedPoint)0.5f;
		}

		/** Get the pitch euler angle in degrees, which is the rotation around the x axis. Requires that this quaternion is normalized.
		 * @return the rotation around the x axis in degrees (between -90 and +90) */
		public DGFixedPoint getPitch()
		{
			return getPitchRad() * DGMath.Rad2Deg;
		}

		/** Get the yaw euler angle in radians, which is the rotation around the y axis. Requires that this quaternion is normalized.
		 * @return the rotation around the y axis in radians (between -PI and +PI) */
		public DGFixedPoint getYawRad()
		{
			return getGimbalPole() == (DGFixedPoint)0
				? DGMath.Atan2((DGFixedPoint)2f * (y * w + x * z), (DGFixedPoint)1f - (DGFixedPoint)2f * (y * y + x * x))
				: (DGFixedPoint)0f;
		}

		/** Get the yaw euler angle in degrees, which is the rotation around the y axis. Requires that this quaternion is normalized.
		 * @return the rotation around the y axis in degrees (between -180 and +180) */
		public DGFixedPoint getYaw()
		{
			return getYawRad() * DGMath.Rad2Deg;
		}

		public static DGFixedPoint len2(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint w)
		{
			return x * x + y * y + z * z + w * w;
		}

		/** @return the length of this quaternion without square root */
		public DGFixedPoint len2()
		{
			return x * x + y * y + z * z + w * w;
		}

		/** Normalizes this quaternion to unit length
		 * @return the quaternion for chaining */
		public DGQuaternion nor()
		{
			DGFixedPoint len = this.len2();
			if (len != (DGFixedPoint)0f && !DGMath.IsEqual(len, (DGFixedPoint)1f))
			{
				len = DGMath.Sqrt(len);
				w /= len;
				x /= len;
				y /= len;
				z /= len;
			}

			return this;
		}

		/** Conjugate the quaternion.
		 * 
		 * @return This quaternion for chaining */
		public DGQuaternion conjugate()
		{
			x = -x;
			y = -y;
			z = -z;
			return this;
		}

		// TODO : this would better fit into the vector3 class
		/** Transforms the given vector using this quaternion
		 * 
		 * @param v Vector to transform */
		public DGVector3 transform(DGVector3 v)
		{
			tmp2 = tmp2.set(this);
			tmp2 = tmp2.conjugate();
			tmp2 = tmp2.mulLeft(tmp1.set(v.x, v.y, v.z, (DGFixedPoint)0)).mulLeft(this);

			v.x = tmp2.x;
			v.y = tmp2.y;
			v.z = tmp2.z;
			return v;
		}

		/** Multiplies this quaternion with another one in the form of this = this * other
		 * 
		 * @param other Quaternion to multiply with
		 * @return This quaternion for chaining */
		public DGQuaternion mul(DGQuaternion other)
		{
			DGFixedPoint newX = this.w * other.x + this.x * other.w + this.y * other.z - this.z * other.y;
			DGFixedPoint newY = this.w * other.y + this.y * other.w + this.z * other.x - this.x * other.z;
			DGFixedPoint newZ = this.w * other.z + this.z * other.w + this.x * other.y - this.y * other.x;
			DGFixedPoint newW = this.w * other.w - this.x * other.x - this.y * other.y - this.z * other.z;
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
			return this;
		}

		/** Multiplies this quaternion with another one in the form of this = this * other
		 * 
		 * @param x the x component of the other quaternion to multiply with
		 * @param y the y component of the other quaternion to multiply with
		 * @param z the z component of the other quaternion to multiply with
		 * @param w the w component of the other quaternion to multiply with
		 * @return This quaternion for chaining */
		public DGQuaternion mul(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint w)
		{
			DGFixedPoint newX = this.w * x + this.x * w + this.y * z - this.z * y;
			DGFixedPoint newY = this.w * y + this.y * w + this.z * x - this.x * z;
			DGFixedPoint newZ = this.w * z + this.z * w + this.x * y - this.y * x;
			DGFixedPoint newW = this.w * w - this.x * x - this.y * y - this.z * z;
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
			return this;
		}

		/** Multiplies this quaternion with another one in the form of this = other * this
		 * 
		 * @param other Quaternion to multiply with
		 * @return This quaternion for chaining */
		public DGQuaternion mulLeft(DGQuaternion other)
		{
			DGFixedPoint newX = other.w * this.x + other.x * this.w + other.y * this.z - other.z * this.y;
			DGFixedPoint newY = other.w * this.y + other.y * this.w + other.z * this.x - other.x * this.z;
			DGFixedPoint newZ = other.w * this.z + other.z * this.w + other.x * this.y - other.y * this.x;
			DGFixedPoint newW = other.w * this.w - other.x * this.x - other.y * this.y - other.z * this.z;
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
			return this;
		}

		/** Multiplies this quaternion with another one in the form of this = other * this
		 * 
		 * @param x the x component of the other quaternion to multiply with
		 * @param y the y component of the other quaternion to multiply with
		 * @param z the z component of the other quaternion to multiply with
		 * @param w the w component of the other quaternion to multiply with
		 * @return This quaternion for chaining */
		public DGQuaternion mulLeft(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint w)
		{
			DGFixedPoint newX = w * this.x + x * this.w + y * this.z - z * this.y;
			DGFixedPoint newY = w * this.y + y * this.w + z * this.x - x * this.z;
			DGFixedPoint newZ = w * this.z + z * this.w + x * this.y - y * this.x;
			DGFixedPoint newW = w * this.w - x * this.x - y * this.y - z * this.z;
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
			return this;
		}

		/** Add the x,y,z,w components of the passed in quaternion to the ones of this quaternion */
		public DGQuaternion add(DGQuaternion quaternion)
		{
			this.x += quaternion.x;
			this.y += quaternion.y;
			this.z += quaternion.z;
			this.w += quaternion.w;
			return this;
		}

		/** Add the x,y,z,w components of the passed in quaternion to the ones of this quaternion */
		public DGQuaternion add(DGFixedPoint qx, DGFixedPoint qy, DGFixedPoint qz, DGFixedPoint qw)
		{
			this.x += qx;
			this.y += qy;
			this.z += qz;
			this.w += qw;
			return this;
		}

		// TODO : the matrix4 set(quaternion) doesnt set the last row+col of the matrix to 0,0,0,1 so... that's why there is this
		// method
		/** Fills a 4x4 matrix with the rotation matrix represented by this quaternion.
		 * 
		 * @param matrix Matrix to fill */
		public void toMatrix(DGFixedPoint[] matrix)
		{
			DGFixedPoint xx = x * x;
			DGFixedPoint xy = x * y;
			DGFixedPoint xz = x * z;
			DGFixedPoint xw = x * w;
			DGFixedPoint yy = y * y;
			DGFixedPoint yz = y * z;
			DGFixedPoint yw = y * w;
			DGFixedPoint zz = z * z;
			DGFixedPoint zw = z * w;
			// Set matrix from quaternion
			matrix[DGMatrix4x4.M00Index] = (DGFixedPoint)1 - (DGFixedPoint)2 * (yy + zz);
			matrix[DGMatrix4x4.M01Index] = (DGFixedPoint)2 * (xy - zw);
			matrix[DGMatrix4x4.M02Index] = (DGFixedPoint)2 * (xz + yw);
			matrix[DGMatrix4x4.M03Index] = (DGFixedPoint)0;
			matrix[DGMatrix4x4.M10Index] = (DGFixedPoint)2 * (xy + zw);
			matrix[DGMatrix4x4.M11Index] = (DGFixedPoint)1 - (DGFixedPoint)2 * (xx + zz);
			matrix[DGMatrix4x4.M12Index] = (DGFixedPoint)2 * (yz - xw);
			matrix[DGMatrix4x4.M13Index] = (DGFixedPoint)0;
			matrix[DGMatrix4x4.M20Index] = (DGFixedPoint)2 * (xz - yw);
			matrix[DGMatrix4x4.M21Index] = (DGFixedPoint)2 * (yz + xw);
			matrix[DGMatrix4x4.M22Index] = (DGFixedPoint)1 - (DGFixedPoint)2 * (xx + yy);
			matrix[DGMatrix4x4.M23Index] = (DGFixedPoint)0;
			matrix[DGMatrix4x4.M30Index] = (DGFixedPoint)0;
			matrix[DGMatrix4x4.M31Index] = (DGFixedPoint)0;
			matrix[DGMatrix4x4.M32Index] = (DGFixedPoint)0;
			matrix[DGMatrix4x4.M33Index] = (DGFixedPoint)1;
		}

		/** Sets the quaternion to an identity Quaternion
		 * @return this quaternion for chaining */
		public DGQuaternion idt()
		{
			return this.set((DGFixedPoint)0, (DGFixedPoint)0, (DGFixedPoint)0, (DGFixedPoint)1);
		}

		/** @return If this quaternion is an identity Quaternion */
		public bool isIdentity()
		{
			return DGMath.IsZero(x) && DGMath.IsZero(y) && DGMath.IsZero(z) && DGMath.IsEqual(w, (DGFixedPoint)1f);
		}

		/** @return If this quaternion is an identity Quaternion */
		public bool isIdentity(DGFixedPoint tolerance)
		{
			return DGMath.IsZero(x, tolerance) && DGMath.IsZero(y, tolerance) && DGMath.IsZero(z, tolerance)
				   && DGMath.IsEqual(w, (DGFixedPoint)1f, tolerance);
		}

		// todo : the setFromAxis(v3,float) method should replace the set(v3,float) method
		/** Sets the quaternion components from the given axis and angle around that axis.
		 * 
		 * @param axis The axis
		 * @param degrees The angle in degrees
		 * @return This quaternion for chaining. */
		public DGQuaternion setFromAxis(DGVector3 axis, DGFixedPoint degrees)
		{
			return setFromAxis(axis.x, axis.y, axis.z, degrees);
		}

		/** Sets the quaternion components from the given axis and angle around that axis.
		 * 
		 * @param axis The axis
		 * @param radians The angle in radians
		 * @return This quaternion for chaining. */
		public DGQuaternion setFromAxisRad(DGVector3 axis, DGFixedPoint radians)
		{
			return setFromAxisRad(axis.x, axis.y, axis.z, radians);
		}

		/** Sets the quaternion components from the given axis and angle around that axis.
		 * @param x X direction of the axis
		 * @param y Y direction of the axis
		 * @param z Z direction of the axis
		 * @param degrees The angle in degrees
		 * @return This quaternion for chaining. */
		public DGQuaternion setFromAxis(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint degrees)
		{
			return setFromAxisRad(x, y, z, degrees * DGMath.Deg2Rad);
		}

		/** Sets the quaternion components from the given axis and angle around that axis.
		 * @param x X direction of the axis
		 * @param y Y direction of the axis
		 * @param z Z direction of the axis
		 * @param radians The angle in radians
		 * @return This quaternion for chaining. */
		public DGQuaternion setFromAxisRad(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint radians)
		{
			DGFixedPoint d = DGVector3.len(x, y, z);
			if (d == (DGFixedPoint)0f) return idt();
			d = (DGFixedPoint)1f / d;
			DGFixedPoint l_ang = radians < (DGFixedPoint)0 ? DGMath.TwoPI - (-radians % DGMath.TwoPI) : radians % DGMath.TwoPI;
			DGFixedPoint l_sin = DGMath.Sin(l_ang / (DGFixedPoint)2);
			DGFixedPoint l_cos = DGMath.Cos(l_ang / (DGFixedPoint)2);
			return this.set(d * x * l_sin, d * y * l_sin, d * z * l_sin, l_cos).nor();
		}

		/** Sets the Quaternion from the given matrix, optionally removing any scaling. */
		public DGQuaternion setFromMatrix(bool normalizeAxes, DGMatrix4x4 matrix)
		{
			DGVector3 scale = default;
			matrix.getScale(ref scale);
			return setFromAxes(normalizeAxes, matrix.m00 / scale.x, matrix.m01 / scale.y,
				matrix.m02 / scale.z,
				matrix.m10 / scale.x, matrix.m11 / scale.y, matrix.m12 / scale.z,
				matrix.m20 / scale.x,
				matrix.m21 / scale.y, matrix.m22 / scale.z);
		}

		/** Sets the Quaternion from the given rotation matrix, which must not contain scaling. */
		public DGQuaternion setFromMatrix(DGMatrix4x4 matrix)
		{
			return setFromMatrix(false, matrix);
		}

		/** Sets the Quaternion from the given matrix, optionally removing any scaling. */
		public DGQuaternion setFromMatrix(bool normalizeAxes, DGMatrix3x3 matrix)
		{
			return setFromAxes(normalizeAxes, matrix.m00, matrix.m01,
				matrix.m02,
				matrix.m10, matrix.m11, matrix.m12,
				matrix.m20,
				matrix.m21, matrix.m22);
		}

		/** Sets the Quaternion from the given rotation matrix, which must not contain scaling. */
		public DGQuaternion setFromMatrix(DGMatrix3x3 matrix)
		{
			return setFromMatrix(false, matrix);
		}

		/**
		 * <p>
		 * Sets the Quaternion from the given x-, y- and z-axis which have to be orthonormal.
		 * </p>
		 * 
		 * <p>
		 * Taken from Bones framework for JPCT, see http://www.aptalkarga.com/bones/ which in turn took it from Graphics Gem code at
		 * ftp://ftp.cis.upenn.edu/pub/graphics/shoemake/quatut.ps.Z.
		 * </p>
		 * 
		 * @param xx x-axis x-coordinate
		 * @param xy x-axis y-coordinate
		 * @param xz x-axis z-coordinate
		 * @param yx y-axis x-coordinate
		 * @param yy y-axis y-coordinate
		 * @param yz y-axis z-coordinate
		 * @param zx z-axis x-coordinate
		 * @param zy z-axis y-coordinate
		 * @param zz z-axis z-coordinate */
		public DGQuaternion setFromAxes(DGFixedPoint xx, DGFixedPoint xy, DGFixedPoint xz, DGFixedPoint yx, DGFixedPoint yy, DGFixedPoint yz, DGFixedPoint zx, DGFixedPoint zy, DGFixedPoint zz)
		{
			return setFromAxes(false, xx, xy, xz, yx, yy, yz, zx, zy, zz);
		}

		/**
		 * <p>
		 * Sets the Quaternion from the given x-, y- and z-axis.
		 * </p>
		 * 
		 * <p>
		 * Taken from Bones framework for JPCT, see http://www.aptalkarga.com/bones/ which in turn took it from Graphics Gem code at
		 * ftp://ftp.cis.upenn.edu/pub/graphics/shoemake/quatut.ps.Z.
		 * </p>
		 * 
		 * @param normalizeAxes whether to normalize the axes (necessary when they contain scaling)
		 * @param xx x-axis x-coordinate
		 * @param xy x-axis y-coordinate
		 * @param xz x-axis z-coordinate
		 * @param yx y-axis x-coordinate
		 * @param yy y-axis y-coordinate
		 * @param yz y-axis z-coordinate
		 * @param zx z-axis x-coordinate
		 * @param zy z-axis y-coordinate
		 * @param zz z-axis z-coordinate */
		public DGQuaternion setFromAxes(bool normalizeAxes, DGFixedPoint xx, DGFixedPoint xy, DGFixedPoint xz, DGFixedPoint yx, DGFixedPoint yy, DGFixedPoint yz, DGFixedPoint zx,
			DGFixedPoint zy, DGFixedPoint zz)
		{
			if (normalizeAxes)
			{
				DGFixedPoint lx = (DGFixedPoint)1f / DGVector3.len(xx, xy, xz);
				DGFixedPoint ly = (DGFixedPoint)1f / DGVector3.len(yx, yy, yz);
				DGFixedPoint lz = (DGFixedPoint)1f / DGVector3.len(zx, zy, zz);
				xx *= lx;
				xy *= lx;
				xz *= lx;
				yx *= ly;
				yy *= ly;
				yz *= ly;
				zx *= lz;
				zy *= lz;
				zz *= lz;
			}

			// the trace is the sum of the diagonal elements; see
			// http://mathworld.wolfram.com/MatrixTrace.html
			DGFixedPoint t = xx + yy + zz;

			// we protect the division by s by ensuring that s>=1
			if (t >= (DGFixedPoint)0)
			{
				// |w| >= .5
				DGFixedPoint s = DGMath.Sqrt(t + (DGFixedPoint)1); // |s|>=1 ...
				w = (DGFixedPoint)0.5f * s;
				s = (DGFixedPoint)0.5f / s; // so this division isn't bad
				x = (zy - yz) * s;
				y = (xz - zx) * s;
				z = (yx - xy) * s;
			}
			else if ((xx > yy) && (xx > zz))
			{
				DGFixedPoint s = DGMath.Sqrt((DGFixedPoint)1.0 + xx - yy - zz); // |s|>=1
				x = s * (DGFixedPoint)0.5f; // |x| >= .5
				s = (DGFixedPoint)0.5f / s;
				y = (yx + xy) * s;
				z = (xz + zx) * s;
				w = (zy - yz) * s;
			}
			else if (yy > zz)
			{
				DGFixedPoint s = DGMath.Sqrt((DGFixedPoint)1.0 + yy - xx - zz); // |s|>=1
				y = s * (DGFixedPoint)0.5f; // |y| >= .5
				s = (DGFixedPoint)0.5f / s;
				x = (yx + xy) * s;
				z = (zy + yz) * s;
				w = (xz - zx) * s;
			}
			else
			{
				DGFixedPoint s = DGMath.Sqrt((DGFixedPoint)1.0 + zz - xx - yy); // |s|>=1
				z = s * (DGFixedPoint)0.5f; // |z| >= .5
				s = (DGFixedPoint)0.5f / s;
				x = (xz + zx) * s;
				y = (zy + yz) * s;
				w = (yx - xy) * s;
			}

			return this;
		}

		/** Set this quaternion to the rotation between two vectors.
		 * @param v1 The base vector, which should be normalized.
		 * @param v2 The target vector, which should be normalized.
		 * @return This quaternion for chaining */
		public DGQuaternion setFromCross(DGVector3 v1, DGVector3 v2)
		{
			DGFixedPoint dot = DGMath.Clamp(v1.dot(v2), (DGFixedPoint)(-1f), (DGFixedPoint)1f);
			DGFixedPoint angle = DGMath.Acos(dot);
			return setFromAxisRad(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x, angle);
		}

		/** Set this quaternion to the rotation between two vectors.
		 * @param x1 The base vectors x value, which should be normalized.
		 * @param y1 The base vectors y value, which should be normalized.
		 * @param z1 The base vectors z value, which should be normalized.
		 * @param x2 The target vector x value, which should be normalized.
		 * @param y2 The target vector y value, which should be normalized.
		 * @param z2 The target vector z value, which should be normalized.
		 * @return This quaternion for chaining */
		public DGQuaternion setFromCross(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint z1, DGFixedPoint x2, DGFixedPoint y2,
			DGFixedPoint z2)
		{
			DGFixedPoint dot = DGMath.Clamp(DGVector3.dot(x1, y1, z1, x2, y2, z2), (DGFixedPoint)(-1f), (DGFixedPoint)1f);
			DGFixedPoint angle = DGMath.Acos(dot);
			return setFromAxisRad(y1 * z2 - z1 * y2, z1 * x2 - x1 * z2, x1 * y2 - y1 * x2, angle);
		}

		/** Spherical linear interpolation between this quaternion and the other quaternion, based on the alpha value in the range
		 * [0,1]. Taken from Bones framework for JPCT, see http://www.aptalkarga.com/bones/
		 * @param end the end quaternion
		 * @param alpha alpha in the range [0,1]
		 * @return this quaternion for chaining */
		public DGQuaternion slerp(DGQuaternion end, DGFixedPoint alpha)
		{
			DGFixedPoint d = this.x * end.x + this.y * end.y + this.z * end.z + this.w * end.w;
			DGFixedPoint absDot = d < (DGFixedPoint)0f ? -d : d;

			// Set the first and second scale for the interpolation
			DGFixedPoint scale0 = (DGFixedPoint)1f - alpha;
			DGFixedPoint scale1 = alpha;

			// Check if the angle between the 2 quaternions was big enough to
			// warrant such calculations
			if (((DGFixedPoint)1 - absDot) > (DGFixedPoint)0.1)
			{
				// Get the angle between the 2 quaternions,
				// and then store the sin() of that angle
				DGFixedPoint angle = DGMath.Acos(absDot);
				DGFixedPoint invSinTheta = (DGFixedPoint)1f / DGMath.Sin(angle);

				// Calculate the scale for q1 and q2, according to the angle and
				// it's sine value
				scale0 = (DGMath.Sin(((DGFixedPoint)1f - alpha) * angle) * invSinTheta);
				scale1 = (DGMath.Sin((alpha * angle)) * invSinTheta);
			}

			if (d < (DGFixedPoint)0f) scale1 = -scale1;

			// Calculate the x, y, z and w values for the quaternion by using a
			// special form of linear interpolation for quaternions.
			x = (scale0 * x) + (scale1 * end.x);
			y = (scale0 * y) + (scale1 * end.y);
			z = (scale0 * z) + (scale1 * end.z);
			w = (scale0 * w) + (scale1 * end.w);

			// Return the interpolated quaternion
			return this;
		}

		/** Spherical linearly interpolates multiple quaternions and stores the result in this Quaternion. Will not destroy the data
		 * previously inside the elements of q. result = (q_1^w_1)*(q_2^w_2)* ... *(q_n^w_n) where w_i=1/n.
		 * @param q List of quaternions
		 * @return This quaternion for chaining */
		public DGQuaternion slerp(DGQuaternion[] q)
		{
			// Calculate exponents and multiply everything from left to right
			DGFixedPoint w = (DGFixedPoint)1.0f / (DGFixedPoint)q.Length;
			q[0] = set(q[0]).exp(w);
			for (int i = 1; i < q.Length; i++)
				mul(tmp1.set(q[i]).exp(w));
			nor();
			return this;
		}

		/** Spherical linearly interpolates multiple quaternions by the given weights and stores the result in this Quaternion. Will
		 * not destroy the data previously inside the elements of q or w. result = (q_1^w_1)*(q_2^w_2)* ... *(q_n^w_n) where the sum of
		 * w_i is 1. Lists must be equal in length.
		 * @param q List of quaternions
		 * @param w List of weights
		 * @return This quaternion for chaining */
		public DGQuaternion slerp(DGQuaternion[] q, DGFixedPoint[] w)
		{
			// Calculate exponents and multiply everything from left to right
			q[0] = set(q[0]).exp(w[0]);
			for (int i = 1; i < q.Length; i++)
				mul(tmp1.set(q[i]).exp(w[i]));
			nor();
			return this;
		}

		/** Calculates (this quaternion)^alpha where alpha is a real number and stores the result in this quaternion. See
		 * http://en.wikipedia.org/wiki/Quaternion#Exponential.2C_logarithm.2C_and_power
		 * @param alpha Exponent
		 * @return This quaternion for chaining */
		public DGQuaternion exp(DGFixedPoint alpha)
		{
			// Calculate |q|^alpha
			DGFixedPoint norm = len();
			DGFixedPoint normExp = DGMath.Pow(norm, alpha);

			// Calculate theta
			DGFixedPoint theta = DGMath.Acos(w / norm);

			// Calculate coefficient of basis elements
			DGFixedPoint coeff = (DGFixedPoint)0;
			if (DGMath.Abs(theta) < (DGFixedPoint)0.001
				) // If theta is small enough, use the limit of sin(alpha*theta) / sin(theta) instead of actual
				  // value
				coeff = normExp * alpha / norm;
			else
				coeff = normExp * DGMath.Sin(alpha * theta) / (norm * DGMath.Sin(theta));

			// Write results
			w = normExp * DGMath.Cos(alpha * theta);
			x *= coeff;
			y *= coeff;
			z *= coeff;

			// Fix any possible discrepancies
			nor();

			return this;
		}


		/** Get the dot product between the two quaternions (commutative).
		 * @param x1 the x component of the first quaternion
		 * @param y1 the y component of the first quaternion
		 * @param z1 the z component of the first quaternion
		 * @param w1 the w component of the first quaternion
		 * @param x2 the x component of the second quaternion
		 * @param y2 the y component of the second quaternion
		 * @param z2 the z component of the second quaternion
		 * @param w2 the w component of the second quaternion
		 * @return the dot product between the first and second quaternion. */
		public static DGFixedPoint dot(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint z1, DGFixedPoint w1, DGFixedPoint x2, DGFixedPoint y2,
			DGFixedPoint z2, DGFixedPoint w2)
		{
			return x1 * x2 + y1 * y2 + z1 * z2 + w1 * w2;
		}

		/** Get the dot product between this and the other quaternion (commutative).
		 * @param other the other quaternion.
		 * @return the dot product of this and the other quaternion. */
		public DGFixedPoint dot(DGQuaternion other)
		{
			return this.x * other.x + this.y * other.y + this.z * other.z + this.w * other.w;
		}

		/** Get the dot product between this and the other quaternion (commutative).
		 * @param x the x component of the other quaternion
		 * @param y the y component of the other quaternion
		 * @param z the z component of the other quaternion
		 * @param w the w component of the other quaternion
		 * @return the dot product of this and the other quaternion. */
		public DGFixedPoint dot(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint w)
		{
			return this.x * x + this.y * y + this.z * z + this.w * w;
		}

		/** Multiplies the components of this quaternion with the given scalar.
		 * @param scalar the scalar.
		 * @return this quaternion for chaining. */
		public DGQuaternion mul(DGFixedPoint scalar)
		{
			this.x *= scalar;
			this.y *= scalar;
			this.z *= scalar;
			this.w *= scalar;
			return this;
		}

		/** Get the axis angle representation of the rotation in degrees. The supplied vector will receive the axis (x, y and z values)
		 * of the rotation and the value returned is the angle in degrees around that axis. Note that this method will alter the
		 * supplied vector, the existing value of the vector is ignored.
		 * </p>
		 * This will normalize this quaternion if needed. The received axis is a unit vector. However, if this is an identity
		 * quaternion (no rotation), then the length of the axis may be zero.
		 * 
		 * @param axis vector which will receive the axis
		 * @return the angle in degrees
		 * @see <a href="http://en.wikipedia.org/wiki/Axis%E2%80%93angle_representation">wikipedia</a>
		 * @see <a href="http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToAngle">calculation</a> */
		public DGFixedPoint getAxisAngle(out DGVector3 axis)
		{
			return getAxisAngleRad(out axis) * DGMath.Rad2Deg;
		}

		/** Get the axis-angle representation of the rotation in radians. The supplied vector will receive the axis (x, y and z values)
		 * of the rotation and the value returned is the angle in radians around that axis. Note that this method will alter the
		 * supplied vector, the existing value of the vector is ignored.
		 * </p>
		 * This will normalize this quaternion if needed. The received axis is a unit vector. However, if this is an identity
		 * quaternion (no rotation), then the length of the axis may be zero.
		 * 
		 * @param axis vector which will receive the axis
		 * @return the angle in radians
		 * @see <a href="http://en.wikipedia.org/wiki/Axis%E2%80%93angle_representation">wikipedia</a>
		 * @see <a href="http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToAngle">calculation</a> */
		public DGFixedPoint getAxisAngleRad(out DGVector3 axis)
		{
			if (this.w > (DGFixedPoint)1)
				this.nor(); // if w>1 acos and sqrt will produce errors, this cant happen if quaternion is normalised
			DGFixedPoint angle = (DGFixedPoint)2.0 * DGMath.Acos(this.w);
			DGFixedPoint s = DGMath.Sqrt((DGFixedPoint)1 - this.w *
							   this.w); // assuming quaternion normalised then w is less than 1, so term always positive.
			if (s < DGMath.Epsilon)
			{
				// test to avoid divide by zero, s is always positive due to sqrt
				// if s close to zero then direction of axis not important
				axis.x = this.x; // if it is important that axis is normalised then replace with x=1; y=z=0;
				axis.y = this.y;
				axis.z = this.z;
			}
			else
			{
				axis.x = this.x / s; // normalise axis
				axis.y = this.y / s;
				axis.z = this.z / s;
			}

			return angle;
		}

		/** Get the angle in radians of the rotation this quaternion represents. Does not normalize the quaternion. Use
		 * {@link #getAxisAngleRad(Vector3)} to get both the axis and the angle of this rotation. Use
		 * {@link #getAngleAroundRad(Vector3)} to get the angle around a specific axis.
		 * @return the angle in radians of the rotation */
		public DGFixedPoint getAngleRad()
		{
			return (DGFixedPoint)2.0 * DGMath.Acos((this.w > (DGFixedPoint)1) ? (this.w / len()) : this.w);
		}

		/** Get the angle in degrees of the rotation this quaternion represents. Use {@link #getAxisAngle(Vector3)} to get both the
		 * axis and the angle of this rotation. Use {@link #getAngleAround(Vector3)} to get the angle around a specific axis.
		 * @return the angle in degrees of the rotation */
		public DGFixedPoint getAngle()
		{
			return getAngleRad() * DGMath.Rad2Deg;
		}

		/** Get the swing rotation and twist rotation for the specified axis. The twist rotation represents the rotation around the
		 * specified axis. The swing rotation represents the rotation of the specified axis itself, which is the rotation around an
		 * axis perpendicular to the specified axis.
		 * </p>
		 * The swing and twist rotation can be used to reconstruct the original quaternion: this = swing * twist
		 * 
		 * @param axisX the X component of the normalized axis for which to get the swing and twist rotation
		 * @param axisY the Y component of the normalized axis for which to get the swing and twist rotation
		 * @param axisZ the Z component of the normalized axis for which to get the swing and twist rotation
		 * @param swing will receive the swing rotation: the rotation around an axis perpendicular to the specified axis
		 * @param twist will receive the twist rotation: the rotation around the specified axis
		 * @see <a href="http://www.euclideanspace.com/maths/geometry/rotations/for/decomposition">calculation</a> */
		public void getSwingTwist(DGFixedPoint axisX, DGFixedPoint axisY, DGFixedPoint axisZ, ref DGQuaternion swing,
			ref DGQuaternion twist)
		{
			DGFixedPoint d = DGVector3.dot(this.x, this.y, this.z, axisX, axisY, axisZ);
			twist = twist.set(axisX * d, axisY * d, axisZ * d, this.w).nor();
			if (d < (DGFixedPoint)0) twist = twist.mul((DGFixedPoint)(-1f));
			swing = swing.set(twist).conjugate().mulLeft(this);
		}

		/** Get the swing rotation and twist rotation for the specified axis. The twist rotation represents the rotation around the
		 * specified axis. The swing rotation represents the rotation of the specified axis itself, which is the rotation around an
		 * axis perpendicular to the specified axis.
		 * </p>
		 * The swing and twist rotation can be used to reconstruct the original quaternion: this = swing * twist
		 * 
		 * @param axis the normalized axis for which to get the swing and twist rotation
		 * @param swing will receive the swing rotation: the rotation around an axis perpendicular to the specified axis
		 * @param twist will receive the twist rotation: the rotation around the specified axis
		 * @see <a href="http://www.euclideanspace.com/maths/geometry/rotations/for/decomposition">calculation</a> */
		public void getSwingTwist(DGVector3 axis, ref DGQuaternion swing, ref DGQuaternion twist)
		{
			getSwingTwist(axis.x, axis.y, axis.z, ref swing, ref twist);
		}

		/** Get the angle in radians of the rotation around the specified axis. The axis must be normalized.
		 * @param axisX the x component of the normalized axis for which to get the angle
		 * @param axisY the y component of the normalized axis for which to get the angle
		 * @param axisZ the z component of the normalized axis for which to get the angle
		 * @return the angle in radians of the rotation around the specified axis */
		public DGFixedPoint getAngleAroundRad(DGFixedPoint axisX, DGFixedPoint axisY, DGFixedPoint axisZ)
		{
			DGFixedPoint d = DGVector3.dot(this.x, this.y, this.z, axisX, axisY, axisZ);
			DGFixedPoint l2 = len2(axisX * d, axisY * d, axisZ * d, this.w);
			return DGMath.IsZero(l2)
				? (DGFixedPoint)0f
				: (DGFixedPoint)2.0 * DGMath.Acos(DGMath.Clamp((d < (DGFixedPoint)0 ? -this.w : this.w) / DGMath.Sqrt(l2), -(DGFixedPoint)1f,
					  (DGFixedPoint)1f));
		}

		/** Get the angle in radians of the rotation around the specified axis. The axis must be normalized.
		 * @param axis the normalized axis for which to get the angle
		 * @return the angle in radians of the rotation around the specified axis */
		public DGFixedPoint getAngleAroundRad(DGVector3 axis)
		{
			return getAngleAroundRad(axis.x, axis.y, axis.z);
		}

		/** Get the angle in degrees of the rotation around the specified axis. The axis must be normalized.
		 * @param axisX the x component of the normalized axis for which to get the angle
		 * @param axisY the y component of the normalized axis for which to get the angle
		 * @param axisZ the z component of the normalized axis for which to get the angle
		 * @return the angle in degrees of the rotation around the specified axis */
		public DGFixedPoint getAngleAround(DGFixedPoint axisX, DGFixedPoint axisY, DGFixedPoint axisZ)
		{
			return getAngleAroundRad(axisX, axisY, axisZ) * DGMath.Rad2Deg;
		}

		/** Get the angle in degrees of the rotation around the specified axis. The axis must be normalized.
		 * @param axis the normalized axis for which to get the angle
		 * @return the angle in degrees of the rotation around the specified axis */
		public DGFixedPoint getAngleAround(DGVector3 axis)
		{
			return getAngleAround(axis.x, axis.y, axis.z);
		}
	}
}
