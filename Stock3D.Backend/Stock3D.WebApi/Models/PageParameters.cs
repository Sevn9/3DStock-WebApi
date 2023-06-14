using System;

namespace Stock3D.WebApi.Models
{
  public class PageParameters
  {
    const int maxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    //кол-во записей на странице
    private int _pageSize = 10;
    public int PageSize
    {
      get
      {
        return _pageSize;
      }
      set
      {
        _pageSize = (value > maxPageSize) ? maxPageSize : value;
      }
    }

  }
}
