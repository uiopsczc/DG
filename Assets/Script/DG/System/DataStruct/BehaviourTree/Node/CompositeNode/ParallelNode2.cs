//一个返回成功则成功，一个返回失败则失败
namespace DG
{
	public class ParallelNode2 : BehaviourTreeCompositeNode
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
				if (childStatus == EBehaviourTreeNodeStatus.Fail)
				{
					status = EBehaviourTreeNodeStatus.Fail;
					return status;
				}

				if (childStatus == EBehaviourTreeNodeStatus.Success)
				{
					status = EBehaviourTreeNodeStatus.Success;
					return status;
				}
			}

			status = EBehaviourTreeNodeStatus.Running;
			return status;
		}

		#endregion
	}
}