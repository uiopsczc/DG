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

namespace DG
{
	public class FPPolyline : IFPShape2D
	{
		private FP[] localVertices;
		private FP[] worldVertices;
		private FP x, y;
		private FP originX, originY;
		private FP rotation;
		private FP scaleX = 1, scaleY = 1;
		private FP length;
		private FP scaledLength;
		private bool _calculateScaledLength = true;
		private bool _calculateLength = true;
		private bool _dirty = true;
		private FPRectangle bounds;

		public FPPolyline()
		{
			localVertices = new FP[0];
		}

		public FPPolyline(FP[] vertices)
		{
			if (vertices.Length < 4) throw new Exception("polylines must contain at least 2 points.");
			localVertices = vertices;
		}

		/** Returns vertices without scaling or rotation and without being offset by the polyline position. */
		public FP[] getVertices()
		{
			return localVertices;
		}

		/** Returns vertices scaled, rotated, and offset by the polygon position. */
		public FP[] getTransformedVertices()
		{
			if (!_dirty) return this.worldVertices;
			_dirty = false;

			FP[] localVertices = this.localVertices;
			if (this.worldVertices == null || this.worldVertices.Length < localVertices.Length)
				this.worldVertices = new FP[localVertices.Length];

			FP[] worldVertices = this.worldVertices;
			FP positionX = x;
			FP positionY = y;
			FP originX = this.originX;
			FP originY = this.originY;
			FP scaleX = this.scaleX;
			FP scaleY = this.scaleY;
			bool scale = scaleX != 1 || scaleY != 1;
			FP rotation = this.rotation;
			FP cos = FPMath.CosDeg(rotation);
			FP sin = FPMath.SinDeg(rotation);

			for (int i = 0, n = localVertices.Length; i < n; i += 2)
			{
				FP x = localVertices[i] - originX;
				FP y = localVertices[i + 1] - originY;

				// scale if needed
				if (scale)
				{
					x *= scaleX;
					y *= scaleY;
				}

				// rotate if needed
				if (rotation != 0)
				{
					FP oldX = x;
					x = cos * x - sin * y;
					y = sin * oldX + cos * y;
				}

				worldVertices[i] = positionX + x + originX;
				worldVertices[i + 1] = positionY + y + originY;
			}

			return worldVertices;
		}

		/** Returns the euclidean length of the polyline without scaling */
		public FP getLength()
		{
			if (!_calculateLength) return length;
			_calculateLength = false;

			length = 0;
			for (int i = 0, n = localVertices.Length - 2; i < n; i += 2)
			{
				FP x = localVertices[i + 2] - localVertices[i];
				FP y = localVertices[i + 1] - localVertices[i + 3];
				length += FPMath.Sqrt(x * x + y * y);
			}

			return length;
		}

		/** Returns the euclidean length of the polyline */
		public FP getScaledLength()
		{
			if (!_calculateScaledLength) return scaledLength;
			_calculateScaledLength = false;

			scaledLength = 0;
			for (int i = 0, n = localVertices.Length - 2; i < n; i += 2)
			{
				FP x = localVertices[i + 2] * scaleX - localVertices[i] * scaleX;
				FP y = localVertices[i + 1] * scaleY - localVertices[i + 3] * scaleY;
				scaledLength += FPMath.Sqrt(x * x + y * y);
			}

			return scaledLength;
		}

		public FP getX()
		{
			return x;
		}

		public FP getY()
		{
			return y;
		}

		public FP getOriginX()
		{
			return originX;
		}

		public FP getOriginY()
		{
			return originY;
		}

		public FP getRotation()
		{
			return rotation;
		}

		public FP getScaleX()
		{
			return scaleX;
		}

		public FP getScaleY()
		{
			return scaleY;
		}

		public void setOrigin(FP originX, FP originY)
		{
			this.originX = originX;
			this.originY = originY;
			_dirty = true;
		}

		public void setPosition(FP x, FP y)
		{
			this.x = x;
			this.y = y;
			_dirty = true;
		}

		public void setVertices(FP[] vertices)
		{
			if (vertices.Length < 4) throw new Exception("polylines must contain at least 2 points.");
			localVertices = vertices;
			_dirty = true;
		}

		public void setRotation(FP degrees)
		{
			rotation = degrees;
			_dirty = true;
		}

		public void rotate(FP degrees)
		{
			rotation += degrees;
			_dirty = true;
		}

		public void setScale(FP scaleX, FP scaleY)
		{
			this.scaleX = scaleX;
			this.scaleY = scaleY;
			_dirty = true;
			_calculateScaledLength = true;
		}

		public void scale(FP amount)
		{
			scaleX += amount;
			scaleY += amount;
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

		public void translate(FP x, FP y)
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
		public FPRectangle getBoundingRectangle()
		{
			FP[] vertices = getTransformedVertices();

			FP minX = vertices[0];
			FP minY = vertices[1];
			FP maxX = vertices[0];
			FP maxY = vertices[1];

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

		public bool contains(FPVector2 point)
		{
			return false;
		}

		public bool contains(FP x, FP y)
		{
			return false;
		}
	}
}
