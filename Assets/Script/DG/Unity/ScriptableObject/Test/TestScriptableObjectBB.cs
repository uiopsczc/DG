using System;

namespace DG
{
	[Serializable]
	public class TestScriptableObjectBB<T1, T2>
	{
		public T1 street;
		public T2 age;

		public TestScriptableObjectBB(T1 t1, T2 t2)
		{
			street = t1;
			age = t2;
		}

		public override string ToString()
		{
			return street + age.ToString();
		}
	}
}