using System;

namespace upp
{
    /// <summary>
    /// Типы легковых автомобилей
    /// </summary>
    public enum PassengerCarType { универсал, хэчбэк, седан, минивен, джип };

    /// <summary>
    /// Типы коробки передач
    /// </summary>
    public enum GearBoxType { механическая, автоматическая };

    /// <summary>
    /// Типы грузовиков
    /// </summary>
    public enum TruckType { бортовой, самосвал, рефрижератор, фургон };

    /// <summary>
    /// Типы автобусов
    /// </summary>
    public enum AutobusType { городской, рейсовый, туристический };

    /// <summary>
    /// Абстрактный базовый класс для всех автомобилей
    /// </summary>
    abstract class Car
    {
        /// <summary>
        /// Марка автомобиля
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Модель автомобиля
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Год выпуска автомобиля
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Максимальная скорость автомобиля (км/ч)
        /// </summary>
        public int MaxSpeed { get; set; }

        /// <summary>
        /// Конструктор базового класса Car
        /// </summary>
        /// <param name="brand">Марка автомобиля</param>
        /// <param name="model">Модель автомобиля</param>
        /// <param name="year">Год выпуска</param>
        /// <param name="maxSpeed">Максимальная скорость (км/ч)</param>
        /// <exception cref="Exception">Выбрасывается при отрицательной скорости</exception>
        public Car(string brand, string model, int year, int maxSpeed)
        {
            Brand = brand;
            Model = model;
            Year = year;
            MaxSpeed = maxSpeed;

            if (maxSpeed <= 0)
            {
                throw new Exception("Скорость не может быть отрицательной");
            }
        }

        /// <summary>
        /// Абстрактный метод для получения информации об автомобиле
        /// </summary>
        /// <returns>Строка с информацией об автомобиле</returns>
        public abstract string GetInfo();
    }

    /// <summary>
    /// Класс, представляющий легковой автомобиль
    /// </summary>
    sealed class PassengerCar : Car, ICloneable
    {
        /// <summary>
        /// Тип кузова легкового автомобиля
        /// </summary>
        public PassengerCarType Type { get; set; }

        /// <summary>
        /// Количество дверей
        /// </summary>
        public int DoorCount { get; set; }

        /// <summary>
        /// Тип коробки передач
        /// </summary>
        public GearBoxType GearBox { get; set; }

        /// <summary>
        /// Цвет автомобиля
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Конструктор класса PassengerCar
        /// </summary>
        /// <param name="brand">Марка автомобиля</param>
        /// <param name="model">Модель автомобиля</param>
        /// <param name="year">Год выпуска</param>
        /// <param name="maxSpeed">Максимальная скорость (км/ч)</param>
        /// <param name="type">Тип кузова</param>
        /// <param name="doorCount">Количество дверей</param>
        /// <param name="gearBox">Тип коробки передач</param>
        /// <param name="color">Цвет автомобиля</param>
        /// <exception cref="Exception">Выбрасывается при недопустимом количестве дверей</exception>
        public PassengerCar(string brand, string model, int year, int maxSpeed, PassengerCarType type, int doorCount, GearBoxType gearBox, string color) : base(brand, model, year, maxSpeed)
        {
            Type = type;
            DoorCount = doorCount;
            GearBox = gearBox;
            Color = color;

            if (doorCount <= 0)
            {
                throw new Exception("Количество дверей не может быть отрицательным");
            }
        }

        /// <summary>
        /// Создает копию объекта PassengerCar
        /// </summary>
        /// <returns>Новый объект PassengerCar с такими же свойствами</returns>
        public object Clone()
        {
            return new PassengerCar(Brand, Model, Year, MaxSpeed, Type, DoorCount, GearBox, Color);
        }

        /// <summary>
        /// Возвращает строку с информацией о легковом автомобиле
        /// </summary>
        /// <returns>Строка с данными автомобиля</returns>
        public override string GetInfo()
        {
            return $"{Brand}, {Model}, {Year}, {MaxSpeed}, {Type}, {DoorCount}, {GearBox}, {Color}";
        }
    }

    /// <summary>
    /// Класс, представляющий грузовик
    /// </summary>
    sealed class Truck : Car, ICloneable
    {
        /// <summary>
        /// Грузоподъемность в тоннах
        /// </summary>
        public double Tonnage { get; set; }

        /// <summary>
        /// Количество осей (2 или 3)
        /// </summary>
        public int Axles { get; set; }

        /// <summary>
        /// Тип грузовика
        /// </summary>
        public TruckType TruckType { get; set; }

        /// <summary>
        /// Конструктор класса Truck
        /// </summary>
        /// <param name="brand">Марка грузовика</param>
        /// <param name="model">Модель грузовика</param>
        /// <param name="year">Год выпуска</param>
        /// <param name="maxSpeed">Максимальная скорость (км/ч)</param>
        /// <param name="tonnage">Грузоподъемность (тонны)</param>
        /// <param name="axles">Количество осей</param>
        /// <param name="truckType">Тип грузовика</param>
        /// <exception cref="Exception">Выбрасывается при недопустимой грузоподъемности или количестве осей</exception>
        public Truck(string brand, string model, int year, int maxSpeed, double tonnage, int axles, TruckType truckType) : base(brand, model, year, maxSpeed)
        {
            Tonnage = tonnage;
            Axles = axles;
            TruckType = truckType;

            if (tonnage <= 0)
            {
                throw new Exception("Грузоподъемность не может быть отрицательной");
            }

            if (axles < 2 || axles > 3)
            {
                throw new Exception("Количество осей может быть только от 2 до 3");
            }
        }

        /// <summary>
        /// Создает копию объекта Truck
        /// </summary>
        /// <returns>Новый объект Truck с такими же свойствами</returns>
        public object Clone()
        {
            return new Truck(Brand, Model, Year, MaxSpeed, Tonnage, Axles, TruckType);
        }

        /// <summary>
        /// Возвращает строку с информацией о грузовике
        /// </summary>
        /// <returns>Строка с данными грузовика</returns>
        public override string GetInfo()
        {
            return $"{Brand}, {Model}, {Year}, {MaxSpeed}, {Tonnage}, {Axles}, {TruckType}";
        }
    }

    /// <summary>
    /// Класс, представляющий автобус
    /// </summary>
    sealed class Autobus : Car, ICloneable
    {
        /// <summary>
        /// Количество сидячих мест
        /// </summary>
        public int Seats { get; set; }

        /// <summary>
        /// Общее количество мест
        /// </summary>
        public int TotalSeats { get; set; }

        /// <summary>
        /// Тип автобуса
        /// </summary>
        public AutobusType AutobusType { get; set; }

        /// <summary>
        /// Конструктор класса Autobus
        /// </summary>
        /// <param name="brand">Марка автобуса</param>
        /// <param name="model">Модель автобуса</param>
        /// <param name="year">Год выпуска</param>
        /// <param name="maxSpeed">Максимальная скорость (км/ч)</param>
        /// <param name="seats">Количество сидячих мест</param>
        /// <param name="totalSeats">Общее количество мест</param>
        /// <param name="autobusType">Тип автобуса</param>
        /// <exception cref="Exception">Выбрасывается при недопустимом количестве мест</exception>
        public Autobus(string brand, string model, int year, int maxSpeed, int seats, int totalSeats, AutobusType autobusType) : base(brand, model, year, maxSpeed)
        {
            Seats = seats;
            TotalSeats = totalSeats;
            AutobusType = autobusType;

            if (seats <= 0 || totalSeats <= 0)
            {
                throw new Exception("Количество мест не может быть отрицательным");
            }
        }

        /// <summary>
        /// Создает копию объекта Autobus
        /// </summary>
        /// <returns>Новый объект Autobus с такими же свойствами</returns>
        public object Clone()
        {
            return new Autobus(Brand, Model, Year, MaxSpeed, Seats, TotalSeats, AutobusType);
        }

        /// <summary>
        /// Возвращает строку с информацией об автобусе
        /// </summary>
        /// <returns>Строка с данными автобуса</returns>
        public override string GetInfo()
        {
            return $"{Brand}, {Model}, {Year}, {MaxSpeed}, {Seats}, {TotalSeats}, {AutobusType}";
        }
    }
}