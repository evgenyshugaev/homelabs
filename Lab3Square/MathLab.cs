using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3Square
{
    public class MathLab
    {
        public const string  ANotZero = "a не равно 0";

        /// <summary>
        /// Get square.
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <param name="c">c</param>
        /// <param name="e">e</param>
        /// <returns>
        /// D < 0: пустой массив
        /// D = 0: один корень
        /// D > 0: два корня  x1= (-b+sqrt(D))/(2*a), x=2(-b-sqrt(D))/(2*a)
        /// </returns>
        public double[] SquareRoot(double a, double b, double c, double e = 1e-5)
        {
            if (Math.Abs(a) < e)
            {
                throw new ArgumentException(ANotZero);
            }

            if (double.IsNaN(a))
            {
                throw new NotFiniteNumberException("a не число");
            }

            if (double.IsNaN(b))
            {
                throw new NotFiniteNumberException("b не число");
            }

            if (double.IsNaN(c))
            {
                throw new NotFiniteNumberException("c не число");
            }

            if (double.IsInfinity(a))
            {
                throw new NotFiniteNumberException("a бесконечное число");
            }

            if (double.IsInfinity(b))
            {
                throw new NotFiniteNumberException("b бесконечное число");
            }

            if (double.IsInfinity(c))
            {
                throw new NotFiniteNumberException("c бесконечное число");
            }

            var D = b * b - 4 * a * c;
            
            if (D < 0)
            {
                return new double[0];
            }
            if (Math.Abs(D) < e)
            {
                return new double[] { -b / (2 * a), -b / (2 * a) };
            }
            if (D > 0)
            {
                return new double[] { (-b + Math.Sqrt(D)) / (2 * a), (-b - Math.Sqrt(D)) / (2 * a) };
            }

            return new double[0];
        }
    }
}
