using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceShipGame
{
    /// <summary>
    /// Команда проверки столкновений двух объектов
    /// </summary>
    public class ObjectCollisionCheckCommand : ICommand
    {
        private IUObject SectorSpaceShip;
        private IUObject CurrentSpaceShip;

        public ObjectCollisionCheckCommand(IUObject sectorSpaceShip, IUObject currentSpaceShip)
        {
            SectorSpaceShip = sectorSpaceShip;
            CurrentSpaceShip = currentSpaceShip;
        }

        public void Execute()
        {
            //Здесь алгоритм определения столкновений объектов
        }
    }
}
