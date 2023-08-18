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

public partial struct DGMinimumTranslationVector
{
	public static DGMinimumTranslationVector Null = Max.cpy();
	public static DGMinimumTranslationVector Max = new DGMinimumTranslationVector(DGVector2.max, DGFixedPoint.MaxValue);
	/** Unit length vector that indicates the direction for the separation */
	public DGVector2 normal;
	/** Distance of the translation required for the separation */
	public DGFixedPoint depth;

	public DGMinimumTranslationVector(DGVector2 normal, DGFixedPoint depth)
	{
		this.normal = normal;
		this.depth = depth;
	}

	public DGMinimumTranslationVector cpy()
	{
		return new DGMinimumTranslationVector(normal, depth);
	}

	public override bool Equals(object obj)
	{
		DGMinimumTranslationVector other = (DGMinimumTranslationVector)obj;
		return this.normal == other.normal && this.depth == other.depth;
	}

	public override int GetHashCode()
	{
		return this.normal.GetHashCode() ^ this.depth.GetHashCode();
	}
}