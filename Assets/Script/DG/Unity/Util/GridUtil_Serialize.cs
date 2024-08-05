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
                [StringConst.STRING_CELL_SIZE] = grid.cellSize.ToStringOrDefault(),
                [StringConst.STRING_CELL_GAP] = grid.cellGap.ToStringOrDefault(),
                [StringConst.STRING_CELL_LAYOUT] = (int)grid.cellLayout,
                [StringConst.STRING_CELL_SWIZZLE] = (int)grid.cellSwizzle
            };
            hashtable.Trim();
            return hashtable;
        }

        public static void LoadSerializeHashtable(Grid grid, Hashtable hashtable)
        {
            grid.cellSize = hashtable.Get<string>(StringConst.STRING_CELL_SIZE).ToVector3OrDefault();
            grid.cellGap = hashtable.Get<string>(StringConst.STRING_CELL_GAP).ToVector3OrDefault();
            grid.cellLayout = hashtable.Get<int>(StringConst.STRING_CELL_LAYOUT).ToEnum<GridLayout.CellLayout>();
            grid.cellSwizzle = hashtable.Get<int>(StringConst.STRING_CELL_SWIZZLE).ToEnum<GridLayout.CellSwizzle>();
        }
    }
}