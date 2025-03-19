using System;

class Program
{
    static List<Exam> examItem = new List<Exam>();
    static int currentIndex = -1;

    static void Main(string[] args)
    {
        MyApplication app = new MyApplication();
        app.Start();
    }

    public static void AddExam()
    {
        Console.Write("Имя студента: ");
        string name = Console.ReadLine();

        Console.Write("Дата экзамена: ");
        string date = Console.ReadLine();

        Console.Write("Оценка за экзамен: ");
        byte mark = byte.Parse(Console.ReadLine());

        examItem.Add(new Exam(name, date, mark));
        if (currentIndex == -1)
        {
            currentIndex = 0;
        }
    }

    public static void RemoveExam()
    {
        if (currentIndex == -1 || examItem.Count == 0)
        {
            Console.WriteLine("Пустой проект");
            return;
        }

        examItem.RemoveAt(currentIndex);

        if (examItem.Count == 0)
        {
            currentIndex = -1;
        }
        else if (currentIndex >= examItem.Count)
        {
            currentIndex = examItem.Count - 1;
        }
    }

    public static void EditExam()
    {
        RemoveExam();
        AddExam();
    }

    public static void SortByASC()
    {
        for (int i = 0; i < examItem.Count - 1; i++)
        {
            for (int j = 0; j < examItem.Count - i - 1; j++)
            {
                string name1 = examItem[j].Name;
                string name2 = examItem[j + 1].Name;

                if (string.Compare(name1, name2) > 0)
                {
                    Exam temp = examItem[j];
                    examItem[j] = examItem[j + 1];
                    examItem[j + 1] = temp;
                }
            }
        }
        Console.WriteLine("Список отсортирован по фамилии (возрастание).");

        ShowExams();
    }

    public static void SortByDESC()
    {
        for (int i = 0; i < examItem.Count - 1; i++)
        {
            for (int j = 0; j < examItem.Count - i - 1; j++)
            {
                string name1 = examItem[j].Name;
                string name2 = examItem[j + 1].Name;

                if (string.Compare(name1, name2) < 0)
                {
                    Exam temp = examItem[j];
                    examItem[j] = examItem[j + 1];
                    examItem[j + 1] = temp;
                }
            }
        }
        Console.WriteLine("Список отсортирован по фамилии (убывание).");

        ShowExams();
    }

    public static void MoveToStart()
    {
        if (examItem.Count > 0)
        {
            currentIndex = 0;
        }
    }

    public static void MoveToEnd()
    {
        if (examItem.Count > 0)
        {
            currentIndex = examItem.Count - 1;
        }
    }

    public static void MoveForward()
    {
        if (currentIndex < examItem.Count - 1)
        {
            currentIndex++;
        }
    }

    public static void MoveBackward()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
    }

    public static void  GetCurrentExam()
    {
        if (currentIndex == -1 || examItem.Count == 0)
        {
            Console.WriteLine("Список пуст");
        }
        else
        {
            Exam currentExam = examItem[currentIndex];
            currentExam.Print();
        }
    }

    public static void ShowExams()
    {
        foreach (var exam in examItem) {
            exam.Print();
        }
    }
}

class MyApplication
{
    public int choice;

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("1. Добавить экзамен\n" +
                "2. Удалить экзамен\n" +
                "3. Редактировать экзамен\n" +
                "4. Сортировать список экзаменов\n" +
                "5. Вывести текущий экзамен в списке\n" +
                "[. Переместиться в начало списка\n" +
                "]. Переместиться в конец списка\n" +
                "-. Переместиться назад\n" +
                "=. Переместиться вперед\n" +
                "6. Вывести список экзаменов");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": Program.AddExam(); break;
                case "2": Program.RemoveExam(); break;
                case "3": Program.EditExam(); break;
                case "4": Sort(); break;
                case "5": Program.GetCurrentExam(); break;
                case "[": Program.MoveToStart(); break;
                case "]": Program.MoveToEnd(); break;
                case "-": Program.MoveBackward(); break;
                case "=": Program.MoveForward(); break;
                case "6": Program.ShowExams(); break;
                case "": return;
                default: Console.WriteLine("Неверный ввод"); break;
            }
        }
    }

    public void Sort()
    {
        Console.WriteLine("1. Фамилия студента. Возрастание\n" +
                "2. Фамилия студента. Убывание\n" +
                "0. Назад");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": Program.SortByASC(); break;
            case "2": Program.SortByDESC(); break;
            case "0": Start(); break;
            default: Console.WriteLine("Неверный ввод"); break;
        }
    }
}

class Exam
{
    public string Name { get; set; }
    public string Date { get; set; }
    public byte Mark { get; set; }

    public Exam(string name, string date, byte mark)
    {
        Name = name;
        Date = date;
        Mark = mark;
    }

    public void Print()
    {
        Console.WriteLine($"Имя студента: {Name}, дата экзамена: {Date}, оценка за экзамен: {Mark}");
    }
}