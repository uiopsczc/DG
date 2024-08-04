namespace DG
{
	public interface IDGPoolItemIndex
	{
		int GetIndex();

		object GetValue();

		T2 GetValue<T2>() where T2 : class;

		IDGPool GetIPool();
	}
}