/*************************************************************************************
 * ��    ��:  
 * �� �� ��:  czq
 * ����ʱ��:  2023/5/12
 * ======================================
 * ��ʷ���¼�¼
 * �汾:V          �޸�ʱ��:         �޸���:
 * �޸�����:
 * ======================================
*************************************************************************************/
namespace DG
{
	public class FPInterpolationCircleOut : FPInterpolation
	{

		public override FP Apply(FP a)
		{
			a = a - 1;
			return FPMath.Sqrt(1 - a * a);
		}

	}
}
