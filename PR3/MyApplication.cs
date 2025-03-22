using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upp
{
    class MyApplication
    {
        private string choice;

        private Complex z1;
        private Complex z2;

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("1 - Введите два числа\n" +
                    "--------------------\n" +
                    "Действия над числами\n" +
                    "+\n" +
                    "-\n" +
                    "*\n" +
                    "/\n" +
                    "--------------------\n" +
                    "2 - Вернуть действительную и мнимую часть числа\n");
                choice = Console.ReadLine();

                switch (choice) {
                    case "1": AddNum(); Complex z3 = (Complex)z1.Clone(); break;
                    case "+": z3 = Complex.Addition(z1, z2); Console.WriteLine($"({z1}) + ({z2}) = {z3}"); break;
                    case "-": z3 = Complex.Subtraction(z1, z2); Console.WriteLine($"({z1}) - ({z2}) = {z3}"); break;
                    case "*": z3 = Complex.Multiplication(z1, z2); Console.WriteLine($"({z1}) * ({z2}) = {z3}"); break;
                    case "/": z3 = Complex.Division(z1, z2); Console.WriteLine($"({z1}) / ({z2}) = {z3}"); break;
                    case "2": ReturnReAndIm(); break;
                    case "": return;
                    default: Console.WriteLine("Неверная команда"); break;
                }
            }
        }

        public void AddNum()
        {
            try
            {
                Console.WriteLine("Введите действительную часть");
                double real1 = double.Parse(Console.ReadLine());
                Console.WriteLine("Введите мнимую часть");
                double imaginary1 = double.Parse(Console.ReadLine());

                Console.WriteLine("Введите действительную часть");
                double real2 = double.Parse(Console.ReadLine());
                Console.WriteLine("Введите мнимую часть");
                double imaginary2 = double.Parse(Console.ReadLine());

                z1 = new Complex(real1, imaginary1);
                z2 = new Complex(real2, imaginary2);

                Console.WriteLine(z1);
                Console.WriteLine(z2);
            }
            catch {
                Console.WriteLine("Ошибка: Введите числа!");
            }
        }

        public void ReturnReAndIm()
        {
            if (z1 != null) {
                Console.WriteLine("1 - Вывести действительную и мнимую часть ПЕРВОГО числа\n" +
                "2 - Вывести действительную и мнимую часть ВТОРОГО числа\n" +
                "0 - Назад");
                string num = Console.ReadLine();

                switch (num)
                {
                    case "1": Console.WriteLine($"Действительная часть - {z1.Re()}, мнимая часть - {z1.Im()}"); break;
                    case "2": Console.WriteLine($"Действительная часть - {z2.Re()}, мнимая часть - {z2.Im()}"); break;
                    case "0": Start(); break;
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Сначала надо ввести числа!");
            }
        }
    }
}