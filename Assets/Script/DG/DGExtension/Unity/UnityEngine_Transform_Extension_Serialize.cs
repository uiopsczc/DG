using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DG
{
	public static partial class UnityEngine_Transform_Extension
	{
		public static Hashtable GetSerializeHashtable(this Transform self)
		{
			return TransformUtil.GetSerializeHashtable(self);
		}

		public static void LoadSerializeHashtable(this Transform self, Hashtable hashtable)
		{
			TransformUtil.LoadSerializeHashtable(self, hashtable);
		}
	}
}