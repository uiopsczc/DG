namespace DG
{
    /// <summary>
    ///   全部返回成功才会成功（或者一个返回失败）
    /// </summary>
    public class ParallelNode : BehaviourTreeCompositeNode
    {
        #region override method

        public override EBehaviourTreeNodeStatus Update()
        {
            if (childList == null || childList.Count == 0)
            {
                status = EBehaviourTreeNodeStatus.Success;
                return status;
            }

            var successCount = 0;
            for (var i = 0; i < childList.Count; i++)
            {
                var child = childList[i];
                var childStatus = child.Update();
                if (childStatus == EBehaviourTreeNodeStatus.Fail)
                {
                    status = EBehaviourTreeNodeStatus.Fail;
                    return status;
                }

                if (childStatus == EBehaviourTreeNodeStatus.Success)
                    successCount++;
            }

            if (successCount == childList.Count)
            {
                status = EBehaviourTreeNodeStatus.Success;
                return status;
            }

            status = EBehaviourTreeNodeStatus.Running;
            return status;
        }

        #endregion
    }
}