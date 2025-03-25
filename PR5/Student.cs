using System;

namespace upp
{
    class Student
    {
        //private char[] Surname = new char[15];
        //private char[] Name = new char[15];
        //private char[] Patronymic = new char[15];
        //private char[] Specialty = new char[2];
        //private byte Course;
        //private DateTime Birthday;

        private string Surname;
        private string Name;
        private string Patronymic;
        private string Specialty;
        private byte Course;
        private DateTime Birthday;

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