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

public class DGPolyline : IDGShape2D
{
	private DGFixedPoint[] localVertices;
	private DGFixedPoint[] worldVertices;
	private DGFixedPoint x, y;
	private DGFixedPoint originX, originY;
	private DGFixedPoint rotation;
	private DGFixedPoint scaleX = (DGFixedPoint) 1, scaleY = (DGFixedPoint) 1;
	private DGFixedPoint length;
	private DGFixedPoint scaledLength;
	private bool _calculateScaledLength = true;
	private bool _calculateLength = true;
	private bool _dirty = true;
	private DGRectangle bounds;

	public DGPolyline()
	{
		this.localVertices = new DGFixedPoint[0];
	}

	public DGPolyline(DGFixedPoint[] vertices)
	{
		if (vertices.Length < 4) throw new Exception("polylines must contain at least 2 points.");
		this.localVertices = vertices;
	}

/** Returns vertices without scaling or rotation and without being offset by the polyline position. */
	public DGFixedPoint[] getVertices()
	{
		return localVertices;
	}

/** Returns vertices scaled, rotated, and offset by the polygon position. */
	public DGFixedPoint[] getTransformedVertices()
	{
		if (!_dirty) return this.worldVertices;
		_dirty = false;

		DGFixedPoint[] localVertices = this.localVertices;
		if (this.worldVertices == null || this.worldVertices.Length < localVertices.Length)
			this.worldVertices = new DGFixedPoint[localVertices.Length];

		DGFixedPoint[] worldVertices = this.worldVertices;
		DGFixedPoint positionX = x;
		DGFixedPoint positionY = y;
		DGFixedPoint originX = this.originX;
		DGFixedPoint originY = this.originY;
		DGFixedPoint scaleX = this.scaleX;
		DGFixedPoint scaleY = this.scaleY;
		bool scale = scaleX != (DGFixedPoint) 1 || scaleY != (DGFixedPoint) 1;
		DGFixedPoint rotation = this.rotation;
		DGFixedPoint cos = DGMath.CosDeg(rotation);
		DGFixedPoint sin = DGMath.SinDeg(rotation);

		for (int i = 0, n = localVertices.Length; i < n; i += 2)
		{
			DGFixedPoint x = localVertices[i] - originX;
			DGFixedPoint y = localVertices[i + 1] - originY;

			// scale if needed
			if (scale)
			{
				x *= scaleX;
				y *= scaleY;
			}

			// rotate if needed
			if (rotation != (DGFixedPoint) 0)
			{
				DGFixedPoint oldX = x;
				x = cos * x - sin * y;
				y = sin * oldX + cos * y;
			}

			worldVertices[i] = positionX + x + originX;
			worldVertices[i + 1] = positionY + y + originY;
		}

		return worldVertices;
	}

/** Returns the euclidean length of the polyline without scaling */
	public DGFixedPoint getLength()
	{
		if (!_calculateLength) return length;
		_calculateLength = false;

		length = (DGFixedPoint) 0;
		for (int i = 0, n = localVertices.Length - 2; i < n; i += 2)
		{
			DGFixedPoint x = localVertices[i + 2] - localVertices[i];
			DGFixedPoint y = localVertices[i + 1] - localVertices[i + 3];
			length += DGMath.Sqrt(x * x + y * y);
		}

		return length;
	}

/** Returns the euclidean length of the polyline */
	public DGFixedPoint getScaledLength()
	{
		if (!_calculateScaledLength) return scaledLength;
		_calculateScaledLength = false;

		scaledLength = (DGFixedPoint) 0;
		for (int i = 0, n = localVertices.Length - 2; i < n; i += 2)
		{
			DGFixedPoint x = localVertices[i + 2] * scaleX - localVertices[i] * scaleX;
			DGFixedPoint y = localVertices[i + 1] * scaleY - localVertices[i + 3] * scaleY;
			scaledLength += DGMath.Sqrt(x * x + y * y);
		}

		return scaledLength;
	}

	public DGFixedPoint getX()
	{
		return x;
	}

	public DGFixedPoint getY()
	{
		return y;
	}

	public DGFixedPoint getOriginX()
	{
		return originX;
	}

	public DGFixedPoint getOriginY()
	{
		return originY;
	}

	public DGFixedPoint getRotation()
	{
		return rotation;
	}

	public DGFixedPoint getScaleX()
	{
		return scaleX;
	}

	public DGFixedPoint getScaleY()
	{
		return scaleY;
	}

	public void setOrigin(DGFixedPoint originX, DGFixedPoint originY)
	{
		this.originX = originX;
		this.originY = originY;
		_dirty = true;
	}

	public void setPosition(DGFixedPoint x, DGFixedPoint y)
	{
		this.x = x;
		this.y = y;
		_dirty = true;
	}

	public void setVertices(DGFixedPoint[] vertices)
	{
		if (vertices.Length < 4) throw new Exception("polylines must contain at least 2 points.");
		this.localVertices = vertices;
		_dirty = true;
	}

	public void setRotation(DGFixedPoint degrees)
	{
		this.rotation = degrees;
		_dirty = true;
	}

	public void rotate(DGFixedPoint degrees)
	{
		rotation += degrees;
		_dirty = true;
	}

	public void setScale(DGFixedPoint scaleX, DGFixedPoint scaleY)
	{
		this.scaleX = scaleX;
		this.scaleY = scaleY;
		_dirty = true;
		_calculateScaledLength = true;
	}

	public void scale(DGFixedPoint amount)
	{
		this.scaleX += amount;
		this.scaleY += amount;
		_dirty = true;
		_calculateScaledLength = true;
	}

	public void calculateLength()
	{
		_calculateLength = true;
	}

	public void calculateScaledLength()
	{
		_calculateScaledLength = true;
	}

	public void dirty()
	{
		_dirty = true;
	}

	public void translate(DGFixedPoint x, DGFixedPoint y)
	{
		this.x += x;
		this.y += y;
		_dirty = true;
	}

/** Returns an axis-aligned bounding box of this polyline.
 *
 * Note the returned Rectangle is cached in this polyline, and will be reused if this Polyline is changed.
 *
 * @return this polyline's bounding box {@link Rectangle} */
	public DGRectangle getBoundingRectangle()
	{
		DGFixedPoint[] vertices = getTransformedVertices();

		DGFixedPoint minX = vertices[0];
		DGFixedPoint minY = vertices[1];
		DGFixedPoint maxX = vertices[0];
		DGFixedPoint maxY = vertices[1];

		int numFloats = vertices.Length;
		for (int i = 2; i < numFloats; i += 2)
		{
			minX = minX > vertices[i] ? vertices[i] : minX;
			minY = minY > vertices[i + 1] ? vertices[i + 1] : minY;
			maxX = maxX < vertices[i] ? vertices[i] : maxX;
			maxY = maxY < vertices[i + 1] ? vertices[i + 1] : maxY;
		}

		bounds = default;
		bounds.x = minX;
		bounds.y = minY;
		bounds.width = maxX - minX;
		bounds.height = maxY - minY;

		return bounds;
	}

	public bool contains(DGVector2 point)
	{
		return false;
	}

	public bool contains(DGFixedPoint x, DGFixedPoint y)
	{
		return false;
	}
}