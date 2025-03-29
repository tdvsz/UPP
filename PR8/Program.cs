using System;
using System.IO;
using System.Text.RegularExpressions;

namespace upp
{
    class Program
    {
        public static void Main(string[] args)
        {
            string path = "data.txt";
            List<string> validLogins = new List<string>();
            List<string> validEmails = new List<string>();

            string loginPattern = @"^[a-zA-Z][a-zA-Z0-9]{1,9}$";
            string emailPattern = @"^[a-z][a-z0-9]+@[a-z]+\.[a-z]+$";

            Console.WriteLine("Исходные данные из файла:");

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);

                        if (Regex.IsMatch(line, loginPattern))
                        {
                            validLogins.Add(line);
                        }
                        else if (Regex.IsMatch(line, emailPattern))
                        {
                            validEmails.Add(line);
                        }
                    }
                }

                Console.WriteLine("\nВалидные логины:");
                foreach (string login in validLogins)
                {
                    Console.WriteLine(login);
                }

                Console.WriteLine("\nВалидные email:");
                foreach (string email in validEmails)
                {
                    Console.WriteLine(email);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
        }
    }
}