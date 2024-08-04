namespace DG
{
	public sealed class ByteOrder
	{
		private readonly string _name;
		public static readonly ByteOrder BIG_ENDIAN = new("BIG_ENDIAN");
		public static readonly ByteOrder LITTLE_ENDIAN = new("LITTLE_ENDIAN");

		private ByteOrder(string name)
		{
			_name = name;
		}


		public override string ToString()
		{
			return _name;
		}
	}
}