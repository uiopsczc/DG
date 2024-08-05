using System;

namespace DG
{
    public class MethodInvoker
    {
        private readonly object _target;
        private readonly string _methodName;
        private object[] _args;
        private readonly Delegate _delegation;

        public MethodInvoker(object target, string methodName, params object[] args)
        {
            _target = target;
            _methodName = methodName;
            _args = args;
        }

        public MethodInvoker(Delegate delegation)
        {
            _delegation = delegation;
        }

        public object Invoke(params object[] args)
        {
            if (args.Length != 0)
                _args = args;
            //二者只会有一个被调用
            if (_delegation != null)
                return _delegation.DynamicInvoke(new object[] { _args });
            return _target.InvokeMethod<object>(_methodName, false, _args);
        }

        public T Invoke<T>(params object[] args)
        {
            return (T)Invoke(args);
        }
    }
}