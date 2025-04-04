using System;

namespace upp
{
    class Program
    {
        static void Main(string[] args)
        {
            void DisplayMessage(string message) => Console.WriteLine(message);

            Student student = new Student("Широкородцевич", "Сергей");
            student.Notify += DisplayMessage;
            student.AddMark(9);
            student.AddMark(10);
            student.AddMark(8);
            student.Print();

            Console.WriteLine();

            Bachelor bachelor = new Bachelor("Балтрук", "Алексей");
            bachelor.Notify += DisplayMessage;
            bachelor.AddMark(11);
            bachelor.AddMark(10);
            bachelor.AddMark(8);
            bachelor.Print();

            Console.WriteLine();

            Master master = new Master("Стоминок", "Никита");
            master.Notify += DisplayMessage;
            master.AddMark(10);
            master.AddMark(10);
            master.AddMark(9);
            master.Print();
        }
    }
}