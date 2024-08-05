using System;
using System.Collections.Generic;

namespace DG
{
    public class EnumeratorScope<T> : IDisposable
    {
        public IEnumerator<T> iterator;


        public EnumeratorScope(IEnumerator<T> iterator)
        {
            this.iterator = iterator;
        }


        public void Dispose()
        {
            iterator.Dispose();
            iterator = null;
        }
    }
}