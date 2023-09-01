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
	public partial class DGIntersector
	{

		private static DGVector3 v0 = new DGVector3();
		private static DGVector3 v1 = new DGVector3();
		private static DGVector3 v2 = new DGVector3();
		private static List<DGFixedPoint> floatArray = new List<DGFixedPoint>();
		private static List<DGFixedPoint> floatArray2 = new List<DGFixedPoint>();


		/** Returns whether the given point is inside the triangle. This assumes that the point is on the plane of the triangle. No
		 * check is performed that this is the case. <br>
		 * If the Vector3 parameters contain both small and large values, such as one that contains 0.0001 and one that contains
		 * 10000000.0, this can fail due to floating-point imprecision.
		 * 
		 * @param t1 the first vertex of the triangle
		 * @param t2 the second vertex of the triangle
		 * @param t3 the third vertex of the triangle
		 * @return whether the point is in the triangle */
		public static bool isPointInTriangle(DGVector3 point, DGVector3 t1, DGVector3 t2, DGVector3 t3)
		{
			v0 = v0.set(t1).sub(point);
			v1 = v1.set(t2).sub(point);
			v2 = v2.set(t3).sub(point);

			v1 = v1.crs(v2);
			v2 = v2.crs(v0);

			if (v1.dot(v2) < (DGFixedPoint)0f) return false;
			v0 = v0.crs(v2.set(t2).sub(point));
			return (v1.dot(v0) >= (DGFixedPoint)0f);
		}

		/** Returns true if the given point is inside the triangle. */
		public static bool isPointInTriangle(DGVector2 p, DGVector2 a, DGVector2 b, DGVector2 c)
		{
			return isPointInTriangle(p.x, p.y, a.x, a.y, b.x, b.y, c.x, c.y);
		}

		/** Returns true if the given point is inside the triangle. */
		public static bool isPointInTriangle(DGFixedPoint px, DGFixedPoint py, DGFixedPoint ax, DGFixedPoint ay, DGFixedPoint bx, DGFixedPoint by, DGFixedPoint cx, DGFixedPoint cy)
		{
			DGFixedPoint px1 = px - ax;
			DGFixedPoint py1 = py - ay;
			bool side12 = (bx - ax) * py1 - (by - ay) * px1 > (DGFixedPoint)0;
			if ((cx - ax) * py1 - (cy - ay) * px1 > (DGFixedPoint)0 == side12) return false;
			if ((cx - bx) * (py - by) - (cy - by) * (px - bx) > (DGFixedPoint)0 != side12) return false;
			return true;
		}

		public static bool intersectSegmentPlane(DGVector3 start, DGVector3 end, DGPlane plane, ref DGVector3 intersection)
		{
			DGVector3 dir = v0.set(end).sub(start);
			DGFixedPoint denom = dir.dot(plane.getNormal());
			if (denom == (DGFixedPoint)0f) return false;
			DGFixedPoint t = -(start.dot(plane.getNormal()) + plane.getD()) / denom;
			if (t < (DGFixedPoint)0 || t > (DGFixedPoint)1) return false;

			if (intersection != DGVector3.Null)
				intersection = start.add(dir.scl(t));
			return true;
		}
		public static bool intersectSegmentPlane(DGVector3 start, DGVector3 end, DGPlane plane)
		{
			return intersectSegmentPlane(start, end, plane, ref DGVector3.Null);
		}


		/** Determines on which side of the given line the point is. Returns 1 if the point is on the left side of the line, 0 if the
		 * point is on the line and -1 if the point is on the right side of the line. Left and right are relative to the lines
		 * direction which is linePoint1 to linePoint2. */
		public static int pointLineSide(DGVector2 linePoint1, DGVector2 linePoint2, DGVector2 point)
		{
			return DGMath.Sign(
				(linePoint2.x - linePoint1.x) * (point.y - linePoint1.y) - (linePoint2.y - linePoint1.y) * (point.x - linePoint1.x));
		}

		public static int pointLineSide(DGFixedPoint linePoint1X, DGFixedPoint linePoint1Y, DGFixedPoint linePoint2X, DGFixedPoint linePoint2Y, DGFixedPoint pointX,
			DGFixedPoint pointY)
		{
			return DGMath.Sign((linePoint2X - linePoint1X) * (pointY - linePoint1Y) - (linePoint2Y - linePoint1Y) * (pointX - linePoint1X));
		}

		/** Checks whether the given point is in the polygon.
		 * @param polygon The polygon vertices passed as an array
		 * @return true if the point is in the polygon */
		public static bool isPointInPolygon(List<DGVector2> polygon, DGVector2 point)
		{
			DGVector2 last = polygon[0];
			DGFixedPoint x = point.x, y = point.y;
			bool oddNodes = false;
			for (int i = 0; i < polygon.Count; i++)
			{
				DGVector2 vertex = polygon[i];
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
		public static bool isPointInPolygon(DGFixedPoint[] polygon, int offset, int count, DGFixedPoint x, DGFixedPoint y)
		{
			bool oddNodes = false;
			DGFixedPoint sx = polygon[offset], sy = polygon[offset + 1], y1 = sy;
			int yi = offset + 3;
			for (int n = offset + count; yi < n; yi += 2)
			{
				DGFixedPoint y2 = polygon[yi];
				if ((y2 < y && y1 >= y) || (y1 < y && y2 >= y))
				{
					DGFixedPoint x2 = polygon[yi - 1];
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

		private static DGVector2 ip = new DGVector2();
		private static DGVector2 ep1 = new DGVector2();
		private static DGVector2 ep2 = new DGVector2();
		private static DGVector2 s = new DGVector2();
		private static DGVector2 e = new DGVector2();

		/** Intersects two convex polygons with clockwise vertices and sets the overlap polygon resulting from the intersection.
		 * Follows the Sutherland-Hodgman algorithm.
		 * @param p1 The polygon that is being clipped
		 * @param p2 The clip polygon
		 * @param overlap The intersection of the two polygons (can be null, if an intersection polygon is not needed)
		 * @return Whether the two polygons intersect. */
		public static bool intersectPolygons(DGPolygon p1, DGPolygon p2, DGPolygon overlap = null)
		{
			if (p1.getVertices().Length == 0 || p2.getVertices().Length == 0)
			{
				return false;
			}
			DGVector2 ip = DGIntersector.ip, ep1 = DGIntersector.ep1, ep2 = DGIntersector.ep2, s = DGIntersector.s, e = DGIntersector.e;
			List<DGFixedPoint> floatArray = DGIntersector.floatArray, floatArray2 = DGIntersector.floatArray2;
			floatArray.Clear();
			floatArray2.Clear();
			floatArray2.AddRange(p1.getTransformedVertices());
			DGFixedPoint[] vertices2 = p2.getTransformedVertices();
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
					bool side = DGIntersector.pointLineSide(ep2, ep1, s) > 0;
					if (DGIntersector.pointLineSide(ep2, ep1, e) > 0)
					{
						if (!side)
						{
							DGIntersector.intersectLines(s, e, ep1, ep2, ref ip);
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
						DGIntersector.intersectLines(s, e, ep1, ep2, ref ip);
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
		public static bool intersectPolygons(List<DGFixedPoint> polygon1, List<DGFixedPoint> polygon2)
		{
			var polygon1Array = polygon1.ToArray();
			var polygon2Array = polygon2.ToArray();
			if (DGIntersector.isPointInPolygon(polygon1Array, 0, polygon1.Count, polygon2Array[0], polygon2Array[1])) return true;
			if (DGIntersector.isPointInPolygon(polygon2Array, 0, polygon2.Count, polygon1Array[0], polygon1Array[1])) return true;
			return intersectPolygonEdges(polygon1, polygon2);
		}

		/** Returns true if the lines of the specified poygons intersect. */
		public static bool intersectPolygonEdges(List<DGFixedPoint> polygon1, List<DGFixedPoint> polygon2)
		{
			int last1 = polygon1.Count - 2, last2 = polygon2.Count - 2;
			DGFixedPoint[] p1 = polygon1.ToArray(), p2 = polygon2.ToArray();
			DGFixedPoint x1 = p1[last1], y1 = p1[last1 + 1];
			for (int i = 0; i <= last1; i += 2)
			{
				DGFixedPoint x2 = p1[i], y2 = p1[i + 1];
				DGFixedPoint x3 = p2[last2], y3 = p2[last2 + 1];
				for (int j = 0; j <= last2; j += 2)
				{
					DGFixedPoint x4 = p2[j], y4 = p2[j + 1];
					if (intersectSegments(x1, y1, x2, y2, x3, y3, x4, y4)) return true;
					x3 = x4;
					y3 = y4;
				}
				x1 = x2;
				y1 = y2;
			}
			return false;
		}

		static DGVector2 v2a = new DGVector2();
		static DGVector2 v2b = new DGVector2();
		static DGVector2 v2c = new DGVector2();
		static DGVector2 v2d = new DGVector2();

		/** Returns the distance between the given line and point. Note the specified line is not a line segment. */
		public static DGFixedPoint distanceLinePoint(DGFixedPoint startX, DGFixedPoint startY, DGFixedPoint endX, DGFixedPoint endY, DGFixedPoint pointX, DGFixedPoint pointY)
		{
			DGFixedPoint normalLength = DGVector2.len(endX - startX, endY - startY);
			return DGMath.Abs((pointX - startX) * (endY - startY) - (pointY - startY) * (endX - startX)) / normalLength;
		}

		/** Returns the distance between the given segment and point. */
		public static DGFixedPoint distanceSegmentPoint(DGFixedPoint startX, DGFixedPoint startY, DGFixedPoint endX, DGFixedPoint endY, DGFixedPoint pointX, DGFixedPoint pointY)
		{
			return nearestSegmentPoint(startX, startY, endX, endY, pointX, pointY, ref v2a).dst(pointX, pointY);
		}

		/** Returns the distance between the given segment and point. */
		public static DGFixedPoint distanceSegmentPoint(DGVector2 start, DGVector2 end, DGVector2 point)
		{
			return nearestSegmentPoint(start, end, point, ref v2a).dst(point);
		}

		/** Returns a point on the segment nearest to the specified point. */
		public static DGVector2 nearestSegmentPoint(DGVector2 start, DGVector2 end, DGVector2 point, ref DGVector2 nearest)
		{
			DGFixedPoint length2 = start.dst2(end);
			if (length2 == (DGFixedPoint)0) return nearest.set(start);
			DGFixedPoint t = ((point.x - start.x) * (end.x - start.x) + (point.y - start.y) * (end.y - start.y)) / length2;
			if (t <= (DGFixedPoint)0) return nearest.set(start);
			if (t >= (DGFixedPoint)1) return nearest.set(end);
			return nearest.set(start.x + t * (end.x - start.x), start.y + t * (end.y - start.y));
		}

		/** Returns a point on the segment nearest to the specified point. */
		public static DGVector2 nearestSegmentPoint(DGFixedPoint startX, DGFixedPoint startY, DGFixedPoint endX, DGFixedPoint endY, DGFixedPoint pointX, DGFixedPoint pointY,
			ref DGVector2 nearest)
		{
			DGFixedPoint xDiff = endX - startX;
			DGFixedPoint yDiff = endY - startY;
			DGFixedPoint length2 = xDiff * xDiff + yDiff * yDiff;
			if (length2 == (DGFixedPoint)0) return nearest.set(startX, startY);
			DGFixedPoint t = ((pointX - startX) * (endX - startX) + (pointY - startY) * (endY - startY)) / length2;
			if (t <= (DGFixedPoint)0) return nearest.set(startX, startY);
			if (t >= (DGFixedPoint)1) return nearest.set(endX, endY);
			return nearest.set(startX + t * (endX - startX), startY + t * (endY - startY));
		}

		/** Returns whether the given line segment intersects the given circle.
		 * @param start The start point of the line segment
		 * @param end The end point of the line segment
		 * @param center The center of the circle
		 * @param squareRadius The squared radius of the circle
		 * @return Whether the line segment and the circle intersect */
		public static bool intersectSegmentCircle(DGVector2 start, DGVector2 end, DGVector2 center, DGFixedPoint squareRadius)
		{
			tmp = tmp.set(end.x - start.x, end.y - start.y, (DGFixedPoint)0);
			tmp1 = tmp1.set(center.x - start.x, center.y - start.y, (DGFixedPoint)0);
			DGFixedPoint l = tmp.len();
			DGFixedPoint u = tmp1.dot(tmp.nor());
			if (u <= (DGFixedPoint)0)
			{
				tmp2.set(start.x, start.y, (DGFixedPoint)0);
			}
			else if (u >= l)
			{
				tmp2.set(end.x, end.y, (DGFixedPoint)0);
			}
			else
			{
				tmp3.set(tmp.scl(u)); // remember tmp is already normalized
				tmp2.set(tmp3.x + start.x, tmp3.y + start.y, (DGFixedPoint)0);
			}

			DGFixedPoint x = center.x - tmp2.x;
			DGFixedPoint y = center.y - tmp2.y;

			return x * x + y * y <= squareRadius;
		}

		/** Returns whether the given line segment intersects the given circle.
		 * @param start The start point of the line segment
		 * @param end The end point of the line segment
		 * @param circle The circle
		 * @param mtv A Minimum Translation Vector to fill in the case of a collision, or null (optional).
		 * @return Whether the line segment and the circle intersect */
		public static bool intersectSegmentCircle(DGVector2 start, DGVector2 end, DGCircle circle, ref DGMinimumTranslationVector mtv)
		{
			v2a = v2a.set(end).sub(start);
			v2b = v2b.set(circle.x - start.x, circle.y - start.y);
			DGFixedPoint len = v2a.len();
			DGFixedPoint u = v2b.dot(v2a.nor());
			if (u <= (DGFixedPoint)0)
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

			if (!mtv.Equals(DGMinimumTranslationVector.Null))
			{
				// Handle special case of segment containing circle center
				if (v2a.Equals(DGVector2.Zero))
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
		public static bool intersectFrustumBounds(DGFrustum frustum, DGBoundingBox bounds)
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
				DGVector3 point = frustum.planePoints[i];
				frustumIsInsideBounds |= bounds.contains(point);
			}
			return frustumIsInsideBounds;
		}

		/** Returns whether the given {@link Frustum} intersects a {@link OrientedBoundingBox}.
		 * @param frustum The frustum
		 * @param obb The oriented bounding box
		 * @return Whether the frustum intersects the oriented bounding box */
		public static bool intersectFrustumBounds(DGFrustum frustum, DGOrientedBoundingBox obb)
		{

			bool boundsIntersectsFrustum = false;
			var obbVectors = obb.getVertices();
			for (int i = 0; i < obbVectors.Length; i++)
			{
				DGVector3 v = obbVectors[i];
				boundsIntersectsFrustum |= frustum.pointInFrustum(v);
			}

			if (boundsIntersectsFrustum)
			{
				return true;
			}

			bool frustumIsInsideBounds = false;
			for (int i = 0; i < frustum.planePoints.Length; i++)
			{
				DGVector3 point = frustum.planePoints[i];
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
		public static DGFixedPoint intersectRayRay(DGVector2 start1, DGVector2 direction1, DGVector2 start2, DGVector2 direction2)
		{
			DGFixedPoint difx = start2.x - start1.x;
			DGFixedPoint dify = start2.y - start1.y;
			DGFixedPoint d1xd2 = direction1.x * direction2.y - direction1.y * direction2.x;
			if (d1xd2 == (DGFixedPoint)0.0f)
			{
				return DGFixedPoint.MaxValue; // collinear
			}
			DGFixedPoint d2sx = direction2.x / d1xd2;
			DGFixedPoint d2sy = direction2.y / d1xd2;
			return difx * d2sy - dify * d2sx;
		}

		/** Intersects a {@link Ray} and a {@link Plane}. The intersection point is stored in intersection in case an intersection is
		 * present.
		 * @param intersection The vector the intersection point is written to (optional)
		 * @return Whether an intersection is present. */
		public static bool intersectRayPlane(DGRay ray, DGPlane plane, ref DGVector3 intersection)
		{
			DGFixedPoint denom = ray.direction.dot(plane.getNormal());
			if (denom != (DGFixedPoint)0)
			{
				DGFixedPoint t = -(ray.origin.dot(plane.getNormal()) + plane.getD()) / denom;
				if (t < (DGFixedPoint)0) return false;

				if (intersection != DGVector3.Null)
					intersection = ray.origin.add(v0.set(ray.direction).scl(t));
				return true;
			}
			else if (plane.testPoint(ray.origin) == DGPlaneSide.OnPlane)
			{
				if (intersection != DGVector3.Null)
					intersection = ray.origin;
				return true;
			}
			else
				return false;
		}

		public static bool intersectRayPlane(DGRay ray, DGPlane plane)
		{
			return intersectRayPlane(ray, plane, ref DGVector3.Null);
		}

		/** Intersects a line and a plane. The intersection is returned as the distance from the first point to the plane. In case an
		 * intersection happened, the return value is in the range [0,1]. The intersection point can be recovered by point1 + t *
		 * (point2 - point1) where t is the return value of this method. */
		public static DGFixedPoint intersectLinePlane(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint x2, DGFixedPoint y2, DGFixedPoint z2, DGPlane plane,
			ref DGVector3 intersection)
		{
			DGVector3 direction = tmp.set(x2, y2, z2).sub(x, y, z);
			DGVector3 origin = tmp2.set(x, y, z);
			DGFixedPoint denom = direction.dot(plane.getNormal());
			if (denom != (DGFixedPoint)0)
			{
				DGFixedPoint t = -(origin.dot(plane.getNormal()) + plane.getD()) / denom;
				if (intersection != DGVector3.Null)
					intersection = origin.add(direction.scl(t));
				return t;
			}
			else if (plane.testPoint(origin) == DGPlaneSide.OnPlane)
			{
				if (intersection != DGVector3.Null)
					intersection = origin;
				return (DGFixedPoint)0;
			}

			return (DGFixedPoint)(-1);
		}

		public static DGFixedPoint intersectLinePlane(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint x2,
			DGFixedPoint y2, DGFixedPoint z2, DGPlane plane)
		{
			return intersectLinePlane(x, y, z, x2, y2, z2, plane, ref DGVector3.Null);
		}

		/** Returns true if the three {@link Plane planes} intersect, setting the point of intersection in {@code intersection}, if
		 * any.
		 * @param intersection The point where the three planes intersect */
		public static bool intersectPlanes(DGPlane a, DGPlane b, DGPlane c, ref DGVector3 intersection)
		{
			tmp1 = tmp1.set(a.normal).crs(b.normal);
			tmp2 = tmp2.set(b.normal).crs(c.normal);
			tmp3 = tmp3.set(c.normal).crs(a.normal);

			DGFixedPoint f = -a.normal.dot(tmp2);
			if (DGMath.Abs(f) < DGMath.Epsilon)
			{
				return false;
			}

			if (intersection != DGVector3.Null)
			{
				tmp1 = tmp1.scl(c.d);
				tmp2 = tmp2.scl(a.d);
				tmp3 = tmp3.scl(b.d);

				intersection.set(tmp1.x + tmp2.x + tmp3.x, tmp1.y + tmp2.y + tmp3.y, tmp1.z + tmp2.z + tmp3.z);
				intersection.scl((DGFixedPoint)1 / f);
			}
			return true;
		}

		public static bool intersectPlanes(DGPlane a, DGPlane b, DGPlane c)
		{
			return intersectPlanes(a, b, c, ref DGVector3.Null);
		}

		private static DGPlane p = new DGPlane(default, default(DGFixedPoint));
		private static DGVector3 i = new DGVector3();

		/** Intersect a {@link Ray} and a triangle, returning the intersection point in intersection.
		 * @param t1 The first vertex of the triangle
		 * @param t2 The second vertex of the triangle
		 * @param t3 The third vertex of the triangle
		 * @param intersection The intersection point (optional)
		 * @return True in case an intersection is present. */
		public static bool intersectRayTriangle(DGRay ray, DGVector3 t1, DGVector3 t2, DGVector3 t3, ref DGVector3 intersection)
		{
			DGVector3 edge1 = v0.set(t2).sub(t1);
			DGVector3 edge2 = v1.set(t3).sub(t1);

			DGVector3 pvec = v2.set(ray.direction).crs(edge2);
			DGFixedPoint det = edge1.dot(pvec);
			if (DGMath.IsZero(det))
			{
				p.set(t1, t2, t3);
				if (p.testPoint(ray.origin) == DGPlaneSide.OnPlane && DGIntersector.isPointInTriangle(ray.origin, t1, t2, t3))
				{
					if (intersection != DGVector3.Null)
						intersection = ray.origin;
					return true;
				}
				return false;
			}

			det = (DGFixedPoint)1.0f / det;

			DGVector3 tvec = i.set(ray.origin).sub(t1);
			DGFixedPoint u = tvec.dot(pvec) * det;
			if (u < (DGFixedPoint)0.0f || u > (DGFixedPoint)1.0f) return false;

			DGVector3 qvec = tvec.crs(edge1);
			DGFixedPoint v = ray.direction.dot(qvec) * det;
			if (v < (DGFixedPoint)0.0f || u + v > (DGFixedPoint)1.0f) return false;

			DGFixedPoint t = edge2.dot(qvec) * det;
			if (t < (DGFixedPoint)0) return false;

			if (intersection != DGVector3.Null)
			{
				intersection = t <= DGMath.Epsilon ? ray.origin : ray.getEndPoint(t);
			}

			return true;
		}

		public static bool intersectRayTriangle(DGRay ray, DGVector3 t1, DGVector3 t2, DGVector3 t3)
		{
			return intersectRayTriangle(ray, t1, t2, t3, ref DGVector3.Null);
		}

		private static DGVector3 dir = new DGVector3();
		private static DGVector3 start = new DGVector3();

		/** Intersects a {@link Ray} and a sphere, returning the intersection point in intersection.
		 * @param ray The ray, the direction component must be normalized before calling this method
		 * @param center The center of the sphere
		 * @param radius The radius of the sphere
		 * @param intersection The intersection point (optional, can be null)
		 * @return Whether an intersection is present. */
		public static bool intersectRaySphere(DGRay ray, DGVector3 center, DGFixedPoint radius, ref DGVector3 intersection)
		{
			DGFixedPoint len = ray.direction.dot(center.x - ray.origin.x, center.y - ray.origin.y, center.z - ray.origin.z);
			if (len < (DGFixedPoint)0.0f) // behind the ray
				return false;
			DGFixedPoint dst2 = center.dst2(ray.origin.x + ray.direction.x * len, ray.origin.y + ray.direction.y * len,
				ray.origin.z + ray.direction.z * len);
			DGFixedPoint r2 = radius * radius;
			if (dst2 > r2) return false;
			if (intersection != DGVector3.Null)
				intersection = ray.direction.scl(len - DGMath.Sqrt(r2 - dst2)).add(ray.origin);
			return true;
		}

		public static bool intersectRaySphere(DGRay ray, DGVector3 center, DGFixedPoint radius)
		{
			return intersectRaySphere(ray, center, radius, ref DGVector3.Null);
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
		public static bool intersectRayBounds(DGRay ray, DGBoundingBox box, ref DGVector3 intersection)
		{
			if (box.contains(ray.origin))
			{
				if (intersection != DGVector3.Null)
					intersection = ray.origin;
				return true;
			}
			DGFixedPoint lowest = (DGFixedPoint)0, t;
			bool hit = false;

			// min x
			if (ray.origin.x <= box.min.x && ray.direction.x > (DGFixedPoint)0)
			{
				t = (box.min.x - ray.origin.x) / ray.direction.x;
				if (t >= (DGFixedPoint)0)
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
			if (ray.origin.x >= box.max.x && ray.direction.x < (DGFixedPoint)0)
			{
				t = (box.max.x - ray.origin.x) / ray.direction.x;
				if (t >= (DGFixedPoint)0)
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
			if (ray.origin.y <= box.min.y && ray.direction.y > (DGFixedPoint)0)
			{
				t = (box.min.y - ray.origin.y) / ray.direction.y;
				if (t >= (DGFixedPoint)0)
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
			if (ray.origin.y >= box.max.y && ray.direction.y < (DGFixedPoint)0)
			{
				t = (box.max.y - ray.origin.y) / ray.direction.y;
				if (t >= (DGFixedPoint)0)
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
			if (ray.origin.z <= box.min.z && ray.direction.z > (DGFixedPoint)0)
			{
				t = (box.min.z - ray.origin.z) / ray.direction.z;
				if (t >= (DGFixedPoint)0)
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
			if (ray.origin.z >= box.max.z && ray.direction.z < (DGFixedPoint)0)
			{
				t = (box.max.z - ray.origin.z) / ray.direction.z;
				if (t >= (DGFixedPoint)0)
				{
					v2 = v2.set(ray.direction).scl(t).add(ray.origin);
					if (v2.x >= box.min.x && v2.x <= box.max.x && v2.y >= box.min.y && v2.y <= box.max.y && (!hit || t < lowest))
					{
						hit = true;
						lowest = t;
					}
				}
			}
			if (hit && intersection != DGVector3.Null)
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

		public static bool intersectRayBounds(DGRay ray, DGBoundingBox box)
		{
			return intersectRayBounds(ray, box, ref DGVector3.Null);
		}

		/** Quick check whether the given {@link Ray} and {@link BoundingBox} intersect.
		 * @return Whether the ray and the bounding box intersect. */
		public static bool intersectRayBoundsFast(DGRay ray, DGBoundingBox box)
		{
			return intersectRayBoundsFast(ray, box.getCenter(ref tmp1), box.getDimensions(ref tmp2));
		}

		/** Quick check whether the given {@link Ray} and {@link BoundingBox} intersect.
		 * @param center The center of the bounding box
		 * @param dimensions The dimensions (width, height and depth) of the bounding box
		 * @return Whether the ray and the bounding box intersect. */
		public static bool intersectRayBoundsFast(DGRay ray, DGVector3 center, DGVector3 dimensions)
		{
			DGFixedPoint divX = (DGFixedPoint)1f / ray.direction.x;
			DGFixedPoint divY = (DGFixedPoint)1f / ray.direction.y;
			DGFixedPoint divZ = (DGFixedPoint)1f / ray.direction.z;

			DGFixedPoint minx = ((center.x - dimensions.x * (DGFixedPoint)0.5f) - ray.origin.x) * divX;
			DGFixedPoint maxx = ((center.x + dimensions.x * (DGFixedPoint)0.5f) - ray.origin.x) * divX;
			if (minx > maxx)
			{
				DGFixedPoint t = minx;
				minx = maxx;
				maxx = t;
			}

			DGFixedPoint miny = ((center.y - dimensions.y * (DGFixedPoint)0.5f) - ray.origin.y) * divY;
			DGFixedPoint maxy = ((center.y + dimensions.y * (DGFixedPoint)0.5f) - ray.origin.y) * divY;
			if (miny > maxy)
			{
				DGFixedPoint t = miny;
				miny = maxy;
				maxy = t;
			}

			DGFixedPoint minz = ((center.z - dimensions.z * (DGFixedPoint)0.5f) - ray.origin.z) * divZ;
			DGFixedPoint maxz = ((center.z + dimensions.z * (DGFixedPoint)0.5f) - ray.origin.z) * divZ;
			if (minz > maxz)
			{
				DGFixedPoint t = minz;
				minz = maxz;
				maxz = t;
			}

			DGFixedPoint min = DGMath.Max(DGMath.Max(minx, miny), minz);
			DGFixedPoint max = DGMath.Min(DGMath.Min(maxx, maxy), maxz);

			return max >= (DGFixedPoint)0 && max >= min;
		}

		/** Check whether the given {@link Ray} and {@link OrientedBoundingBox} intersect.
		 *
		 * @return Whether the ray and the oriented bounding box intersect. */
		public static bool intersectRayOrientedBoundsFast(DGRay ray, DGOrientedBoundingBox obb)
		{
			return intersectRayOrientedBounds(ray, obb);
		}

		/** Check whether the given {@link Ray} and Oriented {@link BoundingBox} intersect.
		 * @param transform - the BoundingBox transformation
		 *
		 * @return Whether the ray and the oriented bounding box intersect. */
		public static bool intersectRayOrientedBoundsFast(DGRay ray, DGBoundingBox bounds, DGMatrix4x4 transform)
		{
			return intersectRayOrientedBounds(ray, bounds, transform);
		}

		/** Check whether the given {@link Ray} and {@link OrientedBoundingBox} intersect.
		 *
		 * @param intersection The intersection point (optional)
		 * @return Whether an intersection is present. */
		public static bool intersectRayOrientedBounds(DGRay ray, DGOrientedBoundingBox obb, ref DGVector3 intersection)
		{
			DGBoundingBox bounds = obb.getBounds();
			DGMatrix4x4 transform = obb.getTransform();
			return intersectRayOrientedBounds(ray, bounds, transform, ref intersection);
		}

		public static bool intersectRayOrientedBounds(DGRay ray, DGOrientedBoundingBox obb)
		{
			return intersectRayOrientedBounds(ray, obb, ref DGVector3.Null);
		}

		/** Check whether the given {@link Ray} and {@link OrientedBoundingBox} intersect.
		 *
		 * Based on code at: https://github.com/opengl-tutorials/ogl/blob/master/misc05_picking/misc05_picking_custom.cpp#L83
		 * @param intersection The intersection point (optional)
		 * @return Whether an intersection is present. */
		public static bool intersectRayOrientedBounds(DGRay ray, DGBoundingBox bounds, DGMatrix4x4 transform, ref DGVector3 intersection)
		{
			DGFixedPoint tMin = (DGFixedPoint)0.0f;
			DGFixedPoint tMax = DGFixedPoint.MaxValue;
			DGFixedPoint t1, t2;

			DGVector3 oBBposition = transform.getTranslation(ref tmp);
			DGVector3 delta = oBBposition.sub(ray.origin);

			// Test intersection with the 2 planes perpendicular to the OBB's X axis
			tmp1 = tmp1.set(transform.m00, transform.m10, transform.m20);
			DGVector3 xaxis = tmp1;
			DGFixedPoint e = xaxis.dot(delta);
			DGFixedPoint f = ray.direction.dot(xaxis);

			if (DGMath.Abs(f) > DGMath.Epsilon)
			{ // Standard case
				t1 = (e + bounds.min.x) / f; // Intersection with the "left" plane
				t2 = (e + bounds.max.x) / f; // Intersection with the "right" plane
											 // t1 and t2 now contain distances between ray origin and ray-plane intersections

				// We want t1 to represent the nearest intersection,
				// so if it's not the case, invert t1 and t2
				if (t1 > t2)
				{
					DGFixedPoint w = t1;
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
			else if (-e + bounds.min.x > (DGFixedPoint)0.0f || -e + bounds.max.x < (DGFixedPoint)0.0f)
			{
				return false;
			}

			// Test intersection with the 2 planes perpendicular to the OBB's Y axis
			// Exactly the same thing than above.
			tmp2 = tmp2.set(transform.m01, transform.m11, transform.m21);
			DGVector3 yaxis = tmp2;


			e = yaxis.dot(delta);
			f = ray.direction.dot(yaxis);

			if (DGMath.Abs(f) > DGMath.Epsilon)
			{
				t1 = (e + bounds.min.y) / f;
				t2 = (e + bounds.max.y) / f;

				if (t1 > t2)
				{
					DGFixedPoint w = t1;
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
			else if (-e + bounds.min.y > (DGFixedPoint)0.0f || -e + bounds.max.y < (DGFixedPoint)0.0f)
			{
				return false;
			}

			// Test intersection with the 2 planes perpendicular to the OBB's Z axis
			// Exactly the same thing than above.
			tmp3 = tmp3.set(transform.m02, transform.m12, transform.m22);
			DGVector3 zaxis = tmp3;


			e = zaxis.dot(delta);
			f = ray.direction.dot(zaxis);

			if (DGMath.Abs(f) > DGMath.Epsilon)
			{
				t1 = (e + bounds.min.z) / f;
				t2 = (e + bounds.max.z) / f;

				if (t1 > t2)
				{
					DGFixedPoint w = t1;
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
			else if (-e + bounds.min.z > (DGFixedPoint)0.0f || -e + bounds.max.z < (DGFixedPoint)0.0f)
			{
				return false;
			}

			if (intersection != DGVector3.Null)
			{
				intersection = ray.getEndPoint(tMin);
			}

			return true;
		}

		public static bool intersectRayOrientedBounds(DGRay ray, DGBoundingBox bounds, DGMatrix4x4 transform)
		{
			return intersectRayOrientedBounds(ray, bounds, transform, ref DGVector3.Null);
		}

		static DGVector3 best = new DGVector3();
		static DGVector3 tmp = new DGVector3();
		static DGVector3 tmp1 = new DGVector3();
		static DGVector3 tmp2 = new DGVector3();
		static DGVector3 tmp3 = new DGVector3();

		/** Intersects the given ray with list of triangles. Returns the nearest intersection point in intersection
		 * @param triangles The triangles, each successive 9 elements are the 3 vertices of a triangle, a vertex is made of 3
		 *           successive floats (XYZ)
		 * @param intersection The nearest intersection point (optional)
		 * @return Whether the ray and the triangles intersect. */
		public static bool intersectRayTriangles(DGRay ray, DGFixedPoint[] triangles, ref DGVector3 intersection)
		{
			DGFixedPoint min_dist = DGFixedPoint.MaxValue;
			bool hit = false;

			if (triangles.Length % 9 != 0) throw new Exception("triangles array size is not a multiple of 9");

			for (int i = 0; i < triangles.Length; i += 9)
			{
				bool result = intersectRayTriangle(ray, tmp1.set(triangles[i], triangles[i + 1], triangles[i + 2]),
					tmp2.set(triangles[i + 3], triangles[i + 4], triangles[i + 5]),
					tmp3.set(triangles[i + 6], triangles[i + 7], triangles[i + 8]), ref tmp);

				if (result)
				{
					DGFixedPoint dist = ray.origin.dst2(tmp);
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
				if (intersection != DGVector3.Null)
					intersection = best;
				return true;
			}
		}

		public static bool intersectRayTriangles(DGRay ray, DGFixedPoint[] triangles)
		{
			return intersectRayTriangles(ray, triangles, ref DGVector3.Null);
		}

		/** Intersects the given ray with list of triangles. Returns the nearest intersection point in intersection
		 * @param indices the indices, each successive 3 shorts index the 3 vertices of a triangle
		 * @param vertexSize the size of a vertex in floats
		 * @param intersection The nearest intersection point (optional)
		 * @return Whether the ray and the triangles intersect. */
		public static bool intersectRayTriangles(DGRay ray, DGFixedPoint[] vertices, short[] indices, int vertexSize,
			ref DGVector3 intersection)
		{
			DGFixedPoint min_dist = DGFixedPoint.MaxValue;
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
					DGFixedPoint dist = ray.origin.dst2(tmp);
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
				if (intersection != DGVector3.Null)
					intersection = best;
				return true;
			}
		}

		public static bool intersectRayTriangles(DGRay ray, DGFixedPoint[] vertices, short[] indices, int vertexSize)
		{
			return intersectRayTriangles(ray, vertices, indices, vertexSize, ref DGVector3.Null);
		}

		/** Intersects the given ray with list of triangles. Returns the nearest intersection point in intersection
		 * @param triangles The triangles, each successive 3 elements are the 3 vertices of a triangle
		 * @param intersection The nearest intersection point (optional)
		 * @return Whether the ray and the triangles intersect. */
		public static bool intersectRayTriangles(DGRay ray, List<DGVector3> triangles, ref DGVector3 intersection)
		{
			DGFixedPoint min_dist = DGFixedPoint.MaxValue;
			bool hit = false;

			if (triangles.Count % 3 != 0) throw new Exception("triangle list size is not a multiple of 3");

			for (int i = 0; i < triangles.Count; i += 3)
			{
				bool result = intersectRayTriangle(ray, triangles[i], triangles[i + 1], triangles[i + 2], ref tmp);

				if (result)
				{
					DGFixedPoint dist = ray.origin.dst2(tmp);
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
				if (intersection != DGVector3.Null)
					intersection = best;
				return true;
			}
		}

		public static bool intersectRayTriangles(DGRay ray, List<DGVector3> triangles)
		{
			return intersectRayTriangles(ray, triangles, ref DGVector3.Null);
		}

		/** Quick check whether the given {@link BoundingBox} and {@link Plane} intersect.
		 * @return Whether the bounding box and the plane intersect. */
		public static bool intersectBoundsPlaneFast(DGBoundingBox box, DGPlane plane)
		{
			return intersectBoundsPlaneFast(box.getCenter(ref tmp1), box.getDimensions(ref tmp2).scl((DGFixedPoint)0.5f), plane.normal, plane.d);
		}

		/** Quick check whether the given bounding box and a plane intersect. Code adapted from Christer Ericson's Real Time Collision
		 * @param center The center of the bounding box
		 * @param halfDimensions Half of the dimensions (width, height and depth) of the bounding box
		 * @param normal The normal of the plane
		 * @param distance The distance of the plane
		 * @return Whether the bounding box and the plane intersect. */
		public static bool intersectBoundsPlaneFast(DGVector3 center, DGVector3 halfDimensions, DGVector3 normal, DGFixedPoint distance)
		{
			// Compute the projection interval radius of b onto L(t) = b.c + t * p.n
			DGFixedPoint radius = halfDimensions.x * DGMath.Abs(normal.x) + halfDimensions.y * DGMath.Abs(normal.y)
				+ halfDimensions.z * DGMath.Abs(normal.z);

			// Compute distance of box center from plane
			DGFixedPoint s = normal.dot(center) - distance;

			// Intersection occurs when plane distance falls within [-r,+r] interval
			return DGMath.Abs(s) <= radius;
		}

		/** Intersects the two lines and returns the intersection point in intersection.
		 * @param p1 The first point of the first line
		 * @param p2 The second point of the first line
		 * @param p3 The first point of the second line
		 * @param p4 The second point of the second line
		 * @param intersection The intersection point. May be null.
		 * @return Whether the two lines intersect */
		public static bool intersectLines(DGVector2 p1, DGVector2 p2, DGVector2 p3, DGVector2 p4, ref DGVector2 intersection)
		{
			return intersectLines(p1.x, p1.y, p2.x, p2.y, p3.x, p3.y, p4.x, p4.y, ref intersection);
		}

		public static bool intersectLines(DGVector2 p1, DGVector2 p2, DGVector2 p3, DGVector2 p4)
		{
			return intersectLines(p1, p2, p3, p4, ref DGVector2.Null);
		}


		/** Intersects the two lines and returns the intersection point in intersection.
		 * @param intersection The intersection point, or null.
		 * @return Whether the two lines intersect */
		public static bool intersectLines(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2, DGFixedPoint x3, DGFixedPoint y3, DGFixedPoint x4, DGFixedPoint y4,
			ref DGVector2 intersection)
		{
			DGFixedPoint d = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
			if (d == (DGFixedPoint)0) return false;

			if (intersection != DGVector2.Null)
			{
				DGFixedPoint ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / d;
				intersection = new DGVector2(x1 + (x2 - x1) * ua, y1 + (y2 - y1) * ua);
			}
			return true;
		}

		public static bool intersectLines(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2,
			DGFixedPoint x3, DGFixedPoint y3, DGFixedPoint x4, DGFixedPoint y4)
		{
			return intersectLines(x1, y1, x2, y2, x3, y3, x4, y4, ref DGVector2.Null);
		}

		/** Check whether the given line and {@link Polygon} intersect.
		 * @param p1 The first point of the line
		 * @param p2 The second point of the line
		 * @return Whether polygon and line intersects */
		public static bool intersectLinePolygon(DGVector2 p1, DGVector2 p2, DGPolygon polygon)
		{
			DGFixedPoint[] vertices = polygon.getTransformedVertices();
			DGFixedPoint x1 = p1.x, y1 = p1.y, x2 = p2.x, y2 = p2.y;
			int n = vertices.Length;
			DGFixedPoint x3 = vertices[n - 2], y3 = vertices[n - 1];
			for (int i = 0; i < n; i += 2)
			{
				DGFixedPoint x4 = vertices[i], y4 = vertices[i + 1];
				DGFixedPoint d = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
				if (d != (DGFixedPoint)0)
				{
					DGFixedPoint yd = y1 - y3;
					DGFixedPoint xd = x1 - x3;
					DGFixedPoint ua = ((x4 - x3) * yd - (y4 - y3) * xd) / d;
					if (ua >= (DGFixedPoint)0 && ua <= (DGFixedPoint)1)
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
		public static bool intersectRectangles(DGRectangle rectangle1, DGRectangle rectangle2, ref DGRectangle intersection)
		{
			if (rectangle1.overlaps(rectangle2))
			{
				if (!intersection.Equals(DGRectangle.Null))
				{
					intersection.x = DGMath.Max(rectangle1.x, rectangle2.x);
					intersection.width = DGMath.Min(rectangle1.x + rectangle1.width, rectangle2.x + rectangle2.width) - intersection.x;
					intersection.y = DGMath.Max(rectangle1.y, rectangle2.y);
					intersection.height = DGMath.Min(rectangle1.y + rectangle1.height, rectangle2.y + rectangle2.height) - intersection.y;
				}
				return true;
			}
			return false;
		}

		public static bool intersectRectangles(DGRectangle rectangle1, DGRectangle rectangle2)
		{
			return intersectRectangles(rectangle1, rectangle2, ref DGRectangle.Null);
		}

		/** Determines whether the given rectangle and segment intersect
		 * @param startX x-coordinate start of line segment
		 * @param startY y-coordinate start of line segment
		 * @param endX y-coordinate end of line segment
		 * @param endY y-coordinate end of line segment
		 * @param rectangle rectangle that is being tested for collision
		 * @return whether the rectangle intersects with the line segment */
		public static bool intersectSegmentRectangle(DGFixedPoint startX, DGFixedPoint startY, DGFixedPoint endX, DGFixedPoint endY, DGRectangle rectangle)
		{
			DGFixedPoint rectangleEndX = rectangle.x + rectangle.width;
			DGFixedPoint rectangleEndY = rectangle.y + rectangle.height;

			if (intersectSegments(startX, startY, endX, endY, rectangle.x, rectangle.y, rectangle.x, rectangleEndY)) return true;

			if (intersectSegments(startX, startY, endX, endY, rectangle.x, rectangle.y, rectangleEndX, rectangle.y)) return true;

			if (intersectSegments(startX, startY, endX, endY, rectangleEndX, rectangle.y, rectangleEndX, rectangleEndY))
				return true;

			if (intersectSegments(startX, startY, endX, endY, rectangle.x, rectangleEndY, rectangleEndX, rectangleEndY))
				return true;

			return rectangle.contains(startX, startY);
		}

		/** {@link #intersectSegmentRectangle(float, float, float, float, Rectangle)} */
		public static bool intersectSegmentRectangle(DGVector2 start, DGVector2 end, DGRectangle rectangle)
		{
			return intersectSegmentRectangle(start.x, start.y, end.x, end.y, rectangle);
		}

		/** Check whether the given line segment and {@link Polygon} intersect.
		 * @param p1 The first point of the segment
		 * @param p2 The second point of the segment
		 * @return Whether polygon and segment intersect */
		public static bool intersectSegmentPolygon(DGVector2 p1, DGVector2 p2, DGPolygon polygon)
		{
			DGFixedPoint[] vertices = polygon.getTransformedVertices();
			DGFixedPoint x1 = p1.x, y1 = p1.y, x2 = p2.x, y2 = p2.y;
			int n = vertices.Length;
			DGFixedPoint x3 = vertices[n - 2], y3 = vertices[n - 1];
			for (int i = 0; i < n; i += 2)
			{
				DGFixedPoint x4 = vertices[i], y4 = vertices[i + 1];
				DGFixedPoint d = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
				if (d != (DGFixedPoint)0)
				{
					DGFixedPoint yd = y1 - y3;
					DGFixedPoint xd = x1 - x3;
					DGFixedPoint ua = ((x4 - x3) * yd - (y4 - y3) * xd) / d;
					if (ua >= (DGFixedPoint)0 && ua <= (DGFixedPoint)1)
					{
						DGFixedPoint ub = ((x2 - x1) * yd - (y2 - y1) * xd) / d;
						if (ub >= (DGFixedPoint)0 && ub <= (DGFixedPoint)1)
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
		public static bool intersectSegments(DGVector2 p1, DGVector2 p2, DGVector2 p3, DGVector2 p4, ref DGVector2 intersection)
		{
			return intersectSegments(p1.x, p1.y, p2.x, p2.y, p3.x, p3.y, p4.x, p4.y, ref intersection);
		}

		public static bool intersectSegments(DGVector2 p1, DGVector2 p2, DGVector2 p3, DGVector2 p4)
		{
			return intersectSegments(p1, p2, p3, p4, ref DGVector2.Null);
		}


		/** @param intersection May be null. */
		public static bool intersectSegments(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2, DGFixedPoint x3, DGFixedPoint y3, DGFixedPoint x4, DGFixedPoint y4,
			ref DGVector2 intersection)
		{
			DGFixedPoint d = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
			if (d == (DGFixedPoint)0) return false;

			DGFixedPoint yd = y1 - y3;
			DGFixedPoint xd = x1 - x3;
			DGFixedPoint ua = ((x4 - x3) * yd - (y4 - y3) * xd) / d;
			if (ua < (DGFixedPoint)0 || ua > (DGFixedPoint)1) return false;

			DGFixedPoint ub = ((x2 - x1) * yd - (y2 - y1) * xd) / d;
			if (ub < (DGFixedPoint)0 || ub > (DGFixedPoint)1) return false;

			if (intersection != DGVector2.Null)
				intersection = new DGVector2(x1 + (x2 - x1) * ua, y1 + (y2 - y1) * ua);
			return true;
		}

		public static bool intersectSegments(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2,
			DGFixedPoint x3, DGFixedPoint y3, DGFixedPoint x4, DGFixedPoint y4)
		{
			return intersectSegments(x1, y1, x2, y2, x3, y3, x4, y4, ref DGVector2.Null);
		}


		static DGFixedPoint det(DGFixedPoint a, DGFixedPoint b, DGFixedPoint c, DGFixedPoint d)
		{
			return a * d - b * c;
		}

		public static bool overlaps(DGCircle c1, DGCircle c2)
		{
			return c1.overlaps(c2);
		}

		public static bool overlaps(DGRectangle r1, DGRectangle r2)
		{
			return r1.overlaps(r2);
		}

		public static bool overlaps(DGCircle c, DGRectangle r)
		{
			DGFixedPoint closestX = c.x;
			DGFixedPoint closestY = c.y;

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
		public static bool overlapConvexPolygons(DGPolygon p1, DGPolygon p2)
		{
			return overlapConvexPolygons(p1, p2, ref DGMinimumTranslationVector.Null);
		}

		/** Check whether convex polygons overlap (clockwise or counter-clockwise wound doesn't matter). If they do, optionally obtain
		 * a Minimum Translation Vector indicating the minimum magnitude vector required to push the polygon p1 out of collision with
		 * polygon p2.
		 * @param p1 The first polygon.
		 * @param p2 The second polygon.
		 * @param mtv A Minimum Translation Vector to fill in the case of a collision, or null (optional).
		 * @return Whether polygons overlap. */
		public static bool overlapConvexPolygons(DGPolygon p1, DGPolygon p2, ref DGMinimumTranslationVector mtv)
		{
			return overlapConvexPolygons(p1.getTransformedVertices(), p2.getTransformedVertices(), ref mtv);
		}

		/** @see #overlapConvexPolygons(float[], int, int, float[], int, int, MinimumTranslationVector) */
		public static bool overlapConvexPolygons(DGFixedPoint[] verts1, DGFixedPoint[] verts2, ref DGMinimumTranslationVector mtv)
		{
			return overlapConvexPolygons(verts1, 0, verts1.Length, verts2, 0, verts2.Length, ref mtv);
		}

		public static bool overlapConvexPolygons(DGFixedPoint[] verts1, DGFixedPoint[] verts2)
		{
			return overlapConvexPolygons(verts1, 0, verts1.Length, verts2, 0, verts2.Length, ref DGMinimumTranslationVector.Null);
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
		public static bool overlapConvexPolygons(DGFixedPoint[] verts1, int offset1, int count1, DGFixedPoint[] verts2, int offset2, int count2,
			ref DGMinimumTranslationVector mtv)
		{
			bool overlaps;
			if (!mtv.Equals(DGMinimumTranslationVector.Null))
			{
				mtv.depth = DGFixedPoint.MaxValue;
				mtv.normal.setZero();
			}
			overlaps = overlapsOnAxisOfShape(verts2, offset2, count2, verts1, offset1, count1, ref mtv, true);
			if (overlaps)
			{
				overlaps = overlapsOnAxisOfShape(verts1, offset1, count1, verts2, offset2, count2, ref mtv, false);
			}

			if (!overlaps)
			{
				if (!mtv.Equals(DGMinimumTranslationVector.Null))
				{
					mtv.depth = (DGFixedPoint)0;
					mtv.normal.setZero();
				}
				return false;
			}
			return true;
		}

		public static bool overlapConvexPolygons(DGFixedPoint[] verts1, int offset1, int count1, DGFixedPoint[] verts2,
			int offset2, int count2)
		{
			return overlapConvexPolygons(verts1, offset1, count1, verts2, offset2, count2, ref DGMinimumTranslationVector.Null);
		}

		/** Implementation of the separating axis theorem (SAT) algorithm
		 * @param offset1 offset of verts1
		 * @param count1 count of verts1
		 * @param offset2 offset of verts2
		 * @param count2 count of verts2
		 * @param mtv the minimum translation vector
		 * @param shapesShifted states if shape a and b are shifted. Important for calculating the axis translation for verts1. */
		private static bool overlapsOnAxisOfShape(DGFixedPoint[] verts1, int offset1, int count1, DGFixedPoint[] verts2, int offset2, int count2,
			ref DGMinimumTranslationVector mtv, bool shapesShifted)
		{
			int endA = offset1 + count1;
			int endB = offset2 + count2;
			// get axis of polygon A
			for (int i = offset1; i < endA; i += 2)
			{
				DGFixedPoint x1 = verts1[i];
				DGFixedPoint y1 = verts1[i + 1];
				DGFixedPoint x2 = verts1[(i + 2) % count1];
				DGFixedPoint y2 = verts1[(i + 3) % count1];

				// Get the Axis for the 2 vertices
				DGFixedPoint axisX = y1 - y2;
				DGFixedPoint axisY = -(x1 - x2);

				DGFixedPoint len = DGVector2.len(axisX, axisY);
				// We got a normalized Vector
				axisX /= len;
				axisY /= len;
				DGFixedPoint minA = DGFixedPoint.MaxValue;
				DGFixedPoint maxA = -DGFixedPoint.MaxValue;
				// project shape a on axis
				for (int v = offset1; v < endA; v += 2)
				{
					DGFixedPoint p = verts1[v] * axisX + verts1[v + 1] * axisY;
					minA = DGMath.Min(minA, p);
					maxA = DGMath.Max(maxA, p);
				}

				DGFixedPoint minB = DGMath.MaxValue;
				DGFixedPoint maxB = -DGMath.MaxValue;

				// project shape b on axis
				for (int v = offset2; v < endB; v += 2)
				{
					DGFixedPoint p = verts2[v] * axisX + verts2[v + 1] * axisY;
					minB = DGMath.Min(minB, p);
					maxB = DGMath.Max(maxB, p);
				}
				// There is a gap
				if (maxA < minB || maxB < minA)
				{
					return false;
				}
				else
				{
					if (!mtv.Equals(DGMinimumTranslationVector.Null))
					{
						DGFixedPoint o = DGMath.Min(maxA, maxB) - DGMath.Max(minA, minB);
						bool aContainsB = minA < minB && maxA > maxB;
						bool bContainsA = minB < minA && maxB > maxA;
						// if it contains one or another
						DGFixedPoint mins = (DGFixedPoint)0;
						DGFixedPoint maxs = (DGFixedPoint)0;
						if (aContainsB || bContainsA)
						{
							mins = DGMath.Abs(minA - minB);
							maxs = DGMath.Abs(maxA - maxB);
							o += DGMath.Min(mins, maxs);
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

		private static bool overlapsOnAxisOfShape(DGFixedPoint[] verts1, int offset1, int count1, DGFixedPoint[] verts2,
			int offset2, int count2, bool shapesShifted)
		{
			return overlapsOnAxisOfShape(verts1, offset1, count1, verts2,
			 offset2, count2, ref DGMinimumTranslationVector.Null, shapesShifted);
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
		public static void splitTriangle(DGFixedPoint[] triangle, DGPlane plane, DGSplitTriangle split)
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

		static DGVector3 intersection = new DGVector3();

		private static void splitEdge(DGFixedPoint[] vertices, int s, int e, int stride, DGPlane plane, DGFixedPoint[] split, int offset)
		{
			DGFixedPoint t = DGIntersector.intersectLinePlane(vertices[s], vertices[s + 1], vertices[s + 2], vertices[e], vertices[e + 1],
				vertices[e + 2], plane, ref intersection);
			split[offset + 0] = intersection.x;
			split[offset + 1] = intersection.y;
			split[offset + 2] = intersection.z;
			for (int i = 3; i < stride; i++)
			{
				DGFixedPoint a = vertices[s + i];
				DGFixedPoint b = vertices[e + i];
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
		public static bool hasOverlap(DGVector3[] axes, DGVector3[] aVertices, DGVector3[] bVertices)
		{
			for (int i = 0; i < axes.Length; i++)
			{
				DGVector3 axis = axes[i];
				DGFixedPoint minA = DGFixedPoint.MaxValue;
				DGFixedPoint maxA = -DGFixedPoint.MaxValue;
				// project shape a on axis
				for (int m = 0; m < aVertices.Length; m++)
				{
					DGVector3 aVertex = aVertices[m];
					DGFixedPoint p = aVertex.dot(axis);
					minA = DGMath.Min(minA, p);
					maxA = DGMath.Max(maxA, p);
				}

				DGFixedPoint minB = DGFixedPoint.MaxValue;
				DGFixedPoint maxB = -DGFixedPoint.MaxValue;
				// project shape b on axis
				for (int n = 0; n < bVertices.Length; n++)
				{
					DGVector3 bVertex = bVertices[n];
					DGFixedPoint p = bVertex.dot(axis);
					minB = DGMath.Min(minB, p);
					maxB = DGMath.Max(maxB, p);
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

