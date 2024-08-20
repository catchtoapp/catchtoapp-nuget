using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using catchtoapp_nuget_blobstorage.implements;
using catchtoapp_nuget_models;
using Moq;
using Xunit.Abstractions;

namespace catchtoapp_nuget_test
{
    public class CatchtoappBlobStorageServiceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly MockRepository _mockRepository;
        private readonly Mock<BlobServiceClient> _moqBlobServiceClient;
        private readonly Mock<BlobContainerClient> _moqBlobContainerClient;

        public CatchtoappBlobStorageServiceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _mockRepository = new MockRepository(MockBehavior.Default);
            _moqBlobServiceClient = _mockRepository.Create<BlobServiceClient>();
            _moqBlobContainerClient = _mockRepository.Create<BlobContainerClient>();
        }

        [Fact]
        public void MessageCatchtoappBlobStorageServiceTest()
        {
            _testOutputHelper.WriteLine("MessageCatchtoappBlobStorageServiceTest");
        }

        [Fact]
        public async void CorrectCatchtoappBlobStorageServiceTest()
        {
            var objBlobStorage = new CatchtoappBlobStorageService(_moqBlobServiceClient.Object);
            var existsResponse = new Mock<Response<bool>>();
            existsResponse.Setup(r => r.Value).Returns(true);

            var uploadResponse = new Mock<Response<BlobContentInfo>>();
            uploadResponse.Setup(r => r.Value).Returns(BlobsModelFactory.BlobContentInfo(default, DateTime.Now, default, string.Empty, string.Empty, string.Empty, default));

            var blobClient = new Mock<BlobClient>();
            blobClient.Setup(c => c.UploadAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).ReturnsAsync(uploadResponse.Object);

            _moqBlobContainerClient.Setup(c => c.ExistsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(existsResponse.Object);
            _moqBlobContainerClient.Setup(c => c.GetBlobClient(It.IsAny<string>())).Returns(blobClient.Object);
            _moqBlobServiceClient.Setup(x => x.GetBlobContainerClient(It.IsAny<string>())).Returns(_moqBlobContainerClient.Object);

            var request = new RequestModel
            {
                UriStorageAccount = "https://localhost.net/",
                Container = "lfmreq",
                FileName = "CorrectCatchtoappBlobStorageServiceTest_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffffK"),
                RequestBody = "Content Test"
            };

            var result = await objBlobStorage.AddFileLogger(request);

            //Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public async void ModelInvalidCatchtoappBlobStorageServiceTest()
        {
            var objBlobStorage = new CatchtoappBlobStorageService(_moqBlobServiceClient.Object);
            var existsResponse = new Mock<Response<bool>>();
            existsResponse.Setup(r => r.Value).Returns(true);

            var uploadResponse = new Mock<Response<BlobContentInfo>>();
            uploadResponse.Setup(r => r.Value).Returns(BlobsModelFactory.BlobContentInfo(default, DateTime.Now, default, string.Empty, string.Empty, string.Empty, default));

            var blobClient = new Mock<BlobClient>();
            blobClient.Setup(c => c.UploadAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).ReturnsAsync(uploadResponse.Object);

            _moqBlobContainerClient.Setup(c => c.ExistsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(existsResponse.Object);
            _moqBlobContainerClient.Setup(c => c.GetBlobClient(It.IsAny<string>())).Returns(blobClient.Object);
            _moqBlobServiceClient.Setup(x => x.GetBlobContainerClient(It.IsAny<string>())).Returns(_moqBlobContainerClient.Object);

            var request = new RequestModel
            {
                UriStorageAccount = "https://localhost.net/",
                Container = "",
                FileName = "ModelInvalidCatchtoappBlobStorageServiceTest_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffffK"),
                RequestBody = "Content Test"
            };

            var result = await objBlobStorage.AddFileLogger(request);

            //Assert
            Assert.Equal("Model invalid.", result);
        }

        [Fact]
        public async void ContainerNotExistCatchtoappBlobStorageServiceTest()
        {
            var objBlobStorage = new CatchtoappBlobStorageService(_moqBlobServiceClient.Object);
            var existsResponse = new Mock<Response<bool>>();
            existsResponse.Setup(r => r.Value).Returns(false);

            var uploadResponse = new Mock<Response<BlobContentInfo>>();
            uploadResponse.Setup(r => r.Value).Returns(BlobsModelFactory.BlobContentInfo(default, DateTime.Now, default, string.Empty, string.Empty, string.Empty, default));

            var blobClient = new Mock<BlobClient>();
            blobClient.Setup(c => c.UploadAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).ReturnsAsync(uploadResponse.Object);

            _moqBlobContainerClient.Setup(c => c.ExistsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(existsResponse.Object);
            _moqBlobContainerClient.Setup(c => c.GetBlobClient(It.IsAny<string>())).Returns(blobClient.Object);
            _moqBlobServiceClient.Setup(x => x.GetBlobContainerClient(It.IsAny<string>())).Returns(_moqBlobContainerClient.Object);

            var request = new RequestModel
            {
                UriStorageAccount = "https://localhost.net/",
                Container = "lfmreq",
                FileName = "ContainerNotExistCatchtoappBlobStorageServiceTest_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffffK"),
                RequestBody = "Content Test"
            };

            var result = await objBlobStorage.AddFileLogger(request);

            //Assert
            Assert.Equal("Container does not exist. File can not be created " + request.Container, result);
        }

        [Fact]
        public async void CatchExceptionCatchtoappBlobStorageServiceTest()
        {
            var objBlobStorage = new CatchtoappBlobStorageService(_moqBlobServiceClient.Object);
            var uploadResponse = new Mock<Response<BlobContentInfo>>();
            uploadResponse.Setup(r => r.Value).Returns(BlobsModelFactory.BlobContentInfo(default, DateTime.Now, default, string.Empty, string.Empty, string.Empty, default));

            var blobClient = new Mock<BlobClient>();
            blobClient.Setup(c => c.UploadAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).ReturnsAsync(uploadResponse.Object);

            _moqBlobContainerClient.Setup(c => c.GetBlobClient(It.IsAny<string>())).Returns(blobClient.Object);
            _moqBlobServiceClient.Setup(x => x.GetBlobContainerClient(It.IsAny<string>())).Returns(_moqBlobContainerClient.Object);

            var request = new RequestModel
            {
                UriStorageAccount = "https://localhost.net/",
                Container = "lfmreq",
                FileName = "CatchExceptionCatchtoappBlobStorageServiceTest_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffffK"),
                RequestBody = "Content Test"
            };

            var result = await objBlobStorage.AddFileLogger(request);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void CatchRequestFailedExceptionCatchtoappBlobStorageServiceTest()
        {
            var objBlobStorage = new CatchtoappBlobStorageService(_moqBlobServiceClient.Object);
            var existsResponse = new Mock<Response<bool>>();
            existsResponse.Setup(r => r.Value).Returns(true);

            var uploadResponse = new Mock<Response<BlobContentInfo>>();
            uploadResponse.Setup(r => r.Value).Returns(BlobsModelFactory.BlobContentInfo(default, DateTime.Now, default, string.Empty, string.Empty, string.Empty, default));

            var blobClient = new Mock<BlobClient>();
            blobClient.Setup(c => c.UploadAsync(It.IsAny<Stream>())).Throws(new RequestFailedException("Simulated exception"));

            _moqBlobContainerClient.Setup(c => c.ExistsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(existsResponse.Object);
            _moqBlobContainerClient.Setup(c => c.GetBlobClient(It.IsAny<string>())).Returns(blobClient.Object);
            _moqBlobServiceClient.Setup(x => x.GetBlobContainerClient(It.IsAny<string>())).Returns(_moqBlobContainerClient.Object);

            var request = new RequestModel
            {
                UriStorageAccount = "https://localhost.net/",
                Container = "lfmreq",
                FileName = "CatchRequestFailedExceptionCatchtoappBlobStorageServiceTest_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffffK"),
                RequestBody = "Content Test"
            };

            var result = await objBlobStorage.AddFileLogger(request);

            //Assert
            Assert.NotNull(result);
        }
    }
}
