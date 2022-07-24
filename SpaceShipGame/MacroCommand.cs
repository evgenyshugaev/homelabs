using Lab5SpaceShipGame.Exeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5SpaceShipGame
{
    public class MacroCommand : ICommand
    {
        private List<ICommand> Commands;
        public MacroCommand(List<ICommand> commands)
        {
            Commands = commands;
        }

        public void Execute()
        {
            try
            {
                foreach (var command in Commands)
                {
                    command.Execute();
                }
            }
            catch
            {
                throw new CommandException();
            }
        }
    }
}
