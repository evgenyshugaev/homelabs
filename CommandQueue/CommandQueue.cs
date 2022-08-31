using Interfaces;
using System.Collections.Concurrent;

namespace CommandQueue
{
    /// <summary>
    /// Очередь команд.
    /// </summary>
    public class CommandQueue : IQueue
    {
        ConcurrentQueue<ICommand> Queue;

        public CommandQueue()
        {
            Queue = new ConcurrentQueue<ICommand>();
        }

        public int Count()
        {
            return Queue.Count;
        }

        public ICommand Get()
        {
            Queue.TryDequeue(out ICommand cmd);
            return cmd;
        }

        public void Put(ICommand command)
        {
            Queue.Enqueue(command);
        }
    }
}
