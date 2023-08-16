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

public partial class DGFrustum
{
	protected static DGVector3[] clipSpacePlanePoints = {new DGVector3(-1, -1, -1), new DGVector3(1, -1, -1),
		new DGVector3(1, 1, -1), new DGVector3(-1, 1, -1), // near clip
		new DGVector3(-1, -1, 1), new DGVector3(1, -1, 1), new DGVector3(1, 1, 1), new DGVector3(-1, 1, 1)}; // far clip
	protected static DGFixedPoint[] _clipSpacePlanePointsArray;

	protected static DGFixedPoint[] clipSpacePlanePointsArray
	{
		get
		{
			if (_clipSpacePlanePointsArray == null)
			{
				_clipSpacePlanePointsArray = new DGFixedPoint[8 * 3];
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

	private static DGVector3 tmpV = new DGVector3();

/** the six clipping planes, near, far, left, right, top, bottom **/
public DGPlane[] planes = new DGPlane[6];

	/** eight points making up the near and far clipping "rectangles". order is counter clockwise, starting at bottom left **/
	public DGVector3[] planePoints = {new DGVector3(), new DGVector3(), new DGVector3(), new DGVector3(), new DGVector3(), new DGVector3(),
		new DGVector3(), new DGVector3()};
	protected DGFixedPoint[] planePointsArray = new DGFixedPoint[8 * 3];

public DGFrustum()
{
	for (int i = 0; i < 6; i++)
		planes[i] = new DGPlane(new DGVector3(), (DGFixedPoint) 0);
}

/** Updates the clipping plane's based on the given inverse combined projection and view matrix, e.g. from an
 * {@link OrthographicCamera} or {@link PerspectiveCamera}.
 * @param inverseProjectionView the combined projection and view matrices. */
public void update(DGMatrix4x4 inverseProjectionView)
{
	Array.Copy(clipSpacePlanePointsArray, 0, planePointsArray, 0, clipSpacePlanePointsArray.Length);
	DGMatrix4x4.prj(inverseProjectionView.getValues(), planePointsArray, 0, 8, 3);
	for (int i = 0, j = 0; i < 8; i++)
	{
		DGVector3 v = planePoints[i];
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
public bool pointInFrustum(DGVector3 point)
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
public bool pointInFrustum(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z)
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
public bool sphereInFrustum(DGVector3 center, DGFixedPoint radius)
{
	for (int i = 0; i < 6; i++)
		if ((planes[i].normal.x * center.x + planes[i].normal.y * center.y + planes[i].normal.z * center.z) < (-radius
			- planes[i].d)) return false;
	return true;
}

/** Returns whether the given sphere is in the frustum.
 * 
 * @param x The X coordinate of the center of the sphere
 * @param y The Y coordinate of the center of the sphere
 * @param z The Z coordinate of the center of the sphere
 * @param radius The radius of the sphere
 * @return Whether the sphere is in the frustum */
public bool sphereInFrustum(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint radius)
{
	for (int i = 0; i < 6; i++)
		if ((planes[i].normal.x * x + planes[i].normal.y * y + planes[i].normal.z * z) < (-radius - planes[i].d)) return false;
	return true;
}

/** Returns whether the given sphere is in the frustum not checking whether it is behind the near and far clipping plane.
 * 
 * @param center The center of the sphere
 * @param radius The radius of the sphere
 * @return Whether the sphere is in the frustum */
public bool sphereInFrustumWithoutNearFar(DGVector3 center, DGFixedPoint radius)
{
	for (int i = 2; i < 6; i++)
		if ((planes[i].normal.x * center.x + planes[i].normal.y * center.y + planes[i].normal.z * center.z) < (-radius
			- planes[i].d)) return false;
	return true;
}

/** Returns whether the given sphere is in the frustum not checking whether it is behind the near and far clipping plane.
 * 
 * @param x The X coordinate of the center of the sphere
 * @param y The Y coordinate of the center of the sphere
 * @param z The Z coordinate of the center of the sphere
 * @param radius The radius of the sphere
 * @return Whether the sphere is in the frustum */
public bool sphereInFrustumWithoutNearFar(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint radius)
{
	for (int i = 2; i < 6; i++)
		if ((planes[i].normal.x * x + planes[i].normal.y * y + planes[i].normal.z * z) < (-radius - planes[i].d)) return false;
	return true;
}

/** Returns whether the given {@link BoundingBox} is in the frustum.
 * 
 * @param bounds The bounding box
 * @return Whether the bounding box is in the frustum */
public bool boundsInFrustum(BoundingBox bounds)
{
	for (int i = 0, len2 = planes.Length; i < len2; i++)
	{
		if (planes[i].testPoint(bounds.getCorner000(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(bounds.getCorner001(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(bounds.getCorner010(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(bounds.getCorner011(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(bounds.getCorner100(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(bounds.getCorner101(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(bounds.getCorner110(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(bounds.getCorner111(tmpV)) != DGPlaneSide.Back) continue;
		return false;
	}

	return true;
}

/** Returns whether the given bounding box is in the frustum.
 * @return Whether the bounding box is in the frustum */
public bool boundsInFrustum(DGVector3 center, DGVector3 dimensions)
{
	return boundsInFrustum(center.x, center.y, center.z, dimensions.x / (DGFixedPoint)2, dimensions.y / (DGFixedPoint)2, dimensions.z / (DGFixedPoint)2);
}

/** Returns whether the given bounding box is in the frustum.
 * @return Whether the bounding box is in the frustum */
public bool boundsInFrustum(DGFixedPoint x, DGFixedPoint y, DGFixedPoint z, DGFixedPoint halfWidth, DGFixedPoint halfHeight, DGFixedPoint halfDepth)
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
public bool boundsInFrustum(OrientedBoundingBox obb)
{
	for (int i = 0, len2 = planes.Length; i < len2; i++)
	{
		if (planes[i].testPoint(obb.getCorner000(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(obb.getCorner001(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(obb.getCorner010(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(obb.getCorner011(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(obb.getCorner100(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(obb.getCorner101(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(obb.getCorner110(tmpV)) != DGPlaneSide.Back) continue;
		if (planes[i].testPoint(obb.getCorner111(tmpV)) != DGPlaneSide.Back) continue;
		return false;
	}

	return true;
}
}