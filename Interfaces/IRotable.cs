using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
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
