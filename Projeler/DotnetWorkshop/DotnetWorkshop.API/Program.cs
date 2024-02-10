using DotnetWorkshop.Core.Repositories;
using DotnetWorkshop.Core.Services;
using DotnetWorkshop.Core.UnitOfWorks;
using DotnetWorkshop.Repository;
using DotnetWorkshop.Repository.Repositories;
using DotnetWorkshop.Repository.UnitOfWorks;
using DotnetWorkshop.Service.Mapping;
using DotnetWorkshop.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>),typeof(Service<>));

//AppDbContext ile ilgili iþlemler

builder.Services.AddDbContext<AppDbContext>(x =>
  x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
  {
      option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
  })
); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
