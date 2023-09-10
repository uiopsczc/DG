using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DG
{
	public partial class TransformUtil
	{
		public static Hashtable GetSerializeHashtable(Transform transform)
		{
			Hashtable hashtable = new Hashtable
			{
				[StringConst.String_localPosition] = transform.localPosition.ToStringOrDefault(),
				[StringConst.String_localEulerAngles] = transform.localEulerAngles.ToStringOrDefault(),
				[StringConst.String_localScale] = transform.localScale.ToStringOrDefault(null, Vector3.one)
			};
			hashtable.Trim();
			return hashtable;
		}

		public static void LoadSerializeHashtable(Transform transform, Hashtable hashtable)
		{
			transform.localPosition = hashtable.Get<string>(StringConst.String_localPosition).ToVector3OrDefault();
			transform.localEulerAngles = hashtable.Get<string>(StringConst.String_localEulerAngles).ToVector3OrDefault();
			transform.localScale = hashtable.Get<string>(StringConst.String_localScale)
				.ToVector3OrDefault(null, Vector3.one);
		}
	}
}