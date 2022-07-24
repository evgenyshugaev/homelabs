using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5SpaceShipGame
{
    /// <summary>
    /// Задает поворот объекта в одномерном пространстве.
    /// </summary>
    public interface IRotable
    {
        int GetDirection();
        Vector GetAngularVelocity();
        void SetDirection(int direction);
        int GetDirectionsNumber();
    }
}
