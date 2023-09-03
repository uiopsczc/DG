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
			this._args = args;
		}

		public void Init(T args0, params T[] args)
		{
			int offset = 1;
			T[] _args = new T[args?.Length + offset ?? 0];
			_args[0] = args0;
			if (args != null)
				Array.Copy(args, 0, _args, 1, args.Length);
			this._args = _args;
		}

		public override bool Equals(object obj)
		{
			Args<T> other = (Args<T>) obj;

			if (other == null)
				return false;

			if (this._args == null && other._args == null)
				return true;
			if (this._args == null && other._args != null)
				return false;
			if (this._args != null && other._args == null)
				return false;

			if (this._args.Length == other._args.Length)
			{
				for (int i = 0; i < this._args.Length; i++)
				{
					if (!ObjectUtil.Equals(this._args[i], other._args[i]))
						return false;
				}
				return true;
			}

			return false;
		}

		public override int GetHashCode()
		{
			return ObjectUtil.GetHashCode(_args);
		}

		public override string ToString()
		{
			var result = new StringBuilder("(");
			if (this._args == null)
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