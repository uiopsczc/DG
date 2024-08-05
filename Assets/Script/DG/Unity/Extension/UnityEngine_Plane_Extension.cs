using UnityEngine;

namespace DG
{
    public static class UnityEngine_Plane_Extension
    {
        public static string DGToString(this Plane v)
        {
            return PlaneUtil.DGToString(v);
        }

        public static System.Numerics.Plane To_System_Numerics_Plane(this Plane v)
        {
            return PlaneUtil.To_System_Numerics_Plane(v);
        }
    }
}