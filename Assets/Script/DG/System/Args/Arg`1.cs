using System;
using System.Text;

namespace DG
{
    public class Args<T>
    {
        private T[] _args;

        public Args(params T[] args)
        {
            Init(args);
        }

        public Args(T args0, params T[] args)
        {
            Init(args0, args);
        }

        public void Init(params T[] args)
        {
            _args = args;
        }

        public void Init(T args0, params T[] args)
        {
            int offset = 1;
            T[] result = new T[args?.Length + offset ?? 0];
            result[0] = args0;
            if (args != null)
                Array.Copy(args, 0, result, 1, args.Length);
            _args = result;
        }

        public override bool Equals(object obj)
        {
            Args<T> other = (Args<T>)obj;

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