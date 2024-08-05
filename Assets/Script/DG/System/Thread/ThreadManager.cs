using System.Collections.Generic;
using System.Threading;

namespace DG
{
    public class ThreadManager : ISingleton
    {
        public static ThreadManager instance => SingletonFactory.instance.Get<ThreadManager>();
        public List<Thread> list = new();
        public Dictionary<string, Thread> name2Thread = new();


        public void Init()
        {
        }

        public void Abort()
        {
            for (int i = 0; i < list.Count; i++)
                list[i].Abort();
            list.Clear();
            foreach (var kv in name2Thread)
                kv.Value.Abort();
            name2Thread.Clear();
        }

        public void Start(object args = null)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var t = list[i];
                if (args == null)
                    t.Start();
                else
                    t.Start(args);
            }

            foreach (var kv in name2Thread)
            {
                var t = kv.Value;
                if (args == null)
                    t.Start();
                else
                    t.Start(args);
            }
        }

        public void Add(ParameterizedThreadStart threadCallback)
        {
            list.Add(new Thread(threadCallback));
        }

        public void Add(ThreadStart threadCallback)
        {
            list.Add(new Thread(threadCallback));
        }
    }
}