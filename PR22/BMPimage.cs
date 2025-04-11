using System;
using System.Drawing;
using System.Text.RegularExpressions;


namespace upp
{
    public interface IImageSize
    {
        int Width { get; }
        int Height { get; }
        string Name { get; }
    }

    public class RealBMPImage : IImageSize
    {
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }

        public RealBMPImage(string filePath)
        {
            string[] fullName = filePath.Split('\\');
            Name = fullName[fullName.Length - 1];

            using (Image img = Image.FromFile(filePath))
            {
                Width = img.Width;
                Height = img.Height;
            }
        }

        public override string ToString()
        {
            return $"{Name}, {Width}x{Height}";
        }
    }

    public class BMPImageProxy : IImageSize
    {
        private string _filePath;
        private RealBMPImage _realImage;
        private string _sizeRegex = @".+_(\d+)[×x](\d+)\.bmp";

        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public BMPImageProxy(string filePath)
        {
            _filePath = filePath;
            string[] fullName = filePath.Split('\\');
            Name = fullName[fullName.Length - 1];

            ParseFileName();
        }

        private void ParseFileName()
        {
            if (Regex.IsMatch(Name, _sizeRegex))
            {
                char[] separatingStrings = { '_', 'x', '×', '.' };
                string[] parts = Name.Split(separatingStrings);

                Width = int.Parse(parts[1]);
                Height = int.Parse(parts[2]);
            }
            else
            {
                _realImage = new RealBMPImage(_filePath);
                Width = _realImage.Width;
                Height = _realImage.Height;
            }
        }

        public override string ToString()
        {
            return $"{Name}, {Width}x{Height}";
        }
    }
}