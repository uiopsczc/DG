using UnityEngine;

namespace DG
{
    public static class DirectionInfoUtil
    {
        public static DirectionInfo GetDirectionInfo(int x, int y)
        {
            return DirectionInfoConst.VECTOR2_INT_2_DIRECTION_INFO[new Vector2Int(x, y)];
        }

        public static DirectionInfo GetDirectionInfo(string name)
        {
            return DirectionInfoConst.NAME_2_DIRECTION_INFO[name.ToLower()];
        }
    }
}