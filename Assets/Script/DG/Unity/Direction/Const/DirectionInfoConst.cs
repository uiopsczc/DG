using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    public static class DirectionInfoConst
    {
        public static DirectionInfo LeftTopDirectionInfo =>
            DirectionInfoUtil.GetDirectionInfo(StringConst.STRING_LEFT_TOP);

        public static DirectionInfo TopDirectionInfo => DirectionInfoUtil.GetDirectionInfo(StringConst.STRING_TOP);

        public static DirectionInfo RightTopDirectionInfo =>
            DirectionInfoUtil.GetDirectionInfo(StringConst.STRING_RIGHT_TOP);

        public static DirectionInfo LeftDirectionInfo => DirectionInfoUtil.GetDirectionInfo(StringConst.STRING_LEFT);

        public static DirectionInfo CenterDirectionInfo =>
            DirectionInfoUtil.GetDirectionInfo(StringConst.String_CENTER);

        public static DirectionInfo RightDirectionInfo => DirectionInfoUtil.GetDirectionInfo(StringConst.STRING_RIGHT);

        public static DirectionInfo LeftBottomDirectionInfo =>
            DirectionInfoUtil.GetDirectionInfo(StringConst.STRING_LEFT_BOTTOM);

        public static DirectionInfo BottomDirectionInfo =>
            DirectionInfoUtil.GetDirectionInfo(StringConst.STRING_BOTTOM);

        public static DirectionInfo RightBottomDirectionInfo =>
            DirectionInfoUtil.GetDirectionInfo(StringConst.STRING_RIGHT_BOTTOM);

        public static readonly Dictionary<string, DirectionInfo> NAME_2_DIRECTION_INFO =
            new()
            {
                { StringConst.STRING_LEFT_BOTTOM, new DirectionInfo(-1, -1, StringConst.STRING_LEFT_BOTTOM) },
                { StringConst.STRING_BOTTOM, new DirectionInfo(0, -1, StringConst.STRING_BOTTOM) },
                { StringConst.STRING_RIGHT_BOTTOM, new DirectionInfo(1, -1, StringConst.STRING_RIGHT_BOTTOM) },

                { StringConst.STRING_LEFT, new DirectionInfo(-1, 0, StringConst.STRING_LEFT) },
                { StringConst.String_CENTER, new DirectionInfo(0, 0, StringConst.String_CENTER) },
                { StringConst.STRING_RIGHT, new DirectionInfo(1, 0, StringConst.STRING_RIGHT) },

                { StringConst.STRING_LEFT_TOP, new DirectionInfo(-1, 1, StringConst.STRING_LEFT_TOP) },
                { StringConst.STRING_TOP, new DirectionInfo(0, 1, StringConst.STRING_TOP) },
                { StringConst.STRING_RIGHT_TOP, new DirectionInfo(1, 1, StringConst.STRING_RIGHT_TOP) },
            };

        private static Dictionary<Vector2Int, DirectionInfo> _VECTOR2_INT_2_DIRECTION_INFO;

        public static Dictionary<Vector2Int, DirectionInfo> VECTOR2_INT_2_DIRECTION_INFO
        {
            get
            {
                if (_VECTOR2_INT_2_DIRECTION_INFO != null) return _VECTOR2_INT_2_DIRECTION_INFO;
                _VECTOR2_INT_2_DIRECTION_INFO = new Dictionary<Vector2Int, DirectionInfo>();
                foreach (var directionInfo in NAME_2_DIRECTION_INFO.Values)
                    _VECTOR2_INT_2_DIRECTION_INFO[new Vector2Int(directionInfo.x, directionInfo.y)] = directionInfo;

                return _VECTOR2_INT_2_DIRECTION_INFO;
            }
        }
    }
}