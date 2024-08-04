using System;
using UnityEngine;

namespace DG
{
	public class GLBeginScope : IDisposable
	{
		public GLBeginScope(int mode)
		{
			GL.Begin(mode);
		}

		public void Dispose()
		{
			GL.End();
		}
	}
}