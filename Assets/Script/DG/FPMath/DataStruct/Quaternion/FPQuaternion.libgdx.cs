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
    public partial struct FPQuaternion
    {
        private static FPQuaternion tmp1 = new(0, 0, 0, 0);
        private static FPQuaternion tmp2 = new(0, 0, 0, 0);

        public FP x;
        public FP y;
        public FP z;
        public FP w;

        public static FPQuaternion default2
        {
            get
            {
                FPQuaternion result = default;
                result.w = 1;
                return result;
            }
        }

        /** Constructor, sets the four components of the quaternion.
         * @param x The x-component
         * @param y The y-component
         * @param z The z-component
         * @param w The w-component */
        public FPQuaternion(FP x, FP y, FP z, FP w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /** Constructor, sets the quaternion components from the given quaternion.
         *
         * @param quaternion The quaternion to copy. */
        public FPQuaternion(FPQuaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }

        /** Constructor, sets the quaternion from the given axis vector and the angle around that axis in degrees.
         *
         * @param axis The axis
         * @param angle The angle in degrees. */
        public FPQuaternion(FPVector3 axis, FP angle)
        {
            FP x = axis.x;
            FP y = axis.y;
            FP z = axis.z;
            FP radians = angle * FPMath.DEG2RAD;


            FP d = FPVector3.len(x, y, z);
            if (d == 0f)
            {
                this.x = 0;
                this.y = 0;
                this.z = 0;
                w = 1;
            }
            else
            {
                d = 1f / d;
                FP l_ang = radians < 0 ? FPMath.TWO_PI - (-radians % FPMath.TWO_PI) : radians % FPMath.TWO_PI;
                FP l_sin = FPMath.Sin(l_ang / 2);
                FP l_cos = FPMath.Cos(l_ang / 2);
                FPQuaternion result = new FPQuaternion(d * x * l_sin, d * y * l_sin, d * z * l_sin, l_cos);
                result = result.nor();
                this.x = result.x;
                this.y = result.y;
                this.z = result.z;
                w = result.w;
            }
        }

        /** Sets the components of the quaternion
         * @param x The x-component
         * @param y The y-component
         * @param z The z-component
         * @param w The w-component
         * @return This quaternion for chaining */
        public FPQuaternion set(FP x, FP y, FP z, FP w)
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
        public FPQuaternion set(FPQuaternion quaternion)
        {
            return set(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }

        /** Sets the quaternion components from the given axis and angle around that axis.
         *
         * @param axis The axis
         * @param angle The angle in degrees
         * @return This quaternion for chaining. */
        public FPQuaternion set(FPVector3 axis, FP angle)
        {
            return setFromAxis(axis.x, axis.y, axis.z, angle);
        }

        /** @return a copy of this quaternion */
        public FPQuaternion cpy()
        {
            return new FPQuaternion(this);
        }

        /** @return the euclidean length of the specified quaternion */
        public static FP len(FP x, FP y, FP z, FP w)
        {
            return FPMath.Sqrt(x * x + y * y + z * z + w * w);
        }

        /** @return the euclidean length of this quaternion */
        public FP len()
        {
            return FPMath.Sqrt(x * x + y * y + z * z + w * w);
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
        public FPQuaternion setEulerAngles(FP yaw, FP pitch, FP roll)
        {
            return setEulerAnglesRad(yaw * FPMath.DEG2RAD, pitch * FPMath.DEG2RAD,
                roll * FPMath.DEG2RAD);
        }

        /** Sets the quaternion to the given euler angles in radians.
         * @param yaw the rotation around the y axis in radians
         * @param pitch the rotation around the x axis in radians
         * @param roll the rotation around the z axis in radians
         * @return this quaternion */
        public FPQuaternion setEulerAnglesRad(FP yaw, FP pitch, FP roll)
        {
            FP hr = roll * 0.5f;
            FP shr = FPMath.Sin(hr);
            FP chr = FPMath.Cos(hr);
            FP hp = pitch * 0.5f;
            FP shp = FPMath.Sin(hp);
            FP chp = FPMath.Cos(hp);
            FP hy = yaw * 0.5f;
            FP shy = FPMath.Sin(hy);
            FP chy = FPMath.Cos(hy);
            FP chy_shp = chy * shp;
            FP shy_chp = shy * chp;
            FP chy_chp = chy * chp;
            FP shy_shp = shy * shp;

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
        public FP getGimbalPole()
        {
            FP t = y * x + z * w;
            return t > 0.499f ? 1 : (t < (-0.499f) ? (-1) : 0);
        }

        /** Get the roll euler angle in radians, which is the rotation around the z axis. Requires that this quaternion is normalized.
         * @return the rotation around the z axis in radians (between -PI and +PI) */
        public FP getRollRad()
        {
            FP pole = getGimbalPole();
            return pole == 0
                ? FPMath.Atan2(2f * (w * z + y * x), 1f - 2f * (x * x + z * z))
                : pole * 2f * FPMath.Atan2(y, w);
        }

        /** Get the roll euler angle in degrees, which is the rotation around the z axis. Requires that this quaternion is normalized.
         * @return the rotation around the z axis in degrees (between -180 and +180) */
        public FP getRoll()
        {
            return getRollRad() * FPMath.Rad2Deg;
        }

        /** Get the pitch euler angle in radians, which is the rotation around the x axis. Requires that this quaternion is normalized.
         * @return the rotation around the x axis in radians (between -(PI/2) and +(PI/2)) */
        public FP getPitchRad()
        {
            FP pole = getGimbalPole();
            return pole == 0
                ? FPMath.Asin(FPMath.Clamp(2f * (w * x - z * y), (-1f), 1f))
                : pole * FPMath.PI * 0.5f;
        }

        /** Get the pitch euler angle in degrees, which is the rotation around the x axis. Requires that this quaternion is normalized.
         * @return the rotation around the x axis in degrees (between -90 and +90) */
        public FP getPitch()
        {
            return getPitchRad() * FPMath.Rad2Deg;
        }

        /** Get the yaw euler angle in radians, which is the rotation around the y axis. Requires that this quaternion is normalized.
         * @return the rotation around the y axis in radians (between -PI and +PI) */
        public FP getYawRad()
        {
            return getGimbalPole() == 0
                ? FPMath.Atan2(2f * (y * w + x * z), 1f - 2f * (y * y + x * x))
                : 0f;
        }

        /** Get the yaw euler angle in degrees, which is the rotation around the y axis. Requires that this quaternion is normalized.
         * @return the rotation around the y axis in degrees (between -180 and +180) */
        public FP getYaw()
        {
            return getYawRad() * FPMath.Rad2Deg;
        }

        public static FP len2(FP x, FP y, FP z, FP w)
        {
            return x * x + y * y + z * z + w * w;
        }

        /** @return the length of this quaternion without square root */
        public FP len2()
        {
            return x * x + y * y + z * z + w * w;
        }

        /** Normalizes this quaternion to unit length
         * @return the quaternion for chaining */
        public FPQuaternion nor()
        {
            FP len = len2();
            if (len != 0f && !FPMath.IsEqual(len, 1f))
            {
                len = FPMath.Sqrt(len);
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
        public FPQuaternion conjugate()
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
        public FPVector3 transform(FPVector3 v)
        {
            tmp2 = tmp2.set(this);
            tmp2 = tmp2.conjugate();
            tmp2 = tmp2.mulLeft(tmp1.set(v.x, v.y, v.z, 0)).mulLeft(this);

            v.x = tmp2.x;
            v.y = tmp2.y;
            v.z = tmp2.z;
            return v;
        }

        /** Multiplies this quaternion with another one in the form of this = this * other
         *
         * @param other Quaternion to multiply with
         * @return This quaternion for chaining */
        public FPQuaternion mul(FPQuaternion other)
        {
            FP newX = w * other.x + x * other.w + y * other.z - z * other.y;
            FP newY = w * other.y + y * other.w + z * other.x - x * other.z;
            FP newZ = w * other.z + z * other.w + x * other.y - y * other.x;
            FP newW = w * other.w - x * other.x - y * other.y - z * other.z;
            x = newX;
            y = newY;
            z = newZ;
            w = newW;
            return this;
        }

        /** Multiplies this quaternion with another one in the form of this = this * other
         *
         * @param x the x component of the other quaternion to multiply with
         * @param y the y component of the other quaternion to multiply with
         * @param z the z component of the other quaternion to multiply with
         * @param w the w component of the other quaternion to multiply with
         * @return This quaternion for chaining */
        public FPQuaternion mul(FP x, FP y, FP z, FP w)
        {
            FP newX = this.w * x + this.x * w + this.y * z - this.z * y;
            FP newY = this.w * y + this.y * w + this.z * x - this.x * z;
            FP newZ = this.w * z + this.z * w + this.x * y - this.y * x;
            FP newW = this.w * w - this.x * x - this.y * y - this.z * z;
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
        public FPQuaternion mulLeft(FPQuaternion other)
        {
            FP newX = other.w * x + other.x * w + other.y * z - other.z * y;
            FP newY = other.w * y + other.y * w + other.z * x - other.x * z;
            FP newZ = other.w * z + other.z * w + other.x * y - other.y * x;
            FP newW = other.w * w - other.x * x - other.y * y - other.z * z;
            x = newX;
            y = newY;
            z = newZ;
            w = newW;
            return this;
        }

        /** Multiplies this quaternion with another one in the form of this = other * this
         *
         * @param x the x component of the other quaternion to multiply with
         * @param y the y component of the other quaternion to multiply with
         * @param z the z component of the other quaternion to multiply with
         * @param w the w component of the other quaternion to multiply with
         * @return This quaternion for chaining */
        public FPQuaternion mulLeft(FP x, FP y, FP z, FP w)
        {
            FP newX = w * this.x + x * this.w + y * this.z - z * this.y;
            FP newY = w * this.y + y * this.w + z * this.x - x * this.z;
            FP newZ = w * this.z + z * this.w + x * this.y - y * this.x;
            FP newW = w * this.w - x * this.x - y * this.y - z * this.z;
            this.x = newX;
            this.y = newY;
            this.z = newZ;
            this.w = newW;
            return this;
        }

        /** Add the x,y,z,w components of the passed in quaternion to the ones of this quaternion */
        public FPQuaternion add(FPQuaternion quaternion)
        {
            x += quaternion.x;
            y += quaternion.y;
            z += quaternion.z;
            w += quaternion.w;
            return this;
        }

        /** Add the x,y,z,w components of the passed in quaternion to the ones of this quaternion */
        public FPQuaternion add(FP qx, FP qy, FP qz, FP qw)
        {
            x += qx;
            y += qy;
            z += qz;
            w += qw;
            return this;
        }

        // TODO : the matrix4 set(quaternion) doesnt set the last row+col of the matrix to 0,0,0,1 so... that's why there is this
        // method
        /** Fills a 4x4 matrix with the rotation matrix represented by this quaternion.
         *
         * @param matrix Matrix to fill */
        public void toMatrix(FP[] matrix)
        {
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
            matrix[FPMatrix4x4.M00Index] = 1 - 2 * (yy + zz);
            matrix[FPMatrix4x4.M01Index] = 2 * (xy - zw);
            matrix[FPMatrix4x4.M02Index] = 2 * (xz + yw);
            matrix[FPMatrix4x4.M03Index] = 0;
            matrix[FPMatrix4x4.M10Index] = 2 * (xy + zw);
            matrix[FPMatrix4x4.M11Index] = 1 - 2 * (xx + zz);
            matrix[FPMatrix4x4.M12Index] = 2 * (yz - xw);
            matrix[FPMatrix4x4.M13Index] = 0;
            matrix[FPMatrix4x4.M20Index] = 2 * (xz - yw);
            matrix[FPMatrix4x4.M21Index] = 2 * (yz + xw);
            matrix[FPMatrix4x4.M22Index] = 1 - 2 * (xx + yy);
            matrix[FPMatrix4x4.M23Index] = 0;
            matrix[FPMatrix4x4.M30Index] = 0;
            matrix[FPMatrix4x4.M31Index] = 0;
            matrix[FPMatrix4x4.M32Index] = 0;
            matrix[FPMatrix4x4.M33Index] = 1;
        }

        /** Sets the quaternion to an identity Quaternion
         * @return this quaternion for chaining */
        public FPQuaternion idt()
        {
            return set(0, 0, 0, 1);
        }

        /** @return If this quaternion is an identity Quaternion */
        public bool isIdentity()
        {
            return FPMath.IsZero(x) && FPMath.IsZero(y) && FPMath.IsZero(z) && FPMath.IsEqual(w, 1f);
        }

        /** @return If this quaternion is an identity Quaternion */
        public bool isIdentity(FP tolerance)
        {
            return FPMath.IsZero(x, tolerance) && FPMath.IsZero(y, tolerance) && FPMath.IsZero(z, tolerance)
                   && FPMath.IsEqual(w, 1f, tolerance);
        }

        // todo : the setFromAxis(v3,float) method should replace the set(v3,float) method
        /** Sets the quaternion components from the given axis and angle around that axis.
         *
         * @param axis The axis
         * @param degrees The angle in degrees
         * @return This quaternion for chaining. */
        public FPQuaternion setFromAxis(FPVector3 axis, FP degrees)
        {
            return setFromAxis(axis.x, axis.y, axis.z, degrees);
        }

        /** Sets the quaternion components from the given axis and angle around that axis.
         *
         * @param axis The axis
         * @param radians The angle in radians
         * @return This quaternion for chaining. */
        public FPQuaternion setFromAxisRad(FPVector3 axis, FP radians)
        {
            return setFromAxisRad(axis.x, axis.y, axis.z, radians);
        }

        /** Sets the quaternion components from the given axis and angle around that axis.
         * @param x X direction of the axis
         * @param y Y direction of the axis
         * @param z Z direction of the axis
         * @param degrees The angle in degrees
         * @return This quaternion for chaining. */
        public FPQuaternion setFromAxis(FP x, FP y, FP z, FP degrees)
        {
            return setFromAxisRad(x, y, z, degrees * FPMath.DEG2RAD);
        }

        /** Sets the quaternion components from the given axis and angle around that axis.
         * @param x X direction of the axis
         * @param y Y direction of the axis
         * @param z Z direction of the axis
         * @param radians The angle in radians
         * @return This quaternion for chaining. */
        public FPQuaternion setFromAxisRad(FP x, FP y, FP z, FP radians)
        {
            FP d = FPVector3.len(x, y, z);
            if (d == 0f) return idt();
            d = 1f / d;
            FP l_ang = radians < 0 ? FPMath.TWO_PI - (-radians % FPMath.TWO_PI) : radians % FPMath.TWO_PI;
            FP l_sin = FPMath.Sin(l_ang / 2);
            FP l_cos = FPMath.Cos(l_ang / 2);
            return set(d * x * l_sin, d * y * l_sin, d * z * l_sin, l_cos).nor();
        }

        /** Sets the Quaternion from the given matrix, optionally removing any scaling. */
        public FPQuaternion setFromMatrix(bool normalizeAxes, FPMatrix4x4 matrix)
        {
            FPVector3 scale = default;
            matrix.getScale(ref scale);
            return setFromAxes(normalizeAxes, matrix.m00 / scale.x, matrix.m01 / scale.y,
                matrix.m02 / scale.z,
                matrix.m10 / scale.x, matrix.m11 / scale.y, matrix.m12 / scale.z,
                matrix.m20 / scale.x,
                matrix.m21 / scale.y, matrix.m22 / scale.z);
        }

        /** Sets the Quaternion from the given rotation matrix, which must not contain scaling. */
        public FPQuaternion setFromMatrix(FPMatrix4x4 matrix)
        {
            return setFromMatrix(false, matrix);
        }

        /** Sets the Quaternion from the given matrix, optionally removing any scaling. */
        public FPQuaternion setFromMatrix(bool normalizeAxes, FPMatrix3x3 matrix)
        {
            return setFromAxes(normalizeAxes, matrix.m00, matrix.m01,
                matrix.m02,
                matrix.m10, matrix.m11, matrix.m12,
                matrix.m20,
                matrix.m21, matrix.m22);
        }

        /** Sets the Quaternion from the given rotation matrix, which must not contain scaling. */
        public FPQuaternion setFromMatrix(FPMatrix3x3 matrix)
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
        public FPQuaternion setFromAxes(FP xx, FP xy, FP xz, FP yx, FP yy, FP yz, FP zx, FP zy, FP zz)
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
        public FPQuaternion setFromAxes(bool normalizeAxes, FP xx, FP xy, FP xz, FP yx, FP yy, FP yz, FP zx,
            FP zy, FP zz)
        {
            if (normalizeAxes)
            {
                FP lx = 1f / FPVector3.len(xx, xy, xz);
                FP ly = 1f / FPVector3.len(yx, yy, yz);
                FP lz = 1f / FPVector3.len(zx, zy, zz);
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
            FP t = xx + yy + zz;

            // we protect the division by s by ensuring that s>=1
            if (t >= 0)
            {
                // |w| >= .5
                FP s = FPMath.Sqrt(t + 1); // |s|>=1 ...
                w = 0.5f * s;
                s = 0.5f / s; // so this division isn't bad
                x = (zy - yz) * s;
                y = (xz - zx) * s;
                z = (yx - xy) * s;
            }
            else if ((xx > yy) && (xx > zz))
            {
                FP s = FPMath.Sqrt(1.0 + xx - yy - zz); // |s|>=1
                x = s * 0.5f; // |x| >= .5
                s = 0.5f / s;
                y = (yx + xy) * s;
                z = (xz + zx) * s;
                w = (zy - yz) * s;
            }
            else if (yy > zz)
            {
                FP s = FPMath.Sqrt(1.0 + yy - xx - zz); // |s|>=1
                y = s * 0.5f; // |y| >= .5
                s = 0.5f / s;
                x = (yx + xy) * s;
                z = (zy + yz) * s;
                w = (xz - zx) * s;
            }
            else
            {
                FP s = FPMath.Sqrt(1.0 + zz - xx - yy); // |s|>=1
                z = s * 0.5f; // |z| >= .5
                s = 0.5f / s;
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
        public FPQuaternion setFromCross(FPVector3 v1, FPVector3 v2)
        {
            FP dot = FPMath.Clamp(v1.dot(v2), (-1f), 1f);
            FP angle = FPMath.Acos(dot);
            return setFromAxisRad(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x,
                angle);
        }

        /** Set this quaternion to the rotation between two vectors.
         * @param x1 The base vectors x value, which should be normalized.
         * @param y1 The base vectors y value, which should be normalized.
         * @param z1 The base vectors z value, which should be normalized.
         * @param x2 The target vector x value, which should be normalized.
         * @param y2 The target vector y value, which should be normalized.
         * @param z2 The target vector z value, which should be normalized.
         * @return This quaternion for chaining */
        public FPQuaternion setFromCross(FP x1, FP y1, FP z1, FP x2, FP y2,
            FP z2)
        {
            FP dot = FPMath.Clamp(FPVector3.dot(x1, y1, z1, x2, y2, z2), (-1f), 1f);
            FP angle = FPMath.Acos(dot);
            return setFromAxisRad(y1 * z2 - z1 * y2, z1 * x2 - x1 * z2, x1 * y2 - y1 * x2, angle);
        }

        /** Spherical linear interpolation between this quaternion and the other quaternion, based on the alpha value in the range
         * [0,1]. Taken from Bones framework for JPCT, see http://www.aptalkarga.com/bones/
         * @param end the end quaternion
         * @param alpha alpha in the range [0,1]
         * @return this quaternion for chaining */
        public FPQuaternion slerp(FPQuaternion end, FP alpha)
        {
            FP d = x * end.x + y * end.y + z * end.z + w * end.w;
            FP absDot = d < 0f ? -d : d;

            // Set the first and second scale for the interpolation
            FP scale0 = 1f - alpha;
            FP scale1 = alpha;

            // Check if the angle between the 2 quaternions was big enough to
            // warrant such calculations
            if ((1 - absDot) > 0.1)
            {
                // Get the angle between the 2 quaternions,
                // and then store the sin() of that angle
                FP angle = FPMath.Acos(absDot);
                FP invSinTheta = 1f / FPMath.Sin(angle);

                // Calculate the scale for q1 and q2, according to the angle and
                // it's sine value
                scale0 = (FPMath.Sin((1f - alpha) * angle) * invSinTheta);
                scale1 = (FPMath.Sin((alpha * angle)) * invSinTheta);
            }

            if (d < 0f) scale1 = -scale1;

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
        public FPQuaternion slerp(FPQuaternion[] q)
        {
            // Calculate exponents and multiply everything from left to right
            FP w = 1.0f / q.Length;
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
        public FPQuaternion slerp(FPQuaternion[] q, FP[] w)
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
        public FPQuaternion exp(FP alpha)
        {
            // Calculate |q|^alpha
            FP norm = len();
            FP normExp = FPMath.Pow(norm, alpha);

            // Calculate theta
            FP theta = FPMath.Acos(w / norm);

            // Calculate coefficient of basis elements
            FP coeff = 0;
            if (FPMath.Abs(theta) < 0.001
               ) // If theta is small enough, use the limit of sin(alpha*theta) / sin(theta) instead of actual
                // value
                coeff = normExp * alpha / norm;
            else
                coeff = normExp * FPMath.Sin(alpha * theta) / (norm * FPMath.Sin(theta));

            // Write results
            w = normExp * FPMath.Cos(alpha * theta);
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
        public static FP dot(FP x1, FP y1, FP z1, FP w1, FP x2, FP y2,
            FP z2, FP w2)
        {
            return x1 * x2 + y1 * y2 + z1 * z2 + w1 * w2;
        }

        /** Get the dot product between this and the other quaternion (commutative).
         * @param other the other quaternion.
         * @return the dot product of this and the other quaternion. */
        public FP dot(FPQuaternion other)
        {
            return x * other.x + y * other.y + z * other.z + w * other.w;
        }

        /** Get the dot product between this and the other quaternion (commutative).
         * @param x the x component of the other quaternion
         * @param y the y component of the other quaternion
         * @param z the z component of the other quaternion
         * @param w the w component of the other quaternion
         * @return the dot product of this and the other quaternion. */
        public FP dot(FP x, FP y, FP z, FP w)
        {
            return this.x * x + this.y * y + this.z * z + this.w * w;
        }

        /** Multiplies the components of this quaternion with the given scalar.
         * @param scalar the scalar.
         * @return this quaternion for chaining. */
        public FPQuaternion mul(FP scalar)
        {
            x *= scalar;
            y *= scalar;
            z *= scalar;
            w *= scalar;
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
        public FP getAxisAngle(out FPVector3 axis)
        {
            return getAxisAngleRad(out axis) * FPMath.Rad2Deg;
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
        public FP getAxisAngleRad(out FPVector3 axis)
        {
            if (w > 1)
                nor(); // if w>1 acos and sqrt will produce errors, this cant happen if quaternion is normalised
            FP angle = 2.0 * FPMath.Acos(w);
            FP s = FPMath.Sqrt(1 - w *
                w); // assuming quaternion normalised then w is less than 1, so term always positive.
            if (s < FPMath.EPSILION)
            {
                // test to avoid divide by zero, s is always positive due to sqrt
                // if s close to zero then direction of axis not important
                axis.x = x; // if it is important that axis is normalised then replace with x=1; y=z=0;
                axis.y = y;
                axis.z = z;
            }
            else
            {
                axis.x = x / s; // normalise axis
                axis.y = y / s;
                axis.z = z / s;
            }

            return angle;
        }

        /** Get the angle in radians of the rotation this quaternion represents. Does not normalize the quaternion. Use
         * {@link #getAxisAngleRad(Vector3)} to get both the axis and the angle of this rotation. Use
         * {@link #getAngleAroundRad(Vector3)} to get the angle around a specific axis.
         * @return the angle in radians of the rotation */
        public FP getAngleRad()
        {
            return 2.0 * FPMath.Acos((w > 1) ? (w / len()) : w);
        }

        /** Get the angle in degrees of the rotation this quaternion represents. Use {@link #getAxisAngle(Vector3)} to get both the
         * axis and the angle of this rotation. Use {@link #getAngleAround(Vector3)} to get the angle around a specific axis.
         * @return the angle in degrees of the rotation */
        public FP getAngle()
        {
            return getAngleRad() * FPMath.Rad2Deg;
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
        public void getSwingTwist(FP axisX, FP axisY, FP axisZ, ref FPQuaternion swing,
            ref FPQuaternion twist)
        {
            FP d = FPVector3.dot(x, y, z, axisX, axisY, axisZ);
            twist = twist.set(axisX * d, axisY * d, axisZ * d, w).nor();
            if (d < 0) twist = twist.mul((-1f));
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
        public void getSwingTwist(FPVector3 axis, ref FPQuaternion swing, ref FPQuaternion twist)
        {
            getSwingTwist(axis.x, axis.y, axis.z, ref swing, ref twist);
        }

        /** Get the angle in radians of the rotation around the specified axis. The axis must be normalized.
         * @param axisX the x component of the normalized axis for which to get the angle
         * @param axisY the y component of the normalized axis for which to get the angle
         * @param axisZ the z component of the normalized axis for which to get the angle
         * @return the angle in radians of the rotation around the specified axis */
        public FP getAngleAroundRad(FP axisX, FP axisY, FP axisZ)
        {
            FP d = FPVector3.dot(x, y, z, axisX, axisY, axisZ);
            FP l2 = len2(axisX * d, axisY * d, axisZ * d, w);
            return FPMath.IsZero(l2)
                ? 0f
                : 2.0 * FPMath.Acos(FPMath.Clamp((d < 0 ? -w : w) / FPMath.Sqrt(l2), -1f,
                    1f));
        }

        /** Get the angle in radians of the rotation around the specified axis. The axis must be normalized.
         * @param axis the normalized axis for which to get the angle
         * @return the angle in radians of the rotation around the specified axis */
        public FP getAngleAroundRad(FPVector3 axis)
        {
            return getAngleAroundRad(axis.x, axis.y, axis.z);
        }

        /** Get the angle in degrees of the rotation around the specified axis. The axis must be normalized.
         * @param axisX the x component of the normalized axis for which to get the angle
         * @param axisY the y component of the normalized axis for which to get the angle
         * @param axisZ the z component of the normalized axis for which to get the angle
         * @return the angle in degrees of the rotation around the specified axis */
        public FP getAngleAround(FP axisX, FP axisY, FP axisZ)
        {
            return getAngleAroundRad(axisX, axisY, axisZ) * FPMath.Rad2Deg;
        }

        /** Get the angle in degrees of the rotation around the specified axis. The axis must be normalized.
         * @param axis the normalized axis for which to get the angle
         * @return the angle in degrees of the rotation around the specified axis */
        public FP getAngleAround(FPVector3 axis)
        {
            return getAngleAround(axis.x, axis.y, axis.z);
        }
    }
}