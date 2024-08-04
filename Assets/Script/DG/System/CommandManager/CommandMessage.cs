namespace DG
{
	public class CommandMessage : ICommandMessage
	{
		private object _body;
		private string _type;
		private readonly string _name;

		public CommandMessage(string name, object body) : this(name, body, null)
		{
		}

		public CommandMessage(string name, object body = null, string type = null)
		{
			_name = name;
			_body = body;
			_type = type;
		}

		public virtual string name => _name;

		public virtual object body
		{
			get => _body;
			set => _body = value;
		}

		public virtual string type
		{
			get => _type;
			set => _type = value;
		}

		public override string ToString()
		{
			return string.Format("Notification Name:{0}\nBody:{1}\nType:{2}", name, body, type);
		}
	}
}