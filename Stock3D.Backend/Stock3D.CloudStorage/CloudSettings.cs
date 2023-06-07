using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Stock3D.CloudStorage
{
  public class CloudSettings
  {
    IConfiguration _configuration;
    public CloudSettings(IConfiguration configuration) {
      _configuration = configuration;
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
