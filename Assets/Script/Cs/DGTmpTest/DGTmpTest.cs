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
	public Transform tf1;

	public Transform tf2;

	public Transform tf3;
	// Start is called before the first frame update
	void Start()
	{
		var position = new Vector3(70, 80, 100);
		var target = new Vector3(45, 258, 157);
				var up = new Vector3(32, 45, 67);
		var rotation = Quaternion.Euler(30, 40, 70);
		var scale = new Vector3(6, 7, 11);
		//		var up = Vector3.up;

//		System.Numerics.Matrix3x2 a1 = new Matrix3x2(11, 2, 33,4,56,6);
//		System.Numerics.Matrix3x2 a2 = new Matrix3x2(26, 78, 45, 4, 82, 36);
//		a1 = Matrix3x2.Lerp(a1, a2, 0.3f);
		var axis = new Vector3(25f, 48.6f, 268f).normalized;
		var angle = 273.56f;
		var a1 = Matrix4x4.Rotate(Quaternion.AngleAxis(angle, axis));
		var c1 = Quaternion.AngleAxis(angle, axis);
//		var f1 = System.Numerics.Matrix4x4.CreateFromAxisAngle(axis.To_System_Numerics_Vector3(),
//			angle * Mathf.Deg2Rad);
//		Matrix4x4 a1 = tf2.localToWorldMatrix;
//		Matrix4x4 a1 = Matrix4x4(Quaternion.AngleAxis(45, position.normalized));
//	
//		a1 = a1 * Matrix4x4.Rotate(Quaternion.Euler(10, 20, 30)) ;
//		var a1 = Matrix4x4.LookAt(new Vector3(0,0,0), new Vector3(0, 0, 0) + new Vector3(0, 0, 1), Vector3.up);
//		var a2 = a1 * Matrix4x4.Rotate(rotation);
//		Matrix4x4 a1 = Matrix4x4.Rotate(rotation);

		//		var right = rotation * Vector3.right;
		//		var up = rotation * Vector3.up;
		//		var forward = rotation * Vector3.forward;


		//						a1 = Matrix4x4.LookAt(position, target, up);
		//		System.Numerics.Matrix4x4 dd = a1.To_System_Numerics_Matrix4x4();
		//			target.To_System_Numerics_Vector3(), up.To_System_Numerics_Vector3());
		//		a1.rotation = new Vector3(10, 20, 30),Quaternion.identity, new Vector3(4.5f, 3, 4));

		//		DGAffine2 b1 = new DGAffine2((DGFixedPoint)11, (DGFixedPoint)2, (DGFixedPoint)33, (DGFixedPoint)4, (DGFixedPoint)56, (DGFixedPoint)6);
		//		DGMatrix3x2 b2 = new DGMatrix3x2((DGFixedPoint)26, (DGFixedPoint)78, (DGFixedPoint)45, (DGFixedPoint)4, (DGFixedPoint)82, (DGFixedPoint)36);
		DGMatrix4x4 b1 = DGMatrix4x4.CreateFromQuaternion(DGQuaternion.CreateFromAxisAngle(new DGVector3(axis), (DGFixedPoint)angle));
		var d1 = DGQuaternion.CreateFromAxisAngle(new DGVector3(axis), (DGFixedPoint) angle);
		//		b1 = b1.rotate(DGQuaternion.Euler((DGFixedPoint)10, (DGFixedPoint)20, (DGFixedPoint)30));
		//		b1 = b1.setToLookAt(new DGVector3(new Vector3(10, 20, 30)), new DGVector3(new Vector3(10, 20, 30)) + new DGVector3(new Vector3(22, 36, 130)), new DGVector3(Vector3.right));
		//		b1 = b1.rotateTowardDirection(new DGVector3(10, 20, 30), DGVector3.up);
		//		DGMatrix4x4 b1 = new DGMatrix4x4(false);
		//		b1 = DGMatrix4x4.CreateLookAt(new DGVector3(position), new DGVector3(target), new DGVector3(up));

		//				DGMatrix4x4 b2 = new DGMatrix4x4(false);
		//				b2.setToLookAt(new DGVector3(position), new DGVector3(target), new DGVector3(up));
		//		var rotation = new DGQuaternion(false);
		//		b1.getRotation(ref rotation);
		//		b1 = b1.setToLookAt(new DGVector3(position),new DGVector3(target), new DGVector3(up));
		//		b1 = b1.translate((DGFixedPoint)position.x, (DGFixedPoint) position.y, (DGFixedPoint)position.z);
		//		Debug.LogWarning(dd.ToString2());
		Debug.LogWarning(a1);
//		Debug.LogWarning(f1.ToString2());
		//		Debug.LogWarning(dd.ToString2());
		Debug.LogWarning(b1);

		//		Debug.LogWarning(b2.ToString());
		//		Debug.LogWarning(up.ToString2());
		//		Debug.LogWarning(forward.ToString2());
		//		Debug.LogWarning(b1);
		//		Debug.LogWarning(b1.ToString());
		//		Debug.LogWarning(a1.rotation.ToString2());
		//		Debug.LogWarning(rotation.ToString());

	}
	
}