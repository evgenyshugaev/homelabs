using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandQueue
{
    /// <summary>
    /// Остановка очереди после выполнения всех команд в ней.
    /// </summary>
    public class SoftStopCommand : ICommand
    {
        IUObject UObject;

        public SoftStopCommand(IUObject uobject)
        {
            UObject = uobject;
        }

        public void Execute()
        {
            ((CommandQueueHandler)UObject.GetProperty("CommandQueueHandler")).CommandQueueStrategy = (State state) => { return state != null; };
        }
    }
}
