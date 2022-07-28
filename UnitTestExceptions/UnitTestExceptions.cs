using Interfaces;
using Lab8Exceptions;
using Moq;
using NUnit.Framework;
using System;

namespace UnitTestExceptions
{
    public class UnitTestExceptions
    {
        [Test]
        public void OneRepeatStrategyCheckSuccess()
        {
            var simpleCommand1 = new SimpleCommand1();

            try
            {
                simpleCommand1.Execute();
            }
            catch (Exception exception)
            {
                Assert.DoesNotThrow(() => ExceptionHandler.Handle(simpleCommand1, exception));
            }
        }

        [Test]
        public void OneRepeatStrategyCheckFailed()
        {
            var simpleCommand = new Mock<ICommand>();

            try
            {
                simpleCommand.Object.Execute();
            }
            catch (Exception exception)
            {
                Assert.Throws<Exception>(() => ExceptionHandler.Handle(simpleCommand.Object, exception));
            }
        }

        [Test]
        public void TwoRepeatStrategyCheckSuccess()
        {
            var simpleCommand2 = new SimpleCommand2();

            try
            {
                simpleCommand2.Execute();
            }
            catch (Exception exception)
            {
                Assert.DoesNotThrow(() => ExceptionHandler.Handle(simpleCommand2, exception));
            }
        }
    }
}