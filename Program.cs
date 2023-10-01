using eventz.Controllers;
using eventz.Data;
using eventz.Mappings;
using eventz.Repositories;
using eventz.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


var connectionStringMysql = builder.Configuration.GetConnectionString("myConnectionString");

builder.Services.AddDbContext<UserDbContext>(options => options.UseMySql(
    connectionStringMysql,
    new MySqlServerVersion(new Version(8, 1, 0))
));

builder.Services.AddScoped<IUserRepositorie, UserRepositorie>();
builder.Services.AddAutoMapper(typeof(UserMapToDto));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
