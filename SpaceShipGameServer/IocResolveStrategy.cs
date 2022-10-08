using CodeGenerator;
using CommandQueue;
using Interfaces;
using SimpleIoc;
using SpaceShipGame;
using System;
using System.Collections.Generic;

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

            RegisterGameCommand();
            RegisterInetrpretCommand();
            RegisterMovableAdapter();
            RegisterCommandQueueHandler();
            RegisterStartQueueCommand();
            RegisterHardStopCommand();
            RegisterSoftStopCommand();
            RegisterObjectCollisionCheckCommand();
            RegisterSectorCheckCommand();
        }


        /// <summary>
        /// Стратегия, позволяет генерировать любые смещения координат по оси x для разных окресностей.
        /// </summary>
        /// <param name="shift"></param>
        /// <returns></returns>
        public static Sector[] initializeSectors(int shift = 0)
        {
            var sectors = new Sector[4];
            Sector neighbor = null;

            for (int i = 3; i >= 0; i--)
            {
                List<double> coordinates = new List<double>();
                int coord = i * 10 + 1 + shift;
                int coordTen = coord + 9;
                for (int j = coord; j <= coordTen; j++)
                {
                    coordinates.Add(j);
                }

                var sector = new Sector(i + 1, neighbor, coordinates.ToArray());
                sectors[i] = sector;
                neighbor = sector;
            }

            return sectors;
        }

        private static void RegisterSectorCheckCommand()
        {
            Func<object[], object> f = (args) =>
            {
                return new SectorCheckCommand((IUObject)args[0], (Sector)args[1]);
            };

            Ioc.Resolve<ICommand>(
            "IoC.Register",
            "SectorCheckCommand",
            f);
        }

        private static void RegisterObjectCollisionCheckCommand()
        {
            Func<object[], object> f = (args) =>
            {
                return new ObjectCollisionCheckCommand((IUObject)args[0], (IUObject)args[1]);
            };

            Ioc.Resolve<ICommand>(
            "IoC.Register",
            "ObjectCollisionCheckCommand",
            f);
        }

        private static void RegisterGameCommand()
        {
            Func<object[], object> f = (args) =>
            {
                return new GameCommand((string)args[0], (List<IUObject>)args[1]);
            };

            Ioc.Resolve<ICommand>(
            "IoC.Register",
            "GameCommand",
            f);
        }

        private static void RegisterInetrpretCommand()
        {
            Func<object[], object> f = (args) =>
            {
                return new InterpretCommand((GameCommand)args[0]);
            };

            Ioc.Resolve<ICommand>(
            "IoC.Register",
            "InetrpretCommand",
            f);
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

        private static void RegisterCommandQueueHandler()
        {
            Func<object[], object> f = (args) =>
            {
                return new CommandQueueHandler((IQueue)args[0], args.Length > 1 ? (IQueue)args[1] : (IQueue)null);
            };

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "CommandQueueHandler",
                f
            );
        }

        private static void RegisterStartQueueCommand()
        {
            Func<object[], object> f = (args) =>
            {
                return new StartQueueCommand((CommandQueueHandler)args[0]);
            };

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "StartQueueCommand",
                f
            );
        }

        private static void RegisterHardStopCommand()
        {
            Func<object[], object> f = (args) =>
            {
                return new HardStopCommand((IUObject)args[0]);
            };

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "HardStopCommand",
                f
            );
        }

        private static void RegisterSoftStopCommand()
        {
            Func<object[], object> f = (args) =>
            {
                return new SoftStopCommand((IUObject)args[0]);
            };

            Ioc.Resolve<ICommand>(
                "IoC.Register",
                "SoftStopCommand",
                f
            );
        }
    }
}
