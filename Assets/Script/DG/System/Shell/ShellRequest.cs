using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DG
{
	public class ShellRequest
	{
		public event Action<EDGLogLevel, string> onLogAction;
		public event Action onErrorAction;
		public event Action onDoneAction;

		public void Log(EDGLogLevel logLevel, string log)
		{
			onLogAction?.Invoke(logLevel, log);

			if (logLevel == EDGLogLevel.Error)
				DGLog.Error(log);
		}

		public void NotifyDone()
		{
			onDoneAction?.Invoke();
		}

		public void Error()
		{
			onErrorAction?.Invoke();
		}

		public TaskAwaiter GetAwaiter()
		{
			var tcs = new TaskCompletionSource<object>();
			onDoneAction += () => { tcs.SetResult(true); };
			return ((Task)tcs.Task).GetAwaiter();
		}
	}
}