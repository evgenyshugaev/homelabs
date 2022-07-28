using Interfaces;
using System;

namespace Lab8Exceptions
{
    public class SimpleCommand2: ICommand
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
