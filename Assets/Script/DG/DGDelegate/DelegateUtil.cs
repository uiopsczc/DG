using System;

namespace DG
{
	public static class DelegateUtil
	{
		public static Delegate CreateGenericAction(Type[] genericTypes, object target, string methodName)
		{
			Type actionType = typeof(Action<>).MakeGenericType(genericTypes);

			var targetMethodInfo = target.GetGenericMethodInfo2(methodName, genericTypes);
			Delegate result = Delegate.CreateDelegate(actionType, (target is Type) ? null : target, targetMethodInfo);
			return result;
		}

		public static Delegate CreateGenericFunc(Type[] genericTypes, object target, string methodName)
		{
			Type actionType = typeof(Func<>).MakeGenericType(genericTypes);

			var targetMethodInfo = target.GetGenericMethodInfo2(methodName, genericTypes);
			Delegate result = Delegate.CreateDelegate(actionType, (target is Type) ? null : target, targetMethodInfo);
			return result;
		}


		/// <summary>
		/// ���ò�ɾ�������Ⱥ�˳����isRemoveBeforeInvoke������
		/// ���Delegate�еĲ������һ������������Ϊremove���delegate�ᱻɾ��  ������ DelegateInfo
		/// </summary>
		public static void InvokeThenRemove(ref Delegate delegation, params object[] delegationArgs)
		{
			if (delegation == null)
				return;
			delegation.DynamicInvoke(delegationArgs);
			DelegateInfo delegateInfo = DelegateInfo.GetDelegateInfo(delegation);
			RemoveDelegate(ref delegation, delegateInfo.toRemove);
		}


		public static void InvokeThenRemove<T0>(ref Action<T0> delegation)
		{
			Delegate d = delegation;
			InvokeThenRemove(ref d);
		}

		public static void InvokeThenRemove<T0, T1>(ref Action<T0, T1> delegation, T0 args0)
		{
			Delegate d = delegation;
			InvokeThenRemove(ref d, args0);
		}

		public static void InvokeThenRemove<T0, T1, T2>(ref Action<T0, T1, T2> delegation, T0 args0, T1 args1)
		{
			Delegate d = delegation;
			InvokeThenRemove(ref d, args0, args1);
		}

		public static void InvokeThenRemove<T0, T1, T2, T3>(ref Action<T0, T1, T2, T3> delegation, T0 args0, T1 args1,
			T2 args2)
		{
			Delegate d = delegation;
			InvokeThenRemove(ref d, args0, args1, args2);
		}

		public static void InvokeIfNotNull(Delegate self, params object[] delegationArgs)
		{
			self?.DynamicInvoke(delegationArgs);
		}

		public static Delegate InsertFirst(Delegate delegation, Delegate firstDelegation)
		{
			return Delegate.Combine(firstDelegation, delegation);
		}

		private static void RemoveDelegate(ref Delegate delegation, Delegate delegateToRemove)
		{
			for (int i = delegateToRemove.GetInvocationList().Length - 1; i >= 0; i--)
			{
				delegation = Delegate.Remove(delegation, delegateToRemove.GetInvocationList()[i]);
			}
		}
	}
}

