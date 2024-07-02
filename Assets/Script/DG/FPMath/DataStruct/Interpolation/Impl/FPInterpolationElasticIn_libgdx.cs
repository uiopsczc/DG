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
	public class FPInterpolationElasticIn : FPInterpolationElastic
	{
		public FPInterpolationElasticIn(FP value, FP power, FP bounces, FP scale) : base(value, power, bounces, scale)
		{
		}

		public override FP Apply(FP a)
		{
			if (a >= 0.99) return 1;
			return FPMath.Pow(value, power * (a - 1)) * FPMath.Sin(a * bounces) * scale;
		}

	}
}

