using Interfaces;
using Lab8Exceptions;
using System;

namespace CommandQueue
{
    /// <summary>
    /// Обработка очереди команд.
    /// </summary>
    public class CommandQueueHandler: ICommand
    {
        private IQueue Queue;
        public int QueueCount { get { return Queue.Count(); } }

        public CommandQueueHandler(IQueue queue)
        {
            Queue = queue;
        }

        public Func<int, bool> CommandQueueStrategy = (int count) =>
        {
            return true;
        };

        public void Execute()
        {
            while(CommandQueueStrategy(QueueCount))
            {
                Handle(Queue);
            }
        }

        private void Handle(IQueue queue)
        {
            ICommand command = queue.Get();

            try
            {
                command.Execute();
            }
            catch (Exception ex)
            {
                ICommand loger = new LogCommand(command, ex);
                loger.Execute();
            }
        }
    }
}
