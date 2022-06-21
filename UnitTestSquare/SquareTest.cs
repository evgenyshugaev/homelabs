using Lab3Square;
using System;
using Xunit;

namespace UnitTestSquare
{
    public class SquareTest
    {
        /// <summary>
        /// 3. Ќаписать тест, который провер€ет, что дл€ уравнени€ x^2+1 = 0 корней нет (возвращаетс€ пустой массив)
        /// </summary>
        [Fact]
        public void NoSquareTest()
        {
            MathLab mathLab = new MathLab();

            var result = mathLab.SquareRoot(1, 0, 1);

            Assert.True(result.Length == 0);
        }

        /// <summary>
        /// 5. Ќаписать тест, который провер€ет, что дл€ уравнени€ x^2-1 = 0 есть два корн€ кратности 1 (x1=1, x2=0)
        /// </summary>
        [Fact]
        public void SquareForOneTest()
        {
            MathLab mathLab = new MathLab();

            var result = mathLab.SquareRoot(1, -1, 0);

            Assert.True(result.Length == 2 && result[0] == 1 && result[1] == 0);
        }

        /// <summary>
        /// 7. Ќаписать тест, который провер€ет, что дл€ уравнени€ x^2+2x+1 = 0 есть один корень кратности 2 (x1= x2 = -1).
        /// </summary>
        [Fact]
        public void SquareForTwoTest()
        {
            MathLab mathLab = new MathLab();

            var result = mathLab.SquareRoot(1, 2, 1);

            Assert.True(result.Length == 2 && result[0] == -1 && result[1] == -1);
        }

        /// <summary>
        /// 9. Ќаписать тест, который провер€ет, что коэффициент a не может быть равен 0. ¬ этом случае solve выбрасывает исключение.
        /// </summary>
        [Fact]
        public void SquareForZeroTest()
        {
            MathLab mathLab = new MathLab();

            Assert.Throws<ArgumentException>(() => mathLab.SquareRoot(0, 0, 0));
        }

        /// <summary>
        /// 11. — учетом того, что дискриминант тоже нельз€ сравнивать с 0 через знак равенства, подобрать такие коэффициенты квадратного уравнени€ дл€ случа€ одного корн€ кратности два, 
        /// чтобы дискриминант был отличный от нул€, но меньше заданного эпсилон.
        /// </summary>
        [Fact]
        public void SquareForSmallNumbersTest()
        {
            MathLab mathLab = new MathLab();

            var result = mathLab.SquareRoot(2e-3, 2e-3, 5e-5);

            Assert.True(result.Length == 2 && result[0] == -0.5 && result[1] == -0.5);
        }

        //13 ѕосмотреть какие еще значени€ могут принимать числа типа double, кроме числовых и написать тест с их использованием на все коэффициенты. solve должен выбрасывать исключение.

        [Fact]
        public void SquareForNunTest()
        {
            MathLab mathLab = new MathLab();

            Assert.Throws<NotFiniteNumberException>(() => mathLab.SquareRoot(2e-3, double.NaN, 5e-5));
        }

        [Fact]
        public void SquareForInfinityTest()
        {
            MathLab mathLab = new MathLab();

            Assert.Throws<NotFiniteNumberException>(() => mathLab.SquareRoot(2e-3, double.PositiveInfinity, 5e-5));
        }
    }
}
