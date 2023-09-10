using UnityEngine;

namespace DG
{
	public class CollisionUtil
	{
		public static ContactPoint[] GetContactPoints(Collision collision)
		{
			var contactCount = collision.contactCount;
			if (contactCount == 0)
				return null;
			ContactPoint[] contactPoints = new ContactPoint[contactCount];
			for (int i = 0; i < contactCount; i++)
				contactPoints[i] = collision.GetContact(i);

			return contactPoints;
		}

		public static Collider[] GetContactThisColliders(Collision collision)
		{
			var contactCount = collision.contactCount;
			if (contactCount == 0)
				return null;
			Collider[] colliders = new Collider[contactCount];
			for (int i = 0; i < contactCount; i++)
				colliders[i] = collision.GetContact(i).thisCollider;

			return colliders;
		}

		public static ContactPoint GetContactPoint(Collision collision, Collider collider)
		{
			for (int i = 0; i < collision.contactCount; i++)
				if (collision.GetContact(i).thisCollider == collider)
					return collision.GetContact(i);

			return default;
		}
	}
}