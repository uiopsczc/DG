using System;

namespace DG
{
    public class AOPScope : IDisposable
    {
        private Action _preCallback;
        private Action _postCallback;

        public AOPScope(Action preCallback, Action postCallback)
        {
            _preCallback = preCallback;
            _postCallback = postCallback;
            preCallback?.Invoke();
        }


        public void Dispose()
        {
            _postCallback?.Invoke();
        }
    }
}