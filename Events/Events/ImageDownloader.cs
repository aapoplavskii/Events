﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class ImageDownloader
    {

        public void Download(string remoteUri, string fileName)
        { 
           
            var _webclient = new WebClient();

            Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\n\n", fileName, remoteUri);

            _webclient.DownloadFile(remoteUri, fileName);

            Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileName, remoteUri);

        }

    }
}
