using System;

namespace upp
{
    class Program
    {
        public delegate void AddCarDelegate(Car car);
        public delegate void RemoveCarDelegate(int index);
        public delegate void PrintAllCarsDelegate();
        public delegate void PrintCarDelegate(int index);

        static List<Car> carCollection = new List<Car>();

        static void Main(string[] args)
        {
            AddCarDelegate addCar = AddCar;
            RemoveCarDelegate removeCar = RemoveCar;
            PrintAllCarsDelegate printAll = PrintAllCars;
            PrintCarDelegate printOne = PrintCar;

            try
            {
                addCar(new PassengerCar("Toyota", "Corolla", 2020, 180, PassengerCarType.седан, 4, GearBoxType.автоматическая, "красны"));

                addCar(new Truck("Volvo", "FH16", 2019, 120, 20.5, 3, TruckType.фургон));

                addCar(new Autobus("Mercedes", "Sprinter", 2021, 130, 16, 20, AutobusType.городской));

                Console.WriteLine("Все автомобили в коллекции:");
                printAll();

                Console.WriteLine("\nАвтомобиль с индексом 1:");
                printOne(1);

                removeCar(0);

                Console.WriteLine("\nПосле удаления автомобиля с индексом 0:");
                printAll();
            }

            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }

        static void AddCar(Car car)
        {
            carCollection.Add(car);
        }

        static void RemoveCar(int index)
        {
            if (index < 0 || index >= carCollection.Count)
            {
                throw new IndexOutOfRangeException("Неверный индекс автомобиля");
            }

            carCollection.RemoveAt(index);
        }

        static void PrintAllCars()
        {
            if (carCollection.Count == 0)
            {
                Console.WriteLine("Коллекция пуста");
                return;
            }

            for (int i = 0; i < carCollection.Count; i++)
            {
                Console.WriteLine($"{i}. {carCollection[i].GetInfo()}");
            }
        }

        static void PrintCar(int index)
        {
            if (index < 0 || index >= carCollection.Count)
            {
                throw new IndexOutOfRangeException("Неверный индекс автомобиля");
            }

            Console.WriteLine(carCollection[index].GetInfo());
        }
    }
}