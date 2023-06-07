using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
