using System.Numerics;

namespace DG
{
    public static class System_Numerics_Quaternion_Extension
    {
        public static string DGToString(this Quaternion v)
        {
            return string.Format("x:{0},y:{1},z:{2},w:{3}", v.X, v.Y, v.Z, v.W);
        }
    }
}