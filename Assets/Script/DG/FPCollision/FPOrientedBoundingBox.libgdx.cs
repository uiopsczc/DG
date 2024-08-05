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

namespace DG
{
    public class FPOrientedBoundingBox
    {
        private static FPVector3[] tempAxes = new FPVector3[15];
        private static FPVector3[] tempVertices = new FPVector3[8];
        private static FPVector3[] tmpVectors = new FPVector3[9];

        /** Bounds used as size. */
        private FPBoundingBox bounds = new();

        /** Transform matrix. */
        public FPMatrix4x4 transform;

        private FPMatrix4x4 inverseTransform;

        private FPVector3[] axes = new FPVector3[3];
        private FPVector3[] vertices = new FPVector3[8];

        /** Constructs a new oriented bounding box with the minimum and maximum vector set to zeros. */
        public FPOrientedBoundingBox()
        {
            bounds.clr();
            init();
        }

        /** Constructs a new oriented bounding box from the given bounding box.
         *
         * @param bounds The bounding box to copy */
        public FPOrientedBoundingBox(FPBoundingBox bounds)
        {
            this.bounds.set(bounds.min, bounds.max);
            init();
        }

        /** Constructs a new oriented bounding box from the given bounding box and transform.
         *
         * @param bounds The bounding box to copy
         * @param transform The transformation matrix to copy */
        public FPOrientedBoundingBox(FPBoundingBox bounds, FPMatrix4x4 transform)
        {
            this.bounds.set(bounds.min, bounds.max);
            this.transform.set(transform);
            init();
        }

        private void init()
        {
            for (int i = 0; i < axes.Length; i++)
            {
                axes[i] = new FPVector3();
            }

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new FPVector3();
            }

            update();
        }

        public FPVector3[] getVertices()
        {
            return vertices;
        }

        /** Get the current bounds. Call {@link #update()} if you manually change this bounding box. */
        public FPBoundingBox getBounds()
        {
            return bounds;
        }

        /** Sets the base bounds of the oriented bounding box as the bounds given, the transform is applied to the vertices.
         *
         * @param bounds The bounding box to copy */
        public void setBounds(FPBoundingBox bounds)
        {
            this.bounds.set(bounds);
            vertices[0b000] = bounds.getCorner000(ref vertices[0b000]).mul(transform);
            vertices[0b001] = bounds.getCorner001(ref vertices[0b001]).mul(transform);
            vertices[0b010] = bounds.getCorner010(ref vertices[0b010]).mul(transform);
            vertices[0b011] = bounds.getCorner011(ref vertices[0b011]).mul(transform);
            vertices[0b100] = bounds.getCorner100(ref vertices[0b100]).mul(transform);
            vertices[0b101] = bounds.getCorner101(ref vertices[0b101]).mul(transform);
            vertices[0b110] = bounds.getCorner110(ref vertices[0b110]).mul(transform);
            vertices[0b111] = bounds.getCorner111(ref vertices[0b111]).mul(transform);
        }

        /** Get the current transformation matrix. Call {@link #update()} if you manually change this matrix. */
        public FPMatrix4x4 getTransform()
        {
            return transform;
        }

        public void setTransform(FPMatrix4x4 transform)
        {
            this.transform.set(transform);
            update();
        }

        public FPOrientedBoundingBox set(FPBoundingBox bounds, FPMatrix4x4 transform)
        {
            setBounds(bounds);
            setTransform(transform);
            return this;
        }

        public FPVector3 getCorner000(ref FPVector3 outCorner)
        {
            if (outCorner == FPVector3.Null)
                return vertices[0b000].cpy();
            return outCorner.set(vertices[0b000]);
            ;
        }

        public FPVector3 getCorner000()
        {
            return getCorner000(ref FPVector3.Null);
        }

        public FPVector3 getCorner001(ref FPVector3 outCorner)
        {
            if (outCorner == FPVector3.Null)
                return vertices[0b001].cpy();
            return outCorner.set(vertices[0b001]);
        }

        public FPVector3 getCorner001()
        {
            return getCorner001(ref FPVector3.Null);
        }


        public FPVector3 getCorner010(ref FPVector3 outCorner)
        {
            if (outCorner == FPVector3.Null)
                return vertices[0b010].cpy();
            return outCorner.set(vertices[0b010]);
        }

        public FPVector3 getCorner010()
        {
            return getCorner010(ref FPVector3.Null);
        }

        public FPVector3 getCorner011(ref FPVector3 outCorner)
        {
            if (outCorner == FPVector3.Null)
                return vertices[0b011].cpy();
            return outCorner.set(vertices[0b011]);
        }

        public FPVector3 getCorner011()
        {
            return getCorner011(ref FPVector3.Null);
        }

        public FPVector3 getCorner100(ref FPVector3 outCorner)
        {
            if (outCorner == FPVector3.Null)
                return vertices[0b100].cpy();
            return outCorner.set(vertices[0b100]);
        }

        public FPVector3 getCorner100()
        {
            return getCorner100(ref FPVector3.Null);
        }

        public FPVector3 getCorner101(ref FPVector3 outCorner)
        {
            if (outCorner == FPVector3.Null)
                return vertices[0b110].cpy();
            return outCorner.set(vertices[0b110]);
        }

        public FPVector3 getCorner110(ref FPVector3 outCorner)
        {
            return getCorner110(ref FPVector3.Null);
        }

        public FPVector3 getCorner111(ref FPVector3 outCorner)
        {
            if (outCorner == FPVector3.Null)
                return vertices[0b111].cpy();
            return outCorner.set(vertices[0b111]);
        }

        public FPVector3 getCorner111()
        {
            return getCorner111(ref FPVector3.Null);
        }

        /** Returns whether the given vector is contained in this oriented bounding box.
         * @param v The vector
         * @return Whether the vector is contained or not. */
        public bool contains(FPVector3 v)
        {
            return contains(v, inverseTransform);
        }

        private bool contains(FPVector3 v, FPMatrix4x4 invTransform)
        {
            FPVector3 localV = tmpVectors[0].set(v).mul(invTransform);
            return bounds.contains(localV);
        }

        /** Returns whether the given bounding box is contained in this oriented bounding box.
         * @param b The bounding box
         * @return Whether the given bounding box is contained */
        public bool contains(FPBoundingBox b)
        {
            FPVector3 tmpVector = tmpVectors[0];
            return contains(b.getCorner000(ref tmpVector), inverseTransform) && contains(b.getCorner001(ref tmpVector),
                                                                                 inverseTransform)
                                                                             && contains(b.getCorner010(ref tmpVector),
                                                                                 inverseTransform) &&
                                                                             contains(b.getCorner011(ref tmpVector),
                                                                                 inverseTransform)
                                                                             && contains(b.getCorner100(ref tmpVector),
                                                                                 inverseTransform) &&
                                                                             contains(b.getCorner101(ref tmpVector),
                                                                                 inverseTransform)
                                                                             && contains(b.getCorner110(ref tmpVector),
                                                                                 inverseTransform) &&
                                                                             contains(b.getCorner111(ref tmpVector),
                                                                                 inverseTransform);
        }

        /** Returns whether the given oriented bounding box is contained in this oriented bounding box.
         * @param obb The oriented bounding box
         * @return Whether the given oriented bounding box is contained */
        public bool contains(FPOrientedBoundingBox obb)
        {
            return contains(obb.getCorner000(ref tmpVectors[0]), inverseTransform)
                   && contains(obb.getCorner001(ref tmpVectors[0]), inverseTransform)
                   && contains(obb.getCorner010(ref tmpVectors[0]), inverseTransform)
                   && contains(obb.getCorner011(ref tmpVectors[0]), inverseTransform)
                   && contains(obb.getCorner100(ref tmpVectors[0]), inverseTransform)
                   && contains(obb.getCorner101(ref tmpVectors[0]), inverseTransform)
                   && contains(obb.getCorner110(ref tmpVectors[0]), inverseTransform)
                   && contains(obb.getCorner111(ref tmpVectors[0]), inverseTransform);
        }

        /** Returns whether the given bounding box is intersecting this oriented bounding box (at least one point in).
        * @param b The bounding box
        * @return Whether the given bounding box is intersected */
        public bool intersects(FPBoundingBox b)
        {
            FPVector3[] aAxes = axes;

            tempAxes[0] = aAxes[0];
            tempAxes[1] = aAxes[1];
            tempAxes[2] = aAxes[2];
            tempAxes[3] = FPVector3.X;
            tempAxes[4] = FPVector3.Y;
            tempAxes[5] = FPVector3.Z;
            tempAxes[6] = tmpVectors[0].set(aAxes[0]).crs(FPVector3.X);
            tempAxes[7] = tmpVectors[1].set(aAxes[0]).crs(FPVector3.Y);
            tempAxes[8] = tmpVectors[2].set(aAxes[0]).crs(FPVector3.Z);
            tempAxes[9] = tmpVectors[3].set(aAxes[1]).crs(FPVector3.X);
            tempAxes[10] = tmpVectors[4].set(aAxes[1]).crs(FPVector3.Y);
            tempAxes[11] = tmpVectors[5].set(aAxes[1]).crs(FPVector3.Z);
            tempAxes[12] = tmpVectors[6].set(aAxes[2]).crs(FPVector3.X);
            tempAxes[13] = tmpVectors[7].set(aAxes[2]).crs(FPVector3.Y);
            tempAxes[14] = tmpVectors[8].set(aAxes[2]).crs(FPVector3.Z);

            FPVector3[] aVertices = getVertices();
            FPVector3[] bVertices = getVertices(b);

            return FPIntersector.hasOverlap(tempAxes, aVertices, bVertices);
        }

        /** Returns whether the given oriented bounding box is intersecting this oriented bounding box (at least one point in).
         * @param obb The oriented bounding box
         * @return Whether the given bounding box is intersected */
        public bool intersects(FPOrientedBoundingBox obb)
        {
            FPVector3[] aAxes = axes;
            FPVector3[] bAxes = obb.axes;

            tempAxes[0] = aAxes[0];
            tempAxes[1] = aAxes[1];
            tempAxes[2] = aAxes[2];
            tempAxes[3] = bAxes[0];
            tempAxes[4] = bAxes[1];
            tempAxes[5] = bAxes[2];
            tempAxes[6] = tmpVectors[0].set(aAxes[0]).crs(bAxes[0]);
            tempAxes[7] = tmpVectors[1].set(aAxes[0]).crs(bAxes[1]);
            tempAxes[8] = tmpVectors[2].set(aAxes[0]).crs(bAxes[2]);
            tempAxes[9] = tmpVectors[3].set(aAxes[1]).crs(bAxes[0]);
            tempAxes[10] = tmpVectors[4].set(aAxes[1]).crs(bAxes[1]);
            tempAxes[11] = tmpVectors[5].set(aAxes[1]).crs(bAxes[2]);
            tempAxes[12] = tmpVectors[6].set(aAxes[2]).crs(bAxes[0]);
            tempAxes[13] = tmpVectors[7].set(aAxes[2]).crs(bAxes[1]);
            tempAxes[14] = tmpVectors[8].set(aAxes[2]).crs(bAxes[2]);

            return FPIntersector.hasOverlap(tempAxes, vertices, obb.vertices);
        }

        private FPVector3[] getVertices(FPBoundingBox b)
        {
            b.getCorner000(ref tempVertices[0b000]);
            b.getCorner001(ref tempVertices[0b001]);
            b.getCorner010(ref tempVertices[0b010]);
            b.getCorner011(ref tempVertices[0b011]);
            b.getCorner100(ref tempVertices[0b100]);
            b.getCorner101(ref tempVertices[0b101]);
            b.getCorner110(ref tempVertices[0b110]);
            b.getCorner111(ref tempVertices[0b111]);
            return tempVertices;
        }

        public void mul(FPMatrix4x4 transform)
        {
            this.transform.mul(transform);
            update();
        }

        private void update()
        {
            // Update vertices
            vertices[0b000] = bounds.getCorner000(ref vertices[0b000]).mul(transform);
            vertices[0b001] = bounds.getCorner001(ref vertices[0b001]).mul(transform);
            vertices[0b010] = bounds.getCorner010(ref vertices[0b010]).mul(transform);
            vertices[0b011] = bounds.getCorner011(ref vertices[0b011]).mul(transform);
            vertices[0b100] = bounds.getCorner100(ref vertices[0b100]).mul(transform);
            vertices[0b101] = bounds.getCorner101(ref vertices[0b101]).mul(transform);
            vertices[0b110] = bounds.getCorner110(ref vertices[0b110]).mul(transform);
            vertices[0b111] = bounds.getCorner111(ref vertices[0b111]).mul(transform);

            axes[0] = axes[0].set(transform.m00, transform.m10, transform.m20).nor();
            axes[1] = axes[1].set(transform.m01, transform.m11, transform.m21).nor();
            axes[2] = axes[2].set(transform.m02, transform.m12, transform.m22).nor();

            inverseTransform = inverseTransform.set(transform).inv();
        }
    }
}