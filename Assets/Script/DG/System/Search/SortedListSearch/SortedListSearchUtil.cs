using System;
using System.Collections.Generic;

namespace DG
{
    public class SortedListSearchUtil
    {
        //is_first_occur
        //1.null:只要找出来就行，不管在第一个还是第n个
        //2.true:第一个出现的位置
        //3.false:最后一个出现的位置
        public static int BinarySearchCat<T>(IList<T> list, T targetValue,
            EIndexOccurType indexOccurType = EIndexOccurType.Any_Index,
            IList<Comparison<T>> compareRules = null)
        {
            return BinarySearchCat(list, targetValue, 0, list.Count - 1, indexOccurType, compareRules);
        }

        public static int BinarySearchCat<T>(IList<T> list, T targetValue, int leftIndex, int rightIndex,
            EIndexOccurType indexOccurType = EIndexOccurType.Any_Index,
            IList<Comparison<T>> compareRules = null)
        {
            int resultIndex = -1;
            EListSortedType listSortedType = list.GetListSortedType(compareRules);
            while (leftIndex <= rightIndex)
            {
                int middleIndex = (leftIndex + rightIndex) / 2;
                T middleValue = list[middleIndex];
                int compareResult = CompareUtil.CompareWithRules(targetValue, middleValue, compareRules);
                if (compareResult == 0) //相等的情况
                {
                    switch (indexOccurType)
                    {
                        case EIndexOccurType.Any_Index:
                            return middleIndex;
                        default:
                            resultIndex = middleIndex;
                            _BinarySearchSetLeftRightIndex(ref leftIndex, ref rightIndex, compareResult,
                                indexOccurType, listSortedType);
                            break;
                    }
                }
                else
                    _BinarySearchSetLeftRightIndex(ref leftIndex, ref rightIndex, compareResult, indexOccurType,
                        listSortedType);
            }

            return resultIndex;
        }


        private static void _BinarySearchSetLeftRightIndex(ref int leftIndex, ref int rightIndex, int compareResult,
            EIndexOccurType indexOccurType = EIndexOccurType.Any_Index,
            EListSortedType listSortedType = EListSortedType.Increase)
        {
            int middleIndex = (leftIndex + rightIndex) / 2;
            if (compareResult == 0)
            {
                switch (indexOccurType)
                {
                    case EIndexOccurType.First_Index:
                        rightIndex = middleIndex - 1;
                        return;
                    case EIndexOccurType.Last_Index:
                        leftIndex = middleIndex + 1;
                        return;
                }

                return;
            }

            switch (listSortedType)
            {
                case EListSortedType.Increase:
                    if (compareResult > 0)
                        leftIndex = middleIndex + 1;
                    else
                        rightIndex = middleIndex - 1;
                    break;
                case EListSortedType.Decrease:
                    if (compareResult > 0)
                        rightIndex = middleIndex - 1;
                    else
                        leftIndex = middleIndex + 1;
                    break;
                default:
                    throw new Exception("Not Support ListSortedType:" + listSortedType);
            }
        }
    }
}