/*************************************************************************************
 * ��    ��:
 * �� �� ��:  czq
 * ����ʱ��:  2023/5/21
 * ======================================
 * ��ʷ���¼�¼
 * �汾:V          �޸�ʱ��:         �޸���:
 * �޸�����:
 * ======================================
 *************************************************************************************/

namespace DG
{
    public interface IFPShape2D
    {
        /** Returns whether the given point is contained within the shape. */
        bool contains(FPVector2 point);

        /** Returns whether a point with the given coordinates is contained within the shape. */
        bool contains(FP x, FP y);
    }
}