using System;
using UnityEngine;

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