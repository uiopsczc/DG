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
	public class FPInterpolationPowOut : FPInterpolationPow
	{
		public FPInterpolationPowOut(FP power) : base(power)
		{
		}

		public override FP Apply(FP a)
		{
			return FPMath.Pow(a - 1, power) * (power % 2 == 0 ? (-1) : 1) + 1;
		}

	}
}

