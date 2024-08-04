using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Plane = UnityEngine.Plane;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Vector4 = UnityEngine.Vector4;

namespace DG
{
	public class DGTmpTest : MonoBehaviour
	{
		
		public GameObject prefab;

		

		// Start is called before the first frame update
		void Start()
		{
			// Vector3[] list = new Vector3[3];
			// Vector3 poolItem = new Vector3(4, 3,2);
			// list[0] = poolItem;
			//
			// var d = list[0];
			// d.x = 6;
			// DGLog.Warn(list[0].x);
			// d.SetIsDeSpawned(true);
			// DGLog.Warn(list[0].IsDeSpawned());
			// Vector3 v = new Vector3(2, 1, 3);
			// v.x = 4;
			// DGLog.Warn(list[0].IsDeSpawned());
			DGPoolGameObjectTest.Test(prefab);
			// prefab.SetActive(true);
			// prefab.SetActive(false);
		}

	}
}
