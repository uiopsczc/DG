using UnityEngine;

namespace DG
{
	public class ContactPointUtil
	{
		public static float PercentYOfThisCollider(ContactPoint contactPoint)
		{
			Vector3 point = contactPoint.point;
			float yDistance = contactPoint.thisCollider.bounds.extents.y * 2;
			float result = (point.y - contactPoint.thisCollider.bounds.FrontBottomLeft().y) / yDistance;
			return result;
		}

		public static float PercentYOfOtherCollider(ContactPoint contactPoint)
		{
			Vector3 point = contactPoint.point;
			float yDistance = contactPoint.otherCollider.bounds.extents.y * 2;
			float result = (point.y - contactPoint.otherCollider.bounds.FrontBottomLeft().y) / yDistance;
			return result;
		}
	}
}