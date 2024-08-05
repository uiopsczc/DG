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
    public class FPInterpolationElasticOut : FPInterpolationElastic
    {
        public FPInterpolationElasticOut(FP value, FP power, FP bounces, FP scale) : base(value, power, bounces, scale)
        {
        }

        public override FP Apply(FP a)
        {
            if (a == 0) return 0;
            a = 1 - a;
            return (1 - FPMath.Pow(value, power * (a - 1)) * FPMath.Sin(a * bounces) * scale);
        }
    }
}