using UnityEngine;

namespace DG
{
	public static class UnityEngine_ContactPoint_Extension
	{
		public static float PercentYOfThisCollider(this ContactPoint self)
		{
			return ContactPointUtil.PercentYOfThisCollider(self);
		}

		public static float PercentYOfOtherCollider(this ContactPoint self)
		{
			return ContactPointUtil.PercentYOfOtherCollider(self);
		}
	}
}


