using Lab5SpaceShipGame.Exeptions;

namespace Lab5SpaceShipGame
{
    public class CheckFuelCommand : ICheckFuel
    {
        private IUObject UObject;
        private decimal Fuel;

        public CheckFuelCommand(IUObject uObject, decimal fuel)
        {
            UObject = uObject;
            Fuel = fuel;
        }

        public void Execute()
        {
            if ((decimal)UObject.GetProperty("fuel") < Fuel)
            {
                throw new CommandException();
            }
        }
    }
}
