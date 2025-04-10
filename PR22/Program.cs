using System;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;


namespace upp
{
    class Program
    {
        public static void Main(string[] args)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("C:/Users/User/Desktop/test");
            List<BMPimage> bMPimages = new List<BMPimage>();
            foreach (var file in directoryInfo.GetFiles())
            {
                bMPimages.Add(new BMPimage(file.FullName));
            }

            foreach (BMPimage image in bMPimages) { 
                Console.WriteLine(image);
            }
        }
    }
}