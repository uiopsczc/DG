using System.Collections;
using UnityEngine;

namespace DG
{
	public static class CompositeCollider2DUtil
	{
		public static Hashtable GetSerializeHashtable(CompositeCollider2D compositeCollider2D)
		{
			Hashtable hashtable = new Hashtable
			{
				[StringConst.String_isTrigger] = compositeCollider2D.isTrigger,
				[StringConst.String_usedByEffector] = compositeCollider2D.usedByEffector,
				[StringConst.String_offset] = compositeCollider2D.offset.ToStringOrDefault(),
				[StringConst.String_geometryType] = (int) compositeCollider2D.geometryType,
				[StringConst.String_generationType] = (int) compositeCollider2D.generationType,
				[StringConst.String_vertexDistance] = compositeCollider2D.vertexDistance,
				[StringConst.String_offsetDistance] = compositeCollider2D.offsetDistance,
				[StringConst.String_edgeRadius] = compositeCollider2D.edgeRadius
			};
			hashtable.Trim();
			return hashtable;
		}

		public static void LoadSerializeHashtable(CompositeCollider2D compositeCollider2D, Hashtable hashtable)
		{
			compositeCollider2D.isTrigger = hashtable.Get<bool>(StringConst.String_isTrigger);
			compositeCollider2D.usedByEffector = hashtable.Get<bool>(StringConst.String_usedByEffector);
			compositeCollider2D.offset = hashtable.Get<string>(StringConst.String_offset).ToVector2OrDefault();
			compositeCollider2D.geometryType = hashtable.Get<int>(StringConst.String_geometryType)
				.ToEnum<CompositeCollider2D.GeometryType>();
			compositeCollider2D.generationType = hashtable.Get<int>(StringConst.String_generationType)
				.ToEnum<CompositeCollider2D.GenerationType>();
			compositeCollider2D.vertexDistance = hashtable.Get<float>(StringConst.String_vertexDistance);
			compositeCollider2D.offsetDistance = hashtable.Get<float>(StringConst.String_offsetDistance);
			compositeCollider2D.edgeRadius = hashtable.Get<float>(StringConst.String_edgeRadius);
		}
	}
}