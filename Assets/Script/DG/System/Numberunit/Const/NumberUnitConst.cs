using System.Collections.Generic;

namespace DG
{
    public static class NumberUnitConst
    {
        public static Dictionary<string, NumberUnitInfo> ToNumberUnitDict(List<NumberUnitInfo> numberUnitList)
        {
            if (numberUnitList == null)
                return null;
            var unit2NumberUnitInfo = new Dictionary<string, NumberUnitInfo>();
            foreach (var numberUnitInfo in numberUnitList)
                unit2NumberUnitInfo[numberUnitInfo.id] = numberUnitInfo;
            return unit2NumberUnitInfo;
        }

        public static List<NumberUnitInfo> ToNumberUnitList(Dictionary<string, NumberUnitInfo> unit2NumberUnitInfo)
        {
            if (unit2NumberUnitInfo == null)
                return null;
            var numberUnitList = new List<NumberUnitInfo>();
            foreach (var numberUnitInfo in unit2NumberUnitInfo.Values)
                numberUnitList.Add(numberUnitInfo);
            numberUnitList.QuickSort((a, b) => a.index <= b.index);
            return numberUnitList;
        }

        public static List<NumberUnitInfo> NumberUnitList = new()
        {
            new NumberUnitInfo(0, 0, "", "n"),
            new NumberUnitInfo(1, 3, "K", "K"),
            new NumberUnitInfo(2, 6, "M", "M"),
            new NumberUnitInfo(3, 9, "G", "G"),
            new NumberUnitInfo(4, 12, "T", "T"),
            new NumberUnitInfo(5, 15, "q", "q"),
            new NumberUnitInfo(6, 18, "Q", "Q"),
            new NumberUnitInfo(7, 21, "s", "s"),
            new NumberUnitInfo(8, 24, "S", "S"),
            new NumberUnitInfo(9, 27, "O", "O"),
            new NumberUnitInfo(10, 30, "N", "N"),
            new NumberUnitInfo(11, 33, "D", "D"),
            new NumberUnitInfo(12, 36, "Ud", "Ud"),
            new NumberUnitInfo(13, 39, "Dd", "Dd"),
            new NumberUnitInfo(14, 42, "Td", "Td"),
            new NumberUnitInfo(15, 45, "Qt", "Qt"),
            new NumberUnitInfo(16, 48, "Qd", "Qd"),
            new NumberUnitInfo(17, 51, "Sd", "Sd"),
            new NumberUnitInfo(18, 54, "St", "St"),
            new NumberUnitInfo(19, 57, "Od", "Od"),
            new NumberUnitInfo(20, 60, "Nd", "Nd"),
            new NumberUnitInfo(21, 63, "V", "V"),
        };

        private static Dictionary<string, NumberUnitInfo> _unit2NumberUnitInfo = new();

        public static Dictionary<string, NumberUnitInfo> Unit2NumberUnitInfo
        {
            get
            {
                if (_unit2NumberUnitInfo.Count == 0)
                    _unit2NumberUnitInfo = ToNumberUnitDict(NumberUnitList);
                return _unit2NumberUnitInfo;
            }
        }


        public static List<NumberUnitInfo> NumberUnitList2 = new()
        {
            new NumberUnitInfo(0, 0, "", "n"),
            new NumberUnitInfo(1, 3, "K", "K"),
            new NumberUnitInfo(2, 6, "M", "M"),
            new NumberUnitInfo(3, 9, "B", "B"), //此处与Number_Unit_List1不同
            new NumberUnitInfo(4, 12, "T", "T"),
            new NumberUnitInfo(5, 15, "q", "q"),
            new NumberUnitInfo(6, 18, "Q", "Q"),
            new NumberUnitInfo(7, 21, "s", "s"),
            new NumberUnitInfo(8, 24, "S", "S"),
            new NumberUnitInfo(9, 27, "O", "O"),
            new NumberUnitInfo(10, 30, "N", "N"),
            new NumberUnitInfo(11, 33, "D", "D"),
            new NumberUnitInfo(12, 36, "Ud", "Ud"),
            new NumberUnitInfo(13, 39, "Dd", "Dd"),
            new NumberUnitInfo(14, 42, "Td", "Td"),
            new NumberUnitInfo(15, 45, "Qt", "Qt"),
            new NumberUnitInfo(16, 48, "Qd", "Qd"),
            new NumberUnitInfo(17, 51, "Sd", "Sd"),
            new NumberUnitInfo(18, 54, "St", "St"),
            new NumberUnitInfo(19, 57, "Od", "Od"),
            new NumberUnitInfo(20, 60, "Nd", "Nd"),
            new NumberUnitInfo(21, 63, "V", "V"),
        };

        private static Dictionary<string, NumberUnitInfo> _unit2NumberUnitInfo2 = new();

        public static Dictionary<string, NumberUnitInfo> unit2NumberUnitInfo2
        {
            get
            {
                if (_unit2NumberUnitInfo2.Count == 0)
                    _unit2NumberUnitInfo2 = ToNumberUnitDict(NumberUnitList2);
                return _unit2NumberUnitInfo2;
            }
        }
    }
}