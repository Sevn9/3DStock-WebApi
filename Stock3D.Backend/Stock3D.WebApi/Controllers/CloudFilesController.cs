using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Stock3D.WebApi.Controllers
{
  public class CloudFilesController : Controller
  {
    private readonly IConfiguration _configuration;

    // внедрим его как зависимость через конструктор
    public CloudFilesController(IConfiguration configuration) => _configuration = configuration;
    // загрузка файлов на облако
    /// <summary>
    /// upload files to cloud
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("api/UploadFileToCloud")]
    public async Task<IActionResult> UploadFileToCloud(IFormFile file, CancellationToken cancellationToken)
    {
      var result = await WriteFile(file);
      return Ok(result);
    }
    // получение файла по имени
    /// <summary>
    /// get a file by name
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("api/GetFileFromCloud")]
    public async Task<IActionResult> GetFileFromCloud(string fileName)
    {
      string AccessKeyId = _configuration.GetValue<string>("Aws_access_key_id");
      string SecretAccessKey = _configuration.GetValue<string>("Aws_secret_access_key");
      string Region = _configuration.GetValue<string>("Region");
      string BucketName = _configuration.GetValue<string>("Bucket");

      string UserIDTest = "34948920478370245";

      var Client = new AmazonS3Client(AccessKeyId, SecretAccessKey,
          new AmazonS3Config()
          {
            RegionEndpoint = RegionEndpoint
                            .GetBySystemName(Region),
            ServiceURL = _configuration.GetValue<string>("ServiceURL")
          });
      var response = await Client.GetObjectAsync(BucketName, fileName);

      //using var reader = new StreamReader(response.ResponseStream);
      //var fileContents = await reader.ReadToEndAsync();

      return File(response.ResponseStream, response.Headers.ContentType);
    }

    // получение всех файлов
    /// <summary>
    /// get all files from cloud
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("api/GetAllFilesFromCloud")]
    public async Task<IActionResult> GetAllFilesFromCloud(string? prefix)
    {
      string AccessKeyId = _configuration.GetValue<string>("Aws_access_key_id");
      string SecretAccessKey = _configuration.GetValue<string>("Aws_secret_access_key");
      string Region = _configuration.GetValue<string>("Region");
      string BucketName = _configuration.GetValue<string>("Bucket");

      string UserIDTest = "34948920478370245";

      var Client = new AmazonS3Client(AccessKeyId, SecretAccessKey,
          new AmazonS3Config()
          {
            RegionEndpoint = RegionEndpoint
                            .GetBySystemName(Region),
            ServiceURL = _configuration.GetValue<string>("ServiceURL")
          });
      var request = new ListObjectsV2Request()
      {
        BucketName = BucketName,
        Prefix = prefix,
      };
      var response = await Client.ListObjectsV2Async(request);

      var presignedUrls = response.S3Objects.Select(obj =>
      {
        var request = new GetPreSignedUrlRequest()
        {
          BucketName = BucketName,
          Key = obj.Key,
          Expires = DateTime.UtcNow.AddSeconds(30),
        };
        return Client.GetPreSignedURL(request);
      });

      return Ok(presignedUrls);
    }

    //метод загрузки файла
    private async Task<string> WriteFile(IFormFile file)
    {
      string filename = "";
      string AccessKeyId = _configuration.GetValue<string>("Aws_access_key_id");
      string SecretAccessKey = _configuration.GetValue<string>("Aws_secret_access_key");
      string Region = _configuration.GetValue<string>("Region");
      string Bucket = _configuration.GetValue<string>("Bucket");

      string UserIDTest = "34948920478370245";
      try
      {
        //инициализация
        var Client = new AmazonS3Client(AccessKeyId, SecretAccessKey,
          new AmazonS3Config()
          {
            RegionEndpoint = RegionEndpoint
                            .GetBySystemName(Region),
            ServiceURL = _configuration.GetValue<string>("ServiceURL")
          });

        var objectRequest = new PutObjectRequest()
        {
          BucketName = Bucket,
          Key = $"{UserIDTest}/{file.FileName}",
          InputStream = file.OpenReadStream(),

        };
        var responce = await Client.PutObjectAsync(objectRequest);

      }
      catch (Exception ex)
      {

      }
      return filename;
    }


    // удаление файла
    /// <summary>
    /// delete file from cloud
    /// </summary>
    /// [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("api/DeleteFileFromCloud")]
    [HttpDelete]
    public async Task<IActionResult> DeleteFileFromCloud(string fileName)
    {
      string AccessKeyId = _configuration.GetValue<string>("Aws_access_key_id");
      string SecretAccessKey = _configuration.GetValue<string>("Aws_secret_access_key");
      string Region = _configuration.GetValue<string>("Region");
      string BucketName = _configuration.GetValue<string>("Bucket");

      string UserIDTest = "34948920478370245";

      var Client = new AmazonS3Client(AccessKeyId, SecretAccessKey,
          new AmazonS3Config()
          {
            RegionEndpoint = RegionEndpoint
                            .GetBySystemName(Region),
            ServiceURL = _configuration.GetValue<string>("ServiceURL")
          });

      var response = await Client.DeleteObjectAsync(BucketName, fileName);

      return Ok();

    }
  }
}
