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
	public class FPInterpolationPowIn : FPInterpolationPow
	{
		public FPInterpolationPowIn(FP power) : base(power)
		{
		}

		public override FP Apply(FP a)
		{
			return FPMath.Pow(a, power);
		}

	}
}

