using System;
using System.Collections.Generic;
using System.IO;
using upp;

class Program
{
    static void Main()
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
}