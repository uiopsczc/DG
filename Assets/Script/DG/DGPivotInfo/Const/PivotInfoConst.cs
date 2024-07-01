using System.Collections.Generic;
using UnityEngine;

namespace DG
{
	public static class PivotInfoConst
	{
		public static PivotInfo LeftTopPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.STRING_LEFT_TOP);
		public static PivotInfo TopPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.STRING_TOP);
		public static PivotInfo RightTopPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.STRING_RIGHT_TOP);
		public static PivotInfo LeftPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.STRING_LEFT);
		public static PivotInfo CenterPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.String_CENTER);
		public static PivotInfo RightPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.STRING_RIGHT);
		public static PivotInfo LeftBottomPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.STRING_LEFT_BOTTOM);
		public static PivotInfo BottomPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.STRING_BOTTOM);
		public static PivotInfo RightBottomPivotInfo => PivotInfoUtil.GetPivotInfo(StringConst.STRING_RIGHT_BOTTOM);

		public static readonly Dictionary<string, PivotInfo> PivotInfoDict = new Dictionary<string, PivotInfo>()
		{
			{StringConst.STRING_LEFT_BOTTOM, new PivotInfo(0, 0, StringConst.STRING_LEFT_BOTTOM)},
			{StringConst.STRING_BOTTOM, new PivotInfo(0.5f, 0, StringConst.STRING_BOTTOM)},
			{StringConst.STRING_RIGHT_BOTTOM, new PivotInfo(1, 0, StringConst.STRING_RIGHT_BOTTOM)},

			{StringConst.STRING_LEFT, new PivotInfo(0, 0.5f, StringConst.STRING_LEFT)},
			{StringConst.String_CENTER, new PivotInfo(0.5f, 0.5f, StringConst.String_CENTER)},
			{StringConst.STRING_RIGHT, new PivotInfo(1, 0.5f, StringConst.STRING_RIGHT)},

			{StringConst.STRING_LEFT_TOP, new PivotInfo(0, 1, StringConst.STRING_LEFT_TOP)},
			{StringConst.STRING_TOP, new PivotInfo(0.5f, 1, StringConst.STRING_TOP)},
			{StringConst.STRING_RIGHT_TOP, new PivotInfo(1, 1, StringConst.STRING_RIGHT_TOP)},
		};

		private static Dictionary<Vector2, PivotInfo> _PivotInfoDict2;

		public static Dictionary<Vector2, PivotInfo> PivotInfoDict2
		{
			get
			{
				if (_PivotInfoDict2 != null) return _PivotInfoDict2;
				_PivotInfoDict2 = new Dictionary<Vector2, PivotInfo>();
				foreach (var pivotInfo in PivotInfoDict.Values)
					_PivotInfoDict2[new Vector2(pivotInfo.x, pivotInfo.y)] = pivotInfo;

				return _PivotInfoDict2;
			}
		}
	}
}