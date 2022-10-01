using CommandQueue;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandQueue
{
    public abstract class State
    {
        public virtual State Handle(CommandQueueHandler commandQueueHandler)
        {
            return null;
        }
    }
}
