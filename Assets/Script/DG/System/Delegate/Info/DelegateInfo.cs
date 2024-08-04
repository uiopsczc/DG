using System;
using System.Reflection;

namespace DG
{
	public class DelegateInfo
	{
		public Delegate toRemove;
		private Delegate _remain;

		private DelegateInfo(Delegate remain)
		{
			_remain = remain;
		}

		//如果Delegate中的参数最后一个参数的名称为remove则该delegate会被删除
		public static DelegateInfo GetDelegateInfo(Delegate sources)
		{
			DelegateInfo result = new DelegateInfo(sources);
			if (sources == null)
				return result;
			for (int i = sources.GetInvocationList().Length - 1; i >= 0; i--)
				SetDelegateInfo(result, sources.GetInvocationList()[i]);

			return result;
		}

		private static void SetDelegateInfo(DelegateInfo delegateInfo, Delegate delegateToRemove)
		{
			ParameterInfo[] delegateParameterInfos = delegateToRemove.Method.GetParameters();
			if (delegateParameterInfos.Length == 0)
				return;
			//最后一个参数的名称不为remove开始的话，则不用处理
			if (!delegateParameterInfos[delegateParameterInfos.Length - 1].Name
				.StartsWith(StringConst.STRING_REMOVE))
				return;
			delegateInfo.toRemove = Delegate.Combine(delegateInfo.toRemove, delegateToRemove);
			delegateInfo._remain = Delegate.Remove(delegateInfo._remain, delegateToRemove);
		}
	}
}