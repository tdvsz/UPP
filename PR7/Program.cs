namespace upp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Задание 1");
            Task1();
            Console.WriteLine();

            Console.WriteLine("Задание 2");
            Task2();
            Console.WriteLine();

            Console.WriteLine("Задание 3");
            Task3();
        }

        public static void Task1()
        {
            Console.WriteLine("Введите строку:");
            string str = Console.ReadLine();

            string result = string.Join(", ", str, str, str);
            Console.WriteLine($"{result}. Кол-во символов: {str.Length}");
        }

        public static void Task2()
        {
            Console.WriteLine("Введите текст:");
            string str = Console.ReadLine();

            Console.WriteLine("Введите номер предложения для удаления:");
            int k = int.Parse(Console.ReadLine());

            string[] words = str.Split('.');

            if (k <= 0 || k > words.Length)
            {
                Console.WriteLine(str);
                return;
            }

            List<string> newWords = new List<string>();

            for (int i = 0; i < words.Length; i++)
            {
                if (i != k - 1)
                {
                    newWords.Add(words[i].Trim());
                }
            }

            string result = string.Join(". ", newWords);
            Console.WriteLine(result);
        }

        public static void Task3()
        {
            Console.WriteLine("Введите текст:");
            string text = Console.ReadLine();

            Console.WriteLine("Введите слово:");
            string wordToFind = Console.ReadLine();

            string[] sentences = text.Split('.', StringSplitOptions.RemoveEmptyEntries);
            List<string> newSentences = new List<string>();

            foreach (string sentence in sentences)
            {
                int counter = 0;
                string[] words = sentence.Split(new[] { ' ', ',', ';', ':', '-' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    if (word == wordToFind)
                    {
                        counter++;
                    }
                }

                if (counter <= 2)
                {
                    newSentences.Add(sentence.Trim());
                }
            }

            string result = string.Join(". ", newSentences);
            Console.WriteLine(result);
        }
    }
}