using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	    Quaternion a1 = new Quaternion(2, 4, 9, 3);
	    Quaternion a2 = new Quaternion(45, 69, 5,4);
	    Quaternion a3 = new Quaternion(500,300, 700, 2);
	    Vector3 a4 = new Vector3(1.3f, 2.2f, 4.92f);
		Quaternion a5 = Quaternion.Euler(4,56,30);
	    Quaternion a6 = Quaternion.Euler(48, 526, 320);
	    Vector3 a7 = new Vector3(32, 54, 4.92f);


		DGQuaternion b1 = new DGQuaternion(a1);
	    DGQuaternion b2 = new DGQuaternion(a2);
	    DGQuaternion b3 = new DGQuaternion(a3);
		DGVector3 b4 = new DGVector3(a4);
	    DGQuaternion b5 = new DGQuaternion(a5);
	    DGQuaternion b6 = new DGQuaternion(a6);
	    DGVector3 b7 = new DGVector3(a7);

		var a = Quaternion.Slerp(a1,a2, 0.7f);
		//			    var b = DGVector3.Lerp(b1, b2, (DGFixedPoint)0.3f);
	    var b = DGQuaternion.Slerp(b1, b2, (DGFixedPoint)0.7f);

		//	    var a = Vector2.ClampMagnitude(a2, 10);
		//	    var b = DGVector2.ClampMagnitude(b2, (DGFixedPoint)10);

		Debug.LogWarning(a.ToString2());
			    Debug.LogWarning(b);

//		Debug.LogWarning(a);
//	    	    Debug.LogWarning(b);
	}


	Vector3 Slerp(Vector3 start, Vector3 end, float percent)
	{
		float dot = Vector3.Dot(start.normalized, end.normalized);
		dot = Mathf.Clamp(dot, -1.0f, 1.0f);

		var thita = Math.Acos(dot);
		return  (float) (Math.Sin((1 - percent) * thita) / Math.Sin(thita)) * start +
			(float) (Math.Sin(percent * thita) / Math.Sin(thita)) * end;



	}


}
