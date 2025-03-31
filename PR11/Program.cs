using System;
using upp;

class Program
{
    public static void Main(string[] args)
    {
        // Легковая машина
        try
        {
            PassengerCar invalidCar = new PassengerCar("Toyota", "Camry", 2020, -100, PassengerCarType.седан, 4, GearBoxType.автоматическая, "черный");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }

        PassengerCar validCar = new PassengerCar("Toyota", "Camry", 2020, 200, PassengerCarType.седан, 4, GearBoxType.автоматическая, "черный");

        Console.WriteLine(validCar.GetInfo());
        Console.WriteLine();

        // Грузовик
        try
        {
            Truck invalidTruck = new Truck("Volvo", "FH16", 2019, 120, 15.5, 4, TruckType.фургон);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }

        Truck validTruck = new Truck("MAN", "TGX", 2021, 110, 20.0, 3, TruckType.самосвал);

        Console.WriteLine(validTruck.GetInfo());
        Console.WriteLine();

        // Автобус
        try
        {
            Autobus invalidBus = new Autobus("Mercedes", "Sprinter", 2022, 100, -10, 20, AutobusType.городской);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }

        Autobus validBus = new Autobus("Volvo", "9700", 2023, 120, 50, 60, AutobusType.туристический);

        Console.WriteLine(validBus.GetInfo());
        Console.WriteLine();

        // Клонирование
        PassengerCar originalCar = new PassengerCar("BMW", "X5", 2022, 250, PassengerCarType.джип, 5, GearBoxType.автоматическая, "синий");

        PassengerCar clonedCar = (PassengerCar)originalCar.Clone();

        Console.WriteLine("Оригинал: " + originalCar.GetInfo());
        Console.WriteLine("Клон:    " + clonedCar.GetInfo());
    }
}