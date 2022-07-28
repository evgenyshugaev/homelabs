using Interfaces;
using System;

namespace SpaceShipGame
{
    public class MovableAdapter : IMovable
    {
        private IUObject UObject;

        public MovableAdapter(IUObject uObject)
        {
            this.UObject = uObject;
        }

        public Vector GetPosition()
        {
            return (Vector)UObject.GetProperty("Positiion");
        }

        public Vector GetVelocity()
        {
            try
            {
                int d = (int)UObject.GetProperty("Direction");
                int n = (int)UObject.GetProperty("DirectionsNumber");
                int v = (int)UObject.GetProperty("Velocity");

                return new Vector()
                {
                    x = v * Math.Cos(2 * Math.PI/n * d),
                    y = v * Math.Sin(2 * Math.PI/n * d),
                };
            }
            catch(Exception ex)
            {
                throw new Exception("Не возможно получить мгновенную скорость обхекта", ex.InnerException);
            }
        }

        public void SetPosition(Vector vector)
        {
            if (vector == null)
            {
                throw new NullReferenceException("Не определены координаты объекта");
            }
            
            UObject.SetProperty("Position", vector);
        }
    }
}
