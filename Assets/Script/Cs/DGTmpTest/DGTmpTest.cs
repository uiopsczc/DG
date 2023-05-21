using System;
using System.Numerics;
using UnityEngine;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Plane = UnityEngine.Plane;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class DGTmpTest : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		var a1 = new Vector3(1, 2, 3);

		var b1 = new DGVector3((DGFixedPoint)1, (DGFixedPoint)2, (DGFixedPoint)3);

		Debug.LogWarning(a1);
		Debug.LogWarning(b1);
	}
	
}