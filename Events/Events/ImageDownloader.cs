using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class ImageDownloader
    {
        public event Action<string> ImageStarted;
        public event Action<string> ImageCompleted;
        public void Download(string remoteUri, string fileName)
        { 
           
            var _webclient = new WebClient();

            ImageStarted?.Invoke("cкачивание началось");

            Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\n\n", fileName, remoteUri);

            _webclient.DownloadFile(remoteUri, fileName);

            Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileName, remoteUri);
            Console.WriteLine("");

            ImageCompleted?.Invoke("cкачивание завершено!");

        }

        //public async Task DownloadAsync(string remoteUri, string fileName)
        //{

        //    var _webclient = new WebClient();

        //    ImageStarted?.Invoke("асинхронное cкачивание началось");

        //    Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\n\n", fileName, remoteUri);

        //    await Task.Run(()=> _webclient.DownloadFile(remoteUri, fileName));

        //    Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileName, remoteUri);
        //    Console.WriteLine("");

        //    ImageCompleted?.Invoke("асинхронное cкачивание завершено!");

        //}

    }
}
