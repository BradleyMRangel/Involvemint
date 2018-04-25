using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WebApplication1.Repository
{
    public interface IBlobRepository
    {
        /// <summary>
        /// Creates a private container with the given name
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task<bool> CreateContainerAsync(string containerName, bool isPublic);

        Task<bool> DeleteContainerAsync(string containerName);

        /// <summary>
        /// Uploads the given stream to the name of the blob. Please note that if blob exists, contents will be overriten.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="blobNameToCreate"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task UploadBlobAsync(Stream fileStream, string blobNameToCreate, string containerName);

        /// <summary>
        /// gets the collection of IListBlobItem in the given container holding CloudBlobContainer, CloudBlobDirectory, StorageUri and URI
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        IEnumerable<IListBlobItem> GetListBlobs(string containerName);

        /// <summary>
        /// Download given blob and return filestream
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        Task<FileStream> DownloadBlobAsStreamAsync(FileStream fileStream, string containerName, string blobName);

        /// <summary>
        /// Download given blob as string and return filestream
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        Task<string> DownloadBlobContentAsStringAsync(string containerName, string blobName);

        /// <summary>
        /// Download given blob as byte[]
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        Task<byte[]> DownloadBlobContentAsByteArrayAsync(string containerName, string blobName);

        /// <summary>
        /// Deletes the given blob
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        Task DeleteBlobAsync(string containerName, string blobName);
    }

    public class BlobRepository : IBlobRepository
    {
        CloudStorageAccount storageAccount;
        CloudBlobClient blobClient;
        CloudBlobContainer blobContainer;

        public  BlobRepository()
        {      // see Web.config for definition of "StorageConnectionString"
              //< add key = "StorageConnectionString" value = "DefaultEndpointsProtocol=https;AccountName=bacs488;AccountKey=OdVhrFB05FhYW5wbtmRcoeQwfoVUZ25+3G2z1fXONW1fbcI8ueWcfUvwLEWoVp+ICBBUbeG4pHSc1NdPcl50Jw==" />

             var test = CloudConfigurationManager.GetSetting("StorageConnectionString");
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            blobClient = storageAccount.CreateCloudBlobClient();
           // CreateContainerAsync("todd", true).Wait();
        }

        
        public async Task<bool> CreateContainerAsync(string containerName, bool isPublic)
        {
            // Create the container if it doesn't exist.
            blobContainer = blobClient.GetContainerReference(containerName);
            if (isPublic)
            {
                try
                {
                    var returnData = await blobContainer.CreateIfNotExistsAsync();
                if (returnData)
                    await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                return returnData;
                }
                catch (Exception e)
                {

                    var message = e.Message;
                }
                
            }
            return await blobContainer.CreateIfNotExistsAsync();
        }

        public async Task<bool> DeleteContainerAsync(string containerName)
        {
            // Delete the container if it doesn't exist.
            blobContainer = blobClient.GetContainerReference(containerName);
            return await blobContainer.DeleteIfExistsAsync();
        }

        public CloudBlockBlob GetBlockBlob(string blobNameToCreate, string containerName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Retrieve reference to the blob
           return container.GetBlockBlobReference(blobNameToCreate);
        }
        public async Task UploadBlobAsync(Stream fileStream, string blobNameToCreate, string containerName)
        {
            // Retrieve reference the given container
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Retrieve reference to the blob
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobNameToCreate);

            // Create or overwrite blob with contents from parameter.
            await blockBlob.UploadFromStreamAsync(fileStream);
           
        }

        
        public IEnumerable<IListBlobItem> GetListBlobs(string containerName)
        {
            // Retrieve reference the given container
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Get List of blobs is not async, so we need to wrap it with a Task to wait until finished
            return container.ListBlobs(null, false);
        }

        
        public async Task<FileStream> DownloadBlobAsStreamAsync(FileStream fileStream, string containerName, string blobName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            // Save blob contents to a filestream.
            await blockBlob.DownloadToStreamAsync(fileStream);
            return fileStream;
        }

       
        public async Task<string> DownloadBlobContentAsStringAsync(string containerName, string blobName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            // Save blob contents to a filestream.
            using (MemoryStream mStream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(mStream);
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
        }

     
        public async Task<byte[]> DownloadBlobContentAsByteArrayAsync(string containerName, string blobName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            // Save blob contents to a MemoryStream.
            using (MemoryStream mStream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(mStream);
                return mStream.ToArray();
            }
        }

       
        public async Task DeleteBlobAsync(string containerName, string blobName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            await blockBlob.DeleteAsync();
        }
    }
}