using System;

namespace upp
{
    class Application
    {
        static List<Student> students = new List<Student>();

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

        public void ShowStudents()
        {
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
    }
}