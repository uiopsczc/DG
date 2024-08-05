using System.Numerics;

namespace DG
{
    public static class System_Numerics_Matrix4x4_Extension
    {
        public static string DGToString(this Matrix4x4 v)
        {
            return "{" + v.M11 + ", " + v.M12 + ", " + v.M13 + ", " + v.M14 + "} \n" +
                   "{" + v.M21 + ", " + v.M22 + ", " + v.M23 + ", " + v.M24 + "} \n" +
                   "{" + v.M31 + ", " + v.M32 + ", " + v.M33 + ", " + v.M34 + "} \n" +
                   "{" + v.M41 + ", " + v.M42 + ", " + v.M43 + ", " + v.M44 + "} \n";
        }
    }
}