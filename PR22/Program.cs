using System;
using System.IO;
using System.Collections.Generic;

namespace upp
{
    class Program
    {
        public static void Main(string[] args)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("C:/Users/User/Desktop/test");
            List<IImageSize> images = new List<IImageSize>();

            foreach (var file in directoryInfo.GetFiles("*.bmp"))
            {
                images.Add(new BMPImageProxy(file.FullName));
            }

            foreach (IImageSize image in images)
            {
                Console.WriteLine(image);
            }
        }
    }
}