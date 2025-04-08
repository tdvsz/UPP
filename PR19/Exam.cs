using System;


namespace upp
{
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

        public override string ToString()
        {
            return $"{Name}. {Date}. {Mark}";
        }
    }
}