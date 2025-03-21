using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upp
{
    class Complex : ICloneable
    {
        private double Real { get; set; }
        private double Imaginary { get; set; }

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public object Clone()
        {
            return new Complex(Real, Imaginary);
        }

        public double Re()
        {
            return Real;
        }

        public double Im()
        {
            return Imaginary;
        }

        public static Complex Addition(Complex z1, Complex z2) 
        { 
            double real = z1.Re() + z2.Re();
            double imaginary = z1.Im() + z2.Im();

            return new Complex(real, imaginary);
        }

        public static Complex Subtraction(Complex z1, Complex z2)
        {
            double real = z1.Re() - z2.Re();
            double imaginary = z1.Im() - z2.Im();

            return new Complex(real, imaginary);
        }
        
        public static Complex Multiplication(Complex z1, Complex z2)
        {
            double real = (z1.Re() * z2.Re()) - (z1.Im() * z2.Im());
            double imaginary = (z1.Re() * z2.Im()) + (z1.Im() * z2.Re());

            return new Complex(real, imaginary);
        }

        public static Complex Division(Complex z1, Complex z2)
        {
            double denominator = z2.Re() * z2.Re() + z2.Im() * z2.Im();
            if (denominator == 0)
            {
                return new Complex(0, 0);
            }
            double real = (z1.Re() * z2.Re() + z1.Im() * z2.Im()) / denominator;
            double imaginary = (z1.Im() * z2.Re() - z1.Re() * z2.Im()) / denominator;

            return new Complex(real, imaginary);
        }

        public override string ToString()
        {
            return ($"{Real} + {Imaginary}i");
        }
    }
}