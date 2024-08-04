namespace DG
{
	public class SequenceNode : BehaviourTreeCompositeNode
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
				if (childStatus == EBehaviourTreeNodeStatus.Running || childStatus == EBehaviourTreeNodeStatus.Fail)
				{
					status = childStatus;
					return status;
				}
			}

			status = EBehaviourTreeNodeStatus.Success;
			return status;
		}

		#endregion
	}
}