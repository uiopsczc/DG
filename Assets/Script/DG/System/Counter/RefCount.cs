using System;

namespace DG
{
    public class RefCount
    {
        private readonly Counter _counter = new();

        public int count => _counter.count;

        public void Increase()
        {
            _counter.Increase();
        }

        public void Decrease()
        {
            _counter.Decrease();
        }

        public void Reset()
        {
            _counter.Reset();
        }


        public void AddChangeValueCallback(Action callback)
        {
            _counter.AddChangeValueCallback(callback);
        }
    }
}