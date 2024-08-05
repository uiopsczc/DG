using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    public static class PivotInfoConst
    {
        public static PivotInfo LEFT_TOP_PIVOT_INFO => PivotInfoUtil.GetPivotInfo(StringConst.STRING_LEFT_TOP);
        public static PivotInfo TOP_PIVOT_INFO => PivotInfoUtil.GetPivotInfo(StringConst.STRING_TOP);
        public static PivotInfo RIGHT_TOP_PIVOT_INFO => PivotInfoUtil.GetPivotInfo(StringConst.STRING_RIGHT_TOP);
        public static PivotInfo LEFT_PIVOT_INFO => PivotInfoUtil.GetPivotInfo(StringConst.STRING_LEFT);
        public static PivotInfo CENTER_PIVOT_INFO => PivotInfoUtil.GetPivotInfo(StringConst.String_CENTER);
        public static PivotInfo RIGHT_PIVOT_INFO => PivotInfoUtil.GetPivotInfo(StringConst.STRING_RIGHT);
        public static PivotInfo LEFT_BOTTOM_PIVOT_INFO => PivotInfoUtil.GetPivotInfo(StringConst.STRING_LEFT_BOTTOM);
        public static PivotInfo BOTTOM_PIVOT_INFO => PivotInfoUtil.GetPivotInfo(StringConst.STRING_BOTTOM);
        public static PivotInfo RIGHT_BOTTOM_PIVOT_INFO => PivotInfoUtil.GetPivotInfo(StringConst.STRING_RIGHT_BOTTOM);

        public static readonly Dictionary<string, PivotInfo> NAME_2_PIVOT_INFO = new()
        {
            { StringConst.STRING_LEFT_BOTTOM, new PivotInfo(0, 0, StringConst.STRING_LEFT_BOTTOM) },
            { StringConst.STRING_BOTTOM, new PivotInfo(0.5f, 0, StringConst.STRING_BOTTOM) },
            { StringConst.STRING_RIGHT_BOTTOM, new PivotInfo(1, 0, StringConst.STRING_RIGHT_BOTTOM) },

            { StringConst.STRING_LEFT, new PivotInfo(0, 0.5f, StringConst.STRING_LEFT) },
            { StringConst.String_CENTER, new PivotInfo(0.5f, 0.5f, StringConst.String_CENTER) },
            { StringConst.STRING_RIGHT, new PivotInfo(1, 0.5f, StringConst.STRING_RIGHT) },

            { StringConst.STRING_LEFT_TOP, new PivotInfo(0, 1, StringConst.STRING_LEFT_TOP) },
            { StringConst.STRING_TOP, new PivotInfo(0.5f, 1, StringConst.STRING_TOP) },
            { StringConst.STRING_RIGHT_TOP, new PivotInfo(1, 1, StringConst.STRING_RIGHT_TOP) },
        };

        private static Dictionary<Vector2, PivotInfo> _VECTOR2_2_PIVOT_INFO;

        public static Dictionary<Vector2, PivotInfo> VECTOR2_2_PIVOT_INFO
        {
            get
            {
                if (_VECTOR2_2_PIVOT_INFO != null) return _VECTOR2_2_PIVOT_INFO;
                _VECTOR2_2_PIVOT_INFO = new Dictionary<Vector2, PivotInfo>();
                foreach (var pivotInfo in NAME_2_PIVOT_INFO.Values)
                    _VECTOR2_2_PIVOT_INFO[new Vector2(pivotInfo.x, pivotInfo.y)] = pivotInfo;

                return _VECTOR2_2_PIVOT_INFO;
            }
        }
    }
}