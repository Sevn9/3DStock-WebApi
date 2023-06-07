using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock3D.CloudStorage;
using System.Xml.Linq;

namespace Stock3D.WebApi.Controllers
{
  public class CloudFilesController : Controller
  {
    private readonly IConfiguration _configuration;
    private readonly CloudStorageAuth _cloudStorageAuth;
    private ClientData data;

    // внедрим его как зависимость через конструктор
    public CloudFilesController(IConfiguration configuration, CloudStorageAuth cloudStorageAuth)
    {
      _configuration = configuration;
      _cloudStorageAuth = cloudStorageAuth;
      data = _cloudStorageAuth.GetClientData();

    }
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
    //метод загрузки файла
    private async Task<string> WriteFile(IFormFile file)
    {
      //инициализация
      //--------------------------------------
      string AccessKeyId = _configuration.GetValue<string>("Aws_access_key_id");
      string SecretAccessKey = _configuration.GetValue<string>("Aws_secret_access_key");
      string Region = _configuration.GetValue<string>("Region");
      string Bucket = _configuration.GetValue<string>("Bucket");


      var Client = new AmazonS3Client(AccessKeyId, SecretAccessKey,
          new AmazonS3Config()
          {
            RegionEndpoint = RegionEndpoint
                            .GetBySystemName(Region),
            ServiceURL = _configuration.GetValue<string>("ServiceURL")
          });

      //-----------------------------------------

      string filename = "";
      string UserIDTest = "34948920478370245";
      var objectRequest = new PutObjectRequest()
      {
        BucketName = Bucket,
        Key = $"{UserIDTest}/{file.FileName}",
        InputStream = file.OpenReadStream(),

      };
      var responce = await Client.PutObjectAsync(objectRequest);

      return filename;
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
      //инициализация
      //--------------------------------------
      string AccessKeyId = _configuration.GetValue<string>("Aws_access_key_id");
      string SecretAccessKey = _configuration.GetValue<string>("Aws_secret_access_key");
      string Region = _configuration.GetValue<string>("Region");
      string BucketName = _configuration.GetValue<string>("Bucket");
 
        var Client = new AmazonS3Client(AccessKeyId, SecretAccessKey,
          new AmazonS3Config()
          {
            RegionEndpoint = RegionEndpoint
                            .GetBySystemName(Region),
            ServiceURL = _configuration.GetValue<string>("ServiceURL")
          });
      //-----------------------------------------
      
      
      string UserIDTest = "34948920478370245";
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
      //string AccessKeyId = _configuration.GetValue<string>("Aws_access_key_id");
      //string SecretAccessKey = _configuration.GetValue<string>("Aws_secret_access_key");
      //string Region = _configuration.GetValue<string>("Region");
      //string BucketName = _configuration.GetValue<string>("Bucket");

      //var data = _cloudStorageAuth.GetClientData();
  
      Console.WriteLine(data);

      string UserIDTest = "34948920478370245";
      /*
      var Client = new AmazonS3Client(AccessKeyId, SecretAccessKey,
          new AmazonS3Config()
          {
            RegionEndpoint = RegionEndpoint
                            .GetBySystemName(Region),
            ServiceURL = _configuration.GetValue<string>("ServiceURL")
          });
      */
      var request = new ListObjectsV2Request()
      {
        BucketName = data.CloudSettings.BucketName,
        Prefix = prefix,
      };
      
      var response = await data.Client.ListObjectsV2Async(request);

      var presignedUrls = response.S3Objects.Select(obj =>
      {
        var request = new GetPreSignedUrlRequest()
        {
          BucketName = data.CloudSettings.BucketName,
          Key = obj.Key,
          Expires = DateTime.UtcNow.AddSeconds(30),
        };
        return data.Client.GetPreSignedURL(request);
      });

      return Ok(presignedUrls);
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
      //инициализация
      //--------------------------------------
      string AccessKeyId = _configuration.GetValue<string>("Aws_access_key_id");
      string SecretAccessKey = _configuration.GetValue<string>("Aws_secret_access_key");
      string Region = _configuration.GetValue<string>("Region");
      string BucketName = _configuration.GetValue<string>("Bucket");

      var Client = new AmazonS3Client(AccessKeyId, SecretAccessKey,
          new AmazonS3Config()
          {
            RegionEndpoint = RegionEndpoint
                            .GetBySystemName(Region),
            ServiceURL = _configuration.GetValue<string>("ServiceURL")
          });
      //-----------------------------------------

      string UserIDTest = "34948920478370245";

      var response = await Client.DeleteObjectAsync(BucketName, fileName);

      return Ok();

    }
  }
}
