using System;

namespace upp
{
    class Student
    {
        public string Surname;
        public string Name;
        public string Patronymic;
        public string Specialty;
        public byte Course;
        public DateTime Birthday;

        public Student(string surname, string name, string patronymic, string specialty, byte course, DateTime birthday)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Specialty = specialty;
            Course = course;
            Birthday = birthday;
        }

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic}. Специальность: {Specialty} {Course} курс. Дата рождения: {Birthday:dd.MM.yyyy}";
        }
    }
}