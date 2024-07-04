namespace DG
{
	public partial class GLUtil
	{
		public static GLBeginScope Begin(int mode)
		{
			return new GLBeginScope(mode);
		}
	}
}