using Microsoft.Extensions.Configuration;
using System;

namespace Stock3D.CloudStorage
{
  public class CloudSettings
  {
    public CloudSettings(IConfiguration configuration) {
      BucketName = configuration["Bucket"];
      AccessKeyId = configuration["Aws_access_key_id"];
      SecretAccessKey = configuration["Aws_secret_access_key"];
      Region = configuration["Region"];
      ServiceURL = configuration["ServiceURL"];
    }
    public string BucketName { get; set; } = string.Empty;
    public string AccessKeyId { get; set; } = string.Empty;
    public string SecretAccessKey { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string ServiceURL { get; set; } = string.Empty;
  }
}
