using System;
using UnityEngine;

public class Test : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		Quaternion a3 = Quaternion.Euler(23, 45, 124);
		Vector4 a1 = new Vector4(1.3f, 2.2f, 4.92f, 5.67f);
		Vector4 a2 = new Vector4(32, 54, 4.92f, 322f);
		//
		//
		DGQuaternion b3 = new DGQuaternion(a3);
		DGVector4 b1 = new DGVector4(a1);
		DGVector4 b2 = new DGVector4(a2);
		//


		//	    var c = Slerp(a1, a2, 1.7f);
		//		//			    var b = DGVector3.Lerp(b1, b2, (DGFixedPoint)0.3f);
		var a = Vector4.MoveTowards(a1, a2, 0.4f);
		var b = DGVector4.MoveTowards(b1, b2, (DGFixedPoint) 0.4f);

		//	    var b = Slerp(a1, a2, 0.7f);


		//	    var a = Vector2.ClampMagnitude(a2, 10);
		//	    var b = DGVector2.ClampMagnitude(b2, (DGFixedPoint)10);

		Debug.LogWarning(a.ToString2());
		Debug.LogWarning(b);
//	    Debug.LogWarning(c.ToString2());

		//		Debug.LogWarning(a);
		//	    	    Debug.LogWarning(b);
	}


	public static Quaternion Slerp(Quaternion q1, Quaternion q2, float t)
	{
		var dot = q1.x * q2.x + q1.y * q2.y + q1.z * q2.z + q1.w * q2.w;


		if (dot < 0)
		{
			dot = -dot;
			q2 = new Quaternion(-q2.x, -q2.y, -q2.z, -q2.w);
		}


		if (dot < 0.95)
		{
			var angle = Math.Acos(dot);

			var invSinAngle = 1 / Math.Sin(angle);

			var t1 = Math.Sin((1 - t) * angle) * invSinAngle;

			var t2 = Math.Sin(t * angle) * invSinAngle;

			q1 = new Quaternion((float) (q1.x * t1 + q2.x * t2), (float) (q1.y * t1 + q2.y * t2),
				(float) (q1.z * t1 + q2.z * t2), (float) (q1.w * t1 + q2.w * t2));
			return q1;
		}

		else
		{
			var x = (float) (q1.x + t * (q2.x - q1.x));
			var y = (float) (q1.y + t * (q2.y - q1.y));
			var z = (float) (q1.z + t * (q2.z - q1.z));
			var w = (float) (q1.w + t * (q2.w - q1.w));
			q1 = new Quaternion(x, y, z, w);
		}

		q1 = q1.normalized;
		return q1;
	}
}