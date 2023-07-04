using System;

namespace Stock3D.Domain
{
  public class Model3D
  {
    //id пользователя
    public Guid UserId { get; set; }
    //id 3д модели
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }

    public string? Category { get; set; }

    public string? FileFormat { get; set; }
    public string? Price { get; set; }

    public DateTime? UploadDate { get; set; }

    public string? FilePath { get; set; }

    public string? FileFullName { get; set; }

    public string? ImagePath { get; set; }
  }
}
