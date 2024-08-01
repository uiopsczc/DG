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
	/// <summary>
	/// Computes the convex hull of a set of points using the monotone chain convex hull algorithm (aka Andrew's algorithm).
	/// </summary>
	public partial class FPConvexHull
	{
		private List<int> quicksortStack = new List<int>();
		private FP[] sortedPoints;
		private List<FP> hull = new List<FP>();
		private List<int> indices = new List<int>();
		private List<short> originalIndices = new List<short>();

		/** @see #computePolygon(float[], int, int, boolean) */
		public List<FP> computePolygon(List<FP> points, bool sorted)
		{
			return computePolygon(points.ToArray(), 0, points.Count, sorted);
		}

		/** @see #computePolygon(float[], int, int, boolean) */
		public List<FP> computePolygon(FP[] polygon, bool sorted)
		{
			return computePolygon(polygon, 0, polygon.Length, sorted);
		}

		/** Returns a list of points on the convex hull in counter-clockwise order. Note: the last point in the returned list is the
		 * same as the first one. */
		/** Returns the convex hull polygon for the given point cloud.
		 * @param points x,y pairs describing points. Duplicate points will result in undefined behavior.
		 * @param sorted If false, the points will be sorted by the x coordinate then the y coordinate, which is required by the convex
		 *           hull algorithm. If sorting is done the input array is not modified and count additional working memory is needed.
		 * @return pairs of coordinates that describe the convex hull polygon in counterclockwise order. Note the returned array is
		 *         reused for later calls to the same method. */
		public List<FP> computePolygon(FP[] points, int offset, int count, bool sorted)
		{
			int end = offset + count;

			if (!sorted)
			{
				if (sortedPoints == null || sortedPoints.Length < count) sortedPoints = new FP[count];
				Array.Copy(points, offset, sortedPoints, 0, count);
				points = sortedPoints;
				offset = 0;
				sort(points, count);
			}

			List<FP> hull = this.hull;
			hull.Clear();

			// Lower hull.
			for (int i = offset; i < end; i += 2)
			{
				FP x = points[i];
				FP y = points[i + 1];
				while (hull.Count >= 4 && ccw(x, y) <= 0)
				{
					//				hull.Count -= 2;
					hull.RemoveAt(hull.Count - 1);
					hull.RemoveAt(hull.Count - 1);
				}

				hull.Add(x);
				hull.Add(y);
			}

			// Upper hull.
			for (int i = end - 4, t = hull.Count + 2; i >= offset; i -= 2)
			{
				FP x = points[i];
				FP y = points[i + 1];
				while (hull.Count >= t && ccw(x, y) <= 0)
				{
					//hull.size -= 2;
					hull.RemoveAt(hull.Count - 1);
					hull.RemoveAt(hull.Count - 1);
				}

				hull.Add(x);
				hull.Add(y);
			}

			return hull;
		}

		/** @see #computeIndices(float[], int, int, boolean, boolean) */
		public List<int> computeIndices(List<FP> points, bool sorted, bool yDown)
		{
			return computeIndices(points.ToArray(), 0, points.Count, sorted, yDown);
		}

		/** @see #computeIndices(float[], int, int, boolean, boolean) */
		public List<int> computeIndices(FP[] polygon, bool sorted, bool yDown)
		{
			return computeIndices(polygon, 0, polygon.Length, sorted, yDown);
		}

		/** Computes a hull the same as {@link #computePolygon(float[], int, int, boolean)} but returns indices of the specified
		 * points. */
		public List<int> computeIndices(FP[] points, int offset, int count, bool sorted, bool yDown)
		{
			if (count > 32767) throw new Exception("count must be <= " + 32767);
			int end = offset + count;

			if (!sorted)
			{
				if (sortedPoints == null || sortedPoints.Length < count) sortedPoints = new FP[count];
				Array.Copy(points, offset, sortedPoints, 0, count);
				points = sortedPoints;
				offset = 0;
				sortWithIndices(points, count, yDown);
			}

			List<int> indices = this.indices;
			indices.Clear();

			List<FP> hull = this.hull;
			hull.Clear();

			// Lower hull.
			for (int i = offset, index = i / 2; i < end; i += 2, index++)
			{
				FP x = points[i];
				FP y = points[i + 1];
				while (hull.Count >= 4 && ccw(x, y) <= 0)
				{
					//hull.size -= 2;
					hull.RemoveAt(hull.Count - 1);
					hull.RemoveAt(hull.Count - 1);
					//indices.size--;
					indices.RemoveAt(indices.Count - 1);
				}

				hull.Add(x);
				hull.Add(y);
				indices.Add(index);
			}

			// Upper hull.
			for (int i = end - 4, index = i / 2, t = hull.Count + 2; i >= offset; i -= 2, index--)
			{
				FP x = points[i];
				FP y = points[i + 1];
				while (hull.Count >= t && ccw(x, y) <= 0)
				{
					//hull.size -= 2;
					hull.RemoveAt(hull.Count - 1);
					hull.RemoveAt(hull.Count - 1);
					//indices.size--;
					indices.RemoveAt(indices.Count - 1);
				}

				hull.Add(x);
				hull.Add(y);
				indices.Add(index);
			}

			// Convert sorted to unsorted indices.
			if (!sorted)
			{
				short[] originalIndicesArray = originalIndices.ToArray();
				int[] indicesArray = indices.ToArray();
				for (int i = 0, n = indices.Count; i < n; i++)
					indicesArray[i] = originalIndicesArray[indicesArray[i]];
			}

			return indices;
		}

		/** Returns > 0 if the points are a counterclockwise turn, < 0 if clockwise, and 0 if colinear. */
		private FP ccw(FP p3x, FP p3y)
		{
			List<FP> hull = this.hull;
			int size = hull.Count;
			FP p1x = hull[size - 4];
			FP p1y = hull[size - 3];
			FP p2x = hull[size - 2];
			FP p2y = hull[size - 1];
			return (p2x - p1x) * (p3y - p1y) - (p2y - p1y) * (p3x - p1x);
		}

		/** Sorts x,y pairs of values by the x value, then the y value.
		 * @param count Number of indices, must be even. */
		private void sort(FP[] values, int count)
		{
			int lower = 0;
			int upper = count - 1;
			List<int> stack = quicksortStack;
			stack.Add(lower);
			stack.Add(upper - 1);
			while (stack.Count > 0)
			{
				upper = stack[stack.Count - 1];
				stack.RemoveAt(stack.Count - 1);
				lower = stack[stack.Count - 1];
				stack.RemoveAt(stack.Count - 1);
				if (upper <= lower) continue;
				int i = quicksortPartition(values, lower, upper);
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

		private int quicksortPartition(FP[] values, int lower, int upper)
		{
			FP x = values[lower];
			FP y = values[lower + 1];
			int up = upper;
			int down = lower;
			FP temp;
			while (down < up)
			{
				while (down < up && values[down] <= x)
					down = down + 2;
				while (values[up] > x || (values[up] == x && values[up + 1] < y))
					up = up - 2;
				if (down < up)
				{
					temp = values[down];
					values[down] = values[up];
					values[up] = temp;

					temp = values[down + 1];
					values[down + 1] = values[up + 1];
					values[up + 1] = temp;
				}
			}

			if (x > values[up] || (x == values[up] && y < values[up + 1]))
			{
				values[lower] = values[up];
				values[up] = x;

				values[lower + 1] = values[up + 1];
				values[up + 1] = y;
			}

			return up;
		}

		/** Sorts x,y pairs of values by the x value, then the y value and stores unsorted original indices.
		 * @param count Number of indices, must be even. */
		private void sortWithIndices(FP[] values, int count, bool yDown)
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
				upper = stack[stack.Count - 1];
				stack.RemoveAt(stack.Count - 1);
				lower = stack[stack.Count - 1];
				stack.RemoveAt(stack.Count - 1);
				if (upper <= lower) continue;
				int i = quicksortPartitionWithIndices(values, lower, upper, yDown, originalIndicesArray);
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

		private int quicksortPartitionWithIndices(FP[] values, int lower, int upper, bool yDown,
			short[] originalIndices)
		{
			FP x = values[lower];
			FP y = values[lower + 1];
			int up = upper;
			int down = lower;
			FP temp;
			short tempIndex;
			while (down < up)
			{
				while (down < up && values[down] <= x)
					down = down + 2;
				if (yDown)
				{
					while (values[up] > x || (values[up] == x && values[up + 1] < y))
						up = up - 2;
				}
				else
				{
					while (values[up] > x || (values[up] == x && values[up + 1] > y))
						up = up - 2;
				}

				if (down < up)
				{
					temp = values[down];
					values[down] = values[up];
					values[up] = temp;

					temp = values[down + 1];
					values[down + 1] = values[up + 1];
					values[up + 1] = temp;

					tempIndex = originalIndices[down / 2];
					originalIndices[down / 2] = originalIndices[up / 2];
					originalIndices[up / 2] = tempIndex;
				}
			}

			if (x > values[up] || (x == values[up] && (yDown ? y < values[up + 1] : y > values[up + 1])))
			{
				values[lower] = values[up];
				values[up] = x;

				values[lower + 1] = values[up + 1];
				values[up + 1] = y;

				tempIndex = originalIndices[lower / 2];
				originalIndices[lower / 2] = originalIndices[up / 2];
				originalIndices[up / 2] = tempIndex;
			}

			return up;
		}
	}
}
