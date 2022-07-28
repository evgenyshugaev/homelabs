using Interfaces;
using System;

namespace Lab8Exceptions
{
    /// <summary>
    /// Команды для примера.
    /// </summary>
    public class SimpleCommand1 : ICommand
    {
        public void Execute()
        {
            DoSomething();
        }

        private void DoSomething()
        {
            throw new Exception("Ошибка DoSomething");
        }
    }
}
