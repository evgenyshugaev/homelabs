using Interfaces;
using SimpleIoc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommandQueue
{
    /// <summary>
    /// Команда запуска очереди.
    /// </summary>
    public class StartQueueCommand : ICommand
    {
        private CommandQueueHandler CommandQueueHandler;

        public StartQueueCommand(CommandQueueHandler commandQueueHandler)
        {
            CommandQueueHandler = commandQueueHandler;
        }

        public void Execute()
        {
            Thread thread = new Thread(() => CommandQueueHandler.Execute());
            thread.Start();
        }
    }
}
