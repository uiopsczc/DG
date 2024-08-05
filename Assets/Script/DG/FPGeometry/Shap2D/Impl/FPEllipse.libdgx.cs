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

namespace DG
{
    public struct FPEllipse : IFPShape2D
    {
        public FP x, y;
        public FP width, height;


        /** Copy constructor
         *
         * @param ellipse Ellipse to construct a copy of. */
        public FPEllipse(FPEllipse ellipse)
        {
            x = ellipse.x;
            y = ellipse.y;
            width = ellipse.width;
            height = ellipse.height;
        }

        /** Constructs a new ellipse
         *
         * @param x X coordinate
         * @param y Y coordinate
         * @param width the width of the ellipse
         * @param height the height of the ellipse */
        public FPEllipse(FP x, FP y, FP width, FP height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /** Costructs a new ellipse
         *
         * @param position Position vector
         * @param width the width of the ellipse
         * @param height the height of the ellipse */
        public FPEllipse(FPVector2 position, FP width, FP height)
        {
            x = position.x;
            y = position.y;
            this.width = width;
            this.height = height;
        }

        public FPEllipse(FPVector2 position, FPVector2 size)
        {
            x = position.x;
            y = position.y;
            width = size.x;
            height = size.y;
        }

        /** Constructs a new {@link Ellipse} from the position and radius of a {@link Circle} (since circles are special cases of
         * ellipses).
         * @param circle The circle to take the values of */
        public FPEllipse(FPCircle circle)
        {
            x = circle.x;
            y = circle.y;
            width = circle.radius * 2f;
            height = circle.radius * 2f;
        }

        /** Checks whether or not this ellipse contains the given point.
         *
         * @param x X coordinate
         * @param y Y coordinate
         *
         * @return true if this ellipse contains the given point; false otherwise. */
        public bool contains(FP x, FP y)
        {
            x -= this.x;
            y -= this.y;

            return (x * x) / (width * 0.5f * width * 0.5f) +
                (y * y) / (height * 0.5f * height * 0.5f) <= 1.0f;
        }

        /** Checks whether or not this ellipse contains the given point.
         *
         * @param point Position vector
         *
         * @return true if this ellipse contains the given point; false otherwise. */
        public bool contains(FPVector2 point)
        {
            return contains(point.x, point.y);
        }

        /** Sets a new position and size for this ellipse.
         *
         * @param x X coordinate
         * @param y Y coordinate
         * @param width the width of the ellipse
         * @param height the height of the ellipse */
        public void set(FP x, FP y, FP width, FP height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /** Sets a new position and size for this ellipse based upon another ellipse.
         *
         * @param ellipse The ellipse to copy the position and size of. */
        public void set(FPEllipse ellipse)
        {
            x = ellipse.x;
            y = ellipse.y;
            width = ellipse.width;
            height = ellipse.height;
        }

        public void set(FPCircle circle)
        {
            x = circle.x;
            y = circle.y;
            width = circle.radius * 2f;
            height = circle.radius * 2f;
        }

        public void set(FPVector2 position, FPVector2 size)
        {
            x = position.x;
            y = position.y;
            width = size.x;
            height = size.y;
        }

        /** Sets the x and y-coordinates of ellipse center from a {@link Vector2}.
         * @param position The position vector
         * @return this ellipse for chaining */
        public FPEllipse setPosition(FPVector2 position)
        {
            x = position.x;
            y = position.y;

            return this;
        }

        /** Sets the x and y-coordinates of ellipse center
         * @param x The x-coordinate
         * @param y The y-coordinate
         * @return this ellipse for chaining */
        public FPEllipse setPosition(FP x, FP y)
        {
            this.x = x;
            this.y = y;

            return this;
        }

        /** Sets the width and height of this ellipse
         * @param width The width
         * @param height The height
         * @return this ellipse for chaining */
        public FPEllipse setSize(FP width, FP height)
        {
            this.width = width;
            this.height = height;

            return this;
        }

        /** @return The area of this {@link Ellipse} as {@link MathUtils#PI} * {@link Ellipse#width} * {@link Ellipse#height} */
        public FP area()
        {
            return FPMath.PI * (width * height) / 4;
        }

        /** Approximates the circumference of this {@link Ellipse}. Oddly enough, the circumference of an ellipse is actually difficult
         * to compute exactly.
         * @return The Ramanujan approximation to the circumference of an ellipse if one dimension is at least three times longer than
         *         the other, else the simpler approximation */
        public FP circumference()
        {
            FP a = width / 2;
            FP b = height / 2;
            if (a * 3 > b || b * 3 > a)
            {
                // If one dimension is three times as long as the other...
                return FPMath.PI * ((3 * (a + b)) - FPMath.Sqrt((3 * a + b) * (a + 3 * b)));
            }

            // We can use the simpler approximation, then
            return FPMath.TWO_PI * FPMath.Sqrt((a * a + b * b) / 2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var other = (FPEllipse)obj;
            return Equals(other);
        }

        public bool Equals(FPEllipse other)
        {
            return other.x == x && other.y == y && other.width == width && other.height == height;
        }

        public override int GetHashCode()
        {
            int prime = 53;
            int result = 1;
            result = prime * result + x.GetHashCode();
            result = prime * result + y.GetHashCode();
            result = prime * result + width.GetHashCode();
            result = prime * result + height.GetHashCode();
            return result;
        }
    }
}