using SpaceShipGame.Exeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceShipGame
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
