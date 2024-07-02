using UnityEngine;

namespace DG
{
	public class YieldWWW : YieldBase
	{
		public WWW www;

		public YieldWWW(WWW www)
		{
			this.www = www;
		}

		public override bool IsDone(float deltaTime)
		{
			return _CheckIsStarted() && www.isDone;
		}
	}
}