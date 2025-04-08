using System;

namespace upp
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<Exam> exams = new List<Exam>()
            {
                new Exam("Стоминок Никита", "10-06-2022", 9),
                new Exam("Широкорадцевич Сергей", "11-06-2022", 8),
                new Exam("Балтрук Александр", "11-06-2022", 6),
                new Exam("Мирский Тимофей", "12-06-2022", 7),
                new Exam("Марчук Ульяна", "11-06-2022", 6),
                new Exam("Вишневецкий Даниил", "10-06-2022", 5),
                new Exam("Ковалевич Александр", "11-06-2022", 7),
            };

            bool isValid = exams.All(e => e.Mark >= 1 && e.Mark <= 10);
            Console.WriteLine($"Все оценки корректны: {isValid}");

            Exam extracted = exams.FirstOrDefault(e => e.Name == "Стоминок Никита");
            if (extracted != null)
            {
                Console.WriteLine($"Извлеченный экзамен: {extracted}");
            }

            Console.WriteLine("\nВсе экзамены:");
            Print(exams);

            Console.WriteLine("\nLINQ");

            // Фильтрация
            Console.WriteLine("\nОценка 6 и ниже");
            var querry1 = from exam in exams
                          where exam.Mark <= 6
                          select exam;
            Print(querry1);

            // Проекция
            Console.WriteLine("\nВывод фамилии и имени");
            var querry2 = from exam in exams
                          select exam.Name;
            Print(querry2);

            // Сортировка
            Console.WriteLine("\nСортировка по фамилии (возрастание)");
            var querry3 = from exam in exams
                          orderby exam.Name ascending
                          select exam;
            Print(querry3);

            Console.WriteLine("\nСортировка по фамилии (убывание)");
            querry3 = from exam in exams
                      orderby exam.Name descending
                      select exam;
            Print(querry3);

            // Агрегатные состояния
            Console.WriteLine($"\nСредняя оценка: {exams.Average(e => e.Mark):F2}");
            Console.WriteLine($"Максимальная оценка: {exams.Max(e => e.Mark)}");
            Console.WriteLine($"Минимальная оценка: {exams.Min(e => e.Mark)}");
            Console.WriteLine($"Сумма оценок: {exams.Sum(e => e.Mark)}");

            // Группировка
            var examDate = exams.GroupBy(e => e.Date);
            Console.WriteLine("\nГруппировка по дате:");
            foreach (var group in examDate)
            {
                Console.WriteLine($"Дата: {group.Key}");
                Print(group);
            }

            // Соединение коллекций
            string[] honorsStudents = { "Стоминок Никита", "Широкорадцевич Сергей", "Ковалевич Александр", "Савицкий Леонид", "Савицкий Денис" };

            var honorsExams = from exam in exams
                              join student in honorsStudents on exam.Name equals student
                              select exam;

            Console.WriteLine("\nОтличники, сдавшие экзамены:");
            Print(honorsExams);

            string filePath = "exams.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Результаты экзаменов:");
                foreach(var exam in exams)
                {
                    writer.WriteLine(exam);
                }
                writer.WriteLine($"Средняя оценка: {exams.Average(e => e.Mark):F2}");
                writer.WriteLine($"Максимальная оценка: {exams.Max(e => e.Mark)}");
                writer.WriteLine($"Минимальная оценка: {exams.Min(e => e.Mark)}");
                writer.WriteLine($"Сумма оценок: {exams.Sum(e => e.Mark)}");
            }
        }

        public static void Print(List<Exam> elements)
        {
            foreach (var element in elements)
            {
                Console.WriteLine(element);
            }
        }

        public static void Print(IEnumerable<Exam> elements)
        {
            foreach (var element in elements)
            {
                Console.WriteLine(element);
            }
        }
        
        public static void Print(IEnumerable<string> elements)
        {
            foreach (var element in elements)
            {
                Console.WriteLine(element);
            }
        }
    }
}