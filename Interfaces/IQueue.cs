namespace Interfaces
{
    public interface IQueue
    {
        ICommand Get();

        void Put(ICommand command);

        int Count();
    }
}
