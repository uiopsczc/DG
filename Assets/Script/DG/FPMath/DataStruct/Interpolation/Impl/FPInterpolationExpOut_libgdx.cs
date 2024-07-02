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
	public class FPInterpolationExpOut : FPInterpolationExp
	{
		public FPInterpolationExpOut(FP value, FP power) : base(value, power)
		{
		}

		public override FP Apply(FP a)
		{
			return 1 - (FPMath.Pow(value, -power * a) - min) * scale;
		}

	}
}

