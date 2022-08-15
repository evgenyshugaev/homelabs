using CodeGenerator;
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

            RegisterMovableAdapter();
        }

        private static void RegisterMovableAdapter()
        {
            Func<object[], object> f = (args) =>
            {
                ClassGeneratorCommand generator = new ClassGeneratorCommand((string)args[0], typeof(IMovable), (IUObject)args[1]);
                generator.Execute();
                return generator.Instance as IMovable;
            };

            Ioc.Resolve<IMovable>(
            "IoC.Register",
            "MovableAdapter",
            f);

            RegisterMovableAdapterGetPosition();
            RegisterMovableAdapterFinish();
        }

        private static void RegisterMovableAdapterGetPosition()
        {
            Func<object[], object> f = (args) =>
            {
                return (Vector)((IUObject)args[0]).GetProperty("Positiion");
            };

            Ioc.Resolve<IMovable>(
            "IoC.Register",
            "Operations.IMovable.GetPosition",
            f);
        }

        private static void RegisterMovableAdapterFinish()
        {
            Func<object[], object> f = (args) =>
            {
                Console.WriteLine("Method finish executed");
                return default(IMovable);
            };

            Ioc.Resolve<IMovable>(
            "IoC.Register",
            "Operations.IMovable.Finish",
            f);
        }
    }
}
