using System;
using UnityEngine;

public class Test : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		Transform tf = this.gameObject.transform;
		tf.position = new Vector3(1, 4, 5);
		tf.eulerAngles = new Vector3(20, 32, 321);
		tf.localScale = new Vector3(2, 7, 8);

		Transform tf2 = new GameObject().transform;
		tf2.position = new Vector3(45, 54, 125);
		tf2.eulerAngles = new Vector3(120, 32, 221);
		tf2.localScale = new Vector3(25, 78, 148);

		
		
//		Matrix4x4 a1 = tf.localToWorldMatrix;
//		Matrix4x4 a2 = tf2.localToWorldMatrix;
//		//
//		//
//		DGMatrix4x4 b1 = new DGMatrix4x4(a1);
//		DGMatrix4x4 b2 = new DGMatrix4x4(a2);
//		//
//		System.Numerics.Matrix4x4 m = new System.Numerics.Matrix4x4((float)b1.M11, (float)b1.M12, (float)b1.M13, (float)b1.M14, (float)b1.M21, (float)b1.M22, (float)b1.M23, (float)b1.M24, (float)b1.M31, (float)b1.M32, (float)b1.M33, (float)b1.M34, (float)b1.M41, (float)b1.M42, (float)b1.M43, (float)b1.M44);
//		m * 
//
//		//	    var c = Slerp(a1, a2, 1.7f);
//		//		//			    var b = DGVector3.Lerp(b1, b2, (DGFixedPoint)0.3f);
//		var a = a1;
//		var b = b1;

		//	    var b = Slerp(a1, a2, 0.7f);


		//	    var a = Vector2.ClampMagnitude(a2, 10);
		//	    var b = DGVector2.ClampMagnitude(b2, (DGFixedPoint)10);

//		Debug.LogWarning(a1.ToString2());
//		Debug.LogWarning(m.ToString2());
//	    Debug.LogWarning(c.ToString2());

		//		Debug.LogWarning(a);
		//	    	    Debug.LogWarning(b);
	}


	public static Matrix4x4 Mul(Matrix4x4 lhs, Matrix4x4 rhs)
	{
		Matrix4x4 matrix4x4;
		matrix4x4.m00 = (float)((double)lhs.m00 * (double)rhs.m00 + (double)lhs.m01 * (double)rhs.m10 + (double)lhs.m02 * (double)rhs.m20 + (double)lhs.m03 * (double)rhs.m30);
		matrix4x4.m01 = (float)((double)lhs.m00 * (double)rhs.m01 + (double)lhs.m01 * (double)rhs.m11 + (double)lhs.m02 * (double)rhs.m21 + (double)lhs.m03 * (double)rhs.m31);
		matrix4x4.m02 = (float)((double)lhs.m00 * (double)rhs.m02 + (double)lhs.m01 * (double)rhs.m12 + (double)lhs.m02 * (double)rhs.m22 + (double)lhs.m03 * (double)rhs.m32);
		matrix4x4.m03 = (float)((double)lhs.m00 * (double)rhs.m03 + (double)lhs.m01 * (double)rhs.m13 + (double)lhs.m02 * (double)rhs.m23 + (double)lhs.m03 * (double)rhs.m33);
		matrix4x4.m10 = (float)((double)lhs.m10 * (double)rhs.m00 + (double)lhs.m11 * (double)rhs.m10 + (double)lhs.m12 * (double)rhs.m20 + (double)lhs.m13 * (double)rhs.m30);
		matrix4x4.m11 = (float)((double)lhs.m10 * (double)rhs.m01 + (double)lhs.m11 * (double)rhs.m11 + (double)lhs.m12 * (double)rhs.m21 + (double)lhs.m13 * (double)rhs.m31);
		matrix4x4.m12 = (float)((double)lhs.m10 * (double)rhs.m02 + (double)lhs.m11 * (double)rhs.m12 + (double)lhs.m12 * (double)rhs.m22 + (double)lhs.m13 * (double)rhs.m32);
		matrix4x4.m13 = (float)((double)lhs.m10 * (double)rhs.m03 + (double)lhs.m11 * (double)rhs.m13 + (double)lhs.m12 * (double)rhs.m23 + (double)lhs.m13 * (double)rhs.m33);
		matrix4x4.m20 = (float)((double)lhs.m20 * (double)rhs.m00 + (double)lhs.m21 * (double)rhs.m10 + (double)lhs.m22 * (double)rhs.m20 + (double)lhs.m23 * (double)rhs.m30);
		matrix4x4.m21 = (float)((double)lhs.m20 * (double)rhs.m01 + (double)lhs.m21 * (double)rhs.m11 + (double)lhs.m22 * (double)rhs.m21 + (double)lhs.m23 * (double)rhs.m31);
		matrix4x4.m22 = (float)((double)lhs.m20 * (double)rhs.m02 + (double)lhs.m21 * (double)rhs.m12 + (double)lhs.m22 * (double)rhs.m22 + (double)lhs.m23 * (double)rhs.m32);
		matrix4x4.m23 = (float)((double)lhs.m20 * (double)rhs.m03 + (double)lhs.m21 * (double)rhs.m13 + (double)lhs.m22 * (double)rhs.m23 + (double)lhs.m23 * (double)rhs.m33);
		matrix4x4.m30 = (float)((double)lhs.m30 * (double)rhs.m00 + (double)lhs.m31 * (double)rhs.m10 + (double)lhs.m32 * (double)rhs.m20 + (double)lhs.m33 * (double)rhs.m30);
		matrix4x4.m31 = (float)((double)lhs.m30 * (double)rhs.m01 + (double)lhs.m31 * (double)rhs.m11 + (double)lhs.m32 * (double)rhs.m21 + (double)lhs.m33 * (double)rhs.m31);
		matrix4x4.m32 = (float)((double)lhs.m30 * (double)rhs.m02 + (double)lhs.m31 * (double)rhs.m12 + (double)lhs.m32 * (double)rhs.m22 + (double)lhs.m33 * (double)rhs.m32);
		matrix4x4.m33 = (float)((double)lhs.m30 * (double)rhs.m03 + (double)lhs.m31 * (double)rhs.m13 + (double)lhs.m32 * (double)rhs.m23 + (double)lhs.m33 * (double)rhs.m33);
		return matrix4x4;
	}
}