using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;

namespace upp
{
    class Account<T>
    {
        public decimal Amount { get; private set; }
        public T Currency {  get; private set; }

        //private static readonly string[] validAlphaCurrencies = { "USD", "BYN", "EUR" };
        //private static readonly int[] validNumericCurrencies = { 840, 933, 978 };

        public Account(decimal amount, T currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Account<T> AddAccount()
        {
            decimal amount;
            T currency;

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
                Console.WriteLine("Валюта:\n" +
                    "1 - Ввести буквенный код\n" +
                    "2 - Ввести цифровой код");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.Write("Буквенный код: ");
                            string strCurrency = Console.ReadLine();
                            currency = (T)(object)strCurrency;
                            return new Account<T>(amount, currency);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case "2":
                        try
                        {
                            Console.Write("Цифровой код: ");
                            int intCurrency = int.Parse(Console.ReadLine());
                            currency = (T)(object)intCurrency;
                            return new Account<T>(amount, currency);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    default:
                        Console.WriteLine("Не верная команда");
                        break;
                }
            }
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

        public bool ChangeCurrency(T newCurrency)
        {
            if (Equals(Currency , newCurrency))
            {
                Console.WriteLine("Эта валюта уже установлена.");
                return false;
            }

            else
            {
                Currency = newCurrency;
                return true;
            }
        }

        //public static bool ValidateCurrency(T currencyToValidate) 
        //{
            
        //}

        public void AccountPrint()
        {
            Console.WriteLine($"Сумма счета: {Amount} {Currency}");
        }

        public static Account<T> operator +(Account<T> account1, Account<T> account2)
        {
            if (!Equals(account1.Currency, account2.Currency))
            {
                throw new InvalidOperationException("Валюты счетов не совпадают");
            }

            return new Account<T>(account1.Amount + account2.Amount, account1.Currency);
        }

        public static Account<T> operator -(Account<T> account, decimal amount)
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

        public static Account<T> operator +(Account<T> account, decimal amount)
        {
            if (amount > 0)
            {
                account.Amount += amount;
            }

            return account;
        }

        public static bool operator >(Account<T> account1, Account<T> account2)
        {
            if (!Equals(account1.Currency, account2.Currency))
            {
                throw new InvalidOperationException("Нельзя сравнивать счета с разными валютами");
            }

            return account1.Amount > account2.Amount;
        }

        public static bool operator <(Account<T> account1, Account<T> account2)
        {
            if (!Equals(account1.Currency, account2.Currency))
            {
                throw new InvalidOperationException("Нельзя сравнивать счета с разными валютами");
            }

            return account1.Amount < account2.Amount;
        }

        public static Account<T> operator ++(Account<T> account)
        {
            decimal interestRate = 0.05m;
            account.Amount += account.Amount * interestRate;
            return account;
        }
    }
}