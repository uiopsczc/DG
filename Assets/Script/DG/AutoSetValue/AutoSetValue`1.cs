using System;

namespace DG
{
	public struct AutoSetValue<T>
	{
		public T postValue;

		public T preValue;

		public AutoSetValue<T> IfChanged(Action<T, T> action)
		{
			if (!ObjectUtil.Equals(preValue, postValue))
				action(preValue, postValue);
			return this;
		}
	}
}