using System;

namespace upp
{
    class Application
    {
        Stack<Processor> processors = new Stack<Processor>();

        public void Start()
        {
            while (true)
            {
                Console.WriteLine(
                    "1. Добавить процессор\n" +
                    "2. Вывести список процессоров\n" +
                    "3. Вывести среднюю стоимость\n" +
                    "4. Переместить элемент\n" +
                    "5. Удалить процессор с минимальной стоимостью");

                Console.Write("Действие: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": AddProcessor(); break;
                        case "2": ShowProcessors(); break;
                        case "3": AverageCost(); break;
                        case "4": ReplaceProcessor(); break;
                        case "5": RemoveMinCostProcessor(); break;
                        case "": return;
                        default: Console.WriteLine("Неверная команда"); break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void AddProcessor()
        {
            Console.WriteLine("Введите информацию о процессоре по шаблону:" +
                "\nНазвание, Кол-во ядер, Тактовая частота, Стоимость");
            string input = Console.ReadLine();

            try
            {
                string[] words = input.Split(", ");
                ProcCompany company = (ProcCompany)Enum.Parse(typeof(ProcCompany), words[0]);
                string name = words[1];
                int coresNum = int.Parse(words[2]);
                double clockFreq = double.Parse(words[3]);
                decimal cost = decimal.Parse(words[4]);

                processors.Push(new Processor(company, name, coresNum, clockFreq, cost));
            }

            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }

        public void AverageCost()
        {
            double lowerRange = double.Parse(Console.ReadLine());
            double upperRange = double.Parse(Console.ReadLine());

            decimal totalCost = 0;
            int count = 0;

            foreach (Processor processor in processors)
            {
                if (processor.ClockFrequency >= lowerRange && processor.ClockFrequency <= upperRange)
                {
                    totalCost += processor.Cost;
                    count++;
                }
            }

            if (count > 0)
            {
                decimal averageCost = totalCost / count;
                Console.WriteLine($"Средняя стоимость: {averageCost}");
            }
            else
            {
                Console.WriteLine("Процессоры отсутствуют в данном диапазон");
            }
        }

        public void ReplaceProcessor()
        {
            if (processors.Count == 0)
            {
                Console.WriteLine("Спсок пуст");
                return;
            }

            Processor firstEl = processors.Peek();
            Processor minProcessor = firstEl;

            bool found = false;

            foreach (Processor processor in processors)
            {
                if (processor.CoreNumber == 2 && (!found || processor.ClockFrequency < minProcessor.ClockFrequency))
                {
                    minProcessor = processor;
                    found = true;
                }
            }

            if (found)
            {
                processors.Pop();
                Stack<Processor> temp = new Stack<Processor>();

                foreach (Processor processor in processors)
                {
                    if (processor.Equals(minProcessor))
                    {
                        temp.Push(firstEl);
                    }
                    else
                    {
                        temp.Push(processor);
                    }
                }

                processors.Clear();
                processors.Push(minProcessor);

                while (temp.Count > 0)
                {
                    processors.Push(temp.Pop());
                }
            }
            else
            {
                Console.WriteLine("Двухъядерный Процесс с минимальной тактовой частой не найден");
            }
        }

        public void RemoveMinCostProcessor()
        {
            if (processors.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            Processor minProcessor = processors.Peek();

            foreach (Processor processor in processors)
            {
                if (processor.Cost < minProcessor.Cost)
                {
                    minProcessor = processor;
                }
            }

            Stack<Processor> tempStack = new Stack<Processor>();

            foreach (Processor processor in processors)
            {
                if (!processor.Equals(minProcessor))
                {
                    tempStack.Push(processor);
                }
            }

            processors.Clear();
            while (tempStack.Count > 0)
            {
                processors.Push(tempStack.Pop());
            }
        }

        public void ShowProcessors()
        {
            foreach (Processor processor in processors)
            {
                Console.WriteLine(processor);
            }
        }
    }
}