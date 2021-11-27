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

        public void StartDownload()
        {

            string remoteUri = "https://effigis.com/wp-content/uploads/2015/02/Iunctus_SPOT5_5m_8bit_RGB_DRA_torngat_mountains_national_park_8bits_1.jpg";

            string fileName = "bigimage.jpg";

            ImageDownloader myimageDownloader = new ImageDownloader();

            myimageDownloader.ImageStarted += MyimageDownloader_ImageStarted;
           
            myimageDownloader.ImageCompleted += MyimageDownloader_ImageCompleted;



            myimageDownloader.Download(remoteUri, fileName);



            //var taskloader = myimageDownloader.DownloadAsync(remoteUri, fileName);

            //if (taskloader.IsCompleted)
            //{
            //    myimageDownloader.ImageStarted -= MyimageDownloader_ImageStarted;
            //    myimageDownloader.ImageCompleted -= MyimageDownloader_ImageCompleted;
            //}

        }

        private void ViewStatusDownload()
        {



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
