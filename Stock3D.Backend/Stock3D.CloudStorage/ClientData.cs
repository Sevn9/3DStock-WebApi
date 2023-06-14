using Amazon.S3;
using System;

namespace Stock3D.CloudStorage
{
  public class ClientData : IDisposable
  {
    public IAmazonS3 Client { get; set; }
    public CloudSettings CloudSettings { get; set; }

    public void Dispose()
    {
      Client?.Dispose();
    }

  }
}
