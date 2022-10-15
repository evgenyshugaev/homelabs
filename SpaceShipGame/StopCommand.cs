using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace SpaceShipGame
{
    /// <summary>
    /// Команда остановки.
    /// </summary>
    public class StopCommand : ICommand
    {
        private IUObject UObject;

        public StopCommand(IUObject uObject)
        {
            UObject = uObject;
        }

        public void Execute()
        {
            // Команда остановки
            UObject.SetProperty("velocity", 0);
        }
    }
}
