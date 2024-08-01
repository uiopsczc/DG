using System.Collections;
using UnityEngine;

namespace DG
{
	public static partial  class TransformUtil
	{
		public static Hashtable GetSerializeHashtable(Transform transform)
		{
			Hashtable hashtable = new Hashtable
			{
				[StringConst.STRING_LOCAL_POSITION] = transform.localPosition.ToStringOrDefault(),
				[StringConst.STRING_LOCAL_EULER_ANGLES] = transform.localEulerAngles.ToStringOrDefault(),
				[StringConst.STRING_LOCAL_SCALE] = transform.localScale.ToStringOrDefault(null, Vector3.one)
			};
			hashtable.Trim();
			return hashtable;
		}

		public static void LoadSerializeHashtable(Transform transform, Hashtable hashtable)
		{
			transform.localPosition = hashtable.Get<string>(StringConst.STRING_LOCAL_POSITION).ToVector3OrDefault();
			transform.localEulerAngles = hashtable.Get<string>(StringConst.STRING_LOCAL_EULER_ANGLES).ToVector3OrDefault();
			transform.localScale = hashtable.Get<string>(StringConst.STRING_LOCAL_SCALE)
				.ToVector3OrDefault(null, Vector3.one);
		}
	}
}