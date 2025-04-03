using System;
using System.Collections;

namespace upp
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public enum SortField
    {
        Manufacturer,
        Model,
        Year
    }

    public enum MemoryType
    {
        DDR,
        DDR2,
        DDR3
    }

    public class AccessoriesCollection : IComparer<Accessories>, IEnumerable<Accessories>
    {
        private List<Accessories> accessories = new List<Accessories>();

        public void Add(Accessories item)
        {
            accessories.Add(item);
        }

        public Accessories this[int index]
        {
            get
            {
                if (index < 0 || index >= accessories.Count)
                    throw new IndexOutOfRangeException("Индекс находится вне границ коллекции");
                return accessories[index];
            }

            set
            {
                if (index < 0 || index >= accessories.Count)
                    throw new IndexOutOfRangeException("Индекс находится вне границ коллекции");
                accessories[index] = value;
            }
        }

        private SortField sortField;
        private SortDirection sortDirection;

        public int Compare(Accessories x, Accessories y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1 * GetDirectionMultiplier();
            if (y == null) return 1 * GetDirectionMultiplier();

            int result = 0;

            switch (sortField)
            {
                case SortField.Manufacturer:
                    result = string.Compare(x.Manufacturer, y.Manufacturer, StringComparison.Ordinal);
                    break;
                case SortField.Model:
                    result = string.Compare(x.Model, y.Model, StringComparison.Ordinal);
                    break;
                case SortField.Year:
                    result = x.Year.CompareTo(y.Year);
                    break;
            }

            return result * GetDirectionMultiplier();
        }

        private int GetDirectionMultiplier()
        {
            return sortDirection == SortDirection.Ascending ? 1 : -1;
        }

        public void Sort(SortField field, SortDirection direction)
        {
            sortField = field;
            sortDirection = direction;
            accessories.Sort(this);
        }

        public void PrintCollection(string filter = "Все")
        {
            if (accessories.Count <= 0)
            {
                return;
            }

            foreach (var element in accessories)
            {
                if (filter == "Все" ||
                    (filter == "Аудио карта" && element is AudioCard) ||
                    (filter == "Модуль памяти" && element is MemoryModul))
                {
                    Console.WriteLine(element.GetInfo());
                }
            }
        }

        public string GetAccessoriesType(int index)
        {
            if (index < 0 || index >= accessories.Count)
                return "Неверный индекс";

            if (accessories[index] is AudioCard)
            {
                return "Аудио карта";
            }
            else
            {
                return "Модуль памяти";
            }
        }

        public int Count => accessories.Count;

        public IEnumerator<Accessories> GetEnumerator()
        {
            return accessories.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Accessories
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public Accessories(string manufacturer, string model, int year)
        {
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
        }

        public virtual string GetInfo()
        {
            return $"{Manufacturer}, {Model}, {Year}";
        }

        public virtual string BoolToStatus(bool param)
        {
            return param ? "да" : "нет";
        }
    }

    public class AudioCard : Accessories
    {
        public string AudioChip { get; set; }
        public int AudioOutputsCount { get; set; }
        public bool Subwoofer { get; set; }

        public AudioCard(string manufacturer, string model, int year, string audioChip, int audioOutputsCount, bool subwoofer) : base(manufacturer, model, year)
        {
            AudioChip = audioChip;
            AudioOutputsCount = audioOutputsCount;
            Subwoofer = subwoofer;
        }

        public override string GetInfo()
        {
            return $"{Manufacturer}, {Model}, {Year}, {AudioChip}, {AudioOutputsCount}, {BoolToStatus(Subwoofer)}";
        }

        public override string BoolToStatus(bool subwoofer)
        {
            return subwoofer ? "сабвуфер присутствует" : "сабвуфер отсутствует";
        }
    }

    public class MemoryModul : Accessories
    {
        public int AmountOfMemory { get; set; }
        public MemoryType MemoryType { get; set; }
        public int MemoryFrequency { get; set; }
        public bool ECC { get; set; }

        public MemoryModul(string manufacturer, string model, int year, int amountOfMemory, MemoryType memoryType, int memoryFrequency, bool ecc) : base(manufacturer, model, year)
        {
            AmountOfMemory = amountOfMemory;
            MemoryType = memoryType;
            MemoryFrequency = memoryFrequency;
            ECC = ecc;
        }

        public override string GetInfo()
        {
            return $"{Manufacturer}, {Model}, {Year}, {AmountOfMemory}, {MemoryType}, {MemoryFrequency}, {BoolToStatus(ECC)}";
        }

        public override string BoolToStatus(bool ECC)
        {
            return ECC ? "поддерживает ECC" : "не поддерживает ECC";
        }
    }
}