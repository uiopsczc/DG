using UnityEngine;

namespace DG
{
	public static class UnityEngine_Collision_Extension
	{
		public static ContactPoint[] GetContactPoints(this Collision self)
		{
			return CollisionUtil.GetContactPoints(self);
		}

		public static Collider[] GetContactThisColliders(this Collision self)
		{
			return CollisionUtil.GetContactThisColliders(self);
		}

		public static ContactPoint GetContactPoint(this Collision self, Collider collider)
		{
			return CollisionUtil.GetContactPoint(self, collider);
		}


	}
}


