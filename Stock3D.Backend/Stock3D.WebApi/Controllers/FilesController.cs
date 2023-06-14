using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Stock3D.WebApi.Controllers
{
  [ApiController]
  public class FilesController : Controller
  {
    // загрузка файлов локально
    /// <summary>
    /// upload files to local machine
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("api/UploadFileLocal")]
    public async Task<IActionResult> UploadFileLocal(IFormFile file, CancellationToken cancellationToken)
    {
      var result = await WriteFile(file);
      return Ok(result);
    }

    //метод загрузки файла
    private async Task<string> WriteFile(IFormFile file)
    {
      string filename = "";
      try
      {
        var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        filename = DateTime.Now.Ticks.ToString() + extension;

        var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

        if (!Directory.Exists(filepath))
        {
          Directory.CreateDirectory(filepath);
        }

        var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
        using (var stream = new FileStream(exactpath, FileMode.Create))
        {
          await file.CopyToAsync(stream);
        }
      }
      catch (Exception ex)
      {

      }
      return filename;
    }
    /// <summary>
    /// download files to local machine
    /// </summary>
    [HttpGet]
    [Route("DownloadFileLocal")]
    public async Task<IActionResult> DownloadFileLocal(string filename, CancellationToken cancellationToken)
    {
      var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
      var provider = new FileExtensionContentTypeProvider();
      if (!provider.TryGetContentType(filepath, out var contentType))
      {
        contentType= "application/octet-stream";
      }

      var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
      return File(bytes, contentType, Path.GetFileName(filepath));
    }
  }
}
