using System;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {

            string remoteUri = "https://effigis.com/wp-content/uploads/2015/02/Iunctus_SPOT5_5m_8bit_RGB_DRA_torngat_mountains_national_park_8bits_1.jpg";

            string fileName = "bigimage.jpg";

            ImageDownloader myimageDownloader = new ImageDownloader();

            myimageDownloader.Download(remoteUri, fileName);

            Console.WriteLine("Для выхода из программы нажмите любую клавишу");
            Console.ReadKey();

        }
    }
}
