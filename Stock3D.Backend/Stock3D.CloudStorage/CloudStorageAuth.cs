using Amazon.S3;
using Amazon;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.CloudStorage
{
  public class CloudStorageAuth
  {
    //private readonly IConfiguration _configuration;
    private readonly CloudSettings _cloudSettings;
    public CloudStorageAuth(CloudSettings cloudSettings)
    {
      //проверить
      _cloudSettings = cloudSettings;
    }
   

    public ClientData GetClientData()
    {
      //var settings = LoadSettings();

      return new ClientData()
      {
        Client = new AmazonS3Client(
              _cloudSettings.AccessKeyId,
              _cloudSettings.SecretAccessKey,
              new AmazonS3Config()
              {
                RegionEndpoint = RegionEndpoint
                      .GetBySystemName(_cloudSettings.Region),
                ServiceURL = _cloudSettings.ServiceURL
              }),
        CloudSettings = _cloudSettings
      };
    }
    /*
    private static CloudSettings LoadSettings()
    {
      var settings = new CloudSettings()
      {
        BucketName = _cloudSettings["Bucket"],
        AccessKeyId = _configuration["Aws_access_key_id"],
        SecretAccessKey = _configuration["Aws_secret_access_key"],
        Region = _configuration["Region"],
        ServiceURL = _configuration["ServiceURL"]
      };
      return settings;
    }
    */
  }
}
