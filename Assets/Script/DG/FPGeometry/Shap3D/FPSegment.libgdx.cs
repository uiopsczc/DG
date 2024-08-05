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
    public struct FPSegment
    {
        public FPVector3 a;

        /** the ending position **/
        public FPVector3 b;

        /** Constructs a new Segment from the two points given.
         *
         * @param a the first point
         * @param b the second point */
        public FPSegment(FPVector3 a, FPVector3 b)
        {
            this.a = a;
            this.b = b;
        }

        /** Constructs a new Segment from the two points given.
         * @param aX the x-coordinate of the first point
         * @param aY the y-coordinate of the first point
         * @param aZ the z-coordinate of the first point
         * @param bX the x-coordinate of the second point
         * @param bY the y-coordinate of the second point
         * @param bZ the z-coordinate of the second point */
        public FPSegment(FP aX, FP aY, FP aZ, FP bX, FP bY, FP bZ)
        {
            a = new FPVector3(aX, aY, aZ);
            b = new FPVector3(bX, bY, bZ);
        }

        public FP len()
        {
            return a.dst(b);
        }

        public FP len2()
        {
            return a.dst2(b);
        }

        public override int GetHashCode()
        {
            int prime = 71;
            int result = 1;
            result = prime * result + a.GetHashCode();
            result = prime * result + b.GetHashCode();
            return result;
        }

        public override bool Equals(object o)
        {
            var other = (FPSegment)o;
            return a == other.a && b == other.b;
        }

        public override string ToString()
        {
            return "(" + a + ", " + b + ")";
        }
    }
}