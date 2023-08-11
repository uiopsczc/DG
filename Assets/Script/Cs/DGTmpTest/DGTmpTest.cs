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
		var a1 = Quaternion.Euler(45.8f, 2.35f, 39.45f).To_System_Numerics_Quaternion();
		var a2 = new Vector3(25.6f, 87.69f, 45.71f).To_System_Numerics_Vector3();
		var a3 = new Vector3(4, 588, 23).To_System_Numerics_Vector3();
		var a4 = this.transform.localToWorldMatrix.To_System_Numerics_Matrix4x4();

		var b1 = new DGQuaternion(a1);
		var b2 = new DGVector3(a2);
		var b3 = new DGVector3(a3);
		var b4 = new DGMatrix4x4(a4);

			

	}
	
}