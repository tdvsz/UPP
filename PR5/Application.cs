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

                try
                {
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
                catch (Exception e)
                { 
                    Console.WriteLine($"Ошибка: {e.Message}"); 
                }
            }
        }

        public void AddStudent()
        {
            string[] separatingStrings = { " ", ",", ", " };

            Console.WriteLine("Введите информацию о студенте по шаблону: Фамилия Имя Отчество Специальность, Курс, Дата рождения (дд.мм.гггг)");
            string input = Console.ReadLine();

            try
            {
                string[] words = input.Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);

                string surname = words[0];

                ValidateNamePart(surname);

                string name = words[1];

                ValidateNamePart(name);

                string patronymic = words[2];

                ValidateNamePart(patronymic);

                string specialty = words[3];

                if (specialty.Length != 2)
                {
                    throw new Exception("Специальность должна состоять из 2 букв");
                }

                byte course = byte.Parse(words[4]);

                if (course < 1 || course > 5)
                {
                    throw new Exception("Курс должен быть числом от 1 до 5");
                }

                DateTime birthday = DateTime.Parse(words[5]);

                students.Add(new Student(surname, name, patronymic, specialty, course, birthday));
            }

            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
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
            try
            {
                if (students.Count == 0)
                {
                    Console.WriteLine("Список пуст");
                    return;
                }

                Console.Write("Номер элемента: ");
                int index = int.Parse(Console.ReadLine());

                if (index < 0 || index > students.Count)
                {
                    throw new Exception("Неверный номер элемента");
                }

                Student currentStudent = students[index];
                Console.WriteLine(currentStudent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ChangeStudentInfo()
        {
            try
            {
                if (students.Count == 0)
                {
                    Console.WriteLine("Список пуст");
                    return;
                }

                Console.Write("Номер элемента: ");
                int index = int.Parse(Console.ReadLine());

                if (index < 0 || index > students.Count)
                {
                    throw new Exception("Неверный номер элемента");
                }

                students.RemoveAt(index);
                AddStudent();
            }

            catch (Exception e) 
            { 
                Console.WriteLine(e.Message); 
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

        public void ValidateNamePart(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Поле не может быть пустым");
            }

            if (value.Length > 15)
            {
                throw new Exception("Поле не может может быть больше 15 символов");
            }
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