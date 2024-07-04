using UnityEngine;
using System;

namespace DG
{
	public class GLPushMatrixScope : IDisposable
	{
		public GLPushMatrixScope()
		{
			GL.PushMatrix();
		}

		public void Dispose()
		{
			GL.PopMatrix();
		}
	}
}