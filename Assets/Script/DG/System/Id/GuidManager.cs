namespace DG
{
	public class GuidManager
	{
		private ulong _keyNumber;

		public GuidManager(ulong currentKeyNumber)
		{
			this._keyNumber = currentKeyNumber;
		}

		public GuidManager()
		{
		}

		public string NewGuid(string id = null)
		{
			_keyNumber++;
			return (id.IsNullOrWhiteSpace() ? StringConst.STRING_EMPTY : id) + IdConst.RID_INFIX + _keyNumber;
		}
	}
}