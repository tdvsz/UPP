using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upp
{
    class Account
    {
        private decimal Amount { get; set; }
        private string Currency {  get; set; }

        public Account(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Account AddAccount()
        {
            Console.Write("Введите сумму счета: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Write("Введите валюту счета: ");
            string currency = Console.ReadLine();

            return new Account(amount, currency); 
        }

        public void GetAmount() 
        {
            Console.WriteLine($"Сумма: {Amount}");
        }
        
        public decimal ChangeAmount(decimal newAmount) {
            if (newAmount > 0)
            {
                return Amount = newAmount;
            }

            else {
                Console.WriteLine("Ошибка. Невозможно изменить счет");
                return 0;
            }
        }

        public string GetCurrency()
        {
            return Currency;
        }

        public string ChangeCurrency(string newCurrency)
        {
            bool validCurrency = false;
            string[] currencyList = { "USD", "BYN", "EUR" };

            for (int i = 0; i < currencyList.Length; i++) { 
                if (currencyList[i] == newCurrency)
                {
                    validCurrency = true;
                    break;
                }
            }

            if (validCurrency)
            {
                return Currency = newCurrency;
            }

            else
            {
                Console.WriteLine("Ошибка. Неверная валюта");
                return null;
            }
        }

        public void AccountPrint()
        {
            Console.WriteLine($"Сумма счета: {Amount} {Currency}");
        }

        public static Account operator +(Account account1, Account account2)
        {
            if (account1.Currency != account2.Currency)
            {
                throw new InvalidOperationException("Валюты счетов не совпадают");
            }

            return new Account(account1.Amount + account2.Amount, account1.Currency);
        }

        public static Account operator -(Account account, decimal amount)
        {
            if (amount > 0 && account.Amount >= amount)
            {
                account.Amount -= amount;
            }
            else
            {
                Console.WriteLine("Ошибка. На счете недостаточно средств.");
            }

            return account;
        }

        public static Account operator +(Account account, decimal amount)
        {
            if (amount > 0)
            {
                account.Amount += amount;
            }

            return account;
        }

        public static bool operator >(Account account1, Account account2)
        {
            if (account1.Currency == account2.Currency)
            {
                return account1.Amount > account2.Amount;
            }
            else
            {
                throw new InvalidOperationException("Валюты счетов не совпадают");
            }
        }

        public static bool operator <(Account account1, Account account2)
        {
            if (account1.Currency == account2.Currency)
            {
                return account1.Amount < account2.Amount;
            }
            else
            {
                throw new InvalidOperationException("Валюты счетов не совпадают");
            }
        }

        public static Account operator ++(Account account)
        {
            decimal interestRate = 0.05m;
            account.Amount += account.Amount * interestRate;
            return account;
        }
    }
}