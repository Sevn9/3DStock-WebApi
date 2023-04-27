using System;

namespace Stock3D.Application.Common.Exceptions
{
  public class NotFoundException : Exception
  {
    public NotFoundException(string name, object key)
      : base($"Entity \"{name}\" ({key}) not found.") { }

  }
}
