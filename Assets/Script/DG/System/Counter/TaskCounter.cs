using System;

namespace DG
{
	//只会执行一次的任务counter
	//test例子可以参考F6的TaskCounter的使用
	public class TaskCounter
	{
		private int _maxTaskCount;
		private int _curFinishTaskCount;
		private readonly bool _isCanFinishCallback; //是否能调用_finishCallback
		private bool _isFinishCallbackInvoked; //是否_finishCallback调用了
		private Action _finishCallback;

		public TaskCounter(int maxTaskCount, int curFinishTaskCount = 0)
		{
			_maxTaskCount = maxTaskCount;
			_curFinishTaskCount = curFinishTaskCount;
			_isCanFinishCallback = false;
			_isFinishCallbackInvoked = false;
		}

		public void SetFinishCallback(Action finishCallback)
		{
			_finishCallback = finishCallback;
		}

		public void CheckFinishCallback()
		{
			if (_isFinishCallbackInvoked) //只执行一次
				return;
			if (_isCanFinishCallback) //能调用_finishCallback
			{
				if (IsAllTaskFinished()) //任务全部完成了
				{
					_isFinishCallbackInvoked = true;
					_finishCallback?.Invoke();
				}
			}
		}

		public void AddFinishTaskCount(int addValue)
		{
			_curFinishTaskCount += addValue;
		}

		public bool IsAllTaskFinished()
		{
			return _curFinishTaskCount >= _maxTaskCount;
		}
	}
}