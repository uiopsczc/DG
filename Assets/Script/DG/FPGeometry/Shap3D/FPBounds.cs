/*************************************************************************************
 * 描    述:
 * 创 建 者:  czq
 * 创建时间:  2023/5/21
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
 *************************************************************************************/


using System;
using System.Collections.Generic;
#if UNITY_STANDALONE
using UnityEngine;

#endif

namespace DG
{
    public struct FPBounds : IEquatable<FPBounds>
    {
        private FPVector3 _center;
        private FPVector3 _extents;

        //顶部的点
        private FPVector3 localRightTopForward => size * 0.5f;
        private FPVector3 localRightTopBack => new FPVector3(size.x, size.y, -size.z) * 0.5f;
        private FPVector3 localLeftTopBack => new FPVector3(-size.x, size.y, -size.z) * 0.5f;
        private FPVector3 localLeftTopForward => new FPVector3(-size.x, size.y, size.z) * 0.5f;


        //底部的点
        private FPVector3 localRightBottomForward => new FPVector3(size.x, -size.y, size.z) * 0.5f;
        private FPVector3 localRightBottomBack => new FPVector3(size.x, -size.y, -size.z) * 0.5f;
        private FPVector3 localLeftBottomBack => -size * 0.5f;
        private FPVector3 localLeftBottomForward => new FPVector3(-size.x, -size.y, size.z) * 0.5f;

        //顶部的点
        private FPVector3 rightTopForward => localRightTopForward + center;
        private FPVector3 rightTopBack => localRightTopBack + center;
        private FPVector3 leftTopBack => localLeftTopBack + center;
        private FPVector3 leftTopForward => localLeftTopForward + center;


        //底部的点
        private FPVector3 rightBottomForward => localRightBottomForward + center;
        private FPVector3 rightBottomBack => localRightBottomBack + center;
        private FPVector3 leftBottomBack => localLeftBottomBack + center;
        private FPVector3 leftBottomForward => localLeftBottomForward + center;

        /// <summary>
        ///   <para>The center of the bounding box.</para>
        /// </summary>
        public FPVector3 center
        {
            get => _center;
            set => _center = value;
        }

        /// <summary>
        ///   <para>The total size of the box. This is always twice as large as the extents.</para>
        /// </summary>
        public FPVector3 size
        {
            get => _extents * 2f;
            set => _extents = value * 0.5f;
        }

        /// <summary>
        ///   <para>The extents of the Bounding Box. This is always half of the size of the Bounds.</para>
        /// </summary>
        public FPVector3 extents
        {
            get => _extents;
            set => _extents = value;
        }

        /// <summary>
        ///   <para>The minimal point of the box. This is always equal to center-extents.</para>
        /// </summary>
        public FPVector3 min
        {
            get => center - extents;
            set => SetMinMax(value, max);
        }

        /// <summary>
        ///   <para>The maximal point of the box. This is always equal to center+extents.</para>
        /// </summary>
        public FPVector3 max
        {
            get => center + extents;
            set => SetMinMax(min, value);
        }

        /// <summary>
        ///   <para>Creates a new Bounds.</para>
        /// </summary>
        /// <param name="center">The location of the origin of the Bounds.</param>
        /// <param name="size">The dimensions of the Bounds.</param>
        public FPBounds(FPVector3 center, FPVector3 size)
        {
            _center = center;
            _extents = size * 0.5f;
        }

#if UNITY_STANDALONE
        public static implicit operator Bounds(FPBounds value)
        {
            return new Bounds(value.center, value.size);
        }

        public static implicit operator FPBounds(Bounds value)
        {
            return new FPBounds(value.center, value.size);
        }
#endif

        public List<FPVector3> GetVertices()
        {
            List<FPVector3> list = new List<FPVector3>
            {
                rightTopForward,
                rightTopBack,
                leftTopBack,
                leftTopForward,
                rightBottomForward,
                rightBottomBack,
                leftBottomBack,
                leftBottomForward
            };

            return list;
        }

        public List<(FPVector3 p1, FPVector3 p2)> GetLines()
        {
            List<(FPVector3 p1, FPVector3 p2)> list = new List<(FPVector3 p1, FPVector3 p2)>
            {
                //顶部面
                (rightTopForward, rightTopBack),
                (rightTopBack, leftTopBack),
                (leftTopBack, leftTopForward),
                (leftTopForward, rightTopForward),
                //底部面
                (rightBottomForward, rightBottomBack),
                (rightBottomBack, leftBottomBack),
                (leftBottomBack, leftBottomForward),
                (leftBottomForward, rightBottomForward),
                //其余四条线
                (rightTopForward, rightBottomForward),
                (rightTopBack, rightBottomBack),
                (leftTopForward, leftBottomForward),
                (leftTopBack, leftBottomBack)
            };
            return list;
        }

        /*************************************************************************************
         * 模块描述:Equal ToString
         *************************************************************************************/
        public override bool Equals(object other)
        {
            if (!(other is FPBounds))
                return false;
            return Equals((FPBounds)other);
        }

        public bool Equals(FPBounds other)
        {
            return center.Equals(other.center) && extents.Equals(other.extents);
        }

        public override int GetHashCode()
        {
            FPVector3 vector3 = center;
            int hashCode = vector3.GetHashCode();
            vector3 = extents;
            int num = vector3.GetHashCode() << 2;
            return hashCode ^ num;
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for the bounds.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return string.Format("center: {0}, extents: {1}", _center, _extents);
        }

        /*************************************************************************************
         * 模块描述:关系运算符
         *************************************************************************************/
        public static bool operator ==(FPBounds lhs, FPBounds rhs)
        {
            return lhs.center == rhs.center && lhs.extents == rhs.extents;
        }

        public static bool operator !=(FPBounds lhs, FPBounds rhs)
        {
            return !(lhs == rhs);
        }

        /*************************************************************************************
         * 模块描述:StaticUtil
         *************************************************************************************/
        public static bool IntersectRayAABB(FPRay ray, FPBounds bounds, out FP distance)
        {
            return bounds.IntersectRay(ray, out distance);
        }

        /*************************************************************************************
         * 模块描述:MemberUtil
         *************************************************************************************/
        /// <summary>
        ///   <para>Sets the bounds to the min and max value of the box.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void SetMinMax(FPVector3 min, FPVector3 max)
        {
            extents = (max - min) * 0.5f;
            center = min + extents;
        }

        /// <summary>
        ///   <para>Grows the Bounds to include the point.</para>
        /// </summary>
        /// <param name="point"></param>
        public void Encapsulate(FPVector3 point)
        {
            SetMinMax(FPVector3.Min(min, point), FPVector3.Max(max, point));
        }

        /// <summary>
        ///   <para>Grow the bounds to encapsulate the bounds.</para>
        /// </summary>
        /// <param name="bounds"></param>
        public void Encapsulate(FPBounds bounds)
        {
            Encapsulate(bounds.center - bounds.extents);
            Encapsulate(bounds.center + bounds.extents);
        }

        /// <summary>
        ///   <para>Expand the bounds by increasing its size by amount along each side.</para>
        /// </summary>
        /// <param name="amount"></param>
        public void Expand(FP amount)
        {
            amount *= 0.5f;
            extents += new FPVector3(amount, amount, amount);
        }

        /// <summary>
        ///   <para>Expand the bounds by increasing its size by amount along each side.</para>
        /// </summary>
        /// <param name="amount"></param>
        public void Expand(FPVector3 amount)
        {
            extents += amount * 0.5f;
        }

        /// <summary>
        ///   <para>Does another bounding box intersect with this bounding box?</para>
        /// </summary>
        /// <param name="bounds"></param>
        public bool Intersects(FPBounds bounds)
        {
            return min.x <= bounds.max.x && max.x >= bounds.min.x &&
                   (min.y <= bounds.max.y && max.y >= bounds.min.y) && min.z <= bounds.max.z &&
                   max.z >= bounds.min.z;
        }

        /// <summary>
        ///   <para>Does ray intersect this bounding box?</para>
        /// </summary>
        /// <param name="ray"></param>
        public (bool isIntersect, FP distance) IntersectRay(FPRay ray)
        {
            var tmin = FPMath.MIN_VALUE;
            var tmax = FPMath.MAX_VALUE;
            FP t0, t1, f;
            FPVector3 t = center - ray.origin;
            var p = new FPVector3(t.x, t.y, t.z);
            t = extents;
            var extent = new FPVector3(t.x, t.y, t.z);
            t = ray.direction;
            var dir = new FPVector3(t.x, t.y, t.z);

            for (int i = 0; i < 3; i++)
            {
                f = 1 / dir[i];
                t0 = (p[i] + extent[i]) * f;
                t1 = (p[i] - extent[i]) * f;
                if (t0 < t1)
                {
                    if (t0 > tmin) tmin = t0;
                    if (t1 < tmax) tmax = t1;
                    if (tmin > tmax) return (false, default);
                    if (tmax < 0) return (false, default);
                }
                else
                {
                    if (t1 > tmin) tmin = t1;
                    if (t0 < tmax) tmax = t0;
                    if (tmin > tmax) return (false, default);
                    if (tmax < 0) return (false, default);
                }
            }

            return (true, tmin);
        }

        public bool IntersectRay(FPRay ray, out FP inDistance)
        {
            var (isIntersect, distance) = IntersectRay(ray);
            inDistance = distance;
            return isIntersect;
        }


        /// <summary>
        ///   <para>Is point contained in the bounding box?</para>
        /// </summary>
        /// <param name="point"></param>
        public bool Contains(FPVector3 point)
        {
            var min = this.min;
            var max = this.max;
            if (point.x < min.x || point.y < min.y || point.z < min.z || point.x > max.x || point.y > max.y ||
                point.z > max.z)
                return false;
            return true;
        }

        /// <summary>
        ///   <para>The smallest squared distance between the point and this bounding box.</para>
        /// </summary>
        /// <param name="point"></param>
        public FP SqrDistance(FPVector3 point)
        {
            var (closestPoint, distance) = ClosestPoint(point);
            return distance;
        }

        /// <summary>
        ///   <para>The closest point on the bounding box.</para>
        /// </summary>
        /// <param name="point">Arbitrary point.</param>
        /// <returns>
        ///   <para>The point on the bounding box or inside the bounding box.</para>
        /// </returns>
        public (FPVector3 closestPoint, FP distance) ClosestPoint(FPVector3 point)
        {
            var t = point - center;
            var closest = new FPVector3(t.x, t.y, t.z);
            var et = extents;
            var extent = new FPVector3(et.x, et.y, et.z);
            FP sqrtDistance = 0;
            FP delta;

            for (int i = 0; i < 3; i++)
            {
                if (closest[i] < -extent[i])
                {
                    delta = closest[i] + extent[i];
                    sqrtDistance += delta * delta;
                    closest[i] = -extent[i];
                }
                else if (closest[i] > extent[i])
                {
                    delta = closest[i] - extent[i];
                    sqrtDistance += delta * delta;
                    closest[i] = extent[i];
                }
            }

            if (sqrtDistance == 0)
                return (point, 0);
            var outPoint = closest + center;
            return (outPoint, sqrtDistance);
        }
    }
}