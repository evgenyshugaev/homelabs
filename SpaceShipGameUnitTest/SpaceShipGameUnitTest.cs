using SpaceShipGame;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using SpaceShipGame.Exeptions;
using Interfaces;
using SimpleIoc;
using System;
using SpaceShipGameServer;
using System.Threading.Tasks;
using CommandQueue;
using SecurityToken;
using SpaceShipGameApi;
using System.Linq;

namespace SpaceShipGameUnitTest
{
    public class SpaceShipGameUnitTest
    {
        [Test]
        public void OrderInterpetatorCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();
            Ioc.ClearCurrentScope();

            var user1 = new UObject();
            user1.SetProperty("name", "Евгений");
            var newGame = Ioc.Resolve<GameCommand>("GameCommand", "new_game", new List<IUObject>() { user1 });

            UObject shipOrder = new UObject();
            new SetPropertyCommand(shipOrder, "userName", "Евгений").Execute();
            new SetPropertyCommand(shipOrder, "id", "x-ray").Execute();
            new SetPropertyCommand(shipOrder, "action", "MoveCommand").Execute();
            new SetPropertyCommand(shipOrder, "initialVelocity", (decimal)2).Execute();

            OrderInterpretatorCommand orderInterpretatorCommand = Ioc.Resolve<OrderInterpretatorCommand>("OrderInterpretatorCommand", shipOrder, newGame);
            Assert.DoesNotThrow(() => orderInterpretatorCommand.Execute());
        }

        [Test]
        public void OrderMoveCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();
            Ioc.ClearCurrentScope();

            var user1 = new UObject();
            user1.SetProperty("name", "Евгений");
            var newGame = Ioc.Resolve<GameCommand>("GameCommand", "new_game", new List<IUObject>() { user1 });

            UObject shipOrder = new UObject();
            new SetPropertyCommand(shipOrder, "userName", "Евгений").Execute();
            new SetPropertyCommand(shipOrder, "id", "x-ray").Execute();
            new SetPropertyCommand(shipOrder, "action", "MoveCommand").Execute();
            new SetPropertyCommand(shipOrder, "initialVelocity", (decimal)2).Execute();

            OrderInterpretatorCommand orderInterpretatorCommand = Ioc.Resolve<OrderInterpretatorCommand>("OrderInterpretatorCommand", shipOrder, newGame);
            Assert.DoesNotThrow(() => orderInterpretatorCommand.Execute());
            Assert.IsTrue(orderInterpretatorCommand.Command is MoveCommand);
        }

        [Test]
        public void OrderStopCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();
            Ioc.ClearCurrentScope();

            var user1 = new UObject();
            user1.SetProperty("name", "Евгений");
            var newGame = Ioc.Resolve<GameCommand>("GameCommand", "new_game", new List<IUObject>() { user1 });

            UObject shipOrder = new UObject();
            new SetPropertyCommand(shipOrder, "userName", "Евгений").Execute();
            new SetPropertyCommand(shipOrder, "id", "x-ray").Execute();
            new SetPropertyCommand(shipOrder, "action", "StopCommand").Execute();

            OrderInterpretatorCommand orderInterpretatorCommand = Ioc.Resolve<OrderInterpretatorCommand>("OrderInterpretatorCommand", shipOrder, newGame);
            Assert.DoesNotThrow(() => orderInterpretatorCommand.Execute());
            Assert.IsTrue(orderInterpretatorCommand.Command is StopCommand);
        }

        [Test]
        public void OrderFireCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();
            Ioc.ClearCurrentScope();

            var user1 = new UObject();
            user1.SetProperty("name", "Евгений");
            var newGame = Ioc.Resolve<GameCommand>("GameCommand", "new_game", new List<IUObject>() { user1 });

            UObject shipOrder = new UObject();
            new SetPropertyCommand(shipOrder, "userName", "Евгений").Execute();
            new SetPropertyCommand(shipOrder, "id", "x-ray").Execute();
            new SetPropertyCommand(shipOrder, "action", "FireCommand").Execute();
            new SetPropertyCommand(shipOrder, "fireDirection", "up").Execute();
            new SetPropertyCommand(shipOrder, "fireCoordinate", new Vector(2.2, 3.0)).Execute();


            OrderInterpretatorCommand orderInterpretatorCommand = Ioc.Resolve<OrderInterpretatorCommand>("OrderInterpretatorCommand", shipOrder, newGame);
            Assert.DoesNotThrow(() => orderInterpretatorCommand.Execute());
            Assert.IsTrue(orderInterpretatorCommand.Command is FireCommand);
        }

        [Test]
        public void CanUserOrderSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();
            Ioc.ClearCurrentScope();

            var user1 = new UObject();
            user1.SetProperty("name", "Евгений");
            var newGame = Ioc.Resolve<GameCommand>("GameCommand", "new_game", new List<IUObject>() { user1 });

            UObject shipOrder = new UObject();
            new SetPropertyCommand(shipOrder, "userName", "Евгений").Execute();
            new SetPropertyCommand(shipOrder, "id", "x-ray").Execute();
            new SetPropertyCommand(shipOrder, "action", "StopCommand").Execute();

            OrderInterpretatorCommand orderInterpretatorCommand = Ioc.Resolve<OrderInterpretatorCommand>("OrderInterpretatorCommand", shipOrder, newGame);
            Assert.DoesNotThrow(() => orderInterpretatorCommand.Execute());
        }

        [Test]
        public void CanUserOrderFailed()
        {
            IocResolveStrategy.RegisterDependensies();
            Ioc.ClearCurrentScope();

            var user1 = new UObject();
            user1.SetProperty("name", "Евгений");
            var newGame = Ioc.Resolve<GameCommand>("GameCommand", "new_game", new List<IUObject>() { user1 });

            UObject shipOrder = new UObject();
            new SetPropertyCommand(shipOrder, "userName", "Вольдемар").Execute();
            new SetPropertyCommand(shipOrder, "id", "x-ray").Execute();
            new SetPropertyCommand(shipOrder, "action", "StopCommand").Execute();

            OrderInterpretatorCommand orderInterpretatorCommand = Ioc.Resolve<OrderInterpretatorCommand>("OrderInterpretatorCommand", shipOrder, newGame);
            Assert.Throws<Exception>(() => orderInterpretatorCommand.Execute());
        }


        [Test]
        public void SectorCheckCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();
            Ioc.ClearCurrentScope();

            // Генерация 4-х секторов игрового поля
            var mainSectors = IocResolveStrategy.initializeSectors();

            UObject ship1 = new UObject();
            new SetPropertyCommand(ship1, "id", "ship1").Execute();
            new SetPropertyCommand(ship1, "Positiion", new Vector(1, 5)).Execute();

            UObject ship2 = new UObject();
            new SetPropertyCommand(ship2, "id", "ship2").Execute();
            new SetPropertyCommand(ship2, "Positiion", new Vector(1, 9)).Execute();

            UObject ship3 = new UObject();
            new SetPropertyCommand(ship3, "id", "ship3").Execute();
            new SetPropertyCommand(ship3, "Positiion", new Vector(1, 35)).Execute();

            mainSectors[0].Spaceships.Add(ship2);
            mainSectors[3].Spaceships.Add(ship3);

            SectorCheckCommand sectorCheckCommand = Ioc.Resolve<SectorCheckCommand>("SectorCheckCommand", ship1, mainSectors[0]);
            Assert.DoesNotThrow(() => sectorCheckCommand.Execute());
        }

        [Test]
        public void TwoSectorsCheckCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();
            Ioc.ClearCurrentScope();

            // Генерация 4-х секторов игрового поля
            var mainSectors = IocResolveStrategy.initializeSectors();

            // Генерация 4-х секторов игрового поля со смещеним на 5 координат от центра
            var shiftSectors = IocResolveStrategy.initializeSectors(5);


            UObject ship1 = new UObject();
            new SetPropertyCommand(ship1, "id", "ship1").Execute();
            new SetPropertyCommand(ship1, "Positiion", new Vector(1, 5)).Execute();

            UObject ship2 = new UObject();
            new SetPropertyCommand(ship2, "id", "ship2").Execute();
            new SetPropertyCommand(ship2, "Positiion", new Vector(1, 9)).Execute();

            UObject ship3 = new UObject();
            new SetPropertyCommand(ship3, "id", "ship3").Execute();
            new SetPropertyCommand(ship3, "Positiion", new Vector(1, 35)).Execute();

            mainSectors[0].Spaceships.Add(ship2);
            mainSectors[3].Spaceships.Add(ship3);

            SectorCheckCommand sectorCheckCommand = Ioc.Resolve<SectorCheckCommand>("SectorCheckCommand", ship1, mainSectors[0]);
            Assert.DoesNotThrow(() => sectorCheckCommand.Execute());

            SectorCheckCommand shiftSectorCheckCommand = Ioc.Resolve<SectorCheckCommand>("SectorCheckCommand", ship1, shiftSectors[0]);
            Assert.DoesNotThrow(() => shiftSectorCheckCommand.Execute());
        }

        [Test]
        public void HardStopCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();

            var checkFuelCommandMock = new UObject();
            CheckFuelCommand checkFuelCommand = Ioc.Resolve<CheckFuelCommand>("CheckFuelCommand", checkFuelCommandMock, (decimal)5);

            CommandQueue.CommandQueue commandQueue = new CommandQueue.CommandQueue();
            CommandQueueHandler commandQueueHandler = Ioc.Resolve<CommandQueueHandler>("CommandQueueHandler", commandQueue);

            UObject uobject = new UObject();
            new SetPropertyCommand(uobject, "CommandQueueHandler", commandQueueHandler).Execute();

            commandQueue.Put(checkFuelCommand);
            commandQueue.Put(checkFuelCommand);
            commandQueue.Put(checkFuelCommand);

            StartQueueCommand startQueueCommand = Ioc.Resolve<StartQueueCommand>("StartQueueCommand", commandQueueHandler);
            HardStopCommand hardStopCommand = Ioc.Resolve<HardStopCommand>("HardStopCommand", uobject);

            Assert.DoesNotThrow(() => startQueueCommand.Execute());
            Assert.DoesNotThrow(() => hardStopCommand.Execute());

            // Ждем принудительного завершения потока State = null
            while (!commandQueueHandler.IsStoped)
            {
                continue;
            }

            Assert.AreEqual(commandQueueHandler.IsStoped, true);
        }

        [Test]
        public void MoveToCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();
            Ioc.ClearCurrentScope();

            var checkFuelCommandMock = new UObject();
            CheckFuelCommand checkFuelCommand = Ioc.Resolve<CheckFuelCommand>("CheckFuelCommand", checkFuelCommandMock, (decimal)5);

            CommandQueue.CommandQueue commandQueue = new CommandQueue.CommandQueue();
            CommandQueue.CommandQueue secondaryCommandQueue = new CommandQueue.CommandQueue();
            CommandQueueHandler commandQueueHandler = Ioc.Resolve<CommandQueueHandler>("CommandQueueHandler", commandQueue, secondaryCommandQueue);

            UObject uobject = new UObject();
            new SetPropertyCommand(uobject, "CommandQueueHandler", commandQueueHandler).Execute();

            commandQueue.Put(checkFuelCommand);
            commandQueue.Put(new MoveToCommand());
            commandQueue.Put(checkFuelCommand);
            commandQueue.Put(checkFuelCommand);

            StartQueueCommand startQueueCommand = Ioc.Resolve<StartQueueCommand>("StartQueueCommand", commandQueueHandler);

            Assert.DoesNotThrow(() => startQueueCommand.Execute());

            while (commandQueueHandler.SecondaryQueue.Count() != 2)
            {
                continue;
            }

            Assert.AreEqual(commandQueueHandler.SecondaryQueue.Count(), 2);
        }

        [Test]
        public void RunCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();
            Ioc.ClearCurrentScope();

            var checkFuelCommandMock = new UObject();
            CheckFuelCommand checkFuelCommand = Ioc.Resolve<CheckFuelCommand>("CheckFuelCommand", checkFuelCommandMock, (decimal)5);

            CommandQueue.CommandQueue commandQueue = new CommandQueue.CommandQueue();
            CommandQueue.CommandQueue secondaryCommandQueue = new CommandQueue.CommandQueue();
            CommandQueueHandler commandQueueHandler = Ioc.Resolve<CommandQueueHandler>("CommandQueueHandler", commandQueue, secondaryCommandQueue);

            UObject uobject = new UObject();
            new SetPropertyCommand(uobject, "CommandQueueHandler", commandQueueHandler).Execute();

            commandQueue.Put(new MoveToCommand());
            commandQueue.Put(checkFuelCommand);
            commandQueue.Put(new RunCommand());
            commandQueue.Put(checkFuelCommand);
            commandQueue.Put(checkFuelCommand);
            commandQueue.Put(checkFuelCommand);

            StartQueueCommand startQueueCommand = Ioc.Resolve<StartQueueCommand>("StartQueueCommand", commandQueueHandler);

            Assert.DoesNotThrow(() => startQueueCommand.Execute());

            Assert.AreEqual(commandQueueHandler.IsSimple, true);
        }


        [Test]
        public void GetTokenSuccsess()
        {
            string userName = "Василий";
            string gameId = "game_637991670406364617";
            
            Assert.DoesNotThrow(() => TokenService.GetToken(userName, gameId));
        }

        [Test]
        public void CheckTokenSuccsess()
        {
            string userName = "Василий";
            string gameId = "game_637991670406364617";

            var token = TokenService.GetToken(userName, gameId);

            Assert.IsTrue(TokenValidator.ValidateToken(token));
        }

        [Test]
        public void CheckTokenFailed()
        {
            string userName = "Василий";
            string gameId = "game_637991670406364617";

            var token = TokenService.GetToken(userName, gameId);
            token = token + "111hso";

            Assert.IsFalse(TokenValidator.ValidateToken(token));
        }



        [Test]
        public void StartNewGameCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();

            var user1 = new UObject();
            user1.SetProperty("name", "Евгений");
            var newGame = Ioc.Resolve<GameCommand>("GameCommand", "new_game", new List<IUObject>() { user1 });
            Assert.DoesNotThrow(() => newGame.Execute());
        }

        [Test]
        public void InterpretCommandCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();

            var user1 = new UObject();
            user1.SetProperty("name", "Евгений");
            var newGame = Ioc.Resolve<GameCommand>("GameCommand", "new_game", new List<IUObject>() { user1 });
            InterpretCommand inetrpretCommand = Ioc.Resolve<InterpretCommand>("InetrpretCommand", newGame);

            Assert.DoesNotThrow(() => inetrpretCommand.Execute());
        }


        [Test]
        public void StartQueueCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();

            var checkFuelCommandMock = new Mock<IUObject>();
            CheckFuelCommand checkFuelCommand = Ioc.Resolve<CheckFuelCommand>("CheckFuelCommand", checkFuelCommandMock.Object, (decimal)5);


            CommandQueue.CommandQueue commandQueue = new CommandQueue.CommandQueue();
            commandQueue.Put(checkFuelCommand);
            commandQueue.Put(checkFuelCommand);
            commandQueue.Put(checkFuelCommand);

            CommandQueueHandler commandQueueHandler = Ioc.Resolve<CommandQueueHandler>("CommandQueueHandler", commandQueue);
            StartQueueCommand startQueueCommand = Ioc.Resolve<StartQueueCommand>("StartQueueCommand", commandQueueHandler);

            Assert.DoesNotThrow(() => startQueueCommand.Execute());
        }

        [Test]
        public void SoftStopCommandSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();

            UObject uobject = new UObject();
            new SetPropertyCommand(uobject, "fuel", (decimal)5).Execute();

            Ioc.ClearCurrentScope();

            var checkFuelCommandMock = new Mock<IUObject>();
            CheckFuelCommand checkFuelCommand = Ioc.Resolve<CheckFuelCommand>("CheckFuelCommand", uobject, (decimal)3);

            CommandQueue.CommandQueue commandQueue = new CommandQueue.CommandQueue();
            CommandQueueHandler commandQueueHandler = Ioc.Resolve<CommandQueueHandler>("CommandQueueHandler", commandQueue);
            new SetPropertyCommand(uobject, "CommandQueueHandler", commandQueueHandler).Execute();


            commandQueue.Put(checkFuelCommand);
            commandQueue.Put(checkFuelCommand);
            commandQueue.Put(checkFuelCommand);

            StartQueueCommand startQueueCommand = Ioc.Resolve<StartQueueCommand>("StartQueueCommand", commandQueueHandler);
            SoftStopCommand softStopCommand = Ioc.Resolve<SoftStopCommand>("SoftStopCommand", uobject);

            Assert.DoesNotThrow(() => startQueueCommand.Execute());
            Assert.DoesNotThrow(() => softStopCommand.Execute());
        }


        [Test]
        public void GenerateClassSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();

            UObject uobject = new UObject();
            new SetPropertyCommand(uobject, "Positiion", new Vector(1, 5)).Execute();

            // Пункты задания 1 и 2
            var adapter = Ioc.Resolve<IMovable>("Adapter", typeof(IMovable), uobject);

            Assert.True(adapter != null && adapter is IMovable);

            Vector position = adapter.GetPosition();
            Assert.True(position != null && position is Vector);

            //Пункт задания 3
            Assert.DoesNotThrow(() => adapter.Finish());
        }

        [Test]
        public void RegisterIocSuccsess()
        {
            Assert.DoesNotThrow(() => IocResolveStrategy.RegisterDependensies());
        }

        [Test]
        public void IocResolveSuccsess()
        {
            IocResolveStrategy.RegisterDependensies();

            var checkFuelCommandMock = new Mock<IUObject>();
            checkFuelCommandMock.Setup(o => o.GetProperty("fuel")).Returns((decimal)7);

            CheckFuelCommand cmd = Ioc.Resolve<CheckFuelCommand>("CheckFuelCommand", checkFuelCommandMock.Object, (decimal)5);

            Assert.NotNull(cmd);
        }

        [Test]
        public void IocRegisterScopeSuccsess()
        {
            Assert.DoesNotThrow(() => Ioc.Resolve<ICommand>("Scopes.New", "scope1"));
        }

        [Test]
        public void IocSetCurrentScopeSuccsess()
        {
            Ioc.Resolve<ICommand>("Scopes.New", "scope1");
            Ioc.Resolve<ICommand>("Scopes.New", "scope2");
            Ioc.Resolve<ICommand>("Scopes.Current", "scope1");

            Assert.True(Ioc.CurrentScopeProperty == "scope1");
        }


        [Test]
        public void IocRegisterParallelScopeSuccsess()
        {
            Action action1 = () =>
            {
                Ioc.Resolve<ICommand>("Scopes.New", "scope1");
            };

            Action action2 = () =>
            {
                Ioc.Resolve<ICommand>("Scopes.New", "scope2");
            };


            Parallel.Invoke(action1, action2);
        }


        #region SpaceShip tests

        

        [Test]
        public void CheckFuelCommandSuccsess()
        {
            var checkFuelCommandMock = new Mock<IUObject>();
            checkFuelCommandMock.Setup(o => o.GetProperty("fuel")).Returns((decimal)7);

            var checkFuelCommand = new CheckFuelCommand(checkFuelCommandMock.Object, 5);
            Assert.DoesNotThrow(() => checkFuelCommand.Execute());
        }

        [Test]
        public void CheckFuelCommandFailed()
        {
            var checkFuelCommandMock = new Mock<IUObject>();
            checkFuelCommandMock.Setup(o => o.GetProperty("fuel")).Returns((decimal)7);

            var checkFuelCommand = new CheckFuelCommand(checkFuelCommandMock.Object, 10);

            Assert.Throws<CommandException>(() => checkFuelCommand.Execute());
        }

        [Test]
        [TestCase(7, 5)]
        [TestCase(7.3, 5.2)]
        public void BurnFuelCommandSuccess(decimal fuel, decimal burnedFuel)
        {
            var burnFuelCommandMock = new Mock<IUObject>();
            burnFuelCommandMock.Setup(o => o.GetProperty("fuel")).Returns(fuel);

            var burnFuelCommand = new BurnFuelCommand(burnFuelCommandMock.Object, burnedFuel);

            Assert.DoesNotThrow(() => burnFuelCommand.Execute());
        }

        [Test]
        [TestCase(5, 7)]
        public void BurnFuelCommandFailed(decimal fuel, decimal burnedFuel)
        {
            var burnFuelCommandMock = new Mock<IUObject>();
            burnFuelCommandMock.Setup(o => o.GetProperty("fuel")).Returns(fuel);

            var burnFuelCommand = new BurnFuelCommand(burnFuelCommandMock.Object, burnedFuel);

            Assert.Throws<CommandException>(() => burnFuelCommand.Execute());
        }

        [Test]
        public void MacroCommandSuccess()
        {
            var burnFuelMock = new Mock<IBurnFuel>();
            var checkFuelMock = new Mock<ICheckFuel>();
            var moveMock = new Mock<IMove>();

            var macroCommand = new MacroCommand(
                new List<ICommand>
                {
                    checkFuelMock.Object, moveMock.Object, burnFuelMock.Object,
                });

            Assert.DoesNotThrow(() => macroCommand.Execute());
        }

        [Test]
        public void MacroCommandFailed()
        {
            var burnFuelMock = new Mock<IBurnFuel>();
            burnFuelMock.Setup(b => b.Execute()).Throws(new CommandException());
            var checkFuelMock = new Mock<ICheckFuel>();
            var moveMock = new Mock<IMove>();

            var macroCommand = new MacroCommand(
                new List<ICommand>
                {
                    checkFuelMock.Object, moveMock.Object, burnFuelMock.Object,
                });

            Assert.Throws<CommandException>(() => macroCommand.Execute());
        }

        [Test]
        [TestCase(5, 7)]
        [TestCase(0, 3.9)]
        public void ChangeVelocityCommandSuccess(decimal velocity, decimal newVelocity)
        {
            var changeVelocityCommandMock = new Mock<IUObject>();
            changeVelocityCommandMock.Setup(o => o.GetProperty("velocity")).Returns(velocity);

            var changeVelocityCommand = new ChangeVelocityCommand(changeVelocityCommandMock.Object, newVelocity);

            Assert.DoesNotThrow(() => changeVelocityCommand.Execute());
        }

        [Test]
        [TestCase(7, -3)]
        public void ChangeVelocityCommandFailed(decimal velocity, decimal newVelocity)
        {
            var changeVelocityCommandMock = new Mock<IUObject>();
            changeVelocityCommandMock.Setup(o => o.GetProperty("velocity")).Returns(velocity);

            var changeVelocityCommand = new ChangeVelocityCommand(changeVelocityCommandMock.Object, newVelocity);

            Assert.Throws<CommandException>(() => changeVelocityCommand.Execute());
        }

        [Test]
        [TestCase(7, 10, 45, 8)]
        public void ChangeVelocityRotateCommandSuccess(decimal velocity, decimal newVelocity, int newDirection, int newDirectionsNumber)
        {
            var changeVelocityRotateCommandMock = new Mock<IUObject>();
            changeVelocityRotateCommandMock.Setup(o => o.GetProperty("velocity")).Returns(velocity);
            changeVelocityRotateCommandMock.Setup(o => o.GetProperty("direction")).Returns(0);
            changeVelocityRotateCommandMock.Setup(o => o.GetProperty("directionsNumber")).Returns(8);

            var changeVelocityRotateCommand = new ChangeVelocityRotateCommand(
                changeVelocityRotateCommandMock.Object, 
                newVelocity, 
                newDirection, 
                newDirectionsNumber);

            Assert.DoesNotThrow(() => changeVelocityRotateCommand.Execute());
        }

        [Test]
        [TestCase(10, -3, 45, 8)]
        public void ChangeVelocityRotateCommandFailed(decimal velocity, decimal newVelocity, int newDirection, int newDirectionsNumber)
        {
            var changeVelocityRotateCommandMock = new Mock<IUObject>();
            changeVelocityRotateCommandMock.Setup(o => o.GetProperty("velocity")).Returns(velocity);
            changeVelocityRotateCommandMock.Setup(o => o.GetProperty("direction")).Returns(0);
            changeVelocityRotateCommandMock.Setup(o => o.GetProperty("directionsNumber")).Returns(8);

            var changeVelocityRotateCommand = new ChangeVelocityRotateCommand(
                changeVelocityRotateCommandMock.Object,
                newVelocity,
                newDirection,
                newDirectionsNumber);

            Assert.Throws<CommandException>(() => changeVelocityRotateCommand.Execute());
        }

        #endregion
    }
}