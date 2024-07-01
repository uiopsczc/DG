using System;
using System.Diagnostics;
using System.Reflection;

namespace DG
{
	public static class StackTraceUtil
	{
		//����StackTrace����offsetIndex+index��ȡ�����õĺ���
		public static MethodBase GetMethodOfFrame(int index = 0)
		{
			StackTrace stackTrace = new StackTrace();

			int offsetIndex = 1; //��ǰ�ǵ�һ��
			int targetIndex = offsetIndex + index;
			//UnityEngine.LogCat.LogWarning(targetIndex + " "+stackTrace.GetFrame(targetIndex).GetMethod().Name+" "+ stackTrace.GetFrame(targetIndex).GetMethod().DeclaringType);
			return stackTrace.GetFrame(targetIndex)?.GetMethod();
		}
	}
}

