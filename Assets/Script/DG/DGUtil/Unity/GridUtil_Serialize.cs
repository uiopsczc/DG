using System.Collections;
using UnityEngine;

namespace DG
{
	public partial class GridUtil
	{
		public static Hashtable GetSerializeHashtable(Grid grid)
		{
			Hashtable hashtable = new Hashtable
			{
				[StringConst.String_cellSize] = grid.cellSize.ToStringOrDefault(),
				[StringConst.String_cellGap] = grid.cellGap.ToStringOrDefault(),
				[StringConst.String_cellLayout] = (int)grid.cellLayout,
				[StringConst.String_cellSwizzle] = (int)grid.cellSwizzle
			};
			hashtable.Trim();
			return hashtable;
		}

		public static void LoadSerializeHashtable(Grid grid, Hashtable hashtable)
		{
			grid.cellSize = hashtable.Get<string>(StringConst.String_cellSize).ToVector3OrDefault();
			grid.cellGap = hashtable.Get<string>(StringConst.String_cellGap).ToVector3OrDefault();
			grid.cellLayout = hashtable.Get<int>(StringConst.String_cellLayout).ToEnum<GridLayout.CellLayout>();
			grid.cellSwizzle = hashtable.Get<int>(StringConst.String_cellSwizzle).ToEnum<GridLayout.CellSwizzle>();
		}
	}
}