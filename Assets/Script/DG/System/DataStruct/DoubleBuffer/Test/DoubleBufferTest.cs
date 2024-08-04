using System.Threading;
using UnityEngine;

namespace DG
{
	/// <summary>
	///   双缓冲
	/// </summary>
	public class DoubleBufferTest : MonoBehaviour
	{
		private DoubleBuffer<string> db;

		private void Start()
		{
			db = new DoubleBuffer<string>(t => { DGLog.ErrorFormat("consuming {0}", t); },
			  t => { DGLog.WarnFormat("producing {0}", t); });

			CreateProduceThread("ProduceThread A", 500);
			CreateConsumeThread();
			CreateProduceThread("ProduceThread B", 500);
			CreateProduceThread("ProduceThread C", 1000);

			ThreadManager.instance.Start();

			//使用ThreadManager.Instace.Abort()退出所有线程
			//菜单是CZMTool->退出所有线程
		}


		private void CreateConsumeThread()
		{
			ThreadManager.instance.Add(() =>
			{
				while (true)
				{
					db.Consume();
					Thread.Sleep(3000);
				}
			});
		}

		private void CreateProduceThread(string id, int intervalMs)
		{
			ThreadManager.instance.Add(() =>
			{
				var i = 0;
				while (true)
				{
					db.Produce(string.Format("{0}:{1}", id, i));
					i++;
					Thread.Sleep(intervalMs);
				}
			});
		}
	}
}