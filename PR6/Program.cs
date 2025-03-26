using System;

namespace upp
{
    class Program
    {
        public static void Main(string[] args)
        {
            double[] arr = new double[5];
            Random random = new Random();

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

            Console.WriteLine($"\nИндекс минимального элемента: {minIndex}");
            Console.WriteLine($"Сумма элементов после минимума: {sum}");



            int[] arr2 = new int[5];
            int sum2 = 0;

            Console.Write("\nВведите k: ");
            int K = int.Parse(Console.ReadLine());

            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = random.Next(0, 10);
            }

            Console.WriteLine("Исходный массив:");
            PrintArr(arr2);

            while (sum2 < K)
            {
                if (arr2.Length > 1)
                {
                    var tmp = arr2[arr2.Length - 1];

                    for (var i = arr2.Length - 1; i != 0; --i)
                    {
                        arr2[i] = arr2[i - 1];
                    }

                    arr2[0] = tmp;
                    sum2 += arr2[0];
                }
            }

            Console.WriteLine("Преобразованный массив:");
            PrintArr(arr2);
            Console.WriteLine($"Сумма перенесенных элементов: {sum2}");



            int[,] arr3 = new int[5, 5];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    arr3[i, j] = random.Next(0, 10);
                }
            }

            Console.WriteLine("\nИсходный массив");

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write($"{arr3[i, j]} ");
                }
                Console.WriteLine();
            }

            int rows = arr3.GetLength(0);
            int cols = arr3.GetLength(1);

            if (rows > 0 && cols > 0)
            {
                int temp = arr3[0, 0];
                arr3[0, 0] = arr3[rows - 1, 0];
                arr3[rows - 1, 0] = temp;
            }

            Console.WriteLine("\nПреобразованный массив");

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write($"{arr3[i, j]} ");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    int temp = arr3[i, j];
                    arr3[i, j] = arr3[j, i];
                    arr3[j, i] = temp;
                }
            }

            Console.WriteLine("Транспонированная матрица");

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write($"{arr3[i, j]} ");
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