using Interfaces;
using System.Collections.Generic;

namespace CommandQueue
{
    /// <summary>
    /// Очередь команд.
    /// </summary>
    public class CommandQueue : IQueue
    {
        Queue<ICommand> Queue;

        public CommandQueue()
        {
            Queue = new Queue<ICommand>();
        }

        public int Count()
        {
            return Queue.Count;
        }

        public ICommand Get()
        {
            return Queue.Dequeue();
        }

        public void Put(ICommand command)
        {
            Queue.Enqueue(command);
        }
    }
}
