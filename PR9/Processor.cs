using System;

namespace upp
{
    enum ProcCompany
    {
        AMD,
        Intel
    }

    struct Processor
    {
        public ProcCompany Company;
        public string Name;
        public int CoreNumber;
        public double ClockFrequency;
        public decimal Cost;

        public Processor(ProcCompany company, string name, int coreNumber, double clockFrequency, decimal cost)
        {
            Company = company;
            Name = name;
            CoreNumber = coreNumber;
            ClockFrequency = clockFrequency;
            Cost = cost;
        }

        public override string ToString()
        {
            return $"Процессор {Company} {Name}, {CoreNumber} ядер, {ClockFrequency} Ггц. Стоимость {Cost}";
        }
    }
}