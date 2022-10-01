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
        public IQueue Queue { get;  private set; }
        //public int QueueCount { get { return Queue.Count(); } }

        public IQueue SecondaryQueue { get; private set; }
        //public int SecondaryQueueCount { get { return SecondaryQueue.Count(); } }

        private State State;

        public CommandQueueHandler(IQueue queue, State state, IQueue secondaryQueue = null)
        {
            Queue = queue;
            State = state;
            SecondaryQueue = secondaryQueue;
        }

        public Func<State, bool> CommandQueueStrategy = (State state) =>
        {
            return state != null;
        };

        public void Execute()
        {
            while(CommandQueueStrategy(State))
            {
                Handle(Queue);
            }
        }

        private void Handle(IQueue queue)
        {
            State = State.Handle(this);
        }
    }
}
