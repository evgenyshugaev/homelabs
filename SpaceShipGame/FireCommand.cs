using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceShipGame
{
    /// <summary>
    /// Комманда выстрел.
    /// </summary>
    public class FireCommand : ICommand
    {
        private IUObject UObject;
        private string FireDirection;
        private Vector FireCoordinate;

        public FireCommand(IUObject uObject, string fireDirection, Vector fireCoordinate)
        {
            UObject = uObject;
            FireDirection = fireDirection;
            FireCoordinate = fireCoordinate;
        }

        public void Execute()
        {
            // Стреляем
            UObject.SetProperty("fireDirection", FireDirection);
            UObject.SetProperty("fireCoordinate", FireCoordinate);
        }
    }
}
