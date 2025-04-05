using System;
using System.Threading;

class Program
{
    static double[][] matrix;

    static void Main()
    {
        Console.WriteLine("Введите N:");
        int N = int.Parse(Console.ReadLine());
        Random rand = new Random();

        matrix = new double[N][];
        for (int i = 0; i < N; i++)
        {
            matrix[i] = new double[N];
            for (int j = 0; j < N; j++)
            {
                matrix[i][j] = rand.NextDouble() * 20 - 10;
            }
        }

        Console.WriteLine("Исходная матрица:");
        PrintMatrix();

        Console.WriteLine();

        Thread[] threads = new Thread[N];
        for (int i = 0; i < N; i++)
        {
            int row = i;
            threads[i] = new Thread(() => RowProcces(row));
            threads[i].Name = $"Поток {row + 1}";
            threads[i].Priority = (row == 0) ? ThreadPriority.Highest : ThreadPriority.Normal;
            threads[i].Start();
        }


        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("Обработанная матрица:");
        PrintMatrix();
    }

    static void RowProcces(int row)
    {
        double sum = 0;
        for (int i = 0; i < matrix[row].Length; i++)
        {
            sum += matrix[row][i];
        }

        Console.WriteLine($"{Thread.CurrentThread.Name}. Приоритет: {Thread.CurrentThread.Priority}");
        Console.WriteLine($"Сумма строки { row + 1}: {sum}");

        if (sum >= 0)
        {
            double temp;
            for (int i = 0; i < matrix.Length - 1; i++)
            {
                for (int j = i + 1; j < matrix.Length; j++)
                {
                    if (matrix[row][i] > matrix[row][j])
                    {
                        temp = matrix[row][i];
                        matrix[row][i] = matrix[row][j];
                        matrix[row][j] = temp;
                    }
                }
            }
        }

        else
        {
            double temp;
            for (int i = 0; i < matrix.Length - 1; i++)
            {
                for (int j = i + 1; j < matrix.Length; j++)
                {
                    if (matrix[row][i] < matrix[row][j])
                    {
                        temp = matrix[row][i];
                        matrix[row][i] = matrix[row][j];
                        matrix[row][j] = temp;
                    }
                }
            }
        }

        Console.WriteLine($"Обработано в {Thread.CurrentThread.Name}\n");
    }

    static void PrintMatrix()
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                Console.Write($"{matrix[i][j]:F2}\t");
            }
            Console.WriteLine();
        }
    }
}