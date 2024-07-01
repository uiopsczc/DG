using System.Collections;
using UnityEngine;

namespace DG
{
	public partial class Rigidbody2DUtil
	{
		public static Hashtable GetSerializeHashtable(Rigidbody2D rigidbody2D)
		{
			Hashtable hashtable = new Hashtable
			{
				[StringConst.STRING_BODY_TYPE] = (int)rigidbody2D.bodyType,
				[StringConst.STRING_SIMULATED] = rigidbody2D.simulated,
				[StringConst.STRING_USE_FULL_KINEMATIC_CONTACTS] = rigidbody2D.useFullKinematicContacts,
				[StringConst.STRING_COLLISION_DETECTION_MODE] = (int)rigidbody2D.collisionDetectionMode,
				[StringConst.STRING_SLEEP_MODE] = (int)rigidbody2D.sleepMode,
				[StringConst.STRING_INTERPOLATION] = (int)rigidbody2D.interpolation,
				[StringConst.STRING_CONSTRAINTS] = (int)rigidbody2D.constraints
			};
			hashtable.Trim();
			return hashtable;
		}

		public static void LoadSerializeHashtable(Rigidbody2D rigidbody2D, Hashtable hashtable)
		{
			rigidbody2D.bodyType = hashtable.Get<int>(StringConst.STRING_BODY_TYPE).ToEnum<RigidbodyType2D>();
			rigidbody2D.simulated = hashtable.Get<bool>(StringConst.STRING_BODY_TYPE);
			rigidbody2D.useFullKinematicContacts = hashtable.Get<bool>(StringConst.STRING_USE_FULL_KINEMATIC_CONTACTS);
			rigidbody2D.collisionDetectionMode =
				hashtable.Get<int>(StringConst.STRING_COLLISION_DETECTION_MODE).ToEnum<CollisionDetectionMode2D>();
			rigidbody2D.sleepMode = hashtable.Get<int>(StringConst.STRING_SLEEP_MODE).ToEnum<RigidbodySleepMode2D>();
			rigidbody2D.interpolation =
				hashtable.Get<int>(StringConst.STRING_INTERPOLATION).ToEnum<RigidbodyInterpolation2D>();
			rigidbody2D.constraints = hashtable.Get<int>(StringConst.STRING_CONSTRAINTS).ToEnum<RigidbodyConstraints2D>();
		}
	}
}