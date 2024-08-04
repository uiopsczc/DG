using System;
using System.Threading;

namespace DG
{
	public class LockScope : IDisposable
	{
		private object _lockObject;

		public bool isHasLock { get; private set; }

		public LockScope(object obj)
		{
			if (!Monitor.TryEnter(obj))
				return;

			isHasLock = true;
			_lockObject = obj;
		}

		public void Dispose()
		{
			if (!isHasLock)
				return;

			Monitor.Exit(_lockObject);
			_lockObject = null;
			isHasLock = false;
		}
	}
}