using System;
using System.Text.RegularExpressions;

namespace upp
{
    class Program
    {
        public static void Main(string[] args)
        {
            string path = "emails.txt";
            string pattern = @"^[a-z][a-z\d]{1,9}@[a-z]+\.[a-z]+$";
            List<string> emails = new List<string>();

            Console.WriteLine("Исходные email:");
            
            string line;

            try
            {
                StreamReader sr = new StreamReader(path);
                line = sr.ReadLine();
                while (line != null)
                {
                    if (Regex.IsMatch(line, pattern))
                    {
                        emails.Add(line);
                    }

                    Console.WriteLine(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            Console.WriteLine("\nВалидные email:");
            foreach (string email in emails)
            {
                Console.WriteLine(email);
            }
        }
    }
}
