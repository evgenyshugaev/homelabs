using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceShipGame
{
    /// <summary>
    /// Команда движения.
    /// </summary>
    public class MoveCommand : ICommand
    {
        private IUObject UObject;
        private decimal InitialVelocity;

        public MoveCommand(IUObject uObject, decimal initialVelocity)
        {
            UObject = uObject;
            InitialVelocity = initialVelocity;
        }

        public void Execute()
        {
            // Команда движения
            UObject.SetProperty("velocity", InitialVelocity);
        }
    }
}
