using System;
using upp;

class Program
{
    public static void Main(string[] args)
    {
        var collection = new StudentCollection();

        collection.Add(new Student("Стоминок", "Никита", "Сергеевич", "ИТ", 3, new DateTime(2006, 11, 09)));
        collection.Add(new Student("Широкородцевич", "Сергей", "Андреевич", "ИТ", 3, new DateTime(2007, 02, 11)));
        collection.Add(new Student("Балтрук", "Алексей", "Дмитриевич", "ИТ", 3, new DateTime(2007, 04, 03)));

        while (true)
        {
            int i = 0;

            Console.WriteLine("\nКоллекция:");
            foreach (Student student in collection)
            {
                Console.WriteLine($"{i}. {student}");
                i++;
            }

            Console.WriteLine(new string('-', 80));
            Console.WriteLine("\n1 - Добавить студента\n" +
                "2 - Удалить студента\n" +
                "3 - Сортировать коллекцию");
            Console.Write("Действие: ");
            
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddStudent(collection); break;
                case "2": RemoveStudent(collection); break;
                case "3": SortCollection(collection); break;
                case "": return;
                default: Console.WriteLine("Неверная команда"); break;
            }
        }
    }

    public static void SortCollection(StudentCollection collection)
    {
        SortField field = SortField.Surname;
        SortDirection direction;

        Console.WriteLine("Введите поле для сортировки\n" +
            "1 - Фамилия\n" +
            "2 - Курс\n" +
            "3 - Специальность");
        string fieldChoice = Console.ReadLine();

        switch (fieldChoice)
        {
            case "1": field = SortField.Surname; break;
            case "2": field = SortField.Course; break;
            case "3": field = SortField.Specialty; break;
        }
        
        Console.WriteLine("Введите направление сортировки\n" +
            "1 - Убывание\n" +
            "2 - Возрастание");
        int directionChoice = int.Parse(Console.ReadLine());

        direction = directionChoice == 1 ? SortDirection.Ascending : SortDirection.Descending;

        collection.Sort(field, direction);
    }

    public static void RemoveStudent(StudentCollection collection)
    {
        Console.WriteLine("Введите индекс элемента:");
        int index = int.Parse(Console.ReadLine());

        collection.RemoveAt(index);
    }

    public static void AddStudent(StudentCollection collection)
    {
        bool q = false;

        while (true)
        {
            try
            {
                Console.WriteLine("Введите имя: ");
                string name = Console.ReadLine();

                Console.WriteLine("Введите фамилию: ");
                string surname = Console.ReadLine();

                Console.WriteLine("Введите отчество: ");
                string patronymic = Console.ReadLine();

                Console.WriteLine("Введите специальность: ");
                string specialty = Console.ReadLine();

                Console.WriteLine("Введите курс: ");
                byte course = byte.Parse(Console.ReadLine());

                Console.WriteLine("Введите дату рождения: ");
                DateTime birthday = DateTime.Parse(Console.ReadLine());

                Student s1 = new Student(surname, name, patronymic, specialty, course, birthday);


                s1.ValidateData(surname, name, patronymic, specialty, course, out q);

                if (q)
                {
                    collection.Add(s1);
                    return;
                }
            }

            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }
        }
    }
}