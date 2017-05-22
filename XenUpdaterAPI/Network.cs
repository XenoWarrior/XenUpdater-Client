using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XenUpdaterAPI
{
    public class Network
    {
        private string serverURL;
        private bool currentDownloadComplete;
        private bool currentDownloadCancelled;
        private string currentDownloadProgress;

        /// <summary>
        /// Constructor, sets up the connection to the backend.
        /// </summary>
        /// <param name="url">Backend API url</param>
        public Network(string url)
        {
            serverURL = url;
        }

        /// <summary>
        /// Gets a file from a specified url
        /// </summary>
        /// <param name="url">File url</param>
        /// <param name="dest">File save location</param>
        public void GetFile(string url, string dest)
        {
            WebClient dlClient = new WebClient();
            Uri dlUrl = new Uri(url);

            currentDownloadComplete = false;
            currentDownloadCancelled = false;

            dlClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadComplete);
            dlClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgress);
            dlClient.DownloadFileAsync(dlUrl, dest);
        }

        /// <summary>
        /// Handler for on progress changed
        /// </summary>
        private void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            currentDownloadProgress = string.Format(
                "{0}    downloaded {1} of {2} bytes. {3} % complete...",
                (string)e.UserState,
                e.BytesReceived,
                e.TotalBytesToReceive,
                e.ProgressPercentage);
        }

        /// <summary>
        /// Handler for on download complete
        /// </summary>
        private void DownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                currentDownloadCancelled = true;
            }
            else
            {
                currentDownloadComplete = true;
            }
        }

        /// <summary>
        /// Gets the current progress value
        /// </summary>
        /// <returns>string: Progress complete message</returns>
        public string GetProgress()
        {
            return currentDownloadProgress;
        }

        /// <summary>
        /// Gets the updater configuration from the API
        /// </summary>
        public async Task<UpdaterConfig> GetUpdaterConfig()
        {
            HttpClient dlClient = new HttpClient();
            HttpResponseMessage dlResponse = await dlClient.GetAsync(serverURL + "/xu/update/updater_cfg.json");

            string jsonFeed = await dlResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UpdaterConfig>(jsonFeed);
        }

        /// <summary>
        /// Gets a specific package configuration from the API
        /// </summary>
        /// <param name="package">Package name</param>
        public async Task<PackageConfig> GetPackageConfig(string package)
        {
            HttpClient dlClient = new HttpClient();
            HttpResponseMessage dlResponse = await dlClient.GetAsync(serverURL + "/xu/update/updater_cfg.json");

            string jsonFeed = await dlResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PackageConfig>(jsonFeed);
        }

        /// <summary>
        /// Checks if the API is available
        /// </summary>
        /// <returns>bool: success or failure</returns>
        public async Task<bool> TestConnection()
        {
            HttpClient dlClient = new HttpClient();
            HttpResponseMessage dlResponse = await dlClient.GetAsync(serverURL + "/status.php");

            string response = await dlResponse.Content.ReadAsStringAsync();
            if(response == "OK")
            { 
                return true;
            }

            return false;
        }
    }
}