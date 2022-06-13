using Lab3Square;
using System;
using Xunit;

namespace UnitTestSquare
{
    public class SquareTest
    {
        /// <summary>
        /// Test.
        /// </summary>
        [Fact]
        public void GetSquareTest()
        {
            MathLab mathLab = new MathLab();
            
            var result = mathLab.SquareRoot(0.3, 2, 0.5);

            Assert.True(result.Length == 2 && result[0] == -8.2775 && result[1] == 1.6108);
        }
    }
}
