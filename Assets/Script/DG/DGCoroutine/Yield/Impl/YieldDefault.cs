namespace DG
{
	public class YieldDefault : YieldBase
	{
		public override bool IsDone(float deltaTime)
		{
			if (!_CheckIsStarted())
				return false;

			return true;
		}
	}
}