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

namespace DG
{
	/// <summary>
	/// Delaunay triangulation. Adapted from Paul Bourke's triangulate: http://paulbourke.net/papers/triangulate/
	/// </summary>
	public class FPDelaunayTriangulator
	{
		private const int INSIDE = 0;
		private const int COMPLETE = 1;
		private const int INCOMPLETE = 2;

		private List<int> quicksortStack = new();
		private FP[] sortedPoints;
		private List<short> triangles = new(16);
		private List<short> originalIndices = new(0);
		private List<int> edges = new();
		private List<bool> complete = new(16);
		private FP[] superTriangle = new FP[6];
		private FPVector2 centroid = new();

		/** @see #computeTriangles(float[], int, int, boolean) */
		public List<short> computeTriangles(List<FP> points, bool sorted)
		{
			return computeTriangles(points.ToArray(), 0, points.Count, sorted);
		}

		/** @see #computeTriangles(float[], int, int, boolean) */
		public List<short> computeTriangles(FP[] polygon, bool sorted)
		{
			return computeTriangles(polygon, 0, polygon.Length, sorted);
		}

		/** Triangulates the given point cloud to a list of triangle indices that make up the Delaunay triangulation.
		 * @param points x,y pairs describing points. Duplicate points will result in undefined behavior.
		 * @param sorted If false, the points will be sorted by the x coordinate, which is required by the triangulation algorithm. If
		 *           sorting is done the input array is not modified, the returned indices are for the input array, and count*2
		 *           additional working memory is needed.
		 * @return triples of indices into the points that describe the triangles in clockwise order. Note the returned array is reused
		 *         for later calls to the same method. */
		public List<short> computeTriangles(FP[] points, int offset, int count, bool sorted)
		{
			if (count > 32767) throw new Exception("count must be <= " + 32767);
			List<short> triangles = this.triangles;
			triangles.Clear();
			if (count < 6) return triangles;
			triangles.Capacity = count;

			if (!sorted)
			{
				if (sortedPoints == null || sortedPoints.Length < count) sortedPoints = new FP[count];
				Array.Copy(points, offset, sortedPoints, 0, count);
				points = sortedPoints;
				offset = 0;
				sort(points, count);
			}

			int end = offset + count;

			// Determine bounds for super triangle.
			FP xmin = points[0], ymin = points[1];
			FP xmax = xmin, ymax = ymin;
			for (int i = offset + 2; i < end; i++)
			{
				FP value = points[i];
				if (value < xmin) xmin = value;
				if (value > xmax) xmax = value;
				i++;
				value = points[i];
				if (value < ymin) ymin = value;
				if (value > ymax) ymax = value;
			}

			FP dx = xmax - xmin, dy = ymax - ymin;
			FP dmax = (dx > dy ? dx : dy) * 20f;
			FP xmid = (xmax + xmin) / 2f, ymid = (ymax + ymin) / 2f;

			// Setup the super triangle, which contains all points.
			FP[] superTriangle = this.superTriangle;
			superTriangle[0] = xmid - dmax;
			superTriangle[1] = ymid - dmax;
			superTriangle[2] = xmid;
			superTriangle[3] = ymid + dmax;
			superTriangle[4] = xmid + dmax;
			superTriangle[5] = ymid - dmax;

			List<int> edges = this.edges;
			edges.Capacity = count / 2;

			List<bool> complete = this.complete;
			complete.Clear();
			complete.Capacity = count;

			// Add super triangle.
			triangles.Add((short)end);
			triangles.Add((short)(end + 2));
			triangles.Add((short)(end + 4));
			complete.Add(false);

			// Include each point one at a time into the existing mesh.
			for (int pointIndex = offset; pointIndex < end; pointIndex += 2)
			{
				FP x = points[pointIndex], y = points[pointIndex + 1];

				// If x,y lies inside the circumcircle of a triangle, the edges are stored and the triangle removed.
				short[] trianglesArray = triangles.ToArray();
				bool[] completeArray = complete.ToArray();
				for (int triangleIndex = triangles.Count - 1; triangleIndex >= 0; triangleIndex -= 3)
				{
					int completeIndex = triangleIndex / 3;
					if (completeArray[completeIndex]) continue;
					int p1 = trianglesArray[triangleIndex - 2];
					int p2 = trianglesArray[triangleIndex - 1];
					int p3 = trianglesArray[triangleIndex];
					FP x1, y1, x2, y2, x3, y3;
					if (p1 >= end)
					{
						int i = p1 - end;
						x1 = superTriangle[i];
						y1 = superTriangle[i + 1];
					}
					else
					{
						x1 = points[p1];
						y1 = points[p1 + 1];
					}

					if (p2 >= end)
					{
						int i = p2 - end;
						x2 = superTriangle[i];
						y2 = superTriangle[i + 1];
					}
					else
					{
						x2 = points[p2];
						y2 = points[p2 + 1];
					}

					if (p3 >= end)
					{
						int i = p3 - end;
						x3 = superTriangle[i];
						y3 = superTriangle[i + 1];
					}
					else
					{
						x3 = points[p3];
						y3 = points[p3 + 1];
					}

					switch (circumCircle(x, y, x1, y1, x2, y2, x3, y3))
					{
						case COMPLETE:
							completeArray[completeIndex] = true;
							break;
						case INSIDE:
							//edges.Add(p1, p2, p2, p3);
							edges.Add(p1);
							edges.Add(p2);
							edges.Add(p2);
							edges.Add(p3);
							//edges.Add(p3, p1);
							edges.Add(p3);
							edges.Add(p1);

							//triangles.removeRange(triangleIndex - 2, triangleIndex);
							triangles.RemoveRange(triangleIndex - 2, 3);
							complete.RemoveAt(completeIndex);
							break;
					}
				}

				int[] edgesArray = edges.ToArray();
				for (int i = 0, n = edges.Count; i < n; i += 2)
				{
					// Skip multiple edges. If all triangles are anticlockwise then all interior edges are opposite pointing in direction.
					int p1 = edgesArray[i];
					if (p1 == -1) continue;
					int p2 = edgesArray[i + 1];
					bool skip = false;
					for (int ii = i + 2; ii < n; ii += 2)
					{
						if (p1 == edgesArray[ii + 1] && p2 == edgesArray[ii])
						{
							skip = true;
							edgesArray[ii] = -1;
						}
					}

					if (skip) continue;

					// Form new triangles for the current point. Edges are arranged in clockwise order.
					triangles.Add((short)p1);
					triangles.Add((short)edgesArray[i + 1]);
					triangles.Add((short)pointIndex);
					complete.Add(false);
				}

				edges.Clear();
			}

			// Remove triangles with super triangle vertices.
			short[] trianglesArrayTmp = triangles.ToArray();
			for (int i = triangles.Count - 1; i >= 0; i -= 3)
			{
				if (trianglesArrayTmp[i] >= end || trianglesArrayTmp[i - 1] >= end || trianglesArrayTmp[i - 2] >= end)
				{
					triangles.RemoveAt(i);
					triangles.RemoveAt(i - 1);
					triangles.RemoveAt(i - 2);
				}
			}

			// Convert sorted to unsorted indices.
			if (!sorted)
			{
				short[] originalIndicesArray = originalIndices.ToArray();
				for (int i = 0, n = triangles.Count; i < n; i++)
					trianglesArrayTmp[i] = (short)(originalIndicesArray[trianglesArrayTmp[i] / 2] * 2);
			}

			// Adjust triangles to start from zero and count by 1, not by vertex x,y coordinate pairs.
			if (offset == 0)
			{
				for (int i = 0, n = triangles.Count; i < n; i++)
					trianglesArrayTmp[i] = (short)(trianglesArrayTmp[i] / 2);
			}
			else
			{
				for (int i = 0, n = triangles.Count; i < n; i++)
					trianglesArrayTmp[i] = (short)((trianglesArrayTmp[i] - offset) / 2);
			}

			return triangles;
		}

		/** Returns INSIDE if point xp,yp is inside the circumcircle made up of the points x1,y1, x2,y2, x3,y3. Returns COMPLETE if xp
		 * is to the right of the entire circumcircle. Otherwise returns INCOMPLETE. Note: a point on the circumcircle edge is
		 * considered inside. */
		private int circumCircle(FP xp, FP yp, FP x1, FP y1, FP x2,
			FP y2, FP x3, FP y3)
		{
			FP xc, yc;
			FP y1y2 = FPMath.Abs(y1 - y2);
			FP y2y3 = FPMath.Abs(y2 - y3);
			if (y1y2 < FPMath.EPSILION)
			{
				if (y2y3 < FPMath.EPSILION) return INCOMPLETE;
				FP m2 = -(x3 - x2) / (y3 - y2);
				FP mx2 = (x2 + x3) / 2f;
				FP my2 = (y2 + y3) / 2f;
				xc = (x2 + x1) / 2f;
				yc = m2 * (xc - mx2) + my2;
			}
			else
			{
				FP m1 = -(x2 - x1) / (y2 - y1);
				FP mx1 = (x1 + x2) / 2f;
				FP my1 = (y1 + y2) / 2f;
				if (y2y3 < FPMath.EPSILION)
				{
					xc = (x3 + x2) / 2f;
					yc = m1 * (xc - mx1) + my1;
				}
				else
				{
					FP m2 = -(x3 - x2) / (y3 - y2);
					FP mx2 = (x2 + x3) / 2f;
					FP my2 = (y2 + y3) / 2f;
					xc = (m1 * mx1 - m2 * mx2 + my2 - my1) / (m1 - m2);
					yc = m1 * (xc - mx1) + my1;
				}
			}

			FP dx = x2 - xc;
			FP dy = y2 - yc;
			FP rsqr = dx * dx + dy * dy;

			dx = xp - xc;
			dx *= dx;
			dy = yp - yc;
			if (dx + dy * dy - rsqr <= FPMath.EPSILION) return INSIDE;
			return xp > xc && dx > rsqr ? COMPLETE : INCOMPLETE;
		}

		/** Sorts x,y pairs of values by the x value.
		 * @param count Number of indices, must be even. */
		private void sort(FP[] values, int count)
		{
			int pointCount = count / 2;
			originalIndices.Clear();
			originalIndices.Capacity = pointCount;
			short[] originalIndicesArray = originalIndices.ToArray();
			for (short i = 0; i < pointCount; i++)
				originalIndicesArray[i] = i;

			int lower = 0;
			int upper = count - 1;
			List<int> stack = quicksortStack;
			stack.Add(lower);
			stack.Add(upper - 1);
			while (stack.Count > 0)
			{
				upper = stack[stack.Count];
				stack.RemoveAt(stack.Count - 1);
				lower = stack[stack.Count];
				stack.RemoveAt(stack.Count - 1);
				if (upper <= lower) continue;
				int i = quicksortPartition(values, lower, upper, originalIndicesArray);
				if (i - lower > upper - i)
				{
					stack.Add(lower);
					stack.Add(i - 2);
				}

				stack.Add(i + 2);
				stack.Add(upper);
				if (upper - i >= i - lower)
				{
					stack.Add(lower);
					stack.Add(i - 2);
				}
			}
		}

		private int quicksortPartition(FP[] values, int lower, int upper, short[] originalIndices)
		{
			FP value = values[lower];
			int up = upper;
			int down = lower + 2;
			FP tempValue;
			short tempIndex;
			while (down < up)
			{
				while (down < up && values[down] <= value)
					down += 2;
				while (values[up] > value)
					up -= 2;
				if (down < up)
				{
					tempValue = values[down];
					values[down] = values[up];
					values[up] = tempValue;

					tempValue = values[down + 1];
					values[down + 1] = values[up + 1];
					values[up + 1] = tempValue;

					tempIndex = originalIndices[down / 2];
					originalIndices[down / 2] = originalIndices[up / 2];
					originalIndices[up / 2] = tempIndex;
				}
			}

			if (value > values[up])
			{
				values[lower] = values[up];
				values[up] = value;

				tempValue = values[lower + 1];
				values[lower + 1] = values[up + 1];
				values[up + 1] = tempValue;

				tempIndex = originalIndices[lower / 2];
				originalIndices[lower / 2] = originalIndices[up / 2];
				originalIndices[up / 2] = tempIndex;
			}

			return up;
		}

		/** Removes all triangles with a centroid outside the specified hull, which may be concave. Note some triangulations may have
		 * triangles whose centroid is inside the hull but a portion is outside. */
		public void trim(List<short> triangles, FP[] points, FP[] hull, int offset, int count)
		{
			short[] trianglesArray = triangles.ToArray();
			for (int i = triangles.Count - 1; i >= 0; i -= 3)
			{
				int p1 = trianglesArray[i - 2] * 2;
				int p2 = trianglesArray[i - 1] * 2;
				int p3 = trianglesArray[i] * 2;
				FPGeometryUtils.triangleCentroid(points[p1], points[p1 + 1], points[p2], points[p2 + 1], points[p3],
					points[p3 + 1],
					centroid);
				if (!FPIntersector.isPointInPolygon(hull, offset, count, centroid.x, centroid.y))
				{
					triangles.RemoveAt(i);
					triangles.RemoveAt(i - 1);
					triangles.RemoveAt(i - 2);
				}
			}
		}
	}
}
