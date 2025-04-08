using System;

namespace ClassLibrary2
{
    public class Class1
    {
        public double CalculateY(double x)
        {
            if (x >= -9 && x <= -6)
            {
                return -Math.Sqrt(9 - Math.Pow(x + 6, 2));
            }
            else if (x >= -6 && x <= -3)
            {
                return x + 3;
            }
            else if (x >= -3 && x <= 0)
            {
                return Math.Sqrt(9 - Math.Pow(x, 2));
            }
            else if (x >= 0 && x <= 3)
            {
                return -x + 3;
            }
            else if (x >= 3 && x <= 9)
            {
                return 0.5 * x - 1.5;
            }
            else
            {
                return double.NaN;
            }
        }
    }
}