using System;
using System.Numerics;
using UnityEngine;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Plane = UnityEngine.Plane;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Test : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{	
		Matrix3x2 a1 = new Matrix3x2(21,12, 34,56,345,123);
		Matrix3x2 a2 = new Matrix3x2(211, 332, 4, 26, 65, 23);
		System.Numerics.Vector2 a3 = new System.Numerics.Vector2(2.5f, 129.4f);
		System.Numerics.Vector2 a4 = new System.Numerics.Vector2(62.5f, 657.4f);
		System.Numerics.Matrix4x4 a5 = new System.Numerics.Matrix4x4(12f,23.3f, 23.5f,23f, 
			233, 56,78,90,
			45,23,45,32,
			12, 67,9,7);
		//		//
		//		//
		DGMatrix3x2 b1 = new DGMatrix3x2(a1);
		DGMatrix3x2 b2 = new DGMatrix3x2(a2);
		DGVector2 b3 = new DGVector2(a3);
		DGVector2 b4 = new DGVector2(a4);
		DGMatrix4x4 b5 = new DGMatrix4x4(a5);

		var a =a1.GetDeterminant();
		var b = b1.GetDeterminant();

		Debug.LogWarning(a);
		Debug.LogWarning(b);
	}
	
}