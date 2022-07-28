using Interfaces;
using System;
using System.Collections.Generic;

namespace Lab8Exceptions
{
    /// <summary>
    /// Обработчик ислючений.
    /// </summary>
    public static class ExceptionHandler
    {
        private static Dictionary<Type, Action<ICommand, Exception>> handlers;

        private static bool _logged;

        public static bool Logged
        {
            get
            {
                return _logged;
            }
        }

        static ExceptionHandler()
        {
            handlers = new Dictionary<Type, Action<ICommand, Exception>>();

            handlers.Add(typeof(SimpleCommand1), (ICommand command, Exception exception) => {
                ICommand repeater = new OneRepeatCommand(command);
                repeater.Execute();
                Queue.PushCommand(repeater);
            });


            handlers.Add(typeof(SimpleCommand2), (ICommand command, Exception exception) => {
                ICommand repeater = new TwoRepeatCommand(command);
                repeater.Execute();
                Queue.PushCommand(repeater);
            });

            handlers.Add(typeof(OneRepeatCommand), (ICommand command, Exception exception) => {
                ICommand loger = new LogCommand(command, exception);
                loger.Execute();
                Queue.PushCommand(loger);
                _logged = true;
            });

        }
        public static void Handle(ICommand command, Exception exeption)
        {
            _logged = false;
            handlers[command.GetType()](command, exeption);
        }
    }
}
