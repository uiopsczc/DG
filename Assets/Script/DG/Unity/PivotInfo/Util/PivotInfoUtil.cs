using UnityEngine;

namespace DG
{
    public static class PivotInfoUtil
    {
        public static PivotInfo GetPivotInfo(float x, float y)
        {
            return PivotInfoConst.VECTOR2_2_PIVOT_INFO[new Vector2(x, y)];
        }

        public static PivotInfo GetPivotInfo(string name)
        {
            return PivotInfoConst.NAME_2_PIVOT_INFO[name.ToLower()];
        }
    }
}