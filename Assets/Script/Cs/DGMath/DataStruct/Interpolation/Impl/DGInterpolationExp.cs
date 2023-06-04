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
using FP = DGFixedPoint;
//libgdx
public class DGInterpolationExp : DGInterpolation
{

	protected FP value, power, min, scale;

	public DGInterpolationExp(FP value, FP power)
	{
		this.value = value;
		this.power = power;
		min = DGMath.Pow(value, -power);
		scale = (FP)1 / ((FP)1 - min);
	}

	public override FP Apply(FP a)
	{
		if (a <= (FP)0.5f) return (DGMath.Pow(value, power * (a * (FP)2 - (FP)1)) - min) * scale / (FP)2;
		return ((FP)2 - (DGMath.Pow(value, -power * (a * (FP)2 - (FP)1)) - min) * scale) / (FP)2;
	}

}
