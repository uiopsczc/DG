namespace DG
{
	public abstract class OutputStream : DGStream
	{
		public abstract void Flush();

		public abstract bool Write(byte[] buffer, int offset, int length);
	}
}