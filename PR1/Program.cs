using System;

class Program
{
    static void Main(string[] args)
    {
        Account a1 = new Account("a1", 1, 2.4);
        Account a2 = new Account("a2", 2, 5.3);
        Account a3 = new Account("a3", 3, 4.8);

        a1.Print();
        a2.Print();
        a3.Print();
    }
}

class Account
{
    public string Name { get; set; }
    public int AccountNum { get; set; }
    public double Sum { get; set; }

    public Account(string name, int accountNum, double sum)
    {
        Name = name;
        AccountNum = accountNum;
        Sum = sum;
    }

    public void Print()
    {
        Console.WriteLine($"Name: {Name}, account number: {AccountNum}, Sum: {Sum}");
    }
}