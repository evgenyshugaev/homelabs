using Interfaces;
using SimpleIoc;
using SpaceShipGame;
using System;

namespace SpaceShipGameServer
{
    public static class IocResolveStrategy
    {
        public static  void RegisterDependensies()
        {
            Func<object[], object> f = (args) => 
            {
                return new CheckFuelCommand((IUObject)args[0], (decimal)args[1]);
            };

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "CheckFuelCommand",
                f
            );
        }
    }
}
