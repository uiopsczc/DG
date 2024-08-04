using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
	public class TestScriptableObject2<T1, T2> : ScriptableObject
	{
		public TestScriptableObjectBB<T1, T2> indexes;

		//    public T1 t1;
		//    public T2 t2;
		public new string name;
	}
}