using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock3D.Application.Models3D.Commands.CreateModel3D;
using Stock3D.Application.Models3D.Commands.DeleteModel3D;
using Stock3D.Application.Models3D.Commands.UpdateModel3D;
using Stock3D.Application.Models3D.Queries.GetModel3DDetails;
using Stock3D.Application.Models3D.Queries.GetModel3DList;
using Stock3D.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class Model3DController : BaseController
  {
    //mapper чтобы преобразовать входные данные в команду
    private readonly IMapper _mapper;
    // внедрим его как зависимость через конструктор
    public Model3DController(IMapper mapper) => _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<Model3DListVm>> GetAll()
    {
      //сформируем запрос и с помощью медиатора просто отправим его
      // а полученный результат вернем клиенту
      var query = new GetModel3DListQuery
      {
        UserId = UserId,

      };
      var vm = await Mediator.Send(query);
      return Ok(vm);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Model3DListVm>> Get(Guid id)
    {
      var query = new GetModel3DDetailsQuery
      {
        UserId = UserId,
        Id = id
      };
      var vm = await Mediator.Send(query);
      return Ok(vm);
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateModel3DDto createModel3DDto)
    {
      //сформируем команду и добавим к ней id пользователя
      var command = _mapper.Map<CreateModel3DCommand>(createModel3DDto);
      command.UserId = UserId;
      var model3DId = await Mediator.Send(command);
      //по хорошему использовать Created()
      return Ok(model3DId);

    }
    [HttpPut]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateModel3DDto updateModel3DDto)
    {
      var command = _mapper.Map<UpdateModel3DCommand>(updateModel3DDto);
      command.UserId = UserId;
      await Mediator.Send(command);
      return NoContent();

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var command = new DeleteModel3DCommand
      {
        Id = id,
        UserId = UserId

      };
      await Mediator.Send(command);
      return NoContent();

    }
  }
}
