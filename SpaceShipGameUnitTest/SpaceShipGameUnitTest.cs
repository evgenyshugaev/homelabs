using SpaceShipGame;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using SpaceShipGame.Exeptions;

namespace SpaceShipGameUnitTest
{
    public class SpaceShipGameUnitTest
    {
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
    }
}