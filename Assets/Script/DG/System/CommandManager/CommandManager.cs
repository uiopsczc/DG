using System;
using System.Collections.Generic;

namespace DG
{
	public class CommandManager : ICommandManager
	{
		private readonly Dictionary<string, Type> _commandDict = new();


		public virtual void ExecuteCommand(ICommandMessage commandMessage)
		{
			_commandDict.TryGetValue(commandMessage.name, out var commandType);
			if (commandType == null) return;
			var commandInstance = Activator.CreateInstance(commandType);
			if (commandInstance is ICommand command) command.Execute(commandMessage);
		}

		public virtual void RegisterCommand<HandleCommandType>(string commandName)
		{
			_commandDict[commandName] = typeof(HandleCommandType);
		}

		public virtual bool HasCommand(string commandName)
		{
			return _commandDict.ContainsKey(commandName);
		}

		public virtual void RemoveCommand(string commandName)
		{
			if (_commandDict.ContainsKey(commandName)) _commandDict.Remove(commandName);
		}

		public void SendMessageCommand(string message, object body = null)
		{
			ExecuteCommand(new CommandMessage(message, body));
		}
	}
}