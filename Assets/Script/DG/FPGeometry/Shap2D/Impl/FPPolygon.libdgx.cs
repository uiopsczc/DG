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
    public class FPPolygon : IFPShape2D
    {
        private FP[] localVertices;
        private FP[] worldVertices;
        private FP x, y;
        private FP originX, originY;
        private FP rotation;
        private FP scaleX = 1, scaleY = 1;
        private bool _dirty = true;
        private FPRectangle bounds;

        /** Constructs a new polygon with no vertices. */
        public FPPolygon()
        {
            localVertices = new FP[0];
        }

        /** Constructs a new polygon from a float array of parts of vertex points.
         *
         * @param vertices an array where every even element represents the horizontal part of a point, and the following element
         *           representing the vertical part
         *
         * @throws IllegalArgumentException if less than 6 elements, representing 3 points, are provided */
        public FPPolygon(FP[] vertices)
        {
            if (vertices.Length < 6) throw new Exception("polygons must contain at least 3 points.");
            localVertices = vertices;
        }

        /** Returns the polygon's local vertices without scaling or rotation and without being offset by the polygon position. */
        public FP[] getVertices()
        {
            return localVertices;
        }

        /** Calculates and returns the vertices of the polygon after scaling, rotation, and positional translations have been applied,
         * as they are position within the world.
         *
         * @return vertices scaled, rotated, and offset by the polygon position. */
        public FP[] getTransformedVertices()
        {
            if (!_dirty) return this.worldVertices;
            _dirty = false;

            FP[] localVertices = this.localVertices;
            if (this.worldVertices == null || this.worldVertices.Length != localVertices.Length)
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

        /** Sets the origin point to which all of the polygon's local vertices are relative to. */
        public void setOrigin(FP originX, FP originY)
        {
            this.originX = originX;
            this.originY = originY;
            _dirty = true;
        }

        /** Sets the polygon's position within the world. */
        public void setPosition(FP x, FP y)
        {
            this.x = x;
            this.y = y;
            _dirty = true;
        }

        /** Sets the polygon's local vertices relative to the origin point, without any scaling, rotating or translations being
         * applied.
         *
         * @param vertices float array where every even element represents the x-coordinate of a vertex, and the proceeding element
         *           representing the y-coordinate.
         * @throws IllegalArgumentException if less than 6 elements, representing 3 points, are provided */
        public void setVertices(FP[] vertices)
        {
            if (vertices.Length < 6) throw new Exception("polygons must contain at least 3 points.");
            localVertices = vertices;
            _dirty = true;
        }

        /** Set vertex position
         * @param vertexNum min=0, max=vertices.length/2-1
         * @throws IllegalArgumentException if vertex doesnt exist */
        public void setVertex(int vertexNum, FP x, FP y)
        {
            if (vertexNum < 0 || vertexNum > localVertices.Length / 2 - 1)
                throw new Exception("the vertex " + vertexNum + " doesn't exist");
            localVertices[2 * vertexNum] = x;
            localVertices[2 * vertexNum + 1] = y;
            _dirty = true;
        }

        /** Translates the polygon's position by the specified horizontal and vertical amounts. */
        public void translate(FP x, FP y)
        {
            this.x += x;
            this.y += y;
            _dirty = true;
        }

        /** Sets the polygon to be rotated by the supplied degrees. */
        public void setRotation(FP degrees)
        {
            rotation = degrees;
            _dirty = true;
        }

        /** Applies additional rotation to the polygon by the supplied degrees. */
        public void rotate(FP degrees)
        {
            rotation += degrees;
            _dirty = true;
        }

        /** Sets the amount of scaling to be applied to the polygon. */
        public void setScale(FP scaleX, FP scaleY)
        {
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            _dirty = true;
        }

        /** Applies additional scaling to the polygon by the supplied amount. */
        public void scale(FP amount)
        {
            scaleX += amount;
            scaleY += amount;
            _dirty = true;
        }

        /** Sets the polygon's world vertices to be recalculated when calling {@link #getTransformedVertices()
         * getTransformedVertices}. */
        public void dirty()
        {
            _dirty = true;
        }

        /** Returns the area contained within the polygon. */
        public FP area()
        {
            FP[] vertices = getTransformedVertices();
            return FPGeometryUtils.polygonArea(vertices, 0, vertices.Length);
        }

        public int getVertexCount()
        {
            return localVertices.Length / 2;
        }

        /** @return Position(transformed) of vertex */
        public FPVector2 getVertex(int vertexNum, FPVector2 pos)
        {
            if (vertexNum < 0 || vertexNum > getVertexCount())
                throw new Exception("the vertex " + vertexNum + " doesn't exist");
            FP[] vertices = getTransformedVertices();
            return pos.set(vertices[2 * vertexNum], vertices[2 * vertexNum + 1]);
        }

        public FPVector2 getCentroid(FPVector2 centroid)
        {
            FP[] vertices = getTransformedVertices();
            return FPGeometryUtils.polygonCentroid(vertices, 0, vertices.Length, centroid);
        }

        /** Returns an axis-aligned bounding box of this polygon.
         *
         * Note the returned Rectangle is cached in this polygon, and will be reused if this Polygon is changed.
         *
         * @return this polygon's bounding box {@link Rectangle} */
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

        /** Returns whether an x, y pair is contained within the polygon. */
        public bool contains(FP x, FP y)
        {
            FP[] vertices = getTransformedVertices();
            int numFloats = vertices.Length;
            int intersects = 0;

            for (int i = 0; i < numFloats; i += 2)
            {
                FP x1 = vertices[i];
                FP y1 = vertices[i + 1];
                FP x2 = vertices[(i + 2) % numFloats];
                FP y2 = vertices[(i + 3) % numFloats];
                if (((y1 <= y && y < y2) || (y2 <= y && y < y1)) && x < ((x2 - x1) / (y2 - y1) * (y - y1) + x1))
                    intersects++;
            }

            return (intersects & 1) == 1;
        }

        public bool contains(FPVector2 point)
        {
            return contains(point.x, point.y);
        }

        /** Returns the x-coordinate of the polygon's position within the world. */
        public FP getX()
        {
            return x;
        }

        /** Returns the y-coordinate of the polygon's position within the world. */
        public FP getY()
        {
            return y;
        }

        /** Returns the x-coordinate of the polygon's origin point. */
        public FP getOriginX()
        {
            return originX;
        }

        /** Returns the y-coordinate of the polygon's origin point. */
        public FP getOriginY()
        {
            return originY;
        }

        /** Returns the total rotation applied to the polygon. */
        public FP getRotation()
        {
            return rotation;
        }

        /** Returns the total horizontal scaling applied to the polygon. */
        public FP getScaleX()
        {
            return scaleX;
        }

        /** Returns the total vertical scaling applied to the polygon. */
        public FP getScaleY()
        {
            return scaleY;
        }
    }
}