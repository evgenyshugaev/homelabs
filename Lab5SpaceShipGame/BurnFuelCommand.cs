﻿using Lab5SpaceShipGame.Exeptions;

namespace Lab5SpaceShipGame
{
    public class BurnFuelCommand: IBurnFuel
    {
        private IUObject UObject;
        private decimal Fuel;

        public BurnFuelCommand(IUObject uObject, decimal fuel)
        {
            UObject = uObject;
            Fuel = fuel;
        }

        public void Execute()
        {
            var newFuel = (decimal)UObject.GetProperty("fuel") - Fuel;
            if (newFuel < 0)
            {
                throw new CommandException();
            }

            UObject.SetProperty("fuel", newFuel);
        }
    }
}