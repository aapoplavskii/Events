using System;
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

        public event Action<string> ImageStartedasync;
        public event Action<string> ImageCompletedasync;

        private BackgroundWorker backgroundWorker;

        public bool completeddownload { get; set; }

        public ImageDownloader()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            completeddownload = true;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            completeddownload = false;

            var _webclient = new WebClient();

            var str = (string[])e.Argument;

            _webclient.DownloadFile(str[0], str[1]);


        }

        public async void Download(string remoteUri, string fileName)
        {

            string[] str = { remoteUri, fileName };

            ImageStarted?.Invoke($"Синхронное скачивание началось! Качаю \"{remoteUri}\" из \"{fileName}\" .......\n\n");

            backgroundWorker.RunWorkerAsync(str);

            await Task.Delay(5000);

            ImageCompleted?.Invoke($"Синхронное скачивание завершено! Успешно скачал \"{remoteUri}\" из \"{fileName}\"");

        }

        public async Task DownloadAsync(string remoteUri, string fileName)
        {

            var _webclient = new WebClient();

            ImageStartedasync?.Invoke($"Асинхронное скачивание началось! Качаю \"{fileName}\" из \"{remoteUri}\" .......\n\n");

            await Task.Delay(10000);
            await Task.Run(() => _webclient.DownloadFile(remoteUri, fileName));

            ImageCompletedasync?.Invoke($"Асинхронное скачивание завершено! Успешно скачал \"{fileName}\" из \"{remoteUri}\"");

        }

    }
}
