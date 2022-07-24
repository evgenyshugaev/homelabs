using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceShipGame
{
    /// <summary>
    /// Движение объекта в одномерном пространстве.
    /// </summary>
    public interface IMovable
    {
        Vector GetPosition();
        Vector GetVelocity();
        void SetPosition(Vector vector);
    }
}
