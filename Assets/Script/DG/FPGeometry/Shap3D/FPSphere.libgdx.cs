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
    public struct FPSphere
    {
        public FP radius;

        /** the center of the sphere **/
        public FPVector3 center;


        /** Constructs a sphere with the given center and radius
         * @param center The center
         * @param radius The radius */
        public FPSphere(FPVector3 center, FP radius)
        {
            this.center = center;
            this.radius = radius;
        }

        /** @param sphere the other sphere
         * @return whether this and the other sphere overlap */
        public bool overlaps(FPSphere sphere)
        {
            return center.dst2(sphere.center) < (radius + sphere.radius) * (radius + sphere.radius);
        }

        public FP volume()
        {
            return FP.FOUR_PI_DIV_3 * radius * radius * radius;
        }

        public FP surfaceArea()
        {
            return 4 * FP.PI * radius * radius;
        }

        public override int GetHashCode()
        {
            int prime = 71;
            int result = 1;
            result = prime * result + center.GetHashCode();
            result = prime * result + radius.GetHashCode();
            return result;
        }

        public override bool Equals(object o)
        {
            var other = (FPSphere)o;
            return center == other.center && radius == other.radius;
        }

        public override string ToString()
        {
            return string.Format("(center = {0}, radius = {1})", center, radius);
        }
    }
}