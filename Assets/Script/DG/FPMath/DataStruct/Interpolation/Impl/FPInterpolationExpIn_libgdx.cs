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
	public class FPInterpolationExpIn : FPInterpolationExp
	{


		public FPInterpolationExpIn(FP value, FP power) : base(value, power)
		{
		}

		public override FP Apply(FP a)
		{
			return (FPMath.Pow(value, power * (a - 1)) - min) * scale;
		}

	}
}

