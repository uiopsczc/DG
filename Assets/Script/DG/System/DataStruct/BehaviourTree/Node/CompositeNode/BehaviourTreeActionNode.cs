namespace DG
{
    public class BehaviourTreeActionNode : BehaviourTreeNode
    {
        #region override method

        public override EBehaviourTreeNodeStatus Update()
        {
            status = status == EBehaviourTreeNodeStatus.WaitingToRun ? Enter() : Execute();

            if (status != EBehaviourTreeNodeStatus.Running)
                Exit();
            return status;
        }

        #endregion

        #region virtual method

        public virtual EBehaviourTreeNodeStatus Enter()
        {
            status = EBehaviourTreeNodeStatus.Running;
            return status;
        }

        public virtual EBehaviourTreeNodeStatus Execute()
        {
            return status;
        }

        public virtual void Exit()
        {
        }

        public virtual void OnInterrupt()
        {
        }

        public override void Interrupt()
        {
            OnInterrupt();
            base.Interrupt();
        }

        #endregion
    }
}