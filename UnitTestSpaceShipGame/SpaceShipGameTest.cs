using System;
using Xunit;
using Moq;
using SpaceShipGame;

namespace UnitTestSpaceShipGame
{
    public class SpaceShipGameTest
    {
        [Fact]
        public void MoveTest()
        {
            var movableAdapterMock = new Mock<IMovable>();

            Vector vector = new Vector(5, 8);
            movableAdapterMock.Setup(s => s.SetPosition(It.IsAny<Vector>())).Verifiable();
            movableAdapterMock.Object.SetPosition(vector);
            movableAdapterMock.Verify(x => x.SetPosition(It.Is<Vector>(v => v.Equals(vector))), Times.AtLeastOnce);

            movableAdapterMock.Setup(s => s.GetPosition()).Returns(vector);
            var getPositionResult = movableAdapterMock.Object.GetPosition();

            Assert.True(getPositionResult.x == vector.x && getPositionResult.y == vector.y);

            vector = new Vector(-7, 3);
            movableAdapterMock.Setup(s => s.GetVelocity()).Returns(vector);
            var getVelocityResult = movableAdapterMock.Object.GetVelocity();

            Assert.True(getVelocityResult.x == vector.x && getVelocityResult.y == vector.y);
        }

        [Fact]
        public void MoveGetPositionFailedTest()
        {
            var movableAdapterMock = new Mock<IMovable>();
            movableAdapterMock.Setup(s => s.GetPosition()).Throws(new Exception());
            movableAdapterMock.Verify();
        }

        [Fact]
        public void MoveSetPositionFailedTest()
        {
            var movableAdapterMock = new Mock<IMovable>();
            movableAdapterMock.Setup(s => s.SetPosition(null)).Throws(new NullReferenceException());
            movableAdapterMock.Verify();
        }

        [Fact]
        public void MoveGetVelocityFailedTest()
        {
            var movableAdapterMock = new Mock<IMovable>();
            movableAdapterMock.Setup(s => s.GetVelocity()).Throws(new Exception());
            movableAdapterMock.Verify();
        }


        [Fact]
        public void RotateTest()
        {
            var rotableAdapterMock = new Mock<IRotable>();

            Vector vector = new Vector(5, 3);
            rotableAdapterMock.Setup(s => s.GetAngularVelocity()).Returns(new Vector(5, 3));
            var getAngularVelocityResult = rotableAdapterMock.Object.GetAngularVelocity();

            Assert.True(getAngularVelocityResult.x == vector.x && getAngularVelocityResult.y == vector.y);
        }
    }
}
