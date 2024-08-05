namespace DG
{
    public class DecoratorNode : BehaviourTreeCompositeNode
    {
        #region field

        public EBehaviourTreeNodeStatus untilStatus;
        public int countLimit;
        public int curCount;

        #endregion

        #region ctor

        public DecoratorNode(EBehaviourTreeNodeStatus untilStatus, int countLimit = -1)
        {
            this.untilStatus = untilStatus;
            this.countLimit = countLimit;
        }

        #endregion

        #region override method

        /// <summary>
        ///   只包含一个子节点
        /// </summary>
        /// <returns></returns>
        public override EBehaviourTreeNodeStatus Update()
        {
            if (childList == null || childList.Count == 0)
            {
                curCount = 0;
                status = EBehaviourTreeNodeStatus.Success;
                return status;
            }

            var child = childList[0];
            var childStatus = child.Update();
            if (childStatus == untilStatus)
            {
                status = EBehaviourTreeNodeStatus.Success;
                return status;
            }

            if (curCount == -1)
            {
                curCount = 0;
                status = EBehaviourTreeNodeStatus.Running;
                return status;
            }

            curCount++;
            if (curCount >= countLimit)
            {
                curCount = 0;
                status = EBehaviourTreeNodeStatus.Fail;
                return status;
            }

            status = EBehaviourTreeNodeStatus.Running;
            return status;
        }

        #endregion
    }
}