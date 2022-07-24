using SpaceShipGame.Exeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceShipGame
{
    public class ChangeVelocityRotateCommand : IChangeVelocityRotate
    {
        private IUObject UObject;
        private decimal NewVelocity;
        private int Direction;
        private int DirectionsNumber;

        public ChangeVelocityRotateCommand(IUObject uObject, decimal newVelocity, int direction, int directionsNumber)
        {
            UObject = uObject;
            NewVelocity = newVelocity;
            Direction = direction;
            DirectionsNumber = directionsNumber;
        }

        public void Execute()
        {
            var changeVelocityCommand = new ChangeVelocityCommand(UObject, NewVelocity);
            changeVelocityCommand.Execute();

            UObject.SetProperty("direction", Direction);
            UObject.SetProperty("directionsNumber", DirectionsNumber);
        }
    }
}
