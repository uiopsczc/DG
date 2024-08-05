using UnityEngine;

namespace DG
{
    public class YieldAsync : YieldBase
    {
        public AsyncOperation asyncOperation;

        public YieldAsync(AsyncOperation asyncOperation)
        {
            this.asyncOperation = asyncOperation;
        }

        public override bool IsDone(float deltaTime)
        {
            return _CheckIsStarted() && asyncOperation.isDone;
        }
    }
}