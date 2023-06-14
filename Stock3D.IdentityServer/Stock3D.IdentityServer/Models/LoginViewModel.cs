using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.IdentityServer.Models
{
  public class LoginViewModel
  {
    /// <summary>
    /// [Required] - имя и пароль обязательные поля
    /// [DataType(DataType.Password)] - при вводе данных пароль не отображается
    /// </summary>
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    //поле для возврата
    public string ReturnUrl { get; set; }

  }
}
