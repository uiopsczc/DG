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
	public class FPInterpolationSwingOut : FPInterpolationSwing
	{

		public FPInterpolationSwingOut(FP scale) : base(scale)
		{
			this.scale = scale;
		}

		public override FP Apply(FP a)
		{
			a = a - 1;
			return a * a * ((scale + 1) * a + scale) + 1;
		}

	}
}

