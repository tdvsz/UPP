using System;

namespace upp
{
    enum MemoryType
    {
        DDR,
        DDR2,
        DDR3
    }

    class Accessories
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

    class AudioCard : Accessories
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

    class MemoryModul : Accessories
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