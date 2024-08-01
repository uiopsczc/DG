/*************************************************************************************
 * ��    ��:  
 * �� �� ��:  czq
 * ����ʱ��:  2023/8/17
 * ======================================
 * ��ʷ���¼�¼
 * �汾:V          �޸�ʱ��:         �޸���:
 * �޸�����:
 * ======================================
*************************************************************************************/

namespace DG
{
	public partial struct FPMinimumTranslationVector
	{
		public static FPMinimumTranslationVector Null = Max.cpy();
		public static FPMinimumTranslationVector Max = new FPMinimumTranslationVector(FPVector2.max, FP.MAX_VALUE);
		/** Unit length vector that indicates the direction for the separation */
		public FPVector2 normal;
		/** Distance of the translation required for the separation */
		public FP depth;

		public FPMinimumTranslationVector(FPVector2 normal, FP depth)
		{
			this.normal = normal;
			this.depth = depth;
		}

		public FPMinimumTranslationVector cpy()
		{
			return new FPMinimumTranslationVector(normal, depth);
		}

		public override bool Equals(object obj)
		{
			FPMinimumTranslationVector other = (FPMinimumTranslationVector)obj;
			return this.normal == other.normal && this.depth == other.depth;
		}

		public override int GetHashCode()
		{
			return this.normal.GetHashCode() ^ this.depth.GetHashCode();
		}
	}
}
