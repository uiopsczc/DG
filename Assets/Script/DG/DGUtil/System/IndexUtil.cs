namespace DG
{
	public static class IndexUtil
	{
		//����,��,������д
		//scales��1��ʼ����
		//indexes��0��ʼ����
		public static int GetCombinedIndex(int[] scales, int[] indexes)
		{
			int result = 0;
			for (int i = 0; i < indexes.Length; i++)
				result += i != indexes.Length - 1 ? indexes[i] + scales[i + 1] : indexes[i];
			return result;
		}

		//����,��,������д
		//scales��1��ʼ����
		//indexes��0��ʼ����
		public static int GetCombinedIndex((int, int) scales, (int, int) indexes)
		{
			return indexes.Item1 * scales.Item2 + indexes.Item2 * 1;
		}

		//����,��,������д
		//scales��1��ʼ����
		//indexes��0��ʼ����
		public static int GetCombinedIndex((int, int, int) scales, (int, int, int) indexes)
		{
			return indexes.Item1 * scales.Item2 + indexes.Item2 * scales.Item3 + indexes.Item3 * 1;
		}

		//����,��,������д
		//scales��1��ʼ����
		//indexes��0��ʼ����
		public static int GetCombinedIndex((int, int, int, int) scales, (int, int, int, int) indexes)
		{
			return indexes.Item1 * scales.Item2 + indexes.Item2 * scales.Item3 + indexes.Item3 * scales.Item4 +
			       indexes.Item4 * 1;
		}

		//����,��,������д
		//scales��1��ʼ����
		//indexes��0��ʼ����
		public static int GetCombinedIndex((int, int, int, int, int) scales, (int, int, int, int, int) indexes)
		{
			return indexes.Item1 * scales.Item2 + indexes.Item2 * scales.Item3 + indexes.Item3 * scales.Item4 +
			       indexes.Item4 * scales.Item5 + indexes.Item5 * 1;
		}

	}
}

