using Amazon.S3;
using Amazon;
using System;


namespace Stock3D.CloudStorage
{
  public class CloudStorageAuth
  {
    //private readonly IConfiguration _configuration;
    private readonly CloudSettings _cloudSettings;
    public CloudStorageAuth(CloudSettings cloudSettings)
    {
      _cloudSettings = cloudSettings;
    }
   

    public ClientData GetClientData()
    {
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
  }
}
