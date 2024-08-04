namespace DG
{
	public abstract class InputStream : DGStream
	{
		public abstract void Peek(byte[] buffer, int offset, int length);

		public abstract void Read(byte[] buffer, int offset, int length);
	}
}