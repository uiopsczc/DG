using System;
using System.Collections.Generic;

namespace DG
{
	public class SyncUpdate
	{
		#region field

		private readonly List<Action> _runnableList = new();
		private readonly object _lockObj = new();

		#endregion

		#region public method

		public void Run(Action runnable)
		{
			if (runnable == null) return;
			lock (_lockObj)
				_runnableList.Add(runnable);
		}

		public void Update()
		{
			lock (_lockObj)
			{
				var count = _runnableList.Count;
				if (count > 0)
				{
					for (var i = 0; i < count; i++)
						_runnableList[i]();
					_runnableList.Clear();
				}
			}
		}

		#endregion
	}
}