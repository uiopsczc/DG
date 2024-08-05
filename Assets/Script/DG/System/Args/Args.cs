using System;
using System.Text;

namespace DG
{
    public class Args
    {
        private object[] _args;

        public Args()
        {
        }

        public Args(params object[] args)
        {
            Init(args);
        }

        public Args(object args0, params object[] args)
        {
            Init(args0, args);
        }

        public void Init(params object[] args)
        {
            _args = args;
        }

        public void Init(object args0, params object[] args)
        {
            int offset = 1;
            object[] _args = new object[args?.Length + offset ?? 0];
            _args[0] = args0;
            if (args != null)
                Array.Copy(args, 0, _args, 1, args.Length);
            this._args = _args;
        }

        public override bool Equals(object obj)
        {
            Args other = (Args)obj;

            return other != null && ObjectUtil.EqualsArray(_args, other._args);
        }

        public override int GetHashCode()
        {
            return ObjectUtil.GetHashCode(_args);
        }

        public override string ToString()
        {
            var result = new StringBuilder("(");
            if (_args == null)
            {
                result.Append(")");
                return result.ToString();
            }

            for (int i = 0; i < _args.Length; i++)
            {
                var arg = _args[i];
                result.Append(arg);
                if (i != _args.Length - 1)
                    result.Append(",");
            }

            result.Append(")");
            return result.ToString();
        }
    }
}