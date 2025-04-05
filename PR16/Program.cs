using System;

namespace upp
{
    class Program
    {
        static void Main(string[] args)
        {
            Account<string> a1 = Account<string>.AddAccount();
            Account<string> a2 = Account<string>.AddAccount();
            Account<string> a3;

            Console.WriteLine("Вывод счетов");
            a1.AccountPrint();
            a2.AccountPrint();

            Console.WriteLine("\nСлияние счетов с разными валютами");
            try
            {
                a3 = a1 + a2;
                a3.AccountPrint();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nСлияние счетов с одинаковыми валютами");
            string c = a1.Currency;
            a2.ChangeCurrency(c);
            a3 = a1 + a2;
            a3.AccountPrint();

            Console.WriteLine("\nСнятие со счета значения 5");
            try
            {
                Console.Write("До: "); a1.AccountPrint();
                a1 -= 5m;
                Console.Write("После: "); a1.AccountPrint();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nПополнение счета значением 5");
            Console.Write("До: " ); a2.AccountPrint();
            a2 += 5m;
            Console.Write("После: "); a2.AccountPrint();

            Console.WriteLine("\nСравниване счетов с одинаковыми валютами");
            Console.WriteLine(a1 > a2 ? "Первый счет больше" : "Второй счет больше");

            Console.WriteLine("\nСравниване счетов с разными валютами");
            a1.ChangeCurrency("EUR");
            try
            {
                Console.WriteLine(a1 > a2 ? "Первый счет больше" : "Второй счет больше");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nНачисление процентов на текущую сумму на счете");
            Console.Write("До: "); a1.AccountPrint();
            a1++;
            Console.Write("После: "); a1.AccountPrint();
        }
    }
}