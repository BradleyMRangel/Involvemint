using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using WebApplication1.Properties;


namespace WebApplication1.Repository
{
   public class BlogStorageAdapter 
   {
        private BlobRepository _blobRepository = new BlobRepository();
        public string ContainerName { get; set; }


       public BlogStorageAdapter()
       {
           ContainerName = Settings.Default.blogStorageCollection;// store the name in settings
    }
        public async Task Add(string id, Stream anObject)
        {
            await _blobRepository.UploadBlobAsync(anObject, id, ContainerName);
        }

       public CloudBlockBlob GetBlockBlob(string blobNameToCreate)
       {
           return _blobRepository.GetBlockBlob(blobNameToCreate, ContainerName);
       }
        public async Task ClearAll()
        { 
            await _blobRepository.DeleteContainerAsync(ContainerName);
        }

        public async Task Delete(string BlobId)
        {
           await  _blobRepository.DeleteBlobAsync(ContainerName, BlobId);
        }

        public Maybe<Stream> Get(string blobName)
        {
            var result = _blobRepository.DownloadBlobContentAsStringAsync(ContainerName, blobName);
            if (result.Result.Length == 0) return new Maybe<Stream>();
            else
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(result.Result); 
                MemoryStream stream = new MemoryStream(byteArray);
                return new Maybe<Stream>(stream);  // return a stream representing image bytes
            }
           
        }

        public IEnumerator<Stream> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public async Task Update(string id, Stream anObject)
        {
           await Add(id,anObject);
        }

        public Task Clear()
        {
           return  _blobRepository.DeleteContainerAsync(ContainerName);
        }
    }
}
