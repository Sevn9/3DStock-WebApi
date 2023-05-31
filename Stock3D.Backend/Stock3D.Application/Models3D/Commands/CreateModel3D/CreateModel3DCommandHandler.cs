using MediatR;
using Stock3D.Application.Interfaces;
using Stock3D.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock3D.Application.Models3D.Commands.CreateModel3D
{
  //обработчик создания команды, указываем тип запроса и тип ответа.
  //CreateModel3DCommand - это что необходимо для создания модели, 
  // CreateModel3DCommandHandler - содержит в себе логику создания
  public class CreateModel3DCommandHandler: IRequestHandler<CreateModel3DCommand, Guid>
  {
    //сохранение изменений в базу
    private readonly IStock3DDbContext _dbContext;
    //сделаем внедрение зависимости на контекст базы данных через конструктор
    public CreateModel3DCommandHandler(IStock3DDbContext dbContext) => _dbContext = dbContext;

    //логика обработки команд находится в handle
    public async Task<Guid> Handle(CreateModel3DCommand request, CancellationToken cancellationToken)
    {
      //формируем модель из нашего запроса и возвращаем id созданной модели
      var model3D = new Model3D
      {
        UserId = request.UserId,
        Title = request.Title,
        Details = request.Details,
        Id = Guid.NewGuid(),
        UploadDate = DateTime.UtcNow,
      };
      // добавление созданной модели в контекст, а затем сохранение изменений в базу
      await _dbContext.Models3D.AddAsync(model3D, cancellationToken);
      await _dbContext.SaveChangesAsync(cancellationToken);
      return model3D.Id;

    }
  }
}
