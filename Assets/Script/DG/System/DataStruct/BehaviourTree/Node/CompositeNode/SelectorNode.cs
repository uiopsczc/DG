namespace DG
{
    public class SelectorNode : BehaviourTreeCompositeNode
    {
        #region override method

        public override EBehaviourTreeNodeStatus Update()
        {
            if (childList == null || childList.Count == 0)
            {
                status = EBehaviourTreeNodeStatus.Success;
                return status;
            }

            for (var i = 0; i < childList.Count; i++)
            {
                var child = childList[i];
                var childStatus = child.Update();
                if (childStatus == EBehaviourTreeNodeStatus.Running || childStatus == EBehaviourTreeNodeStatus.Success)
                {
                    status = childStatus;
                    return status;
                }
            }

            status = EBehaviourTreeNodeStatus.Fail;
            return status;
        }

        #endregion
    }
}