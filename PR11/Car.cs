using System;

namespace upp
{
    public enum PassengerCarType {универсал, хэчбэк, седан, минивен, джип};
    public enum GearBoxType {механическая, автоматическая};
    public enum TruckType {бортовой, самосвал, рефрижератор, фургон};
    public enum AutobusType {городской, рейсовый, туристический};

    abstract class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int MaxSpeed { get; set; }

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

        public abstract string GetInfo();
    }

    sealed class PassengerCar : Car, ICloneable
    {
        public PassengerCarType Type { get; set; }
        public int DoorCount { get; set; }
        public GearBoxType GearBox { get; set; }
        public string Color { get; set; }

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

        public object Clone()
        {
            return new PassengerCar(Brand, Model, Year, MaxSpeed, Type, DoorCount, GearBox, Color);
        }

        public override string GetInfo()
        {
            return $"{Brand}, {Model}, {Year}, {MaxSpeed}, {Type}, {DoorCount}, {GearBox}, {Color}";
        }
    }

    sealed class Truck : Car, ICloneable
    {
        public double Tonnage { get; set; }
        public int Axles { get; set; }
        public TruckType TruckType { get; set; }

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

        public object Clone()
        {
            return new Truck(Brand, Model, Year, MaxSpeed, Tonnage, Axles, TruckType);
        }

        public override string GetInfo()
        {
            return $"{Brand}, {Model}, {Year}, {MaxSpeed}, {Tonnage}, {Axles}, {TruckType}";
        }
    }

    sealed class Autobus : Car, ICloneable
    {
        public int Seats { get; set; }
        public int TotalSeats { get; set; }
        public AutobusType AutobusType { get; set; }

        public Autobus(string brand, string model, int year, int maxSpeed, int seats, int totalSeats, AutobusType autobusType) : base(brand, model, year, maxSpeed)
        {
            Seats = seats;
            TotalSeats = totalSeats;
            AutobusType = autobusType;

            if (seats <= 0 ||  totalSeats <= 0)
            {
                throw new Exception("Количество мест не может быть отрицательным");
            }
        }

        public object Clone()
        {
            return new Autobus(Brand, Model, Year, MaxSpeed, Seats, TotalSeats, AutobusType);
        }

        public override string GetInfo()
        {
            return $"{Brand}, {Model}, {Year}, {MaxSpeed}, {Seats}, {TotalSeats}, {AutobusType}";
        }
    }
}