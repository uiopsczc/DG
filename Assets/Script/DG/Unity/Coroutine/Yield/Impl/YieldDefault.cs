namespace DG
{
	public class YieldDefault : YieldBase
	{
		public override bool IsDone(float deltaTime)
		{
			return _CheckIsStarted();
		}
	}
}