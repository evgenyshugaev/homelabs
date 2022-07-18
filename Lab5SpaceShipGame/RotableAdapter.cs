using System;

namespace Lab5SpaceShipGame
{
    public class RotableAdapter : IRotable
    {
        private IUObject UObject;

        public RotableAdapter(IUObject uObject)
        {
            this.UObject = uObject;
        }

        public Vector GetAngularVelocity()
        {
            int d = (int)UObject.GetProperty("Direction");
            int n = (int)UObject.GetProperty("DirectionsNumber");
            int v = (int)UObject.GetProperty("Velocity");

            double x = Math.Cos(2 * Math.PI / n * d);
            double y = Math.Sin(2 * Math.PI / n * d);
            return new Vector()
            {
               x = v * (x * Math.Cos(2 * Math.PI / n * d) + y * Math.Sin(2 * Math.PI / n * d)),
               y = v * (x * Math.Sin(2 * Math.PI / n * d) + y * Math.Cos(2 * Math.PI / n * d)),
            };
        }

        public int GetDirection()
        {
            return (int)UObject.GetProperty("Direction");
        }

        public int GetDirectionsNumber()
        {
            return (int)UObject.GetProperty("DirectionsNumber");
        }

        public void SetDirection(int direction)
        {
            UObject.SetProperty("Direction", direction);
        }
    }
}
