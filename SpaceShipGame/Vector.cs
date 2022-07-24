using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceShipGame
{
    /// <summary>
    /// Координаты объекта в пространстве
    /// </summary>
    public class Vector
    {
        public double x;
        public double y;

        public Vector()
        {

        }

        public Vector(double newX, double newY)
        {
            x = newX;
            y = newY;
        }
    }
}
