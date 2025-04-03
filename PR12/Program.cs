using System;
using System.Reflection;

namespace upp
{
    class Program
    {
        public static void Main(string[] args)
        {
            var collection = new AccessoriesCollection();

            collection.Add(new AudioCard(
                "Creative",
                "Sound Blaster Z",
                2013,
                "Sound Core3D",
                5,
                true
            ));

            collection.Add(new AudioCard(
                "ASUS",
                "Xonar AE",
                2018,
                "C-Media CMI8788",
                7,
                false
            ));

            collection.Add(new MemoryModul(
                "Kingston",
                "HyperX Fury",
                2015,
                8,
                MemoryType.DDR3,
                1866,
                false
            ));

            collection.Add(new MemoryModul(
                "Corsair",
                "Vengeance LPX",
                2020,
                16,
                MemoryType.DDR3,
                3200,
                true
            ));

            collection.Add(new AudioCard(
                "EVGA",
                "Nu Audio",
                2019,
                "Realtek ALC1220",
                5,
                true
            ));

            while (true)
            {
                Console.WriteLine("1 - Добавить аудиокарту\n" +
                    "2 - Добавить модуль памяти\n" +
                    "3 - Вывод коллекции\n" +
                    "4 - Сортировать\n" +
                    "5 - Тип комплектующего");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddAudioCard(collection); break;
                    case "2": AddMemoryModul(collection); break;
                    case "3": Print(collection); break;
                    case "4": SortCollection(collection); break;
                    case "5": GetAccessoriesType(collection); break;
                }
            }
        }

        public static void GetAccessoriesType(AccessoriesCollection collection)
        {
            if (collection.Count == 0)
            {
                Console.WriteLine("Коллекция пуста");
                return;
            }

            Console.WriteLine("Введите индекс:");
            int index = int.Parse(Console.ReadLine());
            Console.WriteLine(collection.GetAccessoriesType(index));
        }

        public static void SortCollection(AccessoriesCollection collection)
        {
            SortField field = SortField.Manufacturer;
            SortDirection direction;

            Console.WriteLine("Введите поле для сортировки\n" +
                "1 - Производитель\n" +
                "2 - Модель\n" +
                "3 - Год");
            string fieldChoice = Console.ReadLine();

            switch (fieldChoice)
            {
                case "1": field = SortField.Manufacturer; break;
                case "2": field = SortField.Model; break;
                case "3": field = SortField.Year; break;
            }

            Console.WriteLine("Введите направление сортировки\n" +
                "1 - Убывание\n" +
                "2 - Возрастание");
            int directionChoice = int.Parse(Console.ReadLine());

            direction = directionChoice == 1 ? SortDirection.Ascending : SortDirection.Descending;

            collection.Sort(field, direction);
        }

        public static void Print(AccessoriesCollection collection)
        {
            Console.WriteLine("Вывод:\n" +
                "1 - Все\n" +
                "2 - Аудио карта\n" +
                "3 - Модуль памяти");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Коллекция");
                    collection.PrintCollection(); break;
                case "2":
                    Console.WriteLine("Аудио карты");
                    collection.PrintCollection("Аудио карта"); break;
                case "3":
                    Console.WriteLine("Модули памяти");
                    collection.PrintCollection("Модуль памяти"); break;
            }
        }

        private static void AddMemoryModul(AccessoriesCollection collection)
        {
            Console.WriteLine("Введите производителя");
            string manufacturer = Console.ReadLine();

            Console.WriteLine("Введите модель");
            string model = Console.ReadLine();

            Console.WriteLine("Введите год");
            int year = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите кол-во памяти");
            int amountOfMemory = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите тип памяти (DDR, DDR2, DDR3)");
            MemoryType memoryType = (MemoryType)Enum.Parse(typeof(MemoryType), Console.ReadLine());

            Console.WriteLine("Введите частота памяти");
            int memoryFrequency = int.Parse(Console.ReadLine());

            Console.WriteLine("ECC: да, нет");
            string input = Console.ReadLine();

            bool ecc = input == "да" ? true : false;

            collection.Add(new MemoryModul(manufacturer, model, year, amountOfMemory, memoryType, memoryFrequency, ecc));
        }

        public static void AddAudioCard(AccessoriesCollection collection)
        {
            Console.WriteLine("Введите производителя");
            string manufacturer = Console.ReadLine();
            
            Console.WriteLine("Введите модель");
            string model = Console.ReadLine();
            
            Console.WriteLine("Введите год");
            int year = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите аудио чип");
            string audioChip = Console.ReadLine();

            Console.WriteLine("Введите введите кол-во колонок");
            int audioOutputsCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Сабвуфер: да, нет");
            string input = Console.ReadLine();

            bool subwoofer = input == "да" ? true : false;

            collection.Add(new AudioCard(manufacturer, model, year, audioChip, audioOutputsCount, subwoofer));
        }
    }
}