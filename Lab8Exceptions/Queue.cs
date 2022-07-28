using Interfaces;
using System.Collections.Generic;

namespace Lab8Exceptions
{
    /// <summary>
    /// Очередь для комманд.
    /// </summary>
    public static class Queue
    {
        private static List<ICommand> Commands;

        static Queue ()
        {
            Commands = new List<ICommand>();
        }


        public static void PushCommand(ICommand command)
        {
            Commands.Add(command);
        }

        public static List<ICommand> GetCommandList ()
        {
            return Commands;
        }

        public static void ClearQueue()
        {
            Commands.Clear();
        }

        public static void DeleteCommand(ICommand command)
        {
            Commands.Remove(command);
        }
    }
}
