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

namespace DG
{
	public partial class FPFrustum
	{
		protected static FPVector3[] clipSpacePlanePoints =
		{
		new FPVector3(-1, -1, -1), new FPVector3(1, -1, -1),
		new FPVector3(1, 1, -1), new FPVector3(-1, 1, -1), // near clip
		new FPVector3(-1, -1, 1), new FPVector3(1, -1, 1), new FPVector3(1, 1, 1), new FPVector3(-1, 1, 1)
	}; // far clip

		protected static FP[] _clipSpacePlanePointsArray;

		protected static FP[] clipSpacePlanePointsArray
		{
			get
			{
				if (_clipSpacePlanePointsArray == null)
				{
					_clipSpacePlanePointsArray = new FP[8 * 3];
					int j = 0;
					for (int i = 0; i < clipSpacePlanePoints.Length; i++)
					{
						var v = clipSpacePlanePoints[i];
						clipSpacePlanePointsArray[j++] = v.x;
						clipSpacePlanePointsArray[j++] = v.y;
						clipSpacePlanePointsArray[j++] = v.z;
					}
				}

				return _clipSpacePlanePointsArray;
			}
		}

		private static FPVector3 tmpV = new FPVector3();

		/** the six clipping planes, near, far, left, right, top, bottom **/
		public FPPlane[] planes = new FPPlane[6];

		/** eight points making up the near and far clipping "rectangles". order is counter clockwise, starting at bottom left **/
		public FPVector3[] planePoints =
		{
		new FPVector3(), new FPVector3(), new FPVector3(), new FPVector3(), new FPVector3(), new FPVector3(),
		new FPVector3(), new FPVector3()
	};

		protected FP[] planePointsArray = new FP[8 * 3];

		public FPFrustum()
		{
			for (int i = 0; i < 6; i++)
				planes[i] = new FPPlane(new FPVector3(), 0);
		}

		/** Updates the clipping plane's based on the given inverse combined projection and view matrix, e.g. from an
		* {@link OrthographicCamera} or {@link PerspectiveCamera}.
		* @param inverseProjectionView the combined projection and view matrices. */
		public void update(FPMatrix4x4 inverseProjectionView)
		{
			Array.Copy(clipSpacePlanePointsArray, 0, planePointsArray, 0, clipSpacePlanePointsArray.Length);
			FPMatrix4x4.prj(inverseProjectionView.getValues(), planePointsArray, 0, 8, 3);
			for (int i = 0, j = 0; i < 8; i++)
			{
				FPVector3 v = planePoints[i];
				v.x = planePointsArray[j++];
				v.y = planePointsArray[j++];
				v.z = planePointsArray[j++];
			}

			planes[0].set(planePoints[1], planePoints[0], planePoints[2]);
			planes[1].set(planePoints[4], planePoints[5], planePoints[7]);
			planes[2].set(planePoints[0], planePoints[4], planePoints[3]);
			planes[3].set(planePoints[5], planePoints[1], planePoints[6]);
			planes[4].set(planePoints[2], planePoints[3], planePoints[6]);
			planes[5].set(planePoints[4], planePoints[0], planePoints[1]);
		}

		/** Returns whether the point is in the frustum.
		* 
		* @param point The point
		* @return Whether the point is in the frustum. */
		public bool pointInFrustum(FPVector3 point)
		{
			for (int i = 0; i < planes.Length; i++)
			{
				DGPlaneSide result = planes[i].testPoint(point);
				if (result == DGPlaneSide.Back) return false;
			}

			return true;
		}

		/** Returns whether the point is in the frustum.
		* 
		* @param x The X coordinate of the point
		* @param y The Y coordinate of the point
		* @param z The Z coordinate of the point
		* @return Whether the point is in the frustum. */
		public bool pointInFrustum(FP x, FP y, FP z)
		{
			for (int i = 0; i < planes.Length; i++)
			{
				DGPlaneSide result = planes[i].testPoint(x, y, z);
				if (result == DGPlaneSide.Back) return false;
			}

			return true;
		}

		/** Returns whether the given sphere is in the frustum.
		* 
		* @param center The center of the sphere
		* @param radius The radius of the sphere
		* @return Whether the sphere is in the frustum */
		public bool sphereInFrustum(FPVector3 center, FP radius)
		{
			for (int i = 0; i < 6; i++)
				if ((planes[i].normal.x * center.x + planes[i].normal.y * center.y + planes[i].normal.z * center.z) <
					(-radius
					 - planes[i].d))
					return false;
			return true;
		}

		/** Returns whether the given sphere is in the frustum.
		* 
		* @param x The X coordinate of the center of the sphere
		* @param y The Y coordinate of the center of the sphere
		* @param z The Z coordinate of the center of the sphere
		* @param radius The radius of the sphere
		* @return Whether the sphere is in the frustum */
		public bool sphereInFrustum(FP x, FP y, FP z, FP radius)
		{
			for (int i = 0; i < 6; i++)
				if ((planes[i].normal.x * x + planes[i].normal.y * y + planes[i].normal.z * z) < (-radius - planes[i].d))
					return false;
			return true;
		}

		/** Returns whether the given sphere is in the frustum not checking whether it is behind the near and far clipping plane.
		* 
		* @param center The center of the sphere
		* @param radius The radius of the sphere
		* @return Whether the sphere is in the frustum */
		public bool sphereInFrustumWithoutNearFar(FPVector3 center, FP radius)
		{
			for (int i = 2; i < 6; i++)
				if ((planes[i].normal.x * center.x + planes[i].normal.y * center.y + planes[i].normal.z * center.z) <
					(-radius
					 - planes[i].d))
					return false;
			return true;
		}

		/** Returns whether the given sphere is in the frustum not checking whether it is behind the near and far clipping plane.
		* 
		* @param x The X coordinate of the center of the sphere
		* @param y The Y coordinate of the center of the sphere
		* @param z The Z coordinate of the center of the sphere
		* @param radius The radius of the sphere
		* @return Whether the sphere is in the frustum */
		public bool sphereInFrustumWithoutNearFar(FP x, FP y, FP z, FP radius)
		{
			for (int i = 2; i < 6; i++)
				if ((planes[i].normal.x * x + planes[i].normal.y * y + planes[i].normal.z * z) < (-radius - planes[i].d))
					return false;
			return true;
		}

		/** Returns whether the given {@link BoundingBox} is in the frustum.
		* 
		* @param bounds The bounding box
		* @return Whether the bounding box is in the frustum */
		public bool boundsInFrustum(FPBoundingBox bounds)
		{
			for (int i = 0, len2 = planes.Length; i < len2; i++)
			{
				if (planes[i].testPoint(bounds.getCorner000(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(bounds.getCorner001(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(bounds.getCorner010(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(bounds.getCorner011(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(bounds.getCorner100(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(bounds.getCorner101(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(bounds.getCorner110(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(bounds.getCorner111(ref tmpV)) != DGPlaneSide.Back) continue;
				return false;
			}

			return true;
		}

		/** Returns whether the given bounding box is in the frustum.
		* @return Whether the bounding box is in the frustum */
		public bool boundsInFrustum(FPVector3 center, FPVector3 dimensions)
		{
			return boundsInFrustum(center.x, center.y, center.z, dimensions.x / 2,
				dimensions.y / 2, dimensions.z / 2);
		}

		/** Returns whether the given bounding box is in the frustum.
		* @return Whether the bounding box is in the frustum */
		public bool boundsInFrustum(FP x, FP y, FP z, FP halfWidth,
			FP halfHeight, FP halfDepth)
		{
			for (int i = 0, len2 = planes.Length; i < len2; i++)
			{
				if (planes[i].testPoint(x + halfWidth, y + halfHeight, z + halfDepth) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(x + halfWidth, y + halfHeight, z - halfDepth) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(x + halfWidth, y - halfHeight, z + halfDepth) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(x + halfWidth, y - halfHeight, z - halfDepth) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(x - halfWidth, y + halfHeight, z + halfDepth) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(x - halfWidth, y + halfHeight, z - halfDepth) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(x - halfWidth, y - halfHeight, z + halfDepth) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(x - halfWidth, y - halfHeight, z - halfDepth) != DGPlaneSide.Back) continue;
				return false;
			}

			return true;
		}

		/** Returns whether the given {@link OrientedBoundingBox} is in the frustum.
		*
		* @param obb The oriented bounding box
		* @return Whether the oriented bounding box is in the frustum */
		public bool boundsInFrustum(FPOrientedBoundingBox obb)
		{
			for (int i = 0, len2 = planes.Length; i < len2; i++)
			{
				if (planes[i].testPoint(obb.getCorner000(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(obb.getCorner001(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(obb.getCorner010(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(obb.getCorner011(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(obb.getCorner100(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(obb.getCorner101(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(obb.getCorner110(ref tmpV)) != DGPlaneSide.Back) continue;
				if (planes[i].testPoint(obb.getCorner111(ref tmpV)) != DGPlaneSide.Back) continue;
				return false;
			}

			return true;
		}
	}
}
