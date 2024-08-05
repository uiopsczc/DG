using System.Numerics;

namespace DG
{
    public static class System_Numerics_Vector4_Extension
    {
        public static string DGToString(this Vector4 v)
        {
            return string.Format("x:{0},y:{1},z:{2},w:{3}", v.X, v.Y, v.Z, v.W);
        }
    }
}