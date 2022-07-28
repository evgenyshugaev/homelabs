using Interfaces;
using System;
using System.Diagnostics;

namespace Lab8Exceptions
{
    /// <summary>
    /// Стратегия пишет в лог.
    /// </summary>
    public class LogCommand : ICommand
    {
        private ICommand Command;
        private Exception ExceptionCommand;
        
        public LogCommand(ICommand command, Exception exception)
        {
            Command = command;
            ExceptionCommand = exception;
        }

        public void Execute()
        {
            Debug.WriteLine($"Command {Command.GetType()} execute failed. {ExceptionCommand.Message}, {ExceptionCommand.StackTrace}");
        }
    }
}
