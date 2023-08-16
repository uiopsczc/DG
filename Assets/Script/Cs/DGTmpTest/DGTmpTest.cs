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

public class DGTmpTest : MonoBehaviour
{
	public Transform tf1;

	public Transform tf2;

	public Transform tf3;

	

	// Start is called before the first frame update
	void Start()
	{
		var a1 = tf1.localToWorldMatrix;
		var a2 = tf2.localToWorldMatrix;
		var a3 = new Vector3(12.4f, 21.5f, 153.9f);
		var a4 = new Vector3(52.9f, 147.6f, 78.6f);
		var a5 = new Vector3(15.8f, 45.9f, 68.3f);
		var a6 = Quaternion.Euler(32.3f, 59.8f, 63.7f);
		var a7 = new Vector4(25.9f, 45.7f, 98.6f,69.6f);
		var a8 = new Vector3(25.9f, 45.7f, 98.6f);


		var b1 = a1.To_System_Numerics_Matrix4x4();
		var b2 = a2.To_System_Numerics_Matrix4x4();
		var b3 = a3.To_System_Numerics_Vector3();
		var b4 = a4.To_System_Numerics_Vector3();
		var b5 = a5.To_System_Numerics_Vector3();
		var b6 = a6.To_System_Numerics_Quaternion();
		var b7 = a7.To_System_Numerics_Vector4();
		var b8 = a8.To_System_Numerics_Vector3();

		var c1 = new DGMatrix4x4(a1);
		var c2 = new DGMatrix4x4(a2);
		var c3 = new DGVector3(a3);
		var c4 = new DGVector3(a4);
		var c5 = new DGVector3(a5);
		var c6 = new DGQuaternion(a6);
		var c7 = new DGVector4(a7);
		var c8 = new DGVector3(a8);


		//		Matrix4x4.Perspective()
//		Debug.LogWarning(Matrix4x4.Translate(a5).ToString2());
		//		Debug.LogWarning(System.Numerics.Matrix4x4.CreateWorld(b3,b4,b5).ToString2());
		Hashtable ht  = new Hashtable();
		ht.Add("kk", new List<int>(){ 7, 3, 4 });
		ht.Add("aa", "bb");
		DGLog.Warn(ht, "aaaa{0}", 888, "bbbb",999, "cccc{0}", 10);
		Debug.LogWarning(DGMatrix4x4.default2.translate(c5));
	}
	
}