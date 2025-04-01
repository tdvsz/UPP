using System;

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

    public class AccessoriesComparer : IComparer<Accessories>
    {
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
    }

    public class Accessories
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

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