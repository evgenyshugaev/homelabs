using Interfaces;
using Lab8Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandQueue
{
    public class MoveToState : State
    {
        public override State Handle(CommandQueueHandler commandQueueHandler)
        {
            if (commandQueueHandler.Queue.Count() == 0)
            {
                return null;
            }

            ICommand command = commandQueueHandler.Queue.Get();

            try
            {
                if (command is RunCommand)
                {
                    return new SimpleState();
                }

                commandQueueHandler.SecondaryQueue.Put(command);
            }
            catch (Exception ex)
            {
                ICommand loger = new LogCommand(command, ex);
                loger.Execute();
            }

            return this;
        }
    }
}
