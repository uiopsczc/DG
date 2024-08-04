using UnityEngine;

namespace DG
{
	public static class DirectionInfoUtil
	{
		public static DirectionInfo GetDirectionInfo(int x, int y)
		{
			return DirectionInfoConst.DirectionInfoDict2[new Vector2Int(x, y)];
		}

		public static DirectionInfo GetDirectionInfo(string name)
		{
			return DirectionInfoConst.DirectionInfoDict[name.ToLower()];
		}
	}
}