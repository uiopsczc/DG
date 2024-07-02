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
	public class FPInterpolationPow : FPInterpolation
	{
		protected FP power;

		public FPInterpolationPow(FP power)
		{
			this.power = power;
		}

		public override FP Apply(FP a)
		{
			if (a <= 0.5f) return FPMath.Pow(a * 2, power) / 2;
			return FPMath.Pow((a - 1) * 2, power) / (power % 2 == 0 ? (-2) : 2) + 1;
		}

	}

}
