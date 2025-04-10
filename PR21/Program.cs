using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

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

        Parallel.For(0, N, row =>
        {
            RowProcess(row);
        });

        Console.WriteLine("Обработанная матрица:");
        PrintMatrix();
    }

    static void RowProcess(int row)
    {
        double sum = matrix[row].AsParallel().Sum();

        Console.WriteLine($"Поток {Task.CurrentId}. Сумма строки {row + 1}: {sum}");

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

        Console.WriteLine($"Обработано в потоке {Task.CurrentId}\n");
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