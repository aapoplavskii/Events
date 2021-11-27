using System.Threading.Tasks;
using System.Windows.Input;
using WpfEvents.Implementations;

namespace WpfEvents
{
    internal class MainViewModel : ViewModel
    {
        public ICommand StartDownloadCommand { get { return new Command(StartDownload); } }
        public ICommand ViewStatusDownloadCommand { get { return new Command(ViewStatusDownload); } }

        public string Statusdownload { get; set; }

        public string Statusdownloadasync { get; set; }

        public string CurrentStatusdownload { get; set; }

        ImageDownloader myimageDownloader = new ImageDownloader();

        Task taskloader = null;

        public void StartDownload()
        {

            string remoteUri = "https://effigis.com/wp-content/uploads/2015/02/Iunctus_SPOT5_5m_8bit_RGB_DRA_torngat_mountains_national_park_8bits_1.jpg";

            string fileName = "bigimage.jpg";

            myimageDownloader.ImageStarted += MyimageDownloader_ImageStarted;

            myimageDownloader.ImageCompleted += MyimageDownloader_ImageCompleted;

            myimageDownloader.Download(remoteUri, fileName);

            if (myimageDownloader.completeddownload)
            {
                myimageDownloader.ImageStarted -= MyimageDownloader_ImageStarted;
                myimageDownloader.ImageCompleted -= MyimageDownloader_ImageCompleted;
            }
            
            myimageDownloader.ImageStartedasync += MyimageDownloader_ImageStartedasync;

            myimageDownloader.ImageCompletedasync += MyimageDownloader_ImageCompletedasync; ;

            taskloader = myimageDownloader.DownloadAsync(remoteUri, fileName);

            if (taskloader.IsCompleted)
            {
                myimageDownloader.ImageStarted -= MyimageDownloader_ImageStartedasync;
                myimageDownloader.ImageCompleted -= MyimageDownloader_ImageCompletedasync;
            }

        }

        private void MyimageDownloader_ImageCompletedasync(string status)
        {
            Statusdownloadasync = status;
            RaisePropertyChanged("Statusdownloadasync");
        }


        private void MyimageDownloader_ImageStartedasync(string status)
        {
            Statusdownloadasync = status;
            RaisePropertyChanged("Statusdownloadasync");
        }

        private void ViewStatusDownload()
        {
            string syncdownload = string.Empty;
            string asyncdownload = string.Empty;

            if (myimageDownloader.completeddownload)
            {
                syncdownload = "Сихронная загрузка завершена!";
            }
            else
            {
                syncdownload = "Синхронная загрузка не завершена!";
            }

            if (taskloader.IsCompleted)
            {
                asyncdownload = "Асихронная загрузка завершена!";
            }
            else
            { 
                asyncdownload = "Асинхронная загрузка не завершена!";
            }

            CurrentStatusdownload = $"{syncdownload} / {asyncdownload}";

            RaisePropertyChanged("CurrentStatusdownload");
        }

        public void MyimageDownloader_ImageCompleted(string status)
        {
            Statusdownload = status;
            RaisePropertyChanged("Statusdownload");
        }

        public void MyimageDownloader_ImageStarted(string status)
        {
            Statusdownload = status;
            RaisePropertyChanged("Statusdownload");

        }

    }
}
