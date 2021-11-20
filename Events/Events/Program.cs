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
            
            DownloadSync(myimageDownloader, remoteUri, fileName);

            

            Console.WriteLine("Для выхода из программы нажмите любую клавишу");
            Console.ReadKey();

        }

        private static void DownloadSync(ImageDownloader myimageDownloader, string remoteUri, string fileName)
        {
            myimageDownloader.ImageStarted += MyimageDownloader_ImageStarted;
            myimageDownloader.ImageCompleted += MyimageDownloader_ImageCompleted;

            myimageDownloader.Download(remoteUri, fileName);


            myimageDownloader.ImageStarted -= MyimageDownloader_ImageStarted;
            myimageDownloader.ImageCompleted -= MyimageDownloader_ImageCompleted;

            myimageDownloader.Download(remoteUri, fileName);

        }

        private static void MyimageDownloader_ImageCompleted(string status)
        {
            Console.WriteLine($"Статус загрузки - {status}");
            Console.WriteLine("");
        }

        private static void MyimageDownloader_ImageStarted(string status)
        {
            Console.WriteLine($"Статус загрузки - {status}");
            Console.WriteLine("");
        }
    }
}
