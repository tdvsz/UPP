using System;

namespace upp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Задание 1");
            Task1();
            Console.WriteLine();

            Console.WriteLine("Задание 2");
            Task2();
            Console.WriteLine();

            Console.WriteLine("Задание 3");
            Task3();
        }

        public static void Task1()
        {
            Random random = new Random();

            double[] arr = new double[5];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Math.Round(random.NextDouble(), 2);
            }

            Console.WriteLine("Исходный массив:");
            PrintArr(arr);

            int minIndex = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < arr[minIndex])
                {
                    minIndex = i;
                }
            }

            double sum = 0;
            for (int i = minIndex + 1; i < arr.Length; i++)
            {
                sum += arr[i];
            }

            Console.WriteLine($"Индекс минимального элемента: {minIndex}");
            Console.WriteLine($"Сумма элементов после минимума: {sum}");
        }

        public static void Task2()
        {
            Random random = new Random();

            int[] arr = new int[5];
            int sum = 0;

            Console.Write("Введите k: ");
            int K = int.Parse(Console.ReadLine());

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(0, 10);
            }

            Console.WriteLine("Исходный массив:");
            PrintArr(arr);

            while (sum < K)
            {
                if (arr.Length > 1)
                {
                    int tmp = arr[arr.Length - 1];

                    for (int i = arr.Length - 1; i != 0; --i)
                    {
                        arr[i] = arr[i - 1];
                    }

                    arr[0] = tmp;
                    sum += arr[0];
                }
            }

            Console.WriteLine("Преобразованный массив:");
            PrintArr(arr);
            Console.WriteLine($"Сумма перенесенных элементов: {sum}");
        }

        public static void Task3()
        {
            Random random = new Random();

            int[,] arr = new int[5, 5];

            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    arr[i, j] = random.Next(0, 10);
                }
            }

            Console.WriteLine("Исходный массив");

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{arr[i, j]} ");
                }
                Console.WriteLine();
            }

            if (rows > 0 && cols > 0)
            {
                int temp = arr[0, 0];
                arr[0, 0] = arr[rows - 1, 0];
                arr[rows - 1, 0] = temp;
            }

            Console.WriteLine("Преобразованный массив");

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{arr[i, j]} ");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = i + 1; j < cols; j++)
                {
                    int temp = arr[i, j];
                    arr[i, j] = arr[j, i];
                    arr[j, i] = temp;
                }
            }

            Console.WriteLine("Транспонированная матрица");

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{arr[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        public static void PrintArr(double[] arr) 
        {
            string str = string.Join(" ", arr);
            Console.WriteLine(str);
        }

        public static void PrintArr(int[] arr) 
        {
            string str = string.Join(" ", arr);
            Console.WriteLine(str);
        }
    }
}