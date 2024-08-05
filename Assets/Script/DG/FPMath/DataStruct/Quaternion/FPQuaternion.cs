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
    //https://github.com/sungiant/abacus/blob/master/source/abacus/gen/main/Quaternion.t4
    public partial struct FPQuaternion
    {
        public static readonly FP kEpsilon = 0.000001F;
        public static FPQuaternion identity = new(0, 0, 0, 1);


        public FP this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    case 3:
                        return w;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    case 3:
                        w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
                }
            }
        }

        public FPVector3 xyz
        {
            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
            }
            get => new(x, y, z);
        }

        public FPVector3 eulerAngles
        {
            get => Internal_ToEulerRad(this);
            set => this = Internal_FromEulerRad(value * FPMath.Rad2Deg);
        }

        public FPQuaternion normalized => Normalize(this);
        public FP sqrMagnitude => SqrMagnitude();
        public FP magnitude => Magnitude();


        public FPQuaternion(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }


#if UNITY_STANDALONE
        public FPQuaternion(Quaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }
#endif

        public FPQuaternion(System.Numerics.Quaternion quaternion)
        {
            x = quaternion.X;
            y = quaternion.Y;
            z = quaternion.Z;
            w = quaternion.W;
        }

        /*************************************************************************************
         * 模块描述:Equals ToString
         *************************************************************************************/
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var other = (FPQuaternion)obj;
            return Equals(other);
        }

        public bool Equals(FPQuaternion other)
        {
            return other.x == x && other.y == y && other.z == z && other.w == w;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2 ^ w.GetHashCode() >> 1;
        }
        /*************************************************************************************
         * 模块描述:转换
         *************************************************************************************/
#if UNITY_STANDALONE
        //转换为Unity的Quaternion
        public Quaternion ToQuaternion()
        {
            return new Quaternion(x, y, z, w);
        }
#endif
        /*************************************************************************************
         * 模块描述:关系运算符运算
         *************************************************************************************/
        public static bool operator ==(FPQuaternion value1, FPQuaternion value2)
        {
            return _IsEqualUsingDot(Dot(value1, value2));
        }

        public static bool operator !=(FPQuaternion value1, FPQuaternion value2)
        {
            return !(value1 == value2);
        }

        /*************************************************************************************
         * 模块描述:操作运算
         *************************************************************************************/
        public static FPQuaternion operator +(FPQuaternion value1, FPQuaternion value2)
        {
            FP x = value1.x + value2.x;
            FP y = value1.y + value2.y;
            FP z = value1.z + value2.z;
            FP w = value1.w + value2.w;
            return new FPQuaternion(x, y, z, w);
        }

        public static FPQuaternion operator -(FPQuaternion value1, FPQuaternion value2)
        {
            FP x = value1.x - value2.x;
            FP y = value1.y - value2.y;
            FP z = value1.z - value2.z;
            FP w = value1.w - value2.w;
            return new FPQuaternion(x, y, z, w);
        }

        public static FPQuaternion operator -(FPQuaternion value)
        {
            FP x = -value.x;
            FP y = -value.y;
            FP z = -value.z;
            FP w = -value.w;
            return new FPQuaternion(x, y, z, w);
        }

        public static FPQuaternion operator *(FPQuaternion lhs, FPQuaternion rhs)
        {
            return new FPQuaternion(
                lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
                lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
        }

        // Rotates the point /point/ with /rotation/.
        public static FPVector3 operator *(FPQuaternion rotation, FPVector3 point)
        {
            FP x = rotation.x * 2F;
            FP y = rotation.y * 2F;
            FP z = rotation.z * 2F;
            FP xx = rotation.x * x;
            FP yy = rotation.y * y;
            FP zz = rotation.z * z;
            FP xy = rotation.x * y;
            FP xz = rotation.x * z;
            FP yz = rotation.y * z;
            FP wx = rotation.w * x;
            FP wy = rotation.w * y;
            FP wz = rotation.w * z;

            FPVector3 res;
            res.x = (1 - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z;
            res.y = (xy + wz) * point.x + (1 - (xx + zz)) * point.y + (yz - wx) * point.z;
            res.z = (xz - wy) * point.x + (yz + wx) * point.y + (1 - (xx + yy)) * point.z;
            return res;
        }

        /*************************************************************************************
         * 模块描述:StaticUtil
         *************************************************************************************/
        private static bool _IsEqualUsingDot(FP dot)
        {
            // Returns false in the presence of NaN values.
            return dot > 1 - kEpsilon;
        }

        public static bool IsUnit(FPQuaternion q)
        {
            return FPMath.IsApproximatelyZero(1 - q.w * q.w - q.x * q.x - q.y * q.y - q.z * q.z);
        }

        public static FP Dot(FPQuaternion value1, FPQuaternion value2)
        {
            return value1.x * value2.x + value1.y * value2.y + value1.z * value2.z + value1.w * value2.w;
        }

        public static FPQuaternion Cross(FPQuaternion value1, FPQuaternion value2)
        {
            var a = (value1.z * value2.y) - (value1.y * value2.z);
            var b = (value1.x * value2.z) - (value1.z * value2.x);
            var c = (value1.y * value2.x) - (value1.x * value2.y);
            var d = (value1.x * value2.x) - (value1.y * value2.y);
            var x = (value1.w * value2.x) + (value1.x * value2.w) + a;
            var y = (value1.w * value2.y) + (value1.y * value2.w) + b;
            var z = (value1.w * value2.z) + (value1.z * value2.w) + c;
            var w = (value1.w * value2.w) - (value1.z * value2.z) - d;
            return new FPQuaternion(x, y, z, w);
        }

        public static FPVector3 Transform(FPQuaternion value, FPVector3 vector)
        {
            var i = value.x;
            var j = value.y;
            var k = value.z;
            var u = value.w;
            var ii = i * i;
            var jj = j * j;
            var kk = k * k;
            var ui = u * i;
            var uj = u * j;
            var uk = u * k;
            var ij = i * j;
            var ik = i * k;
            var jk = j * k;
            var x = vector.x - (2 * vector.x * (jj + kk)) + (2 * vector.y * (ij - uk)) +
                    (2 * vector.z * (ik + uj));
            var y = vector.y + (2 * vector.x * (ij + uk)) - (2 * vector.y * (ii + kk)) +
                    (2 * vector.z * (jk - ui));
            var z = vector.z + (2 * vector.x * (ik - uj)) + (2 * vector.y * (jk + ui)) -
                    (2 * vector.z * (ii + jj));
            return new FPVector3(x, y, z);
        }

        /// <summary>
        /// Transforms a vector using a quaternion. Specialized for x,0,0 vectors.
        /// </summary>
        /// <param name="x">X component of the vector to transform.</param>
        /// <param name="rotation">Rotation to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static FPVector3 TransformX(FP x, FPQuaternion rotation)
        {
            //This operation is an optimized-down version of v' = q * v * q^-1.
            //The expanded form would be to treat v as an 'axis only' quaternion
            //and perform standard quaternion multiplication.  Assuming q is normalized,
            //q^-1 can be replaced by a conjugation.
            FP y2 = rotation.y + rotation.y;
            FP z2 = rotation.z + rotation.z;
            FP xy2 = rotation.x * y2;
            FP xz2 = rotation.x * z2;
            FP yy2 = rotation.y * y2;
            FP zz2 = rotation.z * z2;
            FP wy2 = rotation.w * y2;
            FP wz2 = rotation.w * z2;
            //Defer the component setting since they're used in computation.
            FP transformedX = x * (FP.ONE - yy2 - zz2);
            FP transformedY = x * (xy2 + wz2);
            FP transformedZ = x * (xz2 - wy2);
            return new FPVector3(transformedX, transformedY, transformedZ);
        }

        /// <summary>
        /// Transforms a vector using a quaternion. Specialized for 0,y,0 vectors.
        /// </summary>
        /// <param name="y">Y component of the vector to transform.</param>
        /// <param name="rotation">Rotation to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static FPVector3 TransformY(FP y, FPQuaternion rotation)
        {
            //This operation is an optimized-down version of v' = q * v * q^-1.
            //The expanded form would be to treat v as an 'axis only' quaternion
            //and perform standard quaternion multiplication.  Assuming q is normalized,
            //q^-1 can be replaced by a conjugation.
            FP x2 = rotation.x + rotation.x;
            FP y2 = rotation.y + rotation.y;
            FP z2 = rotation.z + rotation.z;
            FP xx2 = rotation.x * x2;
            FP xy2 = rotation.x * y2;
            FP yz2 = rotation.y * z2;
            FP zz2 = rotation.z * z2;
            FP wx2 = rotation.w * x2;
            FP wz2 = rotation.w * z2;
            //Defer the component setting since they're used in computation.
            FP transformedX = y * (xy2 - wz2);
            FP transformedY = y * (FP.ONE - xx2 - zz2);
            FP transformedZ = y * (yz2 + wx2);
            return new FPVector3(transformedX, transformedY, transformedZ);
        }

        /// <summary>
        /// Transforms a vector using a quaternion. Specialized for 0,0,z vectors.
        /// </summary>
        /// <param name="z">Z component of the vector to transform.</param>
        /// <param name="rotation">Rotation to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static FPVector3 TransformZ(FP z, FPQuaternion rotation)
        {
            //This operation is an optimized-down version of v' = q * v * q^-1.
            //The expanded form would be to treat v as an 'axis only' quaternion
            //and perform standard quaternion multiplication.  Assuming q is normalized,
            //q^-1 can be replaced by a conjugation.
            FP x2 = rotation.x + rotation.x;
            FP y2 = rotation.y + rotation.y;
            FP z2 = rotation.z + rotation.z;
            FP xx2 = rotation.x * x2;
            FP xz2 = rotation.x * z2;
            FP yy2 = rotation.y * y2;
            FP yz2 = rotation.y * z2;
            FP wx2 = rotation.w * x2;
            FP wy2 = rotation.w * y2;
            //Defer the component setting since they're used in computation.
            FP transformedX = z * (xz2 + wy2);
            FP transformedY = z * (yz2 - wx2);
            FP transformedZ = z * (FP.ONE - xx2 - yy2);
            return new FPVector3(transformedX, transformedY, transformedZ);
        }

        // Angle of rotation, in radians. Angles are measured anti-clockwise when viewed from the rotation axis (positive side) toward the origin.
        public static FPVector3 ToYawPitchRoll(FPQuaternion value, FPVector3 vector)
        {
            var sinrCosp = 2 * (value.w * value.z + value.x * value.y);
            var cosrCosp = 1 - 2 * (value.z * value.z + value.x * value.y);
            var z = FPMath.Atan2(sinrCosp, cosrCosp);
            // pitch (y-axis rotation)
            var sinp = 2 * (value.w * value.x - value.y * value.z);
            FPVector3 result = FPVector3.zero;
            if (FPMath.Abs(sinp) >= FP.ONE)
                result.y = FPMath.CopySign(FPMath.HALF_PI, sinp);
            else
                result.y = FPMath.Asin(sinp);
            // yaw (z-axis rotation)
            var sinYcosp = 2 * (value.w * value.y + value.z + value.x);
            var cosYcosp = 1 - 2 * (value.x * value.x + value.y * value.y);
            result.x = FPMath.Atan2(sinYcosp, cosYcosp);
            return result;
        }

        public static FPQuaternion Normalize(FPQuaternion q)
        {
            FP num = FPMath.Sqrt(Dot(q, q));
            if (num < FPMath.EPSILION)
                return identity;
            return new FPQuaternion(q.x / num, q.y / num, q.z / num, q.w / num);
        }

        public static FP Angle(FPQuaternion a, FPQuaternion b)
        {
            FP dot = FPMath.Min(FPMath.Abs(Dot(a, b)), 1);
            return _IsEqualUsingDot(dot) ? FP.ZERO : FPMath.Acos(dot) * 2 * FPMath.Rad2Deg;
        }

        // Makes euler angles positive 0/360 with 0.0001 hacked to support old behaviour of QuaternionToEuler
        private static FPVector3 Internal_MakePositive(FPVector3 euler)
        {
            FP negativeFlip = (-0.0001f) * FPMath.Rad2Deg;
            FP positiveFlip = 360 + negativeFlip;

            if (euler.x < negativeFlip)
                euler.x += 360;
            else if (euler.x > positiveFlip)
                euler.x -= 360;

            if (euler.y < negativeFlip)
                euler.y += 360;
            else if (euler.y > positiveFlip)
                euler.y -= 360;

            if (euler.z < negativeFlip)
                euler.z += 360;
            else if (euler.z > positiveFlip)
                euler.z -= 360;

            return euler;
        }

        public static FPQuaternion RotateTowards(FPQuaternion from, FPQuaternion to,
            FP maxDegreesDelta)
        {
            FP angle = Angle(from, to);
            if (angle == FP.ZERO)
                return to;
            return SlerpUnclamped(from, to, FPMath.Min(1, maxDegreesDelta / angle));
        }

        public static FPQuaternion CreateFromAxisAngle(FPVector3 axis, FP angle)
        {
            return CreateFromAxisAngleRad(axis, angle * FPMath.DEG2RAD);
        }

        public static FPQuaternion CreateFromAxisAngleRad(FPVector3 axis, FP radians)
        {
            var theta = radians * FPMath.HALF;
            var sin = FPMath.Sin(theta);
            var cos = FPMath.Cos(theta);
            var x = axis.x * sin;
            var y = axis.y * sin;
            var z = axis.z * sin;
            var w = cos;
            return new FPQuaternion(x, y, z, w);
        }

        /// <summary>
        ///   <para>Creates a rotation which rotates /angle/ degrees around /axis/.</para>
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        public static FPQuaternion AngleAxis(FP angle, FPVector3 axis)
        {
            return INTERNAL_CALL_AngleAxis(angle, ref axis);
        }

        private static FPQuaternion INTERNAL_CALL_AngleAxis(FP degress, ref FPVector3 axis)
        {
            if (axis.sqrMagnitude == 0.0f)
                return identity;

            FPQuaternion result = identity;
            var radians = degress * FPMath.DEG2RAD;
            radians *= 0.5f;
            axis.Normalize();
            axis *= FPMath.Sin(radians);
            result.x = axis.x;
            result.y = axis.y;
            result.z = axis.z;
            result.w = FPMath.Cos(radians);

            return Normalize(result);
        }


        /// <summary>
        ///   <para>Creates a rotation which rotates from /fromDirection/ to /toDirection/.</para>
        /// </summary>
        /// <param name="fromDirection"></param>
        /// <param name="toDirection"></param>
        public static FPQuaternion FromToRotation(FPVector3 fromDirection, FPVector3 toDirection)
        {
            FPVector3 axis = FPVector3.Cross(fromDirection, toDirection);
            FP angle = FPVector3.Angle(fromDirection, toDirection);
            return AngleAxis(angle, axis.normalized);
        }


        /// <summary>
        ///   <para>Creates a rotation with the specified /forward/ and /upwards/ directions.</para>
        /// </summary>
        /// <param name="forward">The direction to look in.</param>
        /// <param name="upwards">The vector that defines in which direction up is.</param>
        public static FPQuaternion LookRotation(FPVector3 forward, FPVector3 upwards)
        {
            return INTERNAL_CALL_LookRotation(ref forward, ref upwards);
        }

        public static FPQuaternion LookRotation(FPVector3 forward)
        {
            FPVector3 up = FPVector3.up;
            return INTERNAL_CALL_LookRotation(ref forward, ref up);
        }

        // from http://answers.unity3d.com/questions/467614/what-is-the-source-code-of-quaternionlookrotation.html
        private static FPQuaternion INTERNAL_CALL_LookRotation(ref FPVector3 forward, ref FPVector3 up)
        {
            forward = FPVector3.Normalize(forward);
            FPVector3 right = FPVector3.Normalize(FPVector3.Cross(up, forward));
            up = FPVector3.Cross(forward, right);
            var m00 = right.x;
            var m01 = right.y;
            var m02 = right.z;
            var m10 = up.x;
            var m11 = up.y;
            var m12 = up.z;
            var m20 = forward.x;
            var m21 = forward.y;
            var m22 = forward.z;


            FP num8 = (m00 + m11) + m22;
            var quaternion = default2;
            if (num8 > 0f)
            {
                var num = FPMath.Sqrt(num8 + 1f);
                quaternion.w = num * 0.5f;
                num = 0.5f / num;
                quaternion.x = (m12 - m21) * num;
                quaternion.y = (m20 - m02) * num;
                quaternion.z = (m01 - m10) * num;
                return quaternion;
            }

            if ((m00 >= m11) && (m00 >= m22))
            {
                var num7 = FPMath.Sqrt(((1f + m00) - m11) - m22);
                var num4 = 0.5f / num7;
                quaternion.x = 0.5f * num7;
                quaternion.y = (m01 + m10) * num4;
                quaternion.z = (m02 + m20) * num4;
                quaternion.w = (m12 - m21) * num4;
                return quaternion;
            }

            if (m11 > m22)
            {
                var num6 = FPMath.Sqrt(((1f + m11) - m00) - m22);
                var num3 = 0.5f / num6;
                quaternion.x = (m10 + m01) * num3;
                quaternion.y = 0.5f * num6;
                quaternion.z = (m21 + m12) * num3;
                quaternion.w = (m20 - m02) * num3;
                return quaternion;
            }

            var num5 = FPMath.Sqrt(((1f + m22) - m00) - m11);
            var num2 = 0.5f / num5;
            quaternion.x = (m20 + m02) * num2;
            quaternion.y = (m21 + m12) * num2;
            quaternion.z = 0.5f * num5;
            quaternion.w = (m01 - m10) * num2;
            return quaternion;
        }


        public static FPQuaternion CreateFromYawPitchRoll(FP yaw, FP pitch, FP roll)
        {
            var hr = roll * FPMath.HALF;
            var hp = pitch * FPMath.HALF;
            var hy = yaw * FPMath.HALF;
            var shr = FPMath.Sin(hr);
            var chr = FPMath.Cos(hr);
            var shp = FPMath.Sin(hp);
            var chp = FPMath.Cos(hp);
            var shy = FPMath.Sin(hy);
            var chy = FPMath.Cos(hy);
            var x = (chy * shp * chr) + (shy * chp * shr);
            var y = (shy * chp * chr) - (chy * shp * shr);
            var z = (chy * chp * shr) - (shy * shp * chr);
            var w = (chy * chp * chr) + (shy * shp * shr);
            return new FPQuaternion(x, y, z, w);
        }


        /// <summary>
        /// Computes the axis radian representation of a normalized quaternion.
        /// </summary>
        /// <param name="q">Quaternion to be converted.</param>
        /// <param name="axis">Axis represented by the quaternion.GetAxisAngleFromQu</param>
        /// <param name="radian">Angle around the axis represented by the quaternion.</param>
        public static FP GetAxisAngleRadFromQuaternion(FPQuaternion q, out FPVector3 axis)
        {
            FP qw = q.w;
            if (qw > FP.ZERO)
            {
                axis.x = q.x;
                axis.y = q.y;
                axis.z = q.z;
            }
            else
            {
                axis.x = -q.x;
                axis.y = -q.y;
                axis.z = -q.z;
                qw = -qw;
            }

            FP lengthSquared = axis.sqrMagnitude;
            FP radian;
            if (lengthSquared > 1e-14m)
            {
                axis /= FPMath.Sqrt(lengthSquared);
                radian = 2 * FPMath.Acos(FPMath.Clamp(qw, (-1), FP.ONE));
            }
            else
            {
                axis = FPVector3.up;
                radian = FP.ZERO;
            }

            return radian;
        }

        public static FP GetAxisAngleFromQuaternion(FPQuaternion q, out FPVector3 axis)
        {
            var radian = GetAxisAngleRadFromQuaternion(q, out axis);
            return radian * FPMath.Rad2Deg;
        }

        /// <summary>
        /// Computes the quaternion rotation between two normalized vectors.
        /// </summary>
        /// <param name="v1">First unit-length vector.</param>
        /// <param name="v2">Second unit-length vector.</param>
        /// <param name="q">Quaternion representing the rotation from v1 to v2.</param>
        public static FPQuaternion GetQuaternionBetweenNormalizedVectors(FPVector3 v1, FPVector3 v2)
        {
            FP dot = FPVector3.Dot(v1, v2);
            FPQuaternion q;
            //For non-normal vectors, the multiplying the axes length squared would be necessary:
            //Fix64 w = dot + (Fix64)Math.Sqrt(v1.LengthSquared() * v2.LengthSquared());
            if (dot < (-0.9999m)) //parallel, opposing direction
            {
                //If this occurs, the rotation required is ~180 degrees.
                //The problem is that we could choose any perpendicular axis for the rotation. It's not uniquely defined.
                //The solution is to pick an arbitrary perpendicular axis.
                //Project onto the plane which has the lowest component magnitude.
                //On that 2d plane, perform a 90 degree rotation.
                FP absX = FP.Abs(v1.x);
                FP absY = FP.Abs(v1.y);
                FP absZ = FP.Abs(v1.z);
                if (absX < absY && absX < absZ)
                    q = new FPQuaternion(FP.ZERO, -v1.z, v1.y, FP.ZERO);
                else if (absY < absZ)
                    q = new FPQuaternion(-v1.z, FP.ZERO, v1.x, FP.ZERO);
                else
                    q = new FPQuaternion(-v1.y, v1.x, FP.ZERO, FP.ZERO);
            }
            else
            {
                FPVector3 axis = FPVector3.Cross(v1, v2);
                q = new FPQuaternion(axis.x, axis.y, axis.z, dot + FP.ONE);
            }

            q.Normalize();
            return q;
        }

        //The following two functions are highly similar, but it's a bit of a brain teaser to phrase one in terms of the other.
        //Providing both simplifies things.

        /// <summary>
        /// Computes the rotation from the start orientation to the end orientation such that end = Quaternion.Concatenate(start, relative).
        /// </summary>
        /// <param name="start">Starting orientation.</param>
        /// <param name="end">Ending orientation.</param>
        /// <param name="relative">Relative rotation from the start to the end orientation.</param>
        public static FPQuaternion GetRelativeRotation(FPQuaternion start, FPQuaternion end)
        {
            FPQuaternion startInverse = Conjugate(start);
            FPQuaternion relative = Concatenate(startInverse, end);
            return relative;
        }


        /// <summary>
        /// Transforms the rotation into the local space of the target basis such that rotation = Quaternion.Concatenate(localRotation, targetBasis)
        /// </summary>
        /// <param name="rotation">Rotation in the original frame of reference.</param>
        /// <param name="targetBasis">Basis in the original frame of reference to transform the rotation into.</param>
        /// <param name="localRotation">Rotation in the local space of the target basis.</param>
        public static FPQuaternion GetLocalRotation(FPQuaternion rotation, FPQuaternion targetBasis)
        {
            FPQuaternion basisInverse = Conjugate(targetBasis);
            FPQuaternion localRotation = Concatenate(rotation, basisInverse);
            return localRotation;
        }


        public static FPQuaternion Slerp(FPQuaternion start, FPQuaternion end, FP pct)
        {
            pct = FPMath.Clamp01(pct);
            return SlerpUnclamped(start, end, pct);
        }

        public static FPQuaternion SlerpUnclamped(FPQuaternion start, FPQuaternion end, FP pct)
        {
            var dot = start.x * end.x + start.y * end.y + start.z * end.z + start.w * end.w;


            if (dot < 0)
            {
                dot = -dot;
                end = new FPQuaternion(-end.x, -end.y, -end.z, -end.w);
            }


            if (dot < 0.95)
            {
                var angle = FPMath.Acos(dot);

                var invSinAngle = 1 / FPMath.Sin(angle);

                var t1 = FPMath.Sin((1 - pct) * angle) * invSinAngle;

                var t2 = FPMath.Sin(pct * angle) * invSinAngle;

                return new FPQuaternion(start.x * t1 + end.x * t2, start.y * t1 + end.y * t2, start.z * t1 + end.z * t2,
                    start.w * t1 + end.w * t2);
                ;
            }

            var x = start.x + pct * (end.x - start.x);
            var y = start.y + pct * (end.y - start.y);
            var z = start.z + pct * (end.z - start.z);
            var w = start.w + pct * (end.w - start.w);
            return new FPQuaternion(x, y, z, w).normalized;
        }

        public static FP SqrMagnitude(FPQuaternion value)
        {
            return value.x * value.x + value.y * value.y + value.z * value.z + value.w + value.w;
        }

        public static FP Magnitude(FPQuaternion value)
        {
            return FPMath.Sqrt(SqrMagnitude(value));
        }

        //共轭，转置
        public static FPQuaternion Conjugate(FPQuaternion value)
        {
            var x = -value.x;
            var y = -value.y;
            var z = -value.z;
            var w = value.w;
            return new FPQuaternion(x, y, z, w);
        }

        public static FPQuaternion Inverse(FPQuaternion value)
        {
            var a = (value.x * value.x) + (value.y * value.y) + (value.z * value.z) + (value.w * value.w);
            var b = 1 / a;
            var x = -value.x * b;
            var y = -value.y * b;
            var z = -value.z * b;
            var w = value.w * b;
            return new FPQuaternion(x, y, z, w);
        }

        public static FPQuaternion Concatenate(FPQuaternion q1, FPQuaternion q2)
        {
            FPQuaternion ans = default;

            // Concatenate rotation is actually q2 * q1 instead of q1 * q2.
            // So that's why value2 goes q1 and value1 goes q2.
            var q1x = q2.x;
            var q1y = q2.y;
            var q1z = q2.z;
            var q1w = q2.w;

            var q2x = q1.x;
            var q2y = q1.y;
            var q2z = q1.z;
            var q2w = q1.w;

            // cross(av, bv)
            var cx = q1y * q2z - q1z * q2y;
            var cy = q1z * q2x - q1x * q2z;
            var cz = q1x * q2y - q1y * q2x;

            var dot = q1x * q2x + q1y * q2y + q1z * q2z;

            ans.x = q1x * q2w + q2x * q1w + cx;
            ans.y = q1y * q2w + q2y * q1w + cy;
            ans.z = q1z * q2w + q2z * q1w + cz;
            ans.w = q1w * q2w - dot;

            return ans;
        }

        public static FPQuaternion Euler(FP x, FP y, FP z)
        {
            return Euler(new FPVector3(x, y, z));
        }

        public static FPQuaternion Euler(FPVector3 v)
        {
            return Internal_FromEulerRad(v * FPMath.DEG2RAD);
        }

        // from http://stackoverflow.com/questions/12088610/conversion-between-euler-quaternion-like-in-unity3d-engine
        private static FPVector3 Internal_ToEulerRad(FPQuaternion rotation)
        {
            FP sqw = rotation.w * rotation.w;
            FP sqx = rotation.x * rotation.x;
            FP sqy = rotation.y * rotation.y;
            FP sqz = rotation.z * rotation.z;
            FP unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            FP test = rotation.x * rotation.w - rotation.y * rotation.z;
            FPVector3 v;

            if (test > 0.4995f * unit)
            {
                // singularity at north pole
                v.y = 2f * FPMath.Atan2(rotation.y, rotation.x);
                v.x = FPMath.HALF_PI;
                v.z = 0;
                return NormalizeAngles(v * FPMath.Rad2Deg);
            }

            if (test < (-0.4995f) * unit)
            {
                // singularity at south pole
                v.y = (-2f) * FPMath.Atan2(rotation.y, rotation.x);
                v.x = -FPMath.HALF_PI;
                v.z = 0;
                return NormalizeAngles(v * FPMath.Rad2Deg);
            }

            FPQuaternion q = new FPQuaternion(rotation.w, rotation.z, rotation.x, rotation.y);
            v.y = FPMath.Atan2(2f * q.x * q.w + 2f * q.y * q.z,
                1 - 2f * (q.z * q.z + q.w * q.w)); // Yaw
            v.x = FPMath.Asin(2f * (q.x * q.z - q.w * q.y)); // Pitch
            v.z = FPMath.Atan2(2f * q.x * q.y + 2f * q.z * q.w,
                1 - 2f * (q.y * q.y + q.z * q.z)); // Roll
            return NormalizeAngles(v * FPMath.Rad2Deg);
        }

        private static FPVector3 NormalizeAngles(FPVector3 angles)
        {
            angles.x = NormalizeAngle(angles.x);
            angles.y = NormalizeAngle(angles.y);
            angles.z = NormalizeAngle(angles.z);
            return angles;
        }

        private static FP NormalizeAngle(FP angle)
        {
            FP modAngle = angle % 360.0f;

            if (modAngle < 0.0f)
                return modAngle + 360.0f;
            return modAngle;
        }


        // from http://stackoverflow.com/questions/11492299/quaternion-to-euler-angles-algorithm-how-to-convert-to-y-up-and-between-ha
        private static FPQuaternion Internal_FromEulerRad(FPVector3 euler)
        {
            var yaw = euler.y;
            var pitch = euler.x;
            var roll = euler.z;
            FP rollOver2 = roll * 0.5f;
            FP sinRollOver2 = FPMath.Sin(rollOver2);
            FP cosRollOver2 = FPMath.Cos(rollOver2);
            FP pitchOver2 = pitch * 0.5f;
            FP sinPitchOver2 = FPMath.Sin(pitchOver2);
            FP cosPitchOver2 = FPMath.Cos(pitchOver2);
            FP yawOver2 = yaw * 0.5f;
            FP sinYawOver2 = FPMath.Sin(yawOver2);
            FP cosYawOver2 = FPMath.Cos(yawOver2);
            FPQuaternion result;
            result.x = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.y = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            result.z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            result.w = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            return result;
        }

        private static void Internal_ToAxisAngleRad(FPQuaternion q, out FPVector3 axis, out FP angle)
        {
            if (FPMath.Abs(q.w) > 1.0f)
                q.Normalize();


            angle = 2.0f * FPMath.Acos(q.w); // angle
            FP den = FPMath.Sqrt(1.0 - q.w * q.w);
            if (den > 0.0001f)
                axis = q.xyz / den;
            else
                // This occurs when the angle is zero. 
                // Not a problem: just set an arbitrary normalized axis.
                axis = new FPVector3(1, 0, 0);
        }


        /*************************************************************************************
         * 模块描述:Util
         *************************************************************************************/
        public void Normalize()
        {
            this = Normalize(this);
        }

        public FP SqrMagnitude()
        {
            return SqrMagnitude(this);
        }

        public FP Magnitude()
        {
            return Magnitude(this);
        }

        public void ToAngleAxis(out FP angle, out FPVector3 axis)
        {
            Internal_ToAxisAngleRad(this, out axis, out angle);
            angle *= FPMath.Rad2Deg;
        }

        /// <summary>
        ///   <para>Creates a rotation which rotates from /fromDirection/ to /toDirection/.</para>
        /// </summary>
        /// <param name="fromDirection"></param>
        /// <param name="toDirection"></param>
        public void SetFromToRotation(FPVector3 fromDirection, FPVector3 toDirection)
        {
            this = FromToRotation(fromDirection, toDirection);
        }

        public void SetLookRotation(FPVector3 view)
        {
            FPVector3 up = FPVector3.up;
            SetLookRotation(view, up);
        }

        /// <summary>
        ///   <para>Creates a rotation with the specified /forward/ and /upwards/ directions.</para>
        /// </summary>
        /// <param name="view">The direction to look in.</param>
        /// <param name="up">The vector that defines in which direction up is.</param>
        public void SetLookRotation(FPVector3 view, FPVector3 up)
        {
            this = LookRotation(view, up);
        }

        public void SetIdentity()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 1;
        }
    }
}