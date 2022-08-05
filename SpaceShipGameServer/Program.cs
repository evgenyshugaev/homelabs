using System;

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
