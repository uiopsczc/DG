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
                [StringConst.STRING_IS_TRIGGER] = compositeCollider2D.isTrigger,
                [StringConst.STRING_USE_BY_EFFECTOR] = compositeCollider2D.usedByEffector,
                [StringConst.STRING_OFFSET] = compositeCollider2D.offset.ToStringOrDefault(),
                [StringConst.STRING_GEOMETRY_TYPE] = (int)compositeCollider2D.geometryType,
                [StringConst.STRING_GENERATION_TYPE] = (int)compositeCollider2D.generationType,
                [StringConst.STRING_VERTEX_DISTANCE] = compositeCollider2D.vertexDistance,
                [StringConst.STRING_OFFSET_DISTANCE] = compositeCollider2D.offsetDistance,
                [StringConst.STRING_EDGE_RADIUS] = compositeCollider2D.edgeRadius
            };
            hashtable.Trim();
            return hashtable;
        }

        public static void LoadSerializeHashtable(CompositeCollider2D compositeCollider2D, Hashtable hashtable)
        {
            compositeCollider2D.isTrigger = hashtable.Get<bool>(StringConst.STRING_IS_TRIGGER);
            compositeCollider2D.usedByEffector = hashtable.Get<bool>(StringConst.STRING_USE_BY_EFFECTOR);
            compositeCollider2D.offset = hashtable.Get<string>(StringConst.STRING_OFFSET).ToVector2OrDefault();
            compositeCollider2D.geometryType = hashtable.Get<int>(StringConst.STRING_GEOMETRY_TYPE)
                .ToEnum<CompositeCollider2D.GeometryType>();
            compositeCollider2D.generationType = hashtable.Get<int>(StringConst.STRING_GENERATION_TYPE)
                .ToEnum<CompositeCollider2D.GenerationType>();
            compositeCollider2D.vertexDistance = hashtable.Get<float>(StringConst.STRING_VERTEX_DISTANCE);
            compositeCollider2D.offsetDistance = hashtable.Get<float>(StringConst.STRING_OFFSET_DISTANCE);
            compositeCollider2D.edgeRadius = hashtable.Get<float>(StringConst.STRING_EDGE_RADIUS);
        }
    }
}