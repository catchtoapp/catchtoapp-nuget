using Azure;
using Azure.Storage.Blobs;
using catchtoapp_nuget.contracts;
using catchtoapp_nuget_models;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace catchtoapp_nuget_blobstorage.implements
{
    public class CatchtoappBlobStorageService : ICatchtoappBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public CatchtoappBlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> AddFileLogger(RequestModel request)
        {
            string _result = string.Empty;
            try
            {
                ValidationContext vcRequestModel = new ValidationContext(request);
                ICollection<ValidationResult> vcResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(request, vcRequestModel, vcResults, true))
                {
                    return "Model invalid.";
                }


                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(request.Container);
                bool exists = await containerClient.ExistsAsync();

                if (exists)
                {
                    BlobClient blobClient = containerClient.GetBlobClient(request.FileName);
                    using var fileStream = new MemoryStream(Encoding.UTF8.GetBytes(request.RequestBody));
                    await blobClient.UploadAsync(fileStream);
                }
                else
                {
                    _result = "Container does not exist. File can not be created " + request.Container;
                }
            }
            catch (RequestFailedException ex)
            {
                _result = $"Error in the request {ex.Message}";
            }
            catch (Exception ex)
            {
                _result = ex.Message.ToString();
            }
            return _result;
        }
    }
}
