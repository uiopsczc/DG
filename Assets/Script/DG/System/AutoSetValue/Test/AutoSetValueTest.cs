namespace DG
{
	public class AutoSetValueTest
	{
		public static void Test()
		{
			int i = 4;
			AutoSetValueUtil.SetValue(ref i, 8).IfChanged((pre, post) => DGLog.Warn(string.Format("pre:{0} change to post:{1}",pre, post)));
		}
	}
}