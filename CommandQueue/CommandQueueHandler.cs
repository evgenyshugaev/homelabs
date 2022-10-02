using Interfaces;
using System;

namespace CommandQueue
{
    /// <summary>
    /// Обработка очереди команд.
    /// </summary>
    public class CommandQueueHandler: ICommand
    {
        public IQueue Queue { get;  private set; }

        public IQueue SecondaryQueue { get; private set; }

        public bool IsStoped
        {
            get { return State == null;  }
        }

        public bool IsSimple
        {
            get { return State is SimpleState; }
        }

        public bool IsMoveTo
        {
            get { return State is MoveToState; }
        }


        private State State;

        public CommandQueueHandler(IQueue queue, IQueue secondaryQueue = null)
        {
            Queue = queue;
            SecondaryQueue = secondaryQueue;
            State = new SimpleState();
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

            State = null;
        }

        private void Handle(IQueue queue)
        {
            State = State.Handle(this);
        }
    }
}
