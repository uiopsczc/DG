using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Vector2 a1 = new Vector2(2, 4);
	    Vector2 a2 = new Vector2(45, 69);
	    DGVector2 b1 = new DGVector2(a1);
	    DGVector2 b2 = new DGVector2(a2);

		var a = Vector2.MoveTowards(a1, a2, 6);
	    var b = DGVector2.MoveTowards(b1, b2, (DGFixedPoint)6);

		Debug.LogWarning(a.x + " "+ b.y);
	    Debug.LogWarning(b.ToString());
	}

	
}
