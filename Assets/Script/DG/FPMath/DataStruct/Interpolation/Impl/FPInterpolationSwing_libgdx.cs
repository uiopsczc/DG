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
	public class FPInterpolationSwing : FPInterpolation
	{

		protected FP scale;

		public FPInterpolationSwing(FP scale)
		{
			this.scale = scale;
		}

		public override FP Apply(FP a)
		{
			if (a <= 0.5f)
			{
				a *= 2;
				return a * a * ((scale + 1) * a - scale) / 2;
			}
			a = a - 1;
			a *= 2;
			return a * a * ((scale + 1) * a + scale) / 2 + 1;
		}

	}
}


