using System;
using System.Reflection;

namespace SpaceShipGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IocResolveStrategy.RegisterDependensies();
        }
    }
}
 