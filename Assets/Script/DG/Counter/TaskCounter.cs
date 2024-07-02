using System;

namespace DG
{
	//ֻ��ִ��һ�ε�����counter
	//test���ӿ��Բο�F6��TaskCounter��ʹ��
	public class TaskCounter
	{
		private int _maxTaskCount;
		private int _curFinishTaskCount;
		private readonly bool _isCanFinishCallback; //�Ƿ��ܵ���_finishCallback
		private bool _isFinishCallbackInvoked; //�Ƿ�_finishCallback������
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
			if (this._isFinishCallbackInvoked) //ִֻ��һ��
				return;
			if (this._isCanFinishCallback) //�ܵ���_finishCallback
			{
				if (this.IsAllTaskFinished()) //����ȫ�������
				{
					this._isFinishCallbackInvoked = true;
					_finishCallback?.Invoke();
				}
			}
		}

		public void AddFinishTaskCount(int addValue)
		{
			this._curFinishTaskCount = this._curFinishTaskCount + addValue;
		}

		public bool IsAllTaskFinished()
		{
			return this._curFinishTaskCount >= this._maxTaskCount;
		}
	}
}