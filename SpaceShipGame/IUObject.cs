using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5SpaceShipGame
{
    /// <summary>
    /// Присвоение/получение свойств универсального объекта.
    /// </summary>
    public interface IUObject
    {
        object GetProperty(string key);
        void SetProperty(string key, object value);
    }
}
