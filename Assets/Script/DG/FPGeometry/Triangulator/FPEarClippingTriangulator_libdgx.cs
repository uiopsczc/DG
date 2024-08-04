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
	/// A simple implementation of the ear cutting algorithm to triangulate simple polygons without holes. For more information:
	/// <li><a href="http://cgm.cs.mcgill.ca/~godfried/teaching/cg-projects/97/Ian/algorithm2.html">http://cgm.cs.mcgill.ca/~godfried/teaching/cg-projects/97/Ian/algorithm2.html</a></li>
	/// <li><a href="http://www.geometrictools.com/Documentation/TriangulationByEarClipping.pdf">http://www.geometrictools.com/Documentation/TriangulationByEarClipping.pdf</a></li>
	/// If the input polygon is not simple (self-intersects), there will be output but it is of unspecified quality (garbage in,garbage out).
	/// If the polygon vertices are very large or very close together then {@link GeometryUtils#isClockwise(float[], int, int)} may not
	/// be able to properly assess the winding (because it uses floats). In that case the vertices should be adjusted, eg by finding
	/// the smallest X and Y values and subtracting that from each vertex.
	/// </summary>
	public class FPEarClippingTriangulator
	{
		private const int CONCAVE = -1;
		private const int CONVEX = 1;

		private List<short> indicesArray = new();
		private short[] indices;
		private FP[] vertices;
		private int vertexCount;
		private List<int> vertexTypes = new();
		private List<short> triangles = new();

		/** @see #computeTriangles(float[], int, int) */
		public List<short> computeTriangles(List<FP> vertices)
		{
			return computeTriangles(vertices.ToArray(), 0, vertices.Count);
		}

		/** @see #computeTriangles(float[], int, int) */
		public List<short> computeTriangles(FP[] vertices)
		{
			return computeTriangles(vertices, 0, vertices.Length);
		}

		/** Triangulates the given (convex or concave) simple polygon to a list of triangle vertices.
		 * @param vertices pairs describing vertices of the polygon, in either clockwise or counterclockwise order.
		 * @return triples of triangle indices in clockwise order. Note the returned array is reused for later calls to the same
		 *         method. */
		public List<short> computeTriangles(FP[] vertices, int offset, int count)
		{
			this.vertices = vertices;
			int vertexCount = this.vertexCount = count / 2;
			int vertexOffset = offset / 2;

			List<short> indicesArray = this.indicesArray;
			indicesArray.Clear();
			indicesArray.Capacity = vertexCount;
			short[] indices = this.indices = indicesArray.ToArray();
			if (FPGeometryUtils.isClockwise(vertices, offset, count))
			{
				for (short i = 0; i < vertexCount; i++)
					indices[i] = (short)(vertexOffset + i);
			}
			else
			{
				for (int i = 0, n = vertexCount - 1; i < vertexCount; i++)
					indices[i] = (short)(vertexOffset + n - i); // Reversed.
			}

			List<int> vertexTypes = this.vertexTypes;
			vertexTypes.Clear();
			vertexTypes.Capacity = vertexCount;
			for (int i = 0, n = vertexCount; i < n; ++i)
				vertexTypes.Add(classifyVertex(i));

			// A polygon with n vertices has a triangulation of n-2 triangles.
			List<short> triangles = this.triangles;
			triangles.Clear();
			triangles.Capacity = Math.Max(0, vertexCount - 2) * 3;
			triangulate();
			return triangles;
		}

		private void triangulate()
		{
			int[] vertexTypes = this.vertexTypes.ToArray();

			while (vertexCount > 3)
			{
				int earTipIndex = findEarTip();
				cutEarTip(earTipIndex);

				// The type of the two vertices adjacent to the clipped vertex may have changed.
				int previousIndexTmp = previousIndex(earTipIndex);
				int nextIndex = earTipIndex == vertexCount ? 0 : earTipIndex;
				vertexTypes[previousIndexTmp] = classifyVertex(previousIndexTmp);
				vertexTypes[nextIndex] = classifyVertex(nextIndex);
			}

			if (vertexCount == 3)
			{
				List<short> triangles = this.triangles;
				short[] indices = this.indices;
				triangles.Add(indices[0]);
				triangles.Add(indices[1]);
				triangles.Add(indices[2]);
			}
		}

		/** @return {@link #CONCAVE} or {@link #CONVEX} */
		private int classifyVertex(int index)
		{
			short[] indices = this.indices;
			int previous = indices[previousIndex(index)] * 2;
			int current = indices[index] * 2;
			int next = indices[nextIndex(index)] * 2;
			FP[] vertices = this.vertices;
			return computeSpannedAreaSign(vertices[previous], vertices[previous + 1], vertices[current],
				vertices[current + 1],
				vertices[next], vertices[next + 1]);
		}

		private int findEarTip()
		{
			int vertexCount = this.vertexCount;
			for (int i = 0; i < vertexCount; i++)
				if (isEarTip(i))
					return i;

			// Desperate mode: if no vertex is an ear tip, we are dealing with a degenerate polygon (e.g. nearly collinear).
			// Note that the input was not necessarily degenerate, but we could have made it so by clipping some valid ears.

			// Idea taken from Martin Held, "FIST: Fast industrial-strength triangulation of polygons", Algorithmica (1998),
			// http://citeseerx.ist.psu.edu/viewdoc/summary?doi=10.1.1.115.291

			// Return a convex or tangential vertex if one exists.
			int[] vertexTypes = this.vertexTypes.ToArray();
			for (int i = 0; i < vertexCount; i++)
				if (vertexTypes[i] != CONCAVE)
					return i;
			return 0; // If all vertices are concave, just return the first one.
		}

		private bool isEarTip(int earTipIndex)
		{
			int[] vertexTypes = this.vertexTypes.ToArray();
			if (vertexTypes[earTipIndex] == CONCAVE) return false;

			int previousIndexTmp = previousIndex(earTipIndex);
			int nextIndexTmp = nextIndex(earTipIndex);
			short[] indices = this.indices;
			int p1 = indices[previousIndexTmp] * 2;
			int p2 = indices[earTipIndex] * 2;
			int p3 = indices[nextIndexTmp] * 2;
			FP[] vertices = this.vertices;
			FP p1x = vertices[p1], p1y = vertices[p1 + 1];
			FP p2x = vertices[p2], p2y = vertices[p2 + 1];
			FP p3x = vertices[p3], p3y = vertices[p3 + 1];

			// Check if any point is inside the triangle formed by previous, current and next vertices.
			// Only consider vertices that are not part of this triangle, or else we'll always find one inside.
			for (int i = nextIndex(nextIndexTmp); i != previousIndexTmp; i = nextIndex(i))
			{
				// Concave vertices can obviously be inside the candidate ear, but so can tangential vertices
				// if they coincide with one of the triangle's vertices.
				if (vertexTypes[i] != CONVEX)
				{
					int v = indices[i] * 2;
					FP vx = vertices[v];
					FP vy = vertices[v + 1];
					// Because the polygon has clockwise winding order, the area sign will be positive if the point is strictly inside.
					// It will be 0 on the edge, which we want to include as well.
					// note: check the edge defined by p1->p3 first since this fails _far_ more then the other 2 checks.
					if (computeSpannedAreaSign(p3x, p3y, p1x, p1y, vx, vy) >= 0)
					{
						if (computeSpannedAreaSign(p1x, p1y, p2x, p2y, vx, vy) >= 0)
						{
							if (computeSpannedAreaSign(p2x, p2y, p3x, p3y, vx, vy) >= 0) return false;
						}
					}
				}
			}

			return true;
		}

		private void cutEarTip(int earTipIndex)
		{
			short[] indices = this.indices;
			List<short> triangles = this.triangles;

			triangles.Add(indices[previousIndex(earTipIndex)]);
			triangles.Add(indices[earTipIndex]);
			triangles.Add(indices[nextIndex(earTipIndex)]);

			indicesArray.RemoveAt(earTipIndex);
			vertexTypes.RemoveAt(earTipIndex);
			vertexCount--;
		}

		private int previousIndex(int index)
		{
			return (index == 0 ? vertexCount : index) - 1;
		}

		private int nextIndex(int index)
		{
			return (index + 1) % vertexCount;
		}

		private static int computeSpannedAreaSign(FP p1x, FP p1y, FP p2x, FP p2y,
			FP p3x, FP p3y)
		{
			FP area = p1x * (p3y - p2y);
			area += p2x * (p1y - p3y);
			area += p3x * (p2y - p1y);
			return FPMath.Sign(area);
		}
	}
}
