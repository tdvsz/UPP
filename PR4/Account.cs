using System;

namespace upp
{
    class Account
    {
        private decimal Amount { get; set; }
        private string Currency {  get; set; }

        private static string[] currencyList = { "USD", "BYN", "EUR" };

        public Account(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Account AddAccount()
        {
            decimal amount;
            string currency;

            while (true)
            {
                Console.Write("Введите сумму счета: ");
                string input = Console.ReadLine();
                try
                {
                    amount = Convert.ToDecimal(input);
                    break;
                }
                catch
                {
                    Console.WriteLine("Ошибка. Введите корректное число.");
                }
            }

            while (true)
            {
                Console.Write("Введите валюту счета: ");
                currency = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(currency) && ValidateCurrency(currency))
                {
                    break;
                }

                Console.WriteLine("Ошибка. Введите корректное название валюты.");
            }

            return new Account(amount, currency); 
        }

        public void GetAmount() 
        {
            Console.WriteLine($"Сумма: {Amount}");
        }

        public void ChangeAmount(decimal newAmount)
        {
            if (newAmount > 0)
            {
                Amount = newAmount;
            }

            else
            {
                Console.WriteLine("Ошибка. Невозможно изменить счет");
            }
        }

        public string GetCurrency()
        {
            return Currency;
        }

        public bool ChangeCurrency(string newCurrency)
        {
            if (Currency == newCurrency)
            {
                Console.WriteLine("Эта валюта уже установлена.");
                return false;
            }

            if (ValidateCurrency(newCurrency))
            {
                Currency = newCurrency;
                return true;
            }

            Console.WriteLine("Ошибка. Неверная валюта");
            return false;
        }

        public static bool ValidateCurrency(string currencyToValidate) 
        {
            for (int i = 0; i < currencyList.Length; i++)
            {
                if (currencyList[i] == currencyToValidate)
                {
                    return true;
                }
            }

            return false;
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
            if (amount <= 0)
            {
                throw new ArgumentException("Сумма должна быть положительной.");
            }

            if (account.Amount < amount)
            {
                throw new InvalidOperationException("Ошибка. На счете недостаточно средств.");
            }

            account.Amount -= amount;
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
            if (account1.Currency != account2.Currency)
            {
                throw new InvalidOperationException("Нельзя сравнивать счета с разными валютами");
            }

            return account1.Amount > account2.Amount;
        }

        public static bool operator <(Account account1, Account account2)
        {
            if (account1.Currency != account2.Currency)
            {
                throw new InvalidOperationException("Нельзя сравнивать счета с разными валютами");
            }

            return account1.Amount < account2.Amount;
        }

        public static Account operator ++(Account account)
        {
            decimal interestRate = 0.05m;
            account.Amount += account.Amount * interestRate;
            return account;
        }
    }
}