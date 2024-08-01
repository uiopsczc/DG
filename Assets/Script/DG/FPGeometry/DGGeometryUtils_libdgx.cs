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
	public static partial class FPGeometryUtils
	{
		private static FPVector2 tmp1 = new FPVector2(), tmp2 = new FPVector2(), tmp3 = new FPVector2();

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
		public static FPVector2 toBarycoord(FPVector2 p, FPVector2 a, FPVector2 b, FPVector2 c, FPVector2 barycentricOut)
		{
			FPVector2 v0 = tmp1.set(b).sub(a);
			FPVector2 v1 = tmp2.set(c).sub(a);
			FPVector2 v2 = tmp3.set(p).sub(a);
			FP d00 = v0.dot(v0);
			FP d01 = v0.dot(v1);
			FP d11 = v1.dot(v1);
			FP d20 = v2.dot(v0);
			FP d21 = v2.dot(v1);
			FP denom = d00 * d11 - d01 * d01;
			barycentricOut.x = (d11 * d20 - d01 * d21) / denom;
			barycentricOut.y = (d00 * d21 - d01 * d20) / denom;
			return barycentricOut;
		}

		/** Returns true if the barycentric coordinates are inside the triangle. */
		public static bool barycoordInsideTriangle(FPVector2 barycentric)
		{
			return barycentric.x >= 0 && barycentric.y >= 0 && barycentric.x + barycentric.y <= 1;
		}

		/** Returns interpolated values given the barycentric coordinates of a point in a triangle and the values at each vertex.
		 * @return interpolatedOut */
		public static FPVector2 fromBarycoord(FPVector2 barycentric, FPVector2 a, FPVector2 b, FPVector2 c,
			FPVector2 interpolatedOut)
		{
			FP u = 1 - barycentric.x - barycentric.y;
			interpolatedOut.x = u * a.x + barycentric.x * b.x + barycentric.y * c.x;
			interpolatedOut.y = u * a.y + barycentric.x * b.y + barycentric.y * c.y;
			return interpolatedOut;
		}

		/** Returns an interpolated value given the barycentric coordinates of a point in a triangle and the values at each vertex.
		 * @return interpolatedOut */
		public static FP fromBarycoord(FPVector2 barycentric, FP a, FP b, FP c)
		{
			FP u = 1 - barycentric.x - barycentric.y;
			return u * a + barycentric.x * b + barycentric.y * c;
		}

		/** Returns the lowest positive root of the quadric equation given by a * x * x + b * x + c = 0. If no solution is given,
		 * Float.NaN is returned.
		 * @param a the first coefficient of the quadric equation
		 * @param b the second coefficient of the quadric equation
		 * @param c the third coefficient of the quadric equation
		 * @return the lowest positive root or Float.Nan */
		public static FP lowestPositiveRoot(FP a, FP b, FP c)
		{
			FP det = b * b - 4 * a * c;
			if (det < 0) throw new Exception("Float NaN");

			FP sqrtD = FPMath.Sqrt(det);
			FP invA = 1 / (2 * a);
			FP r1 = (-b - sqrtD) * invA;
			FP r2 = (-b + sqrtD) * invA;

			if (r1 > r2)
			{
				FP tmp = r2;
				r2 = r1;
				r1 = tmp;
			}

			if (r1 > 0) return r1;
			if (r2 > 0) return r2;
			throw new Exception("Float NaN");
		}

		public static bool colinear(FP x1, FP y1, FP x2, FP y2, FP x3, FP y3)
		{
			FP dx21 = x2 - x1, dy21 = y2 - y1;
			FP dx32 = x3 - x2, dy32 = y3 - y2;
			FP det = dx32 * dy21 - dx21 * dy32;
			return FPMath.Abs(det) < FPMath.EPSILION;
		}

		public static FPVector2 triangleCentroid(FP x1, FP y1, FP x2, FP y2, FP x3, FP y3, FPVector2 centroid)
		{
			centroid.x = (x1 + x2 + x3) / 3;
			centroid.y = (y1 + y2 + y3) / 3;
			return centroid;
		}

		/** Returns the circumcenter of the triangle. The input points must not be colinear. */
		public static FPVector2 triangleCircumcenter(FP x1, FP y1, FP x2, FP y2, FP x3, FP y3, FPVector2 circumcenter)
		{
			FP dx21 = x2 - x1, dy21 = y2 - y1;
			FP dx32 = x3 - x2, dy32 = y3 - y2;
			FP dx13 = x1 - x3, dy13 = y1 - y3;
			FP det = dx32 * dy21 - dx21 * dy32;
			if (FPMath.Abs(det) < FPMath.EPSILION)
				throw new Exception("Triangle points must not be colinear.");
			det *= 2;
			FP sqr1 = x1 * x1 + y1 * y1, sqr2 = x2 * x2 + y2 * y2, sqr3 = x3 * x3 + y3 * y3;
			circumcenter.set((sqr1 * dy32 + sqr2 * dy13 + sqr3 * dy21) / det,
				-(sqr1 * dx32 + sqr2 * dx13 + sqr3 * dx21) / det);
			return circumcenter;
		}

		public static FP triangleCircumradius(FP x1, FP y1, FP x2, FP y2, FP x3, FP y3)
		{
			FP m1, m2, mx1, mx2, my1, my2, x, y;
			if (FPMath.Abs(y2 - y1) < FPMath.EPSILION)
			{
				m2 = -(x3 - x2) / (y3 - y2);
				mx2 = (x2 + x3) / 2;
				my2 = (y2 + y3) / 2;
				x = (x2 + x1) / 2;
				y = m2 * (x - mx2) + my2;
			}
			else if (FPMath.Abs(y3 - y2) < FPMath.EPSILION)
			{
				m1 = -(x2 - x1) / (y2 - y1);
				mx1 = (x1 + x2) / 2;
				my1 = (y1 + y2) / 2;
				x = (x3 + x2) / 2;
				y = m1 * (x - mx1) + my1;
			}
			else
			{
				m1 = -(x2 - x1) / (y2 - y1);
				m2 = -(x3 - x2) / (y3 - y2);
				mx1 = (x1 + x2) / 2;
				mx2 = (x2 + x3) / 2;
				my1 = (y1 + y2) / 2;
				my2 = (y2 + y3) / 2;
				x = (m1 * mx1 - m2 * mx2 + my2 - my1) / (m1 - m2);
				y = m1 * (x - mx1) + my1;
			}

			FP dx = x1 - x, dy = y1 - y;
			return FPMath.Sqrt(dx * dx + dy * dy);
		}

		/** Ratio of circumradius to shortest edge as a measure of triangle quality.
		 * <p>
		 * Gary L. Miller, Dafna Talmor, Shang-Hua Teng, and Noel Walkington. A Delaunay Based Numerical Method for Three Dimensions:
		 * Generation, Formulation, and Partition. */
		public static FP triangleQuality(FP x1, FP y1, FP x2, FP y2, FP x3, FP y3)
		{
			FP sqLength1 = x1 * x1 + y1 * y1;
			FP sqLength2 = x2 * x2 + y2 * y2;
			FP sqLength3 = x3 * x3 + y3 * y3;
			return FPMath.Sqrt(FPMath.Min(sqLength1, FPMath.Min(sqLength2, sqLength3))) /
				   triangleCircumradius(x1, y1, x2, y2, x3, y3);
		}

		public static FP triangleArea(FP x1, FP y1, FP x2, FP y2, FP x3, FP y3)
		{
			return FPMath.Abs((x1 - x3) * (y2 - y1) - (x1 - x2) * (y3 - y1)) * 0.5f;
		}

		public static FPVector2 quadrilateralCentroid(FP x1, FP y1, FP x2, FP y2, FP x3, FP y3, FP x4, FP y4,
			FPVector2 centroid)
		{
			FP avgX1 = (x1 + x2 + x3) / 3;
			FP avgY1 = (y1 + y2 + y3) / 3;
			FP avgX2 = (x1 + x4 + x3) / 3;
			FP avgY2 = (y1 + y4 + y3) / 3;
			centroid.x = avgX1 - (avgX1 - avgX2) / 2;
			centroid.y = avgY1 - (avgY1 - avgY2) / 2;
			return centroid;
		}

		/** Returns the centroid for the specified non-self-intersecting polygon. */
		public static FPVector2 polygonCentroid(FP[] polygon, int offset, int count, FPVector2 centroid)
		{
			if (count < 6) throw new Exception("A polygon must have 3 or more coordinate pairs.");

			FP area = 0, x = 0, y = 0;
			int last = offset + count - 2;
			FP x1 = polygon[last], y1 = polygon[last + 1];
			for (int i = offset; i <= last; i += 2)
			{
				FP x2 = polygon[i], y2 = polygon[i + 1];
				FP a = x1 * y2 - x2 * y1;
				area += a;
				x += (x1 + x2) * a;
				y += (y1 + y2) * a;
				x1 = x2;
				y1 = y2;
			}

			if (area == 0)
			{
				centroid.x = 0;
				centroid.y = 0;
			}
			else
			{
				area *= 0.5f;
				centroid.x = x / (6 * area);
				centroid.y = y / (6 * area);
			}

			return centroid;
		}

		/** Computes the area for a convex polygon. */
		public static FP polygonArea(FP[] polygon, int offset, int count)
		{
			FP area = 0;
			int last = offset + count - 2;
			FP x1 = polygon[last], y1 = polygon[last + 1];
			for (int i = offset; i <= last; i += 2)
			{
				FP x2 = polygon[i], y2 = polygon[i + 1];
				area += x1 * y2 - x2 * y1;
				x1 = x2;
				y1 = y2;
			}

			return area * 0.5f;
		}

		public static void ensureCCW(FP[] polygon)
		{
			ensureCCW(polygon, 0, polygon.Length);
		}

		public static void ensureCCW(FP[] polygon, int offset, int count)
		{
			if (!isClockwise(polygon, offset, count)) return;
			reverseVertices(polygon, offset, count);
		}

		public static void ensureClockwise(FP[] polygon)
		{
			ensureClockwise(polygon, 0, polygon.Length);
		}

		public static void ensureClockwise(FP[] polygon, int offset, int count)
		{
			if (isClockwise(polygon, offset, count)) return;
			reverseVertices(polygon, offset, count);
		}

		public static void reverseVertices(FP[] polygon, int offset, int count)
		{
			int lastX = offset + count - 2;
			for (int i = offset, n = offset + count / 2; i < n; i += 2)
			{
				int other = lastX - i;
				FP x = polygon[i];
				FP y = polygon[i + 1];
				polygon[i] = polygon[other];
				polygon[i + 1] = polygon[other + 1];
				polygon[other] = x;
				polygon[other + 1] = y;
			}
		}

		public static bool isClockwise(FP[] polygon, int offset, int count)
		{
			if (count <= 2) return false;
			FP area = 0;
			int last = offset + count - 2;
			FP x1 = polygon[last], y1 = polygon[last + 1];
			for (int i = offset; i <= last; i += 2)
			{
				FP x2 = polygon[i], y2 = polygon[i + 1];
				area += x1 * y2 - x2 * y1;
				x1 = x2;
				y1 = y2;
			}

			return area < 0;
		}

		public static bool isCCW(FP[] polygon, int offset, int count)
		{
			return !isClockwise(polygon, offset, count);
		}
	}
}
