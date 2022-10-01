using Interfaces;

namespace CommandQueue
{
    /// <summary>
    /// Принудительная остановка очереди.
    /// </summary>
    public class HardStopCommand : ICommand
    {
        IUObject UObject;

        public HardStopCommand(IUObject uobject)
        {
            UObject = uobject;
        }
        
        public void Execute()
        {
            ((CommandQueueHandler)UObject.GetProperty("CommandQueueHandler")).CommandQueueStrategy = (State zero) => { return false; };
        }
    }
}
