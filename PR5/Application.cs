using System;

namespace upp
{
    class Application
    {
        static List<Student> students = new List<Student>();

        public void Start()
        {
            while (true) {
                Console.WriteLine(
                "1 - Добавить студента\n" +
                "2 - Вывести список студентов\n" +
                "3 - Вывести одного студента по номеру\n" +
                "4 - Изменить информацию о студенте\n" +
                "+ - Сортировка по возрастанию\n" +
                "= - Сортировка по убыванию");

                Console.Write("Действие: ");
                string choice = Console.ReadLine();

                switch (choice) 
                {
                    case "1": AddStudent(); break;
                    case "2": ShowStudents(); break;
                    case "3": GetCurrentStudent(); break;
                    case "4": ChangeStudentInfo(); break;
                    case "+": SortByASC(); break;
                    case "=": SortByDESC(); break;
                    case "": return;
                    default: Console.WriteLine("Неверная команда"); break;
                }
            }
        }

        public void AddStudent()
        {
            string[] separatingStrings = { " ", ",", ", " };

            Console.WriteLine("Введите информацию о студенте по шаблону: Фамилия Имя Отчество Специальность, Курс, Дата рождения (дд.мм.гггг)");
            string input = Console.ReadLine();

            string[] words = input.Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);

            string surname = words[0];
            string name = words[1];
            string patronymic = words[2];
            string specialty = words[3];
            byte course = byte.Parse(words[4]);
            DateTime birthday = DateTime.Parse(words[5]);

            students.Add(new Student(surname, name, patronymic, specialty, course, birthday));
        }

        public void SortByASC()
        {
            for (int i = 0; i < students.Count - 1; i++)
            {
                for (int j = 0; j < students.Count - i - 1; j++)
                {
                    string surname1 = students[j].Surname;
                    string surname2 = students[j + 1].Surname;

                    if (string.Compare(surname1, surname2) > 0)
                    {
                        Student temp = students[j];
                        students[j] = students[j + 1];
                        students[j + 1] = temp;
                    }
                }
            }
            Console.WriteLine("Список отсортирован по фамилии (возрастание).");

            ShowStudents();
        }

        public void SortByDESC()
        {
            for (int i = 0; i < students.Count - 1; i++)
            {
                for (int j = 0; j < students.Count - i - 1; j++)
                {
                    string surname1 = students[j].Surname;
                    string surname2 = students[j + 1].Surname;

                    if (string.Compare(surname1, surname2) < 0)
                    {
                        Student temp = students[j];
                        students[j] = students[j + 1];
                        students[j + 1] = temp;
                    }
                }
            }
            Console.WriteLine("Список отсортирован по фамилии (убывание).");

            ShowStudents();
        }

        public void GetCurrentStudent()
        {
            if (students.Count == 0) 
            {
                Console.WriteLine("Список пуст");
                return; 
            }

            else
            {
                Console.Write("Номер элемента: ");
                int index = int.Parse(Console.ReadLine());

                Student currentStudent = students[index];
                Console.WriteLine(currentStudent);
            }
        }

        public void ChangeStudentInfo()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            else
            {
                Console.Write("Номер элемента: ");
                int index = int.Parse(Console.ReadLine());

                students.RemoveAt(index);
                AddStudent();
            }
        }

        public int GetCount()
        {
            int i = 0;
            foreach (var student in students)
            {
                i++;
            }

            return i;
        }

        public void ShowStudents()
        {
            if (students.Count == 0) {
                Console.WriteLine("Список пуст");
                return;
            }

            int i = 0;

            Console.WriteLine("----------------------------------");
            foreach (var student in students) {
                Console.WriteLine($"{i}. {student}");
                i++;
            }
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Кол-во элементов в списке: {GetCount()}\n");
        }
    }
}