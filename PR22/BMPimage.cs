using System;
using System.Drawing;
using System.Text.RegularExpressions;


namespace upp
{
    class BMPimage
    {
        private string reg = @".+_[\d]+[×x][\d]+";
        private string Name { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }

        public BMPimage(string name) {
            string[] fullName = name.Split('\\');
            Name = fullName[fullName.Length - 1];

            if (Regex.IsMatch(Name, reg))
            {
                char[] separatingStrings = { '_', 'x', '×', '.' };
                string[] parts = Name.Split(separatingStrings);

                Width = int.Parse(parts[1]);
                Height = int.Parse(parts[2]);
            }

            else
            {
                Image img = Image.FromFile(name);
                Width = img.Width;
                Height = img.Height;
            }
        }

        public override string ToString()
        {
            return $"{Name}, {Width}x{Height}";
        }
    }
}