using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
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
