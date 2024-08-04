using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
	public class TestScriptableObject1 : ScriptableObject
	{
		[NonSerialized] public List<TestScriptableObjectAA> indexes = new List<TestScriptableObjectAA>();
		public new string name;
	}
}