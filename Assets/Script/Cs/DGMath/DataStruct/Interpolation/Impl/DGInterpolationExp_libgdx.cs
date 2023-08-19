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
public class DGInterpolationExp : DGInterpolation
{

	protected DGFixedPoint value, power, min, scale;

	public DGInterpolationExp(DGFixedPoint value, DGFixedPoint power)
	{
		this.value = value;
		this.power = power;
		min = DGMath.Pow(value, -power);
		scale = (DGFixedPoint)1 / ((DGFixedPoint)1 - min);
	}

	public override DGFixedPoint Apply(DGFixedPoint a)
	{
		if (a <= (DGFixedPoint)0.5f) return (DGMath.Pow(value, power * (a * (DGFixedPoint)2 - (DGFixedPoint)1)) - min) * scale / (DGFixedPoint)2;
		return ((DGFixedPoint)2 - (DGMath.Pow(value, -power * (a * (DGFixedPoint)2 - (DGFixedPoint)1)) - min) * scale) / (DGFixedPoint)2;
	}

}
