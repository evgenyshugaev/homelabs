using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceShipGame
{
    public class SectorCheckCommand : ICommand
    {
        private IUObject Spaceship;
        private Sector FirstSector;

        public SectorCheckCommand(IUObject spaceship, Sector firstSector)
        {
            Spaceship = spaceship;
            FirstSector = firstSector;
        }

        public void Execute()
        {
            FirstSector.UpdateSpaceshipList(Spaceship);
        }
    }
}
