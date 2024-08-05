using System;

namespace DG
{
    public class BehaviourTreeNode
    {
        #region field

        public BehaviourTreeNode parent;
        private EBehaviourTreeNodeStatus _status = BehaviourTreeNodeConst.DEFAULT_STATUS;

        #endregion

        #region delegate

        public Action onSuccess;
        public Action onFail;

        #endregion

        #region property

        public EBehaviourTreeNodeStatus status
        {
            get => _status;
            set
            {
                _status = value;
                switch (_status)
                {
                    case EBehaviourTreeNodeStatus.Success when onSuccess != null:
                        onSuccess();
                        break;
                    case EBehaviourTreeNodeStatus.Fail:
                        onFail?.Invoke();
                        break;
                }
            }
        }

        #endregion

        #region virtual method

        public virtual EBehaviourTreeNodeStatus Update()
        {
            return EBehaviourTreeNodeStatus.Fail;
        }


        public virtual T GetChild<T>(bool loop = false) where T : BehaviourTreeNode
        {
            return null;
        }

        public virtual void RestStatus()
        {
            status = BehaviourTreeNodeConst.DEFAULT_STATUS;
        }

        public virtual void Interrupt()
        {
            status = EBehaviourTreeNodeStatus.WaitingToRun;
        }

        #endregion
    }
}