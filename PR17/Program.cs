using System;
using System.Collections.Generic;
using System.IO;
using upp;

class Program
{
    static void Main()
    {
        Task1();
        Task2();
    }

    static void Task1()
    {
        List<Exam> exams = new List<Exam>
        {
            new Exam("Стоминок Никита", "2024-01-13", 5),
            new Exam("Широкордцивич Сергей", "2023-02-14", 4),
            new Exam("Балтрук Андрей", "2024-01-12", 3)
        };

        string filePath = "exams.txt";

        WriteExamsToFile(exams, filePath);

        List<Exam> loadedExams = ReadExamsFromFile(filePath);

        Console.WriteLine("Прочитанные данные:");
        foreach (var exam in loadedExams)
        {
            Console.WriteLine(exam);
        }
    }

    static void Task2()
    {
        Console.Write("Введите число N: ");
        short N = short.Parse(Console.ReadLine());

        string filePath = "numbers.dat";
        short[] testNumbers = { 10, 15, 20, 25, 30, 17, 23, 12, 18, 5 };

        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            foreach (short num in testNumbers)
                writer.Write(num);
        }

        Console.WriteLine($"Файл {filePath} создан с тестовыми числами:");
        Console.WriteLine(string.Join(", ", testNumbers));

        List<short> numbers = new List<short>();
        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                short num = reader.ReadInt16();
                numbers.Add(num);
            }
        }

        var filteredNumbers = Filter(numbers, N);

        Console.WriteLine("Результат удаления:");
        Console.WriteLine(string.Join(", ", filteredNumbers));

        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            foreach (short num in filteredNumbers)
            {
                writer.Write(num);
            }
        }

        Console.WriteLine($"Числа, кратные {N}, удалены из файла.");
    }

    static void WriteExamsToFile(List<Exam> exams, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var exam in exams)
            {
                writer.WriteLine($"{exam.Name},{exam.Date},{exam.Mark}");
            }
        }

        Console.WriteLine("Данные успешно записаны в файл.");
    }

    static List<Exam> ReadExamsFromFile(string filePath)
    {
        List<Exam> exams = new List<Exam>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    string name = parts[0];
                    string date = parts[1];
                    byte mark = byte.Parse(parts[2]);
                    exams.Add(new Exam(name, date, mark));
                }
            }
        }

        Console.WriteLine("Данные успешно прочитаны из файла.");
        return exams;
    }

    static List<short> Filter(List<short> numbers, short N)
    {
        var result = new List<short>();
        foreach (var num in numbers)
        {
            if (num % N != 0)
            {
                result.Add(num);
            }
        }

        return result;
    }
}