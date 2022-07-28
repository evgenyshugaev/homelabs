using Interfaces;
using System;

namespace Lab8Exceptions
{
    /// <summary>
    /// Стратегия выполнить команду два раза.
    /// </summary>
    public class TwoRepeatCommand: ICommand
    {
        private ICommand Command;

        public TwoRepeatCommand(ICommand command)
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
                ICommand repeater = new OneRepeatCommand(Command);
                ExceptionHandler.Handle(repeater, exception);
            }
        }
    }
}
