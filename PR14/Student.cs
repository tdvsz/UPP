using System;

namespace upp
{
    public interface IMarks
    {
        void AddMark(int mark);
    }

    public interface IStudentInfo
    {
        void Print();
    }

    public class Student : IMarks, IStudentInfo
    {
        public delegate void StudentHandler(string message);
        public event StudentHandler? Notify;

        private List<int> Marks = new List<int>();
        public string Name { get; set; }
        public string Surname { get; set; }

        public Student(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public void AddMark(int mark)
        {
            if (mark < 1 || mark > 10)
            {
                Notify?.Invoke($"Недопустимая оценка");
                return;
            }

            Marks.Add(mark);
            Notify?.Invoke($"Оценка добавлена {mark}");
        }

        public double GetAverageMark()
        {
            return Marks.Count == 0 ? 0 : Marks.Average();
        }

        public void Print()
        {
            Console.WriteLine($"{Name} {Surname}. Средний балл: {GetAverageMark():F1}");
        }
    }

    public class Bachelor : Student
    {
        public Bachelor(string name, string surname) : base(name, surname)
        {

        }

        public void Print()
        {
            Console.WriteLine($"{Name} {Surname}. Средний балл: {GetAverageMark():F1}");
        }
    }

    public class Master : Bachelor
    {
        public Master(string name, string surname) : base(name, surname)
        {

        }

        public void Print()
        {
            Console.WriteLine($"{Name} {Surname}. Средний балл: {GetAverageMark():F1}");
        }
    }
}