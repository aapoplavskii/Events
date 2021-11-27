using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using WpfEvents.Implementations;

namespace WpfEvents
{
    public class ImageDownloader : ViewModel
    {
        public event Action<string> ImageStarted;
        public event Action<string> ImageCompleted;
        private BackgroundWorker backgroundWorker;
              
        public ImageDownloader()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
        
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            var str = (string[])e.Result;

            ImageCompleted?.Invoke($"Синхронное скачивание завершено! Успешно скачал \"{str[0]}\" из \"{str[1]}\"");

        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var _webclient = new WebClient();
            
            var str = (string[])e.Argument;


            ImageStarted?.Invoke($"Синхронное скачивание началось! Качаю \"{str[0]}\" из \"{str[1]}\" .......\n\n");

            _webclient.DownloadFile(str[0], str[1]);

            string[] strresult = { str[0], str[1] };

            e.Result = strresult;

        }

        public void Download(string remoteUri, string fileName)
        {
            
            string[] str = { remoteUri, fileName};
                                  
            backgroundWorker.RunWorkerAsync(str);
                       
        }

        public async Task DownloadAsync(string remoteUri, string fileName)
        {

            var _webclient = new WebClient();

            ImageStarted?.Invoke($"Асинхронное скачивание началось! Качаю \"{fileName}\" из \"{remoteUri}\" .......\n\n");

            await Task.Delay(10000);
            await Task.Run(() => _webclient.DownloadFile(remoteUri, fileName));

            ImageCompleted?.Invoke($"Асинхронное скачивание завершено! Успешно скачал \"{fileName}\" из \"{remoteUri}\"");

        }

    }
}
