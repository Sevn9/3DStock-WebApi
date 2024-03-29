﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock3D.Application.Models3D.Commands.CreateModel3D;
using Stock3D.Application.Models3D.Commands.CreateModel3DWithFile;
using Stock3D.Application.Models3D.Commands.DeleteModel3D;
using Stock3D.Application.Models3D.Commands.UpdateModel3D;
using Stock3D.Application.Models3D.Commands.UpdateModel3DWithFile;
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
  //Produces - действия контроллера поддерживают тип содержимого ответа application/json
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class Model3DWithFileController : BaseController
  {
    //mapper чтобы преобразовать входные данные в команду
    private readonly IMapper _mapper;
    // внедрим его как зависимость через конструктор
    public Model3DWithFileController(IMapper mapper) => _mapper = mapper;

    //для получения списка 3D моделей
    /// <summary>
    /// Get the list of Model3D
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /model3d
    /// </remarks>
    /// <returns>Returns Model3DListVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401"> If the user is unautorized</response>
    /// 
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Model3DListVm>> GetAll([FromQuery] PageParameters pageParameters)
    {
      //сформируем запрос и с помощью медиатора отправим его
      // полученный результат вернем клиенту
      var query = new GetModel3DListQuery
      {
        UserId = UserId,
        PageNumber = pageParameters.PageNumber,
        PageSize = pageParameters.PageSize,

      };
      var vm = await Mediator.Send(query);
      return Ok(vm);
    }
    //get для получения деталей конкретной 3D модели
    /// <summary>
    /// Gets the 3D model by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /model3d/D34D349E-43B8-429E-BCA4-793C932FD580
    /// </remarks>
    /// <param name="id">Model3D id (guid)</param>
    /// <returns>Returns Model3DDetailsVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user in unauthorized</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
    // FromBody указывает что параметр метода контроллера должен быть извлечен из данных тела 
    //http запроса и десериализован с помощью формата входных данных

    //create для создания объекта 3D модели
    /// <summary>
    /// Creates the 3D model object
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /model3d
    /// {
    ///     title: "model3d title",
    ///     details: "model3d details",
    ///     category: "string",
    ///     price: "string"
    /// }
    /// </remarks>
    /// <param name="createModelWithFile3DDto">createModel3DDto object</param>
    /// <returns>Returns id (guid)</returns>
    /// <response code="201">Created</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [DisableRequestSizeLimit,
    RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue,
        ValueLengthLimit = int.MaxValue)]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<Guid>> Create([FromForm] CreateModel3DWithFileDto createModelWithFile3DDto)
    {
      //сформируем команду и добавим к ней id пользователя
      var command = _mapper.Map<CreateModel3DWithFileCommand>(createModelWithFile3DDto);
      command.UserId = UserId;
      var model3DId = await Mediator.Send(command);
      return CreatedAtAction(nameof(Get), new { id = model3DId }, model3DId);

    }
    //update для обновления информации о 3D модели
    /// <summary>
    /// Updates the 3D model object
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT /model3d
    /// {
    ///     title: "updated 3D model title"
    ///     details: "model3d details",
    ///     category: "string",
    ///     price: "string"
    /// }
    /// </remarks>
    /// <param name="updateModel3DWithFileDto">updateModel3DWithFileDto object</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [DisableRequestSizeLimit,
    RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue,
        ValueLengthLimit = int.MaxValue)]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateModel3DWithFileDto updateModel3DWithFileDto)
    {
      var command = _mapper.Map<UpdateModel3DWithFileCommand>(updateModel3DWithFileDto);
      command.UserId = UserId;
      await Mediator.Send(command);
      return NoContent();

    }
    //delete для удаления 3D модели
    /// <summary>
    /// Delete the 3D model by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /model3d/88DEB432-062F-43DE-8DCD-8B6EF79073D3
    /// </remarks>
    /// <param name="id">Id of the 3D model (guid)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
