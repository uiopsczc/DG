namespace DG
{
    public class RandomSelectorNode : BehaviourTreeCompositeNode
    {
        protected RandomManager _randomManager;

        public RandomSelectorNode(RandomManager randomManager = null)
        {
            _randomManager = randomManager;
        }

        #region override method

        public override EBehaviourTreeNodeStatus Update()
        {
            if (childList == null || childList.Count == 0)
            {
                status = EBehaviourTreeNodeStatus.Success;
                return status;
            }

            var random = _randomManager.RandomInt(0, childList.Count);
            var childStatus = childList[random].Update();
            status = childStatus;
            return status;
        }

        #endregion
    }
}