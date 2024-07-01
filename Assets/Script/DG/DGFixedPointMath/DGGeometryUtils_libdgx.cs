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
	public static partial class DGGeometryUtils
	{
		private static DGVector2 tmp1 = new DGVector2(), tmp2 = new DGVector2(), tmp3 = new DGVector2();

		/** Computes the barycentric coordinates v,w for the specified point in the triangle.
		 * <p>
		 * If barycentric.x >= 0 && barycentric.y >= 0 && barycentric.x + barycentric.y <= 1 then the point is inside the triangle.
		 * <p>
		 * If vertices a,b,c have values aa,bb,cc then to get an interpolated value at point p:
		 * 
		 * <pre>
		 * GeometryUtils.toBarycoord(p, a, b, c, barycentric);
		 * // THEN:
		 * float u = 1f - barycentric.x - barycentric.y;
		 * float x = u * aa.x + barycentric.x * bb.x + barycentric.y * cc.x;
		 * float y = u * aa.y + barycentric.x * bb.y + barycentric.y * cc.y;
		 * // OR:
		 * GeometryUtils.fromBarycoord(barycentric, aa, bb, cc, out);
		 * </pre>
		 * 
		 * @return barycentricOut */
		public static DGVector2 toBarycoord(DGVector2 p, DGVector2 a, DGVector2 b, DGVector2 c, DGVector2 barycentricOut)
		{
			DGVector2 v0 = tmp1.set(b).sub(a);
			DGVector2 v1 = tmp2.set(c).sub(a);
			DGVector2 v2 = tmp3.set(p).sub(a);
			DGFixedPoint d00 = v0.dot(v0);
			DGFixedPoint d01 = v0.dot(v1);
			DGFixedPoint d11 = v1.dot(v1);
			DGFixedPoint d20 = v2.dot(v0);
			DGFixedPoint d21 = v2.dot(v1);
			DGFixedPoint denom = d00 * d11 - d01 * d01;
			barycentricOut.x = (d11 * d20 - d01 * d21) / denom;
			barycentricOut.y = (d00 * d21 - d01 * d20) / denom;
			return barycentricOut;
		}

		/** Returns true if the barycentric coordinates are inside the triangle. */
		public static bool barycoordInsideTriangle(DGVector2 barycentric)
		{
			return barycentric.x >= (DGFixedPoint)0 && barycentric.y >= (DGFixedPoint)0 && barycentric.x + barycentric.y <= (DGFixedPoint)1;
		}

		/** Returns interpolated values given the barycentric coordinates of a point in a triangle and the values at each vertex.
		 * @return interpolatedOut */
		public static DGVector2 fromBarycoord(DGVector2 barycentric, DGVector2 a, DGVector2 b, DGVector2 c,
			DGVector2 interpolatedOut)
		{
			DGFixedPoint u = (DGFixedPoint)1 - barycentric.x - barycentric.y;
			interpolatedOut.x = u * a.x + barycentric.x * b.x + barycentric.y * c.x;
			interpolatedOut.y = u * a.y + barycentric.x * b.y + barycentric.y * c.y;
			return interpolatedOut;
		}

		/** Returns an interpolated value given the barycentric coordinates of a point in a triangle and the values at each vertex.
		 * @return interpolatedOut */
		public static DGFixedPoint fromBarycoord(DGVector2 barycentric, DGFixedPoint a, DGFixedPoint b, DGFixedPoint c)
		{
			DGFixedPoint u = (DGFixedPoint)1 - barycentric.x - barycentric.y;
			return u * a + barycentric.x * b + barycentric.y * c;
		}

		/** Returns the lowest positive root of the quadric equation given by a * x * x + b * x + c = 0. If no solution is given,
		 * Float.NaN is returned.
		 * @param a the first coefficient of the quadric equation
		 * @param b the second coefficient of the quadric equation
		 * @param c the third coefficient of the quadric equation
		 * @return the lowest positive root or Float.Nan */
		public static DGFixedPoint lowestPositiveRoot(DGFixedPoint a, DGFixedPoint b, DGFixedPoint c)
		{
			DGFixedPoint det = b * b - (DGFixedPoint)4 * a * c;
			if (det < (DGFixedPoint)0) throw new Exception("Float NaN");

			DGFixedPoint sqrtD = DGFixedPointMath.Sqrt(det);
			DGFixedPoint invA = (DGFixedPoint)1 / ((DGFixedPoint)2 * a);
			DGFixedPoint r1 = (-b - sqrtD) * invA;
			DGFixedPoint r2 = (-b + sqrtD) * invA;

			if (r1 > r2)
			{
				DGFixedPoint tmp = r2;
				r2 = r1;
				r1 = tmp;
			}

			if (r1 > (DGFixedPoint)0) return r1;
			if (r2 > (DGFixedPoint)0) return r2;
			throw new Exception("Float NaN");
		}

		public static bool colinear(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2, DGFixedPoint x3, DGFixedPoint y3)
		{
			DGFixedPoint dx21 = x2 - x1, dy21 = y2 - y1;
			DGFixedPoint dx32 = x3 - x2, dy32 = y3 - y2;
			DGFixedPoint det = dx32 * dy21 - dx21 * dy32;
			return DGFixedPointMath.Abs(det) < DGFixedPointMath.Epsilon;
		}

		public static DGVector2 triangleCentroid(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2, DGFixedPoint x3, DGFixedPoint y3, DGVector2 centroid)
		{
			centroid.x = (x1 + x2 + x3) / (DGFixedPoint)3;
			centroid.y = (y1 + y2 + y3) / (DGFixedPoint)3;
			return centroid;
		}

		/** Returns the circumcenter of the triangle. The input points must not be colinear. */
		public static DGVector2 triangleCircumcenter(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2, DGFixedPoint x3, DGFixedPoint y3, DGVector2 circumcenter)
		{
			DGFixedPoint dx21 = x2 - x1, dy21 = y2 - y1;
			DGFixedPoint dx32 = x3 - x2, dy32 = y3 - y2;
			DGFixedPoint dx13 = x1 - x3, dy13 = y1 - y3;
			DGFixedPoint det = dx32 * dy21 - dx21 * dy32;
			if (DGFixedPointMath.Abs(det) < DGFixedPointMath.Epsilon)
				throw new Exception("Triangle points must not be colinear.");
			det *= (DGFixedPoint)2;
			DGFixedPoint sqr1 = x1 * x1 + y1 * y1, sqr2 = x2 * x2 + y2 * y2, sqr3 = x3 * x3 + y3 * y3;
			circumcenter.set((sqr1 * dy32 + sqr2 * dy13 + sqr3 * dy21) / det,
				-(sqr1 * dx32 + sqr2 * dx13 + sqr3 * dx21) / det);
			return circumcenter;
		}

		public static DGFixedPoint triangleCircumradius(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2, DGFixedPoint x3, DGFixedPoint y3)
		{
			DGFixedPoint m1, m2, mx1, mx2, my1, my2, x, y;
			if (DGFixedPointMath.Abs(y2 - y1) < DGFixedPointMath.Epsilon)
			{
				m2 = -(x3 - x2) / (y3 - y2);
				mx2 = (x2 + x3) / (DGFixedPoint)2;
				my2 = (y2 + y3) / (DGFixedPoint)2;
				x = (x2 + x1) / (DGFixedPoint)2;
				y = m2 * (x - mx2) + my2;
			}
			else if (DGFixedPointMath.Abs(y3 - y2) < DGFixedPointMath.Epsilon)
			{
				m1 = -(x2 - x1) / (y2 - y1);
				mx1 = (x1 + x2) / (DGFixedPoint)2;
				my1 = (y1 + y2) / (DGFixedPoint)2;
				x = (x3 + x2) / (DGFixedPoint)2;
				y = m1 * (x - mx1) + my1;
			}
			else
			{
				m1 = -(x2 - x1) / (y2 - y1);
				m2 = -(x3 - x2) / (y3 - y2);
				mx1 = (x1 + x2) / (DGFixedPoint)2;
				mx2 = (x2 + x3) / (DGFixedPoint)2;
				my1 = (y1 + y2) / (DGFixedPoint)2;
				my2 = (y2 + y3) / (DGFixedPoint)2;
				x = (m1 * mx1 - m2 * mx2 + my2 - my1) / (m1 - m2);
				y = m1 * (x - mx1) + my1;
			}

			DGFixedPoint dx = x1 - x, dy = y1 - y;
			return DGFixedPointMath.Sqrt(dx * dx + dy * dy);
		}

		/** Ratio of circumradius to shortest edge as a measure of triangle quality.
		 * <p>
		 * Gary L. Miller, Dafna Talmor, Shang-Hua Teng, and Noel Walkington. A Delaunay Based Numerical Method for Three Dimensions:
		 * Generation, Formulation, and Partition. */
		public static DGFixedPoint triangleQuality(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2, DGFixedPoint x3, DGFixedPoint y3)
		{
			DGFixedPoint sqLength1 = x1 * x1 + y1 * y1;
			DGFixedPoint sqLength2 = x2 * x2 + y2 * y2;
			DGFixedPoint sqLength3 = x3 * x3 + y3 * y3;
			return DGFixedPointMath.Sqrt(DGFixedPointMath.Min(sqLength1, DGFixedPointMath.Min(sqLength2, sqLength3))) /
				   triangleCircumradius(x1, y1, x2, y2, x3, y3);
		}

		public static DGFixedPoint triangleArea(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2, DGFixedPoint x3, DGFixedPoint y3)
		{
			return DGFixedPointMath.Abs((x1 - x3) * (y2 - y1) - (x1 - x2) * (y3 - y1)) * (DGFixedPoint)0.5f;
		}

		public static DGVector2 quadrilateralCentroid(DGFixedPoint x1, DGFixedPoint y1, DGFixedPoint x2, DGFixedPoint y2, DGFixedPoint x3, DGFixedPoint y3, DGFixedPoint x4, DGFixedPoint y4,
			DGVector2 centroid)
		{
			DGFixedPoint avgX1 = (x1 + x2 + x3) / (DGFixedPoint)3;
			DGFixedPoint avgY1 = (y1 + y2 + y3) / (DGFixedPoint)3;
			DGFixedPoint avgX2 = (x1 + x4 + x3) / (DGFixedPoint)3;
			DGFixedPoint avgY2 = (y1 + y4 + y3) / (DGFixedPoint)3;
			centroid.x = avgX1 - (avgX1 - avgX2) / (DGFixedPoint)2;
			centroid.y = avgY1 - (avgY1 - avgY2) / (DGFixedPoint)2;
			return centroid;
		}

		/** Returns the centroid for the specified non-self-intersecting polygon. */
		public static DGVector2 polygonCentroid(DGFixedPoint[] polygon, int offset, int count, DGVector2 centroid)
		{
			if (count < 6) throw new Exception("A polygon must have 3 or more coordinate pairs.");

			DGFixedPoint area = (DGFixedPoint)0, x = (DGFixedPoint)0, y = (DGFixedPoint)0;
			int last = offset + count - 2;
			DGFixedPoint x1 = polygon[last], y1 = polygon[last + 1];
			for (int i = offset; i <= last; i += 2)
			{
				DGFixedPoint x2 = polygon[i], y2 = polygon[i + 1];
				DGFixedPoint a = x1 * y2 - x2 * y1;
				area += a;
				x += (x1 + x2) * a;
				y += (y1 + y2) * a;
				x1 = x2;
				y1 = y2;
			}

			if (area == (DGFixedPoint)0)
			{
				centroid.x = (DGFixedPoint)0;
				centroid.y = (DGFixedPoint)0;
			}
			else
			{
				area *= (DGFixedPoint)0.5f;
				centroid.x = x / ((DGFixedPoint)6 * area);
				centroid.y = y / ((DGFixedPoint)6 * area);
			}

			return centroid;
		}

		/** Computes the area for a convex polygon. */
		public static DGFixedPoint polygonArea(DGFixedPoint[] polygon, int offset, int count)
		{
			DGFixedPoint area = (DGFixedPoint)0;
			int last = offset + count - 2;
			DGFixedPoint x1 = polygon[last], y1 = polygon[last + 1];
			for (int i = offset; i <= last; i += 2)
			{
				DGFixedPoint x2 = polygon[i], y2 = polygon[i + 1];
				area += x1 * y2 - x2 * y1;
				x1 = x2;
				y1 = y2;
			}

			return area * (DGFixedPoint)0.5f;
		}

		public static void ensureCCW(DGFixedPoint[] polygon)
		{
			ensureCCW(polygon, 0, polygon.Length);
		}

		public static void ensureCCW(DGFixedPoint[] polygon, int offset, int count)
		{
			if (!isClockwise(polygon, offset, count)) return;
			reverseVertices(polygon, offset, count);
		}

		public static void ensureClockwise(DGFixedPoint[] polygon)
		{
			ensureClockwise(polygon, 0, polygon.Length);
		}

		public static void ensureClockwise(DGFixedPoint[] polygon, int offset, int count)
		{
			if (isClockwise(polygon, offset, count)) return;
			reverseVertices(polygon, offset, count);
		}

		public static void reverseVertices(DGFixedPoint[] polygon, int offset, int count)
		{
			int lastX = offset + count - 2;
			for (int i = offset, n = offset + count / 2; i < n; i += 2)
			{
				int other = lastX - i;
				DGFixedPoint x = polygon[i];
				DGFixedPoint y = polygon[i + 1];
				polygon[i] = polygon[other];
				polygon[i + 1] = polygon[other + 1];
				polygon[other] = x;
				polygon[other + 1] = y;
			}
		}

		public static bool isClockwise(DGFixedPoint[] polygon, int offset, int count)
		{
			if (count <= 2) return false;
			DGFixedPoint area = (DGFixedPoint)0;
			int last = offset + count - 2;
			DGFixedPoint x1 = polygon[last], y1 = polygon[last + 1];
			for (int i = offset; i <= last; i += 2)
			{
				DGFixedPoint x2 = polygon[i], y2 = polygon[i + 1];
				area += x1 * y2 - x2 * y1;
				x1 = x2;
				y1 = y2;
			}

			return area < (DGFixedPoint)0;
		}

		public static bool isCCW(DGFixedPoint[] polygon, int offset, int count)
		{
			return !isClockwise(polygon, offset, count);
		}
	}
}
