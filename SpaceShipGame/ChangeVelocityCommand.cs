using Interfaces;
using SpaceShipGame.Exeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceShipGame
{
    public class ChangeVelocityCommand: IChangeVelocity
    {
        private IUObject UObject;
        private decimal NewVelocity;

        public ChangeVelocityCommand(IUObject uObject, decimal newVelocity)
        {
            UObject = uObject;
            NewVelocity = newVelocity;
        }

        public void Execute()
        {
            if (NewVelocity < 0)
            {
                throw new CommandException();
            }

            var velocity = (decimal)UObject.GetProperty("velocity");
            
            if (velocity == 0)
            {
                return;
            }

            UObject.SetProperty("velocity", NewVelocity);
        }
    }
}
