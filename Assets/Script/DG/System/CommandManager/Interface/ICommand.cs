namespace DG
{
	public interface ICommand
	{
		void Execute(ICommandMessage commandMessage);
	}
}