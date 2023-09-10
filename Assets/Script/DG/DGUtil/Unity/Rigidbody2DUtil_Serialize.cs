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
				[StringConst.String_bodyType] = (int)rigidbody2D.bodyType,
				[StringConst.String_simulated] = rigidbody2D.simulated,
				[StringConst.String_useFullKinematicContacts] = rigidbody2D.useFullKinematicContacts,
				[StringConst.String_collisionDetectionMode] = (int)rigidbody2D.collisionDetectionMode,
				[StringConst.String_sleepMode] = (int)rigidbody2D.sleepMode,
				[StringConst.String_interpolation] = (int)rigidbody2D.interpolation,
				[StringConst.String_constraints] = (int)rigidbody2D.constraints
			};
			hashtable.Trim();
			return hashtable;
		}

		public static void LoadSerializeHashtable(Rigidbody2D rigidbody2D, Hashtable hashtable)
		{
			rigidbody2D.bodyType = hashtable.Get<int>(StringConst.String_bodyType).ToEnum<RigidbodyType2D>();
			rigidbody2D.simulated = hashtable.Get<bool>(StringConst.String_bodyType);
			rigidbody2D.useFullKinematicContacts = hashtable.Get<bool>(StringConst.String_useFullKinematicContacts);
			rigidbody2D.collisionDetectionMode =
				hashtable.Get<int>(StringConst.String_collisionDetectionMode).ToEnum<CollisionDetectionMode2D>();
			rigidbody2D.sleepMode = hashtable.Get<int>(StringConst.String_sleepMode).ToEnum<RigidbodySleepMode2D>();
			rigidbody2D.interpolation =
				hashtable.Get<int>(StringConst.String_interpolation).ToEnum<RigidbodyInterpolation2D>();
			rigidbody2D.constraints = hashtable.Get<int>(StringConst.String_constraints).ToEnum<RigidbodyConstraints2D>();
		}
	}
}