/*************************************************************************************
 * 描    述:  
 * 创 建 者:  czq
 * 创建时间:  2023/8/16
 * ======================================
 * 历史更新记录
 * 版本:V          修改时间:         修改人:
 * 修改内容:
 * ======================================
*************************************************************************************/


using System;
using System.Collections.Generic;

namespace DG
{
	public partial class FPIntersector
	{

		private static FPVector3 v0 = new FPVector3();
		private static FPVector3 v1 = new FPVector3();
		private static FPVector3 v2 = new FPVector3();
		private static List<FP> floatArray = new List<FP>();
		private static List<FP> floatArray2 = new List<FP>();


		/** Returns whether the given point is inside the triangle. This assumes that the point is on the plane of the triangle. No
		 * check is performed that this is the case. <br>
		 * If the Vector3 parameters contain both small and large values, such as one that contains 0.0001 and one that contains
		 * 10000000.0, this can fail due to floating-point imprecision.
		 * 
		 * @param t1 the first vertex of the triangle
		 * @param t2 the second vertex of the triangle
		 * @param t3 the third vertex of the triangle
		 * @return whether the point is in the triangle */
		public static bool isPointInTriangle(FPVector3 point, FPVector3 t1, FPVector3 t2, FPVector3 t3)
		{
			v0 = v0.set(t1).sub(point);
			v1 = v1.set(t2).sub(point);
			v2 = v2.set(t3).sub(point);

			v1 = v1.crs(v2);
			v2 = v2.crs(v0);

			if (v1.dot(v2) < 0f) return false;
			v0 = v0.crs(v2.set(t2).sub(point));
			return (v1.dot(v0) >= 0f);
		}

		/** Returns true if the given point is inside the triangle. */
		public static bool isPointInTriangle(FPVector2 p, FPVector2 a, FPVector2 b, FPVector2 c)
		{
			return isPointInTriangle(p.x, p.y, a.x, a.y, b.x, b.y, c.x, c.y);
		}

		/** Returns true if the given point is inside the triangle. */
		public static bool isPointInTriangle(FP px, FP py, FP ax, FP ay, FP bx, FP by, FP cx, FP cy)
		{
			FP px1 = px - ax;
			FP py1 = py - ay;
			bool side12 = (bx - ax) * py1 - (by - ay) * px1 > 0;
			if ((cx - ax) * py1 - (cy - ay) * px1 > 0 == side12) return false;
			if ((cx - bx) * (py - by) - (cy - by) * (px - bx) > 0 != side12) return false;
			return true;
		}

		public static bool intersectSegmentPlane(FPVector3 start, FPVector3 end, FPPlane plane, ref FPVector3 intersection)
		{
			FPVector3 dir = v0.set(end).sub(start);
			FP denom = dir.dot(plane.getNormal());
			if (denom == 0f) return false;
			FP t = -(start.dot(plane.getNormal()) + plane.getD()) / denom;
			if (t < 0 || t > 1) return false;

			if (intersection != FPVector3.Null)
				intersection = start.add(dir.scl(t));
			return true;
		}
		public static bool intersectSegmentPlane(FPVector3 start, FPVector3 end, FPPlane plane)
		{
			return intersectSegmentPlane(start, end, plane, ref FPVector3.Null);
		}


		/** Determines on which side of the given line the point is. Returns 1 if the point is on the left side of the line, 0 if the
		 * point is on the line and -1 if the point is on the right side of the line. Left and right are relative to the lines
		 * direction which is linePoint1 to linePoint2. */
		public static int pointLineSide(FPVector2 linePoint1, FPVector2 linePoint2, FPVector2 point)
		{
			return FPMath.Sign(
				(linePoint2.x - linePoint1.x) * (point.y - linePoint1.y) - (linePoint2.y - linePoint1.y) * (point.x - linePoint1.x));
		}

		public static int pointLineSide(FP linePoint1X, FP linePoint1Y, FP linePoint2X, FP linePoint2Y, FP pointX,
			FP pointY)
		{
			return FPMath.Sign((linePoint2X - linePoint1X) * (pointY - linePoint1Y) - (linePoint2Y - linePoint1Y) * (pointX - linePoint1X));
		}

		/** Checks whether the given point is in the polygon.
		 * @param polygon The polygon vertices passed as an array
		 * @return true if the point is in the polygon */
		public static bool isPointInPolygon(List<FPVector2> polygon, FPVector2 point)
		{
			FPVector2 last = polygon[0];
			FP x = point.x, y = point.y;
			bool oddNodes = false;
			for (int i = 0; i < polygon.Count; i++)
			{
				FPVector2 vertex = polygon[i];
				if ((vertex.y < y && last.y >= y) || (last.y < y && vertex.y >= y))
				{
					if (vertex.x + (y - vertex.y) / (last.y - vertex.y) * (last.x - vertex.x) < x) oddNodes = !oddNodes;
				}
				last = vertex;
			}
			return oddNodes;
		}

		/** Returns true if the specified point is in the polygon.
		 * @param offset Starting polygon index.
		 * @param count Number of array indices to use after offset. */
		public static bool isPointInPolygon(FP[] polygon, int offset, int count, FP x, FP y)
		{
			bool oddNodes = false;
			FP sx = polygon[offset], sy = polygon[offset + 1], y1 = sy;
			int yi = offset + 3;
			for (int n = offset + count; yi < n; yi += 2)
			{
				FP y2 = polygon[yi];
				if ((y2 < y && y1 >= y) || (y1 < y && y2 >= y))
				{
					FP x2 = polygon[yi - 1];
					if (x2 + (y - y2) / (y1 - y2) * (polygon[yi - 3] - x2) < x) oddNodes = !oddNodes;
				}
				y1 = y2;
			}
			if ((sy < y && y1 >= y) || (y1 < y && sy >= y))
			{
				if (sx + (y - sy) / (y1 - sy) * (polygon[yi - 3] - sx) < x) oddNodes = !oddNodes;
			}
			return oddNodes;
		}

		private static FPVector2 ip = new FPVector2();
		private static FPVector2 ep1 = new FPVector2();
		private static FPVector2 ep2 = new FPVector2();
		private static FPVector2 s = new FPVector2();
		private static FPVector2 e = new FPVector2();

		/** Intersects two convex polygons with clockwise vertices and sets the overlap polygon resulting from the intersection.
		 * Follows the Sutherland-Hodgman algorithm.
		 * @param p1 The polygon that is being clipped
		 * @param p2 The clip polygon
		 * @param overlap The intersection of the two polygons (can be null, if an intersection polygon is not needed)
		 * @return Whether the two polygons intersect. */
		public static bool intersectPolygons(FPPolygon p1, FPPolygon p2, FPPolygon overlap = null)
		{
			if (p1.getVertices().Length == 0 || p2.getVertices().Length == 0)
			{
				return false;
			}
			FPVector2 ip = FPIntersector.ip, ep1 = FPIntersector.ep1, ep2 = FPIntersector.ep2, s = FPIntersector.s, e = FPIntersector.e;
			List<FP> floatArray = FPIntersector.floatArray, floatArray2 = FPIntersector.floatArray2;
			floatArray.Clear();
			floatArray2.Clear();
			floatArray2.AddRange(p1.getTransformedVertices());
			FP[] vertices2 = p2.getTransformedVertices();
			for (int i = 0, last = vertices2.Length - 2; i <= last; i += 2)
			{
				ep1.set(vertices2[i], vertices2[i + 1]);
				// wrap around to beginning of array if index points to end;
				if (i < last)
					ep2.set(vertices2[i + 2], vertices2[i + 3]);
				else
					ep2.set(vertices2[0], vertices2[1]);
				if (floatArray2.Count == 0) return false;
				s.set(floatArray2[floatArray2.Count - 2], floatArray2[floatArray2.Count - 1]);
				for (int j = 0; j < floatArray2.Count; j += 2)
				{
					e.set(floatArray2[j], floatArray2[j + 1]);
					// determine if point is inside clip edge
					bool side = FPIntersector.pointLineSide(ep2, ep1, s) > 0;
					if (FPIntersector.pointLineSide(ep2, ep1, e) > 0)
					{
						if (!side)
						{
							FPIntersector.intersectLines(s, e, ep1, ep2, ref ip);
							if (floatArray.Count < 2 || floatArray[floatArray.Count - 2] != ip.x
								|| floatArray[floatArray.Count - 1] != ip.y)
							{
								floatArray.Add(ip.x);
								floatArray.Add(ip.y);
							}
						}
						floatArray.Add(e.x);
						floatArray.Add(e.y);
					}
					else if (side)
					{
						FPIntersector.intersectLines(s, e, ep1, ep2, ref ip);
						floatArray.Add(ip.x);
						floatArray.Add(ip.y);
					}
					s.set(e.x, e.y);
				}
				floatArray2.Clear();
				floatArray2.AddRange(floatArray);
				floatArray.Clear();
			}
			if (floatArray2.Count != 0)
			{
				if (overlap != null)
				{
					if (overlap.getVertices().Length == floatArray2.Count)
						Array.Copy(floatArray2.ToArray(), 0, overlap.getVertices(), 0, floatArray2.Count);
					else
						overlap.setVertices(floatArray2.ToArray());
				}
				return true;
			}
			return false;
		}

		/** Returns true if the specified poygons intersect. */
		public static bool intersectPolygons(List<FP> polygon1, List<FP> polygon2)
		{
			var polygon1Array = polygon1.ToArray();
			var polygon2Array = polygon2.ToArray();
			if (FPIntersector.isPointInPolygon(polygon1Array, 0, polygon1.Count, polygon2Array[0], polygon2Array[1])) return true;
			if (FPIntersector.isPointInPolygon(polygon2Array, 0, polygon2.Count, polygon1Array[0], polygon1Array[1])) return true;
			return intersectPolygonEdges(polygon1, polygon2);
		}

		/** Returns true if the lines of the specified poygons intersect. */
		public static bool intersectPolygonEdges(List<FP> polygon1, List<FP> polygon2)
		{
			int last1 = polygon1.Count - 2, last2 = polygon2.Count - 2;
			FP[] p1 = polygon1.ToArray(), p2 = polygon2.ToArray();
			FP x1 = p1[last1], y1 = p1[last1 + 1];
			for (int i = 0; i <= last1; i += 2)
			{
				FP x2 = p1[i], y2 = p1[i + 1];
				FP x3 = p2[last2], y3 = p2[last2 + 1];
				for (int j = 0; j <= last2; j += 2)
				{
					FP x4 = p2[j], y4 = p2[j + 1];
					if (intersectSegments(x1, y1, x2, y2, x3, y3, x4, y4)) return true;
					x3 = x4;
					y3 = y4;
				}
				x1 = x2;
				y1 = y2;
			}
			return false;
		}

		static FPVector2 v2a = new FPVector2();
		static FPVector2 v2b = new FPVector2();
		static FPVector2 v2c = new FPVector2();
		static FPVector2 v2d = new FPVector2();

		/** Returns the distance between the given line and point. Note the specified line is not a line segment. */
		public static FP distanceLinePoint(FP startX, FP startY, FP endX, FP endY, FP pointX, FP pointY)
		{
			FP normalLength = FPVector2.len(endX - startX, endY - startY);
			return FPMath.Abs((pointX - startX) * (endY - startY) - (pointY - startY) * (endX - startX)) / normalLength;
		}

		/** Returns the distance between the given segment and point. */
		public static FP distanceSegmentPoint(FP startX, FP startY, FP endX, FP endY, FP pointX, FP pointY)
		{
			return nearestSegmentPoint(startX, startY, endX, endY, pointX, pointY, ref v2a).dst(pointX, pointY);
		}

		/** Returns the distance between the given segment and point. */
		public static FP distanceSegmentPoint(FPVector2 start, FPVector2 end, FPVector2 point)
		{
			return nearestSegmentPoint(start, end, point, ref v2a).dst(point);
		}

		/** Returns a point on the segment nearest to the specified point. */
		public static FPVector2 nearestSegmentPoint(FPVector2 start, FPVector2 end, FPVector2 point, ref FPVector2 nearest)
		{
			FP length2 = start.dst2(end);
			if (length2 == 0) return nearest.set(start);
			FP t = ((point.x - start.x) * (end.x - start.x) + (point.y - start.y) * (end.y - start.y)) / length2;
			if (t <= 0) return nearest.set(start);
			if (t >= 1) return nearest.set(end);
			return nearest.set(start.x + t * (end.x - start.x), start.y + t * (end.y - start.y));
		}

		/** Returns a point on the segment nearest to the specified point. */
		public static FPVector2 nearestSegmentPoint(FP startX, FP startY, FP endX, FP endY, FP pointX, FP pointY,
			ref FPVector2 nearest)
		{
			FP xDiff = endX - startX;
			FP yDiff = endY - startY;
			FP length2 = xDiff * xDiff + yDiff * yDiff;
			if (length2 == 0) return nearest.set(startX, startY);
			FP t = ((pointX - startX) * (endX - startX) + (pointY - startY) * (endY - startY)) / length2;
			if (t <= 0) return nearest.set(startX, startY);
			if (t >= 1) return nearest.set(endX, endY);
			return nearest.set(startX + t * (endX - startX), startY + t * (endY - startY));
		}

		/** Returns whether the given line segment intersects the given circle.
		 * @param start The start point of the line segment
		 * @param end The end point of the line segment
		 * @param center The center of the circle
		 * @param squareRadius The squared radius of the circle
		 * @return Whether the line segment and the circle intersect */
		public static bool intersectSegmentCircle(FPVector2 start, FPVector2 end, FPVector2 center, FP squareRadius)
		{
			tmp = tmp.set(end.x - start.x, end.y - start.y, 0);
			tmp1 = tmp1.set(center.x - start.x, center.y - start.y, 0);
			FP l = tmp.len();
			FP u = tmp1.dot(tmp.nor());
			if (u <= 0)
			{
				tmp2.set(start.x, start.y, 0);
			}
			else if (u >= l)
			{
				tmp2.set(end.x, end.y, 0);
			}
			else
			{
				tmp3.set(tmp.scl(u)); // remember tmp is already normalized
				tmp2.set(tmp3.x + start.x, tmp3.y + start.y, 0);
			}

			FP x = center.x - tmp2.x;
			FP y = center.y - tmp2.y;

			return x * x + y * y <= squareRadius;
		}

		/** Returns whether the given line segment intersects the given circle.
		 * @param start The start point of the line segment
		 * @param end The end point of the line segment
		 * @param circle The circle
		 * @param mtv A Minimum Translation Vector to fill in the case of a collision, or null (optional).
		 * @return Whether the line segment and the circle intersect */
		public static bool intersectSegmentCircle(FPVector2 start, FPVector2 end, FPCircle circle, ref FPMinimumTranslationVector mtv)
		{
			v2a = v2a.set(end).sub(start);
			v2b = v2b.set(circle.x - start.x, circle.y - start.y);
			FP len = v2a.len();
			FP u = v2b.dot(v2a.nor());
			if (u <= 0)
			{
				v2c.set(start);
			}
			else if (u >= len)
			{
				v2c.set(end);
			}
			else
			{
				v2d.set(v2a.scl(u)); // remember v2a is already normalized
				v2c.set(v2d).add(start);
			}

			v2a.set(v2c.x - circle.x, v2c.y - circle.y);

			if (!mtv.Equals(FPMinimumTranslationVector.Null))
			{
				// Handle special case of segment containing circle center
				if (v2a.Equals(FPVector2.Zero))
				{
					v2d.set(end.y - start.y, start.x - end.x);
					mtv.normal = v2d.nor();
					mtv.depth = circle.radius;
				}
				else
				{
					mtv.normal = v2a.nor();
					mtv.depth = circle.radius - v2a.len();
				}
			}

			return v2a.len2() <= circle.radius * circle.radius;
		}

		/** Returns whether the given {@link Frustum} intersects a {@link BoundingBox}.
		 * @param frustum The frustum
		 * @param bounds The bounding box
		 * @return Whether the frustum intersects the bounding box */
		public static bool intersectFrustumBounds(FPFrustum frustum, FPBoundingBox bounds)
		{
			bool boundsIntersectsFrustum = frustum.pointInFrustum(bounds.getCorner000(ref tmp))
				|| frustum.pointInFrustum(bounds.getCorner001(ref tmp)) || frustum.pointInFrustum(bounds.getCorner010(ref tmp))
				|| frustum.pointInFrustum(bounds.getCorner011(ref tmp)) || frustum.pointInFrustum(bounds.getCorner100(ref tmp))
				|| frustum.pointInFrustum(bounds.getCorner101(ref tmp)) || frustum.pointInFrustum(bounds.getCorner110(ref tmp))
				|| frustum.pointInFrustum(bounds.getCorner111(ref tmp));

			if (boundsIntersectsFrustum)
			{
				return true;
			}

			bool frustumIsInsideBounds = false;
			for (int i = 0; i < frustum.planePoints.Length; i++)
			{
				FPVector3 point = frustum.planePoints[i];
				frustumIsInsideBounds |= bounds.contains(point);
			}
			return frustumIsInsideBounds;
		}

		/** Returns whether the given {@link Frustum} intersects a {@link OrientedBoundingBox}.
		 * @param frustum The frustum
		 * @param obb The oriented bounding box
		 * @return Whether the frustum intersects the oriented bounding box */
		public static bool intersectFrustumBounds(FPFrustum frustum, FPOrientedBoundingBox obb)
		{

			bool boundsIntersectsFrustum = false;
			var obbVectors = obb.getVertices();
			for (int i = 0; i < obbVectors.Length; i++)
			{
				FPVector3 v = obbVectors[i];
				boundsIntersectsFrustum |= frustum.pointInFrustum(v);
			}

			if (boundsIntersectsFrustum)
			{
				return true;
			}

			bool frustumIsInsideBounds = false;
			for (int i = 0; i < frustum.planePoints.Length; i++)
			{
				FPVector3 point = frustum.planePoints[i];
				frustumIsInsideBounds |= obb.contains(point);
			}

			return frustumIsInsideBounds;
		}

		/** Intersect two 2D Rays and return the scalar parameter of the first ray at the intersection point. You can get the
		 * intersection point by: Vector2 point(direction1).scl(scalar).add(start1); For more information, check:
		 * http://stackoverflow.com/a/565282/1091440
		 * @param start1 Where the first ray start
		 * @param direction1 The direction the first ray is pointing
		 * @param start2 Where the second ray start
		 * @param direction2 The direction the second ray is pointing
		 * @return scalar parameter on the first ray describing the point where the intersection happens. May be negative. In case the
		 *         rays are collinear, Float.POSITIVE_INFINITY will be returned. */
		public static FP intersectRayRay(FPVector2 start1, FPVector2 direction1, FPVector2 start2, FPVector2 direction2)
		{
			FP difx = start2.x - start1.x;
			FP dify = start2.y - start1.y;
			FP d1xd2 = direction1.x * direction2.y - direction1.y * direction2.x;
			if (d1xd2 == 0.0f)
			{
				return FP.MAX_VALUE; // collinear
			}
			FP d2sx = direction2.x / d1xd2;
			FP d2sy = direction2.y / d1xd2;
			return difx * d2sy - dify * d2sx;
		}

		/** Intersects a {@link Ray} and a {@link Plane}. The intersection point is stored in intersection in case an intersection is
		 * present.
		 * @param intersection The vector the intersection point is written to (optional)
		 * @return Whether an intersection is present. */
		public static bool intersectRayPlane(FPRay ray, FPPlane plane, ref FPVector3 intersection)
		{
			FP denom = ray.direction.dot(plane.getNormal());
			if (denom != 0)
			{
				FP t = -(ray.origin.dot(plane.getNormal()) + plane.getD()) / denom;
				if (t < 0) return false;

				if (intersection != FPVector3.Null)
					intersection = ray.origin.add(v0.set(ray.direction).scl(t));
				return true;
			}
			else if (plane.testPoint(ray.origin) == DGPlaneSide.OnPlane)
			{
				if (intersection != FPVector3.Null)
					intersection = ray.origin;
				return true;
			}
			else
				return false;
		}

		public static bool intersectRayPlane(FPRay ray, FPPlane plane)
		{
			return intersectRayPlane(ray, plane, ref FPVector3.Null);
		}

		/** Intersects a line and a plane. The intersection is returned as the distance from the first point to the plane. In case an
		 * intersection happened, the return value is in the range [0,1]. The intersection point can be recovered by point1 + t *
		 * (point2 - point1) where t is the return value of this method. */
		public static FP intersectLinePlane(FP x, FP y, FP z, FP x2, FP y2, FP z2, FPPlane plane,
			ref FPVector3 intersection)
		{
			FPVector3 direction = tmp.set(x2, y2, z2).sub(x, y, z);
			FPVector3 origin = tmp2.set(x, y, z);
			FP denom = direction.dot(plane.getNormal());
			if (denom != 0)
			{
				FP t = -(origin.dot(plane.getNormal()) + plane.getD()) / denom;
				if (intersection != FPVector3.Null)
					intersection = origin.add(direction.scl(t));
				return t;
			}
			else if (plane.testPoint(origin) == DGPlaneSide.OnPlane)
			{
				if (intersection != FPVector3.Null)
					intersection = origin;
				return 0;
			}

			return (-1);
		}

		public static FP intersectLinePlane(FP x, FP y, FP z, FP x2,
			FP y2, FP z2, FPPlane plane)
		{
			return intersectLinePlane(x, y, z, x2, y2, z2, plane, ref FPVector3.Null);
		}

		/** Returns true if the three {@link Plane planes} intersect, setting the point of intersection in {@code intersection}, if
		 * any.
		 * @param intersection The point where the three planes intersect */
		public static bool intersectPlanes(FPPlane a, FPPlane b, FPPlane c, ref FPVector3 intersection)
		{
			tmp1 = tmp1.set(a.normal).crs(b.normal);
			tmp2 = tmp2.set(b.normal).crs(c.normal);
			tmp3 = tmp3.set(c.normal).crs(a.normal);

			FP f = -a.normal.dot(tmp2);
			if (FPMath.Abs(f) < FPMath.EPSILION)
			{
				return false;
			}

			if (intersection != FPVector3.Null)
			{
				tmp1 = tmp1.scl(c.d);
				tmp2 = tmp2.scl(a.d);
				tmp3 = tmp3.scl(b.d);

				intersection.set(tmp1.x + tmp2.x + tmp3.x, tmp1.y + tmp2.y + tmp3.y, tmp1.z + tmp2.z + tmp3.z);
				intersection.scl(1 / f);
			}
			return true;
		}

		public static bool intersectPlanes(FPPlane a, FPPlane b, FPPlane c)
		{
			return intersectPlanes(a, b, c, ref FPVector3.Null);
		}

		private static FPPlane p = new FPPlane(default, 0);
		private static FPVector3 i = new FPVector3();

		/** Intersect a {@link Ray} and a triangle, returning the intersection point in intersection.
		 * @param t1 The first vertex of the triangle
		 * @param t2 The second vertex of the triangle
		 * @param t3 The third vertex of the triangle
		 * @param intersection The intersection point (optional)
		 * @return True in case an intersection is present. */
		public static bool intersectRayTriangle(FPRay ray, FPVector3 t1, FPVector3 t2, FPVector3 t3, ref FPVector3 intersection)
		{
			FPVector3 edge1 = v0.set(t2).sub(t1);
			FPVector3 edge2 = v1.set(t3).sub(t1);

			FPVector3 pvec = v2.set(ray.direction).crs(edge2);
			FP det = edge1.dot(pvec);
			if (FPMath.IsZero(det))
			{
				p.set(t1, t2, t3);
				if (p.testPoint(ray.origin) == DGPlaneSide.OnPlane && FPIntersector.isPointInTriangle(ray.origin, t1, t2, t3))
				{
					if (intersection != FPVector3.Null)
						intersection = ray.origin;
					return true;
				}
				return false;
			}

			det = 1.0f / det;

			FPVector3 tvec = i.set(ray.origin).sub(t1);
			FP u = tvec.dot(pvec) * det;
			if (u < 0.0f || u > 1.0f) return false;

			FPVector3 qvec = tvec.crs(edge1);
			FP v = ray.direction.dot(qvec) * det;
			if (v < 0.0f || u + v > 1.0f) return false;

			FP t = edge2.dot(qvec) * det;
			if (t < 0) return false;

			if (intersection != FPVector3.Null)
			{
				intersection = t <= FPMath.EPSILION ? ray.origin : ray.getEndPoint(t);
			}

			return true;
		}

		public static bool intersectRayTriangle(FPRay ray, FPVector3 t1, FPVector3 t2, FPVector3 t3)
		{
			return intersectRayTriangle(ray, t1, t2, t3, ref FPVector3.Null);
		}

		private static FPVector3 dir = new FPVector3();
		private static FPVector3 start = new FPVector3();

		/** Intersects a {@link Ray} and a sphere, returning the intersection point in intersection.
		 * @param ray The ray, the direction component must be normalized before calling this method
		 * @param center The center of the sphere
		 * @param radius The radius of the sphere
		 * @param intersection The intersection point (optional, can be null)
		 * @return Whether an intersection is present. */
		public static bool intersectRaySphere(FPRay ray, FPVector3 center, FP radius, ref FPVector3 intersection)
		{
			FP len = ray.direction.dot(center.x - ray.origin.x, center.y - ray.origin.y, center.z - ray.origin.z);
			if (len < 0.0f) // behind the ray
				return false;
			FP dst2 = center.dst2(ray.origin.x + ray.direction.x * len, ray.origin.y + ray.direction.y * len,
				ray.origin.z + ray.direction.z * len);
			FP r2 = radius * radius;
			if (dst2 > r2) return false;
			if (intersection != FPVector3.Null)
				intersection = ray.direction.scl(len - FPMath.Sqrt(r2 - dst2)).add(ray.origin);
			return true;
		}

		public static bool intersectRaySphere(FPRay ray, FPVector3 center, FP radius)
		{
			return intersectRaySphere(ray, center, radius, ref FPVector3.Null);
		}

		/** Intersects a {@link Ray} and a {@link BoundingBox}, returning the intersection point in intersection. This intersection is
		 * defined as the point on the ray closest to the origin which is within the specified bounds.
		 *
		 * <p>
		 * The returned intersection (if any) is guaranteed to be within the bounds of the bounding box, but it can occasionally
		 * diverge slightly from ray, due to small floating-point errors.
		 * </p>
		 *
		 * <p>
		 * If the origin of the ray is inside the box, this method returns true and the intersection point is set to the origin of the
		 * ray, accordingly to the definition above.
		 * </p>
		 * @param intersection The intersection point (optional)
		 * @return Whether an intersection is present. */
		public static bool intersectRayBounds(FPRay ray, FPBoundingBox box, ref FPVector3 intersection)
		{
			if (box.contains(ray.origin))
			{
				if (intersection != FPVector3.Null)
					intersection = ray.origin;
				return true;
			}
			FP lowest = 0, t;
			bool hit = false;

			// min x
			if (ray.origin.x <= box.min.x && ray.direction.x > 0)
			{
				t = (box.min.x - ray.origin.x) / ray.direction.x;
				if (t >= 0)
				{
					v2 = v2.set(ray.direction).scl(t).add(ray.origin);
					if (v2.y >= box.min.y && v2.y <= box.max.y && v2.z >= box.min.z && v2.z <= box.max.z && (!hit || t < lowest))
					{
						hit = true;
						lowest = t;
					}
				}
			}
			// max x
			if (ray.origin.x >= box.max.x && ray.direction.x < 0)
			{
				t = (box.max.x - ray.origin.x) / ray.direction.x;
				if (t >= 0)
				{
					v2 = v2.set(ray.direction).scl(t).add(ray.origin);
					if (v2.y >= box.min.y && v2.y <= box.max.y && v2.z >= box.min.z && v2.z <= box.max.z && (!hit || t < lowest))
					{
						hit = true;
						lowest = t;
					}
				}
			}
			// min y
			if (ray.origin.y <= box.min.y && ray.direction.y > 0)
			{
				t = (box.min.y - ray.origin.y) / ray.direction.y;
				if (t >= 0)
				{
					v2 = v2.set(ray.direction).scl(t).add(ray.origin);
					if (v2.x >= box.min.x && v2.x <= box.max.x && v2.z >= box.min.z && v2.z <= box.max.z && (!hit || t < lowest))
					{
						hit = true;
						lowest = t;
					}
				}
			}
			// max y
			if (ray.origin.y >= box.max.y && ray.direction.y < 0)
			{
				t = (box.max.y - ray.origin.y) / ray.direction.y;
				if (t >= 0)
				{
					v2 = v2.set(ray.direction).scl(t).add(ray.origin);
					if (v2.x >= box.min.x && v2.x <= box.max.x && v2.z >= box.min.z && v2.z <= box.max.z && (!hit || t < lowest))
					{
						hit = true;
						lowest = t;
					}
				}
			}
			// min z
			if (ray.origin.z <= box.min.z && ray.direction.z > 0)
			{
				t = (box.min.z - ray.origin.z) / ray.direction.z;
				if (t >= 0)
				{
					v2 = v2.set(ray.direction).scl(t).add(ray.origin);
					if (v2.x >= box.min.x && v2.x <= box.max.x && v2.y >= box.min.y && v2.y <= box.max.y && (!hit || t < lowest))
					{
						hit = true;
						lowest = t;
					}
				}
			}
			// max z
			if (ray.origin.z >= box.max.z && ray.direction.z < 0)
			{
				t = (box.max.z - ray.origin.z) / ray.direction.z;
				if (t >= 0)
				{
					v2 = v2.set(ray.direction).scl(t).add(ray.origin);
					if (v2.x >= box.min.x && v2.x <= box.max.x && v2.y >= box.min.y && v2.y <= box.max.y && (!hit || t < lowest))
					{
						hit = true;
						lowest = t;
					}
				}
			}
			if (hit && intersection != FPVector3.Null)
			{
				intersection = ray.direction.scl(lowest).add(ray.origin);
				if (intersection.x < box.min.x)
				{
					intersection.x = box.min.x;
				}
				else if (intersection.x > box.max.x)
				{
					intersection.x = box.max.x;
				}
				if (intersection.y < box.min.y)
				{
					intersection.y = box.min.y;
				}
				else if (intersection.y > box.max.y)
				{
					intersection.y = box.max.y;
				}
				if (intersection.z < box.min.z)
				{
					intersection.z = box.min.z;
				}
				else if (intersection.z > box.max.z)
				{
					intersection.z = box.max.z;
				}
			}
			return hit;
		}

		public static bool intersectRayBounds(FPRay ray, FPBoundingBox box)
		{
			return intersectRayBounds(ray, box, ref FPVector3.Null);
		}

		/** Quick check whether the given {@link Ray} and {@link BoundingBox} intersect.
		 * @return Whether the ray and the bounding box intersect. */
		public static bool intersectRayBoundsFast(FPRay ray, FPBoundingBox box)
		{
			return intersectRayBoundsFast(ray, box.getCenter(ref tmp1), box.getDimensions(ref tmp2));
		}

		/** Quick check whether the given {@link Ray} and {@link BoundingBox} intersect.
		 * @param center The center of the bounding box
		 * @param dimensions The dimensions (width, height and depth) of the bounding box
		 * @return Whether the ray and the bounding box intersect. */
		public static bool intersectRayBoundsFast(FPRay ray, FPVector3 center, FPVector3 dimensions)
		{
			FP divX = 1f / ray.direction.x;
			FP divY = 1f / ray.direction.y;
			FP divZ = 1f / ray.direction.z;

			FP minx = ((center.x - dimensions.x * 0.5f) - ray.origin.x) * divX;
			FP maxx = ((center.x + dimensions.x * 0.5f) - ray.origin.x) * divX;
			if (minx > maxx)
			{
				FP t = minx;
				minx = maxx;
				maxx = t;
			}

			FP miny = ((center.y - dimensions.y * 0.5f) - ray.origin.y) * divY;
			FP maxy = ((center.y + dimensions.y * 0.5f) - ray.origin.y) * divY;
			if (miny > maxy)
			{
				FP t = miny;
				miny = maxy;
				maxy = t;
			}

			FP minz = ((center.z - dimensions.z * 0.5f) - ray.origin.z) * divZ;
			FP maxz = ((center.z + dimensions.z * 0.5f) - ray.origin.z) * divZ;
			if (minz > maxz)
			{
				FP t = minz;
				minz = maxz;
				maxz = t;
			}

			FP min = FPMath.Max(FPMath.Max(minx, miny), minz);
			FP max = FPMath.Min(FPMath.Min(maxx, maxy), maxz);

			return max >= 0 && max >= min;
		}

		/** Check whether the given {@link Ray} and {@link OrientedBoundingBox} intersect.
		 *
		 * @return Whether the ray and the oriented bounding box intersect. */
		public static bool intersectRayOrientedBoundsFast(FPRay ray, FPOrientedBoundingBox obb)
		{
			return intersectRayOrientedBounds(ray, obb);
		}

		/** Check whether the given {@link Ray} and Oriented {@link BoundingBox} intersect.
		 * @param transform - the BoundingBox transformation
		 *
		 * @return Whether the ray and the oriented bounding box intersect. */
		public static bool intersectRayOrientedBoundsFast(FPRay ray, FPBoundingBox bounds, FPMatrix4x4 transform)
		{
			return intersectRayOrientedBounds(ray, bounds, transform);
		}

		/** Check whether the given {@link Ray} and {@link OrientedBoundingBox} intersect.
		 *
		 * @param intersection The intersection point (optional)
		 * @return Whether an intersection is present. */
		public static bool intersectRayOrientedBounds(FPRay ray, FPOrientedBoundingBox obb, ref FPVector3 intersection)
		{
			FPBoundingBox bounds = obb.getBounds();
			FPMatrix4x4 transform = obb.getTransform();
			return intersectRayOrientedBounds(ray, bounds, transform, ref intersection);
		}

		public static bool intersectRayOrientedBounds(FPRay ray, FPOrientedBoundingBox obb)
		{
			return intersectRayOrientedBounds(ray, obb, ref FPVector3.Null);
		}

		/** Check whether the given {@link Ray} and {@link OrientedBoundingBox} intersect.
		 *
		 * Based on code at: https://github.com/opengl-tutorials/ogl/blob/master/misc05_picking/misc05_picking_custom.cpp#L83
		 * @param intersection The intersection point (optional)
		 * @return Whether an intersection is present. */
		public static bool intersectRayOrientedBounds(FPRay ray, FPBoundingBox bounds, FPMatrix4x4 transform, ref FPVector3 intersection)
		{
			FP tMin = 0.0f;
			FP tMax = FP.MAX_VALUE;
			FP t1, t2;

			FPVector3 oBBposition = transform.getTranslation(ref tmp);
			FPVector3 delta = oBBposition.sub(ray.origin);

			// Test intersection with the 2 planes perpendicular to the OBB's X axis
			tmp1 = tmp1.set(transform.m00, transform.m10, transform.m20);
			FPVector3 xaxis = tmp1;
			FP e = xaxis.dot(delta);
			FP f = ray.direction.dot(xaxis);

			if (FPMath.Abs(f) > FPMath.EPSILION)
			{ // Standard case
				t1 = (e + bounds.min.x) / f; // Intersection with the "left" plane
				t2 = (e + bounds.max.x) / f; // Intersection with the "right" plane
											 // t1 and t2 now contain distances between ray origin and ray-plane intersections

				// We want t1 to represent the nearest intersection,
				// so if it's not the case, invert t1 and t2
				if (t1 > t2)
				{
					FP w = t1;
					t1 = t2;
					t2 = w;
				}
				// tMax is the nearest "far" intersection (amongst the X,Y and Z planes pairs)
				if (t2 < tMax)
				{
					tMax = t2;
				}
				// tMin is the farthest "near" intersection (amongst the X,Y and Z planes pairs)
				if (t1 > tMin)
				{
					tMin = t1;
				}

				// And here's the trick :
				// If "far" is closer than "near", then there is NO intersection.
				if (tMax < tMin)
				{
					return false;
				}
				// Rare case : the ray is almost parallel to the planes, so they don't have any "intersection"
			}
			else if (-e + bounds.min.x > 0.0f || -e + bounds.max.x < 0.0f)
			{
				return false;
			}

			// Test intersection with the 2 planes perpendicular to the OBB's Y axis
			// Exactly the same thing than above.
			tmp2 = tmp2.set(transform.m01, transform.m11, transform.m21);
			FPVector3 yaxis = tmp2;


			e = yaxis.dot(delta);
			f = ray.direction.dot(yaxis);

			if (FPMath.Abs(f) > FPMath.EPSILION)
			{
				t1 = (e + bounds.min.y) / f;
				t2 = (e + bounds.max.y) / f;

				if (t1 > t2)
				{
					FP w = t1;
					t1 = t2;
					t2 = w;
				}
				if (t2 < tMax)
				{
					tMax = t2;
				}
				if (t1 > tMin)
				{
					tMin = t1;
				}
				if (tMin > tMax)
				{
					return false;
				}

			}
			else if (-e + bounds.min.y > 0.0f || -e + bounds.max.y < 0.0f)
			{
				return false;
			}

			// Test intersection with the 2 planes perpendicular to the OBB's Z axis
			// Exactly the same thing than above.
			tmp3 = tmp3.set(transform.m02, transform.m12, transform.m22);
			FPVector3 zaxis = tmp3;


			e = zaxis.dot(delta);
			f = ray.direction.dot(zaxis);

			if (FPMath.Abs(f) > FPMath.EPSILION)
			{
				t1 = (e + bounds.min.z) / f;
				t2 = (e + bounds.max.z) / f;

				if (t1 > t2)
				{
					FP w = t1;
					t1 = t2;
					t2 = w;
				}
				if (t2 < tMax)
				{
					tMax = t2;
				}
				if (t1 > tMin)
				{
					tMin = t1;
				}
				if (tMin > tMax)
				{
					return false;
				}
			}
			else if (-e + bounds.min.z > 0.0f || -e + bounds.max.z < 0.0f)
			{
				return false;
			}

			if (intersection != FPVector3.Null)
			{
				intersection = ray.getEndPoint(tMin);
			}

			return true;
		}

		public static bool intersectRayOrientedBounds(FPRay ray, FPBoundingBox bounds, FPMatrix4x4 transform)
		{
			return intersectRayOrientedBounds(ray, bounds, transform, ref FPVector3.Null);
		}

		static FPVector3 best = new FPVector3();
		static FPVector3 tmp = new FPVector3();
		static FPVector3 tmp1 = new FPVector3();
		static FPVector3 tmp2 = new FPVector3();
		static FPVector3 tmp3 = new FPVector3();

		/** Intersects the given ray with list of triangles. Returns the nearest intersection point in intersection
		 * @param triangles The triangles, each successive 9 elements are the 3 vertices of a triangle, a vertex is made of 3
		 *           successive floats (XYZ)
		 * @param intersection The nearest intersection point (optional)
		 * @return Whether the ray and the triangles intersect. */
		public static bool intersectRayTriangles(FPRay ray, FP[] triangles, ref FPVector3 intersection)
		{
			FP min_dist = FP.MAX_VALUE;
			bool hit = false;

			if (triangles.Length % 9 != 0) throw new Exception("triangles array size is not a multiple of 9");

			for (int i = 0; i < triangles.Length; i += 9)
			{
				bool result = intersectRayTriangle(ray, tmp1.set(triangles[i], triangles[i + 1], triangles[i + 2]),
					tmp2.set(triangles[i + 3], triangles[i + 4], triangles[i + 5]),
					tmp3.set(triangles[i + 6], triangles[i + 7], triangles[i + 8]), ref tmp);

				if (result)
				{
					FP dist = ray.origin.dst2(tmp);
					if (dist < min_dist)
					{
						min_dist = dist;
						best.set(tmp);
						hit = true;
					}
				}
			}

			if (!hit)
				return false;
			else
			{
				if (intersection != FPVector3.Null)
					intersection = best;
				return true;
			}
		}

		public static bool intersectRayTriangles(FPRay ray, FP[] triangles)
		{
			return intersectRayTriangles(ray, triangles, ref FPVector3.Null);
		}

		/** Intersects the given ray with list of triangles. Returns the nearest intersection point in intersection
		 * @param indices the indices, each successive 3 shorts index the 3 vertices of a triangle
		 * @param vertexSize the size of a vertex in floats
		 * @param intersection The nearest intersection point (optional)
		 * @return Whether the ray and the triangles intersect. */
		public static bool intersectRayTriangles(FPRay ray, FP[] vertices, short[] indices, int vertexSize,
			ref FPVector3 intersection)
		{
			FP min_dist = FP.MAX_VALUE;
			bool hit = false;

			if (indices.Length % 3 != 0) throw new Exception("triangle list size is not a multiple of 3");

			for (int i = 0; i < indices.Length; i += 3)
			{
				int i1 = indices[i] * vertexSize;
				int i2 = indices[i + 1] * vertexSize;
				int i3 = indices[i + 2] * vertexSize;

				bool result = intersectRayTriangle(ray, tmp1.set(vertices[i1], vertices[i1 + 1], vertices[i1 + 2]),
					tmp2.set(vertices[i2], vertices[i2 + 1], vertices[i2 + 2]),
					tmp3.set(vertices[i3], vertices[i3 + 1], vertices[i3 + 2]), ref tmp);

				if (result)
				{
					FP dist = ray.origin.dst2(tmp);
					if (dist < min_dist)
					{
						min_dist = dist;
						best.set(tmp);
						hit = true;
					}
				}
			}

			if (!hit)
				return false;
			else
			{
				if (intersection != FPVector3.Null)
					intersection = best;
				return true;
			}
		}

		public static bool intersectRayTriangles(FPRay ray, FP[] vertices, short[] indices, int vertexSize)
		{
			return intersectRayTriangles(ray, vertices, indices, vertexSize, ref FPVector3.Null);
		}

		/** Intersects the given ray with list of triangles. Returns the nearest intersection point in intersection
		 * @param triangles The triangles, each successive 3 elements are the 3 vertices of a triangle
		 * @param intersection The nearest intersection point (optional)
		 * @return Whether the ray and the triangles intersect. */
		public static bool intersectRayTriangles(FPRay ray, List<FPVector3> triangles, ref FPVector3 intersection)
		{
			FP min_dist = FP.MAX_VALUE;
			bool hit = false;

			if (triangles.Count % 3 != 0) throw new Exception("triangle list size is not a multiple of 3");

			for (int i = 0; i < triangles.Count; i += 3)
			{
				bool result = intersectRayTriangle(ray, triangles[i], triangles[i + 1], triangles[i + 2], ref tmp);

				if (result)
				{
					FP dist = ray.origin.dst2(tmp);
					if (dist < min_dist)
					{
						min_dist = dist;
						best.set(tmp);
						hit = true;
					}
				}
			}

			if (!hit)
				return false;
			else
			{
				if (intersection != FPVector3.Null)
					intersection = best;
				return true;
			}
		}

		public static bool intersectRayTriangles(FPRay ray, List<FPVector3> triangles)
		{
			return intersectRayTriangles(ray, triangles, ref FPVector3.Null);
		}

		/** Quick check whether the given {@link BoundingBox} and {@link Plane} intersect.
		 * @return Whether the bounding box and the plane intersect. */
		public static bool intersectBoundsPlaneFast(FPBoundingBox box, FPPlane plane)
		{
			return intersectBoundsPlaneFast(box.getCenter(ref tmp1), box.getDimensions(ref tmp2).scl(0.5f), plane.normal, plane.d);
		}

		/** Quick check whether the given bounding box and a plane intersect. Code adapted from Christer Ericson's Real Time Collision
		 * @param center The center of the bounding box
		 * @param halfDimensions Half of the dimensions (width, height and depth) of the bounding box
		 * @param normal The normal of the plane
		 * @param distance The distance of the plane
		 * @return Whether the bounding box and the plane intersect. */
		public static bool intersectBoundsPlaneFast(FPVector3 center, FPVector3 halfDimensions, FPVector3 normal, FP distance)
		{
			// Compute the projection interval radius of b onto L(t) = b.c + t * p.n
			FP radius = halfDimensions.x * FPMath.Abs(normal.x) + halfDimensions.y * FPMath.Abs(normal.y)
				+ halfDimensions.z * FPMath.Abs(normal.z);

			// Compute distance of box center from plane
			FP s = normal.dot(center) - distance;

			// Intersection occurs when plane distance falls within [-r,+r] interval
			return FPMath.Abs(s) <= radius;
		}

		/** Intersects the two lines and returns the intersection point in intersection.
		 * @param p1 The first point of the first line
		 * @param p2 The second point of the first line
		 * @param p3 The first point of the second line
		 * @param p4 The second point of the second line
		 * @param intersection The intersection point. May be null.
		 * @return Whether the two lines intersect */
		public static bool intersectLines(FPVector2 p1, FPVector2 p2, FPVector2 p3, FPVector2 p4, ref FPVector2 intersection)
		{
			return intersectLines(p1.x, p1.y, p2.x, p2.y, p3.x, p3.y, p4.x, p4.y, ref intersection);
		}

		public static bool intersectLines(FPVector2 p1, FPVector2 p2, FPVector2 p3, FPVector2 p4)
		{
			return intersectLines(p1, p2, p3, p4, ref FPVector2.Null);
		}


		/** Intersects the two lines and returns the intersection point in intersection.
		 * @param intersection The intersection point, or null.
		 * @return Whether the two lines intersect */
		public static bool intersectLines(FP x1, FP y1, FP x2, FP y2, FP x3, FP y3, FP x4, FP y4,
			ref FPVector2 intersection)
		{
			FP d = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
			if (d == 0) return false;

			if (intersection != FPVector2.Null)
			{
				FP ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / d;
				intersection = new FPVector2(x1 + (x2 - x1) * ua, y1 + (y2 - y1) * ua);
			}
			return true;
		}

		public static bool intersectLines(FP x1, FP y1, FP x2, FP y2,
			FP x3, FP y3, FP x4, FP y4)
		{
			return intersectLines(x1, y1, x2, y2, x3, y3, x4, y4, ref FPVector2.Null);
		}

		/** Check whether the given line and {@link Polygon} intersect.
		 * @param p1 The first point of the line
		 * @param p2 The second point of the line
		 * @return Whether polygon and line intersects */
		public static bool intersectLinePolygon(FPVector2 p1, FPVector2 p2, FPPolygon polygon)
		{
			FP[] vertices = polygon.getTransformedVertices();
			FP x1 = p1.x, y1 = p1.y, x2 = p2.x, y2 = p2.y;
			int n = vertices.Length;
			FP x3 = vertices[n - 2], y3 = vertices[n - 1];
			for (int i = 0; i < n; i += 2)
			{
				FP x4 = vertices[i], y4 = vertices[i + 1];
				FP d = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
				if (d != 0)
				{
					FP yd = y1 - y3;
					FP xd = x1 - x3;
					FP ua = ((x4 - x3) * yd - (y4 - y3) * xd) / d;
					if (ua >= 0 && ua <= 1)
					{
						return true;
					}
				}
				x3 = x4;
				y3 = y4;
			}
			return false;
		}

		/** Determines whether the given rectangles intersect and, if they do, sets the supplied {@code intersection} rectangle to the
		 * area of overlap.
		 * @return Whether the rectangles intersect */
		public static bool intersectRectangles(FPRectangle rectangle1, FPRectangle rectangle2, ref FPRectangle intersection)
		{
			if (rectangle1.overlaps(rectangle2))
			{
				if (!intersection.Equals(FPRectangle.Null))
				{
					intersection.x = FPMath.Max(rectangle1.x, rectangle2.x);
					intersection.width = FPMath.Min(rectangle1.x + rectangle1.width, rectangle2.x + rectangle2.width) - intersection.x;
					intersection.y = FPMath.Max(rectangle1.y, rectangle2.y);
					intersection.height = FPMath.Min(rectangle1.y + rectangle1.height, rectangle2.y + rectangle2.height) - intersection.y;
				}
				return true;
			}
			return false;
		}

		public static bool intersectRectangles(FPRectangle rectangle1, FPRectangle rectangle2)
		{
			return intersectRectangles(rectangle1, rectangle2, ref FPRectangle.Null);
		}

		/** Determines whether the given rectangle and segment intersect
		 * @param startX x-coordinate start of line segment
		 * @param startY y-coordinate start of line segment
		 * @param endX y-coordinate end of line segment
		 * @param endY y-coordinate end of line segment
		 * @param rectangle rectangle that is being tested for collision
		 * @return whether the rectangle intersects with the line segment */
		public static bool intersectSegmentRectangle(FP startX, FP startY, FP endX, FP endY, FPRectangle rectangle)
		{
			FP rectangleEndX = rectangle.x + rectangle.width;
			FP rectangleEndY = rectangle.y + rectangle.height;

			if (intersectSegments(startX, startY, endX, endY, rectangle.x, rectangle.y, rectangle.x, rectangleEndY)) return true;

			if (intersectSegments(startX, startY, endX, endY, rectangle.x, rectangle.y, rectangleEndX, rectangle.y)) return true;

			if (intersectSegments(startX, startY, endX, endY, rectangleEndX, rectangle.y, rectangleEndX, rectangleEndY))
				return true;

			if (intersectSegments(startX, startY, endX, endY, rectangle.x, rectangleEndY, rectangleEndX, rectangleEndY))
				return true;

			return rectangle.contains(startX, startY);
		}

		/** {@link #intersectSegmentRectangle(float, float, float, float, Rectangle)} */
		public static bool intersectSegmentRectangle(FPVector2 start, FPVector2 end, FPRectangle rectangle)
		{
			return intersectSegmentRectangle(start.x, start.y, end.x, end.y, rectangle);
		}

		/** Check whether the given line segment and {@link Polygon} intersect.
		 * @param p1 The first point of the segment
		 * @param p2 The second point of the segment
		 * @return Whether polygon and segment intersect */
		public static bool intersectSegmentPolygon(FPVector2 p1, FPVector2 p2, FPPolygon polygon)
		{
			FP[] vertices = polygon.getTransformedVertices();
			FP x1 = p1.x, y1 = p1.y, x2 = p2.x, y2 = p2.y;
			int n = vertices.Length;
			FP x3 = vertices[n - 2], y3 = vertices[n - 1];
			for (int i = 0; i < n; i += 2)
			{
				FP x4 = vertices[i], y4 = vertices[i + 1];
				FP d = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
				if (d != 0)
				{
					FP yd = y1 - y3;
					FP xd = x1 - x3;
					FP ua = ((x4 - x3) * yd - (y4 - y3) * xd) / d;
					if (ua >= 0 && ua <= 1)
					{
						FP ub = ((x2 - x1) * yd - (y2 - y1) * xd) / d;
						if (ub >= 0 && ub <= 1)
						{
							return true;
						}
					}
				}
				x3 = x4;
				y3 = y4;
			}
			return false;
		}

		/** Intersects the two line segments and returns the intersection point in intersection.
		 * @param p1 The first point of the first line segment
		 * @param p2 The second point of the first line segment
		 * @param p3 The first point of the second line segment
		 * @param p4 The second point of the second line segment
		 * @param intersection The intersection point. May be null.
		 * @return Whether the two line segments intersect */
		public static bool intersectSegments(FPVector2 p1, FPVector2 p2, FPVector2 p3, FPVector2 p4, ref FPVector2 intersection)
		{
			return intersectSegments(p1.x, p1.y, p2.x, p2.y, p3.x, p3.y, p4.x, p4.y, ref intersection);
		}

		public static bool intersectSegments(FPVector2 p1, FPVector2 p2, FPVector2 p3, FPVector2 p4)
		{
			return intersectSegments(p1, p2, p3, p4, ref FPVector2.Null);
		}


		/** @param intersection May be null. */
		public static bool intersectSegments(FP x1, FP y1, FP x2, FP y2, FP x3, FP y3, FP x4, FP y4,
			ref FPVector2 intersection)
		{
			FP d = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
			if (d == 0) return false;

			FP yd = y1 - y3;
			FP xd = x1 - x3;
			FP ua = ((x4 - x3) * yd - (y4 - y3) * xd) / d;
			if (ua < 0 || ua > 1) return false;

			FP ub = ((x2 - x1) * yd - (y2 - y1) * xd) / d;
			if (ub < 0 || ub > 1) return false;

			if (intersection != FPVector2.Null)
				intersection = new FPVector2(x1 + (x2 - x1) * ua, y1 + (y2 - y1) * ua);
			return true;
		}

		public static bool intersectSegments(FP x1, FP y1, FP x2, FP y2,
			FP x3, FP y3, FP x4, FP y4)
		{
			return intersectSegments(x1, y1, x2, y2, x3, y3, x4, y4, ref FPVector2.Null);
		}


		static FP det(FP a, FP b, FP c, FP d)
		{
			return a * d - b * c;
		}

		public static bool overlaps(FPCircle c1, FPCircle c2)
		{
			return c1.overlaps(c2);
		}

		public static bool overlaps(FPRectangle r1, FPRectangle r2)
		{
			return r1.overlaps(r2);
		}

		public static bool overlaps(FPCircle c, FPRectangle r)
		{
			FP closestX = c.x;
			FP closestY = c.y;

			if (c.x < r.x)
			{
				closestX = r.x;
			}
			else if (c.x > r.x + r.width)
			{
				closestX = r.x + r.width;
			}

			if (c.y < r.y)
			{
				closestY = r.y;
			}
			else if (c.y > r.y + r.height)
			{
				closestY = r.y + r.height;
			}

			closestX = closestX - c.x;
			closestX *= closestX;
			closestY = closestY - c.y;
			closestY *= closestY;

			return closestX + closestY < c.radius * c.radius;
		}

		/** Check whether specified convex polygons overlap (clockwise or counter-clockwise wound doesn't matter).
		 * @param p1 The first polygon.
		 * @param p2 The second polygon.
		 * @return Whether polygons overlap. */
		public static bool overlapConvexPolygons(FPPolygon p1, FPPolygon p2)
		{
			return overlapConvexPolygons(p1, p2, ref FPMinimumTranslationVector.Null);
		}

		/** Check whether convex polygons overlap (clockwise or counter-clockwise wound doesn't matter). If they do, optionally obtain
		 * a Minimum Translation Vector indicating the minimum magnitude vector required to push the polygon p1 out of collision with
		 * polygon p2.
		 * @param p1 The first polygon.
		 * @param p2 The second polygon.
		 * @param mtv A Minimum Translation Vector to fill in the case of a collision, or null (optional).
		 * @return Whether polygons overlap. */
		public static bool overlapConvexPolygons(FPPolygon p1, FPPolygon p2, ref FPMinimumTranslationVector mtv)
		{
			return overlapConvexPolygons(p1.getTransformedVertices(), p2.getTransformedVertices(), ref mtv);
		}

		/** @see #overlapConvexPolygons(float[], int, int, float[], int, int, MinimumTranslationVector) */
		public static bool overlapConvexPolygons(FP[] verts1, FP[] verts2, ref FPMinimumTranslationVector mtv)
		{
			return overlapConvexPolygons(verts1, 0, verts1.Length, verts2, 0, verts2.Length, ref mtv);
		}

		public static bool overlapConvexPolygons(FP[] verts1, FP[] verts2)
		{
			return overlapConvexPolygons(verts1, 0, verts1.Length, verts2, 0, verts2.Length, ref FPMinimumTranslationVector.Null);
		}

		/** Check whether polygons defined by the given vertex arrays overlap (clockwise or counter-clockwise wound doesn't matter). If
		 * they do, optionally obtain a Minimum Translation Vector indicating the minimum magnitude vector required to push the polygon
		 * defined by verts1 out of the collision with the polygon defined by verts2.
		 * @param verts1 Vertices of the first polygon.
		 * @param offset1 the offset of the verts1 array
		 * @param count1 the amount that is added to the offset1
		 * @param verts2 Vertices of the second polygon.
		 * @param offset2 the offset of the verts2 array
		 * @param count2 the amount that is added to the offset2
		 * @param mtv A Minimum Translation Vector to fill in the case of a collision, or null (optional).
		 * @return Whether polygons overlap. */
		public static bool overlapConvexPolygons(FP[] verts1, int offset1, int count1, FP[] verts2, int offset2, int count2,
			ref FPMinimumTranslationVector mtv)
		{
			bool overlaps;
			if (!mtv.Equals(FPMinimumTranslationVector.Null))
			{
				mtv.depth = FP.MAX_VALUE;
				mtv.normal.setZero();
			}
			overlaps = overlapsOnAxisOfShape(verts2, offset2, count2, verts1, offset1, count1, ref mtv, true);
			if (overlaps)
			{
				overlaps = overlapsOnAxisOfShape(verts1, offset1, count1, verts2, offset2, count2, ref mtv, false);
			}

			if (!overlaps)
			{
				if (!mtv.Equals(FPMinimumTranslationVector.Null))
				{
					mtv.depth = 0;
					mtv.normal.setZero();
				}
				return false;
			}
			return true;
		}

		public static bool overlapConvexPolygons(FP[] verts1, int offset1, int count1, FP[] verts2,
			int offset2, int count2)
		{
			return overlapConvexPolygons(verts1, offset1, count1, verts2, offset2, count2, ref FPMinimumTranslationVector.Null);
		}

		/** Implementation of the separating axis theorem (SAT) algorithm
		 * @param offset1 offset of verts1
		 * @param count1 count of verts1
		 * @param offset2 offset of verts2
		 * @param count2 count of verts2
		 * @param mtv the minimum translation vector
		 * @param shapesShifted states if shape a and b are shifted. Important for calculating the axis translation for verts1. */
		private static bool overlapsOnAxisOfShape(FP[] verts1, int offset1, int count1, FP[] verts2, int offset2, int count2,
			ref FPMinimumTranslationVector mtv, bool shapesShifted)
		{
			int endA = offset1 + count1;
			int endB = offset2 + count2;
			// get axis of polygon A
			for (int i = offset1; i < endA; i += 2)
			{
				FP x1 = verts1[i];
				FP y1 = verts1[i + 1];
				FP x2 = verts1[(i + 2) % count1];
				FP y2 = verts1[(i + 3) % count1];

				// Get the Axis for the 2 vertices
				FP axisX = y1 - y2;
				FP axisY = -(x1 - x2);

				FP len = FPVector2.len(axisX, axisY);
				// We got a normalized Vector
				axisX /= len;
				axisY /= len;
				FP minA = FP.MAX_VALUE;
				FP maxA = -FP.MAX_VALUE;
				// project shape a on axis
				for (int v = offset1; v < endA; v += 2)
				{
					FP p = verts1[v] * axisX + verts1[v + 1] * axisY;
					minA = FPMath.Min(minA, p);
					maxA = FPMath.Max(maxA, p);
				}

				FP minB = FPMath.MAX_VALUE;
				FP maxB = -FPMath.MAX_VALUE;

				// project shape b on axis
				for (int v = offset2; v < endB; v += 2)
				{
					FP p = verts2[v] * axisX + verts2[v + 1] * axisY;
					minB = FPMath.Min(minB, p);
					maxB = FPMath.Max(maxB, p);
				}
				// There is a gap
				if (maxA < minB || maxB < minA)
				{
					return false;
				}
				else
				{
					if (!mtv.Equals(FPMinimumTranslationVector.Null))
					{
						FP o = FPMath.Min(maxA, maxB) - FPMath.Max(minA, minB);
						bool aContainsB = minA < minB && maxA > maxB;
						bool bContainsA = minB < minA && maxB > maxA;
						// if it contains one or another
						FP mins = 0;
						FP maxs = 0;
						if (aContainsB || bContainsA)
						{
							mins = FPMath.Abs(minA - minB);
							maxs = FPMath.Abs(maxA - maxB);
							o += FPMath.Min(mins, maxs);
						}

						if (mtv.depth > o)
						{
							mtv.depth = o;
							bool condition;
							if (shapesShifted)
							{
								condition = minA < minB;
								axisX = condition ? axisX : -axisX;
								axisY = condition ? axisY : -axisY;
							}
							else
							{
								condition = minA > minB;
								axisX = condition ? axisX : -axisX;
								axisY = condition ? axisY : -axisY;
							}

							if (aContainsB || bContainsA)
							{
								condition = mins > maxs;
								axisX = condition ? axisX : -axisX;
								axisY = condition ? axisY : -axisY;
							}

							mtv.normal.set(axisX, axisY);
						}
					}
				}
			}
			return true;
		}

		private static bool overlapsOnAxisOfShape(FP[] verts1, int offset1, int count1, FP[] verts2,
			int offset2, int count2, bool shapesShifted)
		{
			return overlapsOnAxisOfShape(verts1, offset1, count1, verts2,
			 offset2, count2, ref FPMinimumTranslationVector.Null, shapesShifted);
		}

		/** Splits the triangle by the plane. The result is stored in the SplitTriangle instance. Depending on where the triangle is
		 * relative to the plane, the result can be:
		 *
		 * <ul>
		 * <li>Triangle is fully in front/behind: {@link SplitTriangle#front} or {@link SplitTriangle#back} will contain the original
		 * triangle, {@link SplitTriangle#total} will be one.</li>
		 * <li>Triangle has two vertices in front, one behind: {@link SplitTriangle#front} contains 2 triangles,
		 * {@link SplitTriangle#back} contains 1 triangles, {@link SplitTriangle#total} will be 3.</li>
		 * <li>Triangle has one vertex in front, two behind: {@link SplitTriangle#front} contains 1 triangle,
		 * {@link SplitTriangle#back} contains 2 triangles, {@link SplitTriangle#total} will be 3.</li>
		 * </ul>
		 *
		 * The input triangle should have the form: x, y, z, x2, y2, z2, x3, y3, z3. One can add additional attributes per vertex which
		 * will be interpolated if split, such as texture coordinates or normals. Note that these additional attributes won't be
		 * normalized, as might be necessary in case of normals.
		 * @param split output SplitTriangle */
		public static void splitTriangle(FP[] triangle, FPPlane plane, FPSplitTriangle split)
		{
			int stride = triangle.Length / 3;
			bool r1 = plane.testPoint(triangle[0], triangle[1], triangle[2]) == DGPlaneSide.Back;
			bool r2 = plane.testPoint(triangle[0 + stride], triangle[1 + stride], triangle[2 + stride]) == DGPlaneSide.Back;
			bool r3 = plane.testPoint(triangle[0 + stride * 2], triangle[1 + stride * 2],
				triangle[2 + stride * 2]) == DGPlaneSide.Back;

			split.reset();

			// easy case, triangle is on one side (point on plane means front).
			if (r1 == r2 && r2 == r3)
			{
				split.total = 1;
				if (r1)
				{
					split.numBack = 1;
					Array.Copy(triangle, 0, split.back, 0, triangle.Length);
				}
				else
				{
					split.numFront = 1;
					Array.Copy(triangle, 0, split.front, 0, triangle.Length);
				}
				return;
			}

			// set number of triangles
			split.total = 3;
			split.numFront = (r1 ? 0 : 1) + (r2 ? 0 : 1) + (r3 ? 0 : 1);
			split.numBack = split.total - split.numFront;

			// hard case, split the three edges on the plane
			// determine which array to fill first, front or back, flip if we
			// cross the plane
			split.setSide(!r1);

			// split first edge
			int first = 0;
			int second = stride;
			if (r1 != r2)
			{
				// split the edge
				splitEdge(triangle, first, second, stride, plane, split.edgeSplit, 0);

				// add first edge vertex and new vertex to current side
				split.add(triangle, first, stride);
				split.add(split.edgeSplit, 0, stride);

				// flip side and add new vertex and second edge vertex to current side
				split.setSide(!split.getSide());
				split.add(split.edgeSplit, 0, stride);
			}
			else
			{
				// add both vertices
				split.add(triangle, first, stride);
			}

			// split second edge
			first = stride;
			second = stride + stride;
			if (r2 != r3)
			{
				// split the edge
				splitEdge(triangle, first, second, stride, plane, split.edgeSplit, 0);

				// add first edge vertex and new vertex to current side
				split.add(triangle, first, stride);
				split.add(split.edgeSplit, 0, stride);

				// flip side and add new vertex and second edge vertex to current side
				split.setSide(!split.getSide());
				split.add(split.edgeSplit, 0, stride);
			}
			else
			{
				// add both vertices
				split.add(triangle, first, stride);
			}

			// split third edge
			first = stride + stride;
			second = 0;
			if (r3 != r1)
			{
				// split the edge
				splitEdge(triangle, first, second, stride, plane, split.edgeSplit, 0);

				// add first edge vertex and new vertex to current side
				split.add(triangle, first, stride);
				split.add(split.edgeSplit, 0, stride);

				// flip side and add new vertex and second edge vertex to current side
				split.setSide(!split.getSide());
				split.add(split.edgeSplit, 0, stride);
			}
			else
			{
				// add both vertices
				split.add(triangle, first, stride);
			}

			// triangulate the side with 2 triangles
			if (split.numFront == 2)
			{
				Array.Copy(split.front, stride * 2, split.front, stride * 3, stride * 2);
				Array.Copy(split.front, 0, split.front, stride * 5, stride);
			}
			else
			{
				Array.Copy(split.back, stride * 2, split.back, stride * 3, stride * 2);
				Array.Copy(split.back, 0, split.back, stride * 5, stride);
			}
		}

		static FPVector3 intersection = new FPVector3();

		private static void splitEdge(FP[] vertices, int s, int e, int stride, FPPlane plane, FP[] split, int offset)
		{
			FP t = FPIntersector.intersectLinePlane(vertices[s], vertices[s + 1], vertices[s + 2], vertices[e], vertices[e + 1],
				vertices[e + 2], plane, ref intersection);
			split[offset + 0] = intersection.x;
			split[offset + 1] = intersection.y;
			split[offset + 2] = intersection.z;
			for (int i = 3; i < stride; i++)
			{
				FP a = vertices[s + i];
				FP b = vertices[e + i];
				split[offset + i] = a + t * (b - a);
			}
		}



		/** Returns whether two geometries (defined as an array of vertices) have at least one point of intersection using SAT
		 * (separating axis theorem)
		 *
		 * @param axes - Axes to be tested
		 * @param aVertices - Vertices from geometry A
		 * @param bVertices - Vertices from geometry B
		 *
		 * @return if geometries are intersecting */
		public static bool hasOverlap(FPVector3[] axes, FPVector3[] aVertices, FPVector3[] bVertices)
		{
			for (int i = 0; i < axes.Length; i++)
			{
				FPVector3 axis = axes[i];
				FP minA = FP.MAX_VALUE;
				FP maxA = -FP.MAX_VALUE;
				// project shape a on axis
				for (int m = 0; m < aVertices.Length; m++)
				{
					FPVector3 aVertex = aVertices[m];
					FP p = aVertex.dot(axis);
					minA = FPMath.Min(minA, p);
					maxA = FPMath.Max(maxA, p);
				}

				FP minB = FP.MAX_VALUE;
				FP maxB = -FP.MAX_VALUE;
				// project shape b on axis
				for (int n = 0; n < bVertices.Length; n++)
				{
					FPVector3 bVertex = bVertices[n];
					FP p = bVertex.dot(axis);
					minB = FPMath.Min(minB, p);
					maxB = FPMath.Max(maxB, p);
				}

				if (maxA < minB || maxB < minA)
				{
					// Found an axis so the geometries are not intersecting
					return false;
				}
			}

			return true;
		}
	}
}

