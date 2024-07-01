using UnityEngine;

namespace DG
{
	public static class PivotInfoUtil
	{
		public static PivotInfo GetPivotInfo(float x, float y)
		{
			return PivotInfoConst.PIVOT_INFO_DICT2[new Vector2(x, y)];
		}

		public static PivotInfo GetPivotInfo(string name)
		{
			return PivotInfoConst.PIVOT_INFO_DICT[name.ToLower()];
		}
	}
}