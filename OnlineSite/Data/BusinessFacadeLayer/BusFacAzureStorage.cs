using Analyzer.Engine.Common;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSite.Data.BusinessFacadeLayer
{
    public class BusFacAzureStorage
    {
        private bool _hasError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;

        private CloudStorageAccount _cloudStorageAccount = null;
        private string _storageConnectionString = null;

        public bool HasError
        {
            get { return _hasError; }
        }
        public string ErrorStacktrace
        {
            get { return _errorStacktrace; }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
        }
        public BusFacAzureStorage(string pStorageConnStr)
        {
            _storageConnectionString = pStorageConnStr;
            _cloudStorageAccount = CloudStorageAccount.Parse(pStorageConnStr);
        }
        private CloudBlobClient GetBlobConn()
        {
            CloudBlobClient cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();
            return cloudBlobClient;
        }
        public async void CreateBlobContainer(string pStrContainerName)
        {
            CloudBlobClient conn = GetBlobConn();
            CloudBlobContainer container = conn.GetContainerReference(pStrContainerName);
            Task<bool> success = container.CreateIfNotExistsAsync();
        }

        public void MakeBlobContainerPublic(string pStrContainerName)
        {
            CloudBlobClient conn = GetBlobConn();
            CloudBlobContainer container = conn.GetContainerReference(pStrContainerName);
            container.SetPermissionsAsync(
                new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
        }
        public async void DeleteBlobContainer(string pStrContainerName)
        {
            CloudBlobClient conn = GetBlobConn();
            CloudBlobContainer container = conn.GetContainerReference(pStrContainerName);
            await container.DeleteAsync();
        }

        public CloudBlockBlob GetBlockBlobRef(string pStrContainerName, string pStrFilename)
        {
            CloudBlockBlob blockBlob = null;
            CloudBlobClient conn = GetBlobConn();
            CloudBlobContainer container = conn.GetContainerReference(pStrContainerName);

            blockBlob = container.GetBlockBlobReference(pStrFilename);
            return blockBlob;
        }

        //public string UploadBlob(string pStrContainerName, string pStrFilename, string pStrContent)
        //{
        //    CloudBlobClient conn = GetBlobConn();
        //    CloudBlobContainer container = conn.GetContainerReference(pStrContainerName);

        //    CloudBlob blob = container.GetBlobReference(pStrFilename);
        //    blob.u(pStrContent);

        //    return blob.Uri.ToString();
        //}

        public string UploadBlob(string pStrContainerName, string pStrFilename, byte[] pContent, string pStrContentType)
        {
            CloudBlobClient conn = GetBlobConn();
            CloudBlobContainer container = conn.GetContainerReference(pStrContainerName);

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(pStrFilename);
            using (var ms = new MemoryStream(pContent, false))
            {
                blockBlob.UploadFromStreamAsync(ms);
            }
            blockBlob.Properties.ContentType = pStrContentType;
            return blockBlob.Uri.ToString();
        }

        public string UploadBlob(string pStrContainerName, string pStrFilename, string pStrContent, string pStrContentType)
        {
            CloudBlobClient conn = GetBlobConn();
            CloudBlobContainer container = conn.GetContainerReference(pStrContainerName);

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(pStrFilename);
            blockBlob.UploadTextAsync(pStrContent);
            blockBlob.Properties.ContentType = pStrContentType;
          
            return blockBlob.Uri.ToString();
        }

        public Stream DownloadBlobAsStream(CloudStorageAccount account, string blobUri)
        {
            Stream stream = new MemoryStream();
            CloudBlobClient conn = GetBlobConn();
            Uri uri = new Uri(blobUri);
            CloudBlockBlob blob = new CloudBlockBlob(uri);

            if (blob != null)
            {
                blob.DownloadToStreamAsync(stream);
            }

            return stream;
        }
       
    }
}
