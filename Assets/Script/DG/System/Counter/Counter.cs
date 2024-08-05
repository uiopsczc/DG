using System;

namespace DG
{
    public class Counter
    {
        private int _count;
        private Action _changeValueCallback;

        public int count => _count;

        public void Increase()
        {
            _count += 1;
            _CheckCallback();
        }

        public void Decrease()
        {
            _count -= 1;
            _CheckCallback();
        }

        public void Reset()
        {
            _count = 0;
            _changeValueCallback = null;
        }


        public void AddChangeValueCallback(Action callback)
        {
            _changeValueCallback += callback;
        }

        private void _CheckCallback()
        {
            _changeValueCallback?.Invoke();
        }
    }
}