using System;
using System.Collections.Generic;

namespace DG
{
    public class CommandManager : ICommandManager
    {
        private readonly Dictionary<string, Type> _commandName2CommandType = new();


        public virtual void ExecuteCommand(ICommandMessage commandMessage)
        {
            _commandName2CommandType.TryGetValue(commandMessage.name, out var commandType);
            if (commandType == null) return;
            var commandInstance = Activator.CreateInstance(commandType);
            if (commandInstance is ICommand command) command.Execute(commandMessage);
        }

        public virtual void RegisterCommand<HandleCommandType>(string commandName)
        {
            _commandName2CommandType[commandName] = typeof(HandleCommandType);
        }

        public virtual bool HasCommand(string commandName)
        {
            return _commandName2CommandType.ContainsKey(commandName);
        }

        public virtual void RemoveCommand(string commandName)
        {
            if (_commandName2CommandType.ContainsKey(commandName)) _commandName2CommandType.Remove(commandName);
        }

        public void SendMessageCommand(string message, object body = null)
        {
            ExecuteCommand(new CommandMessage(message, body));
        }
    }
}