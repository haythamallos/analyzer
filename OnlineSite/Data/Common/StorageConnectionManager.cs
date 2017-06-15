using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Analyzer.Engine.Common
{
    public class StorageConnectionManager
    {
        private static string _storageConnectionString = null;
        private static bool _bInstance = false;
        public static StorageConnectionManager _storageConnectionManager = null;
        private static CloudStorageAccount _cloudStorageAccount = null;

        private bool _hasError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;

        public static bool Instance
        {
            get { return _bInstance; }
            set { _bInstance = value; }
        }

        public string ErrorStacktrace
        {
            get { return _errorStacktrace; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        public bool hasError
        {
            get { return _hasError; }
        }

        public StorageConnectionManager()
        {

        }
        public CloudStorageAccount GetStorageAccount()
        {
            CloudStorageAccount storageAccountConnection = null;
            try
            {
            }
            catch (Exception e)
            {
                storageAccountConnection = null;
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
            }
            return storageAccountConnection;
        }

        public CloudTableClient GetTableConn()
        {
            CloudTableClient cloudTableClient = null;
            try
            {
                // Create the table client
                cloudTableClient = _cloudStorageAccount.CreateCloudTableClient();
            }
            catch (Exception e)
            {
                cloudTableClient = null;
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
            }
            return cloudTableClient;
        }

        public CloudBlobClient GetBlobConn()
        {
            CloudBlobClient cloudBlobClient = null;
            try
            {
                // Create the table client
                cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();
            }
            catch (Exception e)
            {
                cloudBlobClient = null;
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
            }
            return cloudBlobClient;
        }

        public CloudQueueClient GetQueueConn()
        {
            CloudQueueClient cloudQueueClient = null;
            try
            {
                // Create the table client
                cloudQueueClient = _cloudStorageAccount.CreateCloudQueueClient();
            }
            catch (Exception e)
            {
                cloudQueueClient = null;
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
            }
            return cloudQueueClient;
        }

        public static StorageConnectionManager GetInstance(string pStorageConnStr)
        {
            if (!Instance)
            {
                _storageConnectionManager = new StorageConnectionManager();
                _cloudStorageAccount = CloudStorageAccount.Parse(pStorageConnStr);
                Instance = true;
            }
            return _storageConnectionManager;
        }

        public static StorageConnectionManager GetInstance(Config pConfig)
        {
            if (!Instance)
            {
                _storageConnectionManager = new StorageConnectionManager();
                _cloudStorageAccount = CloudStorageAccount.Parse(_storageConnectionString);
                Instance = true;
            }
            return _storageConnectionManager;
        }

        public static StorageConnectionManager GetInstance()
        {
            if (!Instance)
            {
                _storageConnectionManager = new StorageConnectionManager();
                _cloudStorageAccount = CloudStorageAccount.Parse(_storageConnectionString);
                Instance = true;
            }
            return _storageConnectionManager;
        }
    }
}
