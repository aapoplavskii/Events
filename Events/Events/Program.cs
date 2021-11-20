using System;
using System.Threading.Tasks;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {

            string remoteUri = "https://effigis.com/wp-content/uploads/2015/02/Iunctus_SPOT5_5m_8bit_RGB_DRA_torngat_mountains_national_park_8bits_1.jpg";

            string fileName = "bigimage.jpg";

            ImageDownloader myimageDownloader = new ImageDownloader();

            Console.WriteLine("Начинаю синхронное скачивание!");
            Console.WriteLine("");

            DownloadSync(myimageDownloader, remoteUri, fileName);

            Console.WriteLine("Начинаю асинхронное скачивание!");
            Console.WriteLine("");

            var taskloader = DownloadAsync(myimageDownloader, remoteUri, fileName);

            

            while (true)
            {
                Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");
                Console.WriteLine("");

                var keyinfo = Console.ReadKey();

                if (keyinfo.Key == ConsoleKey.A)
                {
                    break;   
                }
                else
                {
                    if (taskloader.IsCompleted)
                    {
                        Console.WriteLine("Асинхронная загрузка завершена");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Асинхронная загрузка не завершена. Дождитесь её окончания.");
                        Console.WriteLine("");
                    }
                }
            }
        }

        private static void DownloadSync(ImageDownloader myimageDownloader, string remoteUri, string fileName)
        {
            myimageDownloader.ImageStarted += MyimageDownloader_ImageStarted;
            myimageDownloader.ImageCompleted += MyimageDownloader_ImageCompleted;

            myimageDownloader.Download(remoteUri, fileName);


            myimageDownloader.ImageStarted -= MyimageDownloader_ImageStarted;
            myimageDownloader.ImageCompleted -= MyimageDownloader_ImageCompleted;

            //myimageDownloader.Download(remoteUri, fileName);  //для проверки отписки

        }


        private static async Task DownloadAsync(ImageDownloader myimageDownloader, string remoteUri, string fileName)
        {
            myimageDownloader.ImageStarted += MyimageDownloader_ImageStarted;
            myimageDownloader.ImageCompleted += MyimageDownloader_ImageCompleted;

            await Task.Delay(10000);
            await Task.Run(()=> myimageDownloader.Download(remoteUri, fileName));
            
            myimageDownloader.ImageStarted -= MyimageDownloader_ImageStarted;
            myimageDownloader.ImageCompleted -= MyimageDownloader_ImageCompleted;
                       
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
