using Interfaces;
using System;

namespace Lab8Exceptions
{
    /// <summary>
    /// Стратегия выполнить 1 раз
    /// </summary>
    public class OneRepeatCommand : ICommand
    {
        private ICommand Command;

        public OneRepeatCommand(ICommand command)
        {
            Command = command;
        }

        public void Execute()
        {
            try
            {
                Command.Execute();
            }
            catch(Exception exception)
            {
                ExceptionHandler.Handle(this, exception);
            }
        }
    }
}
